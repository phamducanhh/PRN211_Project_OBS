using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRN211_Project_OBS.Controllers
{
    public class UnseenBillController : Controller
    {
        DAO dao = new DAO();
        // GET: UnseenBill
        public ActionResult Index()
        {
            ViewBag.ListUnseenBill = dao.GetUnseenBill();
            ViewBag.Url = "/UnseenBill/Index";
            return View();
        }
        [HttpGet]
        public ActionResult BillUnseenDetail()
        {
            int id = Convert.ToInt32(Request.Params["id"]);
            ViewBag.BillUnseenDetail = dao.GetBillDeltai(id);
            ViewBag.ID = id;
            return View();
        }
        [HttpGet]
        public RedirectResult RegexBill()
        {
            int id = Convert.ToInt32(Request.Params["id"]);

            dao.DeleteBill(id);
            return Redirect("/UnseenBill/Index");
        }
        [HttpGet]
        public RedirectResult ProcessingBill()
        {
            int id = Convert.ToInt32(Request.Params["id"]);

            dao.ChangeStatus(id,"Process");
            return Redirect("/UnseenBill/Index");
        }
    }
}