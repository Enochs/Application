<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionCheckList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionCheckList" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>



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
   
        <div class="widget-content ">
                    <div id="tab1" class="tab-pane active" style=" border-bottom:1px solid #e1dede; height:44px;">
            <div class="alert">
                
                <strong>您今日共有
                <cc2:lblMissionfortodaysum ID="LblMissionfortodaysum1" runat="server"></cc2:lblMissionfortodaysum>
                    项任务!</strong> 请安排时间处理。
            </div>
             
        </div>
            <asp:Repeater ID="repMissionResualt" runat="server">
                <HeaderTemplate>
                    <table id="tblContent" class="table table-bordered table-striped with-check">
                        <thead>
                            <tr>
                                <th style="white-space: nowrap; width:200px;">计划名称</th>
                                <th>提交人</th>
                                <th>提交审核时间</th>
                                <th>审核人</th>
                                <th>操作</th>
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
                            <td>
                                <a class="btn btn-primary btn-mini" href="<%#Eval("KeyWords") %>&ChangeID=<%#Eval("ChangeID") %>&Action=Check"<%#Eval("ChecksEmployee").ToString()==User.Identity.Name?"":"style='display:none;'"%> ><%#Eval("MissionType").ToString()=="1"?"审核任务":"审核计划" %></a>
                            </td>
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
    </div>
</asp:Content>
