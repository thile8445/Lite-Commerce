using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult SignOut()
        {
            return RedirectToAction("SignIn");
        }
        [AllowAnonymous]
        public ActionResult SignIn()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}