using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_PRN211_OBS_Project.Models;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class ManagerAuthorController : Controller
    {
        // GET: AdminAuthor
        DAO dao = new DAO();
        Model db = new Model();
        public void Access()
        {
            User x = (User)Session["user"];
            if (!dao.isAble(x, "Manager"))
            {
                Response.Redirect("/Error");
            }
        }
        // GET: AuthorAdmin
        public ActionResult Index()
        {
            Access();
            List<Author> list = dao.GetAllAuthors();
            int pageSize = list.Count % 5 == 0 ? list.Count / 5 : list.Count / 5 + 1;
            int currentPage;
            try
            {
                currentPage = Int32.Parse(Request.Params["page"]);
            }
            catch (Exception e)
            {
                currentPage = 1;
            }
            ViewBag.AuthorList = list.GetRange(5 * (currentPage - 1), 5 * currentPage > list.Count ? list.Count % 5 : 5);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            return View();
        }

        public ActionResult OwnBook()
        {
            Access();
            string authId = Request.Params["id"];
            ViewBag.Author = dao.GetAuthorById(authId);
            ViewBag.OwnBook = dao.GetBooksByAuthor(Convert.ToInt32(authId));
            return View();
        }

        [HttpPost]
        public ActionResult AddAuthor(string authorName, string wikiUrl)
        {
            Access();
            Author author = new Author
            {
                name = authorName,
                wiki_url = wikiUrl
            };
            db.Authors.Add(author);
            db.SaveChanges();
            return RedirectToAction("Index", "ManagerAuthor");
        }
        [HttpGet]
        public ActionResult DeleteAuthor()
        {
            Access();
            string id = Request.Params["id"];
            db.Database.ExecuteSqlCommand($"delete from Author where id = '{id}' ");
            db.SaveChanges();
            return RedirectToAction("Index", "ManagerAuthor");
        }

        [HttpPost]
        public RedirectResult UpdateAuthor(string id, string authorName, string wikiUrl)
        {
            Access();
            db.Database.ExecuteSqlCommand($"update Author set [name] = '{authorName}', [wiki_url] = '{wikiUrl}' where [id] = '{id}' ");
            db.SaveChanges();
            return Redirect("/ManagerAuthor/Index");
        }
    }
}