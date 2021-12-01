using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    public class LogInController : Controller
    {
        public static MEMBER_TABLE user = new MEMBER_TABLE();
        DB_STOREEntities db = new DB_STOREEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(MEMBER_TABLE memberObj)
        {
            var username = memberObj.USERNAME;
            var password = memberObj.PASSWORD;
            var userInfo = db.MEMBER_TABLE.FirstOrDefault(x => x.PASSWORD == password && x.USERNAME == username);
            if (userInfo != null)
            {
                FormsAuthentication.SetAuthCookie(userInfo.USERNAME, false);
                Session["Username"] = userInfo.USERNAME.ToString();
                user.ID = userInfo.ID;
                return RedirectToAction("Index", "Display");
            }
            else
            {
                //şifre yanlıs falan diyebilir
                return View();
            }
        }
    }
}