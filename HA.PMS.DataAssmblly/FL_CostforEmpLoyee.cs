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
    
    public partial class FL_CostforEmpLoyee
    {
        public int Costkeys { get; set; }
        public Nullable<int> EmpLoyeeID { get; set; }
        public string InsideRemark { get; set; }
        public string AccountClass { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<int> CostKey { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string EmpLoyeeName { get; set; }
    
        public virtual FL_Cost FL_Cost { get; set; }
    }
}
