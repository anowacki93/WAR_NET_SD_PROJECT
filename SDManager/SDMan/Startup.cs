using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SDMan.Context;
using SDMan.Models;
using SDMan.Services;
using SDMan.Services.Interfaces;

namespace SDMan
{
    public class Startup
    {
        
        protected IConfigurationRoot Configuration;

        public Startup()
        {
            


            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddXmlFile("appsettings.xml");
            Configuration = configurationBuilder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SDManDbContext>(builder =>
           {
                builder.UseSqlServer(Configuration["DefaultConnection"]);
            });

            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<SDManDbContext>();

            services.AddIdentity<UserModel, IdentityRole<int>>().AddEntityFrameworkStores<SDManDbContext>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IPriorityService, PriorityService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IIncidentService, IncidentService>();
            services.AddScoped<ILogsSerivce, LogsService>();
            services.AddMvc();
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               // app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
