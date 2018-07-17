using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Report;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Credit
{
    public partial class CreditAddDecrease : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Type"] == "1")
            {
                lblPointType.Text = "增加积分";
            }

            if (Request["Type"] == "2")
            {
                lblPointType.Text = "减少积分";
            }
        }

        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            decimal Point = 0;
            if (Request["Type"] == "1")
            {
                Point = txtPoint.Text.ToDecimal();
            }


            if (Request["Type"] == "2")
            {
                Point = ("-" + txtPoint.Text).ToDecimal(); ;
            }

            HA.PMS.BLLAssmblly.CS.CreditLog ObjCreditBLL = new BLLAssmblly.CS.CreditLog();

            ObjCreditBLL.Insert(new DataAssmblly.CS_CreditLog()
            {
                CustomerID = Request["CustomerID"].ToInt32(),
                CreateDate = DateTime.Now,
                Node = txtNode.Text,
                Point = Point,
                EmployeeID=User.Identity.Name.ToInt32()
            });
            JavaScriptTools.AlertWindowAndLocation("操作成功！", "/AdminPanlWorkArea/CS/Credit/CustomerCredit.aspx", Page);
        }
    }
}