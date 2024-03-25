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
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
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
                e.AccountNumber,
                e.Password,
                e.Role,
            }).ToList();

            return Json(employees, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employees employee)
        {
            if (ModelState.IsValid)
            {
                if (db.Employees.Any(f => f.AccountNumber == employee.AccountNumber))
                {
                    ModelState.AddModelError("AccountNumber", "員工帳號重複");
                    return View(employee);
                }

                db.Employees.Add(employee);
                db.SaveChanges();

                return RedirectToAction("Index", "Employee");
            }

            return View(employee);
        }
        public ActionResult Edit(int id)
        {
            var employee = db.Employees.Where(f => f.EmployeeID == id).FirstOrDefault();
            return View(employee);

        }
        [HttpPost]

        public ActionResult Edit(Employees employee)
        {
            var employees = db.Employees.Where(f => f.EmployeeID == employee.EmployeeID).FirstOrDefault();
            employees.FirstName = employee.FirstName;
            employees.LastName = employee.LastName;
            employees.AccountNumber = employee.AccountNumber;
            employees.Password = employee.Password;
            employees.Role = employee.Role;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int id)
        {
            var employee = db.Employees.Where(f => f.EmployeeID == id).FirstOrDefault();
            if (db.Orders.Any(o => o.EmployeeID == id))
            {
                TempData["ErrorMessage"] = "無法刪除此員工,因為有相關聯的訂單記錄。";
                return RedirectToAction("Index");
            }
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }

}