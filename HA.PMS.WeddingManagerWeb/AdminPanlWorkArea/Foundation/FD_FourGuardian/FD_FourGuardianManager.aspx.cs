
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.21
 Description:四大金刚管理页面
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
using System.Data.Objects;
using System.IO;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian
{
    public partial class FD_FourGuardianManager : SystemPage
    {
        FourGuardian objFourGuardianBLL = new FourGuardian();
        GuardianType objGuardianTypeBLL = new GuardianType();
        GuradianLeven objGuradianLevenBLL = new GuradianLeven();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataDropDownList();
                DataBinder();
            }
        }
        /// <summary>
        /// 绑定主体数据源
        /// </summary>
        protected void DataBinder()
        {
            HA.PMS.DataAssmblly.FD_FourGuardian fourGuardian = new HA.PMS.DataAssmblly.FD_FourGuardian();
            fourGuardian.GuardianLevenId = ddlGuardianLeven.SelectedValue.ToInt32();
            fourGuardian.GuardianTypeId = ddlGuardianType.SelectedValue.ToInt32();
            fourGuardian.GuardianName = txtName.Text.ToString();
            List<PMSParameters> ObjParameterList = new List<PMSParameters>();
            if (ddlGuardianType.SelectedValue.ToInt32() != 0)
            {
                //ObjParameterList.Add(new ObjectParameter("GuardianTypeId", fourGuardian.GuardianTypeId));
                ObjParameterList.Add("GuardianTypeId", fourGuardian.GuardianTypeId, NSqlTypes.Equal);
            }

            if (ddlGuardianLeven.SelectedValue.ToInt32() != 0)
            {
                //ObjParameterList.Add(new ObjectParameter("GuardianLevenId", fourGuardian.GuardianLevenId));
                ObjParameterList.Add("GuardianLevenId", fourGuardian.GuardianLevenId, NSqlTypes.Equal);
            }
            if (txtName.Text != string.Empty)
            {
                //ObjParameterList.Add(new ObjectParameter("GuardianName", fourGuardian.GuardianName));
                ObjParameterList.Add("GuardianName", fourGuardian.GuardianName, NSqlTypes.LIKE);
            }
            ObjParameterList.Add("IsDelete", false, NSqlTypes.Bit);
            #region 分页页码
            int startIndex = GuardianPager.StartRecordIndex;
            int resourceCount = 0;
            var query = objFourGuardianBLL.GetAllByParameter(ObjParameterList, "StarTime", GuardianPager.PageSize, GuardianPager.CurrentPageIndex, out resourceCount);
            GuardianPager.RecordCount = resourceCount;
            HidePage.Value = GuardianPager.CurrentPageIndex.ToString();
            rptGuardian.DataSource = query;
            rptGuardian.DataBind();


            #endregion



        }
        /// <summary>
        /// 绑定下拉框
        /// </summary>
        protected void DataDropDownList()
        {
            ddlGuardianLeven.DataSource = objGuradianLevenBLL.GetByAll();
            ddlGuardianLeven.DataTextField = "LevenName";
            ddlGuardianLeven.DataValueField = "LevenId";
            ddlGuardianLeven.DataBind();
            ddlGuardianLeven.Items.Add(new ListItem("请选择", "0"));
            ddlGuardianLeven.SelectedIndex = ddlGuardianLeven.Items.Count - 1;

            ddlGuardianType.DataSource = objGuardianTypeBLL.GetByAll();
            ddlGuardianType.DataTextField = "TypeName";
            ddlGuardianType.DataValueField = "TypeId";
            ddlGuardianType.DataBind();

            ddlGuardianType.Items.Add(new ListItem("请选择", "0"));
            ddlGuardianType.SelectedIndex = ddlGuardianType.Items.Count - 1;
        }

        protected void rptGuardian_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                int GuardianId = e.CommandArgument.ToString().ToInt32();



                HA.PMS.DataAssmblly.FD_FourGuardian fourGuardian = objFourGuardianBLL.GetByID(GuardianId);
                if (File.Exists(Server.MapPath(fourGuardian.HeadImgPath)))
                {
                    File.Delete(Server.MapPath(fourGuardian.HeadImgPath));
                }
                objFourGuardianBLL.Delete(fourGuardian);
                //删除之后重新绑定数据源
                DataBinder();
            }
        }



        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void GuardianPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        #region  刷新本页面


        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            this.Page.Request.Url.ToString();
        }
        #endregion
    }
}