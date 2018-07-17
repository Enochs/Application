using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork
{
    public partial class ProductForDesctributin : SystemPage
    {
        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();

        Repeater RptData = null;
        string WorkTypes = "花艺,道具,灯光";
        int DispatchingID = 0;


        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DispatchingID = Request["DispatchingId"].ToInt32();
                BindDatasList();
            }
        }
        #endregion


        #region 分配供应商之后  列表显示

        public void BindDatasList()
        {

            List<Person> list = new List<Person>();

            List<FL_ProductforDispatching> ProductList = ObjProductforDispatchingBLL.GetByDispatchingID(DispatchingID, 3, false).Where(C => C.Classification != "人员").ToList();
            var FinishList = ProductList.Where(C => C.IsGet == true).ToList();
            var PartList = ProductList.Where(C => C.IsGet == null || C.IsGet == false).ToList();
            if (PartList.Count > 0 && FinishList.Count > 0)
            {
                lblMessage.Text = "该订单没有完全分解,请您手动分解...";
                return;
            }

            if (FinishList.Count == 0)
            {
                lblMessage.Text = "该订单没有分解,请您先分解该订单...";
                return;
            }

            var td = (from C in ProductList group C by new { C.SupplierName } into g select g).ToList();

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

        protected void rptProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblSupplyName = e.Item.FindControl("lblSupplyName") as Label;
            RptData = e.Item.FindControl("rptDataList") as Repeater;
            List<FL_ProductforDispatching> ProductList1 = ObjProductforDispatchingBLL.GetForSupplierName(lblSupplyName.Text.Trim().ToString(), "", DispatchingID, 3).Where(C => C.Classification != "人员").ToList();

            RptData.DataSource = ProductList1;
            RptData.DataBind();


            
            Label lblSumMoney = e.Item.FindControl("lblSumMoney") as Label;
            Label lblSumQuantity = e.Item.FindControl("lblSumQuantity") as Label;
            lblSumMoney.Text = (lblSumMoney.Text.ToDecimal() + ProductList1.Where(C => C.Subtotal != null).ToList().Sum(C => C.Subtotal.Value)).ToString();
            lblSumQuantity.Text = ProductList1.Sum(C => C.Quantity).ToString();
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

        public class Person
        {
            public string Sname { get; set; }
        }

        #region 点击刷新事件
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindDatasList();
        }
        #endregion
    }
}