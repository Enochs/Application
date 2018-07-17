using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Emnus
{

    /// <summary>
    /// 任务频道对接
    /// </summary>
    public  class MissionChannel
    {
        //电话营销分派 对应未邀约
        public static readonly string FL_TelemarketingManager = "Donotinvite";

        //销售派单 对应策划报价
        public static readonly string StarOrder = "QuotedPriceList";


        //制作执行明细对应
        public static readonly string DispatchingManager = "DispatchingforEmployee";


        //物料派工 对应我的执行任务
        public static readonly string CarrytaskCreate = "DoingTask";

        //婚礼统筹 设计单
        public static readonly string Quoted = "QuotedPriceWorkPanel";
        //public static readonly string FL_TelemarketingManager = "Donotinvite";
        //public static readonly string FL_TelemarketingManager = "Donotinvite";
        //public static readonly string FL_TelemarketingManager = "Donotinvite";
        //public static readonly string FL_TelemarketingManager = "Donotinvite";
        //public static readonly string FL_TelemarketingManager = "Donotinvite";
 
 


    }
}
