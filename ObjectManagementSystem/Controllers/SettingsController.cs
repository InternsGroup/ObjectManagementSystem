using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        DB_STOREEntities db = new DB_STOREEntities();

        // sistemde kayitli adminleri tablo halinde listeleyen metod
        public ActionResult Index()
        {
            var objects = from allItems in db.ADMIN_TABLE select allItems;

            return View(objects.ToList());
        }

        // tablodan secilen admini silme islemini gerceklestiren metod
        public ActionResult Delete(int id)
        {
            var person = db.ADMIN_TABLE.Find(id);
            db.ADMIN_TABLE.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // secilen adminin bilgisini guncelleme sayfasina yonlendiren metod
        public ActionResult GetAdmin(int id)
        {
            var person = db.ADMIN_TABLE.Find(id);
            return View("GetAdmin", person);
        }

        // admin bilgilerini guncelleme islemini gerceklestiren metod
        public ActionResult UpdateAdmin(ADMIN_TABLE admin)
        {
            int x = 0;
            var person = db.ADMIN_TABLE.Find(admin.ID);
            var userName = db.ADMIN_TABLE.FirstOrDefault(y => y.USERNAME == admin.USERNAME.Trim());
            if (userName != null && userName.ID != person.ID)
            {
                ViewBag.Message = "This username is already in use.";
                return View("GetAdmin", person);
            }
            var nr=db.ADMIN_TABLE.FirstOrDefault(defAdmin => defAdmin.NAME == "default");
            if (admin.NAME == "default" && nr.ID != person.ID)
            {
                ViewBag.Message = "You can not use 'default' as admin name. Choose another name. ";
                return View("GetAdmin",person);
            }
            person.NAME = admin.NAME;
            person.PASSWORD = admin.PASSWORD;

            if (admin.NAME == "default")
            {
                person.AUTHORITY = "Admin";
                admin.AUTHORITY = "Admin";
            }

            if (Convert.ToInt32(Session["ID"]) == admin.ID)
            {
                if(person.USERNAME != admin.USERNAME || person.AUTHORITY != admin.AUTHORITY)
                {
                    x = 1;
                }
            }

            if (admin.NAME == "default")
            {
                person.AUTHORITY = "Admin";
            }
            else
            {
                person.AUTHORITY = admin.AUTHORITY;
            }
            person.USERNAME = admin.USERNAME;
            db.SaveChanges();
            if (x == 1)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "AdminLogin");
            }
            return RedirectToAction("Index");
        }

        // admin ekleme sayfasini yukleyen metod
        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }

        // admin ekleme islemini gerceklestiren metod
        [HttpPost]
        public ActionResult AddAdmin(ADMIN_TABLE adminObj)
        {
            var userName = db.ADMIN_TABLE.FirstOrDefault(y => y.USERNAME == adminObj.USERNAME.Trim());
            if (userName != null)
            {
                ViewBag.Message = "This username is already in use.";
                return View("AddAdmin");
            }
            if(adminObj.NAME == "default")
            {
                ViewBag.Message = "You can not use 'default' as admin name. Choose another name. ";
                return View("AddAdmin");
            }
            db.ADMIN_TABLE.Add(adminObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}