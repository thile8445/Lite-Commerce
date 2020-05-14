using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}