using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Data
{
    /// <summary>
    /// The SecurityContext is the database context for the Identity service.
    /// </summary>
    public class SecurityContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// The contsurctor takes in the database context options.
        /// </summary>
        /// <param name="options"></param>
        public SecurityContext(DbContextOptions<SecurityContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// The OnModelCreating method allows for customizations to the Identity model.
        /// No customizations were included.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
