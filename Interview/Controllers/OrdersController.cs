using Interview.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Interview.Controllers
{
    public class OrdersController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        int pageSize = 20;

        // GET: Orders
        public ActionResult Index(int? OrderID, int? EmployeeID, int page = 0)
        {
            if (page == 0)
            {
                int currentPage = page < 1 ? 1 : page;

                var orders = db.Orders.ToList();

                var pagelist = orders.ToPagedList(currentPage, pageSize);

                var model = new ProductViewModel()
                {
                    Orders = pagelist
                };

                return View(model);
            }
            else
            {
                ProductViewModel model = new ProductViewModel();
                model.OrderID = OrderID;
                model.EmployeeID = EmployeeID;

                return Index(model, page);
            }
        }

        [HttpPost]
        public ActionResult Index(ProductViewModel model, int page = 0)
        {


            int currentPage = page < 1 ? 1 : page;

            string oid = "";
            string empid = "";


            if (string.IsNullOrWhiteSpace(model.OrderID.ToString()) == false)
            {
                string orderIdAsString = model.OrderID.ToString();
                string trimmedOrderId = orderIdAsString.Trim();
                oid = trimmedOrderId;
            }

            if (string.IsNullOrWhiteSpace(model.EmployeeID.ToString()) == false)
            {
                string EmployeeIDAsString = model.EmployeeID.ToString();
                string trimmedEmployeeID = EmployeeIDAsString.Trim();
                empid = trimmedEmployeeID;
            }


            var query = db.Orders.Where(f =>
                            (oid == "" || f.OrderID.ToString() == oid)
                            && (empid == "" || f.EmployeeID.ToString() == empid)
            ).OrderByDescending(f => f.OrderID).ThenBy(f => f.EmployeeID).ToList();


            var search = new ProductViewModel()
            {
                OrderID = model.OrderID,
                EmployeeID = model.EmployeeID,
                Orders = query.ToPagedList(currentPage, pageSize),


            };

            return View(search);
        }
    }
}