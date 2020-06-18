using LiteCommerce.Admin.Models;
using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize(Roles =WebUserRoles.STAFF)]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index(int page = 1, string searchValue = "",string ShipperCountry = "")
        {
            int pageSize = 5;
            int rowCount = 0;
            List<EntityOrder> ListOfOrder = OrderBLL.ListOfOrders(page, pageSize, searchValue, ShipperCountry, out rowCount);
            var model = new OrderPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                ShipperCountry = ShipperCountry,
                Data = ListOfOrder
                
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Create(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create a Order";
                EntityOrder newOrder = new EntityOrder()
                {
                    OrderID = 0
                };
                return View(newOrder);
            }
            else
            {
                ViewBag.Title = "View Orders And OrderDetails";      
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(EntityOrder model)
        {
            try
            {

            if (string.IsNullOrEmpty(model.CustomerCompanyName))
                ModelState.AddModelError("CustomerCompanyName", "CustomerCompanyName expected");
            if (string.IsNullOrEmpty(model.ShipperCompanyName))
                ModelState.AddModelError("ShipperCompanyName", "ShipperCompanyName expected");
            if (string.IsNullOrEmpty(model.FullName))
                ModelState.AddModelError("FullName", "Employee expected");
            if (string.IsNullOrEmpty(model.ShipAddress))
                ModelState.AddModelError("ShipAddress", "ShipAddress expected");
            if (string.IsNullOrEmpty(model.ShipCity))
                ModelState.AddModelError("ShipCity", "ShipCity expected");
            if (string.IsNullOrEmpty(model.ShipCountry))
                ModelState.AddModelError("ShipCountry", "ShipCountry expected");
            if (model.Freight <= 0)
                ModelState.AddModelError("Freight", "Freight expected");
            if (model.OrderDate == DateTime.MinValue)
                ModelState.AddModelError("OrderDate", "OrderDate expected");
            if (model.RequiredDate == DateTime.MinValue)
                ModelState.AddModelError("RequiredDate", "RequiredDate expected");
            if (model.ShippedDate == DateTime.MinValue)
                ModelState.AddModelError("ShippedDate", "ShippedDate expected");
            if (Convert.ToDateTime(model.RequiredDate).CompareTo(Convert.ToDateTime(model.OrderDate)) <= 0)
                ModelState.AddModelError("Date", "RequiredDate and OrderDate");

            Order addOrder = new Order();
            if(model.OrderID == 0)
            {
                    ViewBag.Title = "Create Order";
                addOrder.OrderID = model.OrderID;
                addOrder.EmployeeID =Convert.ToInt32(model.FullName);
                addOrder.ShipperID = Convert.ToInt32(model.ShipperCompanyName);
                addOrder.CustomerID = Convert.ToString(model.CustomerCompanyName);
                addOrder.RequiredDate = Convert.ToDateTime(model.RequiredDate);
                addOrder.OrderDate = Convert.ToDateTime(model.OrderDate);
                addOrder.ShippedDate = Convert.ToDateTime(model.ShippedDate);
                addOrder.ShipCity = Convert.ToString(model.ShipCity);
                addOrder.ShipCountry = Convert.ToString(model.ShipCountry);
                addOrder.ShipAddress = Convert.ToString(model.ShipAddress);
                addOrder.Freight = Convert.ToDecimal(model.Freight);
                OrderBLL.Add(addOrder);
            }
            return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ":" + ex.StackTrace);
                return View(model);
            }
        }
    }
}