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
        DAO dao = new DAO();
        // GET: UserAdmin
        public ActionResult Index()
        {
            ViewBag.ListUser = dao.GetUsers();
            return View();
        }

        public ActionResult UserDetail()
        {
            string id = Request.Params["id"];
            ViewBag.User = dao.GetUserByID(id);
            ViewBag.Bills = dao.GetUserBillHistoryByUserID(id);
            return View();
        }
    }
}