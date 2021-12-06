using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    public class LendController : Controller
    {
        public static RESERVED_OBJECT_TABLE myObj = new RESERVED_OBJECT_TABLE();
        // GET: Lend
        DB_STOREEntities db = new DB_STOREEntities();
        public ActionResult Index()
        {
            var loanedObjectsList = db.ACTION_TABLE.Where(action => action.ACTIONSTATUS == false).ToList();
            return View(loanedObjectsList);
        }

        [HttpGet]
        public ActionResult Lend()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Lend(ACTION_TABLE lendObj)
        {
            db.ACTION_TABLE.Add(lendObj);
            var item = db.OBJECT_TABLE.Find(lendObj.OBJECT);
            var memberObj = db.MEMBER_TABLE.Find(lendObj.MEMBER);
            var employeeObj = db.EMPLOYEE_TABLE.Find(lendObj.EMPLOYEE);

            if (item == null)
            {
                ViewBag.Message = "Enter valid object ID.";
                return View("Lend");
            }
            else if (item.STATUS == false)
            {
                ViewBag.Message = "Object is already loaned to another member.";
                return View("Lend");
            }
            else if (memberObj == null)
            {
                ViewBag.Message = "Enter valid member ID.";
                return View("Lend");
            }
            else if (employeeObj == null)
            {
                ViewBag.Message = "Enter valid employee ID.";
                return View("Lend");
            }else if (item.RESERVATIONSTATUS == false)
            {
                ViewBag.Message = "Object is reserved by another member. Please control the reserved objects.";
                return View("Lend");
            }
            else
            {
                item.STATUS = false;
                lendObj.ACTIONSTATUS = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult ReserveObject(RESERVED_OBJECT_TABLE reserveObj)
        {
            int controlToday = DateTime.Compare((DateTime)DateTime.Now.Date, (DateTime)reserveObj.BORROWDATE);
            if (controlToday > 0)
            {
                Session["Danger"] = "Start date of reservation can not be earlier than today.";
                return RedirectToAction("Index", "Display");
            }
            int controlStartEnd = DateTime.Compare((DateTime)reserveObj.BORROWDATE, (DateTime)reserveObj.RETURNDATE);
            if (controlStartEnd >= 0)
            {
                Session["Danger"] = "End date of reservation can not be earlier than the start date or same.";
                return RedirectToAction("Index","Display");
            }
            var item = db.OBJECT_TABLE.FirstOrDefault(x => x.ID == reserveObj.OBJECT);
            item.RESERVATIONSTATUS = false;
            db.RESERVED_OBJECT_TABLE.Add(reserveObj);
            db.SaveChanges();
            return RedirectToAction("Index","Display");
        }
        /*
        public ActionResult LoanObject(int id)
        {
            var item = db.OBJECT_TABLE.Find(id);
            item.STATUS = false;
            ACTION_TABLE lendObj = new ACTION_TABLE();
            lendObj.OBJECT = id;
            lendObj.MEMBER = LogInController.user.ID;
            lendObj.EMPLOYEE = 4;
            lendObj.BORROWDATE = DateTime.Now;
            lendObj.RETURNDATE = DateTime.Now.AddDays(7);
            lendObj.ACTIONSTATUS = false;
            lendObj.MEMBERRETURNDATE = null;
            db.ACTION_TABLE.Add(lendObj);
            db.SaveChanges();
            return RedirectToAction("Index", "Display");
        }
        */

        public ActionResult GetReservedObject(int id)
        {
            var reservedObj = db.RESERVED_OBJECT_TABLE.Find(id);
            myObj = reservedObj;
            return View("GetReservedObject", reservedObj);
        }

        public ActionResult LendReservedObject(RESERVED_OBJECT_TABLE reservedObj)
        {
            try
            {
                int controlToday = DateTime.Compare((DateTime)DateTime.Now.Date, (DateTime)reservedObj.BORROWDATE);
                if (controlToday > 0)
                {
                    ViewBag.Danger = "Start date of lend can not be earlier than today.";
                    return View("GetReservedObject", myObj);
                }
                int controlStartEnd = DateTime.Compare((DateTime)reservedObj.BORROWDATE, (DateTime)reservedObj.RETURNDATE);
                if (controlStartEnd >= 0)
                {
                    ViewBag.Danger = "End date of lend can not be earlier than the start date or same as of start date.";
                    return View("GetReservedObject", myObj);
                }
            }catch(Exception e)
            {
                ViewBag.Danger = "Invalid datetime!!! Control your datetime input format! (dd.mm.yyyy)";
                return View("GetReservedObject", myObj);
            }
            var resObj = db.RESERVED_OBJECT_TABLE.Find(myObj.ID);
            ACTION_TABLE actionObj = new ACTION_TABLE();
            actionObj.OBJECT = resObj.OBJECT;
            actionObj.MEMBER = resObj.MEMBER;
            actionObj.EMPLOYEE = 4;
            actionObj.BORROWDATE = myObj.BORROWDATE;
            actionObj.RETURNDATE = myObj.RETURNDATE;
            actionObj.ACTIONSTATUS = false;
            db.ACTION_TABLE.Add(actionObj);
            db.RESERVED_OBJECT_TABLE.Remove(resObj);
            var item = db.OBJECT_TABLE.FirstOrDefault(x => x.ID == myObj.OBJECT);
            item.STATUS = false;
            db.SaveChanges();
            return RedirectToAction("ReservedObjects");
        }

        public ActionResult ReturnObject(int id)
        {
            var lendObj = db.ACTION_TABLE.Find(id);
            lendObj.OBJECT_TABLE.STATUS = true;

            return View("ReturnObject", lendObj);
        }

        public ActionResult UpdateReturnObject(ACTION_TABLE actionTableObj)
        {
            var actionObj = db.ACTION_TABLE.Find(actionTableObj.ID);
            actionObj.MEMBERRETURNDATE = actionTableObj.MEMBERRETURNDATE;
            actionObj.ACTIONSTATUS = true;
            actionObj.OBJECT_TABLE.STATUS = true;
            actionObj.OBJECT_TABLE.RESERVATIONSTATUS = true;
            //geri verilecek objeler listesinden kaldır
            db.ACTION_TABLE.Remove(actionObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteReservation(int id)
        {
            var reservation = db.RESERVED_OBJECT_TABLE.Find(id);
            reservation.OBJECT_TABLE.RESERVATIONSTATUS = true;
            db.RESERVED_OBJECT_TABLE.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("ReservedObjects");
        }

        public ActionResult ExtendPeriod(int id)
        {
            var action = db.ACTION_TABLE.Find(id);
            var time = action.RETURNDATE;
            action.RETURNDATE = time.Value.AddDays(7);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ReservedObjects()
        {
            var reservedObjectsList = db.RESERVED_OBJECT_TABLE.ToList();
            return View(reservedObjectsList);
        }
    }
}