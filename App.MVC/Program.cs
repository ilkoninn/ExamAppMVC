using App.Business.Services.Impelemtations;
using App.Business.Services.Interfaces;
using App.Core.Entities.Account;
using App.DAL.Context;
using App.DAL.Repositories.Abtractions;
using App.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace App.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IPortfolioService, PortfolioService>();

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();


            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // Default Password settings.
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Default User settings.
                options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            app.MapControllerRoute(
                name: "Admin",
                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
                );

            app.MapControllerRoute(
                name:"Home",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            

            app.UseAuthorization(); 
            app.UseAuthentication();

            app.UseStaticFiles();

            app.Run();
        }
    }
}
