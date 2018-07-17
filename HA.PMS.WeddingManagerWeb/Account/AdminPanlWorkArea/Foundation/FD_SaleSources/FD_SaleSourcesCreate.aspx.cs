/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.16
 Description:创建渠道页面
 History:修改日志

 Author:杨洋
 date:2013.3.16
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
    public partial class FD_SaleSourcesCreate : NoneStylePage
    {
        SaleSources ObjSaleSourcesBLL = new SaleSources();
        ChannelType ObjChannelTypeBLL = new ChannelType();
        Employee objEmployeeBLL = new Employee();

        protected bool IsSaleSourcePrivateOpening = false;   //指示自己录入的渠道只有自己和主管可以看见功能是否处于开启状态

        protected void Page_Load(object sender, EventArgs e)
        {
            #region 自己录入的渠道只有自己和主管可以看见功能。

            IsSaleSourcePrivateOpening = new SysConfig().IsSaleSourcePrivateOpening(User.Identity.Name.ToInt32(), false);
            //设置控件权限
            if (IsSaleSourcePrivateOpening)
            {
                ddlMaintenanceEmployee.Enabled = false;
                ddlProlongationEmployee.Enabled = false;
            }

            #endregion

            if (!IsPostBack)
            {
                ViewState["Enabled"] = false;
                DataBinder();
            }
        }
        /// <summary>
        /// 绑定数据源，主要是DropDownList
        /// </summary>
        protected void DataBinder()
        {
            ddlChannelType.DataSource = ObjChannelTypeBLL.GetByAll();
            ddlChannelType.DataValueField = "ChannelTypeId";
            ddlChannelType.DataTextField = "ChannelTypeName";
            ddlChannelType.DataBind();

            var employeeList = objEmployeeBLL.GetByAll();
            //维护人绑定
            ddlMaintenanceEmployee.DataSource = employeeList;
            ddlMaintenanceEmployee.DataValueField = "EmployeeID";
            ddlMaintenanceEmployee.DataTextField = "EmployeeName";
            ddlMaintenanceEmployee.DataBind();

            //拓展人绑定
            ddlProlongationEmployee.DataSource = employeeList;
            ddlProlongationEmployee.DataValueField = "EmployeeID";
            ddlProlongationEmployee.DataTextField = "EmployeeName";
            ddlProlongationEmployee.DataBind();

            if (ddlProlongationEmployee.Items.Count > 0)
            {
                ddlProlongationEmployee.Items.FindByValue(User.Identity.Name).Selected = true;
                ddlMaintenanceEmployee.Items.FindByValue(User.Identity.Name).Selected = true;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            HA.PMS.DataAssmblly.FD_SaleSources fd_SaleSources = new DataAssmblly.FD_SaleSources();
            fd_SaleSources.Sourcename = txtSourcename.Text;


            fd_SaleSources.ProlongationDate = txtProlongationDate.Text.ToDateTime();
            fd_SaleSources.ProlongationEmployee = ddlProlongationEmployee.SelectedValue.ToInt32();
            fd_SaleSources.MaintenanceEmployee = ddlMaintenanceEmployee.SelectedValue.ToInt32();
            fd_SaleSources.ChannelTypeId = ddlChannelType.SelectedValue.ToInt32();
            fd_SaleSources.Address = txtAddress.Text;
            //fd_SaleSources.Fax = txtFax.Text;
            fd_SaleSources.Synopsis = txtSynopsis.Text;
            fd_SaleSources.Preferentialpolicy = txtPreferentialpolicy.Text;
            fd_SaleSources.Rebatepolicy = txtRebatepolicy.Text;
            fd_SaleSources.NeedRebate = ddlNeedRebate.SelectedValue == "0" ? true : false;


            fd_SaleSources.Tactcontacts1 = txtTactcontacts1.Text;
            fd_SaleSources.TactcontactsType1 = txtTactcontactsType1.Text;
            fd_SaleSources.TactcontactsJob1 = txtTactcontactsJob1.Text;
            fd_SaleSources.TactcontactsPhone1 = txtTactcontactsPhone1.Text;
            fd_SaleSources.Email1 = txtEmail1.Text;
            fd_SaleSources.QQ1 = txtQQ1.Text;
            fd_SaleSources.Weibo1 = txtWeibo1.Text;
            fd_SaleSources.WenXin1 = txtWenXin1.Text;



            fd_SaleSources.Tactcontacts2 = txtTactcontacts2.Text;
            fd_SaleSources.TactcontactsType2 = txtTactcontactsType2.Text;
            fd_SaleSources.TactcontactsJob2 = txtTactcontactsJob2.Text;
            fd_SaleSources.TactcontactsPhone2 = txtTactcontactsPhone2.Text;
            fd_SaleSources.Email2 = txtEmail2.Text;
            fd_SaleSources.QQ2 = txtQQ2.Text;
            fd_SaleSources.Weibo2 = txtWeibo2.Text;
            fd_SaleSources.WenXin2 = txtWenXin2.Text;
            fd_SaleSources.BankName = txtBankName.Text;
            fd_SaleSources.BankCard = txtBankCard.Text;


            fd_SaleSources.Tactcontacts3 = txtTactcontacts3.Text;
            fd_SaleSources.TactcontactsType3 = txtTactcontactsType3.Text;
            fd_SaleSources.TactcontactsJob3 = txtTactcontactsJob3.Text;
            fd_SaleSources.TactcontactsPhone3 = txtTactcontactsPhone3.Text;
            fd_SaleSources.Email3 = txtEmail3.Text;
            fd_SaleSources.QQ3 = txtQQ3.Text;
            fd_SaleSources.Weibo3 = txtWeibo3.Text;
            fd_SaleSources.WenXin3 = txtWenXin3.Text;
            fd_SaleSources.IsDelete = false;


            fd_SaleSources.Node1 = txtNode1.Text;
            fd_SaleSources.Node2 = txtNode2.Text;
            fd_SaleSources.Node3 = txtNode3.Text;
            int result = ObjSaleSourcesBLL.Insert(fd_SaleSources);
            if (result > 0)
            {

                // JavaScriptTools.AlertWindowAndReload("添加成功", this.Page);
                this.btnSave.Visible = false;
                //btnGoList.Visible = true;
                //btnEdit.Visible = true;
                btnCreateCustomer.Visible = false;
                hiddKey.Value = result + string.Empty;

                EnabledControls(false);
                //设置为启用
                ViewState["Enabled"] = true;
                JavaScriptTools.AlertWindowAndLocation("添加成功!", "FD_SaleSourcesManager.aspx?NeedPopu=1", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("添加失败,请重新尝试", this.Page);

            }
        }


        /// <summary>
        /// 为渠道添加新人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateCustomer_Click(object sender, EventArgs e)
        {
            Response.Redirect("FD_SaleSourcesCreateManager.aspx?NeedPopu=1");
        }
        /// <summary>
        /// 禁用或者启用文本框 和下拉框
        /// </summary>
        /// <param name="commandArgment"></param>
        protected void EnabledControls(bool commandArgment)
        {
            //this.Controls
            //foreach (var item in this.FindControl("form1").Controls)
            //{
            //    if (item is TextBox)
            //    {
            //        TextBox currentText = item as TextBox;
            //        currentText.Enabled = commandArgment;
            //    }

            //    if (item is DropDownList)
            //    {
            //        DropDownList CurrentDDL = item as DropDownList;
            //        CurrentDDL.Enabled = commandArgment;
            //    }

            //}
            txtSourcename.Enabled = commandArgment;


            txtProlongationDate.Enabled = commandArgment;
            ddlProlongationEmployee.Enabled = commandArgment;
            ddlMaintenanceEmployee.Enabled = commandArgment;
            ddlChannelType.Enabled = commandArgment;
            txtAddress.Enabled = commandArgment;
            //txtFax.Enabled = commandArgment;
            txtSynopsis.Enabled = commandArgment;
            txtPreferentialpolicy.Enabled = commandArgment;
            txtRebatepolicy.Enabled = commandArgment;
            ddlNeedRebate.Enabled = commandArgment;


            txtTactcontacts1.Enabled = commandArgment;
            txtTactcontactsType1.Enabled = commandArgment;
            txtTactcontactsJob1.Enabled = commandArgment;
            txtTactcontactsPhone1.Enabled = commandArgment;
            txtEmail1.Enabled = commandArgment;
            txtQQ1.Enabled = commandArgment;
            txtWeibo1.Enabled = commandArgment;
            txtWenXin1.Enabled = commandArgment;



            txtTactcontacts2.Enabled = commandArgment;
            txtTactcontactsType2.Enabled = commandArgment;
            txtTactcontactsJob2.Enabled = commandArgment;
            txtTactcontactsPhone2.Enabled = commandArgment;
            txtEmail2.Enabled = commandArgment;
            txtQQ2.Enabled = commandArgment;
            txtWeibo2.Enabled = commandArgment;
            txtWenXin2.Enabled = commandArgment;


            txtTactcontacts3.Enabled = commandArgment;
            txtTactcontactsType3.Enabled = commandArgment;
            txtTactcontactsJob3.Enabled = commandArgment;
            txtTactcontactsPhone3.Enabled = commandArgment;
            txtEmail3.Enabled = commandArgment;
            txtQQ3.Enabled = commandArgment;
            txtWeibo3.Enabled = commandArgment;
            txtWenXin3.Enabled = commandArgment;

        }

        /// <summary>
        /// 跳转渠道明细表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGoList_Click(object sender, EventArgs e)
        {
            Response.Redirect("FD_TelemarketingCustomersDetails.aspx?NeedPopu=1");
        }


        /// <summary>
        /// 编辑渠道信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            bool isoff = Convert.ToBoolean(ViewState["Enabled"]);
            //如果是true 的话就证明是保存之前的，如果是false的话就是保存修改的内容了
            if (isoff)
            {
                EnabledControls(true);
                ViewState["Enabled"] = false;
            }
            else
            {

                HA.PMS.DataAssmblly.FD_SaleSources fd_SaleSources = ObjSaleSourcesBLL.GetByID(hiddKey.Value.ToInt32());
                fd_SaleSources.Sourcename = txtSourcename.Text;


                fd_SaleSources.ProlongationDate = txtProlongationDate.Text.ToDateTime();
                fd_SaleSources.ProlongationEmployee = ddlProlongationEmployee.SelectedValue.ToInt32();
                fd_SaleSources.MaintenanceEmployee = ddlMaintenanceEmployee.SelectedValue.ToInt32();
                fd_SaleSources.ChannelTypeId = ddlChannelType.SelectedValue.ToInt32();
                fd_SaleSources.Address = txtAddress.Text;
                //fd_SaleSources.Fax = txtFax.Text;
                fd_SaleSources.Synopsis = txtSynopsis.Text;
                fd_SaleSources.Preferentialpolicy = txtPreferentialpolicy.Text;
                fd_SaleSources.Rebatepolicy = txtRebatepolicy.Text;
                fd_SaleSources.NeedRebate = ddlNeedRebate.SelectedValue == "0" ? true : false;


                fd_SaleSources.Tactcontacts1 = txtTactcontacts1.Text;
                fd_SaleSources.TactcontactsType1 = txtTactcontactsType1.Text;
                fd_SaleSources.TactcontactsJob1 = txtTactcontactsJob1.Text;
                fd_SaleSources.TactcontactsPhone1 = txtTactcontactsPhone1.Text;
                fd_SaleSources.Email1 = txtEmail1.Text;
                fd_SaleSources.QQ1 = txtQQ1.Text;
                fd_SaleSources.Weibo1 = txtWeibo1.Text;
                fd_SaleSources.WenXin1 = txtWenXin1.Text;



                fd_SaleSources.Tactcontacts2 = txtTactcontacts2.Text;
                fd_SaleSources.TactcontactsType2 = txtTactcontactsType2.Text;
                fd_SaleSources.TactcontactsJob2 = txtTactcontactsJob2.Text;
                fd_SaleSources.TactcontactsPhone2 = txtTactcontactsPhone2.Text;
                fd_SaleSources.Email2 = txtEmail2.Text;
                fd_SaleSources.QQ2 = txtQQ2.Text;
                fd_SaleSources.Weibo2 = txtWeibo2.Text;
                fd_SaleSources.WenXin2 = txtWenXin2.Text;


                fd_SaleSources.Tactcontacts3 = txtTactcontacts3.Text;
                fd_SaleSources.TactcontactsType3 = txtTactcontactsType3.Text;
                fd_SaleSources.TactcontactsJob3 = txtTactcontactsJob3.Text;
                fd_SaleSources.TactcontactsPhone3 = txtTactcontactsPhone3.Text;
                fd_SaleSources.Email3 = txtEmail3.Text;
                fd_SaleSources.QQ3 = txtQQ3.Text;
                fd_SaleSources.Weibo3 = txtWeibo3.Text;
                fd_SaleSources.WenXin3 = txtWenXin3.Text;
                fd_SaleSources.IsDelete = false;
                int result = ObjSaleSourcesBLL.Update(fd_SaleSources);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("修改成功", this.Page);
                    this.btnSave.Visible = true;
                    //btnGoList.Visible = false;
                    //btnEdit.Visible = false;
                    btnCreateCustomer.Visible = false;

                }
                else
                {
                    JavaScriptTools.AlertWindow("修改失败,请重新尝试", this.Page);

                }
            }
        }


        /// <summary>
        /// 为渠道添加新人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreateCustomer_Click1(object sender, EventArgs e)
        {

        }

        protected void btnCreateType_Click(object sender, EventArgs e)
        {
            Response.Redirect("FD_ChannelTypeManager.aspx?NeedPopu=1");
        }
    }
}