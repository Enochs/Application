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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class OrderMessAgeManager:SystemPage
    {
        Customers ObjCustomersBLL = new Customers();
        Order ObjOrderBLL = new Order();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }
      
        /// <summary>
        /// 绑定成功预定的需要派单的
        /// </summary>
        private void BinderData()
        {


            List<ObjectParameter> ObjParList = new List<ObjectParameter>();
            ObjParList.Add(new ObjectParameter("State_NumOr", ((int)CustomerStates.DidNotFollowOrder).ToString() + "," + ((int)CustomerStates.BeginFollowOrder).ToString() + ",200,201,202,203"));
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = PageDataTools<View_GetOrderCustomers>.AddtoPageSize(ObjOrderBLL.GetOrderCustomerByIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, ObjParList));
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();
        }



        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            if (IsPostBack)
            {
                SerchControl.GetParaList();
            }
        }


        /// <summary>
        /// 根据客户ID获取订单ID
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public string GetOrderIDByCustomers(object e)
        {
            var ObjCustomer = ObjOrderBLL.GetbyCustomerID(e.ToString().ToInt32());
            if (ObjCustomer != null)
            {
                return ObjCustomer.OrderID.ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveDate_Click(object sender, EventArgs e)
        {

        }

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
    }
}