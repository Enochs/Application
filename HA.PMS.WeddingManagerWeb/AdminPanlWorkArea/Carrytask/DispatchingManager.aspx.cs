using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask
{
    public partial class DispatchingManager : SystemPage
    {
        /// <summary>
        /// 类别业务逻辑
        /// </summary>
        Category OjbCategoryBLL = new Category();

        Customers ObjCustomersBLL = new Customers();

        Dispatching ObjDispatchingBLL = new Dispatching();

        ProductforDispatching ObjProductforDispatchingBLL = new ProductforDispatching();


        //任务消息
        MissionDetailed ObjMissManagerBLL = new MissionDetailed();
        /// <summary>
        /// 报价单基础表
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        //客户ID
        int CustomersID = 0;
        //坤ID
        int OrderID = 0;
 
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderID = Request["QuotedID"].ToInt32();
            CustomersID = Request["CustomerID"].ToInt32();
            var ObjCustomerUpdateModel = ObjCustomersBLL.GetByID(CustomersID);
            ObjCustomerUpdateModel.State = (int)CustomerStates.DoingCarrytask;
            ObjCustomersBLL.Update(ObjCustomerUpdateModel);
            var DataSource = ObjProductforDispatchingBLL.GetByDispatchingID(Request["DispatchingID"].ToInt32(), 1);

            ObjMissManagerBLL.UpdateforFlow(6,Request["DispatchingID"].ToInt32(),User.Identity.Name.ToInt32());

            var ObjUpdateModel = ObjDispatchingBLL.GetByID(Request["DispatchingID"].ToInt32());
            ObjUpdateModel.IsBegin = true;
            ObjDispatchingBLL.Update(ObjUpdateModel);

            this.reptabstitle.DataSource = DataSource;
            this.reptabstitle.DataBind();
            this.reptabContent.DataSource = DataSource;
            this.reptabContent.DataBind();

            //var ObjCustomerModel = ObjCustomersBLL.GetByID(CustomersID);
            //lblCoder.Text = "订单号";
            //lblHotel.Text = ObjCustomerModel.Wineshop;
            //lblPartyDate.Text = ObjCustomerModel.PartyDate.Value.ToShortDateString();
            //lblPhone.Text = ObjCustomerModel.GroomCellPhone;
            //lblpinpai.Text = "品牌";
            //lblTimerSpan.Text = ObjCustomerModel.TimeSpans;
            //lblTyper.Text = "风格";
            //lblCustomerName.Text = ObjCustomerModel.Groom;
            //if (Request["New"] != null)
            //{
            DispatchingState ObjDispatchingStateBLL = new DispatchingState();
            ObjDispatchingStateBLL.CheckState(Request["DispatchingID"].ToInt32(), Request["StateKey"].ToInt32(), User.Identity.Name.ToInt32());
            //    //ObjProductforDispatchingBLL.UpdateIsGetforDispatchingID(Request["DispatchingID"].ToInt32(), User.Identity.Name.ToInt32());
            //}

 
        }
 
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 全部派给自己
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveByMine_Click(object sender, EventArgs e)
        {
            ObjProductforDispatchingBLL.UpdateforDispatchingID(Request["DispatchingID"].ToInt32(), User.Identity.Name.ToInt32());

           // UpdateforDispatchingID
        }

    }
}