using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork
{
    public partial class PrintTaskWork : System.Web.UI.Page
    {

        ProductforDispatching objProductForDisBLL = new ProductforDispatching();
        List<FL_ProductforDispatching> ProductList1 = new List<FL_ProductforDispatching>();
        int DisId = 0;
        string WorkType = "";
        string SupplierName = "";

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["DispatchingID"] != null)
                {
                    DisId = Request["DispatchingID"].ToInt32();
                    WorkType = Request["WorkType"].ToString();
                    SupplierName = Request["SupplierName"].ToString();
                    DataBinder();
                }
            }
        }
        #endregion

        #region 绑定数据
        public void DataBinder()
        {
            List<FL_ProductforDispatching> ProductList = objProductForDisBLL.GetByDispatchingID(DisId, 3, WorkType,SupplierName);
            var td = (from C in ProductList group C by new { C.SupplierName } into g select g);
            List<Person> list = new List<Person>();
            Person p = new Person();

            foreach (var item in td)
            {
                p = new Person();
                p.Sname = item.Key.SupplierName.ToString();
                list.Add(p);
            }

            rptProduct.DataSource = list;
            rptProduct.DataBind();
        }
        #endregion

        #region 绑定完成
        protected void rptProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSupplyName = e.Item.FindControl("lblSupplyName") as Label;
            Repeater RptData = e.Item.FindControl("rptDataList") as Repeater;
            ProductList1 = objProductForDisBLL.GetForSupplierName(lblSupplyName.Text.Trim().ToString(), WorkType, DisId, 3);


            Label lblSumMoney = e.Item.FindControl("lblSumMoney") as Label;
            Label lblSumUnitPrice = e.Item.FindControl("lblSumUnitPrice") as Label;
            Label lblSumQuantity = e.Item.FindControl("lblSumQuantity") as Label;

            lblSumMoney.Text = (lblSumMoney.Text.ToDecimal() + ProductList1.Where(C => C.Subtotal != null).ToList().Sum(C => C.Subtotal.Value)).ToString();
            lblSumUnitPrice.Text = ProductList1.Sum(C => C.PurchasePrice).ToString();
            lblSumQuantity.Text = ProductList1.Sum(C => C.Quantity).ToString();

            RptData.DataSource = ProductList1;
            RptData.DataBind();
        }
        #endregion

        #region 导出
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            //获取或设置一个值，该值指示是否缓冲输出，并在完成处理整个响应之后将其发送
            Response.Buffer = true;
            //获取或设置输出流的HTTP字符集
            Response.Charset = "GB2312";
            //将HTTP头添加到输出流
            Response.AppendHeader("Content-Disposition", "attachment;filename=PriceManage" + DateTime.Now.Date.ToString("yyyyMMdd") + ".xls");
            //获取或设置输出流的HTTP字符集
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            //获取或设置输出流的HTTP MIME类型
            Response.ContentType = "application/ms-excel";
            System.IO.StringWriter onstringwriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter onhtmltextwriter = new System.Web.UI.HtmlTextWriter(onstringwriter);
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.rptProduct.RenderControl(htw);
            string html = sw.ToString().Trim();
            Response.Output.Write(html);
            Response.Flush();
            Response.End();


        }
        #endregion

    }

    public class Person
    {
        public string Sname { get; set; }

    }
}