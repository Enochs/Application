using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.WorkReport
{
    public partial class WorkReportPanel : SystemPage
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int DispatchingID = Request["DispatchingID"].ToInt32();
                int CustomerID = Request["CustomerID"].ToInt32();
                int OrderID = Request["OrderID"].ToInt32();
                int QuotedID = Request["QuotedID"].ToInt32();
                string Type = Request["Type"].ToString();
                Li1.Visible = true;
                Li3.Visible = false;
                Iframe1.Src = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/WorkReport/PurchaseReport.aspx?DispatchingID=" + DispatchingID + "&CustomerID=" + CustomerID + "&OrderID=" + OrderID + "&QuotedID=" + QuotedID + "&NeedPopu=1";
                switch (Type)
                {
                    case "Prop":        //道具  采购清单
                        Li1.Visible = true;
                        Li3.Visible = false;
                        Iframe1.Src = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/WorkReport/PurchaseReport.aspx?DispatchingID=" + DispatchingID + "&CustomerID=" + CustomerID + "&OrderID=" + OrderID + "&QuotedID=" + QuotedID + "&NeedPopu=1";
                        break;
                    case "Flower":      //花艺采购清单
                        Li1.Visible = false;
                        Li3.Visible = true;
                        Iframe1.Src = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/FlowerReport/FlowerReport.aspx?DispatchingID=" + DispatchingID + "&CustomerID=" + CustomerID + "&OrderID=" + OrderID + "&QuotedID=" + QuotedID + "&NeedPopu=1";
                        break;
                }
            }
        }
    }
}