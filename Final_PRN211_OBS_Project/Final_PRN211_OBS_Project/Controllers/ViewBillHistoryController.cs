using Final_PRN211_OBS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class ViewBillHistoryController : Controller
    {
        DAO dao = new DAO();
        // GET: ViewBillHistory
        public ActionResult Index()
        {
            ViewBag.list = dao.GetBills();
            return View();
        }
        [HttpGet]
        public ActionResult BillDetail()
        {
            string id = Request.Params["id"];
            ViewBag.list = dao.GetOrderlinesInBill(id);
            return View();
        }
    }
}