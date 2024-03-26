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
        public ActionResult Login(string AccountNumber, string Password)
        {
            NorthwindEntities db = new NorthwindEntities();
            var member = db.Employees.Where(f => f.AccountNumber == AccountNumber && f.Password == Password).FirstOrDefault();
            if (member != null)
            {
                Session["Role"] = member.Role;
                Session["UserName"] = member.FirstName;

                FormsAuthentication.RedirectFromLoginPage
                    (member.AccountNumber, true);
                return RedirectToAction("Index", "Orders");
            }
            ViewBag.IsLogin = true;

            return View();
        }

    }
}