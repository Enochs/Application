<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionSumupManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionSumupManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforEmpLoyee.ascx" TagPrefix="HA" TagName="MessageBoardforEmpLoyee" %>
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
        <div id="tab1" class="tab-pane active" style="border-bottom: 1px solid #e1dede; height: 44px;">
            <div class="alert">
            

                <strong>您今日共有
                <cc2:lblMissionfortodaysum ID="LblMissionfortodaysum1" runat="server"></cc2:lblMissionfortodaysum>
                    项任务!</strong> 请安排时间处理。
            </div>

        </div>

        <div class="widget-content ">
           <%-- <a href="FL_MissionSumupManagerCreateEdit.aspx" class="btn btn-success">直接撰写工作总结</a>--%>
            <asp:Repeater ID="repMissionResualt" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered table-striped with-check">
                        <thead>
                            <tr>
                                <th style="white-space: nowrap; height: 16px; width: 200px;">计划名称</th>
                                <th style="white-space: nowrap;">创建时间</th>
                                <th style="white-space: nowrap;">创建人</th>
                                <th style="white-space: nowrap;">操作</th>
                                <th style="white-space: nowrap;">主管辅导意见</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="word-break: break-all;"><a target="_blank" href="FL_PlanShow.aspx?MissionID=<%#Eval("MissionID") %>"><%#Eval("MissionTitle") %></a> </td>
                        <td style="word-break: break-all;"><%#GetShortDateString(Eval("CreateDate")) %></td>

                        <td style="word-break: break-all; white-space: nowrap;">
                            <%#GetEmployeeName(Eval("EmployeeID")) %>
                        </td>
                        <td style="text-align: center;">
                            <a href="FL_MissionSumupManagerCreateEdit.aspx?MissionID=<%#Eval("MissionID") %>" <%#Eval("IsOver")==null&&Eval("EmployeeID").ToString()==User.Identity.Name?"":"style='display:none'" %> class="btn btn-success btn-mini">撰写工作总结</a>
                            <a href="FL_MissionSumupShow.aspx?MissionID=<%#Eval("MissionID") %>"  <%#Eval("IsOver")!=null?"":"style='display:none'" %> class="btn btn-info btn-mini">查看工作总结</a>
                            <a class="btn btn-primary btn-mini" <%#Eval("IsOver")==null&&Eval("EmployeeID").ToString()==User.Identity.Name?"":"style='display:none'" %> href="FL_MissionDetailedCreate.aspx?MissionID=<%#Eval("MissionID") %>&NeedPopu=1&Action=Check">计划变更</a>
                        </td>
                        <td style="text-align: center;">
                            <a href="FL_MissionSumupShow.aspx?MissionID=<%#Eval("MissionID") %>" <%#Eval("IsOver")!=null&&Eval("IsAppraise").ToString()=="True"?"":"style='display:none'" %> class="btn btn-info btn-mini">查看辅导意见</a>
                            <a href="FL_MissionSumupforCheckEmployee.aspx?MissionID=<%#Eval("MissionID") %>" <%#Eval("IsOver")!=null&&Eval("IsAppraise").ToString()=="False"&&Eval("CheckEmpLoyeeID").ToString()==User.Identity.Name?"":"style='display:none'" %> class="btn btn-primary btn-mini">辅导</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <cc1:AspNetPagerTool ID="CtrPageIndex" PageSize="10" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />

        </div>
    </div>
</asp:Content>
