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

        // uyeler tablosunu sayfaya yukleyen metod
        public ActionResult Index(int page = 1,string search="")
        {
            var objects = from allItems in db.MEMBER_TABLE select allItems;
            if (!string.IsNullOrEmpty(search))
            {
                objects = objects.Where(item => item.NAME.Contains(search) || item.ID.ToString() == search);
                var myList=objects.ToList().ToPagedList(page,9);
                return View(myList);
            }
            var values = db.MEMBER_TABLE.ToList().ToPagedList(page, 9);
            return View(values);
        }

        // uye ekleme sayfasina yonlendiren metod
        [HttpGet]
        public ActionResult AddMember()
        {
            return View();
        }

        // uye ekleme islemini gerceklestiren metod
        [HttpPost]
        public ActionResult AddMember(MEMBER_TABLE memberObj)
        {
            ViewBag.Name = memberObj.NAME;
            ViewBag.Surname = memberObj.SURNAME;
            ViewBag.Email = memberObj.EMAIL.Trim();
            ViewBag.Username = memberObj.USERNAME;
            ViewBag.Password = memberObj.PASSWORD;
            ViewBag.Phone = memberObj.TELNUMBER;
            ViewBag.School = memberObj.SCHOOL;
            var userName = db.MEMBER_TABLE.FirstOrDefault(x => x.USERNAME == memberObj.USERNAME);
            var email = db.MEMBER_TABLE.FirstOrDefault(x => x.EMAIL == memberObj.EMAIL.Trim());
            if (userName != null)
            {
                ViewBag.Message = "This username is already in use.";
                return View("AddMember");
            }
            if (email != null)
            {
                ViewBag.Message = "This email is already in use.";
                return View("AddMember");
            }
            db.MEMBER_TABLE.Add(memberObj);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // tablodan uye silme islemini gerceklestiren metod
        public ActionResult DeleteMember(int id)
        {
            var member = db.MEMBER_TABLE.Find(id);
            db.MEMBER_TABLE.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // uye guncelleme sayfasini yukleyen metod
        public ActionResult GetMember(int id)
        {
            var member = db.MEMBER_TABLE.Find(id);
            return View("GetMember", member);
        }

        // uye guncelleme islemini gerceklestiren metod
        public ActionResult UpdateMember(MEMBER_TABLE memberObj)
        {
            var member = db.MEMBER_TABLE.Find(memberObj.ID);
            var userName = db.MEMBER_TABLE.FirstOrDefault(x => x.USERNAME == memberObj.USERNAME.Trim());
            var email = db.MEMBER_TABLE.FirstOrDefault(x => x.EMAIL == memberObj.EMAIL.Trim());
            if (userName != null && userName.ID != member.ID)
            {
                ViewBag.Message = "This username is already in use.";
                return View("GetMember",member);
            }
            if (email != null && email.ID != member.ID)
            {
                ViewBag.Message = "This email is already in use.";
                return View("GetMember", member);
            }
            member.NAME = memberObj.NAME;
            member.SURNAME = memberObj.SURNAME;
            member.EMAIL = memberObj.EMAIL.Trim();
            member.USERNAME = memberObj.USERNAME;
            member.PASSWORD = memberObj.PASSWORD;
            member.TELNUMBER = memberObj.TELNUMBER;
            member.PHOTO = memberObj.PHOTO;
            member.SCHOOL = memberObj.SCHOOL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // uyeler tablosunu excel tablosune ceviren metod
        public ActionResult GetExcelFile()
        {
            var values = db.MEMBER_TABLE.ToList();
            return View(values);
        }
    }
}