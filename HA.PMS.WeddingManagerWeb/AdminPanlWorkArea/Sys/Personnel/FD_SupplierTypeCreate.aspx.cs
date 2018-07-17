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
    public partial class FD_SupplierTypeCreate : SystemPage
    {
        SupplierType objSupplierTypeBLL = new SupplierType();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            FD_SupplierType fd = new FD_SupplierType();
            fd.TypeName = txtSupplieType.Text;
            fd.IsDelete = false;
            //创建部门信息，填入数据库中
            int result = objSupplierTypeBLL.Insert(fd);
            //部门添加之后标识
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