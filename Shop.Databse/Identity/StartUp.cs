using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shop.Database.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Database.Identity
{
    public static class StartUp
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
             .AddEntityFrameworkStores<AppDbContext>()
             .AddClaimsPrincipalFactory<AppUserClaimsPrincipalFactory>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/accessDenied";
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".Fiver.Security.Cookie",
                    Path = "/",
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
            });

            services.AddScoped<DatabaseInitializer>();
        }
    }
}
