using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using System.IO;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass
{
    public partial class DesignclassReports : System.Web.UI.Page
    {
        int CustomerID = 0;
        int SourceCount = 0;
        string OrderColumnName = "PartyDate";
        HA.PMS.BLLAssmblly.Flow.Designclass ObjDesignBLL = new BLLAssmblly.Flow.Designclass();

        #region 页面加载
        /// <summary>
        /// 加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CustomerID = Request["CustomerID"].ToInt32();
                BinderData();
            }
        }
        #endregion

        #region 数据绑定
        public void BinderData()
        {
            CustomerID = Request["CustomerID"].ToInt32();
            var Pars = new List<PMSParameters>();
            //Pars.Add("DesignState", "1,2", NSqlTypes.IN);
            Pars.Add("CustomerID", CustomerID, NSqlTypes.Equal);
            var DataList = ObjDesignBLL.GetAllByParameter(Pars, OrderColumnName, 100, 1, out SourceCount);
            rptDesignList.DataSource = DataList;
            rptDesignList.DataBind();
            lblTotalPriceSum.Text = DataList.Sum(C => C.TotalPrice).ToString();

            for (int i = 0; i < rptDesignList.Items.Count; i++)
            {
                var ObjItem = rptDesignList.Items[i];
                HiddenField State = (HiddenField)ObjItem.FindControl("HideState");
                if (State.Value.ToInt32() == 2)
                {
                    TextBox txtRealQuantity = (TextBox)ObjItem.FindControl("txtRealQuantity");
                    Button btnConfirm = (Button)ObjItem.FindControl("btn_Confirm");
                    txtRealQuantity.ReadOnly = true;
                    btnConfirm.Visible = false;

                }

            }
        }
        #endregion

        #region 导出Excel
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
            this.rptDesignList.RenderControl(htw);
            string html = sw.ToString().Trim();
            Response.Output.Write(html);
            Response.Flush();
            Response.End();


        }
        #endregion

        #region 点击保存事件
        /// <summary>
        /// 保存
        /// </summary>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < rptDesignList.Items.Count; i++)
            {
                var ObjItem = rptDesignList.Items[i];
                HiddenField DesignId = (HiddenField)ObjItem.FindControl("HideDesignID");
                TextBox txtRealQuantity = (TextBox)ObjItem.FindControl("txtRealQuantity");
                FL_Designclass ObjDesignModel = ObjDesignBLL.GetByID(DesignId.Value.ToInt32());
                ObjDesignModel.RealQuantity = txtRealQuantity.Text.ToString().ToInt32();
                ObjDesignBLL.Update(ObjDesignModel);
            }
            JavaScriptTools.AlertWindow("修改成功", Page);
            BinderData();

        }
        #endregion

        protected void rptDesignList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Confirm")
            {
                int DesingId = e.CommandArgument.ToString().ToInt32();
                FL_Designclass ObjDesignModel = ObjDesignBLL.GetByID(DesingId);
                ObjDesignModel.State = 2;
                ObjDesignModel.Evaluation = 6;
                ObjDesignModel.Advance = "";
                ObjDesignModel.ShortCome = "";
                ObjDesignModel.RealQuantity = ((TextBox)e.Item.FindControl("txtRealQuantity")).Text.ToString().ToInt32();
                ObjDesignModel.TotalPrice = (ObjDesignModel.RealQuantity * ObjDesignModel.PurchasePrice);
                ObjDesignBLL.Update(ObjDesignModel);
            }
            JavaScriptTools.AlertWindow("修改成功", Page);
            BinderData();
        }
    }
}