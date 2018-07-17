/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.14
 Description:客户基本信息创建管理页面
 History:修改日志
 （客户电话营销）
 Author:杨洋
 date:2013.3.14
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
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Emnus;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing
{

    public partial class FD_TelemarketingCreate : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        ChannelType objChannelType = new ChannelType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                DataBinder();
            }
        }
        /// <summary>
        /// 这个方法主要绑定渠道类型下拉框
        /// </summary>
        public void DataBinder()
        {
            ddltChannelType.DataSource = objChannelType.GetByAll();
            ddltChannelType.DataValueField = "ChannelTypeId";
            ddltChannelType.DataTextField = "ChannelTypeName";
            ddltChannelType.DataBind();
        }
        protected void btnTelemark_Click(object sender, EventArgs e)
        {
            //构建实体类对象
            FL_Customers fl_Customers = new FL_Customers();
            fl_Customers.Groom = txtGroom.Text;
            fl_Customers.GroomBirthday = txtGroomBirthday.Text.ToDateTime();
            fl_Customers.ChannelType = ddltChannelType.SelectedValue.ToInt32();
            fl_Customers.Channel = ddltChannelType.Text;
            fl_Customers.Bride = txtBride.Text;
            fl_Customers.BrideBirthday = txtBrideirthday.Text.ToDateTime();
            fl_Customers.GroomCellPhone = txtGroomCellPhone.Text;
            fl_Customers.BrideCellPhone = txtBrideCellPhone.Text;
            fl_Customers.Operator = txtOperator.Text;
            fl_Customers.OperatorRelationship = txtOperatorRelationship.Text;
            fl_Customers.OperatorPhone = txtOperatorPhone.Text;
            fl_Customers.PartyBudget = txtPartyBudget.Text.ToDecimal();
            fl_Customers.FormMarriage = txtFormMarriage.Text;
            fl_Customers.LikeColor = txtLikeColor.Text;
            fl_Customers.ExpectedAtmosphere = txtExpectedAtmosphere.Text;
            fl_Customers.Hobbies = txtHobbies.Text;
            fl_Customers.NoTaboos = txtNoTaboos.Text;
            fl_Customers.WeddingServices = txtWeddingServices.Text;
            fl_Customers.ImportantProcess = txtImportantProcess.Text;
            fl_Customers.Experience = txtExperience.Text;
            fl_Customers.IsDelete = false;
            fl_Customers.DesiredAppearance = txtDesiredAppearance.Text;
            fl_Customers.RecorderDate = DateTime.Now;
            fl_Customers.Recorder = User.Identity.Name.ToInt32();
            fl_Customers.IsLose = false;
            //这个参数是通过新人搜索中没有找到该新人，打开本页面此时的新人状态为邀约成功

            if (!string.IsNullOrEmpty(Request.QueryString["InviteSucess"]))
            {
                fl_Customers.State = Request.QueryString["InviteSucess"].ToInt32();
            }
            else
            {
                fl_Customers.State = (int)CustomerStates.New;
            }
            int result = objCustomersBLL.Insert(fl_Customers);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("添加失败,请重新尝试", this.Page);

            }
        }
    }
}