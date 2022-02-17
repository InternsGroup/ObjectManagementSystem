using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;
using PagedList;

namespace ObjectManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ObjectController : Controller
    {
        DB_STOREEntities db = new DB_STOREEntities();

        // objeler tablosunu yukleyen metod
        public ActionResult Index(int page = 1, string search = "")
        {
            var objects = from allItems in db.OBJECT_TABLE select allItems;
            // arama cubugu inputu bos degil ise if calisir
            if (!string.IsNullOrEmpty(search))
            {
                objects = objects.Where(item => item.NAME.Contains(search) || item.ID.ToString() == search);
                var myList = objects.ToList().ToPagedList(page, 9);
                return View(myList);
            }
            var values = db.OBJECT_TABLE.ToList().Reverse<OBJECT_TABLE>().ToPagedList(page, 9);
            return View(values);

        }

        // obje ekleme sayfasini yukleyen metod
        [HttpGet]
        public ActionResult AddObject()
        {
            //kategorileri secilebilen liste halinde alip viewbag uzerinden sayfaya gonderiyoruz
            var categoryList = db.CATEGORY_TABLE.ToList();
            List<SelectListItem> categoryItem = (List<SelectListItem>)(from category in categoryList select new SelectListItem { Text = category.NAME, Value = category.ID.ToString() }).ToList();
            ViewBag.categoryItem = categoryItem;
            return View();
        }

        // obje ekleme islemini gerceklestiren metod
        [HttpPost]
        public ActionResult AddObject(OBJECT_TABLE item)
        {
            // kategori id sini aliyoruz ve obje kategorisine esitliyoruz
            var category = db.CATEGORY_TABLE.Where(c => c.ID == item.CATEGORY_TABLE.ID).FirstOrDefault();
            item.CATEGORY_TABLE = (CATEGORY_TABLE)category;
            // eger objenin resmi icin link bos birakilirsa default resmi gostermesi icin "Empty" ibaresi gonderilir
            // baska bir string olabilir sadece null olmamasi lazim
            if (item.OBJECTIMAGE == null)
            {
                item.OBJECTIMAGE = "Empty";
            }
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
            if (item.OBJECTIMAGE == null)
            {
                obj.OBJECTIMAGE = "Empty";
            }
            else
            {
                obj.OBJECTIMAGE = item.OBJECTIMAGE;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetExcelFile()
        {
            var values = db.OBJECT_TABLE.ToList();
            return View(values);
        }
    }
}