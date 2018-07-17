<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionChecking.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionChecking" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

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
    <style type="text/css">

        /*#tblContent  td{
         text-align:center;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
        <div class="widget-box">

            <asp:Repeater ID="repMissionResualt" runat="server">
                <HeaderTemplate>
                    <table id="tblContent" class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th style="white-space: nowrap; width:200px;">计划名称</th>
                                <th  style="white-space:nowrap;">提交人</th>
                                <th  style="white-space:nowrap;">提交审核时间</th>
                                <th  style="white-space:nowrap;">审核人</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <th style="white-space: nowrap;"><%#Eval("MissionName") %><%#Eval("IsChangeMission").ToString()=="True"?"（变更）":"" %></td>
                        <td><%#GetEmployeeName(Eval("CreateEmpLoyee")) %></td>
                            <td><%#GetShortDateString(Eval("CreateDate")) %></td>
                            <td><%#GetEmployeeName(Eval("ChecksEmployee")) %></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>

            </asp:Repeater>

            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
   
</asp:Content>
