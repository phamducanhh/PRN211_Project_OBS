using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRN211_Project_OBS.Models;

namespace PRN211_Project_OBS.Controllers
{
    public class CartController : Controller
    {
        DAO dao = new DAO();
        // GET: Cart
        [HttpPost]
        public RedirectResult Add(string quantity, string url)
        {
            User x = (User) Session["user"];
            if(x==null)
            {
                ViewBag.Mess = "Must login to buy Books";
                return Redirect("/SignIn/Index");
            }
            List<Orderline> cart = (List<Orderline>)Session["cart"];
            if (cart == null) cart = new List<Orderline>();
            int id = Convert.ToInt32(Request.Params["book_id"]);
            cart = dao.AddToCart(cart, id, Convert.ToInt32(quantity));
            Session["cart"] = cart;
            return Redirect(url);
        }

        [HttpPost]
        public RedirectResult Remove(string url)
        {
            int id = Convert.ToInt32(Request.Params["book_id"]);
            List<Orderline> cart = (List<Orderline>)Session["cart"];
            cart = dao.RemoveFromCart(cart, id);
            Session["cart"] = cart;
            return Redirect(url);
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            ViewBag.ListGenre = dao.GetGenres();
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(string address, string tel, string payment, string total)
        {
            User x = (User)Session["user"];
            List<Orderline> orderlines = (List<Orderline>)Session["cart"];
            dao.AddBill(new Bill { user_id = x.id, address = address, telephone = tel, payment = payment, total = Double.Parse(total) });
            foreach (var item in orderlines)
            {
                dao.AddOrderline(item);
            }
            Session["cart"] = new List<Orderline>();
            return RedirectToAction("Index", "Home");
        }
    }
}