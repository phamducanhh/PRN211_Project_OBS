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
    }
}