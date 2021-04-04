using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Shop.Database.Identity.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Shop.Database.Identity
{
    public static class StartUp
    {
        public static void ConfigureServices(IServiceCollection services,IHostingEnvironment hostingEnvironment)
        {
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = Domain.Constants.Password_Length;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
             .AddEntityFrameworkStores<AppDbContext>()
             .AddClaimsPrincipalFactory<AppUserClaimsPrincipalFactory>();

            var keysDirectoryName = "Keys";
            var keysDirectoryPath = Path.Combine(hostingEnvironment.WebRootPath, keysDirectoryName);

            services.AddDataProtection()
              .PersistKeysToFileSystem(new DirectoryInfo(keysDirectoryPath))
              .SetApplicationName("Auth");

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.Cookie.Name = "Auth";
                options.Cookie.HttpOnly = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            services.AddScoped<DatabaseInitializer>();
        }
    }
}
