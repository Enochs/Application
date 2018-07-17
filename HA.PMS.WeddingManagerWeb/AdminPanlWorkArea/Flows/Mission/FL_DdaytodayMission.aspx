<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_DdaytodayMission.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_DdaytodayMission" Title="处理日常任务" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <table style="width: 100%;">

        <tr>
            <td>处理任务</td>
            <td></td>
        </tr>

        <tr>
            <td class="auto-style1">任务名称</td>
            <td class="auto-style1">
                <asp:Label ID="lblMissionName" runat="server" Text=""></asp:Label>

            </td>
        </tr>

        <tr>
            <td>计划完成时间</td>
            <td>
                <asp:Label ID="lblFinishDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td>紧急重要程度</td>
            <td>
                <asp:Label ID="lblEmergency" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td>工作内容</td>
            <td>
                <asp:Label ID="lblWorkNode" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td>处理结果</td>
            <td>
                <cc1:CKEditorTool ID="CKEditorTool1" runat="server" Width="100%" Height="500">
                </cc1:CKEditorTool>
            </td>
        </tr>

        <tr>
            <td>上传附件</td>
            <td>
                <asp:FileUpload ID="fupfinifile" runat="server" />
            </td>
        </tr>

        <tr>
            <td>计划完成时间</td>
            <td>
                <asp:Label ID="lblPlanDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td>实际完成时间</td>
            <td>
                <asp:TextBox ID="txtFinishDate" runat="server"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <asp:Button ID="btnSaveDate" runat="server" Text="处理任务" OnClick="btnSaveDate_Click" style="height: 21px" />
            </td>
        </tr>
    </table>
</asp:Content>
