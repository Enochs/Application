using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.FD;
 
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch
{
    public partial class Salestrends : NoneStylePage
    {
        Customers ObjCustomersBLL = new Customers();
        Order ObjOrderBLL = new Order();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();

            }
        }

        /// <summary>
        /// 绑定成功预定的需要派单的
        /// </summary>
        private void BinderData()
        {

            //顶部

            List<ObjectParameter> ObjParList = new List<ObjectParameter>();
            ObjParList.Add(new ObjectParameter("State_NumOr", ((int)CustomerStates.DidNotFollowOrder).ToString() + "," + ((int)CustomerStates.BeginFollowOrder).ToString() + "," + ((int)CustomerStates.DidNotFollowOrder)));
            ObjParList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));
            int SourceCount = 0;
            this.daltop.DataSource = ObjOrderBLL.GetOrderCustomerByIndex(10000,0, out SourceCount, ObjParList); ;
            daltop.DataBind();


            //建立姓任
            ObjParList = new List<ObjectParameter>();
            ObjParList.Add(new ObjectParameter("State", 203));
            ObjParList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));



    
            var DataList = ObjOrderBLL.GetOrderCustomerByIndex(10000, 0, out SourceCount, ObjParList);

            daltopEmpLoyeeName.DataSource = DataList;
            daltopEmpLoyeeName.DataBind();


            //燃烧点
            ObjParList = new List<ObjectParameter>();
            ObjParList.Add(new ObjectParameter("State", 205));
            ObjParList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));
            DataList = ObjOrderBLL.GetOrderCustomerByIndex(10000, 0, out SourceCount, ObjParList);
            dalFirepoint.DataSource = DataList;
            dalFirepoint.DataBind();


            //优选
            ObjParList = new List<ObjectParameter>();
            ObjParList.Add(new ObjectParameter("State", 202));
            ObjParList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));
            DataList = ObjOrderBLL.GetOrderCustomerByIndex(10000, 0, out SourceCount, ObjParList);
            dalyouxuan.DataSource = DataList;
            dalyouxuan.DataBind();

            ObjParList = new List<ObjectParameter>();
            ObjParList.Add(new ObjectParameter("State", (int)CustomerStates.SucessOrder));
            ObjParList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));
            DataList = ObjOrderBLL.GetOrderCustomerByIndex(10000, 0, out SourceCount, ObjParList);
            dalSucess.DataSource = DataList;
            dalSucess.DataBind();

            //var SucessDataList = ObjOrderBLL.GetOrderCustomerByIndex(10000, 1, out SourceCount, ObjParList);

            //dtlSucessOrder.DataSource = SucessDataList;
            //dtlSucessOrder.DataBind();
        }

        /// <summary>
        /// 获取头像图片
        /// </summary>
        /// <param name="FlowCount"></param>
        /// <returns></returns>
        public string GetImageString(object FlowCount, object OrderID)
        {

            if (FlowCount.ToString().ToInt32() >= 3)
            {
                return "<img class='Right1' border='0'  style='width: 20px; height: 20px;' OrderID='" + OrderID + "' src='/AdminPanlWorkArea/Commandanddispatch/ManagerImage/baojing.png' />";

            }
            if (FlowCount.ToString().ToInt32() ==2)
            {
                return "<img   class='Right1'  border='0' style='width: 20px; height: 20px;' OrderID='" + OrderID + "'  src='/AdminPanlWorkArea/Commandanddispatch/ManagerImage/lingjie.png' />";
            }
            if (FlowCount.ToString().ToInt32() < 2)
            {
                return "<img  class='Right1'  border='0'  style='width: 20px; height: 20px;' OrderID='" + OrderID + "'  src='/AdminPanlWorkArea/Commandanddispatch/ManagerImage/zhengchang.png' />";
            }
            return string.Empty;
        }
    }
}