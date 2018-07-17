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
using System.Data.Objects;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class OrderDirectCost : SystemPage
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
        /// 成本核算
        /// </summary>
        Cost ObjCostBLL = new Cost();
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


        public string GetOtherItem(object OrderID,object Type)
        {
            var ObjCostItem=  ObjCostBLL.GetByOrderID(OrderID.ToString().ToInt32());
            if (ObjCostItem != null)
            {
                switch (Type.ToString())
                {
                    case "1":
                        return ObjCostItem.TotalAmount + string.Empty;
                        break;

                    case "2":
                        return ObjCostItem.ProfitMargin + string.Empty;
                        break;

                    case "3":
                        return ObjCostItem.Cost + string.Empty;
                        break;
                }
            }
            return string.Empty;
          
        }
        /// <summary>
        /// 绑定成功预定的客户 开始制作报价单
        /// </summary>
        private void BinderData()
        {
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            List<ObjectParameter> ObjParList = new List<ObjectParameter>();

            //if (txtStarDate.Text != string.Empty && txtEndDate.Text != string.Empty)
            //{
            //    ObjParList.Add(new ObjectParameter("PartyDate_between", txtStarDate.Text.ToDateTime() + "," + txtEndDate.Text.ToDateTime()));
            //}

            //ObjParList.Add(new ObjectParameter("Isover", false));
            //ObjParList.Add(new ObjectParameter("IsBegin", true));
            ObjParList.Add(new ObjectParameter("EmployeeID", User.Identity.Name.ToInt32()));

            var DataList = ObjDispatchingBLL.GetforMineByDoing(User.Identity.Name.ToInt32(), CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, User.Identity.Name.ToInt32(), ObjParList);
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
            return ObjQuotedPrice.GetByCustomerID(CustomerID.ToString().ToInt32()).QuotedID.ToString();
        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
    }

}