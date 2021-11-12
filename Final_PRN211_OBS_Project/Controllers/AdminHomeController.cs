using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_PRN211_OBS_Project.Models;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class AdminHomeController : Controller
    {
        DAO dao = new DAO();
        Model db = new Model();
        public void Access()
        {
            User x = (User)Session["user"];
            if (!dao.isAble(x, "Admin"))
            {
                Response.Redirect("/Error");
            }
        }
        //List<Book> books = new List<Book>();
        // GET: AdminHome

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

        public ActionResult AddBook()
        {
            Access();
            ViewBag.GenreList = dao.GetGenres();
            ViewBag.AuthorList = dao.GetAllAuthors();
            return View();
        }

        [HttpPost]
        public ActionResult AddBook(string title, string imageUrl, int author, int[] genre, string description, float price)
        {
            Access();
            Book book = new Book
            {
                title = title,
                image_url = imageUrl,
                author_id = author,
                description = description,
                price = price,
                status = true
            };
            db.Books.Add(book);
            db.SaveChanges();
            var listAll = dao.GetBooks();
            listAll.Reverse();
            int bookId = listAll[0].id;
            for (int i = 0; i < genre.Length; i++)
            {
                dao.InsertGenreBook(bookId, genre[i]);
            }
            db.SaveChanges();
            db.Stocks.Add(new Stock { book_id = bookId, quantity = 0 });
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult EditBook()
        {
            Access();
            string id = Request.Params["id"];
            ViewBag.GenreList = dao.GetGenres();
            ViewBag.AuthorList = dao.GetAllAuthors();
            ViewBag.Book = dao.GetBookById(id);
            ViewBag.GenreListOfBook = dao.GetGenreByBookId(id);
            return View();
        }

        [HttpPost]
        public ActionResult EditBook(string id, string title, string imageUrl, int author, int[] genre, string description, float price, string visible)
        {
            Access();
            Book book = dao.GetBookById(id);
            int status;
            status = visible.Equals("1") ? 1 : 0;
            dao.ChangeStatus(book.id, status);
            if (imageUrl.Length == 0) imageUrl = book.image_url;
            db.Database.ExecuteSqlCommand($"update Book set title = '{title}', image_url = '{imageUrl}', author_id = {author}, " +
                $"description = N'{description}', price={price} where id={book.id}");
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

        public ActionResult DeleteBook()
        {
            Access();
            int id = Int32.Parse(Request.Params["id"]);
            if (dao.GetOrderlineByBookId(id).Count == 0 && dao.GetStockByBookId(id).quantity == 0) dao.DeleteBookById(id);
            else dao.ChangeStatus(id, 0);
            return RedirectToAction("Index", "AdminHome");
        }
    }
}