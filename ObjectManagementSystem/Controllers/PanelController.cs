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
    [Authorize]
    public class PanelController : Controller
    {
        DB_STOREEntities db = new DB_STOREEntities();
        int id = 0;

        // kullanici panelini yukleyen metod
        [HttpGet]
        public ActionResult Index()
        {
            var user = (string)Session["Username"];
            var values = db.MEMBER_TABLE.FirstOrDefault(x => x.USERNAME == user);
            id = values.ID;
            ViewBag.Message = null;
            return View(values);
        }

        // kullanici panelinde bilgi guncelleme islemini gerceklestiren metod
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

        // oturum sonlandirma islemini gerceklestiren metod
        public ActionResult LogOut()
        {
            int x = 0;
            if (User.IsInRole("Admin") || User.IsInRole("Employee")){ x = 1; }
            FormsAuthentication.SignOut();
            if(x==1)
            {
                return RedirectToAction("Index", "AdminLogin");
            }
            return RedirectToAction("Index", "Display");
        }

        // kullanicinin odunc aldigi objeleri gosteren metod
        public ActionResult LoanedItems()
        {
            var values = db.ACTION_TABLE.ToList();
            ViewBag.ID = (string)Session["Username"];
            return View(values);
        }

        // kullanicinin rezerve ettigi objeleri gosteren metod
        public ActionResult ReservedItems()
        {
            var values = db.RESERVED_OBJECT_TABLE.ToList();
            ViewBag.danger = TempData["message"];
            ViewBag.ID = (string)Session["Username"];
            return View(values);
        }

        // rezerve edilen objenin rezerve islemini iptal ederek tekrar erisilebilir olmasini saglayan metod
        public ActionResult CancelReservation(int id)
        {
            var reservation = db.RESERVED_OBJECT_TABLE.Find(id);
            if (reservation != null)
            {
                reservation.OBJECT_TABLE.RESERVATIONSTATUS = true;
                db.RESERVED_OBJECT_TABLE.Remove(reservation);
                db.SaveChanges();
            }
            else
            {
                TempData["message"] = "This object is loaned to you. Please contact your admin to cancel borrow transaction.";
            }
            return RedirectToAction("ReservedItems");
        }

    }
}