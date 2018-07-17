using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.EditoerLibrary;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class CreateSchedule : SystemPage
    {
        //结算表
        Statement ObjStatementBLL = new Statement();

        Supplier ObjSupplierBLL = new Supplier();
        SupplierType ObjSuplierTypeBLL = new SupplierType();
        //总合计
        CostSum ObjCostSumBLL = new CostSum();
        CostforOrder ObjCostForOrderBLL = new CostforOrder();
        //员工
        Employee ObjEmployeeBLL = new Employee();
        //专业团队
        FourGuardian ObjGuardianBLL = new FourGuardian();
        //专业团队类型
        GuardianType ObjGuardianTypeBLL = new GuardianType();
        //派工单
        Dispatching ObjDispatchingBLL = new Dispatching();
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();
        //档期预定
        QuotedPriceSchedule ObjScheduleBLL = new QuotedPriceSchedule();
        int CustomerID = 0;
        int OrderID = 0;
        int QuotedID = 0;


        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomerID = Request["CustomerID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            QuotedID = Request["QuotedID"].ToInt32();
            if (!IsPostBack)
            {
                if (Request["Type"].ToString() == "Edit")
                {
                    div_insert.Visible = true;
                }
                else if (Request["Type"].ToString() == "Look")
                {
                    div_insert.Visible = false;
                }
                DataBinder();
            }
        }
        #endregion

        #region 数据加载
        /// <summary>
        /// 加载
        /// </summary>
        public void DataBinder()
        {
            var DataList = ObjScheduleBLL.GetByCustomerID(CustomerID);
            rptScheduleList.DataBind(DataList);
        }
        #endregion

        #region 选择四大金刚
        /// <summary>
        /// 选择四大金刚 进行绑定
        /// </summary>
        /// <param name="Type"></param>
        protected void btnFourGuardianSave_Click(object sender, EventArgs e)
        {
            int GuardianID = hideGuardianID.Value.ToInt32();
            FD_FourGuardian ObjGuardianModel = ObjGuardianBLL.GetByID(GuardianID);
            txtGuardianName.Text = ObjGuardianModel.GuardianName;       //四大金刚名称
            txtPrice.Text = ObjGuardianModel.CooperationPrice.ToString();       //价格
            txtGuardianType.Text = ObjGuardianTypeBLL.GetByID(ObjGuardianModel.GuardianTypeId.ToString().ToInt32()).TypeName;       //四大金刚类型
        }
        #endregion

        #region 点击添加
        /// <summary>
        /// 添加事件 添加功能
        /// </summary>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            FL_QuotedPriceSchedule ObjScheduleModel = new FL_QuotedPriceSchedule();
            ObjScheduleModel.ScheCustomerID = CustomerID;
            ObjScheduleModel.ScheOrderID = OrderID;
            ObjScheduleModel.ScheQuotedID = QuotedID;
            ObjScheduleModel.ScheGuardianID = hideGuardianID.Value.ToInt32();
            ObjScheduleModel.ScheGuardianType = ObjGuardianBLL.GetByID(hideGuardianID.Value.ToInt32()).GuardianTypeId;
            ObjScheduleModel.ScheGuardianPrice = txtPrice.Text.ToString().ToDecimal();
            ObjScheduleModel.SchePayMent = txtPayMents.Text.ToString().ToDecimal();

            ObjScheduleModel.ScheState = 0;     //第一次添加
            var DataList = ObjScheduleBLL.GetByCustomerID(CustomerID);      //第n次添加
            if (DataList.Count > 0)
            {
                ObjScheduleModel.ScheState = 1;
            }

            ObjScheduleModel.ScheCreateEmployee = User.Identity.Name.ToInt32();
            ObjScheduleModel.ScheCreateDate = DateTime.Now.ToString().ToDateTime();
            ObjScheduleModel.ScheReamrk = txtReamrk.Text.ToString();
            int result = ObjScheduleBLL.Insert(ObjScheduleModel);
            if (result > 0)
            {
                JavaScriptTools.AlertWindow("添加成功", Page);
                InsertCostSum(ObjScheduleModel);
            }
            else if (result == -1)
            {
                JavaScriptTools.AlertWindow("该人员已经预定,不能再次预定", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("添加失败,请稍后再试...", Page);
            }

            DataBinder();
        }
        #endregion

        #region 数据绑定完成时间 ItemDataBound
        /// <summary>
        /// 绑定
        /// </summary> 
        protected void rptScheduleList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            int ScheId = (e.Item.FindControl("HideScheID") as HiddenField).Value.ToInt32();
            FL_QuotedPriceSchedule ObjScheduleModel = ObjScheduleBLL.GetByID(ScheId);
            TextBox txtGuardianName = e.Item.FindControl("txtGuardianNames") as TextBox;
            Label lblGuardianType = e.Item.FindControl("lblGuardianType") as Label;

            txtGuardianName.Text = ObjGuardianBLL.GetByID(ObjScheduleModel.ScheGuardianID.ToString().ToInt32()).GuardianName.ToString();
            txtGuardianName.ToolTip = ObjGuardianBLL.GetByID(ObjScheduleModel.ScheGuardianID.ToString().ToInt32()).GuardianName.ToString();
            lblGuardianType.Text = ObjGuardianTypeBLL.GetByID(ObjScheduleModel.ScheGuardianType.ToString().ToInt32()).TypeName.ToString();
        }
        #endregion

        #region 绑定事件 修改 删除
        /// <summary>
        /// 执行删除
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptScheduleList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int DispatchingID = GetDispatchingID();
            int ScheID = e.CommandArgument.ToString().ToInt32();
            if (e.CommandName == "Del")
            {
                var ObjScheModel = ObjScheduleBLL.GetByID(ScheID);
                if (ObjScheModel != null)
                {
                    string name = ObjGuardianBLL.GetByID((ObjScheModel.ScheGuardianID)).GuardianName;
                    var costModel = ObjCostSumBLL.GetByDispatchingIDName(DispatchingID, name + "(预定)");
                    var stateModel = ObjStatementBLL.GetByDispatchingID(DispatchingID, name + "(预定)");
                    if (costModel != null)
                    {
                        int result1 = ObjStatementBLL.Delete(stateModel);
                    }
                    if (stateModel != null)
                    {
                        int result2 = ObjCostSumBLL.Delete(costModel);
                    }
                    int result = ObjScheduleBLL.Delete(ObjScheModel);
                    if (result > 0)
                    {
                        JavaScriptTools.AlertWindow("删除成功", Page);
                    }
                    else
                    {
                        JavaScriptTools.AlertWindow("删除失败,请稍后再试...", Page);
                    }
                }
            }
            else if (e.CommandName == "Save")       //修改 保存
            {
                Label lblGuardianPrice = e.Item.FindControl("lblGuardianPrice") as Label;
                TextBox txtPayMent = e.Item.FindControl("txtPayMent") as TextBox;
                HiddenField HideGuard = e.Item.FindControl("HideGuardID") as HiddenField;
                var Model = ObjScheduleBLL.GetByID(ScheID);

                if (HideGuard.Value != "")
                {
                    Model.ScheGuardianID = HideGuard.Value.ToInt32();
                    Model.ScheGuardianType = ObjGuardianBLL.GetByID(HideGuard.Value.ToInt32()).GuardianTypeId;
                }
                Model.ScheGuardianPrice = lblGuardianPrice.Text.Trim().ToString().ToDecimal();
                Model.SchePayMent = txtPayMent.Text.Trim().ToString().ToDecimal();
                Model.ScheCreateDate = DateTime.Now;
                Model.ScheCreateEmployee = User.Identity.Name.ToInt32();

                int result = ObjScheduleBLL.Update(Model);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("修改成功", Page);
                }

            }
            DataBinder();
        }
        #endregion

        #region 获取派单ID  DispathcingID
        /// <summary>
        /// 获取ID
        /// </summary>
        public int GetDispatchingID()
        {
            int CustomerID = Request["CustomerID"].ToInt32();
            var ObjDispatchingModel = ObjDispatchingBLL.GetByCustomerID(CustomerID);
            if (ObjDispatchingModel != null)
            {
                return ObjDispatchingModel.DispatchingID.ToString().ToInt32();
            }
            return 0;
        }
        #endregion

        #region 算出总成本
        /// <summary>
        /// 计算成本
        /// </summary>
        /// <param name="ObjItem"></param>

        public void InsertCostSum(FL_QuotedPriceSchedule item)
        {
            int DispatchingID = GetDispatchingID();
            string Name = ObjGuardianBLL.GetByID(item.ScheGuardianID).GuardianName.ToString() + "(预定)";
            var CostModel = ObjCostSumBLL.GetByDispatchingIDName(DispatchingID, Name);
            if (CostModel == null)
            {
                FL_CostSum cost = new FL_CostSum();
                cost.RowType = 4;
                cost.Name = Name;
                cost.Sumtotal = item.SchePayMent.ToString().ToDecimal();
                cost.ActualSumTotal = item.SchePayMent.ToString().ToDecimal();
                cost.DispatchingID = DispatchingID.ToString().ToInt32();
                cost.CustomerId = Request["CustomerID"].ToInt32();
                cost.OrderID = Request["OrderID"].ToInt32();
                cost.QuotedID = Request["QuotedID"].ToInt32();
                cost.EmployeeID = User.Identity.Name.ToInt32();
                cost.CategoryName = "预定" + Name;        //类型名称
                cost.Content = "预定" + Name;             //内容
                cost.Evaluation = 6;
                cost.CreateDate = DateTime.Now.ToString().ToDateTime();
                cost.Advance = "";
                cost.ShortCome = "";
                ObjCostSumBLL.Insert(cost);

                InsertStatement();      //结算表
            }

        }
        #endregion

        #region 保存到结算表中
        public void InsertStatement()
        {
            int DispatchingID = GetDispatchingID();
            var DatasList = ObjCostSumBLL.GetByDispatchingID(DispatchingID);
            foreach (var item in DatasList)
            {
                #region 结算表
                if (item.RowType == 4)
                {
                    #region 判断类别
                    //switch (item.RowType)
                    //{
                    //    case 1:         //供应商
                    //        var ObjSupplierModel = ObjSupplierBLL.GetByName(item.Name);
                    //        ObjStatementModel.Name = item.Name;
                    //        ObjStatementModel.TypeID = ObjSupplierModel.CategoryID;
                    //        ObjStatementModel.TypeName = ObjSuplierTypeBLL.GetByID(ObjSupplierModel.CategoryID).TypeName;
                    //        ObjStatementModel.RowType = 1;

                    //        break;
                    //    case 2:         //库房
                    //        ObjStatementModel.Name = "库房";
                    //        ObjStatementModel.TypeID = -1;
                    //        ObjStatementModel.TypeName = "库房";
                    //        ObjStatementModel.RowType = 2;

                    //        break;
                    //    case 4:         //四大金刚
                    //        var ObjFourGuardianModel = ObjGuardianBLL.GetByName(item.Name.Replace("(预定)", ""));
                    //        if (ObjFourGuardianModel == null)
                    //        {
                    //            var ObjEmployeeModel = ObjEmployeeBLL.GetByName(item.Name);
                    //            if (ObjEmployeeModel != null)
                    //            {
                    //                ObjStatementModel.Name = item.Name;
                    //                ObjStatementModel.TypeID = -2;
                    //                ObjStatementModel.TypeName = "内部人员";
                    //                ObjStatementModel.RowType = 5;
                    //            }
                    //        }
                    //        if (ObjFourGuardianModel != null)
                    //        {
                    //            GuardianType ObjGuardTypeBLL = new GuardianType();

                    //            ObjStatementModel.Name = item.Name;
                    //            ObjStatementModel.TypeID = ObjFourGuardianModel.GuardianTypeId.ToString().ToInt32();
                    //            ObjStatementModel.TypeName = ObjGuardTypeBLL.GetByID(ObjFourGuardianModel.GuardianTypeId).TypeName;
                    //            ObjStatementModel.RowType = 4;
                    //        }
                    //        break;
                    //    case 5:         //人员
                    //        var ObjEmployeeModels = ObjEmployeeBLL.GetByName(item.Name);
                    //        if (ObjEmployeeModels != null)
                    //        {
                    //            ObjStatementModel.Name = item.Name;
                    //            ObjStatementModel.TypeID = -2;
                    //            ObjStatementModel.TypeName = "内部人员";
                    //            ObjStatementModel.RowType = 5;
                    //        }

                    //        break;
                    //}
                    #endregion

                    FL_Statement ObjStatementModel = new FL_Statement();
                    GuardianType ObjGuardTypeBLL = new GuardianType();
                    var ObjFourGuardianModel = ObjGuardianBLL.GetByName(item.Name.Replace("(预定)", ""));

                    ObjStatementModel.Name = item.Name;
                    ObjStatementModel.SupplierID = hideGuardianID.Value.ToInt32();
                    ObjStatementModel.TypeID = ObjFourGuardianModel.GuardianTypeId.ToString().ToInt32();
                    ObjStatementModel.TypeName = ObjGuardTypeBLL.GetByID(ObjFourGuardianModel.GuardianTypeId).TypeName;
                    ObjStatementModel.RowType = 4;

                    ObjStatementModel.CustomerID = Request["CustomerID"].ToInt32();
                    ObjStatementModel.CreateEmployee = User.Identity.Name.ToInt32();
                    ObjStatementModel.CreateDate = DateTime.Now.ToShortDateString().ToDateTime();
                    ObjStatementModel.DispatchingID = DispatchingID.ToString().ToInt32();
                    ObjStatementModel.OrderId = Request["OrderID"].ToInt32();
                    ObjStatementModel.QuotedId = Request["QuotedID"].ToInt32();
                    ObjStatementModel.Remark = "";
                    ObjStatementModel.Finishtation = "";
                    ObjStatementModel.SumTotal = txtPrice.Text.Trim().ToString().ToDecimal();
                    ObjStatementModel.PayMent = txtPayMents.Text.Trim().ToDecimal();
                    ObjStatementModel.NoPayMent = ObjStatementModel.SumTotal - ObjStatementModel.PayMent;
                    FL_Statement StatementModel = ObjStatementBLL.GetByDispatchingID(DispatchingID, item.Name);
                    if (StatementModel != null)    //已经存在
                    {
                        StatementModel.Name = ObjStatementModel.Name;               //名称
                        StatementModel.TypeID = ObjStatementModel.TypeID;           //类型ID
                        StatementModel.TypeName = ObjStatementModel.TypeName;       //类型名称
                        StatementModel.RowType = ObjStatementModel.RowType;         //供应商类别
                        StatementModel.SumTotal = ObjStatementModel.SumTotal;       //金额
                        StatementModel.NoPayMent = ObjStatementModel.NoPayMent;     //未付款
                        ObjStatementBLL.Update(StatementModel);                 //修改更新
                    }
                    else
                    {
                        ObjStatementBLL.Insert(ObjStatementModel);
                    }

                }
                #endregion
            }
        }
        #endregion

        #region 点击改派按钮
        /// <summary>
        /// 改派功能
        /// </summary>
        protected void btnChangeFourGuardianSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptScheduleList.Items.Count; i++)
            {
                var ObjItem = rptScheduleList.Items[i];
                TextBox txtGuardianNames = ObjItem.FindControl("txtGuardianNames") as TextBox;
                Label lblGuardianType = ObjItem.FindControl("lblGuardianType") as Label;
                Label lblGuardianPrice = ObjItem.FindControl("lblGuardianPrice") as Label;
                TextBox txtPayMent = ObjItem.FindControl("txtPayMent") as TextBox;
                HiddenField HideGuardID = ObjItem.FindControl("HideGuardID") as HiddenField;

                int GuardianID = HideGuardID.Value.ToInt32();
                if (GuardianID != 0)
                {
                    FD_FourGuardian ObjGuardianModel = ObjGuardianBLL.GetByID(GuardianID);
                    txtGuardianNames.Text = ObjGuardianModel.GuardianName;       //四大金刚名称
                    lblGuardianType.Text = ObjGuardianTypeBLL.GetByID(ObjGuardianModel.GuardianTypeId).TypeName;
                    lblGuardianPrice.Text = ObjGuardianModel.CooperationPrice.ToString();
                    txtPayMent.Text = "0";
                }
            }
        }
        #endregion


    }
}