using PRN211_Project_OBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRN211_Project_OBS.Controllers
{
    public class AdminHomeController : Controller
    {
        DAO dao = new DAO();
        Model db = new Model();
        //List<Book> books = new List<Book>();
        // GET: AdminHome
        public ActionResult Index()
        {
            ViewBag.Books = dao.GetBooks();
            return View();
        }

        public ActionResult AddBook()
        {
            ViewBag.GenreList = dao.GetGenres();
            ViewBag.AuthorList = dao.GetAllAuthors();
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(string title, string imageUrl, int author, int[] genre, string description, float price)
        {
            Book book = new Book
            {
                title = title,
                image_url = imageUrl,
                author_id = author,
                description = description,
                price = price
            };
            db.Books.Add(book);
            db.SaveChanges();
            int bookId = dao.GetBookByTitle(title).id;
            for(int i = 0; i < genre.Length; i++)
            {
                dao.InsertGenreBook(bookId, genre[i]);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditBook()
        {
            string id = Request.Params["id"];
            //List<Genre> genreIdArray = dao.GetGenreByBookId(id);
            ViewBag.GenreList = dao.GetGenres();
            ViewBag.AuthorList = dao.GetAllAuthors();
            ViewBag.Book = dao.GetBookById(id);
            ViewBag.GenreListOfBook = dao.GetGenreByBookId(id);
            //ViewBag.GenreByBookID ;
            
            return View();
        }

        [HttpPost]
        public ActionResult EditBook(string id, string title, string imageUrl, int author, int[] genre, string description, float price)
        {
            Book book = dao.GetBookByIntID(Convert.ToInt32(id));
            db.Database.ExecuteSqlCommand($"update Book set image_url = '{imageUrl}', author_id = {author}, " +
                $"description = '{description}', price={price} where id={book.id}");
            db.SaveChanges();
            dao.DeleteBookGenreId(book.id);
            db.SaveChanges();
            for (int i = 0; i < genre.Length; i++)
            {
                dao.InsertGenreBook(book.id, genre[i]);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}