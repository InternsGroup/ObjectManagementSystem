using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    public class AdminLogInController : Controller
    {
        public static MEMBER_TABLE user = new MEMBER_TABLE();
        DB_STOREEntities db = new DB_STOREEntities();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ADMIN_TABLE adminObj)
        {
            var userInfo = db.ADMIN_TABLE.FirstOrDefault(x => x.PASSWORD == adminObj.PASSWORD && x.USERNAME == adminObj.USERNAME);
            if (userInfo != null)
            {
                FormsAuthentication.SetAuthCookie(userInfo.USERNAME, false);
                Session["Username"] = userInfo.USERNAME.ToString();
                Session["ID"] = userInfo.ID.ToString();
                user.ID = userInfo.ID;
                return RedirectToAction("Index", "Category");
            }
            else
            {
                ViewBag.Message = "Invalid username or password.";
                return View();
            }
        }
    }
}
