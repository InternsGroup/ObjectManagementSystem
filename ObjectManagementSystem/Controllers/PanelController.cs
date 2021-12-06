﻿using System;
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

        public ActionResult LoanedItems()
        {
            var values = db.ACTION_TABLE.ToList();
            ViewBag.ID = (string)Session["Username"];
            return View(values);
        }

        public ActionResult ReservedItems()
        {
            var values = db.RESERVED_OBJECT_TABLE.ToList();
            ViewBag.danger = TempData["message"];
            ViewBag.ID = (string)Session["Username"];
            return View(values);
        }

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