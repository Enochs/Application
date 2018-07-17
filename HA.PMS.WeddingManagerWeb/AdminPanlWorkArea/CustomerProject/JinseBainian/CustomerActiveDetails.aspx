<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CustomerActiveDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CustomerProject.JinseBainian.CustomerActiveDetails" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btn {
            background-color: #227fc4;
            color: white;
            cursor: pointer;
            border: 1px solid #808080;
        }

            .btn :hover {
                background-color: #0d73be;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "450px", "height": "350px", "background-color": "#24cff6" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-content">
                <table class="table table-bordered table-striped">
                    <tr>
                        <td>套餐名称:</td>
                        <td>
                            <asp:Label runat="server" ID="lblPackage" /></td>
                        <td>录入人:</td>
                        <td>
                            <asp:Label runat="server" ID="lblEmployee" /></td>
                    </tr>
                    <tr>
                        <td>婚宴:</td>
                        <td>
                            <asp:Label runat="server" ID="lblDateItem" /></td>
                        <td>预定状态:</td>
                        <td>
                            <asp:Label runat="server" ID="lblState" /></td>
                    </tr>
                    <tr>
                        <td>录入时间:</td>
                        <td>
                            <asp:Label runat="server" ID="lblCreateDate" /></td>
                        <td>婚期:</td>
                        <td>
                            <asp:Label runat="server" ID="lblPartyDate" /></td>
                    </tr>
                    <tr>
                        <td>修改时间:</td>
                        <td>
                            <asp:Label runat="server" ID="lblupdateTime" /></td>
                        <td>修改人:</td>
                        <td>
                            <asp:Label runat="server" ID="lblupdatePerson" /></td>
                    </tr>
                    <tr>
                        <td>备注:</td>
                        <td colspan="3">
                            <asp:Label runat="server" ID="lblRemark" />
                        </td>
                    </tr>
<%--                    <tr>
                        <td colspan="4" style="text-align:center">
                            <asp:Button runat="server" CssClass="btn" Text="返回" Style="display: none;" /></td>
                    </tr>--%>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
