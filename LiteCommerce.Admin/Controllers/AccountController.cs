using LiteCommerce.Admin.Codes;
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
    public class AccountController : Controller
    {
        // GET: Account
        
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult SignOut()
        {
            //TODO : sign out
            Session.Abandon();
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SignIn(string email="",string password="")
        {

            //TODO:kiểm tra tài khoản từ cơ sở dữ liệu
            //if (email == "thi@gmail.com" && password == "123")
            //{
            //    // ghi nhân phiên đăng nhập của tài khoản
            //    System.Web.Security.FormsAuthentication.SetAuthCookie(email, false);
            //    // chuyển trang darhboard
            //    return RedirectToAction("Index", "Dashboard");
            //}
            //else
            //{
            //    ModelState.AddModelError("LoginError","Login fail");
            //    ViewBag.Email = email;
            //    return View();
            //}           

            //kiểm tra thông tin tài khoản
            password = EncodeMD5.EnCodeMD5(password);
            UserAccount user = UserAccountBLL.Authorize(email,password,UserAccountTypes.Employee);
            if(user != null)
            {
                WebUserData userData = new WebUserData()
                {
                    UserID = user.UserID,
                    FullName = user.FullName,
                    GroupName = user.Roles,
                    LoginTime = DateTime.Now,
                    SessionID = Session.SessionID,
                    ClientIP = Request.UserHostAddress,
                    Photo = user.Photo,
                    Title = user.Title
                };
                System.Web.Security.FormsAuthentication.SetAuthCookie(userData.ToCookieString(),false);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("LoginError", "Login fail");
                ViewBag.Email = email;
                return View();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
            
        }
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}