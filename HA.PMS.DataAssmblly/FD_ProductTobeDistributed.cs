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
    
    public partial class FD_ProductTobeDistributed
    {
        public int DistributedId { get; set; }
        public string SubmitPerson { get; set; }
        public Nullable<System.DateTime> SubmitDate { get; set; }
        public string OrderSubmit { get; set; }
        public string SupplierName { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProjectCategory { get; set; }
        public string Specifications { get; set; }
        public string Data { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public Nullable<decimal> SaleOrice { get; set; }
        public string Unit { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> ProductID { get; set; }
    }
}