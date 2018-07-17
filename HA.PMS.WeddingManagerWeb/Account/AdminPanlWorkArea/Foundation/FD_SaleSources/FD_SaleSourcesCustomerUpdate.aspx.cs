using HA.PMS.BLLAssmblly.Sys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources
{
    public partial class FD_SaleSourcesCustomerUpdate : HA.PMS.Pages.SystemPage
    {
        protected bool IsSaleSourcePrivateOpening = false;   //指示自己录入的渠道只有自己和主管可以看见功能是否处于开启状态

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //基础信息
                FL_Customers fL_Customer = new BLLAssmblly.Flow.Customers().GetByID(Request["CustomerID"].ToInt32());
                if (fL_Customer != null)
                {
                    //绑定所有渠道类型
                    ddlChannelType.DataSource = new ChannelType().GetByAll();
                    ddlChannelType.DataTextField = "ChannelTypeName";
                    ddlChannelType.DataValueField = "ChannelTypeId";
                    ddlChannelType.DataBind();
                    //绑定新人渠道类型
                    ListItem channelTypeItem = ddlChannelType.Items.FindByValue(fL_Customer.ChannelType.ToString());
                    if (channelTypeItem != null)
                    {
                        ddlChannelType.ClearSelection();
                        channelTypeItem.Selected = true;
                    }

                    //绑定该渠道类型下的所有渠道名称
                    ddlChannelName.BindByParent(ddlChannelType.SelectedValue.ToInt32());
                    //绑定新人渠道名称
                    ListItem channelItem = ddlChannelName.Items.FindByText(fL_Customer.Channel);
                    if (channelItem != null)
                    {
                        ddlChannelName.ClearSelection();
                        channelItem.Selected = true;
                    }

                    //绑定该渠道名称下的所有推荐人
                    ddlReferee.BinderbyChannel(ddlChannelName.SelectedValue.ToInt32());
                    //绑定新人推荐人
                    ListItem refereeItem = ddlReferee.Items.FindByText(fL_Customer.Referee);
                    if (refereeItem != null)
                    {
                        ddlReferee.ClearSelection();
                        refereeItem.Selected = true;
                    }

                    //绑定酒店
                    ListItem wineshopItem = ddlHotel.Items.FindByText(fL_Customer.Wineshop);
                    if (wineshopItem != null)
                    {
                        ddlHotel.ClearSelection();
                        wineshopItem.Selected = true;
                    }

                    txtBride.Text = fL_Customer.Bride;
                    txtBrideCellPhone.Text = fL_Customer.BrideCellPhone;
                    txtPartyDate.Text = ShowPartyDate(fL_Customer.PartyDate);
                    ddlTimerSpan.SelectedItem.Text = fL_Customer.TimeSpans;
                    txtOther.Text = fL_Customer.Other;
                    txtBrideQQ.Text = fL_Customer.BrideQQ;
                }
            }
        }

        protected void DataDropDownList()
        {
            //清空选项
            ddlChannelName.Items.Clear();
            ddlChannelName.ClearSelection();

            //频道权限
            IsSaleSourcePrivateOpening = new SysConfig().IsSaleSourcePrivateOpening(User.Identity.Name.ToInt32(), false);

            //如果开启了频道限制功能，上级可以看下级所有，自己可以看。
            if (IsSaleSourcePrivateOpening)
            {
                ddlChannelName.BindSubordinateByParent(ddlChannelType.SelectedValue.ToInt32(), User.Identity.Name.ToInt32());
            }
            else
            {
                ddlChannelName.BindByParent(ddlChannelType.SelectedValue.ToInt32());
            }
        }

        protected void ddlChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataDropDownList();
            ddlReferee.Items.Clear();
        }

        protected void ddlChannelName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlReferee.BinderbyChannel(ddlChannelName.SelectedValue.ToInt32());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            BLLAssmblly.Flow.Customers customersBLL = new BLLAssmblly.Flow.Customers();
            FL_Customers fL_Customer = customersBLL.GetByID(Request["CustomerID"].ToInt32());
            fL_Customer.CustomerID = Request["CustomerID"].ToInt32();
            fL_Customer.ChannelType = ddlChannelType.SelectedValue.ToInt32();
            fL_Customer.Channel = ddlChannelName.SelectedItem.Text;
            fL_Customer.Referee = ddlReferee.SelectedItem.Text;
            fL_Customer.Bride = txtBride.Text;
            fL_Customer.BrideQQ = txtBrideQQ.Text;
            fL_Customer.BrideCellPhone = txtBrideCellPhone.Text;
            fL_Customer.PartyDate = txtPartyDate.Text.ToDateTime(DateTime.Parse("1949-10-01"));
            fL_Customer.TimeSpans = ddlTimerSpan.SelectedItem.Text;
            fL_Customer.Wineshop = ddlHotel.SelectedItem.Text;
            fL_Customer.Other = txtOther.Text;

            customersBLL.Update(fL_Customer);
            JavaScriptTools.AlertAndClosefancybox("修改成功", Page);
        }
    }
}