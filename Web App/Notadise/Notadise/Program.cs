using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notadise.Areas.Identity.Data;
using Notadise.Data;
using System.Threading;

namespace Notadise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("NotadiseDbContextConnection")
                ?? throw new InvalidOperationException("Connection string 'NotadiseDbContextConnection' not found.");

            builder.Services.AddDbContext<NotadiseDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
                options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<NotadiseDbContext>();

            // Добавяне на услуги към контейнера.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Пример: Няколко нишки за различни фонови задачи
            Thread thread1 = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Thread 1: Task A is running...");
                    Thread.Sleep(3000); // Симулиране на периодична работа на всеки 3 секунди
                }
            })
            {
                IsBackground = true
            };
            thread1.Start();

            Thread thread2 = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Thread 2: Task B is running...");
                    Thread.Sleep(5000); // Симулиране на периодична работа на всеки 5 секунди
                }
            })
            {
                IsBackground = true
            };
            thread2.Start();

            Thread thread3 = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Thread 3: Task C is running...");
                    Thread.Sleep(7000); // Симулиране на периодична работа на всеки 5 секунди
                }
            })
            {
                IsBackground = true
            };
            thread3.Start();

            // Конфигуриране на конвейера за HTTP заявки.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //Пренасочване на HTTP заявките към HTTPS за осигуряване на сигурна комуникация
            app.UseHttpsRedirection();

            // Разрешаване на обслужването на статични файлове (например изображения, CSS, JavaScript) от папката wwwroot 
            app.UseStaticFiles();

            // Добавяне на възможности за маршрутизиране към приложението, което му позволява да обработва входящи HTTP заявки 
            app.UseRouting();

            //// Активиране на междинния софтуер за оторизация за прилагане на политики за оторизация (гарантира, че само оторизирани потребители имат достъп до определени ресурси) 
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
