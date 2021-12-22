using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        DB_STOREEntities db = new DB_STOREEntities();
        public ActionResult Index()
        {
            var objects = from allItems in db.ADMIN_TABLE select allItems;

            return View(objects.ToList());
        }

        public ActionResult Delete(int id)
        {
            var person = db.ADMIN_TABLE.Find(id);
            db.ADMIN_TABLE.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetAdmin(int id)
        {
            var person = db.ADMIN_TABLE.Find(id);
            return View("GetAdmin", person);
        }

        public ActionResult UpdateAdmin(ADMIN_TABLE admin)
        {
            var person = db.ADMIN_TABLE.Find(admin.ID);
            person.USERNAME = admin.USERNAME;
            person.PASSWORD = admin.PASSWORD;
            person.AUTHORITY = admin.AUTHORITY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(ADMIN_TABLE adminObj)
        {
            db.ADMIN_TABLE.Add(adminObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}