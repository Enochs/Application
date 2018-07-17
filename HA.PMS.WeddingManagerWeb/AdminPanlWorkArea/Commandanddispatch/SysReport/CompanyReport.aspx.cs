using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.SysReport
{
    public partial class CompanyReport :SystemPage
    {
        //年份
        int Year = DateTime.Now.Year;
        Report ObjReportBLL = new Report();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {

        }



        /// <summary>
        /// 绑定销售目标
        /// </summary>
        /// <returns></returns>
        ///month:月份 SSType统计类型 1新增定金
        public string BinderSaleReport(int Month, int SSType)
        {

            return string.Empty;
           // return ObjReportBLL.GetReportByEmployee(User.Identity.Name.ToInt32(), SSType, Year, Month);

        }


        public string BinderSumReport(int Month, int SSType)
        {
          var ListString= ObjReportBLL.GetSumReport(User.Identity.Name.ToInt32(), SSType, Year).Split(',');
          return "<td>" + ListString[0] + "</td><td>" + ListString[1] + "</td>";
        }
    }
}

