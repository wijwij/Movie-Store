using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieStore.Infrastructure.Data;

namespace MovieStore.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // tell the container we are using EF
            // var conn = Configuration.GetSection("ConnectionStrings")["MovieStoreDbConnection"];
            services.AddDbContext<MoviesStoreDbContext>(options => options.UseSqlServer(connectionString:Configuration.GetConnectionString("MovieStoreDbConnection")));
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                /*
                 * Routing -- Pattern matching technique -- maps your URL to Controller and Action
                 *   1. Convention-based Routing
                 *   2. Attribute Routing
                 */
                endpoints.MapControllerRoute(
                    name: "default",
                    // controller means the URL maps to which Controller and similarly action means the URL maps to which method.
                    // By default, the controller is Home and the action is Index
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}