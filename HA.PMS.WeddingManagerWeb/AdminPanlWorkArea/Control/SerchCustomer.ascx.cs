using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class SerchCustomer : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 查询新人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lkbtnQuery_Click(object sender, EventArgs e)
        {
            Customers ObjCustomersBLL = new Customers();
            if (ObjCustomersBLL.GetOnlyByNameOrPhone(txtCustomer.Text, txtPhone.Text) == null)
            {
                switch (Request["Part"])
                { 
                    case "0":

                        break;
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    default :
                        break;
                }
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("客户已经存在",Page);
            }
        }
    }
}