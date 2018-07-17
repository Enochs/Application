<%@ Page Title="查看计划" Language="C#"  AutoEventWireup="true" CodeBehind="FL_PlanShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_PlanShow" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
  <script>
        function ShowDownLoad(KeyID, Control) {
            var Url = "/AdminPanlWorkArea/Flows/Mission/FL_MissionFileDownload.aspx?DetailedID=" + KeyID + "&Kind=0";
            showPopuWindows(Url, 500, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();

            //var Url = "FL_MissionFileDownload.aspx?DetailedID=" + KeyID;
            //showPopuWindows(Url, 700, 500, "a#" + $(Control).attr("id"));
        }
</script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>

    <div class="widget-box">

        <hr />
        <div class="widget-content ">
            计划名称：<asp:Label ID="lblMInssiontitle" runat="server" Text=""></asp:Label>
            <br />
            计划制作人：<asp:Label ID="lblEmployee" runat="server" Text=""></asp:Label>
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
                                <td><a href="#" class="btn btn-mini btn-primary" onclick="ShowDownLoad(<%#Eval("DetailedID") %>,this)">查看/下载附件</a></td>
                                <td>
                                   
                                     <%#GetShortDateString(Eval("PlanDate")) %></td>
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

                </table>

        </div>
    </div>
</asp:Content>
