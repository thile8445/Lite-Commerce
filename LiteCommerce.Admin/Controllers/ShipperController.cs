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
    public class ShipperController : Controller
    {
        // GET: Shipper
        
        public ActionResult Index()
        {
            int rowCount = 0;
            List<Shipper> model = CatalogBLL.ListOfShippers(1, 10, "", out rowCount);
            ViewBag.rowCount = rowCount;
            return View(model);
        }
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create new Shipper";
            }
            else
            {
                ViewBag.Title = "Edit a Shipper";
            }
            return View();
        }
    }
}