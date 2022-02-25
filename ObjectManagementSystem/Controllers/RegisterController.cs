using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ObjectManagementSystem.Models.Entity;

namespace ObjectManagementSystem.Controllers
{
    public class RegisterController : Controller
    {
        DB_STOREEntities db = new DB_STOREEntities();

        // kayit olma sayfasini yukleyen metod
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // kayit olma islemini gerceklestiren metod
        [HttpPost]
        public ActionResult Index(MEMBER_TABLE userObj)
        {
            var userName = db.MEMBER_TABLE.FirstOrDefault(x => x.USERNAME == userObj.USERNAME);
            var email = db.MEMBER_TABLE.FirstOrDefault(x => x.EMAIL == userObj.EMAIL);
            ViewBag.Name = userObj.NAME;
            ViewBag.Surname = userObj.SURNAME;
            ViewBag.Email = userObj.EMAIL.Trim();
            ViewBag.Username = userObj.USERNAME;
            ViewBag.Password = userObj.PASSWORD;
            ViewBag.Phone = userObj.TELNUMBER;
            // eger kullanici adi veya email baskasi tarafindan kullaniliyor ise uyari gonderir
            if (userName != null)
            {
                ViewBag.UsernameAlert = "This username is already in use. Please choose another username.";
                return View(userObj);
            }
            if(email != null)
            {
                ViewBag.EmailAlert = "This email is already in use. Please choose another email.";
                return View();
            }
            userObj.PASSWORD = EncodePasswordToBase64(userObj.PASSWORD);
            db.MEMBER_TABLE.Add(userObj);
            db.SaveChanges();
            return RedirectToAction("Index", "LogIn");
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decord your Password
        public static string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}