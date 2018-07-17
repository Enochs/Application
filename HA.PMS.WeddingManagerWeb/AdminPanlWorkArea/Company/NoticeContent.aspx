<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeContent.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.NoticeContent" Title="展示公告" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").height(450).css({ "background-color": "transparent" });
        });

    </script>
    <div class="widget-box">
        
        <div class="widget-content">
            <br />
            <div>
               <span style="font-weight:bold;">消息标题：</span><asp:Label ID="lbltitle" runat="server" Text="Label"></asp:Label>
            </div>
            <div>
             <span style="font-weight:bold;">消息内容:</span>  <asp:Label ID="lblContent" runat="server" Text="Label"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
