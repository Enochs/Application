<%@ Page Title="" Language="C#" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="DealWithMission.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.DealWithMission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "450px", "height": "350px", "background-color": "#e1d3d3" });
        });
    </script>
    <style type="text/css">
        .id {
            background-color: #e1d3d3;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_All">
        <table class="table-bordered">
            <tr>
                <th colspan="2" align="left">
                    <h4>任务处理</h4>
                </th>
            </tr>
            <tr>
                <td valign="top">处理结果:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtFinishNode" TextMode="MultiLine" Width="280px" Height="120px" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button runat="server" ID="btnGetSave" Text="提交" CssClass="btn btn-primary" OnClick="btnGetSave_Click" />
                    <asp:Button runat="server" ID="btnCancel" Text="取消" CssClass="btn btn-primary" OnClick="btnCancel_Click"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
