<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionGroupCheck.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionGroupCheck" Title="任务消息审核" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FL_MissionUpdate.aspx?DetailedID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 500, "a#" + $(Control).attr("id"));
        }

        function ShowDownLoad(KeyID, Control) {
            var Url = "/AdminPanlWorkArea/Flows/Mission/FL_MissionFileDownload.aspx?DetailedID=" + KeyID + "&Kind=0";
            showPopuWindows(Url, 800, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();

            //var Url = "FL_MissionFileDownload.aspx?DetailedID=" + KeyID;
            //showPopuWindows(Url, 700, 500, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {

            showPopuWindows($("#createMission").attr("href"), 700, 500, "a#createMission");

        });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>

    <div class="widget-box">

        <hr />
        <div class="widget-content ">
            计划提交人：<asp:Label ID="lblEmployee" runat="server" Text=""></asp:Label>
            <table class="table table-bordered table-striped with-check">
                <thead>
                    <tr>
                        <th style="white-space: nowrap;">任务名称</th>
                        <th style="white-space: nowrap;">工作内容</th>
                        <th style="white-space: nowrap;">完成标准</th>
                        <th style="white-space: nowrap;">附件</th>
                        <th style="white-space: nowrap;">计划完成时间</th>
                        <th style="white-space: nowrap;">倒计时</th>
                        <th style="white-space: nowrap;">紧急程度</th>
                        <th style="white-space: nowrap;">责任人</th>


                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptMission" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>

                                    <%#Eval("MissionName") %></td>
                                <td>
                                    <%#Eval("WorkNode") %></td>
                                <td>
                                    <%#Eval("FinishStandard") %></td>
                                <td><a href="#" onclick="ShowDownLoad(<%#Eval("DetailedID") %>,this)">查看/下载附件</a></td>
                                <td>
                                    <%#Eval("PlanDate") %></td>
                                <td>
                                    <%#Eval("Countdown") %>天</td>
                                <td>
                                    <%#Eval("Emergency") %></td>
                                <td>
                                    <%#GetEmployeeName(Eval("EmployeeID")) %>
                                    <asp:HiddenField ID="hiddDetailedID" runat="server" Value='<%#Eval("DetailedID") %>' />

                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tr>
                    <th colspan="8" style="text-align: left;">&nbsp;</th>
                </tr>

                <tr>
                    <td colspan="8" style="text-align: left;">审核意见<asp:TextBox ID="txtallCheckNode" runat="server" Rows="10" TextMode="MultiLine" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:Button ID="btnSaveChange" runat="server" Text="同意" OnClick="btnSaveChange_Click" />
                        <asp:Button ID="btnReturn" runat="server" Text="不同意" OnClick="btnReturn_Click" />
                    </td>
                </tr>
            </table>

        </div>
    </div>
</asp:Content>
