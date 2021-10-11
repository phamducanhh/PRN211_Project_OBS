using PRN211_Project_OBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRN211_Project_OBS.Controllers
{
    public class GenreController : Controller
    {
        Model db = new Model();

        public List<Book> GetBookByGenre(string id)
        {
            string sql = "select id,title,image_url,author_id,[description],price from "
                       +$"(select * from Book_Genre bg inner join Book b on (bg.book_id = b.id) where genre_id = {id}) x";
            return db.Books.SqlQuery(sql).ToList();
        }

        public Genre GetGenreById(string id)
        {
            return db.Genres.SqlQuery($"select * from Genre where id = {id}").First();
        }

        // GET: Genre
        public ActionResult Index()
        {
            string genre_id = Request.Params["genre_id"];
            List<Book> list = GetBookByGenre(genre_id);
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
            Genre genre = GetGenreById(genre_id);
            ViewBag.ListBook = list.GetRange(6 * (currentPage - 1), 6 * currentPage > list.Count ? list.Count%6 : 6);
            ViewBag.Genre = genre;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            return View();
        }
    }
}