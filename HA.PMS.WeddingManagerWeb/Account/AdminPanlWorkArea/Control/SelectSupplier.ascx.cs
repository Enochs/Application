using HA.PMS.BLLAssmblly.FD;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class SelectSupplier : System.Web.UI.UserControl
    {
        Supplier ObjSupplierBLL = new Supplier();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //供应商类型
                SupplierType ObjSupplierTypeBLL = new SupplierType();
                this.lstTyperBox.DataSource = ObjSupplierTypeBLL.GetByAll();
                lstTyperBox.DataTextField = "TypeName";
                lstTyperBox.DataValueField = "SupplierTypeId";

                this.lstTyperBox.DataBind();


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

            }


        }


        /// <summary>
        /// 绑定供应商
        /// </summary>
        private void BinderData(int typerID)
        {
            var DataSource = ObjSupplierBLL.GetByAll();
            DataSource.Add(new DataAssmblly.FD_Supplier() { Name = "库房" });
            this.repSupplier.DataSource = DataSource.Where(C => C.CategoryID == int.Parse(lstTyperBox.SelectedValue));
            this.repSupplier.DataBind();
        }

        protected void btnSaveSelect_Click(object sender, EventArgs e)
        {

        }

        protected void lstTyperBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinderData(int.Parse(this.lstTyperBox.SelectedValue));
        }

    }
    
}