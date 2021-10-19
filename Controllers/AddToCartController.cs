using PRN211_Project_OBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRN211_Project_OBS.Controllers
{
    public class AddToCartController : Controller
    {
        Model db = new Model();
        // GET: AddToCart
        public ActionResult Index()
        {
            List<Orderline> orderlinesList;
            orderlinesList = (List<Orderline>)Session["Cart"];
            if (orderlinesList == null)
            {
                orderlinesList = new List<Orderline>();
            } 
            int id = Convert.ToInt32(Request.Params["id"]);

            return View();
        }

        public void AddOrderline(int id, int quantity)
        {
            //Thinking
        }
    }
}