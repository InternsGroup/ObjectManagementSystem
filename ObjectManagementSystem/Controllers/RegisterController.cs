using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    public class RegisterController : Controller
    {
        DB_STOREEntities db = new DB_STOREEntities();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(MEMBER_TABLE userObj)
        {
            db.MEMBER_TABLE.Add(userObj);
            db.SaveChanges();
            return RedirectToAction("Index", "LogIn");
        }
    }
}