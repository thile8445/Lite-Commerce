﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class DashboardController : Controller
    {

        // GET: Dashboard
        [Authorize]
        public ActionResult Index()
        {
            //return RedirectToAction("~/Account/SignIn");
            return View();
        }
    }
}