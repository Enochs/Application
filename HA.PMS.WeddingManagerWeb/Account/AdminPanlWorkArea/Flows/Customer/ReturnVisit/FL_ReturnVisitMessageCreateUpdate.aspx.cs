using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HA.PMS.ToolsLibrary;
using System.Web.UI.WebControls;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.ReturnVisit
{
    public partial class FL_ReturnVisitMessageCreateUpdate : SystemPage
    {
        CustomerReturnVisit objCustomerReturnVisitBLL = new CustomerReturnVisit();
        InviteReturnState ObjInviteStateBLL = new InviteReturnState();
        HA.PMS.BLLAssmblly.Flow.Customers ObjCustomerBLL = new HA.PMS.BLLAssmblly.Flow.Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblReturnDate.Text = DateTime.Now.ToShortDateString();
 
                this.repItemList.DataBind(objCustomerReturnVisitBLL.GetReturnItemByall());
                var DataList = ObjInviteStateBLL.GetByAll();
                if (DataList.Count == 0)
                {
                    btnSaveReturn.Enabled = false;
                }
                
            }
        }

        #region 点击保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveReturn_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < repItemList.Items.Count; i++)
            {
                objCustomerReturnVisitBLL.Insert(new FL_CustomerReturnVisit()
                {
                    StateItem = (repItemList.Items[i].FindControl("lblTitle") as Label).Text,
                    ReturnDate = DateTime.Now,
                    Source = (repItemList.Items[i].FindControl("rdoState") as RadioButtonList).SelectedItem.Text,
                    SourceNode = (repItemList.Items[i].FindControl("txtSourceNode") as TextBox).Text,
                    ReturnSource = (repItemList.Items[i].FindControl("txtReturnSource") as TextBox).Text,
                    CustomerId = Request["CustomerID"].ToInt32()
                });
            }

            //添加下次到店日期
            FL_CustomerReturnVisit CReturnVisit = new FL_CustomerReturnVisit();
            CReturnVisit.StateItem = lblNextComeDate.Text.ToString();
            CReturnVisit.ReturnDate = DateTime.Now;
            CReturnVisit.Source = txtDate.Text.ToString() == "" ? "" : txtDate.Text.ToString();
            CReturnVisit.SourceNode = txtDateSource.Text.ToString();
            CReturnVisit.ReturnSource = txtDateReturnSource.Text.ToString();
            CReturnVisit.CustomerId = Request["CustomerID"].ToInt32();
            objCustomerReturnVisitBLL.Insert(CReturnVisit);

            //添加建议
            FL_CustomerReturnVisit ObjReturnVisitModel = new FL_CustomerReturnVisit();

            ObjReturnVisitModel.StateItem = lblSuggest.Text.ToString();
            ObjReturnVisitModel.ReturnDate = DateTime.Now;
            ObjReturnVisitModel.Source = txtSuggest.Text.ToString();
            ObjReturnVisitModel.SourceNode = txtSuggestSource.Text.ToString();
            ObjReturnVisitModel.ReturnSource = txtSuggestReturnSource.Text.ToString();
            ObjReturnVisitModel.CustomerId = Request["CustomerID"].ToInt32();

            objCustomerReturnVisitBLL.Insert(ObjReturnVisitModel);




            var ObjUpdateModel = ObjCustomerBLL.GetByID(Request["CustomerID"].ToInt32());
            ObjUpdateModel.IsReturn = true;
            ObjUpdateModel.ReasonsDate = DateTime.Now;
            ObjCustomerBLL.Update(ObjUpdateModel);
            JavaScriptTools.AlertWindowAndLocation("记录完毕!", "FL_CustomerReturnVisitManagerNot.aspx?NeedPopu=1", Page);
        }

         #endregion

        public void BinderDatas()
        {
            
        }

        protected void repItemList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RadioButtonList rdoState = e.Item.FindControl("rdoState") as RadioButtonList;
            rdoState.DataSource = ObjInviteStateBLL.GetByAll();
            rdoState.DataBind();
        }
    }
}