using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Controllers
{
    /// <summary>
    /// The Movies controller handles the pages for browsing the site's web store.
    /// </summary>
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        /// <summary>
        /// The constructor takes in a database context to provide data to the website.
        /// </summary>
        /// <param name="context"></param>
        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }
        /// <summary>
        /// The Index method returns the genre page, which is the start of the web store experience.
        /// </summary>
        /// <returns></returns>
        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var movies = from m in _context.Movie select m;
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            return View(await movies.ToListAsync());
        }

        /// <summary>
        /// The AdminIndex method provides the admin user with access to pages for CRUD operations.
        /// It takes in search criteria to narrow the view based on genre or title.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        // GET: Movies
        public async Task<IActionResult> AdminIndex(string criteria, string searchString)
        {
            var movies = from m in _context.Movie
                         select m;
            if (criteria == "Title")
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(s => s.Title.Contains(searchString));
                }
            }

            if (criteria == "Genre")
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    movies = movies.Where(s => s.Genre.Contains(searchString));
                }
            }

            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            return View(await movies.ToListAsync());
        }

        /// <summary>
        /// The MoviesByGenre method returns the movie view filtered by the chosen genre.
        /// It takes in an id which is the genre.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Movies by Genre for Users
        public async Task<IActionResult> MoviesByGenre(string id)
        {
            var movies = from m in _context.Movie select m;
            ViewBag.genre = id;
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            if (!string.IsNullOrEmpty(id))
            {
                movies = movies.Where(s => s.Genre.Contains(id));
                return View(await movies.ToListAsync());
            }
            else
            {
                return View("Index");
            }
        }

       /// <summary>
       /// The Details method returns a detail view for CRUD operations.
       /// It takes in an id parameter, which is the title of the movie.
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            return View(movie);
        }

        /// <summary>
        /// The Create method returns a view for creating a new movie entry in the database CRUD operation.
        /// </summary>
        /// <returns></returns>
        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            return View();
        }

        /// <summary>
        /// This entry for the Create method takes in the pertinent fields for the database to create the movie entry.
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price,ImageURL,TrailerURL,Description")] Movie movie)
        {
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdminIndex));
            }
            return View(movie);
        }

        /// <summary>
        /// The Edit method returns a view for editing a movie entry via CRUD operation.
        /// It takes in an id parameter, which is the movie title.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        /// <summary>
        /// This entry for Edit takes in the movie id and binds to the model fields for the movie.  It then updates the database context to save the edit.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="movie"></param>
        /// <returns></returns>
        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,ReleaseDate,Genre,Price,ImageURL,TrailerURL,Description")] Movie movie)
        {
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminIndex));
            }
            return View(movie);
        }

        /// <summary>
        /// The Delete method deletes an entry from the movie database.
        /// It takes in an id, which is the movie title.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        /// <summary>
        /// The DeleteConfirmed method confirms the admin user wishes to delete a movie.  
        /// It takes in an id parameter, which is the movie id to be deleted.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            var movie = await _context.Movie.FindAsync(id);
            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminIndex));
        }

        /// <summary>
        /// The MovieExists method confirms a movie exists in the database.
        /// It takes in an id parameter, which is the private key index for the movie.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
