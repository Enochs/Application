using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StorehouseCreateUpdate :SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        private void BinderData()
        { 
        }
        /// <summary>
        /// 创建库房
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            Storehouse ObjStorehouseBLL = new Storehouse();

            if (Request["HouseID"] == null)
            {
                ObjStorehouseBLL.Insert(new HA.PMS.DataAssmblly.FD_Storehouse() { 
                DepartmentID=0,
                EmpLoyeeID=0,
                HouseName=txtHouseName.Text
                });

                JavaScriptTools.AlertWindowAndReaload("添加成功!", Page);
            }
            else
            { 
            
            }

        }
    }
}