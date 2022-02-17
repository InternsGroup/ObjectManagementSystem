using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;
using PagedList;

namespace ObjectManagementSystem.Controllers
{

    public class LendController : Controller
    {
        public static RESERVED_OBJECT_TABLE myObj = new RESERVED_OBJECT_TABLE();
        DB_STOREEntities db = new DB_STOREEntities();

        // odunc alinmis objeleri sayfaya yukleyen metod
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult Index(int page = 1, string search = "")
        {
            var objects = from allItems in db.ACTION_TABLE where allItems.ACTIONSTATUS == false select allItems;
            // eger arama cubugunda birsey aratilirsa if calisir ve aratilan inputu iceren objeler sayfaya gonderilir
            if (!string.IsNullOrEmpty(search))
            {
                objects = objects.Where(item => (item.MEMBER_TABLE.NAME+" "+item.MEMBER_TABLE.SURNAME).Contains(search) || item.OBJECT_TABLE.NAME.Contains(search));
                var myList = objects.ToList().ToPagedList(page, 9);
                return View(myList);
            }
            // normal sayfa yukleniyorsa direkt odunc alinmis objeler listelenir
            var values = objects.ToList().ToPagedList(page, 9);
            return View(values);
        }

        // elden obje odunc verme sayfasini yukler
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public ActionResult Lend()
        {
            return View();
        }

        //elden obje odunc verme islemini gerceklestiren metod
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        public ActionResult Lend(ACTION_TABLE lendObj)
        {
            int controlToday = 0;
            int controlStartEnd = 0;
            db.ACTION_TABLE.Add(lendObj);
            var item = db.OBJECT_TABLE.Find(lendObj.OBJECT);
            var memberObj = db.MEMBER_TABLE.Find(lendObj.MEMBER);
            lendObj.EMPLOYEE = db.ADMIN_TABLE.Find(AdminLogInController.user.ID).ID;
            // tarih formatlari yanlis ise catch calisir hata gonderir
            try
            {
                // eger odunc alma baslangic tarihi bugunden onceyse hata gonderir
                controlToday = DateTime.Compare((DateTime)DateTime.Now.Date, (DateTime)lendObj.BORROWDATE);
                // eger odunc teslim tarihi baslangic tarihinden onceyse veya aynı gun ise hata gonderir
                controlStartEnd = DateTime.Compare((DateTime)lendObj.BORROWDATE, (DateTime)lendObj.RETURNDATE);
            }
            catch (Exception)
            {
                ViewBag.Message = "Invalid datetime!!! Control your datetime input format! (dd.mm.yyyy)";
                return View("Lend");
            }
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
            else if (item.RESERVATIONSTATUS == false)
            {
                ViewBag.Message = "Object is reserved by another member. Please control the reserved objects.";
                return View("Lend");
            }
            else if (controlToday > 0)
            {
                ViewBag.Message = "Start date of borrow can not be earlier than today.";
                return View("Lend");
            }
            else if (controlStartEnd >= 0)
            {
                ViewBag.Message = "End date of borrow can not be earlier than the start date or the same date.";
                return View("Lend");
            }
            else
            {
                item.STATUS = false;
                item.RESERVATIONSTATUS = false;
                lendObj.ACTIONSTATUS = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // kullanicinin rezerve islemini calistiran metod
        [Authorize]
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
                return RedirectToAction("Index", "Display");
            }
            if (db.OBJECT_TABLE.Find(reserveObj.OBJECT).RESERVATIONSTATUS == false)
            {
                Session["Danger"] = "Sorry but this object is already reserved by another member :(";
                return RedirectToAction("Index", "Display");
            }
            var item = db.OBJECT_TABLE.FirstOrDefault(x => x.ID == reserveObj.OBJECT);
            item.RESERVATIONSTATUS = false;
            db.RESERVED_OBJECT_TABLE.Add(reserveObj);
            db.SaveChanges();
            return RedirectToAction("Index", "Display");
        }

        // rezerve edilmis objeyi odunc verme sayfasina yonlendiren metod
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult GetReservedObject(int id)
        {
            var reservedObj = db.RESERVED_OBJECT_TABLE.Find(id);
            int controlToday = DateTime.Compare((DateTime)DateTime.Now.Date, (DateTime)reservedObj.BORROWDATE);
            if (controlToday > 0)
            {
                reservedObj.BORROWDATE = DateTime.Now.Date;
            }
            myObj = reservedObj;
            return View("GetReservedObject", reservedObj);
        }

