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
using System.Web.Providers.Entities;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class MessageBoard : System.Web.UI.UserControl
    {
        Channel ObjChannelBLL = new Channel();
        Employee objEmployeeBLL = new Employee();
        HA.PMS.BLLAssmblly.Flow.MessageBoard objMessageBoardBLL = new HA.PMS.BLLAssmblly.Flow.MessageBoard();
        protected void Page_Load(object sender, EventArgs e)
        {

            DataBinder();

        }
        /// <summary>
        /// 返回员工姓名
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>

        protected string GetEmployeeName(int EmployeeId)
        {

            Sys_Employee emp = objEmployeeBLL.GetByID(EmployeeId);
            if (emp != null)
            {
                return emp.EmployeeName;
            }
            return "";
        }
        /// <summary>
        /// 给谁的ID
        /// </summary>
        private int ToEmpLoyeeID { get; set; }
        public string ClassType
        {
            get;
            set;
        }

        protected void DataBinder()
        {
            #region 分页页码

            int startIndex = MessagePager.StartRecordIndex;
            int resourceCount = 0;
            int currentLoginEmployeeID = 0;
            if (Request.Cookies["userName"] != null)
            {   //发送留言者
                currentLoginEmployeeID = Server.HtmlEncode(Request.Cookies["userName"].Value).ToInt32();
            }

            var query = objMessageBoardBLL.GetByIndex(currentLoginEmployeeID, MessagePager.PageSize, MessagePager.CurrentPageIndex, out resourceCount);
            MessagePager.RecordCount = resourceCount;
            rptMessList.DataSource = query;
            rptMessList.DataBind();



            #endregion

        }
        protected string GetEmployeeName(object source)
        {
            int employeeId = (source + string.Empty).ToInt32();
            Employee objEmployeeBLL = new Employee();
            Sys_Employee emp = objEmployeeBLL.GetByID(employeeId);

            if (emp != null)
            {
                return emp.EmployeeName;
            }
            return "";
        }
        protected string GetDateStr(object ObjDatetime)
        {

            if (ObjDatetime != null)
            {
                return ObjDatetime.ToString().ToDateTime().ToShortDateString();
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 给他留言
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCreae_Click(object sender, EventArgs e)
        {

            var ChannelID = ObjChannelBLL.GetbyClassType(ClassType);
            FL_MessageBoard mes = new FL_MessageBoard();
            mes.CreateDate = DateTime.Now;
            //访问cookie
            if (Request.Cookies["userName"] != null)
            {   //发送留言者
                mes.CreateEmpLoyee = Server.HtmlEncode(Request.Cookies["userName"].Value).ToInt32();
            }
            mes.EmpLoyeeID = hfParameter.Value.ToInt32();
            mes.MessAgeContent = txtCreateContent.Text;
            //mes.ChannelID = ChannelID.ChannelID;
            int result = objMessageBoardBLL.Insert(mes);
            if (result > 0)
            {
                JavaScriptTools.AlertWindow("操作成功", this.Page);
                txtCreateContent.Text = "";
            }
        }

        protected void MessagePager_PageChanged(object sender, EventArgs e)
        {

        }

        protected void rptMessList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            LinkButton lkbtnReply = e.Item.FindControl("lkbtnReply") as LinkButton;

            FL_MessageBoard messge = e.Item.DataItem as FL_MessageBoard;
            PlaceHolder phReplyTo = e.Item.FindControl("phReplyTo") as PlaceHolder;
            if (e.CommandName == "Reply")
            {

                phReplyTo.Visible = true;
                lkbtnReply.Visible = false;
            }
            if (e.CommandName == "btnSub")
            {
                var ChannelID = ObjChannelBLL.GetbyClassType(ClassType);
                FL_MessageBoard mes = new FL_MessageBoard();
               
                
                mes.CreateDate = DateTime.Now;
                //访问cookie
                if (Request.Cookies["userName"] != null)
                {   //发送留言者
                    mes.CreateEmpLoyee = Server.HtmlEncode(Request.Cookies["userName"].Value).ToInt32();
                }
                HiddenField hfCreateEmployee = e.Item.FindControl("hfCreateEmployee") as HiddenField;
                mes.EmpLoyeeID = hfCreateEmployee.Value.ToInt32();
                string content = Request["ctl00$ContentPlaceHolder1$MessageBoard$rptMessList$ctl00$txtReplyMessage"];
                mes.MessAgeContent = content;
                //mes.ChannelID = ChannelID.ChannelID;
                int result = objMessageBoardBLL.Insert(mes);
               
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("操作成功", this.Page);
                    phReplyTo.Visible = false;
                   
                    lkbtnReply.Visible = true;
                }
            }
        }
    }
}