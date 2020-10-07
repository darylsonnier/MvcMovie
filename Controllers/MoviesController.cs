using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var movies = from m in _context.Movie select m;
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            return View(await movies.ToListAsync());
        }

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

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems") != null ? HttpContext.Session.GetInt32("totalItems") : 0;
            return View();
        }

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

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}
