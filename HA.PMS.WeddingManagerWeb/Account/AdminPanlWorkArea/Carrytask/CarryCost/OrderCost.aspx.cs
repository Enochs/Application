using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.CS;
using System.Web.UI.HtmlControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost
{
    public partial class OrderCost : SystemPage
    {
        CostforOrder ObjOrderCostBLL = new CostforOrder();

        ProductforDispatching ObjProductForDisp = new ProductforDispatching();

        DispathingSatisfaction ObjSatisfictionBLL = new DispathingSatisfaction();       //满意度

        Designclass ObjDesignClassBLL = new Designclass();

        Customers ObjCustomerBLL = new Customers();

        CostSum ObjCostSumBLL = new CostSum();

        string Type = "";

        #region 页面加载
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BinderData();
                if (Type == "Details")
                {
                    //td_Output.Visible = false;
                    //td_Output1.Visible = false;
                    //td_Output2.Visible = false;
                    //td_Output3.Visible = false;
                    //td_Output4.Visible = false;
                    //td_Output5.Visible = false;
                    t_head.Visible = false;
                    td_Handle.Visible = false;
                    //td_body.Visible = false;
                    btnSave.Visible = false;
                }
            }
        }
        #endregion

        #region 数据绑定
        private void BinderData()
        {
            int CustomerID = Request["CustomerID"].ToInt32();
            FL_Customers CustomerModel = ObjCustomerBLL.GetByID(CustomerID);
            Type = Request["Type"].ToString();
            if (CustomerModel.State == 206)
            {
                int DispathingID = Request["DispatchingID"].ToInt32();
                int OrderID = Request["OrderID"].ToInt32();
                int QuotedID = Request["QuotedID"].ToInt32();
                btn_OrderFinish.Visible = false;

                if (Type != "Details")
                {
                    Response.Redirect("OrderCost.aspx?DispatchingID=" + DispathingID + "&CustomerID=" + CustomerID + "&OrderID=" + OrderID + "&QuotedID=" + QuotedID + "&Type=Details&NeedPopu=1");
                }
            }

            int DisID = Request["DispatchingID"].ToInt32();

            //供应商成本 来自供应商明细产品表

            List<FL_CostSum> DataList = ObjCostSumBLL.GetByDispatchingID(DisID);

            repEmployeeCost.DataBind(DataList.Where(C => C.RowType == 3 || C.RowType == 4 || C.RowType == 5));  //执行团队 4

            repSupplierCost.DataBind(DataList.Where(C => C.RowType == 1));        //供应商 1

            rptStore.DataBind(DataList.Where(C => C.RowType == 2));        //库房 2

            repBuyCost.DataBind(DataList.Where(C => C.RowType == 7));  //采购物料

            repFlowerCost.DataBind(DataList.Where(C => C.RowType == 8));  //花艺单

            repOtherCost.DataBind(DataList.Where(C => C.RowType == 9));  //其他

            lblSumMoney.Text = DataList.Sum(C => C.Sumtotal).ToString();

            List<FL_DispathingSatisfaction> ObjSacList = ObjSatisfictionBLL.GetByDispatchingID(DisID);
            rptSatisfaction.DataBind(ObjSacList);

            List<FL_Designclass> List = ObjDesignClassBLL.GetByCustomerId(Request["CustomerID"].ToInt32());
            rptDesignClass.DataBind(List);

        }
        #endregion

        #region 添加成本
        /// <summary>
        /// 添加成本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveEdit_Click(object sender, EventArgs e)
        {

            FL_CostSum CostSum = new FL_CostSum();

            CostSum.Name = txtName.Text;
            CostSum.Content = txtNode.Text.ToString();
            CostSum.CategoryName = txtNode.Text.ToString();
            CostSum.Sumtotal = txtPlanCost.Text.ToDecimal();
            CostSum.ActualSumTotal = txtPlanCost.Text.ToDecimal();
            CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
            CostSum.CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime();
            CostSum.ShortCome = "";
            CostSum.Advance = "";
            CostSum.OrderID = Request["OrderID"].ToInt32();
            CostSum.QuotedID = GetQuotedIDByCustomerID(Request["CustomerID"].ToInt32());
            CostSum.CustomerId = Request["CustomerID"].ToInt32();
            CostSum.DispatchingID = Request["DispatchingID"].ToInt32();
            CostSum.RowType = 9;
            CostSum.Evaluation = 6;
            CostSum.EmployeeID = User.Identity.Name.ToInt32();
            ObjCostSumBLL.Insert(CostSum);
            JavaScriptTools.AlertWindow("添加成功!", Page);
            BinderData();
        }
        #endregion

        #region 删除功能

        protected void repOtherCost_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
           

            if (e.CommandName == "Delete")
            {
                HtmlButton btn = e.Item.FindControl("btnEdit") as HtmlButton;
                FL_CostSum ObjCostSumModel = ObjCostSumBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                ObjCostSumBLL.Delete(ObjCostSumModel);
            }
            
            BinderData();
            JavaScriptTools.AlertWindow("删除成功!", Page);
        }
        #endregion

        #region 不是派工  隐藏删除按钮
        protected void repEmployeeCost_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (Type == "Details")
            {
                Button btnSaveEdit = e.Item.FindControl("btnSaveEdit") as Button;
                btnSaveEdit.Visible = false;
            }
        }
        #endregion

        #region 不是派工人 就不能看见价格
        protected string StatuHideViewInviteInfo()
        {
            //return GetTypes() == true ? "style='display:none'" : string.Empty;
            return string.Empty;
        }
        #endregion

        #region 获取类型
        public bool GetTypes()
        {
            if (Type == "Details")
            {
                return true;
            }
            else if (Type == "Look")
            {
                return false;
            }
            return false;
        }
        #endregion

        #region 项目完结
        /// <summary>
        /// 完结
        /// </summary>
        protected void btn_OrderFinish_Click(object sender, EventArgs e)
        {
            ConfirmFinish();
        }
        #endregion

        #region 项目完结确认 方法
        /// <summary>
        /// 确认
        /// </summary>

        public void ConfirmFinish()
        {
            Customers ObjCustomerBLL = new Customers();
            var CustomerModel = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            CustomerModel.State = 206;
            CustomerModel.FinishOver = true;
            ObjCustomerBLL.Update(CustomerModel);
            //项目完结
            CreateHandle();

            JavaScriptTools.AlertWindow("操作成功,项目成功完成", Page);
            BinderData();
        }

        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            sys_HandleLog HandleModel = new sys_HandleLog();
            var Model = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            HandleModel.HandleContent = "派工管理-执行中订单,客户姓名:" + Model.Bride + "/" + Model.Groom + ",项目确认完结";

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 4;     //派工管理
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}