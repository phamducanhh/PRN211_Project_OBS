using Final_PRN211_OBS_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class SignInController : Controller
    {
        DAO dao = new DAO();
        // GET: SignIn
        [HttpGet]
        public ActionResult Index()
        {
         
            HttpCookie reqCookies = Request.Cookies["userInfo"];
            if (reqCookies != null)
            {
                ViewBag.User_name = reqCookies["Username"].ToString();
                ViewBag.Pass = reqCookies["Password"].ToString();
            }
            else
            {
                ViewBag.User_name = "";
                ViewBag.Pass = "";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string pass)
        {
            User x = dao.GetUserLogin(email, pass);
            if (x == null)
            {
                ViewBag.Mess = "Invalid email or password";
                return View();
            }
            if (Request.Params["remember"] == "yes")
            {
                HttpCookie userInfo = new HttpCookie("userInfo");
                userInfo["Username"] = email;
                userInfo["Password"] = pass;
                userInfo.Expires.Add(new TimeSpan(24, 0, 0));
                Response.Cookies.Add(userInfo);
                // expanding here

            }
            Session["user"] = x;
            if (dao.GetRole(x.role_id).title.Equals("Customer"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (dao.GetRole(x.role_id).title.Equals("Admin"))
            {
                return RedirectToAction("Index", "AdminHome");
            }
            
            return RedirectToAction("Index", "Error");
        }

        [HttpPost]
        public ActionResult SignUp(string email, string name, string pass, string repass)
        {
            if (dao.GetUserByEmail(email) != null)
            {
                ViewBag.Mess = "This Email has already been Registed!";
                return View();
            }
            if (pass.Length < 8)
            {
                ViewBag.Mess = "Password length must be equal or greater than 8 character.";
                return View();
            }
            if (!pass.Equals(repass))
            {
                ViewBag.Mess = "Password and Re-password must be similar.";
                return View();
            }
            dao.RegistUser(email, pass, name);
            Session["user"] = dao.GetUserByEmail(email);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}