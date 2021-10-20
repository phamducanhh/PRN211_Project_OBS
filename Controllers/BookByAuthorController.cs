using PRN211_Project_OBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRN211_Project_OBS.Controllers
{
    public class BookByAuthorController : Controller
    {
        Model db = new Model();

        public List<Book> GetBooksByAuthor(int auid)
        {
            return db.Books.SqlQuery($"SELECT * FROM [OnlineBookShop].[dbo].[Book] where author_id = {auid}").Distinct().ToList();
        }

        // GET: BookByAuthor
        [HttpGet]
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Request.Params["author_id"]);
            List<Book> list = GetBooksByAuthor(id);
            int pageSize = list.Count % 6 == 0 ? list.Count / 6 : list.Count / 6 + 1;
            int currentPage;
            try
            {
                currentPage = Int32.Parse(Request.Params["page"]);
            }
            catch (Exception e)
            {
                currentPage = 1;
            }
            ViewBag.id = id;
            ViewBag.ListBook = list.GetRange(6 * (currentPage - 1), 6 * currentPage > list.Count ? list.Count % 6 : 6);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            return View();
        }
    }
}