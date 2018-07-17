using System.ComponentModel;

namespace HA.PMS.BLLAssmblly.Emnus
{
    public enum InviteSumTypes
    {
        /// <summary>
        /// 客源量
        /// </summary>
        [Description("客源量")]
        TotalInviteCount = 1,

        /// <summary>
        /// 实际到店量
        /// </summary>
        [Description("实际到店量")]
        ActualInviteCount = 2,

        /// <summary>
        /// 邀约中量
        /// </summary>
        [Description("邀约中量")]
        InvitingCount = 3,

        /// <summary>
        /// 邀约成功量
        /// </summary>
        [Description("邀约成功量")]
        SuccessInviteCount = 4,

        /// <summary>
        /// 流失量
        /// </summary>
        [Description("流失量")]
        LoseCount = 5,

        /// <summary>
        /// 未邀约
        /// </summary>
        [Description("未邀约量")]
        NotInviteCount = 6,

        /// <summary>
        /// 订单总额
        /// </summary>
        [Description("订单总额")]
        TotalFinishAmount = 7
    }
}
