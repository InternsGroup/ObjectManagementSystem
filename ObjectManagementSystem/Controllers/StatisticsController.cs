using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class StatisticsController : Controller
    {
        DB_STOREEntities db = new DB_STOREEntities();

        // sistem istatistiklerini statistics sayfasina gonderen metod
        public ActionResult Index()
        {
            var objects = db.OBJECT_TABLE.ToList();
            var loanedObjects = db.ACTION_TABLE.ToList();
            var users = db.MEMBER_TABLE.ToList();
            // bilgiler viewbag uzerinden sayfaya gonderiliyor
            ViewBag.objectNum = objects.Count();
            ViewBag.loanedObjectNum = loanedObjects.Count();
            ViewBag.userNum = users.Count();
            return View();
        }
    }
}