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

namespace MvcMovie.Controllers
{
    public class CartController : Controller
    {
        private readonly MvcMovieContext _context;

        public CartController(MvcMovieContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            byte[] value;
            if (!HttpContext.Session.TryGetValue("cart", out value))
            {
                return View("EmptyCart");
            }
            else
            {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                ViewBag.cart = cart;
                ViewBag.subtotal = cart.Sum(item => item.Movie.Price * item.Quantity);
                ViewBag.tax = Math.Round(ViewBag.subtotal * (decimal)0.08, 2);
                ViewBag.discount = ViewBag.subtotal > (decimal)50.0 ? (decimal)5.00 : (decimal)0.0;
                ViewBag.total = Math.Round(ViewBag.subtotal + ViewBag.tax, 2) - ViewBag.discount;
                try
                {
                    ViewBag.last = cart.Last().Movie.Genre;
                }
                catch
                {
                    ViewBag.last = string.Empty;
                }
                return View();
            }

        }

        public IActionResult FinalizePurchase()
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.subtotal = cart.Sum(item => item.Movie.Price * item.Quantity);
            ViewBag.tax = Math.Round(ViewBag.subtotal * (decimal)0.08, 2);
            ViewBag.discount = ViewBag.subtotal > (decimal)50.0 ? (decimal)5.00 : (decimal)0.0;
            ViewBag.total = Math.Round(ViewBag.subtotal + ViewBag.tax, 2) - ViewBag.discount;
            return View();
        }

        public ActionResult Invoice(PurchaseModel model)
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.billName = model.billName;
            ViewBag.billAdd1 = model.billAdd1;
            ViewBag.billAdd2 = model.billAdd2;
            ViewBag.billState = model.billState;
            ViewBag.billZip = model.billZip;
            ViewBag.shipName = model.shipName;
            ViewBag.shipAdd1 = model.shipAdd1;
            ViewBag.shipAdd2 = model.shipAdd2;
            ViewBag.shipState = model.shipState;
            ViewBag.shipZip = model.shipZip;
            ViewBag.cart = cart;
            ViewBag.subtotal = cart.Sum(item => item.Movie.Price * item.Quantity);
            ViewBag.discount = ViewBag.subtotal > (decimal)50.0 ? (decimal)5.00 : (decimal)0.0;
            ViewBag.tax = Math.Round(ViewBag.subtotal * (decimal)0.08, 2);
            ViewBag.total = Math.Round(ViewBag.subtotal + ViewBag.tax - ViewBag.discount, 2);
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Buy(string id)
        {
            var Movies = from m in _context.Movie select m;
            Movie Movie = new Movie();
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { Movie = Movies.Where(x => x.Title == id).FirstOrDefault(), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
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
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Remove(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Increment(string id)
        {
            List<Item> cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart[index].Quantity++;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
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
