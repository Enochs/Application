using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MessageforEmployee : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Message ObjMessageModel = new Message();
            var ObjModel = ObjMessageModel.GetByID(int.Parse(Request["MessageID"]));
            this.lblCreateDate.Text = ObjModel.CreateDate.ToString();
            this.lblMessage.Text = ObjModel.Message;
            this.lblCreateEmployee.Text = ObjModel.CreateEmployeename;
            ObjModel.IsLook = true;
            ObjMessageModel.Update(ObjModel);

            if (ObjModel.KeyWords != string.Empty && ObjModel.KeyWords != null)
            {
                Response.Redirect(ObjModel.KeyWords);
            }
        }
    }
}