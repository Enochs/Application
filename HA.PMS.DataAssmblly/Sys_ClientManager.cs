//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HA.PMS.DataAssmblly
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sys_ClientManager
    {
        public int ClientID { get; set; }
        public string MAC { get; set; }
        public string HDID { get; set; }
        public string CUPID { get; set; }
        public Nullable<bool> IsClose { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> CreateUser { get; set; }
        public int DepartmentID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string MD5Val { get; set; }
        public Nullable<System.DateTime> PeriodOfValidity { get; set; }
        public System.DateTime DayOfValidity { get; set; }
        public System.DateTime LoginDate { get; set; }
        public Nullable<int> QQ { get; set; }
        public string Email { get; set; }
    
        public virtual Sys_Department Sys_Department { get; set; }
        public virtual Sys_Employee Sys_Employee { get; set; }
    }
}
