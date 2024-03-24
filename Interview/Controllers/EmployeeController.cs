using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interview.Models;

namespace Interview.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        NorthwindEntities db = new NorthwindEntities();
        public ActionResult Index()
        {
            var employees = db.Employees.ToList();
            return View(employees);
        }
    }
}