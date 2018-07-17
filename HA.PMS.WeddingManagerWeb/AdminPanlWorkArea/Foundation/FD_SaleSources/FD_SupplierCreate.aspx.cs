/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.17
 Description:供应商创建页面
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
    public partial class FD_SupplierCreate : SystemPage
    {
        Supplier ObjSupplierBLL = new Supplier();
        SupplierType objSupplierTypeBLL = new SupplierType();

        #region 页面加载
        /// <summary>
        /// 初始化页面
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (objSupplierTypeBLL.GetByAll().Count == 0)
                {
                    BtnCreate.Enabled = false;
                    JavaScriptTools.AlertWindow("该供应商没有类别,请添加类别之后，在录入供应商信息", this.Page);
                    btnFreshen.Visible = true;
                }


                DataBinder();
                int supplierId = Request.QueryString["supplierId"].ToInt32();
                //如果没有参数就代表当前操作是新增
                if (supplierId != 0)
                {
                    EditMsgInit();
                    //~/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SupplierProductCreate.aspx

                    hlCreateProduct.Attributes.Add("target", "_bank");
                    hlCreateProduct.NavigateUrl = "~/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SupplierProductCreate.aspx?supplierId=" + supplierId;
                    BtnCreate.Enabled = false;
                }
                else
                {
                    btnEdit.Visible = false;

                }
            }
        }
        #endregion


        #region 初始化修改信息方法
        /// <summary>
        /// 初始化修改信息方法
        /// </summary>
        protected void EditMsgInit()
        {
            int supplierId = Request.QueryString["supplierId"].ToInt32();
            FD_Supplier fd_Supplier = ObjSupplierBLL.GetByID(supplierId);
            txtStarDate.Text = fd_Supplier.StarDate + string.Empty;
            txtBankName.Text = fd_Supplier.BankName;
            txtAccount.Text = fd_Supplier.AccountInformation;
            txtTelPhone.Text = fd_Supplier.TelPhone;
            txtName.Text = fd_Supplier.Name;
            txtLinkman.Text = fd_Supplier.Linkman;
            txtEmail.Text = fd_Supplier.Email;
            txtCellPhone.Text = fd_Supplier.CellPhone;
            txtAddress.Text = fd_Supplier.Address;
            txtQQ.Text = fd_Supplier.QQ;
            ListItem current = ddlSupplierType.Items.FindByValue(fd_Supplier.CategoryID + string.Empty);
            if (current != null)
            {
                current.Selected = true;
            }

        }
        #endregion

        #region 下拉框绑定
        /// <summary>
        /// 类别绑定
        /// </summary>
        protected void DataBinder()
        {
            ddlSupplierType.DataSource = objSupplierTypeBLL.GetByAll();
            ddlSupplierType.DataTextField = "TypeName";
            ddlSupplierType.DataValueField = "SupplierTypeId";
            ddlSupplierType.DataBind();
        }
        #endregion

        #region 点击保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            FD_Supplier fd_Supplier;
            int result = 0;
            int supplierId = Request.QueryString["supplierId"].ToInt32();
            //如果不等于，当前进行的操作是修改
            if (supplierId != 0)
            {
                fd_Supplier = ObjSupplierBLL.GetByID(supplierId);

            }
            else
            {
                fd_Supplier = new FD_Supplier();
            }
            fd_Supplier.StarDate = txtStarDate.Text.ToDateTime();
            fd_Supplier.BankName = txtBankName.Text;                    //开户银行
            fd_Supplier.AccountInformation = txtAccount.Text;           //银行帐号
            fd_Supplier.TelPhone = txtTelPhone.Text;                    //座机
            fd_Supplier.Name = txtName.Text;                            //供应商名称
            fd_Supplier.Linkman = txtLinkman.Text;                      //联系人名字
            fd_Supplier.Email = txtEmail.Text;                          //癫子右键
            fd_Supplier.CellPhone = txtCellPhone.Text;                  //联系电话
            fd_Supplier.Address = txtAddress.Text;                      //地址
            fd_Supplier.QQ = txtQQ.Text;                                //QQ
            fd_Supplier.CategoryID = ddlSupplierType.SelectedValue.ToInt32();

            if (supplierId != 0)
            {

                result = ObjSupplierBLL.Update(fd_Supplier);
            }
            else
            {

                fd_Supplier.CreateEmployeeId = User.Identity.Name.ToInt32();
                result = ObjSupplierBLL.Insert(fd_Supplier);


            }


            if (result > 0)
            {
                int ProductID = Request.QueryString["ProductID"].ToInt32();
                //如果此时ProductID不为空，就代表是待分配产品添加过来的操作
                if (ProductID != 0)
                {
                    AllProducts objAllProductsBLL = new AllProducts();
                    Productcs objProductsBLL = new Productcs();
                    FD_AllProducts allProduct = objAllProductsBLL.GetByID(ProductID);
                    FD_Product ObjectT = new FD_Product();
                    ObjectT.Count = allProduct.Count;

                    ObjectT.Data = allProduct.Data;
                    ObjectT.IsDelete = false;

                    allProduct.IsDelete = false;

                    ObjectT.CategoryID = allProduct.ProductCategory.Value;
                    ObjectT.ProductProject = allProduct.ProjectCategory.Value;
                    ObjectT.ProductPrice = allProduct.PurchasePrice;
                    ObjectT.SalePrice = allProduct.SalePrice;
                    ObjectT.Specifications = allProduct.Specifications;
                    ObjectT.ProductName = allProduct.ProductName;

                    ObjectT.Unit = allProduct.Unit;
                    ObjectT.Remark = allProduct.Remark;
                    ObjectT.SupplierID = result;
                    objProductsBLL.Insert(ObjectT);
                    //修改之后 改变状态 
                    allProduct.IsTobeDistributed = true;
                    objAllProductsBLL.Update(allProduct);

                }
                if (ProductID == 0)
                {
                    hlCreateProduct.Visible = true;
                    hlCreateProduct.Attributes.Add("target", "_bank");
                    hlCreateProduct.NavigateUrl = "~/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SupplierProductCreate.aspx?supplierId=" + result;
                }
                JavaScriptTools.AlertWindowAndLocation("操作成功", "/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SupplierProductCreate.aspx?supplierId=" + result, this.Page);
                ///AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SupplierProductCreate.aspx?supplierId=23


            }

        }
        #endregion

        #region 点击编辑事件
        /// <summary>
        /// 编辑
        /// </summary>   
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //点击之后进行修改模式
            BtnCreate.Enabled = true;
        }
        #endregion

        #region 点击刷新事件
        /// <summary>
        /// 刷新供应商类别
        /// </summary>
        protected void btnFreshen_Click(object sender, EventArgs e)
        {

            var objResult = objSupplierTypeBLL.GetByAll();
            if (objResult.Count == 0)
            {
                btnFreshen.Visible = true;
            }
            else
            {
                btnFreshen.Visible = false;
                BtnCreate.Enabled = true;
            }
            ddlSupplierType.DataSource = objResult;
            ddlSupplierType.DataTextField = "TypeName";
            ddlSupplierType.DataValueField = "SupplierTypeId";
            ddlSupplierType.DataBind();
        }
        #endregion

    }
}