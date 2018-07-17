using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using System.IO;
using System.Collections.Generic;
using HA.PMS.DataAssmblly;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceSplit
{
    public partial class QuotedPriceManagerMission : SystemPage
    {
        /// <summary>
        /// 用户操作
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 订单操作
        /// </summary>
        Order ObjOrderBLL = new Order();

        /// <summary>
        /// 
        /// </summary>
        Dispatching ObjDispatchingBLL = new Dispatching();


        HA.PMS.BLLAssmblly.Flow.WeddingPlanning ObjWeddingPlanningBLL = new HA.PMS.BLLAssmblly.Flow.WeddingPlanning();
 
        /// <summary>
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPrice = new BLLAssmblly.Flow.QuotedPrice();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();

            }
        }

        /// <summary>
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            this.repManager.DataSource= ObjWeddingPlanningBLL.GetByOrderID(Request["OrderID"].ToInt32());
            this.repManager.DataBind();
           
        }


        /// <summary>
        /// 根据用户ID获取报价单ID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetQuotedIDByCustomers(object CustomerID)
        {
            return ObjQuotedPrice.GetByCustomerID(CustomerID.ToString().ToInt32()).QuotedID.ToString();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }



        protected void WeddingPlanningPager_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnExport_Click(object sender, EventArgs e)
        {
            StreamReader Objreader = new StreamReader(Server.MapPath("/AdminPanlWorkArea/Templet/ExcelTemplet/WeddingPlanningModel.xml"));

            int resourceCount = 0;
            string ObjTempletContent = Objreader.ReadToEnd();
            System.Text.StringBuilder ObjDataString = new System.Text.StringBuilder();
            Objreader.Close();
            var ItemList = ObjWeddingPlanningBLL.GetByOrderIdIndex(Request["OrderID"].ToInt32(),1000000, 1, out resourceCount); ;
            foreach (var ObjDataItem in ItemList)
            {
                ObjDataString.Append("<Row>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Category + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.CategoryItem + "</Data></Cell>\r\n"); //
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.ServiceContent + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Requirement + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetEmployeeName(ObjDataItem.EmpLoyeeID) + "</Data></Cell>\r\n");//
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.Remark + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + GetShortDateString(ObjDataItem.PlanFinishDate) + "</Data></Cell>\r\n");
                ObjDataString.Append(" <Cell><Data ss:Type=\"String\">" + ObjDataItem.State + "</Data></Cell>\r\n");
                ObjDataString.Append("</Row>\r\n");
            }
            ObjTempletContent = ObjTempletContent.Replace("<!=DataRow>", ObjDataString.ToString());
            IOTools.DownLoadByString(ObjTempletContent, "xls");
        }


        /// <summary>
        /// 产看任务完成情况
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repWeddingPlanning_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            

            JavaScriptTools.AlertWindow("确认完成！",Page);
            var ObjModel = ObjWeddingPlanningBLL.GetByID(e.CommandArgument.ToString().ToInt32());
            switch (e.CommandName)
            {
                case "Finish":
                    ObjModel.State = "已完成";
                    ObjWeddingPlanningBLL.Update(ObjModel);
                    break;
                case "Return":
                    ObjModel.State = "未完成";
                    ObjWeddingPlanningBLL.Update(ObjModel);
                    break;
                case "Delete":

                    ObjWeddingPlanningBLL.Delete(ObjModel);
                    break;
            }

            BinderData();
            //MissionDetailed ObjMissionManagerBLL = new MissionDetailed();
            //var ObjID = ObjMissionManagerBLL.GetIDByTypeandFinishKey(e.CommandArgument.ToString().ToInt32(), 8);
            //if (ObjID > 0)
            //{
            //    Response.Redirect("/AdminPanlWorkArea/Flows/Mission/MissionDispose.aspx?DetailedID=" + ObjID);
            //}

        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repManager_DataBinding(object sender, EventArgs e)
        {
            
        }

        
        /// <summary>
        /// 绑定数据显示控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repManager_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
           var DateItem=e.Item.DataItem as FL_WeddingPlanning;
           if (DateItem != null)
           {
               if (DateItem.State == "未完成")
               {
                   (e.Item.FindControl("lnkbtnReturn") as LinkButton).Visible = false;
               }

               if (DateItem.State == "已完成")
               {
                   (e.Item.FindControl("lnkbtnMission") as LinkButton).Visible = false;
               }
           }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuotedPriceMissionList.aspx");
        }
    }
}