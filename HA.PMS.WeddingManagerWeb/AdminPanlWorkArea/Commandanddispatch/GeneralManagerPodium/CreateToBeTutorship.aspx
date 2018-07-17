<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CreateToBeTutorship.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.GeneralManagerPodium.CreateToBeTutorship" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>添加辅导</h5>
            <span class="label label-info"></span>
        </div>
        <div class="widget-content">
            辅导意见<asp:TextBox ID="txtMess" TextMode="MultiLine" runat="server"></asp:TextBox>
            <asp:Button ID="btnCreate" runat="server" CssClass="btn" OnClick="btnCreate_Click" Text="确定" />

        </div>
    </div>
</asp:Content>
