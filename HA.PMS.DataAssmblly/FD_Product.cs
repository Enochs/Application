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
    
    public partial class FD_Product
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
        public string ProductName { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<decimal> ProductPrice { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
        public string Unit { get; set; }
        public string Data { get; set; }
        public string Specifications { get; set; }
        public string Explain { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<int> ProductProject { get; set; }
        public string Remark { get; set; }
    
        public virtual FD_Category FD_Category { get; set; }
        public virtual FD_Supplier FD_Supplier { get; set; }
    }
}
