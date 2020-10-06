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
                return View("EmptyCart");
            }
            else
            {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                ViewBag.cart = cart;
                ViewBag.subtotal = cart.Sum(item => item.Movie.Price * item.Quantity);
                ViewBag.tax = Math.Round(ViewBag.subtotal * (decimal)0.08, 2);
                ViewBag.discount = ViewBag.subtotal / (decimal)50 >= (decimal)1.0 ? (decimal)5.00 * Math.Truncate(ViewBag.subtotal / (decimal)50) : (decimal)0.0;
                ViewBag.shipping = ViewBag.subtotal > (decimal)50.0 ? (decimal)0.00 : (decimal)4.99;
                ViewBag.total = Math.Round(ViewBag.subtotal + ViewBag.tax + ViewBag.shipping - ViewBag.discount, 2);
                try
                {
                    ViewBag.last = cart.Last().Movie.Genre.ToString().Split(",").First();
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
            ViewBag.discount = ViewBag.subtotal / (decimal)50 >= (decimal)1.0 ? (decimal)5.00 * Math.Truncate(ViewBag.subtotal / (decimal)50) : (decimal)0.0;
            ViewBag.shipping = ViewBag.subtotal > (decimal)50.0 ? (decimal)0.00 : (decimal)4.99;
            ViewBag.total = Math.Round(ViewBag.subtotal + ViewBag.tax + ViewBag.shipping - ViewBag.discount, 2);
            return View();
        }

        public ActionResult Invoice(PurchaseModel model)
        {
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (model.billName == null) ViewBag.billName = model.shipName;
            else ViewBag.billName = model.billName;
            if (model.billAdd1 == null) ViewBag.billAdd1 = model.shipAdd1;
            else ViewBag.billAdd1 = model.billAdd1;
            if (model.billAdd2 == null) ViewBag.billAdd2 = model.shipAdd2;
            else ViewBag.billAdd2 = model.billAdd2;
            if (model.billState.ToString() == string.Empty) ViewBag.billState = model.shipState;
            else ViewBag.billState = model.billState;
            if (model.billZip == null) ViewBag.billZip = model.shipZip;
            else ViewBag.billZip = model.billZip;
            
            ViewBag.shipName = model.shipName;
            ViewBag.shipAdd1 = model.shipAdd1;
            ViewBag.shipAdd2 = model.shipAdd2;
            ViewBag.shipState = model.shipState;
            ViewBag.shipZip = model.shipZip;
            ViewBag.cart = cart;
            ViewBag.subtotal = cart.Sum(item => item.Movie.Price * item.Quantity);
            ViewBag.tax = Math.Round(ViewBag.subtotal * (decimal)0.08, 2);
            ViewBag.discount = ViewBag.subtotal / (decimal)50 >= (decimal)1.0 ? (decimal)5.00 * Math.Truncate(ViewBag.subtotal / (decimal)50) : (decimal)0.0;
            ViewBag.shipping = ViewBag.subtotal > (decimal)50.0 ? (decimal)0.00 : (decimal)4.99;
            ViewBag.total = Math.Round(ViewBag.subtotal + ViewBag.tax + ViewBag.shipping - ViewBag.discount, 2);
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Buy(string id)
        {
            var Movies = from m in _context.Movie select m;
            Movie Movie = new Movie();
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<Item>();
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
