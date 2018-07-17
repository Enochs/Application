using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using System.IO;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class CarrytaskWeddingPlanning : SystemPage
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

        HA.PMS.BLLAssmblly.Sys.WeddingPlanning ObjSysWeddingPlanningBLL = new BLLAssmblly.Sys.WeddingPlanning();
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

            int startIndex = CtrPageIndex.StartRecordIndex;
            //如果是最后一页，则重新设置起始记录索引，以使最后一页的记录数与其它页相同，如总记录有101条，每页显示10条，如果不使用此方法，则第十一页即最后一页只有一条记录，使用此方法可使最后一页同样有十条记录。

            int resourceCount = 0;

            var query = ObjWeddingPlanningBLL.GetByOrderIdIndex(Request["OrderID"].ToInt32(), CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out resourceCount); ;

            CtrPageIndex.RecordCount = resourceCount;

            repWeddingPlanning.DataSource = query;
            repWeddingPlanning.DataBind();
       
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
            //var ItemList = ObjWeddingPlanningBLL.GetByAll();
            var ItemList = ObjWeddingPlanningBLL.GetByOrderIdIndex(Request["OrderID"].ToInt32(), CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out resourceCount); ;
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
                //<Row>
                //<Cell><Data ss:Type="String">类别</Data></Cell>
                //<Cell><Data ss:Type="String">项目</Data></Cell>
                //<Cell><Data ss:Type="String">任务内容</Data></Cell>
                //<Cell><Data ss:Type="String">任务要求</Data></Cell>
                //<Cell><Data ss:Type="String">责任人</Data></Cell>
                //<Cell><Data ss:Type="String">备注</Data></Cell>
                //<Cell><Data ss:Type="String">计划完成时间</Data></Cell>
                //<Cell><Data ss:Type="String">状态</Data></Cell>
                //</Row>
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
            MissionDetailed ObjMissionManagerBLL = new MissionDetailed();
            var ObjID= ObjMissionManagerBLL.GetIDByTypeandFinishKey(e.CommandArgument.ToString().ToInt32(),8);
            if (ObjID > 0)
            {
                Response.Redirect("/AdminPanlWorkArea/Flows/Mission/MissionDispose.aspx?DetailedID="+ObjID);
            }
            
        }
    }
}