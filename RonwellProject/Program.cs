using Microsoft.EntityFrameworkCore;
using RonwellProject.Helpers;
using RonwellProject.Interface;
using RonwellProject.Services;

namespace RonwellProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RonwellDB;Trusted_Connection=True;MultipleActiveResultSets=true"));
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=EmployeeInfoes}/{action=Index}/{id?}");

            app.Run();
        }
    }
}