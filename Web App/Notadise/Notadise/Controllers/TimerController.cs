using Microsoft.AspNetCore.Mvc;

namespace YourApp.Controllers
{
    public class TimerController : Controller
    {
        public IActionResult Pomodoro()
        {
            return View();
        }
    }
}
