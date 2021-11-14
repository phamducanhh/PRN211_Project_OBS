using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_PRN211_OBS_Project.Models;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class AdminReviewController : Controller
    {
        DAO dao = new DAO();
        public void Access()
        {
            User x = (User)Session["user"];
            if (!dao.isAble(x, "Admin"))
            {
                Response.Redirect("/Error");
            }
        }
        // GET: AdminReview
        public ActionResult Index()
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
            ViewBag.Books = list.GetRange(10 * (currentPage - 1), 10 * currentPage > list.Count ? list.Count % 10 : 10);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            return View();
        }

        public ActionResult Detail()
        {
            Access();
            string id = Request.Params["id"];
            List<Review> list = dao.GetReviewOfBookID(id);
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
            ViewBag.Reviews = list.GetRange(10 * (currentPage - 1), 10 * currentPage > list.Count ? list.Count % 10 : 10);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            ViewBag.ID = id;
            return View();
        }

        public RedirectResult Delete()
        {
            Access();
            string id = Request.Params["id"];
            string book_id = Request.Params["book_id"];
            string page = Request.Params["page"];
            dao.DeleteReview(id);
            return Redirect($"/AdminReview/Detail?id={book_id}&page={page}");
        }
    }
}