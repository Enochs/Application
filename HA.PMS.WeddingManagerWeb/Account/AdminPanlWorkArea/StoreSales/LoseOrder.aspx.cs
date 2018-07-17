using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.PublicTools;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class LoseOrder : SystemPage
    {
        Customers ObjCustomersBLL = new Customers();
        Order ObjOrderBLL = new Order();
        LoseContent ObjLoseContentBLL = new LoseContent();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlDataBind();
                BinderData();
            }
        }

        /// <summary>
        /// 获取流失原因
        /// </summary>
        /// <param name="ContentKey"></param>
        /// <returns></returns>
        public string GetLoseContent(object ContentKey)
        {
            if (ContentKey != null)
            {

                var Objmodel = ObjLoseContentBLL.GetByID(ContentKey.ToString().ToInt32());
                if (Objmodel != null)
                {
                    return Objmodel.Title;
                }
                else
                {
                    if (ContentKey.ToString() == "-3")
                    {
                        return "渠道信息无效";
                    }
                    else
                    {
                        return "未选择流失原因";
                    }
                }
            }
            else
            {
                return "未选择流失原因";
            }
        }

        /// <summary>
        /// 绑定成功预定的需要派单的 
        /// </summary>
        private void BinderData()
        {
            var objParmList = new List<PMSParameters>();
            objParmList.Add("State", (int)CustomerStates.Lose + ",10", NSqlTypes.IN);
            MyManager.GetEmployeePar(objParmList);
            //按流失愿意查询
            if (ddlLoseContent.SelectedValue.ToInt32() != 0)
            {
                objParmList.Add("ConteenID", ddlLoseContent.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }

            DateTime startTime = new DateTime();
            //如果没有选择结束时间就默认是当前时间
            string dateStr = "2100-1-1";
            DateTime endTime = dateStr.ToDateTime();

            if (!string.IsNullOrEmpty(txtStarTime.Text))
            {
                startTime = txtStarTime.Text.ToDateTime();
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text))
            {
                endTime = txtEndTime.Text.ToDateTime();
            }


            if (ddlType.SelectedValue != "-1")
            {
                //按流失时间查询
                string dateType = "LoseDate";

                if (ddlType.SelectedValue == "1")
                {
                    //按婚期查询
                    dateType = "PartyDate";

                }
                objParmList.Add(dateType, startTime + "," + endTime, NSqlTypes.DateBetween);
            }

            //按联系电话查询
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("BrideCellPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }

            //按新人姓名查询
            if (txtContactMan.Text != string.Empty)
            {
                objParmList.Add("Bride", txtContactMan.Text, NSqlTypes.LIKE);
                objParmList.Add("Groom", txtContactMan.Text, NSqlTypes.ORLike);
            }


            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjOrderBLL.GetByWhereParameter(objParmList, "PartyDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();

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

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }


        /// <summary>
        /// 查询统计结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnserch_Click(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void repCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int CustomerID = Convert.ToInt32(e.CommandArgument.ToString());

            FL_Customers ObjCustomersUpdateModel = ObjCustomersBLL.GetByID(CustomerID);
            switch (e.CommandName)
            {
                case "RecoOrder":
                    ObjCustomersUpdateModel.State = (int)CustomerStates.DidNotFollowOrder;
                    ObjCustomersUpdateModel.IsRescover = 1;
                    ObjCustomersBLL.Update(ObjCustomersUpdateModel);
                    //JavaScriptTools.AlertWindowAndLocation("已将新人恢复到跟单", "FollowOrderDetails.aspx?CustomerID=" + CustomerID, Page);
                    //JavaScriptTools.ResponseScript("alert('已将新人恢复到跟单,请在【跟单】中重新跟单');parent.window.location.href = parent.window.location.href", Page);
                    FL_Order OrderModel = ObjOrderBLL.GetbyCustomerID(CustomerID);
                    OrderModel.IsRecovery = 1;
                    ObjOrderBLL.Update(OrderModel);

                    //操作日志
                    CreateHandle(ObjCustomersUpdateModel);
                    JavaScriptTools.AlertWindow("已将新人恢复到跟单,请在【跟单】中重新跟单", Page);
                    BinderData();
                    break;
                default: break;

            }
        }
        public void ddlDataBind()
        {
            ddlLoseContent.Width = 75;
            LoseContent objLoseContentBLL = new LoseContent();
            ddlLoseContent.DataSource = objLoseContentBLL.GetByType(2);
            ddlLoseContent.DataTextField = "Title";
            ddlLoseContent.DataValueField = "ContentID";
            ddlLoseContent.DataBind();

            ddlLoseContent.Items.Add(new System.Web.UI.WebControls.ListItem("请选择", "0"));
            ddlLoseContent.Items.FindByText("请选择").Selected = true;
        }

        #region 创建操作日志
        /// <summary>
        /// 添加操作日志
        /// </summary>
        public void CreateHandle(FL_Customers ObjCustomersUpdateModel)
        {
            HandleLog ObjHandleBLL = new HandleLog();
            string Customername = ObjCustomersUpdateModel.Bride.ToString() == "" ? ObjCustomersUpdateModel.Groom.ToString() : ObjCustomersUpdateModel.Bride.ToString();
            sys_HandleLog HandleModel = new sys_HandleLog();

            HandleModel.HandleContent = "销售跟单,客户姓名:" + Customername + ",恢复跟单";

            HandleModel.HandleEmployeeID = User.Identity.Name.ToInt32();
            HandleModel.HandleCreateDate = DateTime.Now;
            HandleModel.HandleIpAddress = Page.Request.UserHostAddress;
            HandleModel.HandleUrl = Request.Url.AbsoluteUri.ToString();
            HandleModel.HandleType = 2;     //销售跟单
            ObjHandleBLL.Insert(HandleModel);
        }
        #endregion
    }
}