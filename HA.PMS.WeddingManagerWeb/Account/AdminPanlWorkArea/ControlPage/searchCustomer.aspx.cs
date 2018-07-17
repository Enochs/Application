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
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage
{
    public partial class searchCustomer : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            ///参数
            List<PMSParameters> ObjParList = new List<PMSParameters>();

            ObjParList.Add(txtCustomer.Text.Trim().ToString() != string.Empty, "Bride", txtCustomer.Text.Trim().ToString(), NSqlTypes.StringEquals);
            ObjParList.Add(txtCustomer.Text.Trim().ToString() != string.Empty, "Groom", txtCustomer.Text.Trim().ToString(), NSqlTypes.OR);

            ObjParList.Add(txtCelPhone.Text.Trim().ToString() != string.Empty, "BrideCellPhone", txtCelPhone.Text.Trim().ToString(), NSqlTypes.StringEquals);
            ObjParList.Add(txtCelPhone.Text.Trim().ToString() != string.Empty, "GroomCellPhone", txtCelPhone.Text.Trim().ToString(), NSqlTypes.OR);


            var ReturnCustomerModel = objCustomersBLL.GetCustomerByPmsParameter(ObjParList);
            if (ReturnCustomerModel.Count == 1)
            {

                JavaScriptTools.OpenWindown("/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=" + ReturnCustomerModel[0].CustomerID, Page);
                return;
                //                Response.Redirect("/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=" + ReturnCustomerModel.CustomerID);
            }
            if (ReturnCustomerModel.Count == 0)
            {

                JavaScriptTools.AlertWindow("未找到新人", Page);
                return;
                //                Response.Redirect("/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=" + ReturnCustomerModel.CustomerID);
            }

            if (ReturnCustomerModel.Count > 1)
            {
                repCustomerList.Visible = true;
                this.repCustomerList.DataBind(ReturnCustomerModel);
                return;
                //                Response.Redirect("/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=" + ReturnCustomerModel.CustomerID);
            }
            // var query = objCustomersBLL.GetByAll();

            //if (!string.IsNullOrEmpty(txtCustomer.Text))
            //{
            //    if (query != null)
            //    {
            //        query = query.Where(C => C.BrideCellPhone == txtCustomer.Text.Trim() || C.Bride == txtCustomer.Text.Trim()).ToList();
            //    }
            //}
            //if (!string.IsNullOrEmpty(txtPartyDate.Text))
            //{
            //    if (query != null)
            //    {
            //        query = query.Where(C => C.PartyDate == txtPartyDate.Text.ToDateTime()).ToList();
            //    }

            //}
            //if (!string.IsNullOrEmpty(txtWineShop.Text))
            //{
            //    if (query != null)
            //    {
            //        query = query.Where(C => C.Wineshop == txtWineShop.Text.Trim()).ToList();
            //    }

            //}

            //if (!string.IsNullOrEmpty(txtCelPhone.Text))
            //{
            //    if (query != null)
            //    {
            //        query = query.Where(C => C.GroomCellPhone == txtCelPhone.Text.Trim() || C.BrideCellPhone == txtCelPhone.Text.Trim()).ToList();
            //    }

            //}

            //var singerResult = query.FirstOrDefault();
            //if (singerResult!=null)
            //{
            //    string url = DecodeBase64(Request.QueryString["url"]) + singerResult.CustomerID;
            //    JavaScriptTools.AlertAndCloseJumpfancybox(url, this.Page);
            //}
            //else
            //{
            //    JavaScriptTools.AlertWindow("没有找到该新人资料！", this.Page);
            //}

        }
    }
}