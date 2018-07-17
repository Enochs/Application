
//关于花艺的基本信息修改
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager
{
    public partial class QuotedPriceFlowerItemEdit : SystemPage
    {
        QuotedPriceItems ObjQuotedPriceItemsBLL = new QuotedPriceItems();
        Employee ObjEmployeeBLL = new Employee();

        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
        int QuotedID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            QuotedID = Request["QuotedID"].ToInt32();
            if (!IsPostBack)
            {
                BinderData();
                Label1.Visible = true;
                Text1.Visible = true;

                #region 注释

                //var ObjModel = ObjQuotedPriceBLL.GetByID(QuotedID);


                //if (ObjModel.MoneyCheckEmployee.HasValue)
                //{
                //    txtEmpLoyee.Value = ObjEmployeeBLL.GetByID(ObjModel.MoneyCheckEmployee.Value).EmployeeName;
                //}

                //if (ObjModel.BuyCheckEmployee.HasValue)
                //{
                //    Text1.Value = ObjEmployeeBLL.GetByID(ObjModel.BuyCheckEmployee.Value).EmployeeName;
                //}

                //if (ObjModel.BuyCheck.HasValue)
                //{
                //    if (ObjModel.BuyCheck.Value)
                //    {
                //        Label1.Visible = false;
                //    }
                //}


                //if (ObjModel.FlowerCheck.HasValue)
                //{
                //    if (ObjModel.FlowerCheck.Value && Request["Flower"] != null)
                //    {
                //        lblhejia.Visible = false;
                //        btnSaveRow.Visible = false;
                //        btnCheck.Visible = false;
                //        if (ObjModel.BuyCheck != null)
                //        {
                //            Label1.Visible = false;
                //        }
                //    }
                //}

                //if (ObjModel.MoneyCheck.HasValue)
                //{
                //    if (ObjModel.MoneyCheck.Value)
                //    {
                //        lblhejia.Visible = false;
                //        Label1.Visible = false;

                //        btnSaveRow.Visible = false;
                //        btnCheck.Visible = false;
                //    }
                //}


                //if (Request["Money"] != null)
                //{
                //    lblChengben.Text = "实际总成本";
                //    hideisChange.Value = "1";
                //    Label1.Visible = true;

                //}


                //if (Request["SaleMoney"] != null)
                //{


                //    lblChengben.Text = "实际总成本";
                //    hideisChange.Value = "1";
                //    Label1.Visible = false;

                //    if (ObjModel.BuyCheck.HasValue)
                //    {
                //        if (ObjModel.BuyCheck.Value)
                //        {


                //            btnSaveRow.Visible = false;
                //            btnCheck.Visible = false;
                //        }
                //    }


                //}
                #endregion
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            this.Repeater1.DataSource = ObjQuotedPriceItemsBLL.GetFlowerItem(QuotedID);
            this.Repeater1.DataBind();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {

        }

        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {


            ///华裔负责人填写花艺成本
            TextBox ObjtxtPrice = e.Item.FindControl("txtPurchasePrice") as TextBox;

            var ObjUpdateModel = ObjQuotedPriceItemsBLL.GetByID(e.CommandArgument.ToString().ToInt32());
            ObjUpdateModel.PurchasePrice = ObjtxtPrice.Text.ToDecimal();

            ObjQuotedPriceItemsBLL.Update(ObjUpdateModel);
            JavaScriptTools.AlertWindow("成本保存成功！", Page);
            BinderData();
        }

        protected void btnSaveRow_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {

                ///华裔负责人填写花艺成本
                TextBox ObjtxtPrice = Repeater1.Items[i].FindControl("txtPurchasePrice") as TextBox;

                var ObjUpdateModel = ObjQuotedPriceItemsBLL.GetByID((Repeater1.Items[i].FindControl("btnSaveRow") as Button).CommandArgument.ToString().ToInt32());
                ObjUpdateModel.PurchasePrice = ObjtxtPrice.Text.ToDecimal();
                if (Request["Money"] != null || Request["SaleMoney"] != null)
                {
                    ObjUpdateModel.UnitPrice = (Repeater1.Items[i].FindControl("txtSaleItem") as TextBox).Text.ToDecimal();
                }

                ObjQuotedPriceItemsBLL.Update(ObjUpdateModel);
                JavaScriptTools.AlertWindow("成本保存成功！", Page);

            }

            BinderData();
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {

            if (hideEmpLoyeeID.Value.ToInt32() > 0)
            {
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {

                    ///华裔负责人填写花艺成本
                    TextBox ObjtxtPrice = Repeater1.Items[i].FindControl("txtPurchasePrice") as TextBox;

                    var ObjUpdateModel = ObjQuotedPriceItemsBLL.GetByID((Repeater1.Items[i].FindControl("btnSaveRow") as Button).CommandArgument.ToString().ToInt32());
                    ObjUpdateModel.PurchasePrice = ObjtxtPrice.Text.ToDecimal();
                    if (Request["Money"] != null || Request["SaleMoney"] != null)
                    {
                        ObjUpdateModel.UnitPrice = (Repeater1.Items[i].FindControl("txtSaleItem") as TextBox).Text.ToDecimal();

                    }
                    ObjQuotedPriceItemsBLL.Update(ObjUpdateModel);


                }
                #region 注释


                //    var ObjUpdatemodel = ObjQuotedPriceBLL.GetByID(QuotedID);
                //    if (Request["Flower"] != null)
                //    {

                //        ObjUpdatemodel.FlowerCheck = true;
                //        ObjUpdatemodel.MoneyCheck = false;
                //        ObjUpdatemodel.BuyCheck = false;
                //        ObjUpdatemodel.MoneyCheckEmployee = hideEmpLoyeeID.Value.ToInt32();
                //        ObjUpdatemodel.BuyCheckEmployee = HiddenField1.Value.ToInt32();

                //    }


                //    if (Request["Money"] != null)
                //    {
                //        ObjUpdatemodel.SaleEmployee = hideEmpLoyeeID.Value.ToInt32();
                //        ObjUpdatemodel.MoneyCheck = true;
                //        ObjUpdatemodel.BuyCheckEmployee = HiddenField1.Value.ToInt32();
                //    }


                //    if (Request["SaleMoney"] != null)
                //    {

                //        ObjUpdatemodel.BuyCheck = true;
                //    }

                //    ObjQuotedPriceBLL.Update(ObjUpdatemodel);
                //    JavaScriptTools.AlertWindow("成本保存成功，审核通过！", Page);
                #endregion
            }
            else
            {
                JavaScriptTools.AlertWindow("请选择负责人！", Page);
            }
        }

    }
}