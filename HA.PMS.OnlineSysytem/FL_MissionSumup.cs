//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HA.PMS.OnlineSysytem
{
    using System;
    using System.Collections.Generic;
    
    public partial class FL_MissionSumup
    {
        public int SumID { get; set; }
        public Nullable<int> MissionID { get; set; }
        public Nullable<int> DetailedID { get; set; }
        public string SumUp { get; set; }
        public Nullable<int> ImageType { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsFinish { get; set; }
        public string Title { get; set; }
        public string Counselingadvice { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual FL_MissionManager FL_MissionManager { get; set; }
    }
}
