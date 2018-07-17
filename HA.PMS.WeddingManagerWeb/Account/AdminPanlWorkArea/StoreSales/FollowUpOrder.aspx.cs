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
using HA.PMS.BLLAssmblly.FD;

//预访新人 黄晓可
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class FollowUpOrder : SystemPage
    {
        Customers ObjCustomersBLL = new Customers();
        Order ObjOrderBLL = new Order();

        string OrderByName = "CreateDate";

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
            var objParmList = new List<PMSParameters>();
            objParmList.Add("State", "8,9,202,203,205", NSqlTypes.IN);

            switch (ddlType.SelectedValue.ToInt32())
            {
                case 0:
                    if (txtStarTime.Text.Trim() != string.Empty && txtEndTime.Text.Trim() != string.Empty)
                    {
                        objParmList.Add("OrderCreateDate", txtStarTime.Text + "," + txtEndTime.Text, NSqlTypes.DateBetween);
                        OrderByName = "OrderCreateDate";
                    }
                    break;
                case 1:
                    if (txtStarTime.Text.Trim() != string.Empty && txtEndTime.Text.Trim() != string.Empty)
                    {
                        objParmList.Add("PartyDate", txtStarTime.Text + "," + txtEndTime.Text, NSqlTypes.DateBetween);
                        OrderByName = "PartyDate";

                    }
                    break;
                case 2:
                    if (txtStarTime.Text.Trim() != string.Empty && txtEndTime.Text.Trim() != string.Empty)
                    {
                        objParmList.Add("LastFollowDate", txtStarTime.Text + "," + txtEndTime.Text, NSqlTypes.DateBetween);
                        OrderByName = "LastFollowDate";
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
                objParmList.Add("Groom", txtContactMan.Text, NSqlTypes.ORLike);
            }



            this.MyManager.GetEmployeePar(objParmList);
            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjOrderBLL.GetByWhereParameter(objParmList, OrderByName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

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

        protected void btnserch_Click(object sender, EventArgs e)
        {
            BinderData();
        }

        protected string StatuHideViewInviteInfo()
        {
            if (new Employee().IsManager(User.Identity.Name.ToInt32()))
            {
                return string.Empty;
            }
            else
            {
                return "style=\"display:none\"";
            }
        }
    }
}