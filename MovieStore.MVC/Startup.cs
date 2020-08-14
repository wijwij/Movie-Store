using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.Infrastructure.Data;
using MovieStore.Infrastructure.Repositories;
using MovieStore.Infrastructure.Services;
using MovieStore.MVC.Helpers;

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
            // Add services for controllers. This method will not register services for pages.
            services.AddControllersWithViews();
            // Register DB context as a service.
            // var conn = Configuration.GetSection("ConnectionStrings")["MovieStoreDbConnection"];
            services.AddDbContext<MoviesStoreDbContext>(options => options.UseSqlServer(connectionString:Configuration.GetConnectionString("MovieStoreDbConnection")));
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            // DI in ASP.NET Core has 3 types of lifetimes, scoped, singleton, transient.
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();
            
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IGenreService, GenreService>();

            services.AddScoped<IMovieGenreRepository, MovieGenreRepository>();
            services.AddScoped<IMovieGenreService, MovieGenreService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICryptoService, CryptoService>();

            services.AddScoped<IFavoriteRepository, FavoriteRepository>();

            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<IPurchaseService, PurchaseService>();

            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IReviewService, ReviewService>();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "MovieStoreAuthCookie";
                    options.ExpireTimeSpan = TimeSpan.FromDays(2);
                    options.LoginPath = "/Account/Login";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseMovieStoreExceptionMiddleware();
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

            app.UseAuthentication();
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