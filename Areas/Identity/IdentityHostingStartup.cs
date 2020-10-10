#pragma warning disable 1591
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;

[assembly: HostingStartup(typeof(MvcMovie.Areas.Identity.IdentityHostingStartup))]
namespace MvcMovie.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<SecurityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SecurityContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<SecurityContext>();
            });
        }
    }
}