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
        public static int viewStatus = 1;
        public static int categoryId = 0;
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                ViewBag.myId = Session["ID"];
            }
            ViewBag.viewStatus = viewStatus;
            ViewBag.categories = db.CATEGORY_TABLE.ToList();
            ViewBag.reservedObjects = db.RESERVED_OBJECT_TABLE.ToList();
            ViewBag.loanedObjects = db.ACTION_TABLE.ToList();
            ViewBag.members = db.MEMBER_TABLE.ToList();
            if (TempData["objects"] == null)
            {
                categoryId = 0;
                var allObjects = db.OBJECT_TABLE.ToList();
                return View(allObjects);
            }
            else
            {
                var categorizedObjects = TempData["objects"];
                ViewBag.categoryName = TempData["category"];
                return View(categorizedObjects);
            }
        }

        public ActionResult ChangeView()
        {
            try
            {
                if (categoryId != 0)
                {
                    TempData["objects"] = db.OBJECT_TABLE.Where(myObj => myObj.CATEGORY == categoryId).ToList();
                    TempData["category"] = db.CATEGORY_TABLE.FirstOrDefault(myObj => myObj.ID == categoryId).NAME;
                }
            }
            catch (Exception e) { }
            viewStatus = viewStatus * -1;
            return RedirectToAction("Index");
        }

        public ActionResult GetCategory(int id)
        {
            categoryId = id;
            TempData["objects"] = db.OBJECT_TABLE.Where(myObj => myObj.CATEGORY == id).ToList();
            TempData["category"] = db.CATEGORY_TABLE.FirstOrDefault(myObj => myObj.ID == id).NAME;
            return RedirectToAction("Index");
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