using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys
{
    public partial class LoginLogList : SystemPage
    {
        Employee ObjEmployeeBLL = new Employee();

        LoginLog ObjLoginBLL = new LoginLog();

        string SortName = "CreateDate";
        int SourceCount = 0;

        #region 页面初始化
        /// <summary>
        /// 页面加载
        /// </summary>  
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData(sender, e);
            }
        }
        #endregion

        #region 数据加载
        /// <summary>
        /// 加载数据
        /// </summary>
        public void BinderData(object sender, EventArgs e)
        {
            List<PMSParameters> pars = new List<PMSParameters>();

            //责任人 登陆人 姓名
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                MyManager.GetEmployeePar(pars);
            }

            //登录日期
            pars.Add(DateRanger.IsNotBothEmpty, "CreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);

            var DataList = ObjLoginBLL.GetDataByParameters(pars, SortName, CtrPager.PageSize, CtrPager.CurrentPageIndex, out SourceCount);
            CtrPager.RecordCount = SourceCount;
            lblDetailsCount.Text = "总共" + SourceCount + "条数据";
            rptLoginLogList.DataBind(DataList);
        }
        #endregion

        protected void lbtnTenNum_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = sender as LinkButton;
            if (lbtn.ID == "lbtnTenNum")
            {
                CtrPager.PageSize = 10;
            }
            else if (lbtn.ID == "lbtnTwentyNum")
            {
                CtrPager.PageSize = 20;
            }
            BinderData(sender, e);
        }
    }
}