using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly;
using HA.PMS.ToolsLibrary;
using System.Web.UI.HtmlControls;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class SelectFourGuardian : System.Web.UI.UserControl
    {
        //四大金刚
        FourGuardian ObjFourGuardianBLL = new FourGuardian();

        GuardianType ObjGuardianTypeBLL = new GuardianType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.hideControlKey.Value = Request["ControlKey"];
                this.hideParentKey.Value = Request["ParentControl"];
                if (Request["SetEmployeeName"] != null)
                {
                    hideSetEmployeeName.Value = Request["SetEmployeeName"];
                }
                else
                {
                    hideSetEmployeeName.Value = "txtEmpLoyeeName";

                }
                BinderData();
            }
        }



        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            this.repType.DataSource = ObjGuardianTypeBLL.GetByAll();
            this.repType.DataBind();
        }


        /// <summary>
        /// 点击绑定四大境内各
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "lnkbtnSelect")
            {
                this.repContent.DataSource = ObjFourGuardianBLL.GetByType(e.CommandArgument.ToString().ToInt32());
                this.repContent.DataBind();
            }
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {
            //string KeyIdList = string.Empty;
            //for (int i = 0; i < repContent.Items.Count; i++)
            //{
            //    HtmlInputRadioButton ObjChecj = (HtmlInputRadioButton)repContent.Items[i].FindControl("chkContent");
            //    if (ObjChecj.Checked)
            //    {
            //        KeyIdList += ObjChecj.Value + ",";
            //    }
            //}
            //KeyIdList = KeyIdList.Trim(',');
            //JavaScriptTools.SetValueByParentControl(Request["ControlKey"], KeyIdList, this.Page);
        }
    }
}