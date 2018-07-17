/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.2
 Description:客户未邀约界面
 History:修改日志

 Author:杨洋
 Date:2013.4.2
 version:好爱1.0
 description:修改描述
 
 
 
 */
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
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer
{
    public partial class FL_TelemarketingAnInvitation : SystemPage
    {
        BLLAssmblly.Flow.InvtieContent objInviteBLL = new BLLAssmblly.Flow.InvtieContent();
        Customers objCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }
       
        protected void DataBinder() 
        {

         
          
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            int sourceCount = 0;
            #region 分页页码
            int startIndex = InvitationPager.StartRecordIndex;
      


            //var query = objCustomersBLL.GetbyFL_TelemarketingCustomerEmployeeParameter(ObjParameterList.ToArray(),
            //InvitationPager.PageSize, InvitationPager.CurrentPageIndex, out sourceCount);
            //InvitationPager.RecordCount = sourceCount;
            //rptCustomers.DataSource = query;
            //rptCustomers.DataBind();
            #endregion
        }
        protected void InvitationPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void lkbtnQuery_Click(object sender, EventArgs e)
        {
            //int InviteId = hfInvitation.Value.ToInt32();
            ////沟通表ID

            //FL_InvtieContent invite = new FL_InvtieContent();
            //invite.CommunicationTime = Request.Form["ctl00$hid_Flag"].ToDateTime();
            //int result = objInviteBLL.Update(invite);
            //if (result > 0)


            //{
            //    JavaScriptTools.AlertWindow("操作成功", this.Page);
            //    DataBinder();
            //}
            //else
            //{
            //    JavaScriptTools.AlertWindow("操作失败，请重试", this.Page);
            //}
        }

        
    }
}