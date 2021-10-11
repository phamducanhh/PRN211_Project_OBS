using PRN211_Project_OBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRN211_Project_OBS.Controllers
{
    public class BookDetailController : Controller
    {
        Model db = new Model();

        // GET: BookDetail

        public Book GetBookById(string id)
        {
            return db.Books.SqlQuery($"SELECT * FROM Book WHERE id = {id}").First();
        }

        public Author GetAuthorById(string id)
        {
            return db.Authors.SqlQuery($"SELECT * FROM Author WHERE id = {id}").First();
        }

        public List<Genre> GetGenreByBookId(string id)
        {
            string sql = $"select x.id, x.name from(select * from Book_Genre bg inner join Genre g on(bg.genre_id=g.id) where book_id = {id}) x";
            return db.Genres.SqlQuery(sql).ToList();
        }

        public List<Book> GetRelatedBook(Book b)
        {
            List<Book> list = db.Books.SqlQuery($"SELECT * From Book where author_id = {b.author_id} and id != {b.id}").ToList();
            string sql = "select id,title,image_url,author_id,[description],price from" 
                       + " (select * from Book_Genre bg inner join Book b on (bg.book_id = b.id)"
                       + $" where genre_id = {GetGenreByBookId(b.id.ToString()).First().id}"
                       + $" and id != {b.id}) x";
            List<Book> relateByGenre = db.Books.SqlQuery(sql).ToList();
            foreach (var item in relateByGenre)
            {
                list.Add(item);
            }
            if (list.Count > 4) return list.GetRange(0,4);
            return list;
        }

        public ActionResult Index()
        {
            string id = Request.Params["id"];
            ViewBag.Param_Id = id;
            return View();
        }
    }
}