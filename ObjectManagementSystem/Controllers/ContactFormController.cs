using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ContactFormController : Controller
    {
        
        DB_STOREEntities db = new DB_STOREEntities();
        public ActionResult Index()
        {
            var forms = db.CONTACT_TABLE.ToList();
            return View(forms);
        }

        public ActionResult Delete(int id)
        {
            var form = db.CONTACT_TABLE.Find(id);
            db.CONTACT_TABLE.Remove(form);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Detail(int id)
        {
            var form = db.CONTACT_TABLE.Find(id);
            return View("Detail",form);
        }
    }
}