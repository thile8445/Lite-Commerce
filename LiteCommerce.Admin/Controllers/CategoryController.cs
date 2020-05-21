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
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            int rowCount = 0;
            List<Category> model = CatalogBLL.ListOfCategories(1, 10, "", out rowCount);
            ViewBag.rowCount = rowCount;
            return View(model);
        }
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create new Category";
            }
            else
            {
                ViewBag.Title = "Edit a Category";
            }
            return View();
        }
    }
}