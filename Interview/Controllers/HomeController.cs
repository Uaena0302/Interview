using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Interview.Models;
using Microsoft.Ajax.Utilities;

namespace Interview.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(string Account_Number, string Password)
        {
            NorthwindEntities db = new NorthwindEntities();
            var member = db.Employees.Where(f => f.Account_Number == Account_Number && f.Password == Password).FirstOrDefault();
            if (member != null)
            {
                FormsAuthentication.RedirectFromLoginPage
                    (member.Account_Number, true);
                return RedirectToAction("Index", "Category");
            }
            ViewBag.IsLogin = true;

            return View();
        }

    }
}