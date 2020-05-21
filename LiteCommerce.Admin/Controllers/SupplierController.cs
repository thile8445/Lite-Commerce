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
    public class SupplierController : Controller
    {
        // GET: Supplier
        
        public ActionResult Index()
        {
            int rowCount = 0;
            List<Supplier> model = CatalogBLL.ListOfSuppliers(1, 10, "", out rowCount);
            ViewBag.rowCount = rowCount;
            return View(model);
        }
        /// <summary>
        /// in put
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Input(string id="")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create new Supplier";
            }
            else
            {
                ViewBag.Title = "Edit a Supplier";
            }
            return View();
        }
    }

}