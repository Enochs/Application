<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CS_DegreeOfSatisfactionShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_DegreeOfSatisfactionShow" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () { $("html,body").css({ "width": "100%", "height": "360px", "background-color": "transparent" }); });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>客户满意度调查</h5>
            <span class="label label-info"></span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <th>调查项目</th>
                    <th>满意度</th>
                    <th>建议</th>
                </tr>
                <asp:Repeater ID="repItemList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("Title") %><asp:HiddenField runat="server" ID="HideKey" Value='<%#Eval("ItemKey") %>' />
                            </td>
                            <td><%#Eval("AssessName") %></td>
                            <td><%#Eval("StationContent") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td>总体满意度：</td>
                    <td colspan="2">
                        <asp:Label ID="lblDofContent" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>总体建议：</td>
                    <td>
                        <asp:Label ID="lblDegreeResult" runat="server" Text=""></asp:Label></td>
                </tr>
                </table>
        </div>
    </div>
</asp:Content>
