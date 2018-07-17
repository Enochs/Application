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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse
{
    public partial class FD_StorehouseProductQuery : SystemPage
    {
        StorehouseSourceProduct objStorehouseSourceProductBLL = new StorehouseSourceProduct();
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        int SourceCount = 0;
        string OrderByName = "SourceProductId";

        #region 页面初始化
        /// <summary>
        /// 初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                ddlCategory.ClearSelection();
                ddlCategory.Items.Add(new ListItem("请选择", "-1"));
                ddlCategory.Items[ddlCategory.Items.Count - 1].Selected = true;
                DataDropDownList(ddlCategory, ddlProject.SelectedValue.ToInt32(), true);

                DataDropDownList(ddlProject, ddlCategory.SelectedValue.ToInt32(), false);
                BinderData(sender, e);
            }

        }
        #endregion


        #region 数据绑定
        /// <summary>
        /// 绑定
        /// </summary>
        protected void BinderData(object sender, EventArgs e)
        {
            Repeater1.Visible = false;
            Repeater1.DataSource = ObjQuotedPriceBLL.GetByAll().Take(1).ToList();
            Repeater1.DataBind();
        }
        #endregion

        #region 下拉框绑定
        /// <summary>
        /// 绑定项目
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
        #endregion

        #region 类型选择变化事件
        /// <summary>
        /// 选择变化事件
        /// </summary>
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataDropDownList(ddlProject, ddlCategory.SelectedValue.ToInt32(), false);
        }
        #endregion

        #region 下载
        /// <summary>
        /// 下载
        /// </summary>
        public void DownLoad(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            //防止中文出现乱码
            string filename = HttpUtility.UrlEncode(fileInfo.Name);
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.WriteFile(path);

            Response.End();
        }
        #endregion

        #region 产品绑定完成事件
        /// <summary>
        /// 绑定
        /// </summary>
        protected void rptStorehouse_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FD_StorehouseSourceProduct storehouse = e.Item.DataItem as FD_StorehouseSourceProduct;
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
            PlaceHolder phEditOper = (PlaceHolder)e.Item.FindControl("phEditOper");
            //  Button lkbtnScrapped = (Button)e.Item.FindControl("lkbtnScrapped");
            Button lkbtnEdit = (Button)e.Item.FindControl("lkbtnEdit");
            Button lkbtnOper = (Button)e.Item.FindControl("lkbtnOper");

            //switch (storehouse.ProductState)
            //{
            //    case "编辑":
            //        if (lkbtnScrapped != null)
            //        {
            //            lkbtnScrapped.Visible = true;
            //            lkbtnEdit.Visible = false;
            //            lkbtnOper.Visible = true;
            //            phEditOper.Visible = true;
            //        }
            //        break;
            //    case "报废":
            //        lkbtnScrapped.Enabled = false;
            //        lkbtnEdit.Enabled = false;
            //        lkbtnOper.Enabled = false;
            //        phEditOper.Visible = false;
            //        break;
            //    case "拆用":
            //        lkbtnScrapped.Visible = false;
            //        lkbtnEdit.Visible = false;
            //        phEditOper.Visible = false;
            //        lkbtnOper.Enabled = false;
            //        break;
            //    default:
            //        break;
            //}
        }
        #endregion

        #region 产品绑定事件


        protected void rptStorehouse_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            FD_StorehouseSourceProduct fd = objStorehouseSourceProductBLL.GetByID((e.CommandArgument + string.Empty).ToInt32());
            string ProductState = "";

            switch (e.CommandName)
            {
                case "Scrapped":
                    ProductState = "报废";
                    break;
                case "Edit":
                    ProductState = "编辑";
                    break;
                case "Oper":
                    ProductState = "拆用";
                    break;

                default:
                    break;
            }
            if (e.CommandName != "DownLoad" && e.CommandName != "Edit")
            {
                fd.ProductState = ProductState + "1";

                int result = objStorehouseSourceProductBLL.Update(fd);


                //根据返回判断添加的状态
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("操作成功", this.Page);
                    BinderData(source, e);

                }
                else
                {
                    JavaScriptTools.AlertWindow("操作失败,请重新尝试", this.Page);

                }

            }
            else
            {// /AdminPanlWorkArea/Foundation/FD_Storehouse/FD_StorehouseProductUpdate.aspx
                if (e.CommandName == "Edit")
                {
                    #region
                    //    Button lkbtnEdit = e.Item.FindControl("lkbtnEdit") as Button;

                    //    PlaceHolder phName = e.Item.FindControl("phName") as PlaceHolder;

                    //    PlaceHolder phStoreDate = e.Item.FindControl("phStoreDate") as PlaceHolder;
                    //    PlaceHolder phProductCategory = e.Item.FindControl("phProductCategory") as PlaceHolder;
                    //    PlaceHolder phProductProject = e.Item.FindControl("phProductProject") as PlaceHolder;
                    //    PlaceHolder phSpecifications = e.Item.FindControl("phSpecifications") as PlaceHolder;
                    //    PlaceHolder phData = e.Item.FindControl("phData") as PlaceHolder;
                    //    PlaceHolder phPurchasePrice = e.Item.FindControl("phPurchasePrice") as PlaceHolder;

                    //    PlaceHolder phSaleOrice = e.Item.FindControl("phSaleOrice") as PlaceHolder;

                    //    PlaceHolder phSourceCount = e.Item.FindControl("phSourceCount") as PlaceHolder;
                    //    PlaceHolder phUnit = e.Item.FindControl("phUnit") as PlaceHolder;
                    //    if (lkbtnEdit.Text=="编辑")
                    //    {
                    //        lkbtnEdit.Text = "保存";
                    //        phName.Controls.Clear();
                    //        TextBox txtName = new TextBox();
                    //        txtName.ID = "txtName" + fd.SourceProductId;
                    //        txtName.Text = fd.SourceProductName;
                    //        phName.Controls.Add(txtName);
                    //    }
                    //    else
                    //    {
                    //        string names = (phName.FindControl("txtName" + fd.SourceProductId) as TextBox).Text;
                    //    }


                    #endregion

                }
                else
                {
                    var objResult = objStorehouseSourceProductBLL.GetByID((e.CommandArgument + string.Empty)
                     .ToInt32());
                    if (objResult != null)
                    {
                        string filePath = objResult.Data;
                        filePath = Server.MapPath(filePath);
                        DownLoad(filePath);
                    }

                }

            }

        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询功能
        /// </summary>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            StorePager.CurrentPageIndex = 1;
            BinderData(sender, e);
        }
        #endregion

        #region 获取各种时间  数量的方法
        /// <summary>
        /// 获取
        /// </summary>
        protected string GetLastUsedDate(object productid)
        {
            return objStorehouseSourceProductBLL.GetLastUsedDate(Convert.ToInt32(productid));
        }

        public int GetUsedTimes(object productid)
        {
            return objStorehouseSourceProductBLL.GetUsedTimes(Convert.ToInt32(productid));
        }

        public int GetLeaveCount(object productid)
        {
            return objStorehouseSourceProductBLL.GetLeaveCount(Convert.ToInt32(productid));
        }
        #endregion

        #region 外层Repeater 绑定完成事件
        /// <summary>
        /// 绑定完成事件
        /// </summary> 
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater RptData = e.Item.FindControl("rptStorehouse") as Repeater;

            int startIndex = StorePager.StartRecordIndex;
            int SourceCount = 0;

            List<PMSParameters> paramsList = new List<PMSParameters>();
            //产品名称
            paramsList.Add(txtProductName.Text.Trim().ToString() != "", "SourceProductName", txtProductName.Text.Trim(), NSqlTypes.LIKE);
            //类别
            paramsList.Add(ddlCategory.SelectedValue.ToInt32() > 0, "ProductCategory", ddlCategory.SelectedValue.ToInt32());
            //项目
            paramsList.Add(ddlProject.SelectedValue.ToInt32() > 0, "ProductProject", ddlProject.SelectedValue.ToInt32());
            //显示没有删除的
            paramsList.Add("IsDelete", false, NSqlTypes.Bit);
            //入库日期
            // paramsList.Add(PartyDateRanger.IsNotBothEmpty, "PutStoreDate_between", string.Format("{0},{1}", PartyDateRanger.Start, PartyDateRanger.End));

            List<FD_StorehouseSourceProduct> query = objStorehouseSourceProductBLL.GetAllByParameter(paramsList, "SourceProductId", StorePager.PageSize, StorePager.CurrentPageIndex, out SourceCount);
            StorePager.RecordCount = SourceCount;
            rptStorehouse.DataBind(query);
            RptData.DataBind(query);
        }
        #endregion

        #region 导出Excel
        /// <summary>
        /// 导出Excel 
        /// 分开绑定 分开导出 是因为不要导出最后一列的按钮
        /// </summary>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            List<PMSParameters> ObjParmList = new List<PMSParameters>();
            var ObjAllDataList = objStorehouseSourceProductBLL.GetAllByParameter(GetWhere(ObjParmList), OrderByName, 10000, 1, out SourceCount);
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                var ObjDataItem = Repeater1.Items;
                Repeater RptData = ObjDataItem[i].FindControl("rptStorehouse") as Repeater;
                RptData.DataBind(ObjAllDataList);
            }

            Repeater1.Visible = true;

            Response.Clear();
            //获取或设置一个值，该值指示是否缓冲输出，并在完成处理整个响应之后将其发送
            Response.Buffer = true;
            //获取或设置输出流的HTTP字符集
            Response.Charset = "GB2312";
            //将HTTP头添加到输出流
            Response.AppendHeader("Content-Disposition", "attachment;filename=订单成本明细" + ".xls");
            //获取或设置输出流的HTTP字符集
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //获取或设置输出流的HTTP MIME类型
            Response.ContentType = "application/ms-excel";
            System.IO.StringWriter onstringwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter onhtmltextwriter = new System.Web.UI.HtmlTextWriter(onstringwriter);
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.Repeater1.RenderControl(htw);
            string html = sw.ToString().Trim();
            Response.Output.Write(html);
            Response.Flush();
            Response.End();
        }
        #endregion

        #region 条件 GetWhere

        public List<PMSParameters> GetWhere(List<PMSParameters> Pars)
        {
            //产品名称
            Pars.Add("SourceProductName", txtProductName.Text.Trim(), NSqlTypes.LIKE);
            //类别
            Pars.Add(ddlCategory.SelectedValue.ToInt32() > 0, "ProductCategory", ddlCategory.SelectedValue.ToInt32());
            //项目
            Pars.Add(ddlProject.SelectedValue.ToInt32() > 0, "ProductProject", ddlProject.SelectedValue.ToInt32());
            return Pars;
        }
        #endregion
    }
}