using Final_PRN211_OBS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class CartController : Controller
    {
        DAO dao = new DAO();
        // GET: Cart
        public void Access()
        {
            User x = (User)Session["user"];
            if (!dao.isAble(x, "Customer"))
            {
                Response.Redirect("/Error");
            }
        }

        [HttpPost]
        public RedirectResult Add(string quantity, string url)
        {           
            User x = (User)Session["user"];
            if (x == null)
            {
                return Redirect("/SignIn/Index");
            }
            Access();
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
            Access();
            int id = Convert.ToInt32(Request.Params["book_id"]);
            List<Orderline> cart = (List<Orderline>)Session["cart"];
            cart = dao.RemoveFromCart(cart, id);
            Session["cart"] = cart;
            return Redirect(url);
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            Access();
            ViewBag.ListGenre = dao.GetGenres();
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(string address, string tel, string payment, string total)
        {
            Access();
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