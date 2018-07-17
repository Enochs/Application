using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Emnus
{
    public enum OrderSumTypes
    {
        /// <summary>
        /// 总预约量
        /// </summary>
        [Description("总预约量")]
        TotalOrderCount = 1,

        /// <summary>
        /// 实际到店量
        /// </summary>
        [Description("实际到店量")]
        ActualOrderCount = 2,

        /// <summary>
        /// 成功预定量
        /// </summary>
        [Description("成功预定量")]
        SuccessOrderCount = 3,

        /// <summary>
        /// 流失量
        /// </summary>
        [Description("流失量")]
        LoseCount = 4,

        /// <summary>
        /// 定金总额
        /// </summary>
        [Description("定金总额")]
        TotalEarnestMoney = 5,

        /// <summary>
        /// 订单总额
        /// </summary>
        [Description("订单总额")]
        TotalFinishAmount = 6
    }
}
