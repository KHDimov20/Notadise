using Microsoft.AspNetCore.Mvc;
using Notadise.Models;
using System.Diagnostics;
using System.Threading;

namespace Notadise.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Пример за стартиране на тема за фонова работа 
            Thread thread1 = new Thread(() =>
            {
                _logger.LogInformation("Thread 1: Performing background task in Index...");
                Thread.Sleep(3000); // Симулиране на работа 
                _logger.LogInformation("Thread 1: Task in Index completed.");
            });
            thread1.IsBackground = true;
            thread1.Start();

            return View();
        }

        public IActionResult Privacy()
        {
            // Пример за стартиране на няколко нишки за фонови задачи 
            Thread thread2 = new Thread(() =>
            {
                _logger.LogInformation("Thread 2: Starting task in Privacy...");
                Thread.Sleep(2000); // Симулиране на работа 
                _logger.LogInformation("Thread 2: Privacy task completed.");
            })
            {
                IsBackground = true
            };
            thread2.Start();

            Thread thread3 = new Thread(() =>
            {
                _logger.LogInformation("Thread 3: Another task in Privacy is running...");
                Thread.Sleep(4000); // Симулиране на работа 
                _logger.LogInformation("Thread 3: Another Privacy task completed.");
            })
            {
                IsBackground = true
            };
            thread3.Start();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Асинхронно регистриране на грешка с нишка 
            Thread errorThread = new Thread(() =>
            {
                _logger.LogError("Error thread: Logging error details...");
                Thread.Sleep(1000); // Симулиране на работа 
                _logger.LogError("Error thread: Error details logged.");
            });
            errorThread.IsBackground = true;
            errorThread.Start();

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
