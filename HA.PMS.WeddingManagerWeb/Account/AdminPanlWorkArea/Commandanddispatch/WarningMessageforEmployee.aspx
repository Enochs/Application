<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="WarningMessageforEmployee.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.WarningMessageforEmployee" Title="员工警告信息" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

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

            <asp:Repeater ID="rptMission" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th>标题</th>
                                <th>时间</th>
                                <th>责任人</th>
                                <th>查看</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>

                        <td><%#Eval("MessAgeTitle") %></td>
                        <td><%#GetShortDateString(Eval("CreateDate")) %></td>
                        <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                        <td>
                            <a href="<%#IsNullOrEmpty(Eval("ResualtAddress"))?string.Empty:string.Format("{0}&{1}",Eval("ResualtAddress"),"OnlyView=1") %>">查看</a>
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
