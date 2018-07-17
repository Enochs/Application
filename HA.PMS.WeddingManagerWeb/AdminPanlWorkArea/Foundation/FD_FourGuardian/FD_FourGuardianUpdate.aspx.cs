
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.21
 Description:四大金刚修改管理页面
 History:修改日志

 Author:杨洋
 date:2013.3.21
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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian
{
    public partial class FD_FourGuardianUpdate : SystemPage
    {
        FourGuardian objFourGuardianBLL = new FourGuardian();
        GuardianType objGuardianTypeBLL = new GuardianType();
        GuradianLeven objGuradianLevenBLL = new GuradianLeven();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int GuardianId = Request.QueryString["GuardianId"].ToInt32();
                DataBinder();
                HA.PMS.DataAssmblly.FD_FourGuardian fD_FourGuardian = objFourGuardianBLL.GetByID(GuardianId);
                txtExplain.Text = fD_FourGuardian.Explain;
                txtPersonalDetails.Text = fD_FourGuardian.PersonalDetails;
                txtGuardianName.Text = fD_FourGuardian.GuardianName;
                txtCellPhone.Text = fD_FourGuardian.CellPhone;
                txtStarTime.Text = fD_FourGuardian.StarTime + string.Empty;

                txtCooperationPrice.Text = fD_FourGuardian.CooperationPrice + string.Empty;
                txtSalePrice.Text = fD_FourGuardian.SalePrice.ToString() + string.Empty;
                txtEmail.Text = fD_FourGuardian.Email;
                txtSort.Text = fD_FourGuardian.FourSort.ToString();
                txtBankName.Text = fD_FourGuardian.BankName == null ? "" : fD_FourGuardian.BankName;
                txtAccountInformation.Text = fD_FourGuardian.AccountInformation == null ? "" : fD_FourGuardian.AccountInformation;
                ddlGuardianTypeId.Items.FindByValue(fD_FourGuardian.GuardianTypeId + string.Empty).Selected = true;
                ddlGuardianLevenId.Items.FindByValue(fD_FourGuardian.GuardianLevenId + string.Empty).Selected = true;
            }
        }
        protected void DataBinder()
        {
            ddlGuardianTypeId.DataSource = objGuardianTypeBLL.GetByAll();
            ddlGuardianTypeId.DataValueField = "TypeId";
            ddlGuardianTypeId.DataTextField = "TypeName";
            ddlGuardianTypeId.DataBind();


            ddlGuardianLevenId.DataSource = objGuradianLevenBLL.GetByAll();
            ddlGuardianLevenId.DataValueField = "LevenId";
            ddlGuardianLevenId.DataTextField = "LevenName";
            ddlGuardianLevenId.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int GuardianId = Request.QueryString["GuardianId"].ToInt32();
            HA.PMS.DataAssmblly.FD_FourGuardian four = objFourGuardianBLL.GetByID(GuardianId);
            four.PersonalDetails = txtPersonalDetails.Text.Trim();
            four.Explain = txtExplain.Text.Trim();
            four.GuardianLevenId = ddlGuardianLevenId.SelectedValue.ToInt32();
            four.GuardianName = txtGuardianName.Text;
            four.GuardianTypeId = ddlGuardianTypeId.SelectedValue.ToInt32();
            four.IsDelete = false;
            four.CellPhone = txtCellPhone.Text;
            four.StarTime = txtStarTime.Text.ToDateTime();
            four.CooperationPrice = txtCooperationPrice.Text.ToDecimal();
            four.SalePrice = txtSalePrice.Text.Trim().ToDecimal();
            four.BankName = txtBankName.Text.Trim().ToString();
            four.AccountInformation = txtAccountInformation.Text.Trim().ToString();
            four.FourSort = txtSort.Text.Trim().ToInt32();


            four.Email = txtEmail.Text;

            string savaPath = "~/Files/ImageWareHouse/";
            string fileExt = "";
            bool isOk = true;
            if (flieUp.HasFile)
            {

                fileExt = System.IO.Path.GetExtension(flieUp.FileName).ToLower();
                if (fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp")
                {
                    four.HeadImgPath = savaPath + DateTime.Now.ToFileTimeUtc() + fileExt;

                    flieUp.SaveAs(Server.MapPath(four.HeadImgPath));


                }
                else
                {
                    isOk = false;
                    JavaScriptTools.AlertWindow("请你上传的图片只能是jpeg png jpg gif bmp", this.Page);

                }
            }

            if (isOk)
            {
                int Page = Request["Page"].ToInt32();
                int result = objFourGuardianBLL.Update(four);

                //根据返回判断添加的状态
                if (result > 0)
                {
                    JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
                    //Response.Write("<script>window.opener=null;window.close();</script>");
                }

            }


        }
    }
}