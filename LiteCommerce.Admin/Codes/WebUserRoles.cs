using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
      /// <summary>
      /// Định nghĩa danh sách các Role của user
      /// </summary>
      public class WebUserRoles
      {
            /// <summary>
            /// Không xác định
            /// </summary>
            public const string ANONYMOUS = "anonymous";
            /// <summary>
            /// Nhân viên
            /// </summary>
            public const string STAFF = "staff";
            /// <summary>
            /// Quản trị hệ thống
            /// </summary>
            public const string ADMINISTRATOR = "administrator";
            /// <summary>
            /// Quản trị tài khoản
            /// </summary>
            public const string MANAGEACCOUNT = "manageaccount";
            /// <summary>
            /// Quản trị dữ liệu liên quan đến catalog
            /// </summary>
            public const string MANAGEDATA = "managedata";     
      }
}