        // rezerve edilmis obje icin odunc verme islemini gerceklestiren metod
        [Authorize(Roles = "Admin,Employee")]
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
            }
            catch (Exception)
            {
                ViewBag.Danger = "Invalid datetime!!! Control your datetime input format! (dd.mm.yyyy)";
                return View("GetReservedObject", myObj);
            }
            var resObj = db.RESERVED_OBJECT_TABLE.Find(myObj.ID);
            ACTION_TABLE actionObj = new ACTION_TABLE();
            actionObj.OBJECT = resObj.OBJECT;
            actionObj.MEMBER = resObj.MEMBER;
            actionObj.EMPLOYEE = db.ADMIN_TABLE.Find(AdminLogInController.user.ID).ID;
            actionObj.BORROWDATE = reservedObj.BORROWDATE;
            actionObj.RETURNDATE = reservedObj.RETURNDATE;
            actionObj.ACTIONSTATUS = false;
            db.ACTION_TABLE.Add(actionObj);
            db.RESERVED_OBJECT_TABLE.Remove(resObj);
            var item = db.OBJECT_TABLE.FirstOrDefault(x => x.ID == myObj.OBJECT);
            item.STATUS = false;
            db.SaveChanges();
            return RedirectToAction("ReservedObjects");
        }

        // odunc alinmis objeler icin geri teslim etme sayfasine yonlendiren metod
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult ReturnObject(int id)
        {
            var lendObj = db.ACTION_TABLE.Find(id);
            lendObj.OBJECT_TABLE.STATUS = true;

            return View("ReturnObject", lendObj);
        }

        // odunc alinmis objeler icin geri teslim etme islemini gerceklestiren metod
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult UpdateReturnObject(ACTION_TABLE actionTableObj)
        {
            var actionObj = db.ACTION_TABLE.Find(actionTableObj.ID);
            actionObj.MEMBERRETURNDATE = actionTableObj.MEMBERRETURNDATE;
            actionObj.ACTIONSTATUS = true;
            actionObj.OBJECT_TABLE.STATUS = true;
            actionObj.OBJECT_TABLE.RESERVATIONSTATUS = true;
            // geri verilecek objeler listesinden kaldır
            db.ACTION_TABLE.Remove(actionObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // rezerve edilen objenin odunc verilmesini reddetme islemini gerceklestiren metod
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult DeleteReservation(int id)
        {
            var reservation = db.RESERVED_OBJECT_TABLE.Find(id);
            reservation.OBJECT_TABLE.RESERVATIONSTATUS = true;
            db.RESERVED_OBJECT_TABLE.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("ReservedObjects");
        }

        // odunc verilmis objenin odunc tarihini 7 gunluk uzatan metod
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult ExtendPeriod(int id)
        {
            var action = db.ACTION_TABLE.Find(id);
            var time = action.RETURNDATE;
            action.RETURNDATE = time.Value.AddDays(7);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // rezerve edilmis objeleri tablo halinde listeleyen metod
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult ReservedObjects(int page = 1, string search = "")
        {
            var objects = from allItems in db.RESERVED_OBJECT_TABLE select allItems;
            // arama cubugunda aratilan bir input var ise if calisir
            if (!string.IsNullOrEmpty(search))
            {
                objects = objects.Where(item => (item.MEMBER_TABLE.NAME + " " + item.MEMBER_TABLE.SURNAME).Contains(search) || item.OBJECT_TABLE.NAME.Contains(search));
                var myList = objects.ToList().ToPagedList(page, 9);
                return View(myList);
            }
            var values = objects.ToList().ToPagedList(page, 9);
            return View(values);
        }

        // odunc alinmis objelerin tablosunu excel tablosuna ceviren metod
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult GetExcelFile()
        {
            var values = db.ACTION_TABLE.ToList();
            return View(values);
        }

        // rezerve edilmis objelerin tablosunu excel tablosuna ceviren metod
        [Authorize(Roles = "Admin,Employee")]
        public ActionResult GetExcelFile2()
        {
            var values = db.RESERVED_OBJECT_TABLE.ToList();

            return View(values);
        }
    }
}