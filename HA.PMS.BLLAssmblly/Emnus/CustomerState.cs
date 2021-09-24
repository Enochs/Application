using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HA.PMS.BLLAssmblly.Emnus
{
    public class CustomerState
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

        /// <summary>
        /// 获取枚举类子项描述信息
        /// </summary>
        /// <param name="enumSubitem">枚举类子项</param>        
        public static string GetEnumDescriptions(Enum enumSubitem)
        {
            string strValue = enumSubitem.ToString();

            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);
            Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
            {
                return strValue;
            }
            else
            {
                DescriptionAttribute da = (DescriptionAttribute)objs[0];
                return da.Description;
            }

        }
    }

    public enum CustomerStates
    {
        [Description("新录入")]
        New = 1,
        [Description("未邀约")]
        DidNotInvite = 3,
        [Description("邀约中")]
        DoInvite = 5,
        [Description("邀约成功")]
        InviteSucess = 6,
        [Description("流失(邀约)")]
        InviteLose = 7,
        [Description("未跟单")]
        DidNotFollowOrder = 8,
        [Description("到店")]
        BeginFollowOrder = 9,
        [Description("成功预定")]
        SucessOrder = 13,
        [Description("流失(跟单)")]
        OrderLose = 10,
        [Description("报价单审核中")]
        DoingChecksQuotedPrice = 15,
        [Description("执行中")]
        DoingCarrytask = 19,
        [Description("退单")]
        BackOrder = 20,
        [Description("已签约")]
        NewCarrytask = 24,
        [Description("流失")]
        Lose = 29,
        [Description("二选一")]
        Youxuan = 202,
        [Description("多选一")]
        Other = 203,
        [Description("找到燃烧点")]
        FirePoint = 205,
        [Description("已完成")]
        Finish = 208,
        [Description("已回访")]
        Evaulation = 207,
        [Description("已完结")]
        CompleteEnd = 206,

    }
}
