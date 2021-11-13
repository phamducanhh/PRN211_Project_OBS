using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_PRN211_OBS_Project.Models;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class UnseenBillController : Controller
    {
        // GET: UnseenBill
        DAO dao = new DAO();
        public void Access()
        {
            User x = (User)Session["user"];
            if (!dao.isAble(x, "Manager"))
            {
                Response.Redirect("/Error");
            }
        }
        public ActionResult Index()
        {
            Access();
            List<Bill> list = dao.GetUnseenBill();
            int pageSize = list.Count % 10 == 0 ? list.Count / 10 : list.Count / 10 + 1;
            int currentPage;
            try
            {
                currentPage = Int32.Parse(Request.Params["page"]);
            }
            catch (Exception e)
            {
                currentPage = 1;
            }
            ViewBag.ListUnseenBill = list.GetRange(10 * (currentPage - 1), 10 * currentPage > list.Count ? list.Count % 10 : 10);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            ViewBag.Url = "/UnseenBill/Index";
            return View();
        }
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

            dao.ChangeStatus(id,"Rejected");
            return Redirect("/UnseenBill/Index");
        }
        [HttpGet]
        public RedirectResult ProcessingBill()
        {
            int id = Convert.ToInt32(Request.Params["id"]);

            dao.ChangeStatus(id, "Process");
            return Redirect("/UnseenBill/Index");
        }
    }
}