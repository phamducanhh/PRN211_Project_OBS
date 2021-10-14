using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRN211_Project_OBS.Models;

namespace PRN211_Project_OBS.Controllers
{
    
    public class LoginController : Controller
    {
        Model db = new Model();

        public User GetUserLogin(string email, string pass)
        {
            User x = null;           
            x = db.Users.SqlQuery($"select * from [User] where email = '{email}' and [password] = '{pass}'").First();
            return x;           
        }
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string pass)
        {

            User x = GetUserLogin(email, pass); 
            if (x == null)
            {
                ViewBag.Mess = "Invalid email or password";
                return View();
            }
            
            Session["user"] = x;
            return RedirectToAction("Index", "Home");
        }
    }
}