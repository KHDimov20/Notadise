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

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Example: Multiple threads for different background tasks
            Thread thread1 = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("Thread 1: Task A is running...");
                    Thread.Sleep(3000); // Simulate periodic work every 3 seconds
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
                    Thread.Sleep(5000); // Simulate periodic work every 5 seconds
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
                    Thread.Sleep(7000); // Simulate periodic work every 7 seconds
                }
            })
            {
                IsBackground = true
            };
            thread3.Start();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
