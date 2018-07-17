using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using System.Data.Objects;
using System.IO;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SupplierProductCreate : SystemPage
    {

        Productcs objProductcsBLL = new Productcs();
        Supplier objSupplierBLL = new Supplier();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["find"].ToInt32() != 0)
                {
                    ltlTitle.Text = "查看供应商产品";
                    phcreate.Visible = false;

                }
                int supplierId = Request.QueryString["supplierId"].ToInt32();
                ltlTitle.Text = "录入供应商产品 - " + objSupplierBLL.GetByID(supplierId).Name;
                ListItem firstChoose = new ListItem("请选择", "-1");
                ddlCategory.Items.Add(firstChoose);
                ddlCategory.SelectedIndex = ddlCategory.Items.Count - 1;
                DataDropDownList();


                ddlProject.Items.Add(firstChoose);
                ListItem listtemProject = ddlProject.Items.FindByText("请选择");
                listtemProject.Selected = true;
                DataBinder();

            }
        }


        protected void ClearSave()
        {
            txtSourceProductName.Text = "";

            ddlCategory.SelectedIndex = 0;
            ddlProject.SelectedIndex = 0;
            txtSpecifications.Text = "";

            txtPurchasePrice.Text = "";
            txtSaleOrice.Text = "";
            txtRemark.Text = "";
            txtUnit.Text = "";
            txtExplain.Text = "";
        }
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
        }
        protected void DataBinder()
        {
            #region 分页页码
            int startIndex = StorePager.StartRecordIndex;
            int resourceCount = 0;

            int supplierId = Request.QueryString["supplierId"].ToInt32();
            var listResultContent = objProductcsBLL.GetByIndex(supplierId, StorePager.PageSize, StorePager.CurrentPageIndex, out resourceCount);
            StorePager.RecordCount = resourceCount;

            rptStorehouse.DataSource = listResultContent;
            rptStorehouse.DataBind();

            #endregion
        }

        protected void StorePager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }


        /// <summary>
        /// 保存供应商产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int supplierId = Request.QueryString["supplierId"].ToInt32();

            FD_Product fd_Product = new FD_Product()
            {
                ProductName = txtSourceProductName.Text,
                SupplierID = supplierId,
                CategoryID = ddlCategory.SelectedValue.ToInt32(),
                ProductProject = ddlProject.SelectedValue.ToInt32(),
                ProductPrice = txtPurchasePrice.Text.ToInt32(),
                SalePrice = txtSaleOrice.Text.ToInt32(),
                IsDelete = false,
                Specifications = txtSpecifications.Text,
                Unit = txtUnit.Text,
                Explain = txtExplain.Text,
                Remark = txtRemark.Text,
                
                Count=0

            };
            string savaPath = "~/Files/Supplier/";
            string fileExt = "";
            bool isOk = true;
            if (flieUp.HasFile)
            {

                fileExt = System.IO.Path.GetExtension(flieUp.FileName).ToLower();
                if (fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp")
                {
                    fd_Product.Data = savaPath + DateTime.Now.ToFileTimeUtc() + fileExt;
                    if (!Directory.Exists(Server.MapPath(savaPath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(savaPath));
                    }
                    flieUp.SaveAs(Server.MapPath(fd_Product.Data));
                }
                else
                {
                    isOk = false;
                    JavaScriptTools.AlertWindow("请你上传的图片只能是jpeg png jpg gif bmp", this.Page);

                }


            }
            if (ddlProject.Items.Count == 0)
            {
                isOk = false;
                JavaScriptTools.AlertWindow("项目不能为空", this.Page);
            }

            if (isOk)
            {
                int result = objProductcsBLL.Insert(fd_Product);
                //根据返回判断添加的状态
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("添加成功", this.Page);
                    DataBinder();
                    ClearSave();
                }
                else
                {
                    JavaScriptTools.AlertWindow("添加失败,请重新尝试", this.Page);

                }
            }




        }
        /// <summary>
        /// 图片现实
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetDataDisPlay(object source)
        {
            if (source.ToString().Length > 0)
            {
                //~/Files/Storehouse/hhhhf.jpg
                string filePath = "../../.." + source.ToString().ToLower().Replace("~", "");
                string fileExt = Path.GetExtension(filePath);
                if (fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp")
                {
                    return "<a class='grouped_elements'   href='#' rel='group1'><img style='width:100px; height:70px;' alt='' src='" + filePath + "' /> </a>";
                }


            }
            return "";
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
            FileInfo fileInfo = new FileInfo(path);

            //防止中文出现乱码
            string filename = HttpUtility.UrlEncode(fileInfo.Name);
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.WriteFile(path);


        }


        protected void rptStorehouse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FD_Product storehouse = e.Item.DataItem as FD_Product;
            if (storehouse != null)
            {
                LinkButton linkButton = (LinkButton)e.Item.FindControl("lkbtnDownLoad");
                Image imgStore = (Image)e.Item.FindControl("imgStore");
                if (storehouse.Data != null)
                {


                    string filePath = "../../.." + storehouse.Data.ToLower().Replace("~", "");
                    string fileExt = Path.GetExtension(filePath);

                    if (fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp")
                    {
                        imgStore.Visible = true;
                        linkButton.Visible = false;
                    }
                    else
                    {
                        linkButton.Text = Path.GetFileName(storehouse.Data);
                        imgStore.Visible = false;
                        linkButton.Visible = true;
                    }
                }
                else
                {
                    imgStore.Visible = false;
                    linkButton.Visible = false;
                }
            }
        }

        protected void rptStorehouse_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DownLoad")
            {
                string filePath = objProductcsBLL.GetByID((e.CommandArgument + string.Empty)
                .ToInt32()).Data;
                filePath = Server.MapPath(filePath);
                DownLoad(filePath);
            }
            else
            {
                FD_Product fd = objProductcsBLL.GetByID((e.CommandArgument + string.Empty).ToInt32());
                if (File.Exists(Server.MapPath(fd.Data)))
                {
                    File.Delete(Server.MapPath(fd.Data));
                } 
                int result = objProductcsBLL.Delete(fd);

                //根据返回判断添加的状态
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("删除成功", this.Page);
                    DataBinder();

                }
                else
                {
                    JavaScriptTools.AlertWindow("删除失败,请重新尝试", this.Page);

                }
            }
        }
    }
}