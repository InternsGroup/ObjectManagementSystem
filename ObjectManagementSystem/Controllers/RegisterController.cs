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
            var userName = db.MEMBER_TABLE.FirstOrDefault(x => x.USERNAME == userObj.USERNAME);
            var email = db.MEMBER_TABLE.FirstOrDefault(x => x.EMAIL == userObj.EMAIL);
            ViewBag.Name = userObj.NAME;
            ViewBag.Surname = userObj.SURNAME;
            ViewBag.Email = userObj.EMAIL;
            ViewBag.Username = userObj.USERNAME;
            ViewBag.Password = userObj.PASSWORD;
            ViewBag.Phone = userObj.TELNUMBER;
            if (userName != null)
            {
                ViewBag.UsernameAlert = "This username is already in use. Please choose another username.";
                return View(userObj);
            }
            if(email != null)
            {
                ViewBag.EmailAlert = "This email is already in use. Please choose another email.";
                return View();
            }
            db.MEMBER_TABLE.Add(userObj);
            db.SaveChanges();
            return RedirectToAction("Index", "LogIn");
        }
    }
}