using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            int rowCount = 0;
            List<Customer> model = CatalogBLL.ListOfCustomers(1, 10, "", out rowCount);
            ViewBag.rowCount = rowCount;
            return View(model);

        }
        public ActionResult Input(string id ="")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create a Customer";
            }
            else
            {
                ViewBag.Title = "Edit a Customer";
            }
            return View();
        }
    }
}