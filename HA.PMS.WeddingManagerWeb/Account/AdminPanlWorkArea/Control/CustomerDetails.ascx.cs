
/**
 Version :HaoAi22
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.25
 Description:客户详细页面
 History:修改日志

 Author:杨洋
 date:2013.3.25
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
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class CustomerDetails : System.Web.UI.UserControl
    {
        Customers objCustomersBLL = new Customers();
        public int CustomerID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
                //如果等于0话就代表是使用这个控件是从其他页面打开的
                //相反如何不等于0就在当前页面进行使用
                CustomerID = CustomerID == 0 ? Request.QueryString["CustomerID"].ToInt32() : CustomerID;
                
                FL_Customers fL_Customers = objCustomersBLL.GetByID(CustomerID);

                #region 该代码片段是属于新人必填信息
                ltlGroom.Text = fL_Customers.Groom;
                ltlGroomBirthday.Text = fL_Customers.GroomBirthday + string.Empty;
                ltlBride.Text = fL_Customers.Bride + string.Empty;
                ltlBrideBirthday.Text = fL_Customers.BrideBirthday + string.Empty;
                ltlGroomCellPhone.Text = fL_Customers.GroomCellPhone;
                ltlBrideCellPhone.Text = fL_Customers.BrideCellPhone;
                ltlOperator.Text = fL_Customers.Operator;
                ltlOperatorRelationship.Text = fL_Customers.OperatorRelationship;
                ltlOperatorPhone.Text = fL_Customers.OperatorPhone;
                ltlPartyBudget.Text = fL_Customers.PartyBudget + string.Empty;
                ltlFormMarriage.Text = fL_Customers.FormMarriage;
                ltlLikeColor.Text = fL_Customers.LikeColor;
                ltlExpectedAtmosphere.Text = fL_Customers.ExpectedAtmosphere;
                ltlHobbies.Text = fL_Customers.Hobbies;
                ltlNoTaboos.Text = fL_Customers.NoTaboos;
                ltlWeddingServices.Text = fL_Customers.WeddingServices;
                ltlImportantProcess.Text = fL_Customers.ImportantProcess;
                ltlExperience.Text = fL_Customers.Experience;
                ltlDesiredAppearance.Text = fL_Customers.DesiredAppearance;

                #endregion

            
        }
    }
}