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
    public class ContactFormController : Controller
    {
        
        DB_STOREEntities db = new DB_STOREEntities();

        // pagedlist seklinde tablo donen iletisim formu mesajlarinin yuklendigi metod
        public ActionResult Index(int page = 1)
        {
            var forms = db.CONTACT_TABLE.ToList().ToPagedList(page,9);
            return View(forms);
        }

        // tablodan secilen nesnenin silinmesini saglayan metod
        public ActionResult Delete(int id)
        {
            var form = db.CONTACT_TABLE.Find(id);
            db.CONTACT_TABLE.Remove(form);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // iletisim formunun mesajini detayli gorebilmek icin detail butonuna ait metod
        public ActionResult Detail(int id)
        {
            var form = db.CONTACT_TABLE.Find(id);
            return View("Detail",form);
        }
    }
}