using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Customer
{
    public partial class CustomerExportManager : SystemPage
    {
        /// <summary>
        /// 内部员工
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();

        //统计
        Report ObjReportBLL = new Report();

        /// <summary>
        /// 客户
        /// </summary>
        BLLAssmblly.Flow.Customers customersBLL = new BLLAssmblly.Flow.Customers();


        string dates = "";
        List<PMSParameters> ObjparList = new List<PMSParameters>();

        #region 页面初始化
        /// <summary>
        /// 页面加载
        /// </summary>  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }
        #endregion

        #region 渠道类型选择
        /// <summary>
        /// 选择
        /// </summary>
        protected void ddlChannelType1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChannelName2.Items.Clear();
            ddlChannelName2.BindByParent(ddlChannelType1.SelectedValue.ToInt32());
            ddlChannelName2.Width = 134;
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 数据绑定
        /// </summary>      
        protected void BinderData(object sender, EventArgs e)
        {
            lblTelphone.Visible = false;
            txtTelphone.Text = string.Empty;
            txtTelphone.Visible = false;
            int startIndex = CustomersPager.StartRecordIndex;
            int resourceCount = 0;

            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()) == false)
            {
                rblEmployee.Visible = false;
            }
            if (ddlCustomersState1.SelectedValue.ToInt32() > 0)
            {
                ObjparList.Add("State", ddlCustomersState1.SelectedValue);
            }

            //新人姓名
            CstmNameSelector.AppandTo(ObjparList);
            //ObjparList.Add(!txtBride.Text.Equals(string.Empty), "ContactMan", txtBride.Text.Trim(), NSqlTypes.LIKE);

            //按联系电话查询
            ObjparList.Add(!txtBrideCellPhone.Text.Equals(string.Empty), "ContactPhone", txtBrideCellPhone.Text.Trim(), NSqlTypes.StringEquals);
            ObjparList.Add(!txtBrideCellPhone.Text.Equals(string.Empty), "BrideCellPhone", txtBrideCellPhone.Text.Trim(), NSqlTypes.OR);
            ObjparList.Add(!txtBrideCellPhone.Text.Equals(string.Empty), "GroomCellPhone", txtBrideCellPhone.Text.Trim(), NSqlTypes.OR);



            //按婚期查询
            if (ddltimerType.SelectedValue == "0" && DateRanger.IsNotBothEmpty)
            {
                ObjparList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            //按录入时间查询
            if (ddltimerType.SelectedValue == "1" && DateRanger.IsNotBothEmpty)
            {
                ObjparList.Add("CreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            //按店时间查询
            if (ddltimerType.SelectedValue == "2" && DateRanger.IsNotBothEmpty)
            {
                ObjparList.Add("OrderCreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }



            //按酒店查询
            ObjparList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text, NSqlTypes.LIKE);

            if (Request["Type"] == null)
            {
                //按责任人查询
                if (MyManager1.SelectedValue.ToInt32() == 0)
                {
                    ObjparList.Add("DutyEmployee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
                }
                else
                {
                    //责任人
                    if (rblEmployee.SelectedValue.ToInt32() == 0 && MyManager1.SelectedValue.ToInt32() != 0)
                    {
                        ObjparList.Add("DutyEmployee", MyManager1.SelectedValue, NSqlTypes.IN, true);
                    }
                    //电销(邀约人)
                    if (rblEmployee.SelectedValue.ToInt32() == 1 && MyManager1.SelectedValue.ToInt32() != 0)
                    {
                        ObjparList.Add("InviteEmployee", MyManager1.SelectedValue, NSqlTypes.IN, true);
                    }
                    //录入人
                    if (rblEmployee.SelectedValue.ToInt32() == 2 && MyManager1.SelectedValue.ToInt32() != 0)
                    {
                        ObjparList.Add("CreateEmployee", MyManager1.SelectedValue, NSqlTypes.IN, true);
                    }
                    //策划师
                    if (rblEmployee.SelectedValue.ToInt32() == 3 && MyManager1.SelectedValue.ToInt32() != 0)
                    {
                        ObjparList.Add("QuotedEmployee", MyManager1.SelectedValue, NSqlTypes.IN, true);
                    }
                }
            }

            //按渠道名称查询 
            if (ddlChannelType1.SelectedValue.ToInt32() > 0)
            {
                ObjparList.Add("ChannelType", ddlChannelType1.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }

            //按渠道名称查询 
            if (ddlChannelName2.SelectedValue.ToInt32() > 0)
            {
                ObjparList.Add("Channel", ddlChannelName2.SelectedItem.Text, NSqlTypes.StringEquals);
            }

            DateTime now = DateTime.Now;
            DateTime Star = new DateTime(now.Year, now.Month, 1);

            DateTime End = Star.AddMonths(1).AddDays(-1);

            if (Request["Type"] == "Look")
            {
                tblLook.Visible = false;
                int EmployeeID = Request["EmployeeID"].ToInt32();
                dates = Request["DateRanger"].ToString();
                Star = dates.Split(',')[0].ToString().ToDateTime();
                End = dates.Split(',')[1].ToString().ToDateTime();

                if (Request["Category"] == "SourceCount")
                {
                    ObjparList.Add("InviteEmployee", EmployeeID, NSqlTypes.Equal);
                    ObjparList.Add("CreateDate", Star + "," + End, NSqlTypes.DateBetween);
                }
                else if (Request["Category"] == "InSourceCount")
                {
                    ObjparList.Add("OrderEmployee", EmployeeID, NSqlTypes.Equal);
                    ObjparList.Add("OrderCreateDate", Star + "," + End, NSqlTypes.DateBetween);
                }
                else if (Request["Category"] == "ComeOrderCount")
                {

                    ObjparList.Add("OrderEmployee", EmployeeID, NSqlTypes.Equal);
                    ObjparList.Add("CreateDate", Star + "," + End, NSqlTypes.DateBetween);
                }
                else if (Request["Category"] == "NewOrderByMonth")
                {
                    ObjparList.Add("QuotedEmployee", EmployeeID, NSqlTypes.Equal);
                    ObjparList.Add("OrderSucessDate", Star + "," + End, NSqlTypes.DateBetween);
                }
            }


            var DataList = customersBLL.GetByWhereParameter(ObjparList, "RecorderDate", CustomersPager.PageSize, CustomersPager.CurrentPageIndex, out resourceCount);

            if (rblEmployee.SelectedValue.ToInt32() == 3 && MyManager1.SelectedValue.ToInt32() != 0)
            {
                var ViewDataList = customersBLL.GetByWhereParameter(ObjparList, "RecorderDate", 10000, 1, out resourceCount);
                var DataLists = ViewDataList.Where(C => GetQuotedEmployeesID(C.CustomerID) == MyManager1.SelectedValue.ToInt32()).ToList();
                rptCustomer.DataSource = DataLists.Skip((CustomersPager.CurrentPageIndex - 1) * 10).Take(10);
                rptCustomer.DataBind();
                CustomersPager.RecordCount = DataLists.Count;
                lblCustomerSum.Text = "客户数量：" + DataList.Count.ToString();
            }
            else
            {
                rptCustomer.DataSource = DataList;
                CustomersPager.RecordCount = resourceCount;
                rptCustomer.DataBind();
                lblCustomerSum.Text = "客户数量：" + resourceCount.ToString();
            }
        }
        #endregion

        #region 根据渠道类型ID获取渠道类型名称
        /// <summary>
        /// 获取类型名称
        /// </summary>
        private string GetChannelTypeName(int ChannelTypeId)
        {
            HA.PMS.DataAssmblly.FD_ChannelType channelType = new BLLAssmblly.FD.ChannelType().GetByID(ChannelTypeId);
            return object.ReferenceEquals(channelType, null) ? string.Empty : channelType.ChannelTypeName;
        }
        #endregion

        #region 根据ID获取员工姓名
        /// <summary>
        /// 获取员工姓名
        /// </summary>
        private string GetEmployeeName(int EmployeeID)
        {
            HA.PMS.DataAssmblly.Sys_Employee sys_Employee = new HA.PMS.BLLAssmblly.Sys.Employee().GetByID(EmployeeID);
            return object.ReferenceEquals(sys_Employee, null) ? sys_Employee.EmployeeName : string.Empty;
        }
        #endregion

        #region 点击确定事件
        protected void btnSetups_Click(object sender, EventArgs e)
        {
            int SourceCount = 0;
            List<PMSParameters> pars = new List<PMSParameters>();
            if (CstmSelector.Text == "")
            {
                JavaScriptTools.AlertWindow("请输入新人姓名", Page);
                return;
            }
            else
            {
                CstmSelector.AppandTo(pars);
            }
            Customers ObjCustomerBLL = new Customers();
            List<View_SSCustomer> DataList = ObjCustomerBLL.GetByWhereParameter(pars, "PartyDate", 10, 1, out SourceCount);
            if (DataList.Count == 1)
            {
                SS_Report ObjReportModel = ObjReportBLL.GetByCustomerID(DataList.FirstOrDefault().CustomerID, User.Identity.Name.ToInt32());
                ObjReportModel.DutyEmployee = User.Identity.Name.ToInt32();
                ObjReportBLL.Update(ObjReportModel);
                JavaScriptTools.AlertWindow("操作成功", Page);
            }
            else if (DataList.Count >= 2)
            {
                rptCustomer.DataBind(DataList);

                lblTelphone.Visible = true;
                txtTelphone.Visible = true;
                if (txtTelphone.Text != "")
                {
                    DataList = DataList.Where(C => C.BrideCellPhone == txtTelphone.Text.ToString() || C.GroomCellPhone == txtTelphone.Text.ToString()).ToList();
                    SS_Report ObjReportModel = ObjReportBLL.GetByCustomerID(DataList.FirstOrDefault().CustomerID, User.Identity.Name.ToInt32());
                    ObjReportModel.DutyEmployee = User.Identity.Name.ToInt32();
                    ObjReportBLL.Update(ObjReportModel);
                    JavaScriptTools.AlertWindow("操作成功", Page);
                    rptCustomer.DataBind(DataList);
                }
                if (txtTelphone.Text == "")
                {
                    return;
                }

            }
            else if (DataList.Count == 0)
            {
                JavaScriptTools.AlertWindow("没有查找到该用户信息", Page);
                return;
            }
        }
        #endregion

        #region 点击查询事件
        /// <summary>
        /// 查询
        /// </summary>     
        protected void BtnQuery_Click(object sender, EventArgs e)
        {
            CustomersPager.CurrentPageIndex = 1;
            BinderData(sender, e);
        }
        #endregion

        #region 会员图标的显示
        /// <summary>
        /// 图标
        /// </summary>

        public void IconShow()
        {
            for (int i = 0; i < rptCustomer.Items.Count; i++)
            {
                var ObjItem = rptCustomer.Items[i];
                Image imgIcon = ObjItem.FindControl("ImgIcon") as Image;
                int CustomerID = (ObjItem.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();
                var CustomerModel = customersBLL.GetByID(CustomerID);
                if (CustomerModel.IsVip == true)            //该客户是会员
                {
                    imgIcon.Visible = true;
                }
                else if (CustomerModel.IsVip == false)          //不是会员
                {
                    imgIcon.Visible = false;
                }
            }
        }
        #endregion


        #region 绑定完成事件
        /// <summary>
        /// 绑定完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void rptCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;
            int CustomerID = (e.Item.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();
            var CustomerModel = customersBLL.GetByID(CustomerID);
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

        #region 导出功能
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            int resourceCount = 0;
            GetParmList();
            var DataList = customersBLL.GetByWhereParameter(ObjparList, "RecorderDate", 20000, CustomersPager.CurrentPageIndex, out resourceCount);
            DataTable dt = ListToDataTable.ListToTable<View_SSCustomer>(DataList);
            ExcelUtil.RenderToExcel(dt, Context, "客户渠道信息.xls");
        }
        #endregion

        public void GetParmList()
        {
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()) == false)
            {
                rblEmployee.Visible = false;
            }
            if (ddlCustomersState1.SelectedValue.ToInt32() > 0)
            {
                ObjparList.Add("State", ddlCustomersState1.SelectedValue);
            }

            //新人姓名
            CstmNameSelector.AppandTo(ObjparList);
            //ObjparList.Add(!txtBride.Text.Equals(string.Empty), "ContactMan", txtBride.Text.Trim(), NSqlTypes.LIKE);

            //按联系电话查询
            ObjparList.Add(!txtBrideCellPhone.Text.Equals(string.Empty), "ContactPhone", txtBrideCellPhone.Text.Trim(), NSqlTypes.StringEquals);
            ObjparList.Add(!txtBrideCellPhone.Text.Equals(string.Empty), "BrideCellPhone", txtBrideCellPhone.Text.Trim(), NSqlTypes.OR);
            ObjparList.Add(!txtBrideCellPhone.Text.Equals(string.Empty), "GroomCellPhone", txtBrideCellPhone.Text.Trim(), NSqlTypes.OR);



            //按婚期查询
            if (ddltimerType.SelectedValue == "0" && DateRanger.IsNotBothEmpty)
            {
                ObjparList.Add("PartyDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            //按录入时间查询
            if (ddltimerType.SelectedValue == "1" && DateRanger.IsNotBothEmpty)
            {
                ObjparList.Add("CreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }

            //按店时间查询
            if (ddltimerType.SelectedValue == "2" && DateRanger.IsNotBothEmpty)
            {
                ObjparList.Add("OrderCreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }



            //按酒店查询
            ObjparList.Add(ddlHotel.SelectedValue.ToInt32() > 0, "Wineshop", ddlHotel.SelectedItem.Text, NSqlTypes.LIKE);


            //按渠道名称查询 
            if (ddlChannelType1.SelectedValue.ToInt32() > 0)
            {
                ObjparList.Add("ChannelType", ddlChannelType1.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }

            //按渠道名称查询 
            if (ddlChannelName2.SelectedValue.ToInt32() > 0)
            {
                ObjparList.Add("Channel", ddlChannelName2.SelectedItem.Text, NSqlTypes.StringEquals);
            }

            if (Request["Type"] == null)
            {
                //按责任人查询
                if (MyManager1.SelectedValue.ToInt32() == 0)
                {
                    ObjparList.Add("DutyEmployee", User.Identity.Name.ToInt32(), NSqlTypes.IN, true);
                }
                else
                {
                    //责任人
                    if (rblEmployee.SelectedValue.ToInt32() == 0 && MyManager1.SelectedValue.ToInt32() != 0)
                    {
                        ObjparList.Add("DutyEmployee", MyManager1.SelectedValue, NSqlTypes.IN, true);
                    }
                    //电销(邀约人)
                    if (rblEmployee.SelectedValue.ToInt32() == 1 && MyManager1.SelectedValue.ToInt32() != 0)
                    {
                        ObjparList.Add("InviteEmployee", MyManager1.SelectedValue, NSqlTypes.IN, true);
                    }
                    //录入人
                    if (rblEmployee.SelectedValue.ToInt32() == 2 && MyManager1.SelectedValue.ToInt32() != 0)
                    {
                        ObjparList.Add("CreateEmployee", MyManager1.SelectedValue, NSqlTypes.IN, true);
                    }
                    //策划师
                    //if (rblEmployee.SelectedValue.ToInt32() == 3 && MyManager1.SelectedValue.ToInt32() != 0)
                    //{
                    //    ObjparList.Add("QuotedEmployee", MyManager1.SelectedValue, NSqlTypes.IN, true);
                    //}
                }
            }

            DateTime now = DateTime.Now;
            DateTime Star = new DateTime(now.Year, now.Month, 1);

            DateTime End = Star.AddMonths(1).AddDays(-1);

            if (Request["Type"] == "Look")
            {
                tblLook.Visible = false;
                int EmployeeID = Request["EmployeeID"].ToInt32();
                dates = Request["DateRanger"].ToString();
                Star = dates.Split(',')[0].ToString().ToDateTime();
                End = dates.Split(',')[1].ToString().ToDateTime();

                if (Request["Category"] == "SourceCount")
                {
                    ObjparList.Add("InviteEmployee", EmployeeID, NSqlTypes.Equal);
                    ObjparList.Add("CreateDate", Star + "," + End, NSqlTypes.DateBetween);
                }
                else if (Request["Category"] == "InSourceCount")
                {
                    ObjparList.Add("OrderEmployee", EmployeeID, NSqlTypes.Equal);
                    ObjparList.Add("OrderCreateDate", Star + "," + End, NSqlTypes.DateBetween);
                }
                else if (Request["Category"] == "ComeOrderCount")
                {

                    ObjparList.Add("OrderEmployee", EmployeeID, NSqlTypes.Equal);
                    ObjparList.Add("CreateDate", Star + "," + End, NSqlTypes.DateBetween);
                }
                else if (Request["Category"] == "NewOrderByMonth")
                {
                    ObjparList.Add("QuotedEmployee", EmployeeID, NSqlTypes.Equal);
                    ObjparList.Add("OrderSucessDate", Star + "," + End, NSqlTypes.DateBetween);
                }
            }
        }
    }
}