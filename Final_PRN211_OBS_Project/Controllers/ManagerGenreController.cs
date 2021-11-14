using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_PRN211_OBS_Project.Models;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class ManagerGenreController : Controller
    {
        public void Access()
        {
            User x = (User)Session["user"];
            if (!dao.isAble(x, "Manager"))
            {
                Response.Redirect("/Error");
            }
        }
        // GET: AdminGenre
        DAO dao = new DAO();
        public ActionResult Index()
        {
            Access();
            List<Genre> genres = dao.GetGenres();
            ViewBag.Genre = genres;
            return View();
        }

        public ActionResult ListBook()
        {
            Access();
            string id = Request.Params["id"];
            List<Book> list = dao.GetBookByGenre(id);
            int pageSize = list.Count % 10 == 0 ? list.Count / 10 : list.Count / 10 + 1;
            int currentPage;
            try
            {
                currentPage = Int32.Parse(Request.Params["page"]);
            }
            catch (Exception)
            {
                currentPage = 1;
            }
            ViewBag.ListBook = list.GetRange(10 * (currentPage - 1), 10 * currentPage > list.Count ? list.Count % 10 : 10);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
            ViewBag.ID = id;
            return View();
        }

        public ActionResult Delete()
        {
            Access();
            string id = Request.Params["id"];
            dao.DeleteGenre(id);
            return RedirectToAction("Index", "ManagerGenre");
        }

        [HttpPost]
        public ActionResult Add(string genre)
        {
            Access();
            dao.AddGenre(genre);
            return RedirectToAction("Index", "ManagerGenre");
        }
        [HttpPost]
        public ActionResult Edit(string genre_id, string new_name)
        {
            Access();
            dao.EditGenre(genre_id, new_name);
            return RedirectToAction("Index", "ManagerGenre");
        }
    }
}