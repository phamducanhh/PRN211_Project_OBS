using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRN211_Project_OBS.Models;

namespace PRN211_Project_OBS.Controllers
{
    public class FilterController : Controller
    {
        Model db = new Model();

        public Genre GetGenreByID(int id)
        {
            return db.Genres.SqlQuery($"select * from Genre where id = {id} ").First();
        }

        public List<Book> getBookByFilter(string[] arr)
        {
            if (arr == null)
            {
                return db.Books.SqlQuery("select * from Book").ToList();
            }
            string sql = "select b.id, b.title, b.image_url, b.author_id, b.[description], b.price from (Book b inner join Book_Genre bg on (b.id = bg.book_id)) where ";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == arr.Length - 1) sql += $"genre_id = {arr[i]} ";
                else sql += $"genre_id = {arr[i]} or ";
            }
            return db.Books.SqlQuery(sql).ToList().Distinct().ToList();
        }

        // GET: Filter
        [HttpGet]
        public ActionResult Index()
        {
            string[] checkCate = Request.Params.GetValues("checkCate");
            string tagnav = "";
            string url_part = "";
            for (int i = 0; i < checkCate.Length; i++)
            {
                url_part += $"checkCate={checkCate[i]}&";
                if (i == checkCate.Length - 1) tagnav += GetGenreByID(Int32.Parse(checkCate[i])).name;
                else tagnav += tagnav += GetGenreByID(Int32.Parse(checkCate[i])).name + ", ";
            }
            string tag = "";
            for (int i = 0; i < checkCate.Length; i++)
            {
                tag += checkCate[i];
            }
            List<Book> list = getBookByFilter(checkCate);
            int pageSize = list.Count % 6 == 0 ? list.Count / 6 : list.Count / 6 + 1;
            int currentPage;
            try
            {
                currentPage = Int32.Parse(Request.Params["page"]);
            }
            catch (Exception)
            {
                currentPage = 1;
            }
            ViewBag.ListBook = list.GetRange(6 * (currentPage - 1), 6 * currentPage > list.Count ? list.Count % 6 : 6);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            ViewBag.Tag = tag;
            ViewBag.TagNav = tagnav;
            ViewBag.Url_Part = url_part;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string[] checkCate)
        {
            string tagnav = "";
            string url_part = "";
            for (int i = 0; i < checkCate.Length; i++)
            {
                url_part += $"checkCate={checkCate[i]}&";
                if(i==checkCate.Length-1) tagnav += GetGenreByID(Int32.Parse(checkCate[i])).name;
                else tagnav += tagnav += GetGenreByID(Int32.Parse(checkCate[i])).name + ", ";
            }
            string tag = ""; 
            for (int i = 0; i < checkCate.Length; i++)
            {
                tag += checkCate[i];
            }
            List<Book> list = getBookByFilter(checkCate);
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
            ViewBag.ListBook = list.GetRange(6 * (currentPage - 1), 6 * currentPage > list.Count ? list.Count % 6 : 6);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            ViewBag.Tag = tag;
            ViewBag.TagNav = tagnav;
            ViewBag.Url_Part = url_part;
            return View();
        }
    }
}