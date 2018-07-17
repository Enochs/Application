using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Emnus
{
    public class MissionType
    {
        public static string GetEnumDescription(object e)
        {
            //获取字段信息
            System.Reflection.FieldInfo[] ms = e.GetType().GetFields();

            Type t = e.GetType();
            foreach (System.Reflection.FieldInfo f in ms)
            {
                //判断名称是否相等
                if (f.Name != e.ToString()) continue;

                //反射出自定义属性
                foreach (Attribute attr in f.GetCustomAttributes(true))
                {
                    //类型转换找到一个Description，用Description作为成员名称
                    System.ComponentModel.DescriptionAttribute dscript = attr as System.ComponentModel.DescriptionAttribute;
                    if (dscript != null)
                        return dscript.Description;
                }

            }

            //如果没有检测到合适的注释，则用默认名称
            return e.ToString();
        }
    }

    public enum MissionTypes
    {
        [Description("/AdminPanlWorkArea/Invite/Customer/Donotinvite.aspx")]
        Tel = 1,

        [Description("/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx")]
        Invite = 2,

        [Description("/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx")]
        Order = 3,

        [Description("/AdminPanlWorkArea/QuotedPrice/QuotedPriceListCreateEdit.aspx")]
        Quoted = 4,

        [Description("/AdminPanlWorkArea/QuotedPrice/QuotedPriceChecks.aspx")]
        QuotedCheck = -4,

        [Description("/AdminPanlWorkArea/QuotedPrice/DispatchingManager.aspx")]
        Celebration = 5,

        //收款计划
        [Description("/AdminPanlWorkArea/QuotedPrice/QuotedCollectionsPlanCreate.aspx")]
        QuotedCollectionsPlan = -5,
         
        [Description("/AdminPanlWorkArea/Carrytask/DispatchingManager.aspx")]
        Dispatching = 6,

        [Description("/AdminPanlWorkArea/Carrytask/CarrytaskTab.aspx")]
        MyDispatching = 7,

        [Description("LOCAL")]
        Plan = 8,

         [Description("/AdminPanlWorkArea/Carrytask/CarrytaskWeddingPlanningCreate.aspx")]
        QuotedPlan = -8,

        [Description("LOCAL")]//自身任务
        Mine=9,

        [Description("LOCAL")]//上级任务
        Uper = 10,
        //设计单
        [Description("/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignCreate.aspx")]
        Design = 11,
    }
}
