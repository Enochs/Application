using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Credit
{
    public partial class CustomerKeyID : SystemPage
    {
        Report ObjReport = new Report();
        Customers ObjCustomerBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();

            }
        }

        private void BinderData()
        {
            var SSMOdel = ObjReport.GetByCustomerID(Request["CustomerID"].ToInt32(), User.Identity.Name.ToInt32());
            txtGroomIDCard.Text = SSMOdel.CustomerKeyID;
            txtCardID.Text = SSMOdel.CardID;
            txtGetCardDate.Text = SSMOdel.GetCardDate.ToString();

            var fl_customer = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            lblGroomName.Text = fl_customer.Groom;
            lblBrideName.Text = fl_customer.Bride;
            lblGroomPhone.Text = fl_customer.GroomCellPhone;
            lblBridePhone.Text = fl_customer.BrideCellPhone;
            txtGroomIDCard.Text = fl_customer.GroomIdCard;
            txtBrideIDCard.Text = fl_customer.BrideIdCard;

        }

        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            var SSMOdel = ObjReport.GetByCustomerID(Request["CustomerID"].ToInt32(), User.Identity.Name.ToInt32());
            SSMOdel.CustomerKeyID = txtGroomIDCard.Text.Trim() == "" ? txtBrideIDCard.Text.Trim() : txtGroomIDCard.Text.Trim();
            SSMOdel.CardID = txtCardID.Text;
            SSMOdel.GetCardDate = txtGetCardDate.Text.ToDateTime();
            ObjReport.Update(SSMOdel);

            var fl_customer = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            fl_customer.BrideIdCard = txtBrideIDCard.Text.Trim().ToString();
            fl_customer.GroomIdCard = txtGroomIDCard.Text.Trim().ToString();
            ObjCustomerBLL.Update(fl_customer);

            JavaScriptTools.AlertWindowAndLocation("保存成功!", "CustomerCredit.aspx", Page);
        }
    }
}