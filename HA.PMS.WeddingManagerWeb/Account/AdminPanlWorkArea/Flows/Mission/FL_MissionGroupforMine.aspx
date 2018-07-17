<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionGroupforMine.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionGroupforMine" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>


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
    &nbsp;<div class="widget-box">
        <hr />
        <div class="widget-content ">

            <asp:Repeater ID="repMissionResualt"   runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered table-striped with-check">
                        <thead>
                            <tr>
                                <th>任务名称</th>
                                <th>责任人</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("MissionTitle") %></td>
                        <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                        <td>
                            <a href="FL_MissionDetailedUpdate.aspx?MissionID=<%#Eval("MissionID") %>">修改计划</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </tbody>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
            <div class="row-fluid">
                <div class="span7">
                    <div class="widget-box">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-file"></i></span>
                            <h5>我的留言</h5>
                        </div>
                        <div class="widget-content nopadding">
                            <ul class="recent-posts">
                                <li>
                                    <div class="user-thumb">
                                        <img width="40" height="40" alt="User" src="img/demo/av1.jpg" />
                                    </div>
                                    <div class="article-post">
                                        <div class="fr"><a href="#" class="btn btn-primary btn-mini">编辑</a> <a href="#" class="btn btn-success btn-mini">回复</a> <a href="#" class="btn btn-danger btn-mini">删除</a></div>
                                        <span class="user-info">张总 / 2013.202.20 /09:27 </span>
                                        <p><a href="#">你的某某客户跟踪了4次。还没有签单，请说说具体的原因。</a> </p>
                                    </div>
                                </li>
                                <li>
                                    <button class="btn btn-warning btn-mini">给他留言</button>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="span6"></div>
            </div>
        </div>
    </div>
</asp:Content>
