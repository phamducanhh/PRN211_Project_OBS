using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_PRN211_OBS_Project.Models;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class UserAdminController : Controller
    {
        public void Access()
        {
            User x = (User)Session["user"];
            if (!dao.isAble(x, "Admin"))
            {
                Response.Redirect("/Error");
            }
        }

        DAO dao = new DAO();
        // GET: UserAdmin
        public ActionResult Index()
        {
            Access();
            ViewBag.ListUser = dao.GetUsers();
            ViewBag.Url = "/UserAdmin/Index";
            return View();
        }

        public ActionResult UserDetail()
        {
            Access();
            string id = Request.Params["id"];
            ViewBag.User = dao.GetUserByID(id);
            ViewBag.Bills = dao.GetUserBillHistoryByUserID(id);
            return View();
        }

        [HttpPost]
        public RedirectResult ChangeStatus(string url)
        {
            Access();
            int id = Convert.ToInt32(Request.Params["id"]);
            int status = Convert.ToInt32(Request.Params["selectStatus"]);
            dao.ChangeStatusUser(id, status);
            return Redirect(url);
        }
    }
}