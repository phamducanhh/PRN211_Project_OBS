using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRN211_Project_OBS.Controllers
{
    public class AdminBillController : Controller

    {
        DAO dao = new DAO();
        // GET: AdminBill
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.ListBill = dao.GetAllBill();
            ViewBag.Url = "/AdminBill/Index";
            return View();
        }

        [HttpPost]
        public RedirectResult ChangeStatus(string url)
        {
            int id = Convert.ToInt32(Request.Params["id"]);
            string status = Request.Params["selectStatus"];
            dao.ChangeStatus(id, status);
            return Redirect(url);
        }
        [HttpGet]
        public ActionResult BillDetail()
        {
            int id = Convert.ToInt32(Request.Params["id"]);
            ViewBag.BillDetail = dao.GetBillDeltai(id);
            return View();
        }
    }
}