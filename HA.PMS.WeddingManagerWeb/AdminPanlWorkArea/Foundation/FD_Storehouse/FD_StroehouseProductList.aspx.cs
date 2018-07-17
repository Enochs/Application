using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.IO;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StroehouseProductList : SystemPage
    {
        StorehouseSourceProduct objStorehouseSourceProductBLL = new StorehouseSourceProduct();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCategorySerch.ClearSelection();
                ddlCategorySerch.Items.Add(new ListItem("请选择", "-1"));
                ddlCategorySerch.Items[ddlCategorySerch.Items.Count - 1].Selected = true;
                DataDropDownList(ddlProjectSerch, ddlCategorySerch.SelectedValue.ToInt32(), true);

               

                BinderData(sender, e);
            }
            else
            {
                // Page.ClientScript.RegisterStartupScript(Page.GetType(), "bb", "alert('b')", true);

            }
        }
 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ddl">要绑定的控件</param>
        /// <param name="ParentID"></param>
        /// <param name="IsAddSelectAll">是否添加“请选择”项到最后并选择。</param>
        protected void DataDropDownList(EditoerLibrary.CategoryDropDownList ddl, int ParentID, bool IsAddSelectAll)
        {
            ddl.Items.Clear();
            ddl.ParentID = ParentID;
            ddl.BinderByparent();
            if (IsAddSelectAll)
            {
                ddl.ClearSelection();
                ddl.Items.Add(new ListItem("请选择", "-1"));
                ddl.Items[ddl.Items.Count - 1].Selected = true;
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {
            var objParmList = new List<PMSParameters>();
            objParmList.Add(ddlProjectSerch.SelectedValue.ToInt32() > 0, "Type", 2);
            objParmList.Add(!string.IsNullOrWhiteSpace(txtProductName.Text), "ProductName", txtProductName.Text.Trim(), NSqlTypes.LIKE);
            objParmList.Add(ddlCategorySerch.SelectedValue.ToInt32() > 0, "ProductCategory", ddlCategorySerch.SelectedValue.ToInt32());
            objParmList.Add(ddlProjectSerch.SelectedValue.ToInt32() > 0, "ProjectCategory", ddlProjectSerch.SelectedValue.ToInt32());

            if (Request["Vtype"].ToInt32() > 0)
            {
                objParmList.Add("VirtualType", 9);
            }
            else
            {
                objParmList.Add("VirtualType", "nulls", NSqlTypes.IsNull);
            }

            StorePager.PageSize = 8;
            int resourceCount = 0;
            var query = objStorehouseSourceProductBLL.GetByWhereParameter(objParmList, "Keys", StorePager.PageSize, StorePager.CurrentPageIndex, out resourceCount);
            StorePager.RecordCount = resourceCount;
            rptStorehouse.DataBind(query);


            
        }
 

        /// <summary>
        /// 显示图片
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetDataDisPlay(object source)
        {
            if (source.ToString().Length > 0)
            {

                string filePath = "../../.." + source.ToString().ToLower().Replace("~", "");
                string fileExt = Path.GetExtension(filePath);
                if (fileExt == ".jpeg" || fileExt == ".png" || fileExt == ".jpg" || fileExt == ".gif" || fileExt == ".bmp")
                {
                    return "<a class='grouped_elements'   href='#' rel='group1'><img style='width:100px; height:70px;' alt='' src='" + filePath + "' /> </a>";
                }


            }
            return "";
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

            Response.End();
        }

        protected void rptStorehouse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

 

        }

        protected void rptStorehouse_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DownLoad")
            {
                string filePath = objStorehouseSourceProductBLL.GetByID((e.CommandArgument + string.Empty)
                .ToInt32()).Data;
                filePath = Server.MapPath(filePath);
                DownLoad(filePath);
            }
            else
            {
                FD_StorehouseSourceProduct fd = objStorehouseSourceProductBLL.GetByID((e.CommandArgument + string.Empty).ToInt32());

                if (File.Exists(Server.MapPath(fd.Data)))
                {
                    File.Delete(Server.MapPath(fd.Data));
                }
                int result = objStorehouseSourceProductBLL.Delete(fd);

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
            }
        }


    

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            StorePager.CurrentPageIndex = 1;
            BinderData(sender, e);
        }


        /// <summary>
        /// 根据类别绑定项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlCategorySerch_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataDropDownList(ddlProjectSerch, ddlCategorySerch.SelectedValue.ToInt32(), true);
        }

        protected void txtProductName_TextChanged(object sender, EventArgs e)
        {

        }

        protected string GetIsDisposibleStatu(object IsDisposible)
        {
            string temp = string.Empty;
            if (IsDisposible != null)
            {
                temp = IsDisposible.ToString().ToLower();
                if (temp.Equals("true"))
                { return "是"; }
                else { return "否"; }
            }
            else { return string.Empty; }
        }

    }
}