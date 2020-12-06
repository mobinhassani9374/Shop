using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services
{
    public static class StartUp
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<AdminService>();
            services.AddScoped<UserService>();
        }
    }
}
