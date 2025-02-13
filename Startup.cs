using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcMovie.Data;

namespace MvcMovie
{
    /// <summary>
    /// The Startup class defines the initial starting conditions for the web application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The constuctor provides the configuration to the application.
        /// It takes in a configuration object.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// The Configuration is read only.
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// The ConfigureServices method is used to add services.
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MvcMovieContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MvcMovieContext")));

            services.AddDbContext<SecurityContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SecurityContext")));
            services.AddControllersWithViews();
            services.AddSession();
            services.AddRazorPages();
        }

        /// <summary>
        /// The Configure method configures the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
