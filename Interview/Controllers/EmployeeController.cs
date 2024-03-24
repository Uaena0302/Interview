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

        //public JsonResult GetEmployees()
        //{
        //    var employees = db.Employees.ToList();
        //    return Json(employees, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetEmployees()
        {
            var employees = db.Employees.Select(e => new
            {
                e.EmployeeID,
                e.LastName,
                e.FirstName,
                e.Title,
                e.TitleOfCourtesy,
                e.Address,
                e.PostalCode,
                e.Country,
                e.HomePhone,
                e.Extension
            }).ToList();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }
    }

}