using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    public class PanelController : Controller
    {
        DB_STOREEntities db = new DB_STOREEntities();
        int id = 5;

        // GET: Panel
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var user = (string)Session["Username"];
            var values = db.MEMBER_TABLE.FirstOrDefault(x => x.USERNAME == user);
            id = values.ID;
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

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Display");
        }

        
        public ActionResult MyItems()
        {
            var values = db.ACTION_TABLE.ToList();
            // olanları sergile = view'a gönder
            /*
            List<ACTION_TABLE> objList = new List<ACTION_TABLE>();
            foreach (var actionObj in db.ACTION_TABLE.ToList())
            {
                if (actionObj.MEMBER == id)
                {
                    objList.Add(actionObj);
                }
            }
            ViewBag.objList = objList.ToArray();
            */
            ViewBag.ID = (string)Session["Username"];
            return View(values);
        }

    }
}