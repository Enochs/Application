<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SetIntiveSerch.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SetIntiveSerch" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<div class="widget-box">
    <div class="widget-content">

                    渠道类型:<cc2:ddlChannelType ID="ddlChanneltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChanneltype_SelectedIndexChanged"></cc2:ddlChannelType>
                    渠道名称:<cc2:ddlChannelName ID="ddlChannelname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChannelname_SelectedIndexChanged"></cc2:ddlChannelName>
                    渠道联系人:<cc2:ddlReferee ID="ddlreferrr" runat="server"></cc2:ddlReferee>
                    <br />
                    新人状态:<cc2:ddlCustomersState runat="server" ID="ddlCustomersState1"></cc2:ddlCustomersState>
                    婚&nbsp;&nbsp;&nbsp;&nbsp;期: 
                    <cc2:DateEditTextBox runat="server" onclick="WdatePicker();" ID="txtStar"></cc2:DateEditTextBox>
                    到:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <cc2:DateEditTextBox runat="server" onclick="WdatePicker();" ID="txtEnd"></cc2:DateEditTextBox>

                    <asp:Button ID="btnSerch" runat="server" Text="查询" CssClass="btn" OnClick="btnSerch_Click" />
  
    </div>
</div>
