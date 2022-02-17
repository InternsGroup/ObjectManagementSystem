using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;
using PagedList;

namespace ObjectManagementSystem.Controllers
{
    public class DisplayController : Controller
    {
        DB_STOREEntities db = new DB_STOREEntities();
        public static int viewStatus = 1; // 1 resimli gorunum, -1 tablo gorunumu
        public static int categoryId = 0; // 0 our objects bolumunu temsil eder

        public ActionResult Index(int page = 1, string search="")
        {
            // kullanicinin girisini kontrol ediyor
            if (Session["Username"] != null)
            {
                ViewBag.myId = Session["ID"];
            }
            // sayfaya veri tabanindan gerekli bilgileri cekiyoruz
            ViewBag.viewStatus = viewStatus;
            ViewBag.categories = db.CATEGORY_TABLE.ToList();
            ViewBag.reservedObjects = db.RESERVED_OBJECT_TABLE.ToList();
            ViewBag.loanedObjects = db.ACTION_TABLE.ToList();
            ViewBag.members = db.MEMBER_TABLE.ToList();
            // kategori secili degilse ve sayfa ilk yukleniyorsa if, kategori secilmis ise else calisir
            // sayfa ilk acilirken tempdata bos bu yuzden if calisir ve tum objeler sayfaya gonderilir
            // kategori secilince tempdata["objects"] icerisine kategorideki objeler kaydediliyor
            if (TempData["objects"] == null)
            {
                IQueryable<OBJECT_TABLE> objects;

                // kategori id 0 default yani tum objeleri ceker
                if (categoryId == 0)
                {
                    objects = from allItems in db.OBJECT_TABLE select allItems;
                }
                else
                {
                    objects = from allItems in db.OBJECT_TABLE where allItems.CATEGORY == categoryId select allItems;
                }
                // arama yapilirsa metodun icine search degiskeni gelir ve null degilse aratilan objeler sayfaya gonderilir
                if (!string.IsNullOrEmpty(search))
                {
                    objects = objects.Where(item => item.NAME.Contains(search));
                    // sonuc bulunamazsa uyari gonderir
                    if (objects.Count() == 0)
                    {
                        Session["message"] = "No items match your search...";
                    }
                }
                try
                {
                    // kategori ismini sayfaya gonderiyoruz cunku kategori secildikten sonra tempdata bir sonraki islemde
                    // kayboldugundan her anasayfada yapilan aratma veya goruntu degisikligi isleminde ismi tekrar gonderiyoruz
                    if (categoryId != 0)
                    {
                        ViewBag.categoryName = db.CATEGORY_TABLE.FirstOrDefault(myObj => myObj.ID == categoryId).NAME;
                    }
                }catch (NullReferenceException) { }
                return View(objects.ToList().Reverse<OBJECT_TABLE>().ToPagedList(page,15));
            }
            else
            {
                // kategoriyi menuden secerek acarsak burasi calisir ve tempdata uzerinden objeler alinir
                var categorizedObjects = TempData["objects"];
                ViewBag.categoryName = TempData["category"];
                return View(categorizedObjects);
            }
        }

        public ActionResult ChangeView(int page = 1)
        {
            // sayfada listelenen objeler viewStatus uzerinden tablo seklinde veya modal popup seklinde gosterir
            try
            {
                if (categoryId != 0)
                {
                    // sayfa kategori bolumunun birinde ise tempdata uzerinden hangi kategoride oldugunu indexe gondeririz
                    TempData["objects"] = db.OBJECT_TABLE.Where(myObj => myObj.CATEGORY == categoryId).ToList().ToPagedList(page,15);
                    TempData["category"] = db.CATEGORY_TABLE.FirstOrDefault(myObj => myObj.ID == categoryId).NAME;
                }
            }
            catch (Exception) { }
            viewStatus = viewStatus * -1;
            return RedirectToAction("Index");
        }

        public ActionResult GoToMain()
        {
            // ana sayfa ikonuna tiklaninca kategori sayfasinin birinde ise default ana sayfaya doner
            // ve tum objeler karisik sekilde listelenir
            categoryId = 0;
            return RedirectToAction("Index");
        }

        public ActionResult GetCategory(int id, int page = 1)
        {
            // secilen kategorinin id si class duzeyinde degiskene kayit edilir ve index ona gore hareket eder
            categoryId = id;
            TempData["objects"] = db.OBJECT_TABLE.Where(myObj => myObj.CATEGORY == id).ToList().ToPagedList(page,15);
            TempData["category"] = db.CATEGORY_TABLE.FirstOrDefault(myObj => myObj.ID == id).NAME;
            return RedirectToAction("Index");
        }

        // iletisim formunun veri tabanina kaydedilmesini saglayan metod 
        [HttpPost]
        public ActionResult Index(CONTACT_TABLE contactTableObj)
        {
            db.CONTACT_TABLE.Add(contactTableObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}