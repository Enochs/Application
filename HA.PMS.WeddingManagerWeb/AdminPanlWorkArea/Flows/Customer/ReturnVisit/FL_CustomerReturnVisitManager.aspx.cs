/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.16
 Description:新人回访记录
 History:修改日志
 
 Author:杨洋
 date:2013.4.16
 version:好爱1.0
 description:修改描述
 
 
 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.ReturnVisit
{
    public partial class FL_CustomerReturnVisitManager : SystemPage
    {

        Customers objCustomenrBLL = new Customers();
        Employee objEmployeeBLL = new Employee();
        CustomerReturnVisit objCustomerReturnVisitBLL = new CustomerReturnVisit();

        VisitState ObjStateBLL = new VisitState();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDLDataBinder();
                DataBinder();
            }
        }

        #region 下拉框绑定
        /// <summary>
        /// 绑定
        /// </summary>
        public void DDLDataBinder()
        {
            var DataList = ObjStateBLL.GetByAll();
            ddlCustomerState.DataValueField = "ID";
            ddlCustomerState.DataTextField = "StatenName";
            ddlCustomerState.DataBind(DataList);
            ddlCustomerState.Items.Add(new ListItem { Text = "请选择", Value = "-1" });
            ddlCustomerState.Items.FindByValue("-1").Selected = true;
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>
        protected void DataBinder()
        {
            var objParmList = new List<PMSParameters>();

            //录入人
            objParmList.Add("CreateEmployee", User.Identity.Name.ToString().ToInt32(), NSqlTypes.Equal, true);

            //按到店时间查询
            objParmList.Add(DateRanger.IsNotBothEmpty, "OrderCreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);

            //按回访时间查询
            objParmList.Add(DateRanger1.IsNotBothEmpty, "ReasonsDate", DateRanger1.StartoEnd, NSqlTypes.DateBetween);

            //按新人姓名查询
            if (txtGroom.Text != string.Empty)
            {
                objParmList.Add("Bride", txtGroom.Text, NSqlTypes.LIKE);
                objParmList.Add("Groom", txtGroom.Text, NSqlTypes.ORLike);
            }

            //按联系电话查询
            if (txtBrideCellPhone.Text != string.Empty)
            {
                objParmList.Add("BrideCellPhone", txtBrideCellPhone.Text, NSqlTypes.StringEquals);
                objParmList.Add("GroomCellPhone", txtBrideCellPhone.Text, NSqlTypes.OR);
            }
            //说明是已经回访过得客户
            objParmList.Add("IsReturn", true, NSqlTypes.Bit);


            int startIndex = ReturnPager.StartRecordIndex;
            int resourceCount = 0;
            var query = new CustomerReturnVisit().GetByWhereParameter(objParmList, "OrderCreateDate", ReturnPager.PageSize, ReturnPager.CurrentPageIndex, out resourceCount);
            ReturnPager.RecordCount = resourceCount;
            rptReturn.DataBind(query);


        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        protected void ReturnPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 点击查找
        /// <summary>
        /// 查找
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 获取客户状态
        /// <summary>
        /// 获取状态
        /// </summary>  
        public string GetState(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var Model = objCustomenrBLL.GetByID(CustomerID);
            if (Model != null)
            {
                if (Model.VisisState != null)
                {
                    return ObjStateBLL.GetByID(Model.VisisState).StatenName;
                }
                else
                {
                    return "未知状态";
                }
            }
            return "未知状态";
        }
        #endregion

        #region 会员标志
        /// <summary>
        /// 会员标志
        /// </summary>
        protected void rptReturn_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Customers ObjCustomersBLL = new Customers();
            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;
            int CustomerID = (e.Item.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();
            var CustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            if (CustomerModel.IsVip == true)            //该客户是会员
            {
                imgIcon.Visible = true;
            }
            else     //不是会员
            {
                imgIcon.Visible = false;
            }
        }
        #endregion
    }
}