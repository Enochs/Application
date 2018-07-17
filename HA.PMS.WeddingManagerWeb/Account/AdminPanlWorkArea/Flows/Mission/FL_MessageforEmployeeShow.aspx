<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MessageforEmployeeShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MessageforEmployeeShow" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>


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
        <hr />
        <div class="widget-content ">

            <asp:Repeater ID="rptMission"   runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th>回复时间</th>
                                <th>标题</th>
                                <th>回复人</th>

                                <th>查看</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr  <%#Eval("IsLook").ToString()=="False"?"style='color:#e45a5a'":"" %>>
                        <td><%#GetShortDateString(Eval("CreateDate")) %></td>
                        <td><%#Eval("MessAgeTitle") %></td>
                        <td><%#Eval("CreateEmployeename") %></td>

                        <td>

                            <a class="btn btn-mini btn-info" href="FL_MessageforEmployee.aspx?MessageID=<%#Eval("MessageID") %>">查看消息</a>

                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
