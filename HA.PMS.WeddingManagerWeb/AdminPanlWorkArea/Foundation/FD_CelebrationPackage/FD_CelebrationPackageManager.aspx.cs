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


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage
{
    public partial class FD_CelebrationPackageManager : SystemPage
    {
        CelebrationPackage objCelebrationPackage = new CelebrationPackage();
        Department objDepartmentBLL = new Department();
        CelebrationPackageStyle objCelebrationPackageStyleBLL = new CelebrationPackageStyle();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
        /// <summary>
        /// 返回品牌名称
        /// </summary>
        /// <param name="source"></param>
        protected string GetDepartmentBrandByDepartId(object source)
        {
            int departId = (source + string.Empty).ToInt32();
            Sys_Department sysDepartment = objDepartmentBLL.GetByID(departId);
            if (sysDepartment != null)
            {
                return sysDepartment.Brand;
            }
            return "";

        }
        /// <summary>
        /// 返回套系样式
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetStyleByID(object source)
        {
            int styleId = (source + string.Empty).ToInt32();
            FD_CelebrationPackageStyle style = objCelebrationPackageStyleBLL.GetByID(styleId);
            if (style != null)
            {
                return style.StyleName;
            }
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        protected void DataBinder()
        {
            #region 分页页码
            int startIndex = CelebrationPager.StartRecordIndex;
            int resourceCount = 0;
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            var query = objCelebrationPackage.GetPackageDataByParameter(ObjParameterList.ToArray(), CelebrationPager.PageSize, CelebrationPager.CurrentPageIndex, out resourceCount);
            CelebrationPager.RecordCount = resourceCount;

            rptCelebrationList.DataSource = query;
            rptCelebrationList.DataBind();


            #endregion
        }

        protected void CelebrationPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void rptCelebrationList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Button lkbtnOn = e.Item.FindControl("lkbtnOn") as Button;
            Button lkbtnOff = e.Item.FindControl("lkbtnOff") as Button;
            HA.PMS.DataAssmblly.FD_CelebrationPackage fd = e.Item.DataItem as HA.PMS.DataAssmblly.FD_CelebrationPackage;
            if (fd != null)
            {
                //false 启用状态
                if (!fd.IsDelete)
                {
                    lkbtnOn.Enabled = true;
                    
                }
                else
                {

                    lkbtnOff.Enabled = true;
                }
            }
            Literal ltlTitle = e.Item.FindControl("ltlTitle") as Literal;
            // true 没有制作， false  制作  是否制作了报价单
            if (fd.IsMake.HasValue)
	        {
             
                if (fd.IsMake.Value)
                {
                    ltlTitle.Text = "制作套系报价单"; 
                }
                else
                {
                    ltlTitle.Text = "修改套系报价单"; 

                }
            }
            else
            {
                ltlTitle.Text = "制作套系报价单"; 
            }

        }

        protected void rptCelebrationList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            HA.PMS.DataAssmblly.FD_CelebrationPackage fd = objCelebrationPackage.GetByID((e.CommandArgument + string.Empty).ToInt32());
            if (fd != null)
            {
                if (e.CommandName == "On")
                {
                    fd.IsDelete = true;
                }
                else
                {
                    fd.IsDelete = false;
                }
                int result = objCelebrationPackage.Update(fd);
                if (result > 0)
                {
                    JavaScriptTools.AlertAndClosefancybox("操作成功", this.Page);
                }
            }

        }
    }
}