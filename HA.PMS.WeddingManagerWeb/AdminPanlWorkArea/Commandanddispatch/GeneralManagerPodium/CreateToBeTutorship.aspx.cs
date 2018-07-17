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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.GeneralManagerPodium
{
    public partial class CreateToBeTutorship : SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }
        Channel ObjChannelBLL = new Channel();
        HA.PMS.BLLAssmblly.Flow.MessageBoard objMessageBoardBLL = new HA.PMS.BLLAssmblly.Flow.MessageBoard();
        protected void btnCreate_Click(object sender, EventArgs e)
        {
            int ToEmployee = Request.QueryString["EmployeeID"].ToInt32();
            int createEmployee = User.Identity.Name.ToInt32();

            FL_MessageBoard mes = new FL_MessageBoard();
            mes.CreateDate = DateTime.Now;
            //访问cookie

            mes.CreateEmpLoyee = createEmployee;
     
            mes.EmpLoyeeID = ToEmployee;
            mes.MessAgeContent = txtMess.Text;
             var ChannelID = ObjChannelBLL.GetbyClassType("UnderTheWorkingState");
             if (ChannelID!=null)
             {
                 //mes.ChannelID = ChannelID.ChannelID;
             }
           
            int result = objMessageBoardBLL.Insert(mes);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("操作成功", this.Page);
            }
        }
    }
}