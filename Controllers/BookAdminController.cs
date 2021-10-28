using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRN211_Project_OBS.Controllers
{
    public class BookAdminController : Controller
    {
        // GET: BookAdmin
        public ActionResult BookAdmin()
        {
            ViewBag.Welcome = "welcome to BookAdmin";
            return View();
        }
    }
}