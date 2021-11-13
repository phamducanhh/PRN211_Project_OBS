using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_PRN211_OBS_Project.Models;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class ManagerDashboardController : Controller
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
        // GET: ManagerDashboard
        public ActionResult Index()
        {
            Access();
            int year = DateTime.Now.Year;
            double[] revenue = new double[12];
            for (int i = 1; i < 13; i++)
            {
                revenue[i - 1] = dao.GetRevenueInMonthYear(i, year);
            }
            List<Genre> genres = dao.GetGenres();
            List<Book> top = dao.GetBestSellerBook().GetRange(0,4);
            List<Book> low = dao.GetLowStockBook();
            ViewBag.ListLowStock = low;
            ViewBag.ListTopSeller = top;
            ViewBag.ListGenre = genres;
            ViewBag.Array = revenue;
            return View();
        }
        [HttpGet]
        public ActionResult Stock()
        {
            Access();
            List<Book> list = dao.GetBooks();
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
            ViewBag.ListBook = list.GetRange(10 * (currentPage - 1), 10 * currentPage > list.Count ? list.Count % 10 : 10);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            return View();
        }

        [HttpGet]
        public void UpdateStock()
        {
            Access();
            string id = Request.Params["book_id"];
            int quantity = Convert.ToInt32(Request.Params["quantity"]);
            dao.UpdateStock(id, quantity);
            Redirect("/ManagerDashboard/Stock");
        }
    }
}