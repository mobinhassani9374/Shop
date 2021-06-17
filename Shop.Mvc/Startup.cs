using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Database;
using Shop.Database.Identity;
using Shop.Mvc.Mapping;
using Shop.Mvc.Middleware;
using Shop.Services;
using Shop.Services.Messaging.FarazSms;

namespace Shop.Mvc
{
    public class Startup
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public Startup(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(@"Data Source=185.51.200.186\SQL2014,2014;Initial Catalog=imenForoshTest;Persist Security Info=True;User ID=mobin_imen; Password=Ju#82c8c; MultipleActiveResultSets=True");
            });

            Database.Identity.StartUp.ConfigureServices(services,_hostingEnvironment);

            Services.StartUp.ConfigureServices(services);

            services.AddHttpClient();

            services.AddScoped<SmsService>();
            services.AddScoped<Services.Payment.IdPay.Payment>();

            services.AddMvc();

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AdminService adminService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            Cache.CacheManager.SetInfo(adminService.GetLastInfo().ToViewModel());

            app.UseMiddleware<LogServiceMiddleware>();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
