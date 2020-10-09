using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MvcMovie.Helpers;
using MvcMovie.Models;
using MvcMovie.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore;

namespace MvcMovie.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly MvcMovieContext _context;

        public CartController(MvcMovieContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            if (!HttpContext.Session.TryGetValue("cart", out byte[] value))
            {
                ViewBag.totalItems = 0;
                return View("EmptyCart");
            }
            else
            {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                ViewBag.cart = cart.OrderBy(x => x.Movie.Title);
                ViewBag.subtotal = cart.Sum(item => item.Movie.Price * item.Quantity);
                ViewBag.tax = Math.Round(ViewBag.subtotal * (decimal)0.08, 2);
                ViewBag.discount = ViewBag.subtotal / (decimal)50 >= (decimal)1.0 ? (decimal)5.00 * Math.Truncate(ViewBag.subtotal / (decimal)50) : (decimal)0.0;
                ViewBag.shipping = ViewBag.subtotal > (decimal)50.0 ? (decimal)0.00 : (decimal)4.99;
                ViewBag.total = Math.Round(ViewBag.subtotal + ViewBag.tax + ViewBag.shipping - ViewBag.discount, 2);
                ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems");
                return View();
            }

        }

        public IActionResult FinalizePurchase()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.subtotal = cart.Sum(item => item.Movie.Price * item.Quantity);
            ViewBag.tax = Math.Round(ViewBag.subtotal * (decimal)0.08, 2);
            ViewBag.discount = ViewBag.subtotal / (decimal)50 >= (decimal)1.0 ? (decimal)5.00 * Math.Truncate(ViewBag.subtotal / (decimal)50) : (decimal)0.0;
            ViewBag.shipping = ViewBag.subtotal > (decimal)50.0 ? (decimal)0.00 : (decimal)4.99;
            ViewBag.total = Math.Round(ViewBag.subtotal + ViewBag.tax + ViewBag.shipping - ViewBag.discount, 2);
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems");
            return View();
        }

         public ActionResult Invoice(PurchaseModel model)
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (model.ShipName == null || model.ShipAdd1 == null || model.ShipState.ToString() == string.Empty || model.ShipZip == null)
            {
                ViewBag.ShipName = model.BillName;
                ViewBag.ShipAdd1 = model.BillAdd1;
                ViewBag.ShipAdd2 = model.BillAdd2;
                ViewBag.ShipState = model.BillState;
                ViewBag.ShipZip = model.BillZip;
            }
            else
            {
                ViewBag.ShipName = model.ShipName;
                ViewBag.ShipAdd1 = model.ShipAdd1;
                ViewBag.ShipAdd2 = model.ShipAdd2;
                ViewBag.ShipState = model.ShipState;
                ViewBag.ShipZip = model.ShipZip;
            }
            
            ViewBag.BillName = model.BillName;
            ViewBag.BillAdd1 = model.BillAdd1;
            ViewBag.BillAdd2 = model.BillAdd2;
            ViewBag.BillState = model.BillState;
            ViewBag.BillZip = model.BillZip;
            ViewBag.cart = cart.OrderBy(x => x.Movie.Title);
            ViewBag.subtotal = cart.Sum(item => item.Movie.Price * item.Quantity);
            ViewBag.tax = Math.Round(ViewBag.subtotal * (decimal)0.08, 2);
            ViewBag.discount = ViewBag.subtotal / (decimal)50 >= (decimal)1.0 ? (decimal)5.00 * Math.Truncate(ViewBag.subtotal / (decimal)50) : (decimal)0.0;
            ViewBag.shipping = ViewBag.subtotal > (decimal)50.0 ? (decimal)0.00 : (decimal)4.99;
            ViewBag.total = Math.Round(ViewBag.subtotal + ViewBag.tax + ViewBag.shipping - ViewBag.discount, 2);
            HttpContext.Session.Clear();
            HttpContext.Session.SetInt32("totalItems", 0);
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems");
            return View();
        }

        public IActionResult Buy(string id, string goBack)
        {
            var Movies = from m in _context.Movie select m;
            Movie Movie = new Movie();
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<Item>();
                cart.Add(new Item { Movie = Movies.Where(x => x.Title == id).FirstOrDefault(), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                HttpContext.Session.SetInt32("totalItems", cart.Select(x => x.Quantity).ToList().Sum());
            }
            else
            {
                List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Movie = Movies.Where(x => x.Title == id).FirstOrDefault(), Quantity = 1 });
                }
                HttpContext.Session.SetInt32("totalItems", cart.Select(x => x.Quantity).ToList().Sum());
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            // Redirecting back to page, so add to cart doesn't automatically go to the shopping cart.  Need to hit the cart button in the menu bar to access the cart.
            //return RedirectToAction("Index");
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems");
            return RedirectToAction("MoviesByGenre", "Movies", new { @id = goBack });
        }

        public IActionResult Remove(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            HttpContext.Session.SetInt32("totalItems", cart.Select(x => x.Quantity).ToList().Sum());
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems");
            return RedirectToAction("Index");
        }

        public IActionResult Increment(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart[index].Quantity++;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            HttpContext.Session.SetInt32("totalItems", cart.Select(x => x.Quantity).ToList().Sum());
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems");
            return RedirectToAction("Index");
        }

        public IActionResult Decrement(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart[index].Quantity--;
            if (cart[index].Quantity < 1)
            {
                cart.RemoveAt(index);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            HttpContext.Session.SetInt32("totalItems", cart.Select(x => x.Quantity).ToList().Sum());
            ViewBag.totalItems = HttpContext.Session.GetInt32("totalItems");
            return RedirectToAction("Index");
        }

        private int isExist(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Movie.Title.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
