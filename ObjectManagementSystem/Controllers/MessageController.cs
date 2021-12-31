using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {

        DB_STOREEntities db = new DB_STOREEntities();

        // mesajlarim sayfasini yukleyen metod
        public ActionResult Index()
        {
            // session userName uzerinden yaptigimiz icin sender, receiver bilgisi icin userName kullaniliyor
            var userName = (string)Session["Username"].ToString();
            var messages = db.MESSAGE_TABLE.Where(x => x.RECEIVER == userName.ToString()).ToList();
            return View(messages);
        }

        // yeni mesaj gonderme sayfasini yukleyen metod
        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        // yeni mesaj gonderme islemini gerceklestiren metod
        [HttpPost]
        public ActionResult NewMessage(MESSAGE_TABLE messageObj)
        {
            var userName = (string)Session["Username"].ToString();
            messageObj.SENDER = userName.ToString();
            messageObj.DATE = DateTime.Parse(DateTime.Now.ToShortDateString());
            var receiver = db.MEMBER_TABLE.FirstOrDefault(x => x.USERNAME == messageObj.RECEIVER);
            // alici kullanici ismi mevcut degil ise veya gonderici ile alici ismi ayni ise hata gonderir
            if(receiver == null || receiver.USERNAME == userName.ToString())
            {
                ViewBag.Message = "Invalid receiver username";
            }
            else
            {
                db.MESSAGE_TABLE.Add(messageObj);
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult Detail(int id)
        {
            var message = db.MESSAGE_TABLE.Find(id);
            return View("Detail", message);
        }
    }
}