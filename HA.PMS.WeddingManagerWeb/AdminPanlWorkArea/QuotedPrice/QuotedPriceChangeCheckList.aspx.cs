using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.Pages;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.Sys;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice
{
    public partial class QuotedPriceChangeCheckList : SystemPage
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
        /// 报价
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 根据状态隐藏显示
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public string HideCreate(object State)
        {
            if (State == null)
            {
                return "style='display:none'";
            }
            if (State.ToString() == "True")
            {
                return "style='display:none'";
            }
            else
            {
                return string.Empty;

            }
        }


        /// <summary>
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var ObjParList = new List<ObjectParameter>();
            ObjParList.Add(new ObjectParameter("CheckState", 1));

            ObjParList.Add(new ObjectParameter("EmpLoyeeID", User.Identity.Name.ToInt32()));

            var DataList = ObjQuotedPriceBLL.GetCustomerQuotedByStateIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, ObjParList);
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();
        }


        /// <summary>
        /// 根据用户ID获取报价单ID
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetQuotedIDByCustomers(object CustomerID)
        {
            if (CustomerID.ToString() != string.Empty && CustomerID.ToString() != "0")
            {
                return ObjQuotedPriceBLL.GetByCustomerID(CustomerID.ToString().ToInt32()).QuotedID.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
    }
}