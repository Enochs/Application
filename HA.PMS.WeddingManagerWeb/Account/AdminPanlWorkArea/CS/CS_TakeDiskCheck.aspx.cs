using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.EditoerLibrary;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS
{
    public partial class CS_TakeDiskCheck : System.Web.UI.Page
    {
        TakeDisk ObjTakeDiskBLL = new TakeDisk();
        TakeDiskContent ObjContentBLL = new TakeDiskContent();
        int TakeID = 0;
        #region 页面初始化
        /// <summary>
        /// 初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            TakeID = Request["TakeID"].ToInt32();
            if (!IsPostBack)
            {
                ddlCheckState.Attributes.Add("onchange", "selectIndexChanged()");
                BinderData();
                if (Request["Type"] == "Look")
                {
                    tblChecks.Visible = false;
                }
            }
        }
        #endregion

        #region 数据加载
        public void BinderData()
        {
            var DataList = ObjContentBLL.GetByTakeID(TakeID);
            rptContent.DataBind(DataList);
        }
        #endregion

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (ddlCheckState.SelectedValue.ToInt32() == 1)
            {
                var Model = ObjTakeDiskBLL.GetByID(TakeID);
                Model.State = 1;
                ObjContentBLL.Insert(new CS_TakeDiskContent
                {
                    Status = ddlCheckState.SelectedItem.Text,
                    Content = txtReason.Text.ToString(),
                    CustomerID = Model.CustomerID,
                    TakeID = Model.TakeID,
                    CreateDate = DateTime.Now.ToShortDateString().ToDateTime(),
                    CreateEmployee = User.Identity.Name.ToInt32()
                });
                int result = ObjTakeDiskBLL.Update(Model);
                if (result > 0)
                {
                    //Response.Redirect("CS_TakeforNoneNotice.aspx?NeedPopu=1");
                    JavaScriptTools.AlertWindowAndLocation("修改成功", "CS_TakeforNoneNotice.aspx?NeedPopu=1", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindow("修改失败,请稍候再试...", Page);
                }
            }
            else if (ddlCheckState.SelectedValue.ToInt32() == 2)
            {
                var Model = ObjTakeDiskBLL.GetByID(TakeID);
                int result = ObjContentBLL.Insert(new CS_TakeDiskContent
                {
                    Status = ddlCheckState.SelectedItem.Text,
                    Content = txtReason.Text.ToString(),
                    CustomerID = Model.CustomerID,
                    TakeID = Model.TakeID,
                    CreateDate = DateTime.Now.ToShortDateString().ToDateTime(),
                    CreateEmployee = User.Identity.Name.ToInt32()
                });
                if (result > 0)
                {
                    Model.SecondEmployee = User.Identity.Name.ToInt32();
                    ObjTakeDiskBLL.Update(Model);
                    //Response.Redirect("CS_TakeDiskCheck.aspx?NeedPopu=1");
                    JavaScriptTools.AlertWindowAndLocation("修改成功", "CS_TakeDiskManagerTswkTake.aspx?NeedPopu=1", Page);
                }
            }
            BinderData();
        }

        #region 根据ID获取员工姓名

        public string GetEmployeeName(object Source)
        {
            int EmployeeID = Source.ToString().ToInt32();
            Employee ObjEmployeeBLL = new Employee();
            return ObjEmployeeBLL.GetByID(EmployeeID).EmployeeName;
        }
        #endregion

    }
}