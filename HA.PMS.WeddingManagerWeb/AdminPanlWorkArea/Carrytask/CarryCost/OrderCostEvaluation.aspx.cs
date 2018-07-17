using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost
{
    public partial class OrderCostEvaluation : System.Web.UI.Page
    {
        CostforOrder ObjOrderCostBLL = new CostforOrder();

        ProductforDispatching ObjProductForDisp = new ProductforDispatching();

        DispathingSatisfaction ObjSatisfictionBLL = new DispathingSatisfaction();       //满意度

        Designclass ObjDesignBLL = new Designclass();

        Designclass ObjDesignClassBLL = new Designclass();

        Customers ObjCustomerBLL = new Customers();

        Dispatching ObjDispatchingBLL = new Dispatching();

        CostSum ObjCostSumBLL = new CostSum();
        int DispatchingID = 0;
        int CustomerID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            if (!IsPostBack)
            {
                DispatchingID = Request["DispatchingID"].ToInt32();
                CustomerID = Request["CustomerID"].ToInt32();
                var ObjCustomerModel = ObjCustomerBLL.GetByID(CustomerID);
                if (ObjCustomerModel.EvalState == 1)
                {
                    Response.Redirect("OrderCost.aspx?DispatchingID=" + DispatchingID + "&CustomerID=" + CustomerID + "&Type=Details&NeedPopu=1");
                }
                Insert();
                BinderData();
            }
        }

        #region 页面加载
        private void BinderData()
        {
            DispatchingID = Request["DispatchingID"].ToInt32();


            List<FL_CostSum> DataList = ObjCostSumBLL.GetByDispatchingID(DispatchingID);

            foreach (var item in DataList)
            {
                if (item.Evaluation == null || item.Evaluation == 0)
                {
                    item.Evaluation = 6;        //未评价       --请选择--
                    ObjCostSumBLL.Update(item);
                }
            }

            repEmployeeCost.DataBind(DataList.Where(C => C.RowType == 4 || C.RowType == 5));  //执行团队 4

            repSupplierCost.DataBind(DataList.Where(C => C.RowType == 1));        //供应商 1

            rptStore.DataBind(DataList.Where(C => C.RowType == 2));        //库房 2

            repBuyCost.DataBind(DataList.Where(C => C.RowType == 7));  //采购物料

            repFlowerCost.DataBind(DataList.Where(C => C.RowType == 8));  //花艺单

            rptOtherCost.DataBind(DataList.Where(C => C.RowType == 9));  //其他

            List<FL_DispathingSatisfaction> ObjSacList = ObjSatisfictionBLL.GetByDispatchingID(DispatchingID);
            if (ObjSacList.Count == 0)
            {
                ObjSatisfictionBLL.Insert(new FL_DispathingSatisfaction
                {
                    DispatchingID = DispatchingID,
                    CreateEmployee = User.Identity.Name.ToInt32(),
                    EvaluationId = 6,
                    EvaluationName = "",
                    EvaluationContent = "",

                    SatisfactionName = "",
                    SaEvaluationId = 6,
                    SatisfactionContent = "",
                    SatisfactionRemark = "",
                });
            }
            rptSatisfaction.DataBind(ObjSacList);

            List<FL_Designclass> List = ObjDesignBLL.GetByCustomerId(Request["CustomerID"].ToInt32());
            foreach (var item in List)
            {
                if (item.Evaluation == null || item.Evaluation == 0)
                {
                    item.Evaluation = 6;        //未评价       --请选择--
                    ObjDesignBLL.Update(item);
                }
            }
            rptDesignClass.DataBind(List);
        }
        #endregion

        #region 总评价     如果不存在就新增
        public void Insert()
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            FL_DispathingSatisfaction SacModel = new FL_DispathingSatisfaction();
            SacModel.DispatchingID = DispatchingID;
            SacModel.SaEvaluationId = 6;
            SacModel.SatisfactionName = "总体满意度";
            SacModel.SatisfactionContent = "";
            SacModel.SatisfactionRemark = "";

            SacModel.EvaluationId = 6;
            SacModel.EvaluationName = "总体评价";
            SacModel.EvaluationContent = "";
            SacModel.EvaluationRemark = "";

            ObjSatisfictionBLL.Insert(SacModel);
        }
        #endregion

        #region 点击保存
        /// <summary>
        /// 保存事件
        /// </summary>
        protected void btnSavePri_Click(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            Button btn = sender as Button;
            if (btn.ID == "btnSavePri")
            {
                UpdateCost(repEmployeeCost);
            }
            else if (btn.ID == "btnSaveGong")
            {
                UpdateCost(repSupplierCost);
            }
            else if (btn.ID == "btnSaveKu")
            {
                UpdateCost(rptStore);
            }
            else if (btn.ID == "btnSaveWu")
            {
                UpdateCost(repBuyCost);
            }
            else if (btn.ID == "btnSaveFlower")
            {
                UpdateCost(repFlowerCost);
            }
            else if (btn.ID == "btnSaveOrher")
            {
                UpdateCost(rptOtherCost);
            }
            else if (btn.ID == "btnSaveEvaulation")
            {
                UpdateSactisfaction(rptSatisfaction, sender);
            }
            else if (btn.ID == "btn_SavaClass")
            {
                UpdateDesign(rptDesignClass);
            }

            BinderData();
            JavaScriptTools.AlertWindowAndLocation("修改成功!", "OrderCostEvaluation.aspx?DispatchingID=" + DispatchingID + "&CustomerID=" + CustomerID, Page);

        }
        #endregion

        #region 修改
        public void UpdateCost(Repeater rptItem)
        {
            for (int i = 0; i < rptItem.Items.Count; i++)
            {
                var currentItem = rptItem.Items[i];
                DropDownList ddlEvalution = currentItem.FindControl("ddlEvaluation") as DropDownList;

                TextBox txtAdvance = currentItem.FindControl("txtAdvance") as TextBox;
                TextBox txtShortCome = currentItem.FindControl("txtShortCome") as TextBox;
                HiddenField CostSumId = currentItem.FindControl("HiddenValue") as HiddenField;
                int value = CostSumId.Value.ToInt32();
                FL_CostSum CostSumModel = ObjCostSumBLL.GetByID(value);
                CostSumModel.Evaluation = ddlEvalution.SelectedValue.ToInt32();
                CostSumModel.Advance = txtAdvance.Text.ToString();
                CostSumModel.ShortCome = txtShortCome.Text.ToString();
                CostSumModel.State = 1;         //说明已经评价
                ObjCostSumBLL.Update(CostSumModel);
            }

        }
        #endregion

        #region 修改设计类清单
        public void UpdateDesign(Repeater rptItem)
        {
            for (int i = 0; i < rptItem.Items.Count; i++)
            {
                var currentItem = rptItem.Items[i];
                DropDownList ddlEvalution = currentItem.FindControl("ddlEvaluation") as DropDownList;

                TextBox txtAdvance = currentItem.FindControl("txtAdvance") as TextBox;
                TextBox txtShortCome = currentItem.FindControl("txtShortCome") as TextBox;
                HiddenField DesignClassId = currentItem.FindControl("HiddenValue") as HiddenField;
                int value = DesignClassId.Value.ToInt32();
                FL_Designclass DesignModel = ObjDesignClassBLL.GetByID(value);
                DesignModel.Evaluation = ddlEvalution.SelectedValue.ToInt32();
                DesignModel.Advance = txtAdvance.Text.ToString();
                DesignModel.ShortCome = txtShortCome.Text.ToString();
                DesignModel.State = 1;         //说明已经评价
                ObjDesignBLL.Update(DesignModel);
            }

        }
        #endregion

        #region 修改总体评价 总体满意度
        public void UpdateSactisfaction(Repeater rptItem, object sender)
        {
            for (int i = 0; i < rptItem.Items.Count; i++)
            {
                var currentItem = rptItem.Items[i];
                DropDownList ddlEvalution = currentItem.FindControl("ddlEvaluation") as DropDownList;
                DropDownList ddlSaticfaction = currentItem.FindControl("ddlSaticfaction") as DropDownList;
                TextBox txtSumEvauluation = currentItem.FindControl("txtSumEvauluation") as TextBox;
                TextBox txtSumEvauluationRemark = currentItem.FindControl("txtSumEvauluationRemark") as TextBox;
                TextBox txtSumSaticfaction = currentItem.FindControl("txtSumSaticfaction") as TextBox;
                TextBox txtSumSaticfactionRemark = currentItem.FindControl("txtSumSaticfactionRemark") as TextBox;
                HiddenField SacId = currentItem.FindControl("HiddenValue") as HiddenField;
                int value = SacId.Value.ToInt32();

                FL_DispathingSatisfaction SacModel = ObjSatisfictionBLL.GetByID(value);

                SacModel.EvaluationId = ddlEvalution.SelectedValue.ToInt32();
                SacModel.EvaluationName = "总体评价";
                SacModel.EvaluationContent = txtSumEvauluation.Text.ToString();
                SacModel.EvaluationRemark = txtSumEvauluationRemark.Text.ToString();

                SacModel.SaEvaluationId = ddlSaticfaction.SelectedValue.ToInt32();
                SacModel.SatisfactionName = "总体满意度";
                SacModel.SatisfactionContent = txtSumSaticfaction.Text.ToString();
                SacModel.SatisfactionRemark = txtSumSaticfactionRemark.Text.ToString();

                SacModel.CreateEmployee = User.Identity.Name.ToInt32();

                ObjSatisfictionBLL.Update(SacModel);

                Button btn = sender as Button;
                if (btn.ID == "btnConfirm")
                {
                    var ObjCustomerModel = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());  //更新顾客总体评价
                    ObjCustomerModel.EvaluationId = ddlEvalution.SelectedValue.ToInt32();        //总体评价
                    ObjCustomerModel.UpdateEvaluTime = DateTime.Now.ToString().ToDateTime();    //评价时间
                    ObjCustomerModel.EvalState = 1;
                    ObjCustomerBLL.Update(ObjCustomerModel);
                }
            }
        }
        #endregion

        #region 点击确定事件
        /// <summary>
        /// 确定
        /// </summary> 
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            UpdateCost(repEmployeeCost);
            UpdateCost(repSupplierCost);
            UpdateCost(rptStore);
            UpdateCost(repBuyCost);
            UpdateCost(repFlowerCost);
            UpdateSactisfaction(rptSatisfaction, sender);
            //操作日志
            CreateHandle();
            JavaScriptTools.AlertWindowAndLocation("确认提交成功!", "OrderCost.aspx?DispatchingID=" + DispatchingID + "&CustomerID=" + CustomerID + "&Type=Details&NeedPopu=1", Page);
        }
        #endregion

        #region 注释

        #region 绑定 触发事件  保存
        /// <summary>
        /// 触发事件  保存
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        protected void repEmployeeCost_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            DropDownList ddlEvalution = e.Item.FindControl("ddlEvaluation") as DropDownList;
            if (ddlEvalution.SelectedValue.ToInt32() > 0)
            {
                if (e.CommandName == "Save1")
                {
                    TextBox txtAdvance = e.Item.FindControl("txtAdvance") as TextBox;
                    TextBox txtShortCome = e.Item.FindControl("txtShortCome") as TextBox;
                    int CostSumId = e.CommandArgument.ToString().ToInt32();
                    FL_CostSum CostSum = ObjCostSumBLL.GetByID(CostSumId);
                    CostSum.Evaluation = ddlEvalution.SelectedValue.ToInt32();      //评价
                    CostSum.Advance = txtAdvance.Text.ToString();
                    CostSum.ShortCome = txtShortCome.Text.ToString();
                    ObjCostSumBLL.Update(CostSum);
                }
                if (e.CommandName == "Save2")
                {
                    DropDownList ddlSaticfaction = e.Item.FindControl("ddlSaticfaction") as DropDownList;
                    TextBox txtSumEvauluation = e.Item.FindControl("txtSumEvauluation") as TextBox;
                    TextBox txtSumEvauluationRemark = e.Item.FindControl("txtSumEvauluationRemark") as TextBox;
                    TextBox txtSumSaticfaction = e.Item.FindControl("txtSumSaticfaction") as TextBox;
                    TextBox txtSumSaticfactionRemark = e.Item.FindControl("txtSumSaticfactionRemark") as TextBox;
                    int SacId = e.CommandArgument.ToString().ToInt32();
                    FL_DispathingSatisfaction SacModel = ObjSatisfictionBLL.GetByID(SacId);

                    SacModel.EvaluationId = ddlEvalution.SelectedValue.ToInt32();
                    SacModel.EvaluationName = "总体评价";
                    SacModel.EvaluationContent = txtSumEvauluation.Text.ToString();
                    SacModel.EvaluationRemark = txtSumEvauluationRemark.Text.ToString();

                    SacModel.SaEvaluationId = ddlSaticfaction.SelectedValue.ToInt32();
                    SacModel.SatisfactionName = "总体满意度";
                    SacModel.SatisfactionContent = txtSumSaticfaction.Text.ToString();
                    SacModel.SatisfactionRemark = txtSumSaticfactionRemark.Text.ToString();

                    ObjSatisfictionBLL.Update(SacModel);

                    var ObjDispatchingModel = ObjDispatchingBLL.GetByID(Request["DispatchingID"].ToInt32());
                    ObjDispatchingModel.EvaluationId = ddlEvalution.SelectedValue.ToInt32();        //总体评价
                    ObjDispatchingModel.SacEvluationId = ddlSaticfaction.SelectedValue.ToInt32();   //总体满意
                    ObjDispatchingBLL.Update(ObjDispatchingModel);
                }
            }
            BinderData();
        }
        #endregion

        #region 数据绑定事件
        /// <summary>
        /// 绑定事件
        /// </summary>    
        protected void repEmployeeCost_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //DropDownList ddlEvalution = e.Item.FindControl("ddlEvaluation") as DropDownList;
            //int CostSumID = (e.Item.FindControl("HiddenValue") as HiddenField).Value.ToInt32();
            //var Model = ObjCostSumBLL.GetByID(CostSumID);
            //ddlEvalution.Items.FindByValue(Model.Evaluation.ToString()).Selected = true;

        }
        #endregion

        #endregion

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle()
        {
            HandleLog ObjHandleBLL = new HandleLog();
            var Model = ObjCustomerBLL.GetByCustomerID(Request["CustomerID"].ToInt32());
            sys_HandleLog HandleModel = new sys_HandleLog();
            HandleModel.HandleContent = "策划报价,订单评价,客户姓名:" + Model.Bride + "/" + Model.Groom + ",评价订单！";
            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 3;     //策划报价
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}