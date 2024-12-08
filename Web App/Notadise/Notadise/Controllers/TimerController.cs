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
            // Масив от пътища на фоновото изображение 
            string[] backgrounds = { "img1.jpg", "img2.jpg", "img3.jpg", "img4.jpg" };

            // Изберете произволен фон 
            Random rand = new Random();
            string selectedBackground = $"/img/{backgrounds[rand.Next(backgrounds.Length)]}";

            // Предаване на фоновото изображение на изгледа с помощта на ViewModel
            var model = new PomodoroViewModel
            {
                BackgroundImage = selectedBackground
            };

            // Стартиране на фонова нишка за регистриране 
            Thread logThread = new Thread(() =>
            {
                Console.WriteLine($"[{DateTime.Now}] Pomodoro page accessed with background {selectedBackground}");
            });
            logThread.IsBackground = true;
            logThread.Start();

            // Стартиране на фонова нишка за симулирана задача на таймера 
            Thread timerThread = new Thread(() =>
            {
                Console.WriteLine($"[{DateTime.Now}] Timer started.");
                Thread.Sleep(1000 * 30 * 60); // Симулиране на 30-минутен таймер Pomodoro 
                Console.WriteLine($"[{DateTime.Now}] Timer completed.");
            });
            timerThread.IsBackground = true;
            timerThread.Start();

            return View(model);
        }
    }
}
