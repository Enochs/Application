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

namespace HA.PMS.WeddingManagerWeb.TheStage.Foundation
{
    public partial class ProfessionalTeam : SystemPage
    {
        FourGuardian objFourGuardianBLL = new FourGuardian();
        GuardianType objGuardianTypeBLL = new GuardianType();
        GuradianLeven objGuradianLevenBLL = new GuradianLeven();
        CelebrationPackage objCelebrationPackageBLL = new CelebrationPackage();

        CelebrationPackagePriceSpan objCelebrationPackagePriceSpan = new CelebrationPackagePriceSpan();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataDropDownList();
                DataBinder();
            }
        }
        /// <summary>
        /// 返回等级名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetLevenById(object source)
        {
            int LevenId = (source + string.Empty).ToInt32();
            FD_GuradianLeven gl = objGuradianLevenBLL.GetByID(LevenId);
            return gl.LevenName;
        }
        /// <summary>
        /// 返回风格
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetTypeById(object source)
        {
            int typeId = (source + string.Empty).ToInt32();
            FD_GuardianType types = objGuardianTypeBLL.GetByID(typeId);
            return types.TypeName;

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

        /// <summary>
        /// 绑定主体数据源
        /// </summary>
        protected void DataBinder()
        {
            HA.PMS.DataAssmblly.FD_FourGuardian fourGuardian = new HA.PMS.DataAssmblly.FD_FourGuardian();
            fourGuardian.GuardianLevenId = ddlGuardianLeven.SelectedValue.ToInt32();
            fourGuardian.GuardianTypeId = ddlGuardianType.SelectedValue.ToInt32();
            List<PMSParameters> ObjParameterList = new List<PMSParameters>();
            if (ddlGuardianType.SelectedValue.ToInt32() != 0)
            {
                ObjParameterList.Add("GuardianTypeId", fourGuardian.GuardianTypeId, NSqlTypes.Equal);
            }

            if (ddlGuardianLeven.SelectedValue.ToInt32() != 0)
            {
                ObjParameterList.Add("GuardianLevenId", fourGuardian.GuardianLevenId, NSqlTypes.Equal);
            }
            ObjParameterList.Add("IsDelete", false, NSqlTypes.Bit);
            #region 分页页码
            int startIndex = GuardianPager.StartRecordIndex;
            int resourceCount = 0;
            var query = objFourGuardianBLL.GetbyParameter(ObjParameterList, "FourSort", GuardianPager.PageSize, GuardianPager.CurrentPageIndex, out resourceCount);
            GuardianPager.RecordCount = resourceCount;

            rptGuardian.DataSource = query;
            rptGuardian.DataBind();


            #endregion



        }

        protected void GuardianPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

    }
}