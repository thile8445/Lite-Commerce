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
    public class CountryController : Controller
    {
        // GET: Country
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 5;
            int rowCount = 0;
            List<Country> ListOfCountry = CountryBLL.ListOfCountry(page, pageSize, searchValue, out rowCount);
            var model = new CountryPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = ListOfCountry
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create new Country";
                Country newCountry = new Country()
                {
                    CountryID = 0
                };
                return View(newCountry);
            }
            else
            {
                ViewBag.Title = "Edit a Country";
                Country editCountry = CountryBLL.Get(Convert.ToInt32(id));
                if (editCountry == null)
                    return RedirectToAction("Index");
                return View(editCountry);
            }
        }
        [HttpPost]
        public ActionResult Input(Country model)
        {
            try
            {
                //TODO :Kiểm tra tính hợp lệ của dữ liệu nhập vào
                if (string.IsNullOrEmpty(model.CountryName))
                    ModelState.AddModelError("CountryName", "CountryName expected");
                if (string.IsNullOrEmpty(model.Abbreviation))
                    model.Abbreviation = "";
               
                //TODO :Lưu dữ liệu nhập vào
                if (model.CountryID == 0)
                {
                    CountryBLL.Add(model);
                }
                else
                {
                    CountryBLL.Update(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ":" + ex.StackTrace);
                return View(model);
            }
        }
        /// <summary>
        /// Xóa danh sách employee
        /// </summary>
        /// <param name="employeeIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int[] countries)
        {
            if (countries != null)
            {
                CountryBLL.Delete(countries);

            }
            return RedirectToAction("Index");

        }
    }
}