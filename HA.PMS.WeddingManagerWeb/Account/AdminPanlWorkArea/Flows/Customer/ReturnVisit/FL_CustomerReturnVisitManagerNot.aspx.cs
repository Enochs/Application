/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.16
 Description:新人未回访记录
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
    public partial class FL_CustomerReturnVisitManagerNot : SystemPage
    {
        Customers objCustomenrBLL = new Customers();
        Employee objEmployeeBLL = new Employee();
        CustomerReturnVisit objCustomerReturnVisitBLL = new CustomerReturnVisit();
        Order ObjOrderBLL = new Order();
        HA.PMS.BLLAssmblly.Report.Report ObjReportBLL = new BLLAssmblly.Report.Report();

        #region 页面初始化
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCustomerState.DataBindersOrder();
                DataBinder();
            }
        }
        #endregion

        #region 获取成功预定时间
        /// <summary>
        /// 获取成功预定时间
        /// </summary>
        public string GetSucessOrderDate(object CustomerID)
        {

            var ObjModel = ObjReportBLL.GetByCustomerID(CustomerID.ToString().ToInt32(), User.Identity.Name.ToInt32());
            if (ObjModel != null)
            {
                return ObjModel.QuotedDateSucessDate.ToString();
            }
            return string.Empty;

        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void DataBinder()
        {
            var objParmList = new List<PMSParameters>();


            //录入人
            objParmList.Add("CreateEmployee", User.Identity.Name.ToString().ToInt32(), NSqlTypes.Equal, true);

            //按到店时间查询 
            objParmList.Add(DateRanger.IsNotBothEmpty, "OrderCreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);

            //按新人姓名查询
            if (txtGroom.Text != string.Empty)
            {
                objParmList.Add("Bride", txtGroom.Text, NSqlTypes.LIKE);
                objParmList.Add("Groom", txtGroom.Text, NSqlTypes.ORLike);
            }

            //按联系电话查询
            if (txtGroomCellPhone.Text != string.Empty)
            {
                objParmList.Add("BrideCellPhone", txtGroomCellPhone.Text, NSqlTypes.StringEquals);
                objParmList.Add("GroomCellPhone", txtGroomCellPhone.Text, NSqlTypes.OR);//
            }
            if (ddlCustomerState.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("State", ddlCustomerState.SelectedValue, NSqlTypes.Equal);
            }
            else
            {
                objParmList.Add("State", "9,202,203,205", NSqlTypes.IN);
            }

            //录入人
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                MyManager.GetEmployeePar(objParmList, "CreateEmployee");
            }

            objParmList.Add("IsReturn", false, NSqlTypes.Bit);

            int startIndex = ReturnPager.StartRecordIndex;
            int resourceCount = 0;
            var query = new CustomerReturnVisit().GetByWhereParameter(objParmList, "OrderCreateDate", ReturnPager.PageSize, ReturnPager.CurrentPageIndex, out resourceCount);
            ReturnPager.RecordCount = resourceCount;
            rptReturn.DataBind(query);
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 点击页码查询
        /// </summary>           
        protected void ReturnPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询结果集
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region Repeater 绑定事件
        /// <summary>
        /// 保存下次回访时间
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        protected void rptReturn_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                int CustomerID = e.CommandArgument.ToString().ToInt32();
                TextBox txtNextReturnDate = e.Item.FindControl("txtNextReturnDate") as TextBox;
                if (txtNextReturnDate.Text.Trim() != "")
                {
                    var Model = objCustomenrBLL.GetByID(CustomerID);
                    Model.NextReturnDate = txtNextReturnDate.Text.Trim().ToString().ToDateTime();
                    int result = objCustomenrBLL.Update(Model);
                    if (result > 0)
                    {
                        //保存操作日志
                        CreateHandle(CustomerID, txtNextReturnDate.Text.Trim().ToString());
                        JavaScriptTools.AlertWindow("保存成功", Page);
                        DataBinder();
                    }
                    else
                    {
                        JavaScriptTools.AlertWindow("保存失败,请稍候再试...", Page);
                    }
                }
                else
                {
                    JavaScriptTools.AlertWindow("请选择下次回访时间", Page);
                }

            }
        }
        #endregion


        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle(int CustomerID, string NextReturnDate)
        {
            Customers ObjCustomerBLL = new Customers();
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();

            var Model = ObjCustomerBLL.GetByID(CustomerID);

            HandleModel.HandleContent = "新人回访,客户姓名:" + Model.Bride + "/" + Model.Groom + ",修改下次回访时间：" + NextReturnDate;


            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 11;     //新人回访
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}