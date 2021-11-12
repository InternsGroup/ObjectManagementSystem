using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    public class ObjectController : Controller
    {
        DB_STOREEntities db = new DB_STOREEntities();

        public ActionResult Index(string searchInput)
        {
            var objects = from allItems in db.OBJECT_TABLE select allItems;
            if (!string.IsNullOrEmpty(searchInput))
            {
                objects = objects.Where(item => item.NAME.Contains(searchInput));
            }

            return View(objects.ToList());
        }

        [HttpGet]
        public ActionResult AddObject()
        {
            //get category from its table and send to page with viewbag
            var categoryList = db.CATEGORY_TABLE.ToList();
            List<SelectListItem> categoryItem = (List<SelectListItem>)(from category in categoryList select new SelectListItem { Text = category.NAME, Value = category.ID.ToString() }).ToList();
            ViewBag.categoryItem = categoryItem;

            return View();
        }

        [HttpPost]
        public ActionResult AddObject(OBJECT_TABLE item)
        {
            //before post ı need to send id's of category and author
            var category = db.CATEGORY_TABLE.Where(c => c.ID == item.CATEGORY_TABLE.ID).FirstOrDefault();
            item.CATEGORY_TABLE = (CATEGORY_TABLE)category;

            db.OBJECT_TABLE.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult DeleteObject(int id)
        {
            var item = db.OBJECT_TABLE.Find(id);
            db.OBJECT_TABLE.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetObject(int id)
        {
            var item = db.OBJECT_TABLE.Find(id);
            //get category from its table and send to page with viewbag
            var categoryList = db.CATEGORY_TABLE.ToList();
            List<SelectListItem> categoryItem = (List<SelectListItem>)(from category in categoryList select new SelectListItem { Text = category.NAME, Value = category.ID.ToString() }).ToList();
            ViewBag.categoryItem = categoryItem;

            return View("GetObject", item);
        }

        public ActionResult UpdateObject(OBJECT_TABLE item) //makes post action
        {
            var obj = db.OBJECT_TABLE.Find(item.ID);
            obj.NAME = item.NAME;
            var category = db.CATEGORY_TABLE.Where(c => c.ID == item.CATEGORY_TABLE.ID).FirstOrDefault();
            obj.CATEGORY = category.ID;
            obj.DETAIL = item.DETAIL;
            obj.OBJECTIMAGE = item.OBJECTIMAGE;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}