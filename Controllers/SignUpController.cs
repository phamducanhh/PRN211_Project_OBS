using PRN211_Project_OBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRN211_Project_OBS.Controllers
{
    public class SignUpController : Controller
    {
        Model db = new Model();
        // GET: SignUp
        [HttpPost]
        public ActionResult Index(string email, string name, string pass, string repass)
        {
            string mess="";
            if (CheckLoginDuplicate(email)==true)
            {
                mess = "Email is existed, please enter again";
                TempData["mess"] = mess;
                return RedirectToAction("Index", "Login");

            }
            if (!CheckRepass(pass, repass))
            {
                mess = "Repass and pass must be similar!";
                TempData["mess"] = mess;
                return RedirectToAction("Index", "Login");
            }
            
            RegistUser(email, pass, name);
            Session["user"] = db.Users.SqlQuery($"select * from [User] where email = '{email}'").First();

            return RedirectToAction("Index","Home");
        }

        public bool CheckRepass(string p, string rp)
        {
            return p == rp;
        }
        public bool CheckLoginDuplicate(string email)//email ton tai => true, email k ton tai => sai
        {
            try {
                User x = db.Users.SqlQuery($"SELECT * FROM [User] WHERE BINARY_CHECKSUM(email) = BINARY_CHECKSUM('{email}')").First() ;
                return true;
            }
            catch(Exception e)
            {
                return false;
            }


        }
        public void RegistUser(string emaill, string passwordd, string userr)
        {
            db.Users.Add(new User() { email = emaill, password = passwordd, username = userr, avatar_url = "https://avatars.githubusercontent.com/u/17879520?v=4", role_id = 2 });
            db.SaveChanges();
            
        }
    }
}