/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.14
 Description:客户基本信息修改页面
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
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing
{
    public partial class FD_TelemarketingUpdate : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                //获取从客户管理页面中传递过来的参数
                int CustomerID = Request.QueryString["CustomerID"].ToInt32();
                FL_Customers fl_Customers = objCustomersBLL.GetByID(CustomerID);
                txtGroom.Text = fl_Customers.Groom;
                txtGroomBirthday.Text = fl_Customers.GroomBirthday.ToString();
                txtBride.Text = fl_Customers.Bride;
                txtBrideirthday.Text = fl_Customers.BrideBirthday.ToString();
                txtGroomCellPhone.Text = fl_Customers.GroomCellPhone;
                txtBrideCellPhone.Text = fl_Customers.BrideCellPhone;
                txtOperator.Text = fl_Customers.Operator;
                txtOperatorRelationship.Text = fl_Customers.OperatorRelationship;
                txtOperatorPhone.Text = fl_Customers.OperatorPhone;
                txtPartyBudget.Text = fl_Customers.PartyBudget.ToString();
                txtFormMarriage.Text = fl_Customers.FormMarriage;
                txtLikeColor.Text = fl_Customers.LikeColor;
                txtExpectedAtmosphere.Text = fl_Customers.ExpectedAtmosphere;
                txtHobbies.Text = fl_Customers.Hobbies;
                txtNoTaboos.Text = fl_Customers.NoTaboos;
                txtWeddingServices.Text = fl_Customers.WeddingServices;
                txtImportantProcess.Text = fl_Customers.ImportantProcess;
                txtExperience.Text = fl_Customers.Experience;
                txtDesiredAppearance.Text = fl_Customers.DesiredAppearance;
            }
        }
        /// <summary>
        /// 修改某个客户的基本信息操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnTelemark_Click(object sender, EventArgs e)
        {
            int CustomerID = Request.QueryString["CustomerID"].ToInt32();
            //先查出对应的实体类对象，然后在该实体类上面进行赋值修改操作
            FL_Customers fl_Customers = objCustomersBLL.GetByID(CustomerID);
            fl_Customers.Groom = txtGroom.Text;
            fl_Customers.GroomBirthday = txtGroomBirthday.Text.ToDateTime();

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
            fl_Customers.DesiredAppearance = txtDesiredAppearance.Text;
            fl_Customers.IsDelete = false;
            int result = objCustomersBLL.Update(fl_Customers);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

            }
        }
    }
}