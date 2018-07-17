using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Anniversary
{
    public partial class AnniversaryShow : System.Web.UI.Page
    {
        HA.PMS.BLLAssmblly.CS.Member ObjMemberBLL = new BLLAssmblly.CS.Member();

        Employee ObjEmployeeBLL = new Employee();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            var ObjMemberModel = ObjMemberBLL.GetByID(Request["MemberID"].ToInt32());

            lblContent.Text = ObjMemberModel.ServiceContent;
            lblEmployeeName.Text = ObjEmployeeBLL.GetByID(ObjMemberModel.CreateEmployee).EmployeeName;
            lblType.Text = ObjMemberModel.ServiceType;


        }
    }
}