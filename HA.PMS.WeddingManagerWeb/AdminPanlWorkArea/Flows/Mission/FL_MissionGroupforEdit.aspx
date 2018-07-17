<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionGroupforEdit.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionGroupforEdit" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FL_MissionUpdate.aspx?DetailedID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 500, "a#" + $(Control).attr("id"));
        }

        //$(document).ready(function () {

        //    showPopuWindows($("#createMission").attr("href"), 1000, 1000, "a#createMission");

        //});
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content ">

            <asp:Repeater ID="repMissionResualt" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered table-striped" style="width:90%;">
                        <thead>
                        <tr>
                            <th style="white-space: nowrap; width: 33%;">任务名称</th>
                            <th style="white-space: nowrap; width: 33%;">责任人</th>
                            <th>创建时间</th>
                            <th>操作</th>
                        </tr>
                            </thead>
                </HeaderTemplate>
              
                 <ItemTemplate>
                   
                    <tr>
                        <td style="white-space: nowrap;"><%#Eval("MissionTitle") %></td>
                        <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                        <td><%#GetShortDateString(Eval("CreateDate")) %></td>
                        <td>
                            <a class="btn btn-primary btn-mini"  href="FL_MissionDetailedCreate.aspx?MissionID=<%#Eval("MissionID") %>">编辑</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
