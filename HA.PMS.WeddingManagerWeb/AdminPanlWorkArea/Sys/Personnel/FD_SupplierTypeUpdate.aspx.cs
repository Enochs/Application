using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class FD_SupplierTypeUpdate : SystemPage
    {

        SupplierType objSupplierTypeBLL = new SupplierType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FD_SupplierType supplierType = objSupplierTypeBLL.GetByID(Request.QueryString["typeId"].ToInt32());
                txtSupplieType.Text = supplierType.TypeName;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            FD_SupplierType fd= objSupplierTypeBLL.GetByID(Request.QueryString["typeId"].ToInt32());
            fd.TypeName = txtSupplieType.Text;
            fd.IsDelete = false;
            //创建部门信息，填入数据库中
            int result = objSupplierTypeBLL.Update(fd);
            //部门添加之后标识
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