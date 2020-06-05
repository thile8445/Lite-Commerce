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
    public class ProductController : Controller
    {
        // GET: Product

        public ActionResult Index(int page = 1, string searchValue = "",string Category = "",string Supplier = "")
        {
            int pageSize = 5;
            int rowCount = 0;
            List<Product> ListOfProducts = CatalogBLL.ListOfProducts(page, pageSize, searchValue,Category,Supplier, out rowCount);
            var model = new ProductPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Supplier = Supplier,
                Category = Category,
                Data = ListOfProducts
            };
            return View(model);
        }
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create new Product";
            }
            else
            {
                ViewBag.Title = "Edit a Product";
            }
            return View();
        }

    }
}