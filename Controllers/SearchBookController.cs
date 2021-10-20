using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRN211_Project_OBS.Models;

namespace PRN211_Project_OBS.Controllers
{
    public class SearchBookController : Controller
    {
        Model db = new Model();

        public List<Book> GetBooksByName(string name)
        {
            try
            {
                if (name.Equals("")) return db.Books.SqlQuery("select * from Book").ToList();
                return db.Books.SqlQuery($"SELECT * FROM [OnlineBookShop].[dbo].[Book] where title like '%{name}%'").ToList();
            }
            catch
            {
                return null;
            }
        }
        // GET: SearchBook
        [HttpGet]
        public ActionResult Index()
        {
            string searchContent = Request.Params["searchContent"];
            List<Book> slist = GetBooksByName(searchContent);
            int pageSize = slist.Count % 6 == 0 ? slist.Count / 6 : slist.Count / 6 + 1;
            int currentPage;
            try
            {
                currentPage = Int32.Parse(Request.Params["page"]);
            }
            catch
            {
                currentPage = 1;
            }
            ViewBag.ListBook = slist.GetRange(6 * (currentPage - 1), 6 * currentPage > slist.Count ? slist.Count % 6 : 6);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            ViewBag.sc = searchContent;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string searchContent)
        {
            List<Book> slist = GetBooksByName(searchContent);
            int pageSize = slist.Count % 6 == 0 ? slist.Count / 6 : slist.Count / 6 + 1;
            int currentPage;
            try
            {
                currentPage = Int32.Parse(Request.Params["page"]);
            }
            catch (Exception e)
            {
                currentPage = 1;
            }
            ViewBag.ListBook = slist.GetRange(6 * (currentPage - 1), 6 * currentPage > slist.Count ? slist.Count % 6 : 6);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            ViewBag.sc = searchContent;
            return View();
        }
    }
}