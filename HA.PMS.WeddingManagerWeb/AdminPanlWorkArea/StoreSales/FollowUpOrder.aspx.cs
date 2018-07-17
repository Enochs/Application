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

        string OrderByName = "NextFlowDate";

        #region 页面加载
        /// <summary>
        /// 初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();

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

            //按渠道名称查询
            if (DdlChannelName1.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("Channel", DdlChannelName1.SelectedItem.Text, NSqlTypes.LIKE);
            }

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
            //顾问
            if (MyManager.SelectedValue.ToInt32() > 0)
            {
                objParmList.Add("EmployeeID", MyManager.SelectedValue, NSqlTypes.Equal);
            }
            else
            {
                MyManager.GetEmployeePar(objParmList);
            }



            int startIndex = CtrPageIndex.StartRecordIndex;
            int SourceCount = 0;
            var DataList = ObjOrderBLL.GetByWhereParameter(objParmList, OrderByName, CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);

            CtrPageIndex.RecordCount = SourceCount;
            repCustomer.DataSource = DataList;
            repCustomer.DataBind();


        }
        #endregion

        #region 根据客户ID获取订单ID
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
        /// 分页
        /// </summary>
        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 查询
        /// <summary>
        /// 条件查询
        /// </summary>
        protected void btnserch_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 只有本人才能沟通
        /// <summary>
        /// 隐藏
        /// </summary>
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
        #endregion

        #region 隐藏 超期文字变色(红色)
        /// <summary>
        /// 隐藏
        /// </summary>    

        public string OverChange(object Source)
        {
            int CustomerID = Source.ToString().ToInt32();
            var Model = ObjOrderBLL.GetbyCustomerID(CustomerID);
            if (Model.NextFlowDate.ToString().ToDateTime().ToShortDateString().ToDateTime() <= DateTime.Now.ToShortDateString().ToDateTime())
            {
                return "style='color:red;'";
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 会员标志显示
        /// <summary>
        /// 会员标志
        /// </summary>
        protected void repCustomer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Image imgIcon = e.Item.FindControl("ImgIcon") as Image;
            int CustomerID = (e.Item.FindControl("HideCustomerID") as HiddenField).Value.ToString().ToInt32();
            var CustomerModel = ObjCustomersBLL.GetByID(CustomerID);
            if (CustomerModel.IsVip == true)            //该客户是会员
            {
                imgIcon.Visible = true;
            }
            else     //不是会员
            {
                imgIcon.Visible = false;
            }
        }
        #endregion
    }
}