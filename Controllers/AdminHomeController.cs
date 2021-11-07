﻿using PRN211_Project_OBS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRN211_Project_OBS.Controllers
{
    public class AdminHomeController : Controller
    {
        DAO dao = new DAO();
        //List<Book> books = new List<Book>();
        // GET: AdminHome
        public ActionResult Index()
        {
            ViewBag.Books = dao.GetBooks();
            return View();
        }
    }
}