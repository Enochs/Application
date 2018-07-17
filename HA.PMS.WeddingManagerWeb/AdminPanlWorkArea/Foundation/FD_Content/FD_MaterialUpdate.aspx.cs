using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class FD_MaterialUpdate : SystemPage
    {
        Material ObjMaterialBLL = new Material();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        private void BinderData()
        {
            tr_td1.Visible = false;
            tr_td2.Visible = false;
            string type = Request.QueryString["type"].ToString();
            if (type == "Update")
            {
                int MaterialId = Convert.ToInt32(Request.QueryString["MaterialId"]);
                FD_Material Material = ObjMaterialBLL.GetByID(MaterialId);
                txtMaterialId.Text = Material.MaterialId.ToString();
                txtMaterialName.Text = Material.MaterialName;
                txtUnitPrice.Text = Material.MaterialUnitPrice.ToString();
                txtRemark.Text = Material.MaterialRemark;
            }
            if (type == "Add")
            {
                tr_td1.Visible = false;
                tr_td2.Visible = false;
            }

        }

        /// <summary>
        /// 点击确定修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfrim_Click(object sender, EventArgs e)
        {
            string type = Request.QueryString["type"].ToString();
            if (type == "Update")
            {
                int MaterialId = Convert.ToInt32(txtMaterialId.Text);
                FD_Material Material = ObjMaterialBLL.GetByID(MaterialId);
                Material.MaterialName = txtMaterialName.Text.ToString();
                Material.MaterialUnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                Material.MaterialRemark = txtRemark.Text.ToString();
                int result = ObjMaterialBLL.Update(Material);
                if (result > 0)
                {
                    JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
                }
                else
                {
                    JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

                }
            }
            if (type == "Add")
            {
                FD_Material Material = new FD_Material();
                Material.MaterialName = txtMaterialName.Text.ToString();
                Material.MaterialUnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
                Material.MaterialRemark = txtRemark.Text.ToString();
                Material.IsDelete = false;
                int result = ObjMaterialBLL.Insert(Material);
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
}