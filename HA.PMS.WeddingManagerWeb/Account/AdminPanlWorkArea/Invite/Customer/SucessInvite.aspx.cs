using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Sys;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer
{
    public partial class SucessInvite : SystemPage
    {
        HA.PMS.BLLAssmblly.Flow.Invite ObjInvtieBLL = new BLLAssmblly.Flow.Invite();
        Customers ObjCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
           
        }
        
        /// <summary>
        /// 绑定数据到Repeater控件中
        /// </summary>
        private void BinderData()
        {

            var GetWhereParList = new List<PMSParameters>();
            int SourceCount = 0;


            //是否按照责任人查询
            if (MyManager.SelectedValue.ToInt32() == 0)
            {
                MyManager.GetEmployeePar(GetWhereParList);
            }
            else
            {
                GetWhereParList.Add("EmployeeID", MyManager.SelectedValue, NSqlTypes.Equal);
            }

            //GetWhereParList.Add("State",  (int)CustomerStates.InviteSucess + "," + (int)CustomerStates.SucessOrder + "," + (int)CustomerStates.DoingQuotedPrice + "," + (int)CustomerStates.DoingChecksQuotedPrice + "," + (int)CustomerStates.CheckQuotedPrice + "," + (int)CustomerStates.StarCarrytask + "," + (int)CustomerStates.DoingCarrytask, NSqlTypes.IN);
            GetWhereParList.Add("State", (int)CustomerStates.DidNotFollowOrder + "," + (int)CustomerStates.InviteSucess, NSqlTypes.IN);
           // GetWhereParList.Add(new ObjectParameter("State_Greaterthan", (int)CustomerStates.InviteSucess + "," + (int)CustomerStates.Sucess + string.Empty));
            if (ddlChannelname.SelectedItem != null && ddlChannelname.SelectedValue!="0")
            {
                GetWhereParList.Add("Channel",ddlChannelname.SelectedItem.Text,NSqlTypes.LIKE);
            }

            if (ddlChanneltype.SelectedItem.Text!="无")
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

        protected void CtrPageIndex_PageChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSerch_Click(object sender, EventArgs e)
        {
            CtrPageIndex.CurrentPageIndex = 1;
            BinderData();
        }
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

        /// <summary>
        /// 绑定渠道联系人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlChannelname_SelectedIndexChanged(object sender, EventArgs e)
        {
       
        }

        protected string StatuHideViewInviteInfo()
        {
            return !new Employee().IsManager(User.Identity.Name.ToInt32()) ? "style='display:none'" : string.Empty;
        }
    }
}