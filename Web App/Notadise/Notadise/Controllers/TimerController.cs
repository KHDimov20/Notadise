using Microsoft.AspNetCore.Mvc;
using Notadise.Models;
using System;
using System.Threading;

namespace Notadise.Controllers
{
    public class TimerController : Controller
    {
        public IActionResult Pomodoro()
        {
            // Array of background image paths
            string[] backgrounds = { "img1.jpg", "img2.jpg", "img3.jpg", "img4.jpg" };

            // Select a random background
            Random rand = new Random();
            string selectedBackground = $"/img/{backgrounds[rand.Next(backgrounds.Length)]}";

            // Pass the background image to the view using a ViewModel
            var model = new PomodoroViewModel
            {
                BackgroundImage = selectedBackground
            };

            // Start a background thread for logging
            Thread logThread = new Thread(() =>
            {
                Console.WriteLine($"[{DateTime.Now}] Pomodoro page accessed with background {selectedBackground}");
            });
            logThread.IsBackground = true;
            logThread.Start();

            // Start a background thread for a simulated timer task
            Thread timerThread = new Thread(() =>
            {
                Console.WriteLine($"[{DateTime.Now}] Timer started.");
                Thread.Sleep(1000 * 25 * 60); // Simulate a 25-minute Pomodoro timer
                Console.WriteLine($"[{DateTime.Now}] Timer completed.");
            });
            timerThread.IsBackground = true;
            timerThread.Start();

            return View(model);
        }
    }
}
