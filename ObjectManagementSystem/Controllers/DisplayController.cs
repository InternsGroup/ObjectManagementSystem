using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    public class DisplayController : Controller
    {
        DB_STOREEntities db = new DB_STOREEntities();

        [HttpGet]
        public ActionResult Index()
        {
            var objects = db.OBJECT_TABLE.ToList();
            return View(objects);
        }


        [HttpPost]
        public ActionResult Index(CONTACT_TABLE contactTableObj)
        {
            db.CONTACT_TABLE.Add(contactTableObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}