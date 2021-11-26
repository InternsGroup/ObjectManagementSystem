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
            }
            else
            {
                item.STATUS = false;
                lendObj.ACTIONSTATUS = false;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
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
            //geri verilecek objeler listesinden kaldır
            db.ACTION_TABLE.Remove(actionObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}