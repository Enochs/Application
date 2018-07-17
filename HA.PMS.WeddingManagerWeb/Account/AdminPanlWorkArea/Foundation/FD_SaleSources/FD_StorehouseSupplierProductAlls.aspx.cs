/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.4.7
 Description:库房及供应商虚拟总产品表界面
 History:修改日志

 Author:杨洋
 date:2013.4.7
 version:好爱1.0
 description:修改描述
 
 
 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_StorehouseSupplierProductAlls : HA.PMS.Pages.SystemPage
    {
        Category objCategoryBLL = new Category();
        CelebrationPackageProduct objCelebrationPackageProductBLL = new CelebrationPackageProduct();
        AllProducts objAllProductBLL = new AllProducts();
        BLLAssmblly.FD.StorehouseSupplierProductAll objStorehouseSupplierProductAllBLL = new BLLAssmblly.FD.StorehouseSupplierProductAll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int PackageID = Request.QueryString["PackageID"].ToInt32();
                if (PackageID == 0)
                {
                    Response.Write("请你先保存新增套系内容！！！");
                    Response.End();
                }
                else
                {
                    ddlCategory.Items.Add(new ListItem("请选择", "-1"));
                    ddlCategory.ClearSelection();
                    ddlCategory.Items[ddlCategory.Items.Count - 1].Selected = true;
                    DataDropDownList();
                    DataBinder();
                }

            }
        }

        protected void DataDropDownList()
        {
            ddlProject.Items.Clear();
            ddlProject.ParentID = ddlCategory.SelectedValue.ToInt32();
            ddlProject.BinderByparent();
            ddlProject.ClearSelection();
            ddlProject.Items.Add(new ListItem("请选择", "-1"));
            ddlProject.Items[ddlProject.Items.Count - 1].Selected = true;
        }

        protected void DataBinder()
        {
            List<ObjectParameter> parameters = new List<ObjectParameter>();

            //产品名称
            parameters.Add(!string.IsNullOrWhiteSpace(txtProductName.Text), "ProductName_LIKE", txtProductName.Text.Trim());
            //类别
            parameters.Add(ddlCategory.SelectedValue.ToInt32() > 0, "ProductCategory", ddlCategory.SelectedValue.ToInt32());
            //项目
            parameters.Add(ddlProject.SelectedValue.ToInt32() > 0, "ProjectCategory", ddlProject.SelectedValue.ToInt32());
            //供应商
            parameters.Add(!string.IsNullOrWhiteSpace(txtSupplierName.Text), "SupplierName_LIKE", txtSupplierName.Text.Trim());
            //绑定
            rptMain.DataBind(new AllProducts().GetbyAllProductsParameter(parameters.ToArray()));
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataDropDownList();
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int PackageID = Request.QueryString["PackageID"].ToInt32();
            List<int> keysList = new List<int>();
            //
            for (int i = 0; i < rptMain.Items.Count; i++)
            {
                CheckBox ch = rptMain.Items[i].FindControl("ckSinger") as CheckBox;
                if (ch != null)
                {
                    if (ch.Checked)
                    {
                        keysList.Add(ch.ToolTip.ToInt32());
                    }

                }
            }
            if (keysList.Count == 0)
            {
                JavaScriptTools.AlertWindow("请你选择添加的产品", this.Page);
            }
            else
            {
                if (keysList.Count > 5)
                {
                    JavaScriptTools.AlertWindow("一个套系里面只能选择5个产品搭配", this.Page);

                }
                else
                {
                    int thisPackage = objCelebrationPackageProductBLL.GetByAll().Where(C => C.PackageID == PackageID).Count();
                    if ((thisPackage + keysList.Count) <= 5)
                    {
                        FD_CelebrationPackageProduct product = new FD_CelebrationPackageProduct();
                        for (int i = 0; i < keysList.Count; i++)
                        {
                            product.PackageID = PackageID;
                            product.AllProductKeys = keysList[i];
                            product.CreateDatetime = DateTime.Now;
                            objCelebrationPackageProductBLL.Insert(product);

                        }

                        JavaScriptTools.AlertAndClosefancyboxNoRenovate("选择成功", this.Page);
                    }
                    else
                    {

                        string errorPrompt = string.Format("你之前已经选择了\n{0}\n个产品搭档，只剩下\n{1}\n个供选择",
                        thisPackage, (5 - thisPackage));
                        JavaScriptTools.AlertWindow(errorPrompt, this.Page);
                    }



                }
            }
        }
    }
}