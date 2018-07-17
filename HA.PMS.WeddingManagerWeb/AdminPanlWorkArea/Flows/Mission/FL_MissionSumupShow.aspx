<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionSumupShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionSumupShow" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div style="overflow-y:scroll;height:800px">
     <style type="text/css">
        
        .bolders {
            font-weight: bolder;
        }
    </style>
     <a href="#" onclick="window.history.back();return false" class="btn btn-primary">返回</a><br />
    <table class="table table-bordered table-striped with-check">
        <tr>
            <td colspan="3" class="bolders">任务完成情况统计</td>
        </tr>
        <tr>
            <td  style="white-space: nowrap;"  class="bolders">任务数量</td>
            <td  style="white-space: nowrap; width:100px;"  class="bolders">实际完成数量</td>
            <td  style="white-space: nowrap; width:100px;"  class="bolders">超时完成数量</td>
            <td  style="white-space: nowrap;"  class="bolders">完成率</td>
        </tr>
        <tr>
            <td><asp:Label runat="server" ID="lblAllMissionSum" /></td>
            <td><asp:Label runat="server" ID="lblFinishSum" /></td>
            <td><asp:Label runat="server" ID="lblOverFinishSum" /></td>
            <td><asp:Label runat="server" ID="lblFinishRate" /></td>
        </tr>
    </table>
    <table class="table table-bordered table-striped with-check" style="display:none;">
        <thead>
  
            <tr>
                <th style="white-space: nowrap;">任务名称</th>
                <th style="white-space: nowrap;">工作内容</th>
                <th style="white-space: nowrap;">完成标准</th>
                <th style="white-space: nowrap;">附件</th>
                <th style="white-space: nowrap;">计划开始时间</th>
                <th style="white-space: nowrap;">计完成时间</th>
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
                        <td>
                            <%#Eval("Attachment") %></td>

                        <td>
                            <%#Eval("StarDate") %></td>
                        <td>
                            <%#Eval("FinishDate") %></td>
                        <td>
                            <%#Eval("Countdown") %>天</td>
                        <td>
                            <%#Eval("Emergency") %></td>
                        <td id="<%#Guid.NewGuid().ToString() %>">
                            <%#GetEmployeeName(Eval("EmpLoyeeID")) %>

                        </td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>


    </table>
    <table class="table table-bordered table-striped with-check" style="width: 100%;">
        <tr>
            <td style="white-space: nowrap;" class="bolders">总结名称</td>
            <td>
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td style="white-space: nowrap;"  class="bolders">总结内容</td>
            <td colspan="2" style="vertical-align: bottom;">
                <asp:Label ID="lblSumup" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>

        <tr>
            <td style="white-space: nowrap;"  class="bolders">辅导意见</td>
            <td colspan="2" style="vertical-align: bottom;">
                <asp:Label ID="lblup" runat="server" Text="Label"></asp:Label>
            </td>

    </table>
        </div>
</asp:Content>
