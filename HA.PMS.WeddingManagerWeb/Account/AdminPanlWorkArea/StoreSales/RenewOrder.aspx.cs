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
    public partial class RenewOrder : SystemPage
    {
        Customers ObjCustomersBLL = new Customers();
        Order ObjOrderBLL = new Order();
        LoseContent ObjLoseContentBLL = new LoseContent();

        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        #endregion

        #region 获取流失原因
        /// <summary>
        /// 获取流失原因
        /// </summary>
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
                    return "未流失";
                }
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定成功预定的需要派单的 
        /// </summary>
        private void BinderData()
        {
            var objParmList = new List<PMSParameters>();
            objParmList.Add("State", (int)CustomerStates.Lose + "," + ((int)CustomerStates.BeginFollowOrder).ToString() + ",200,201,202,203,205", NSqlTypes.NotIN);

            switch (ddlType.SelectedValue.ToInt32())
            {
                case 0:
                    if (txtStarTime.Text.Trim() != string.Empty && txtEndTime.Text.Trim() != string.Empty)
                    {
                        objParmList.Add("OrderCreateDate", txtStarTime.Text + "," + txtEndTime.Text, NSqlTypes.DateBetween);
                    }
                    break;
                case 1:
                    if (txtStarTime.Text.Trim() != string.Empty && txtEndTime.Text.Trim() != string.Empty)
                    {
                        objParmList.Add("PartyDate", txtStarTime.Text + "," + txtEndTime.Text, NSqlTypes.DateBetween);

                    }
                    break;
                case 2:
                    if (txtStarTime.Text.Trim() != string.Empty && txtEndTime.Text.Trim() != string.Empty)
                    {
                        objParmList.Add("LastFollowDate", txtStarTime.Text + "," + txtEndTime.Text, NSqlTypes.DateBetween);

                    }
                    break;

            }
            if (txtCellPhone.Text != string.Empty)
            {
                objParmList.Add("BrideCellPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }
            if (txtContactMan.Text != string.Empty)
            {
                objParmList.Add("Bride", txtContactMan.Text, NSqlTypes.LIKE);
            }



            this.MyManager.GetEmployeePar(objParmList);
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjOrderBLL.GetByWhereParameter(objParmList, "CreateDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();

            #region 老方法


            //List<ObjectParameter> ObjParList = new List<ObjectParameter>();
            //ObjParList.Add(new ObjectParameter("State", (int)CustomerStates.Lose));
            //MyManager.GetEmployeePar(ObjParList);
            //if (ddlLoseContent.SelectedValue.ToInt32() != 0) 
            //{
            //    ObjParList.Add(new ObjectParameter("ConteenID", ddlLoseContent.SelectedValue.ToInt32()));
            //}

            //DateTime startTime = new DateTime();
            ////如果没有选择结束时间就默认是当前时间
            //string dateStr = "2100-1-1";
            //DateTime endTime = dateStr.ToDateTime();

            //if (!string.IsNullOrEmpty(txtStarTime.Text))
            //{
            //    startTime = txtStarTime.Text.ToDateTime();
            //}
            //if (!string.IsNullOrEmpty(txtEndTime.Text))
            //{
            //    endTime = txtEndTime.Text.ToDateTime();
            //}


            //if (ddlType.SelectedValue != "-1")
            //{
            //    string dateType = "LoseDate";

            //    if (ddlType.SelectedValue == "1")
            //    {

            //        dateType = "PartyDate";

            //    }
            //    ObjectParameter dates = new ObjectParameter(dateType + "_between", startTime + "," + endTime);
            //    ObjParList.Add(dates);
            //}

            //int startIndex = CtrPageIndex.StartRecordIndex;
            //int SourceCount = 0;
            //var DataList = ObjOrderBLL.GetOrderCustomerByIndex(CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount, ObjParList);
            //CtrPageIndex.RecordCount = SourceCount;

            //repCustomer.DataSource = DataList;
            //repCustomer.DataBind();
            #endregion
        }
        #endregion

        #region 获取订单ID
        /// <summary>
        /// 根据客户ID获取订单ID
        /// </summary>
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
        #endregion

        #region 分页
        /// <summary>
        /// 分页  上一页/下一页
        /// </summary>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 点击查询
        /// <summary>
        /// 查询统计结果
        /// </summary>
        protected void btnserch_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 恢复跟单
        /// <summary>
        /// 恢复跟单
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>

        protected void repCustomer_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            WorkReport ObjWorkBLL = new WorkReport();
            //获取流失时间
            DateTime LoseDate = (e.Item.FindControl("lblLastFollowDate") as Label).Text.ToDateTime();
            var Model = ObjWorkBLL.GetEntityByTimeCustomerID(User.Identity.Name.ToInt32(), LoseDate);
            Model.LoseOrderNum -= 1;       //生成时间当天的流失量减1 电销量不用手动加1  沟通之后 日报表中会自动增加
            ObjWorkBLL.Update(Model);

            int CustomerID = (e.CommandArgument + string.Empty).ToInt32();
            FL_Customers ObjCustomersUpdateModel = ObjCustomersBLL.GetByID(CustomerID);
            switch (e.CommandName)
            {
                case "ReOrder":
                    ObjCustomersUpdateModel.State = (int)CustomerStates.DidNotFollowOrder;
                    ObjCustomersBLL.Update(ObjCustomersUpdateModel);
                    //JavaScriptTools.AlertWindowAndLocation("已将新人恢复到跟单", "FollowOrderDetails.aspx?CustomerID=" + CustomerID, Page);
                    //JavaScriptTools.ResponseScript("alert('已将新人恢复到跟单,请在【跟单】中重新跟单');parent.window.location.href = parent.window.location.href", Page);
                    JavaScriptTools.AlertWindow("已将新人恢复到跟单,请在【跟单】中重新跟单", Page);
                    BinderData();
                    break;
                default: break;
            }
        }
        #endregion
    }
}