using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysReport
{
    public partial class StatementManager : HA.PMS.Pages.SystemPage
    {
        /// <summary>
        /// 结算表
        /// </summary>
        Statement ObjStatementBLL = new Statement();
        /// <summary>
        /// 四大金刚
        /// </summary>
        GuardianType ObjGuardianTypeBLL = new GuardianType();
        /// <summary>
        /// 供应商
        /// </summary>
        SupplierType ObjSupplierTypeBLL = new SupplierType();

        Customers ObjCustomerBLL = new Customers();

        int SourceCount = 0;
        string OrderByColumnName = "CreateDate";

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        #endregion

        #region 数据绑定方法
        /// <summary>
        /// 加载
        /// </summary>
        public void DataBinder()
        {
            List<PMSParameters> pars = new List<PMSParameters>();

            //供应商点击查看跳转过来 查看相应的数据
            if (Request["RowType"] != null)
            {
                if (Request["RowType"].ToInt32() == 1)        //供应商
                {
                    Supplier ObjSupllierBLL = new Supplier();
                    int SupplierID = Request["SupplierID"].ToInt32();
                    txtName.Text = ObjSupllierBLL.GetByID(SupplierID).Name;

                    pars.Add("RowType", 1, NSqlTypes.Equal);
                    pars.Add("SupplierID", SupplierID, NSqlTypes.Equal);
                }
                else if (Request["RowType"].ToInt32() == 4)     //四大金刚
                {
                    //GuradianFile ObjGuardBLL = new GuradianFile();
                    FourGuardian ObjGuardBLL = new FourGuardian();
                    int GuardianID = Request["GuardianID"].ToInt32();
                    txtName.Text = ObjGuardBLL.GetByID(GuardianID).GuardianName;

                    pars.Add("RowType", 4, NSqlTypes.Equal);
                    pars.Add("SupplierID", GuardianID, NSqlTypes.Equal);
                }
                else if (Request["RowType"].ToInt32() == 5)     //内部人员
                {
                    Employee ObjEmployeeBLL = new Employee();
                    int EmployeeID = Request["EmployeeID"].ToInt32();
                    txtName.Text = ObjEmployeeBLL.GetByID(EmployeeID).EmployeeName;

                    pars.Add("RowType", 5, NSqlTypes.Equal);
                    pars.Add("SupplierID", EmployeeID, NSqlTypes.Equal);
                }
            }
            else
            {
                if (ddlCategory.SelectedValue.ToInt32() > 0)        //类别
                {
                    if (ddlTypeCategory.SelectedItem.Text != "请选择")
                    {
                        pars.Add("TypeName", ddlTypeCategory.SelectedItem.Text.ToString(), NSqlTypes.StringEquals);
                    }
                }
            }

            //供应商名称
            pars.Add(txtName.Text.Trim().ToString() != string.Empty, "Name", txtName.Text.Trim().ToString(), NSqlTypes.StringEquals);

            //责任人
            CstmNameSelector.AppandTo(pars);

            //婚期
            pars.Add(DatePartyDate.IsNotBothEmpty, "PartyDate", DatePartyDate.StartoEnd, NSqlTypes.DateBetween);

            //付款是否完成
            if (ddLPayState.SelectedValue.ToInt32() == 1)       //未完成
            {
                pars.Add("NoPayMent", 0, NSqlTypes.Greaterthan); ;
            }
            else if (ddLPayState.SelectedValue.ToInt32() == 2)      //已完成
            {
                pars.Add("NoPayMent", 0, NSqlTypes.Equal); ;
            }

            var DataList = ObjStatementBLL.GetByAllParameter(pars, OrderByColumnName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            rptStatement.DataBind(DataList);
            txtPrePayMent.Text = "";
            int index = 0;
            foreach (var item in DataList)
            {
                if (item.NoPayMent == 0)
                {
                    index += 1;
                }
            }
            if (DataList.Count == index)
            {
                lbtnPayAll.Visible = false;
            }

            #region 本页  本期 合计

            //本页合计
            lblPageSumTotal.Text = DataList.Sum(C => C.SumTotal).ToString();
            lblPagePayMent.Text = DataList.Sum(C => C.PayMent).ToString();
            lblPageNoPayment.Text = DataList.Sum(C => C.NoPayMent).ToString();

            //本期合计
            var DataLists = ObjStatementBLL.GetByAllParameter(pars, OrderByColumnName, 10000, 1, out SourceCount);
            lblAllSumTotal.Text = DataLists.Sum(C => C.SumTotal).ToString();
            lblAllPayMent.Text = DataLists.Sum(C => C.PayMent).ToString();
            lblAllNoPayMent.Text = DataLists.Sum(C => C.NoPayMent).ToString();
            #endregion
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 查找事件
        /// <summary>
        /// 点击查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLook_Click(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            DataBinder();
        }
        #endregion


        #region 选择变化事件
        /// <summary>
        /// 选择重新绑定
        /// </summary>
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLDataBind();
            ddlTypeCategory.SelectedItem.Text = "请选择";
        }
        #endregion

        #region 绑定事件 保存
        /// <summary>
        /// 信息保存
        /// </summary>
        protected void rptStatement_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            lbtnPayAll.Visible = false;
            LinkButton lbtnPaysFor = (e.Item.FindControl("lbtnPaysFor") as LinkButton);
            TextBox txtPayFor = (e.Item.FindControl("txtPayFor") as TextBox);
            LinkButton lbtnCancel = (e.Item.FindControl("lbtnCancel") as LinkButton);
            LinkButton btnRowSave = (e.Item.FindControl("btnRowSave") as LinkButton);
            if (e.CommandName == "RowSave")
            {
                int Id = e.CommandArgument.ToString().ToInt32();
                var ObjStatementModel = ObjStatementBLL.GetByID(Id);

                if (txtPayFor.Text.ToInt32() <= 0 || txtPayFor.Text == "")
                {
                    JavaScriptTools.AlertWindow("请输入正确的付款额", Page);
                    return;
                }
                else if (txtPayFor.Text.ToInt32() > ObjStatementModel.NoPayMent)
                {
                    JavaScriptTools.AlertWindow("您的付款额不能大于未付款额", Page);
                    return;
                }
                ObjStatementModel.PayMent += txtPayFor.Text.ToString().ToDecimal();            //已付款
                ObjStatementModel.NoPayMent = (ObjStatementModel.SumTotal - ObjStatementModel.PayMent).ToString().ToDecimal();      //未付款
                int result = ObjStatementBLL.Update(ObjStatementModel);
                if (result > 0)
                {
                    DataBinder();
                    lbtnPaysFor.Visible = true;
                    txtPayFor.Visible = false;
                    lbtnCancel.Visible = false;
                    btnSavePre.Visible = false;
                    txtPayFor.Text = "0";
                }
            }
            else if (e.CommandName == "lbtnPaysFor")
            {
                lbtnPaysFor.Visible = false;
                txtPayFor.Visible = true;
                lbtnCancel.Visible = true;
                btnRowSave.Visible = true;
                btnSavePre.Visible = true;
                txtPayFor.Text = "0";

            }
            else if (e.CommandName == "lbtnCancel")
            {
                lbtnPaysFor.Visible = true;
                txtPayFor.Visible = false;
                lbtnCancel.Visible = false;
                btnRowSave.Visible = false;
                btnSavePre.Visible = false;
                txtPayFor.Text = "0";
            }
        }
        #endregion

        #region 绑定完成事件
        /// <summary>
        /// 绑定完成 是否还能付款
        /// </summary> 
        protected void rptStatement_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            HiddenField HideStateID = e.Item.FindControl("HideStateID") as HiddenField;
            LinkButton btnRowSave = e.Item.FindControl("btnRowSave") as LinkButton;
            int Id = HideStateID.Value.ToInt32();
            var ObjStatementModel = ObjStatementBLL.GetByID(Id);
            if (ObjStatementModel.SumTotal == ObjStatementModel.PayMent)
            {
                LinkButton lbtnPaysFor = e.Item.FindControl("lbtnPaysFor") as LinkButton;
                lbtnPaysFor.Text = "付款完成";

                btnRowSave.Visible = false;
                lbtnPaysFor.Enabled = false;
            }
        }
        #endregion

        decimal PrePayMent = 0;
        #region 保存预备付款
        /// <summary>
        /// 预备付款
        /// </summary>
        protected void btnSavePre_Click(object sender, EventArgs e)
        {

            LinkButton button = (sender as LinkButton);
            if (button.Text == "保存")
            {
                for (int i = 0; i < rptStatement.Items.Count; i++)
                {
                    var ObjItem = rptStatement.Items[i];
                    PrePayMent += (ObjItem.FindControl("txtPayFor") as TextBox).Text.ToString().ToDecimal();
                    txtPrePayMent.Text = PrePayMent.ToString();
                }
                txtPrePayMent.Visible = true;
                btnSavePre.Visible = true;
                button.Text = "确认付款";
            }
            else if (button.Text == "确认付款")
            {
                for (int i = 0; i < rptStatement.Items.Count; i++)
                {
                    var ObjItem = rptStatement.Items[i];
                    LinkButton lbtnPaysFor = (ObjItem.FindControl("lbtnPaysFor") as LinkButton);
                    TextBox txtPayFor = (ObjItem.FindControl("txtPayFor") as TextBox);
                    LinkButton lbtnCancel = (ObjItem.FindControl("lbtnCancel") as LinkButton);
                    LinkButton btnRowSave = (ObjItem.FindControl("btnRowSave") as LinkButton);

                    decimal PayMent = txtPayFor.Text.ToString().ToDecimal();

                    int Id = (ObjItem.FindControl("HideStateID") as HiddenField).Value.ToInt32();
                    var ObjStatementModel = ObjStatementBLL.GetByID(Id);
                    if (ObjStatementModel != null)
                    {
                        if (txtPayFor.Text.ToInt32() < 0 || txtPayFor.Text == "")
                        {
                            JavaScriptTools.AlertWindow("请输入正确的付款额", Page);
                            return;
                        }
                        else if (txtPayFor.Text.ToInt32() > ObjStatementModel.NoPayMent)
                        {
                            JavaScriptTools.AlertWindow("您的付款额不能大于未付款额", Page);
                            return;
                        }
                        ObjStatementModel.PayMent += PayMent;       //已付款
                        ObjStatementModel.NoPayMent = (ObjStatementModel.SumTotal - ObjStatementModel.PayMent).ToString().ToDecimal();      //未付款
                        int result = ObjStatementBLL.Update(ObjStatementModel);      //更新 更改
                        if (i == 9)
                        {
                            DataBinder();
                            lbtnPaysFor.Visible = true;
                            txtPayFor.Visible = false;
                            lbtnCancel.Visible = false;
                            txtPayFor.Text = "0";
                        }
                    }
                }
                Response.Redirect(Page.Request.Url.ToString());
                //txtPrePayMent.Text = "0";
                //txtPrePayMent.Visible = false;
                //btnSavePre.Visible = false;
                //button.Text = "保存";
            }
        }
        #endregion


        #region 下拉框绑定
        public void DDLDataBind()
        {
            
            if (ddlCategory.SelectedValue.ToInt32() == 1)        //供应商
            {
                var DataList = ObjSupplierTypeBLL.GetByAll();
                ddlTypeCategory.DataSource = DataList;
                ddlTypeCategory.DataTextField = "TypeName";
                ddlTypeCategory.DataValueField = "SupplierTypeId";
                ddlTypeCategory.DataBind();
            }
            else if (ddlCategory.SelectedValue.ToInt32() == 2)     //四大金刚
            {
                var DataList = ObjGuardianTypeBLL.GetByAll();
                ddlTypeCategory.DataSource = DataList;
                ddlTypeCategory.DataTextField = "TypeName";
                ddlTypeCategory.DataValueField = "TypeId";
                ddlTypeCategory.DataBind();
            }
            else if (ddlCategory.SelectedValue.ToInt32() == 0)       //供应商
            {
                ddlTypeCategory.Items.Clear();
                return;
            }
        }
        #endregion

        #region 付款所有
        /// <summary>
        /// 付款
        /// </summary>
        protected void lbtnPayAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptStatement.Items.Count; i++)
            {
                var DataItem = rptStatement.Items[i];
                int StatementID = (DataItem.FindControl("HideStateID") as HiddenField).Value.ToInt32();
                var Model = ObjStatementBLL.GetByID(StatementID);
                Model.PayMent += Model.NoPayMent;
                Model.NoPayMent = Model.SumTotal - Model.PayMent;
                ObjStatementBLL.Update(Model);
            }
            Response.Redirect(Page.Request.Url.ToString());
        }
        #endregion

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Response.Redirect("StatementManager.aspx?NeedPopu=1");
        }
    }
}