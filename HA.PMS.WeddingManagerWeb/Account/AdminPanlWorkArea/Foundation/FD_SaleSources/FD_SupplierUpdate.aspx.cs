/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.17
 Description:供应商修改页面
 History:修改日志

 Author:杨洋
 date:2013.3.17
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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SupplierUpdate : SystemPage
    {
        Supplier ObjSupplierBLL = new Supplier();
        SupplierType objSupplierTypeBLL = new SupplierType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
                int SupplierID = Request.QueryString["SupplierID"].ToInt32();
                GetSupplierInit(SupplierID);
                int employeeId = User.Identity.Name.ToInt32();
                int operEmployeeId = ViewState["OperEmployeeId"].ToString().ToInt32();
                if (employeeId != operEmployeeId)
                {
                    EnabledTextBox();
                }
            }
        }

        protected void EnabledTextBox()
        {
            foreach (var item in this.phContent.Controls)
            {
                if (item is TextBox)
                {
                    TextBox itemTxt = item as TextBox;
                    itemTxt.Enabled = false;
                }
                if (item is Button)
                {
                    Button itemBtn = item as Button;
                    itemBtn.Enabled = false;
                    itemBtn.Visible = false;
                }
                if (item is DropDownList)
                {
                    DropDownList itemDdl = item as DropDownList;
                    itemDdl.Enabled = false;
                }
            }

        }

        protected void DataBinder()
        {

            ddlSupplierType.DataSource = objSupplierTypeBLL.GetByAll();
            ddlSupplierType.DataTextField = "TypeName";
            ddlSupplierType.DataValueField = "SupplierTypeId";
            ddlSupplierType.DataBind();
        }
        /// <summary>
        /// 初始化要修改的信息
        /// </summary>
        /// <param name="SupplierID"></param>
        protected void GetSupplierInit(int SupplierID)
        {
            FD_Supplier fd_Supplier = ObjSupplierBLL.GetByID(SupplierID);

            if (fd_Supplier != null)
            {
                //存入该用户操作人信息
                ViewState["OperEmployeeId"] = fd_Supplier.CreateEmployeeId;
            }
            else
            {
                ViewState["OperEmployeeId"] = 0;
            }

            txtStarDate.Text = fd_Supplier.StarDate.ToString();
            txtAccount.Text = fd_Supplier.AccountInformation;
            txtTelPhone.Text = fd_Supplier.TelPhone;
            txtName.Text = fd_Supplier.Name;
            txtLinkman.Text = fd_Supplier.Linkman;
            txtEmail.Text = fd_Supplier.Email;
            txtCellPhone.Text = fd_Supplier.CellPhone;
            txtAddress.Text = fd_Supplier.Address;
            txtQQ.Text = fd_Supplier.QQ;
            string categoryId = fd_Supplier.CategoryID.ToString();

            ListItem current = ddlSupplierType.Items.FindByValue(fd_Supplier.CategoryID + string.Empty);
            if (current != null)
            {
                current.Selected = true;
            }
        }
        /// <summary>
        /// 修改保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int SupplierID = Request.QueryString["SupplierID"].ToInt32();
            FD_Supplier fd_Supplier = ObjSupplierBLL.GetByID(SupplierID);

            fd_Supplier.StarDate = txtStarDate.Text.ToDateTime();
            fd_Supplier.AccountInformation = txtAccount.Text;
            fd_Supplier.TelPhone = txtTelPhone.Text;
            fd_Supplier.Name = txtName.Text;
            fd_Supplier.Linkman = txtLinkman.Text;
            fd_Supplier.Email = txtEmail.Text;
            fd_Supplier.CellPhone = txtCellPhone.Text;
            fd_Supplier.Address = txtAddress.Text;
            fd_Supplier.QQ = txtQQ.Text;
            fd_Supplier.CategoryID = ddlSupplierType.SelectedValue.ToInt32();
            fd_Supplier.IsDelete = false;

            int result = ObjSupplierBLL.Update(fd_Supplier);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("修改失败;请重新尝试", this.Page);

            }
        }
    }
}