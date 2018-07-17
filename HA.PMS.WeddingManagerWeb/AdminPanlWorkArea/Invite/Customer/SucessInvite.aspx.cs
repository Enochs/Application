using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Sys;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer
{
    public partial class SucessInvite : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.Invite ObjInvtieBLL = new BLLAssmblly.Flow.Invite();
        Customers ObjCustomersBLL = new Customers();

        #region 页面加载
        /// <summary>
        /// 加载
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
        /// 绑定数据到Repeater控件中
        /// </summary>
        private void BinderData()
        {

            var GetWhereParList = new List<PMSParameters>();
            int SourceCount = 0;



            //员工类型
            if (ddlEmployeeType.SelectedValue != "-1")
            {
                if (MyManager.SelectedValue.ToInt32() > 0)
                {
                    GetWhereParList.Add(ddlEmployeeType.SelectedValue, MyManager.SelectedValue.ToInt32(), NSqlTypes.Equal);
                }
            }

            //GetWhereParList.Add("State",  (int)CustomerStates.InviteSucess + "," + (int)CustomerStates.SucessOrder + "," + (int)CustomerStates.DoingQuotedPrice + "," + (int)CustomerStates.DoingChecksQuotedPrice + "," + (int)CustomerStates.CheckQuotedPrice + "," + (int)CustomerStates.StarCarrytask + "," + (int)CustomerStates.DoingCarrytask, NSqlTypes.IN);
            GetWhereParList.Add("State", (int)CustomerStates.DidNotFollowOrder + "," + (int)CustomerStates.InviteSucess, NSqlTypes.IN);
            // GetWhereParList.Add(new ObjectParameter("State_Greaterthan", (int)CustomerStates.InviteSucess + "," + (int)CustomerStates.Sucess + string.Empty));
            if (ddlChannelname.SelectedItem != null && ddlChannelname.SelectedValue != "0")
            {
                GetWhereParList.Add("Channel", ddlChannelname.SelectedItem.Text, NSqlTypes.LIKE);
            }

            if (ddlChanneltype.SelectedItem.Text != "无")
            {
                GetWhereParList.Add("ChannelType", ddlChanneltype.SelectedItem.Value.ToInt32(), NSqlTypes.Equal);

            }
            if (ddlType.SelectedValue != "-1")
            {
                string dateType = "LastFollowDate";

                //邀约成功
                if (ddlType.SelectedValue == "0")
                {
                    dateType = "InviteSucessDate";
                }

                //婚期
                if (ddlType.SelectedValue == "1")
                {
                    dateType = "PartyDate";
                }

                //到店时间
                if (ddlType.SelectedValue == "2")
                {
                    dateType = "OrderCreateDate";
                }

                GetWhereParList.Add(dateType, DateRanger.StartoEnd, NSqlTypes.DateBetween);
            }


            //联系电话
            if (txtCellPhone.Text != string.Empty)
            {
                GetWhereParList.Add("ContactPhone", txtCellPhone.Text, NSqlTypes.StringEquals);
            }
            //联系人
            GetWhereParList.Add(txtContactMan.Text != string.Empty, "ContactMan", txtContactMan.Text, NSqlTypes.LIKE);

            var DataList = ObjInvtieBLL.GetByWhereParameter(GetWhereParList, "CreateDate", CtrPageIndex.PageSize, CtrPageIndex.CurrentPageIndex, out SourceCount);
            CtrPageIndex.RecordCount = SourceCount;
            repTelemarketingManager.DataSource = DataList;
            repTelemarketingManager.DataBind();

            //repTelemarketingManager.DataSource = ObjCustomersBLL.GetInviteCustomerByStateIndex();
            //repTelemarketingManager.DataBind();



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

        #region 条件查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            BinderData();
        }
        #endregion

        #region 渠道类型选择
        /// <summary>
        /// 选择渠道类型 绑定渠道名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlChanneltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChannelname.Items.Clear();
            ddlChannelname.BindByParent(ddlChanneltype.SelectedValue.ToInt32());
        }
        #endregion


        protected string StatuHideViewInviteInfo()
        {
            return !new Employee().IsManager(User.Identity.Name.ToInt32()) ? "style='display:none'" : string.Empty;
        }
        #region 会员标志是否显示   绑定事件
        /// <summary>
        /// 会员标志
        /// </summary>
        protected void repTelemarketingManager_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
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