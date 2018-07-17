using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class FD_QuotedCatgoryProductManager : SystemPage
    {
        StorehouseSourceProduct objStorehouseSourceProductBLL = new StorehouseSourceProduct();
        QuotedCatgory ObjQuotedCatgoryBLL = new QuotedCatgory();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataDropDownList();

                ddlParentCatogry.ClearSelection();
                ddlParentCatogry.Items.Add(new ListItem("请选择", "-1"));
                ddlParentCatogry.Items[ddlParentCatogry.Items.Count - 1].Selected = true;

                BinderData(sender, e);
            }
            else
            {
                // Page.ClientScript.RegisterStartupScript(Page.GetType(), "bb", "alert('b')", true);

            }
        }




        /// <summary>
        /// 绑定父极
        /// </summary>
        protected void DataDropDownList()
        {
            ddlParentCatogry.DataSource = ObjQuotedCatgoryBLL.GetByParentID(0);
            ddlParentCatogry.DataTextField = "Title";
            ddlParentCatogry.DataValueField = "QcKey";
            ddlParentCatogry.DataBind();
        }

        /// <summary>
        /// 根据父极绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlParentCatogry_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSecondCatgory.DataSource = ObjQuotedCatgoryBLL.GetByParentID(ddlParentCatogry.SelectedValue.ToInt32());

            ddlSecondCatgory.DataTextField = "Title";
            ddlSecondCatgory.DataValueField = "QcKey";
            ddlSecondCatgory.DataBind();
            ddlSecondCatgory.Items.Add(new ListItem { Text = "请选择", Value = "0" });
            ddlSecondCatgory.Items[ddlSecondCatgory.Items.Count - 1].Selected = true;

        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {
            var objParmList = new List<PMSParameters>();
            objParmList.Add(ddlParentCatogry.SelectedValue.ToInt32() > 0, "Type", 2);
            objParmList.Add(!string.IsNullOrWhiteSpace(txtProductName.Text), "ProductName", txtProductName.Text.Trim(), NSqlTypes.LIKE);
            objParmList.Add(ddlParentCatogry.SelectedValue.ToInt32() > 0, "ProductCategory", ddlParentCatogry.SelectedValue.ToInt32());
            if (ddlSecondCatgory.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("ProjectCategory", ddlSecondCatgory.SelectedValue.ToInt32());
            }
            else if (ddlSecondCatgory.SelectedValue.ToInt32() == 0)
            {
                if (ddlParentCatogry.SelectedValue.ToInt32() > 0)
                {
                    var DataList = ObjQuotedCatgoryBLL.GetByParentID(ddlParentCatogry.SelectedValue.ToInt32());
                    string ProCategory = "";
                    int index = 1;
                    foreach (var item in DataList)
                    {
                        if (index == DataList.Count)
                        {
                            ProCategory += item.QCKey;
                        }
                        else
                        {
                            ProCategory += item.QCKey + ",";
                        }
                        index++;
                    }
                    objParmList.Add("ProjectCategory", ProCategory, NSqlTypes.IN);
                }
            }

            if (Request["Vtype"].ToInt32() > 0)
            {
                objParmList.Add("VirtualType", 9);
            }
            else
            {
                objParmList.Add("VirtualType", null, NSqlTypes.IsNull);

            }


            int resourceCount = 0;
            var query = objStorehouseSourceProductBLL.GetByWhereParameter(objParmList, "Keys", StorePager.PageSize, StorePager.CurrentPageIndex, out resourceCount);
            StorePager.RecordCount = resourceCount;
            lblProductSum.Text = "本页" + StorePager.PageSize + "条数据 总共" + resourceCount.ToString() + "条数据";
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

            FD_StorehouseSourceProduct storehouse = objStorehouseSourceProductBLL.GetByID((e.Item.DataItem as FD_AllProducts).KindID);
            if (storehouse != null)
            {
                LinkButton linkButton = (LinkButton)e.Item.FindControl("lkbtnDownLoad");
                Image imgStore = (Image)e.Item.FindControl("imgStore");
                if (!string.IsNullOrEmpty(storehouse.Data))
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


        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/ProductList.xml"));

            string ObjTempletContent = Objreader.ReadToEnd();
            System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();
            Objreader.Close();
            var ItemList = objStorehouseSourceProductBLL.GetByAll();
            foreach (var ObjDataItem in ItemList)
            {

                ObjDataString.Append("<Row>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.SourceProductName + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetCategoryName(ObjDataItem.ProductCategory) + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetCategoryName(ObjDataItem.ProductProject) + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Specifications + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.PurchasePrice + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.SaleOrice + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.SourceCount + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Unit + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Position + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Remark + "</Data></Cell>\r\n");
                ObjDataString.Append("</Row>\r\n");
                //                <Row>
                // <Cell><Data ss:Type="String">A</Data></Cell>
                // <Cell><Data ss:Type="String">B</Data></Cell>
                // <Cell><Data ss:Type="String">V</Data></Cell>
                // <Cell><Data ss:Type="String">V</Data></Cell>
                // <Cell><Data ss:Type="String">1500</Data></Cell>
                // <Cell><Data ss:Type="String">1500</Data></Cell>
                // <Cell><Data ss:Type="String">1500</Data></Cell>
                // <Cell><Data ss:Type="String">1500</Data></Cell>
                // <Cell><Data ss:Type="String">1500</Data></Cell>
                //</Row>
            }
            ObjTempletContent = ObjTempletContent.Replace("<!=DataRow>", ObjDataString.ToString());
            IOTools.DownLoadByString(ObjTempletContent, "xls");



        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            StorePager.CurrentPageIndex = 1;
            BinderData(sender, e);
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