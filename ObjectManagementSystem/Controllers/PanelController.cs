using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    public class PanelController : Controller
    {
        DB_STOREEntities db = new DB_STOREEntities();
        // GET: Panel
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var user = (string)Session["Username"];
            var values = db.MEMBER_TABLE.FirstOrDefault(x => x.USERNAME == user);
            ViewBag.Message = null;
            return View(values);
        }
        [HttpPost]
        public ActionResult Index(MEMBER_TABLE memberObj)
        {
            var user = (string)Session["Username"];
            var member = db.MEMBER_TABLE.FirstOrDefault(x => x.USERNAME == user);
            member.NAME = memberObj.NAME;
            member.SURNAME = memberObj.SURNAME;
            member.PASSWORD = memberObj.PASSWORD;
            member.PHOTO = memberObj.PHOTO;
            member.TELNUMBER = memberObj.TELNUMBER;
            member.SCHOOL = memberObj.SCHOOL;
            ViewBag.Message = "Info successfully updated.";
            db.SaveChanges();
            return View(member);
        }
    }
}