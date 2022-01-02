using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    // yetkilendirme yapiliyor giris yapan rol Admin veya Employee degil ise bu controller altindaki metodlara yani sayfalara erisemez
    [Authorize(Roles = "Admin,Employee")]
    public class CategoryController : Controller
    {
        // db nesnesi ile veri tabanındaki tablolara ve property'lere ulasabiliriz
        DB_STOREEntities db = new DB_STOREEntities();

        // kategori tablosunu yukleyen metod
        public ActionResult Index()
        {
            // veri tabanindan kategori objelerini alip listeye koyar ve sayfaya gonderir
            var values = db.CATEGORY_TABLE.ToList();
            var allObjects = db.OBJECT_TABLE.ToList();
            ViewBag.allobjects = allObjects;
            return View(values);

        }

        // kategori ekleme sayfasina yonlendirme yapiliyor
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }

        // kategori ekleme butonuna basilinca calisan post islemi
        [HttpPost]
        public ActionResult AddCategory(CATEGORY_TABLE categoryObj)
        {
            // post isleminden gelen categoryObj objesi veri tabaninda CATEGORY_TABLE'a ekleniyor
            var categoryName = db.CATEGORY_TABLE.FirstOrDefault(x => x.NAME == categoryObj.NAME.Trim());
            if (categoryName != null)
            {
                ViewBag.Message = "This category is already existing.";
                return View("AddCategory");
            }
            db.CATEGORY_TABLE.Add(categoryObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Index.cshtml sayfasinda delete butonuna basilinca Category/DeleteCategory/category.ID calistirilarak secilen alanin id si gonderiliyor
        // veri tabaninda bulunarak siliniyor redirect() ile sayfa guncelleniyor
        public ActionResult DeleteCategory(int id)
        {
            var category = db.CATEGORY_TABLE.Find(id);
            db.CATEGORY_TABLE.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        // Index.cshtml sayfasinda kategori guncelle butonun basilinca cagirilan ActionResult
        public ActionResult GetCategory(int id)
        {
            var category = db.CATEGORY_TABLE.Find(id);
            return View("GetCategory", category);
        }

        // kategori guncellemesinde kullanilan metod
        public ActionResult UpdateCategory(CATEGORY_TABLE categoryObj)
        {
            var category = db.CATEGORY_TABLE.Find(categoryObj.ID);
            var categoryName = db.CATEGORY_TABLE.FirstOrDefault(x => x.NAME == categoryObj.NAME.Trim());
            if (categoryName != null && categoryName.ID != category.ID)
            {
                ViewBag.Message = "This category is already existing.";
                return View("GetCategory",categoryObj);
            }
            category.NAME = categoryObj.NAME;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}