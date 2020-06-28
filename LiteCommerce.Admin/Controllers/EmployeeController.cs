using LiteCommerce.Admin.Codes;
using LiteCommerce.Admin.Models;
using LiteCommerce.Admin.Models.Password;
using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize(Roles =WebUserRoles.MANAGEACCOUNT)]
    
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 3;
            int rowCount = 0;
            List<Employee> ListOfEmployee = EmloyeeBLL.ListOfEmployees(page, pageSize, searchValue, out rowCount);
            var model = new EmployeePaginationResult()
            { 
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = ListOfEmployee
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create new Employee";
                Employee newEmployee = new Employee()
                {
                    EmployeeID = 0
                };
                return View(newEmployee);
            }
            else
            {
                ViewBag.Title = "Edit a Employee";
                Employee editEmployee = EmloyeeBLL.GetEmployee(Convert.ToInt32(id));
                if (editEmployee == null)
                    return RedirectToAction("Index");
                return View(editEmployee);
            }
        }
        [HttpPost]
        public ActionResult Input(Employee model, HttpPostedFileBase PhotoPath,string PhotoPathDraft,string staff="",string manageaccount="",string managedata="")
        {
            try
            {
                //TODO :Kiểm tra tính hợp lệ của dữ liệu nhập vào
                if (string.IsNullOrEmpty(model.FirstName))
                    ModelState.AddModelError("FirstName", "FirstName expected");
                if (string.IsNullOrEmpty(model.LastName))
                    ModelState.AddModelError("LastName", "LastName expected");
                if (string.IsNullOrEmpty(model.Title))
                    ModelState.AddModelError("Title", "Title expected");
                if (string.IsNullOrEmpty(model.Password))
                    ModelState.AddModelError("Password", "Password expected");
                if (string.IsNullOrEmpty(model.PhotoPath))
                    ModelState.AddModelError("PhotoPath", "PhotoPath expected");
                if (string.IsNullOrEmpty(model.Email))
                    ModelState.AddModelError("Email", "Email expected");
                if (model.BirthDate == DateTime.MinValue)
                    ModelState.AddModelError("BirthDate", "BirthDate expected");
                if (model.HireDate == DateTime.MinValue)
                    ModelState.AddModelError("HireDate", "HireDate expected");
                if (Convert.ToDateTime(model.HireDate).CompareTo(Convert.ToDateTime(model.BirthDate)) <= 0)
                    ModelState.AddModelError("Date", "Date expected");
                if (string.IsNullOrEmpty(staff) && string.IsNullOrEmpty(manageaccount) && string.IsNullOrEmpty(managedata))
                    ModelState.AddModelError("Roles", "Roles expected");
                if (string.IsNullOrEmpty(model.Email))
                    model.Email = "";
                if (string.IsNullOrEmpty(model.Address))
                    model.Address = "";
                if (string.IsNullOrEmpty(model.Country))
                    model.Country = "";
                if (string.IsNullOrEmpty(model.City))
                    model.City = "";
                if (string.IsNullOrEmpty(model.HomePhone))
                    model.HomePhone = "";
                if (string.IsNullOrEmpty(model.Notes))
                    model.Notes = "";
                if (string.IsNullOrEmpty(model.PhotoPath))
                    model.PhotoPath = "";
                //TODO :upload image
                if (PhotoPath != null)
                {
                    string FileName = $"{DateTime.Now.Ticks}{Path.GetExtension(PhotoPath.FileName)}";
                    string path = Path.Combine(Server.MapPath("~/Images/uploads"), FileName);
                    PhotoPath.SaveAs(path);
                    model.PhotoPath = FileName;
                }
                if (string.IsNullOrEmpty(staff))
                {
                   
                    if (string.IsNullOrEmpty(manageaccount) && string.IsNullOrEmpty(managedata))
                    {
                        model.Roles = "";
                    }
                    else if (string.IsNullOrEmpty(managedata))
                    {
                        model.Roles = manageaccount;
                    }
                    else if (string.IsNullOrEmpty(manageaccount))
                    {
                        model.Roles = managedata;
                    }
                    else
                    {
                        model.Roles = manageaccount + "," + managedata;
                    }
                }
                else 
                {
                    if (string.IsNullOrEmpty(manageaccount) && string.IsNullOrEmpty(managedata))
                    {
                        model.Roles = staff;
                    }
                    else if (string.IsNullOrEmpty(managedata))
                    {
                        model.Roles =staff+","+ manageaccount;
                    }
                    else if (string.IsNullOrEmpty(manageaccount))
                    {
                        model.Roles = staff + "," + managedata;
                    }
                    else
                    {
                        model.Roles = staff + "," + manageaccount + "," + managedata;
                    }
                }
                
                if (model.EmployeeID == 0)
                {
                    model.Password = EncodeMD5.EnCodeMD5(model.Password);
                    EmloyeeBLL.AddEmployee(model);
                }
                else
                {
                    if (string.IsNullOrEmpty(model.PhotoPath))
                    {
                        model.PhotoPath = PhotoPathDraft;
                    }
                    EmloyeeBLL.UpdateEmployee(model);
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
        public ActionResult Delete(int[] employeeIDs)
        {
            if (employeeIDs != null)
            {
                EmloyeeBLL.DeleteEmployees(employeeIDs);

            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult ChangePassword(string Id ="")
        {
            EmployeePassword employeePassword = new EmployeePassword()
            {
                Id = Convert.ToInt32(Id)
            };
            ViewBag.Title = "ChangePassword";
            return View(employeePassword);
        }
        [HttpPost]
        public ActionResult ChangePassword(EmployeePassword model)
        {

            try
            {
                if (string.IsNullOrEmpty(model.password))
                    ModelState.AddModelError("password", "Old password expected");
                if (string.IsNullOrEmpty(model.nPassword))
                    ModelState.AddModelError("nPassword", "New password expected");
                if (string.IsNullOrEmpty(model.aPassword))
                    ModelState.AddModelError("aPassword", "Password expected");
                model.password = EncodeMD5.EnCodeMD5(model.password);
                model.nPassword = EncodeMD5.EnCodeMD5(model.nPassword);
                model.aPassword = EncodeMD5.EnCodeMD5(model.aPassword);
                EmloyeeBLL.ChangePassword(model.Id, model.password, model.nPassword, model.aPassword);
                return RedirectToAction("Input/" + model.Id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ":" + ex.StackTrace);
                return View(model);
            }
        }
    }
}