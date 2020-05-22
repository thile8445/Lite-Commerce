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
    [Authorize]
    public class SupplierController : Controller
    {
        // GET: Supplier
        
        public ActionResult Index(int page = 1,string searchValue = "")
        {
            int pageSize = 3;
            int rowCount = 0;
            List<Supplier> ListOfSuppliers = CatalogBLL.ListOfSuppliers(page, pageSize, searchValue, out rowCount);
            var model = new SupplierPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = ListOfSuppliers
            };
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