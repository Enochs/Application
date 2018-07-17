using HA.PMS.BLLAssmblly.SysTarget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Flow;
using System.IO;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch
{
    public partial class SalesRanking :HA.PMS.Pages.SystemPage
    {
        Employee ObjEmployeeBLL = new Employee();

        FinishTargetSum objFinishTargetSumBLL = new FinishTargetSum();
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
            repRanking.DataSource = objFinishTargetSumBLL.GetinTargetRanking(7, DateTime.Now.Year);
            repRanking.DataBind();
        }


        public string GetCustomerCount(object Employee)
        {
            Order ObjOrderBLL = new Order();
            return ObjOrderBLL.GetCustomerOrderBYEmployee(int.Parse(Employee.ToString())).ToString();
        }



        public string GetCustomerCountAvg(object FinishSum,object Employee)
        {
            Order ObjOrderBLL = new Order();
            var CustomerCount= ObjOrderBLL.GetCustomerOrderBYEmployee(int.Parse(Employee.ToString()));
            if(CustomerCount>0)
            {
              return( decimal.Parse(FinishSum.ToString()) / CustomerCount).ToString("0.00");
            }else
            {
            return string.Empty;
            }

        }

    }
}