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

        // kayit olma sayfasini yukleyen metod
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // kayit olma islemini gerceklestiren metod
        [HttpPost]
        public ActionResult Index(MEMBER_TABLE userObj)
        {
            var userName = db.MEMBER_TABLE.FirstOrDefault(x => x.USERNAME == userObj.USERNAME);
            var email = db.MEMBER_TABLE.FirstOrDefault(x => x.EMAIL == userObj.EMAIL);
            ViewBag.Name = userObj.NAME;
            ViewBag.Surname = userObj.SURNAME;
            ViewBag.Email = userObj.EMAIL.Trim();
            ViewBag.Username = userObj.USERNAME;
            ViewBag.Password = userObj.PASSWORD;
            ViewBag.Phone = userObj.TELNUMBER;
            // eger kullanici adi veya email baskasi tarafindan kullaniliyor ise uyari gonderir
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