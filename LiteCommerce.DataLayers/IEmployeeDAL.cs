using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Hiển thị danh sách Employee ,phân trang và tìm kiếm
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Employee> List(int page, int pagesize, string searchValue);
        /// <summary>
        /// Đếm số lượng Employee
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
        /// <summary>
        /// Lấy Employee
        /// </summary>
        /// <returns></returns>
        Employee Get(int employeeID);
        /// <summary>
        /// Thêm Employee.Hàm trả về ID Employee được bổ sung.
        /// Nếu lỗi ,hàm trả về giá trj nhỏ hơn hoặc bằng 0.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Employee data);
        /// <summary>
        /// Sữa đỗi 1 Product.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);
        /// <summary>
        /// Xóa nhiều Employee.
        /// Trả về số lượng được xóa.
        /// </summary>
        /// <param name="employeeIDs"></param>
        /// <returns></returns>
        int Delete(int[] employeeIDs);
        bool ChangePassword(int id, string password, string nPassword,string aPassword);
        List<Employee> GetAll();
       
    }
}
