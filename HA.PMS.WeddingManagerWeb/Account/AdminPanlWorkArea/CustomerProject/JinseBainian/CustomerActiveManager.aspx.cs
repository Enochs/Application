using HA.PMS.BLLAssmblly.CustomerSystem;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.EditoerLibrary;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CustomerProject.JinseBainian
{
    public partial class CustomerActiveManager : SystemPage
    {
        PackageReserve ObjReserveBLL = new PackageReserve();

        CelebrationPackage ObjCeleBrationBLL = new CelebrationPackage();

        int SourceCount = 0;            //返回数据总记录条数
        string OrderColumnName = "SourceKey";   //排序字段名称

        #region 页面加载
        /// <summary>
        /// 页面加载  初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
                DDLBinder();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void DataBinder()
        {
            PMSParameters pars = new PMSParameters();
            List<PMSParameters> objParmList = new List<PMSParameters>();
            //套餐名称
            objParmList.Add(ddlPackage.SelectedValue.ToInt32() > 0,"PackageID",ddlPackage.SelectedValue.ToInt32(),NSqlTypes.Equal);
            //按照录入人
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                MyManager.GetEmployeePar(objParmList);
            }
            //套餐时段
            objParmList.Add(ddlTimeSpan.SelectedValue.ToInt32() > 0,"DateItem",ddlTimeSpan.SelectedItem.Text,NSqlTypes.StringEquals);
            //预定状态
            objParmList.Add(ddlSchedule.SelectedValue.ToInt32() >= 0,"State",ddlSchedule.SelectedValue.ToInt32(),NSqlTypes.Equal);
            List<CC_PackageReserve> OjbReserveList = ObjReserveBLL.GetAllByParameter(objParmList, OrderColumnName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            rptReserver.DataSource = OjbReserveList;
            rptReserver.DataBind();
            
        }
        #endregion

        #region 分页 上一页  下一页
        /// <summary>
        /// 分页
        /// </summary>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region 获取套餐名称
        /// <summary>
        /// 获取套餐名称
        /// </summary>
        /// <returns></returns>
        public string GetByPackgetID(object source)
        {
            int PackageID = source.ToString().ToInt32();
            FD_CelebrationPackage Cele = ObjCeleBrationBLL.GetByID(PackageID);
            if (Cele != null)
            {
                return Cele.PackageTitle.ToString();
            }
            return "";
        }
        #endregion


        #region 下拉框绑定
        /// <summary>
        /// 下拉框绑定
        /// </summary>
        public void DDLBinder()
        {
            List<FD_CelebrationPackage> CelebrationList = ObjCeleBrationBLL.GetByAll();
            ddlPackage.DataTextField = "PackageTitle";
            ddlPackage.DataValueField = "PackageID";
            CelebrationList.Insert(0,new FD_CelebrationPackage { PackageID = 0, PackageTitle = "请选择" });
            ddlPackage.DataSource = CelebrationList;
            ddlPackage.DataBind();
            ddlPackage.SelectedIndex = 0;
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 条件查询
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataBinder();
        }
        #endregion

        #region Repeater 绑定触发事件
        /// <summary>
        /// 事件
        /// </summary>
        protected void rptReserver_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "LookDetails")
            {
                int SourceKey = e.CommandArgument.ToString().ToInt32();
                Response.Redirect("CustomerActiveDetails.aspx?SourceKey=" + SourceKey);
            }
        }
        #endregion
    }
}