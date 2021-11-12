using Final_PRN211_OBS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class AuthorAdminController : Controller
    {
        DAO dao = new DAO();
        Model db = new Model();
        // GET: AuthorAdmin
        public ActionResult Index()
        {
            ViewBag.AuthorList = dao.GetAllAuthors();
            return View();
        }

        public ActionResult OwnBook()
        {
            string authId = Request.Params["id"];
            ViewBag.Author = dao.GetAuthorById(authId);
            ViewBag.OwnBook = dao.GetBooksByAuthor(Convert.ToInt32(authId));
            return View();
        }

        [HttpPost]
        public ActionResult AddAuthor(string authorName, string wikiUrl)
        {
            Author author = new Author
            {
                name = authorName,
                wiki_url = wikiUrl
            };
            db.Authors.Add(author);
            db.SaveChanges();
            return RedirectToAction("Index", "AuthorAdmin");
        }
    }
}