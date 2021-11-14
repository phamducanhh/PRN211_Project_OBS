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
            List<User> list = dao.GetUsers();
            int pageSize = list.Count % 10 == 0 ? list.Count / 10 : list.Count / 10 + 1;
            int currentPage;
            try
            {
                currentPage = Int32.Parse(Request.Params["page"]);
            }
            catch (Exception e)
            {
                currentPage = 1;
            }
            ViewBag.ListUser = list.GetRange(10 * (currentPage - 1), 10 * currentPage > list.Count ? list.Count % 10 : 10);
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentPage = currentPage;
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