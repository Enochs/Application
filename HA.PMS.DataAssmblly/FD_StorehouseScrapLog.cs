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
    
    public partial class FD_StorehouseScrapLog
    {
        public int ScrapID { get; set; }
        public string Reason { get; set; }
        public decimal ScrapSum { get; set; }
        public string ScrapEmpLoyee { get; set; }
        public System.DateTime ScrapDate { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateEmployee { get; set; }
        public int SourceProductId { get; set; }
    }
}
