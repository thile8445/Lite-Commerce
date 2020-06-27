using LiteCommerce.Admin.Models.Dashboard;
using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        public ActionResult Index()
        {
            DashboardResult dashboard = new DashboardResult();
            dashboard.countEmployees = EmloyeeBLL.CountAll();
            dashboard.countCategories = CatalogBLL.CountAllCategories();
            dashboard.countCustomers = CatalogBLL.CountAllCustomers();
            dashboard.countOrders = OrderBLL.CountAll();
            dashboard.countProducts = CatalogBLL.CountAllProducts();
            dashboard.countShippers = CatalogBLL.CountAllShippers();
            dashboard.countSuppliers = CatalogBLL.CountAllSuppliers();
            return View(dashboard);
        }
    }
}