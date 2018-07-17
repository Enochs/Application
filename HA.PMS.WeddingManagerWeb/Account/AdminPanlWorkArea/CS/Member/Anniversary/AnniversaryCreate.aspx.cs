using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Flow;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Anniversary
{
    public partial class AnniversaryCreate :SystemPage
    {


        /// <summary>
        /// 服务内容
        /// </summary>
        HA.PMS.BLLAssmblly.CS.Member ObjMemberBLL = new BLLAssmblly.CS.Member();


        /// <summary>
        /// 系统用户
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();


        /// <summary>
        /// 服务方式 
        /// </summary>
        MemberServiceMethodResult ObjMemberServiceMethodResultBLL = new MemberServiceMethodResult();


        /// <summary>
        /// 客户
        /// </summary>
        Customers ObjCustomersBLL = new Customers();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
                txtSpNode.Enabled = false;
            }

        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {

            lblEmployeeName.Text =ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
            var ObjList=ObjMemberServiceMethodResultBLL.GetByAll();
            foreach (var ObjItem in ObjList)
            {
                ddltype.Items.Add(new ListItem(ObjItem.ServiceName, ObjItem.IsSP.ToString()+","+ObjItem.ServiceId));
            }

       
            //ddltype.DataTextField = "ServiceName";
            //ddltype.DataValueField = "IsSP";

            //ddltype.DataBind();

            
        }


        /// <summary>
        /// 保存服务内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            ObjMemberBLL.Insert(new CS_Member() { CreateDate = DateTime.Now, CreateEmployee = User.Identity.Name.ToInt32(), CustomerID = Request["CustomerID"].ToInt32(),ServiceContent=txtContent.Text,ServiceType=ddltype.SelectedItem.Text,Type=Request["Type"].ToInt32() });
            JavaScriptTools.AlertAndClosefancybox("保存成功!",Page);
        }


        /// <summary>
        /// 去掉开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {

            var ObjModel= ObjMemberServiceMethodResultBLL.GetByID(ddltype.SelectedValue.Split(',')[1].ToInt32());
            if (ObjModel.IsSP)
            {
                txtSpNode.Enabled = true;
                txtSpNode.Text = ObjModel.SpTemplete.Replace("&Name&", ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32()).Bride);

                lblPhone.Visible = true;
                lblPhone.Text = "客户电话";
                lblSp.Visible = true;
                txtPhone.Visible = true;
                txtPhone.Enabled = true;
                lblSp.Text = "短信";
                txtPhone.Text = ObjCustomersBLL.GetByID(Request["CustomerID"].ToInt32()).BrideCellPhone;
            }
            else

            {

                lblPhone.Visible = false;
                lblPhone.Text = "客户电话";
                lblSp.Visible = false;
                txtPhone.Visible = false;
            }
  
        }
    }
}