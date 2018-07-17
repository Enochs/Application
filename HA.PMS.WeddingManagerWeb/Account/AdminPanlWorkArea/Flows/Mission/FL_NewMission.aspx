<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_NewMission.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_NewMission" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FL_MissionUpdate.aspx?DetailedID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 500, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {
            $("html,body").css({ "background-color": "transparent" });
        //    showPopuWindows($("#createMission").attr("href"), 1000, 1000, "a#createMission");

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a class="btn btn-primary  btn-mini" href="FL_MissionDetailedCreate.aspx" id="createMission">创建任务</a>
    <asp:HiddenField ID="hideManagerValue" runat="server" />
    <div class="widget-box">
        <div id="tab1" class="tab-pane active">
            <div class="alert">
               
                <strong>您今日共有
                    <asp:Label ID="lblMissionCount" runat="server" Text="Label"></asp:Label>
                    项任务!</strong> 请安排时间处理。
            </div>
            <div class="widget-box">
            </div>
        </div>
        <hr />
        <div class="widget-content ">

            <asp:Repeater ID="rptMission" OnItemCommand="rptMission_ItemCommand" OnItemCreated="rptMission_ItemCreated" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th>任务名称</th>
                                <th>工作内容</th>
                                <th>完成标准</th>
                                <th>附件</th>
                                <th>完成时间</th>
                                <th>倒计时</th>
                                <th>紧急程度</th>
                                <th>责任人</th>
                                <th style="white-space: nowrap;">任务状态</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("MissionName") %></td>
                        <td><%#Eval("WorkNode") %></td>
                        <td><%#Eval("FinishStandard") %></td>
                        <td><%#Eval("Attachment") %></td>
                        <td><%#Eval("FinishDate") %></td>
                        <td><%#Eval("Countdown") %>天</td>
                        <td><%#Eval("Emergency") %></td>
                        <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                        <td><%#Eval("MissionState").ToString()=="1"?"拒绝":"未查看" %></td>
                        <td>

                            <a href="MissionDispose.aspx?DetailedID=<%#Eval("DetailedID") %>" class="btn btn-danger btn-mini">处理任务</a>

                            <a href="FL_MissionChangeApply.aspx?DetailedID=<%#Eval("DetailedID") %>" class="btn btn-danger btn-mini <%#Eval("Type").ToString()=="3"?"":"RemoveClass" %>">申请变更</a>


                            <asp:LinkButton ID="lkbtnDelete" CssClass='btn btn-danger btn-mini ' CommandArgument='<%#Eval("DetailedID") %>' CommandName="Delete" runat="server">拒绝</asp:LinkButton>
                            <a class="btn btn-danger btn-mini" <%#GetPlanStyle(Eval("MissionType").ToString(),Eval("IsOver").ToString()) %> href="FL_MissionDetailedCreate.aspx?Parent=<%#Eval("DetailedID") %>" >下达任务</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
            <HA:MessageBoard runat="server" ClassType="FL_MissioninWait" ID="MessageBoard" />
        </div>
    </div>
</asp:Content>
