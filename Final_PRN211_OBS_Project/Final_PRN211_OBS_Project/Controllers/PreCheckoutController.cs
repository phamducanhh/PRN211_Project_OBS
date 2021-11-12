using Final_PRN211_OBS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class PreCheckoutController : Controller
    {
        [HttpPost]
        // GET: PreCheckout
        public ActionResult Index()
        {

            string[] qua = Request.Params.GetValues("quantity");
           

            List<Orderline> list = (List<Orderline>)Session["cart"];
            for (int i = 0; i < list.Count; i++)
            {
                list[i].quantity = Int32.Parse(qua[i]);
            }
            Session["cart"] = list;

            return RedirectToAction("Checkout","Cart");
        }
    }
}