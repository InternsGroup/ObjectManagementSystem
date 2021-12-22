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
    public class MemberController : Controller
    {
        // GET: Member
        DB_STOREEntities db = new DB_STOREEntities();
        public ActionResult Index(int page = 1)
        {
            //var values = db.MEMBER_TABLE.ToList();
            var values = db.MEMBER_TABLE.ToList().ToPagedList(page, 10);
            return View(values);
        }

        [HttpGet]
        public ActionResult AddMember()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMember(MEMBER_TABLE memberObj)
        {
            db.MEMBER_TABLE.Add(memberObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteMember(int id)
        {
            var member = db.MEMBER_TABLE.Find(id);
            db.MEMBER_TABLE.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetMember(int id)
        {
            var member = db.MEMBER_TABLE.Find(id);
            return View("GetMember", member);
        }

        public ActionResult UpdateMember(MEMBER_TABLE memberObj)
        {
            var member = db.MEMBER_TABLE.Find(memberObj.ID);
            member.NAME = memberObj.NAME;
            member.SURNAME = memberObj.SURNAME;
            member.EMAIL = memberObj.EMAIL;
            member.USERNAME = memberObj.USERNAME;
            member.PASSWORD = memberObj.PASSWORD;
            member.TELNUMBER = memberObj.TELNUMBER;
            member.PHOTO = memberObj.PHOTO;
            member.SCHOOL = memberObj.SCHOOL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}