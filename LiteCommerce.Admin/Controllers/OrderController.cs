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
        public ActionResult Index(int page = 1, string searchValue = "",string country = "")
        {
            int pageSize = 5;
            int rowCount = 0;
            List<Order> ListOfOrder = OrderBLL.ListOfOrders(page, pageSize, "",country, out rowCount);
            var model = new OrderPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                ShipperCountry = country,
                Data = ListOfOrder
                
            };
            return View(model);
        }

        public ActionResult Create(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create a Order";
            }
            else
            {
                ViewBag.Title = "Edit a Order";
            }
            return View();
        }
    }
}