using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    /// <summary>
    /// The MvcMovieContext class defines the database context for the Movies model.
    /// </summary>
    public class MvcMovieContext : DbContext
    {
        /// <summary>
        /// The constructor takes in DbContextOptions.
        /// </summary>
        /// <param name="options"></param>
        public MvcMovieContext(DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// The Movie field is the DbSet for the context.
        /// </summary>
        public DbSet<Movie> Movie { get; set; }
    }
}