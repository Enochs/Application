<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="UnderTheWorkingState.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.GeneralManagerPodium.UnderTheWorkingState" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "CreateToBeTutorship.aspx?EmployeeID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }


        //留言板
        function MessageBorde(EmpLoyeeID) {
            var URI = "/AdminPanlWorkArea/ControlPage/MessageBoardPage.aspx?EmpLoyeeID=" + EmpLoyeeID;
            showPopuWindows(URI, 600, 600, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        $(document).ready(function () {
            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {
                if (indexs != 3) {
                    $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });
            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");
            $("html,body").css({ "background-color": "transparent" });
        });
    </script>
    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 25px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">

        <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
        <table class="queryTable">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>部门
                                    <cc1:DepartmentDropdownList ID="DepartmentDropdownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DepartmentDropdownList1_SelectedIndexChanged">
                                    </cc1:DepartmentDropdownList>
                            </td>
                            <td>员工:<cc1:ddlEmployee ID="DdlEmployee1" runat="server"></cc1:ddlEmployee>
                            </td>

                            <td>时间:<cc1:DateEditTextBox ID="txtStar" onclick="WdatePicker();" runat="server"></cc1:DateEditTextBox>至<cc1:DateEditTextBox ID="txtEnd" onclick="WdatePicker();" runat="server"></cc1:DateEditTextBox></td>

                            <td>&nbsp;&nbsp;
                                    <asp:Button ID="btnQuery" Height="29" CssClass="btn btn-primary" runat="server" Text="查询" OnClick="btnQuery_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <table class="table table-bordered table-striped">
            <thead>


                <tr id="trContent">
                    <th>员工姓名</th>
                    <th>任务总数数量</th>
                    <th>按时完成数量</th>
                    <th>超时完成数量</th>
                    <th>任务完成率</th>
                    <th>按时进行任务数量</th>
                    <%--<th>待办任务数量</th>--%>
                    <th>超时进行任务数量</th>
                    <th>辅导</th>
                    <th>安排工作</th>
                </tr>
            </thead>
            <asp:Repeater ID="rptMission" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("EmployeeName") %></td>
                        <%--<td><%#Eval("FlowSum") %></td>
                        <td><%#Eval("FinishSUm") %></td>
                        <td><%#Eval("FinishRatio") %></td>
                        <td><%#Eval("DoingSum") %></td>
                        <td><%#Eval("WaitDoingSum") %></td>
                        <td><%#Eval("NewSum") %></td>--%>
                        <td><%#Eval("AllMissionSum") %></td>
                        <td><%#Eval("FinishSum") %></td>
                        <td><%#Eval("OverFinishSum") %></td>
                        <td><%#GetFinishRate(Eval("AllMissionSum"),Eval("FinishSum")) %></td>
                        <td><%#Eval("UnFinishSum") %></td>
                        <td><%#Eval("DelaySum") %></td>
                        <%--<td><%#Eval("AllMissionSum") %></td>--%>
                        <td><a href="#" class="btn btn-primary  btn-mini" onclick='MessageBorde(<%#Eval("EmployeeID") %>,this);'>辅导</a></td>
                        <td>
                            <a class="btn btn-primary  btn-mini" href="/AdminPanlWorkArea/Flows/Mission/FL_MissionDetailedCreate.aspx?EmployeeID=<%#Eval("EmployeeID") %>" target="_blank">下达任务</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>

        </table>
        <cc2:AspNetPagerTool ID="MissionPager" AlwaysShow="true" PageSize="2" OnPageChanged="MissionPager_PageChanged" runat="server"></cc2:AspNetPagerTool>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />

    </div>
</asp:Content>
