using HA.PMS.BLLAssmblly.Emnus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HA.PMS.EditoerLibrary
{
    public class ddlCustomersState : DropDownList
    {
        public ddlCustomersState()
        {
            this.Width = 75;
            BinderData();
        }


        /// <summary>
        /// 绑定类型
        /// </summary>
        private void BinderData()
        {
            this.Items.Clear();
            var ValueList = Enum.GetValues(typeof(CustomerStates));
            foreach (var ObjItem in ValueList)
            {

                //ObjItem
                this.Items.Add(new ListItem(CustomerState.GetEnumDescription(ObjItem), (int)ObjItem + string.Empty));
            }
            this.Items.Add(new System.Web.UI.WebControls.ListItem("无", "-1"));
            this.Items.FindByText("无").Selected = true;
            //var EnumFiles = typeof(CustomerStates).GetFields();
            //var DIS= EnumFiles[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);

            //Type type = en.GetType(); 
            //MemberInfo[] memInfo = type.GetMember(en.ToString());
            //if (memInfo != null && memInfo.Length > 0)
            //{
            //    object[] attrs = memInfo[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            //    if (attrs != null && attrs.Length > 0)
            //        return ((DescriptionAttribute)attrs[0]).Description;
            //}
            //return en.ToString();
        }

        /// <summary>
        /// 邀约分派
        /// </summary>
        public void DataBinders()
        {
            this.Items.Clear();
            var ValueList = Enum.GetValues(typeof(CustomerStates));
            foreach (var ObjItem in ValueList)
            {

                //ObjItem
                if ((int)ObjItem == 3 || (int)ObjItem == 5 || (int)ObjItem == 6)
                {
                    this.Items.Add(new ListItem(CustomerState.GetEnumDescription(ObjItem), (int)ObjItem + string.Empty));
                }
            }
            this.Items.Add(new System.Web.UI.WebControls.ListItem("无", "-1"));
            this.Items.FindByText("无").Selected = true;
        }

        /// <summary>
        /// 派单中改派
        /// </summary>
        public void DataBinderOrder()
        {
            this.Items.Clear();
            var ValueList = Enum.GetValues(typeof(CustomerStates));
            foreach (var ObjItem in ValueList)
            {

                //ObjItem
                if ((int)ObjItem == 6 || (int)ObjItem == 8 || (int)ObjItem == 9 || (int)ObjItem == 29 || (int)ObjItem == 202 || (int)ObjItem == 203)
                {
                    this.Items.Add(new ListItem(CustomerState.GetEnumDescription(ObjItem), (int)ObjItem + string.Empty));
                }
            }
            this.Items.Add(new System.Web.UI.WebControls.ListItem("无", "-1"));
            this.Items.FindByText("无").Selected = true;
        }

        /// <summary>
        /// 跟单
        /// </summary>
        public void DataBindersOrder()
        {
            this.Items.Clear();
            var ValueList = Enum.GetValues(typeof(CustomerStates));
            foreach (var ObjItem in ValueList)
            {

                //ObjItem
                if ((int)ObjItem == 9 || (int)ObjItem == 202 || (int)ObjItem == 203 || (int)ObjItem == 205)
                {
                    this.Items.Add(new ListItem(CustomerState.GetEnumDescription(ObjItem), (int)ObjItem + string.Empty));
                }
            }
            this.Items.Add(new System.Web.UI.WebControls.ListItem("无", "-1"));
            this.Items.FindByText("无").Selected = true;
        }
    }
}
