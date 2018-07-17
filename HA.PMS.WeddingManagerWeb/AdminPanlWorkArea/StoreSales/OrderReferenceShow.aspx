<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderReferenceShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.OrderReferenceShow" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <link href="/App_Themes/Default/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/App_Themes/Default/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "600px", "height": "320px", "overflow": "hidden" });
        });
    </script>
    <div>
        <ul class="nav nav-tabs">
            <li><a href="#tabone" data-toggle="tab">建立信任</a></li>
            <li><a href="#tabtwo" data-toggle="tab">找到燃烧点</a></li>
            <li><a href="#tabthree" data-toggle="tab">优选</a></li>
            <li><a href="#tabfour" data-toggle="tab">确定</a></li>
        </ul>
        <div class="tab-content" style="height:300px;overflow-y:scroll">
            <div id="tabone" class="tab-pane fade in active"><asp:Literal ID="lblContent1" runat="server" Text=""></asp:Literal></div>
            <div id="tabtwo" class="tab-pane fade"><asp:Literal ID="lblContent2" runat="server" Text=""></asp:Literal></div>
            <div id="tabthree" class="tab-pane fade"><asp:Literal ID="lblContent3" runat="server" Text=""></asp:Literal></div>
            <div id="tabfour" class="tab-pane fade"><asp:Literal ID="lblContent4" runat="server" Text=""></asp:Literal></div>
        </div>
    </div>
</asp:Content>
