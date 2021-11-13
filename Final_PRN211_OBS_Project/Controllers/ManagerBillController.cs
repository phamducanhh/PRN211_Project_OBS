using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_PRN211_OBS_Project.Models;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class ManagerBillController : Controller
    {
        DAO dao = new DAO();

        public void Access()
        {
            User x = (User)Session["user"];
            if (!dao.isAble(x, "Manager"))
            {
                Response.Redirect("/Error");
            }
        }
        // GET: AdminBill
        [HttpGet]
        public ActionResult Index()
        {
            Access();
            List<Bill> list = dao.GetBills();
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
            ViewBag.ListBill = list.GetRange(10 * (currentPage - 1), 10 * currentPage > list.Count ? list.Count % 10 : 10);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            ViewBag.Url = "/ManagerBill/Index";
            return View();
        }

        [HttpPost]
        public RedirectResult ChangeStatus(string url)
        {
            Access();
            int id = Convert.ToInt32(Request.Params["id"]);
            string status = Request.Params["selectStatus"];
            var ListBookInBill = dao.GetBillDeltai(id);
            if (status.Equals("Done"))
            {
                foreach (var item in ListBookInBill)
                {
                    dao.UpdateStock(item.book_id.ToString(), -item.quantity);
                }
            }
            dao.ChangeStatus(id, status);
            return Redirect(url);
        }
        [HttpGet]
        public ActionResult BillDetail()
        {
            Access();
            int id = Convert.ToInt32(Request.Params["id"]);
            ViewBag.BillDetail = dao.GetBillDeltai(id);
            return View();
        }
    }
}