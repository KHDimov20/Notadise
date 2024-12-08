using Microsoft.AspNetCore.Mvc;
using Notadise.Models;

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

            return View(model);
        }
    }
}
