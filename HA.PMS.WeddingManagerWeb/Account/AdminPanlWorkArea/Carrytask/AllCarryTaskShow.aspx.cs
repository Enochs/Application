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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class AllCarryTaskShow : System.Web.UI.Page
    {
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();


        string WorkType = string.Empty;
        Repeater RptData = null;


        int DispatchingID = 0;
        int OrderID = 0;
        int QuotedID = 0;
        int CustomerID = 0;

        #region 页面初始化
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            DispatchingID = Request["DispatchingID"].ToInt32();
            OrderID = Request["OrderID"].ToInt32();
            QuotedID = Request["QuotedId"].ToInt32();
            CustomerID = Request["CustomerID"].ToInt32();
            //WorkType = Request["WorkType"];

            if (!IsPostBack)
            {
                BindDatasList();
            }
        }
        #endregion

        #region 分配供应商之后  底部列表显示
        /// <summary>
        /// 分配供应商
        /// </summary>
        public void BindDatasList()
        {
            //if (WorkType != "人员")
            //{
            List<FL_ProductforDispatching> ProductList = ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 3);
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
            //}
        }
        #endregion

        #region 外部的供应商绑定
        /// <summary>
        /// 获取内部Repeater 进行绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSupplyName = e.Item.FindControl("lblSupplyName") as Label;
            RptData = e.Item.FindControl("rptDataList") as Repeater;
            List<FL_ProductforDispatching> ProductList1 = ObjProductforDispatchingBLL.GetForSupplierName(lblSupplyName.Text.Trim().ToString(), "", DispatchingID, 3);
            //if (WorkType != "人员")
            //{
                Label lblSumMoney = e.Item.FindControl("lblSumMoney") as Label;
                Label lblSumQuantity = e.Item.FindControl("lblSumQuantity") as Label;

                lblSumMoney.Text = (lblSumMoney.Text.ToDecimal() + ProductList1.Where(C => C.Subtotal != null).ToList().Sum(C => C.Subtotal.Value)).ToString();
                lblSumQuantity.Text = ProductList1.Sum(C => C.Quantity).ToString();

                RptData.DataSource = ProductList1;
                RptData.DataBind();
            //}

        }

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



        #endregion



    }

    #region 供应商
    //派工之下的列表显示需要此类的帮助
    public class Person
    {
        public string Sname { get; set; }
    }
    #endregion

}