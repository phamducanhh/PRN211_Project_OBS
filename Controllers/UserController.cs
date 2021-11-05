using System;
using System.Web.Mvc;
using PRN211_Project_OBS.Models;
namespace PRN211_Project_OBS.Controllers
{
    public class UserController : Controller
    {
        DAO dao = new DAO();

        // GET: User
        public ActionResult Index()
        {
            var users = dao.AllUser();
            return View(users);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = dao.GetUserByIdWithRecentBillInOneMonth(id);
            if (user == null){
                return HttpNotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    User user = new User();
                    user.email = collection["email"];
                    user.password = collection["password"];
                    user.username = collection["username"];
                    user.avatar_url = collection["avatar_url"];
                    user.role_id = int.Parse(collection["role_id"]);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    User newuser = new User();
                    newuser.email = collection["email"];
                    newuser.password = collection["password"];
                    newuser.username = collection["username"];
                    newuser.avatar_url = collection["avatar_url"];
                    newuser.role_id = int.Parse(collection["role_id"]);
                    dao.EditUser(newuser, id); 
                    return RedirectToAction("Index");
                }
                
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            dao.DeleteUserById(id);
            return RedirectToAction("Index");
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                dao.DeleteUserById(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
