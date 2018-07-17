
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.21
 Description:四大金刚创建管理页面
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
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian
{
    public partial class FD_FourGuardianCreate : SystemPage
    {
        FourGuardian objFourGuardianBLL = new FourGuardian();
        GuardianType objGuardianTypeBLL = new GuardianType();
        GuradianLeven objGuradianLevenBLL = new GuradianLeven();
         
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
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
            if (ddlGuardianLevenId.SelectedIndex < 0)
            {
                JavaScriptTools.AlertWindow("请先添加专业人员等级", Page);
                return;
            }
            if (ddlGuardianTypeId.SelectedIndex < 0)
            {
                JavaScriptTools.AlertWindow("请先添加专业人员类型", Page);
                return;
            }

            HA.PMS.DataAssmblly.FD_FourGuardian four = new HA.PMS.DataAssmblly.FD_FourGuardian()
            {
                GuardianLevenId = ddlGuardianLevenId.SelectedValue.ToInt32(),
                GuardianName = txtGuardianName.Text,
                GuardianTypeId = ddlGuardianTypeId.SelectedValue.ToInt32(),
                IsDelete = false,
                CellPhone = txtCellPhone.Text,
                StarTime = txtStarTime.Text.ToDateTime(),
                Explain = txtExplain.Text.Trim(),
                CooperationPrice = txtCooperationPrice.Text.ToDecimal(),
                SalePrice=txtSalePrice.Text.ToDecimal(),
                Email = txtEmail.Text,
                PersonalDetails = txtPersonalDetails.Text.Trim(),
                AccountInformation=txtAccountInformation.Text.Trim().ToString()
            };


            string savaPath = "~/Files/ImageWareHouse/";
            string fileExt = "";
            bool isOk = true;
            if (flieUp.HasFile)
            {
                if (!Directory.Exists(Server.MapPath(savaPath)))
                {
                    Directory.CreateDirectory(Server.MapPath(savaPath));
                }

                fileExt = System.IO.Path.GetExtension(flieUp.FileName).ToLower();
                if (fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp")
                {
                    four.HeadImgPath = savaPath + DateTime.Now.ToFileTimeUtc() + fileExt;

                    flieUp.SaveAs(Server.MapPath(four.HeadImgPath));
                    //if (isOk)
                    //{
                    //    int result = objFourGuardianBLL.Insert(four);

                    //    //根据返回判断添加的状态
                    //    if (result > 0)
                    //    {

                    //        JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);


                    //    }
                    //    else
                    //    {
                    //        JavaScriptTools.AlertWindow("添加失败,请重新尝试", this.Page);

                    //    }
                    //}

                }
                else
                {
                    isOk = false;
                    JavaScriptTools.AlertWindow("请你上传的图片只能是jpeg png jpg gif bmp", this.Page);

                }
            }

            //else
            //{
            //    JavaScriptTools.AlertWindow("请你选择人员头像进行上传", this.Page);
            //}

            int result = objFourGuardianBLL.Insert(four);

            //根据返回判断添加的状态
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);

            }
            else
            {
                JavaScriptTools.AlertWindow("添加失败,请重新尝试", this.Page);

            }
        }
            
        
    }
}