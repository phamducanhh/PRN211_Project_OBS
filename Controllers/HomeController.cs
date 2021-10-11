using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRN211_Project_OBS.Models;

namespace PRN211_Project_OBS.Controllers
{
    public class HomeController : Controller
    {
        Model db = new Model();

        public List<Book> GetNewestBook()
        {
            return db.Books.SqlQuery("SELECT TOP 7 * FROM [OnlineBookShop].[dbo].[Book] ORDER BY id DESC").ToList();
        }

        public List<Book> GetBestSellerBook()
        {
            String sql = "select id,title,image_url,author_id,[description],price from"
                  +"(SELECT top 7 b.id, b.title, b.[image_url], b.author_id, b.[description], b.price, Sum(quantity) as Qty"
                  +" from Orderline o inner join Book b on (o.book_id = b.id)"
                  +" group by b.id, b.title, b.[image_url], b.author_id, b.[description], b.price"
                  +" order by Qty desc, b.price desc) x";
            return db.Books.SqlQuery(sql).ToList();
        }

        public List<Genre> GetGenres()
        {
            return db.Genres.SqlQuery("SELECT * FROM [OnlineBookShop].[dbo].[Genre]").ToList();
        }

        public ActionResult Index()
        {
            
            return View();
        }
    }
}