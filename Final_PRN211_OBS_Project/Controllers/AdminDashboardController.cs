using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Final_PRN211_OBS_Project.Models;

namespace Final_PRN211_OBS_Project.Controllers
{
    public class AdminDashboardController : Controller
    {
        DAO dao = new DAO();
        public void Access()
        {
            User x = (User)Session["user"];
            if (!dao.isAble(x, "Admin"))
            {
                Response.Redirect("/Error");
            }
        }
        // GET: AdminDashboard
        public ActionResult Index()
        {
            Access();
            int year = DateTime.Now.Year;
            double[] revenue = new double[12];
            for (int i = 1; i < 13; i++)
            {
                revenue[i - 1] = dao.GetRevenueInMonthYear(i, year);
            }           
            ViewBag.Array = revenue;
            return View();
        }
    }
}