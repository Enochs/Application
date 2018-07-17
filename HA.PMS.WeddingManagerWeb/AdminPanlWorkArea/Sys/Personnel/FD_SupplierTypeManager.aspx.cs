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
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel
{
    public partial class FD_SupplierTypeManager : SystemPage
    {
        SupplierType objSupplierTypeBLL = new SupplierType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        protected void DataBinder()
        {


            int startIndex = SupplierTypePager.StartRecordIndex;
            int resourceCount = 0;

            var query = objSupplierTypeBLL.GetByIndex(SupplierTypePager.PageSize, SupplierTypePager.CurrentPageIndex, out resourceCount);
            SupplierTypePager.RecordCount = resourceCount;
            //加载部门信息
            rptSupplierType.DataSource = query;
            rptSupplierType.DataBind();

        }
        protected void SupplierTypePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void rptSupplierType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

            if (e.CommandName == "Delete")
            {

                int SupplierTypeId = e.CommandArgument.ToString().ToInt32();
                FD_SupplierType fd = new FD_SupplierType();
                fd.SupplierTypeId = SupplierTypeId;

                objSupplierTypeBLL.Delete(fd);
                //删除之后重新绑定数据源
                DataBinder();


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            JavaScriptTools.AlertWindowAndReaload("退出成功", Page);
        }
    }
}