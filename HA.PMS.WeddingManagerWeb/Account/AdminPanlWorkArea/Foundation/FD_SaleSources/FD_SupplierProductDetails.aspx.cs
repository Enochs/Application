using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SupplierProductDetails : HA.PMS.Pages.SystemPage
    {
        string[] exts = { ".jpeg", ".png", ".jpg", ".gif", ".bmp" };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ListItem firstChoose = new ListItem("请选择", "-1");
                ddlCategory.Items.Add(firstChoose);
                ddlCategory.SelectedIndex = ddlCategory.Items.Count - 1;
                DataDropDownList();

                ListItem listtemProject = ddlProject.Items.FindByText("请选择");
                listtemProject.Selected = true;
                BinderData(sender, e);
            }
           
        }

        /// <summary>
        /// 根据产品ID返回所属供应商
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetSupplierNameByProductID(object ProductID)
        {
            return new Supplier().GetByID(new Productcs().GetByID((ProductID + string.Empty).ToInt32()).SupplierID).Name;
        }


        /// <summary>
        /// 绑定页面下拉框
        /// </summary>
        protected void DataDropDownList()
        {
            if (ddlCategory.SelectedValue == "-1")
            {
                if (ddlCategory.SelectedIndex > 0)
                {
                    ddlProject.ParentID = ddlCategory.Items[1].Value.ToInt32();
                    ddlProject.BinderByparent();
                }
            }
            else
            {
                ddlProject.ParentID = ddlCategory.SelectedValue.ToInt32();
                ddlProject.BinderByparent();
            }
            ListItem firstChoose = new ListItem("请选择", "-1");
            ddlProject.Items.Add(firstChoose);
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {
            FD_SupplierProductQuery Suppliers = new FD_SupplierProductQuery();
            Suppliers.Name = txtSupplierName.Text.Trim();
            Suppliers.CategoryID = ddlCategory.SelectedValue.ToInt32();
            Suppliers.ProductProject = ddlProject.SelectedValue.ToInt32();
            Suppliers.ProductName = txtProductName.Text.Trim();


            List<ObjectParameter> paramsList = new List<ObjectParameter>();
            paramsList.Add(!string.IsNullOrEmpty(txtProductName.Text),"ProductName_LIKE", Suppliers.ProductName);
            paramsList.Add(!string.IsNullOrEmpty(txtSupplierName.Text), "Name_LIKE", Suppliers.Name);
            paramsList.Add(ddlCategory.SelectedValue.ToInt32() > 0, "Expr1", Suppliers.CategoryID);
            paramsList.Add(ddlCategory.SelectedValue.ToInt32() > 0, "ProductProject", Suppliers.ProductProject);
            
            int startIndex = StorePager.StartRecordIndex;
            int resourceCount = 0;
            var query = new Productcs().GetSupplierProductByParameterWithoutUnSignIn(paramsList.ToArray(), StorePager.PageSize, StorePager.CurrentPageIndex, out resourceCount);
            StorePager.RecordCount = resourceCount;
            rptStorehouse.DataBind(query);
        }


        protected string GetDataDisPlay(object source)
        {
            string filePath = "../../.." + source.ToString().ToLower().Replace("~", "");
            if (exts.Contains(Path.GetExtension(filePath)))
            {
                return "<a class='grouped_elements'   href='#' rel='group1'><img style='width:100px; height:70px;' alt='' src='" + filePath + "' /> </a>";
            }
            return string.Empty;
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataDropDownList();
        }
        /// <summary>
        /// 单个文件进行下载
        /// </summary>
        /// <param name="path"></param>
        /// <param name="context"></param>
        public void DownLoad(string path)
        {
            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(System.IO.Path.GetFileNameWithoutExtension(path)));
            Response.WriteFile(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptStorehouse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FD_SupplierProductQuery Suppliers = e.Item.DataItem as FD_SupplierProductQuery;
            LinkButton linkButton = (LinkButton)e.Item.FindControl("lkbtnDownLoad");
            Image imgStore = (Image)e.Item.FindControl("imgStore");
            if (Suppliers.Data != null)
            {
                string filePath = "../../.." + Suppliers.Data.ToLower().Replace("~", "");

                if (exts.Contains( Path.GetExtension(filePath)))
                {
                    imgStore.Visible = true;
                    linkButton.Visible = false;
                }
                else
                {
                    linkButton.Text = Path.GetFileName(Suppliers.Data);
                    imgStore.Visible = false;
                    linkButton.Visible = true;
                }
            }
            else
            {
                linkButton.Visible = false;
                imgStore.Visible = false;
            }
        }

        protected void rptStorehouse_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //Delete
            switch (e.CommandName)
            {
                case "DownLoad":
                    string filePath = new Productcs().GetByID((e.CommandArgument + string.Empty).ToInt32()).Data;
                    filePath = Server.MapPath(filePath);
                    DownLoad(filePath);
                    break;
                case "Delete":
                    FD_Product fd = new FD_Product();
                    fd.ProductID = (e.CommandArgument + string.Empty).ToInt32();
                    int result = new Productcs().Delete(fd);

                    //根据返回判断添加的状态
                    if (result > 0)
                    {
                        JavaScriptTools.AlertWindow("删除成功", this.Page);
                        BinderData(source, e);
                    }
                    else
                    {
                        JavaScriptTools.AlertWindow("删除失败,请重新尝试", this.Page);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取产品使用个数
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public int GetUsedCount(object ProductID)
        {
            return new AllProducts().GetUsedTimes(Convert.ToInt32(ProductID), 1);
        }
    }
}