using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class DoingTask : SystemPage
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
            //int startIndex = CtrPageIndex.StartRecordIndex;
            //int SourceCount = 0;
            //var DataList = PageDataTools<View_DispatchingCustomers>.AddtoPageSize(ObjDispatchingBLL.GetDispatchingCustomersByEmpLoyeeIDandState(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, User.Identity.Name.ToInt32(),));
            //CtrPageIndex.RecordCount = SourceCount;
            //repCustomer.DataSource = DataList;
            //repCustomer.DataBind();
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

    }
}