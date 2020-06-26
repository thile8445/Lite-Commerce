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
                OrderDetailsView newOrder = new OrderDetailsView();
                EntityOrder Order = new EntityOrder()
                {
                    OrderID = 0
                };
                newOrder.Order = Order;
                return View(newOrder);
            }
            else
            {
                ViewBag.Title = "View Orders And OrderDetails";
                OrderDetailsView viewOrder = new OrderDetailsView();
                EntityOrder entityOrder = OrderBLL.GetOrders(Convert.ToInt32(id));
                viewOrder.Order = entityOrder;
                viewOrder.OrderDetails = OrderBLL.GetAllOrderDetails(Convert.ToInt32(id));
                double total = OrderBLL.Total(viewOrder.OrderDetails);
                ViewBag.Total = total.ToString();
                if (viewOrder == null)
                    return RedirectToAction("Index");
                return View(viewOrder);

            }
        }
        [HttpPost]
        public ActionResult Create(OrderDetailsView model)
        {
            try
            {

            if (string.IsNullOrEmpty(model.Order.CustomerCompanyName))
                ModelState.AddModelError("CustomerCompanyName", "CustomerCompanyName expected");
            if (string.IsNullOrEmpty(model.Order.ShipperCompanyName))
                ModelState.AddModelError("ShipperCompanyName", "ShipperCompanyName expected");
            if (string.IsNullOrEmpty(model.Order.FullName))
                ModelState.AddModelError("FullName", "Employee expected");
            if (string.IsNullOrEmpty(model.Order.ShipAddress))
                ModelState.AddModelError("ShipAddress", "ShipAddress expected");
            if (string.IsNullOrEmpty(model.Order.ShipCity))
                ModelState.AddModelError("ShipCity", "ShipCity expected");
            if (string.IsNullOrEmpty(model.Order.ShipCountry))
                ModelState.AddModelError("ShipCountry", "ShipCountry expected");
            if (model.Order.Freight <= 0)
                ModelState.AddModelError("Freight", "Freight expected");
            if (model.Order.OrderDate == DateTime.MinValue)
                ModelState.AddModelError("OrderDate", "OrderDate expected");
            if (model.Order.RequiredDate == DateTime.MinValue)
                ModelState.AddModelError("RequiredDate", "RequiredDate expected");
            if (model.Order.ShippedDate == DateTime.MinValue)
                ModelState.AddModelError("ShippedDate", "ShippedDate expected");
            if (Convert.ToDateTime(model.Order.RequiredDate).CompareTo(Convert.ToDateTime(model.Order.OrderDate)) <= 0)
                ModelState.AddModelError("Date", "RequiredDate and OrderDate");
            if (Convert.ToDateTime(model.Order.ShippedDate).CompareTo(Convert.ToDateTime(model.Order.OrderDate)) < 0)
                ModelState.AddModelError("ShippedDate", "RequiredDate and OrderDate");

                Order addOrder = new Order();
            if(model.Order.OrderID == 0)    
            {
                ViewBag.Title = "Create Order";
                addOrder.OrderID = model.Order.OrderID;
                addOrder.EmployeeID = Convert.ToInt32(model.Order.FullName);
                addOrder.ShipperID = Convert.ToInt32(model.Order.ShipperCompanyName);
                addOrder.CustomerID = Convert.ToString(model.Order.CustomerCompanyName);
                addOrder.RequiredDate = Convert.ToDateTime(model.Order.RequiredDate);
                addOrder.OrderDate = Convert.ToDateTime(model.Order.OrderDate);
                addOrder.ShippedDate = Convert.ToDateTime(model.Order.ShippedDate);
                addOrder.ShipCity = Convert.ToString(model.Order.ShipCity);
                addOrder.ShipCountry = Convert.ToString(model.Order.ShipCountry);
                addOrder.ShipAddress = Convert.ToString(model.Order.ShipAddress);
                addOrder.Freight = Convert.ToDecimal(model.Order.Freight);
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