<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionSumupManagerCreateEdit.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionSumupManagerCreateEdit" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
     <style type="text/css">
        
        .bolders {
            font-weight: bolder;
        }
    </style>
    <script type="text/ecmascript">
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtTitle.ClientID%>');
            BindText(65535, '<%=txtSumUp.ClientID%>');
        }
        function CheckSuccess()
        {
            return ValidateForm('input[check],textarea[check]');
        }
    </script>

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
            <td colspan="3"  class="bolders">工作总结</td>
        </tr>
        <tr>
            <td style="white-space: nowrap;" class="bolders"><span style="color:red">*</span>总结名称</td>
            <td>
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                <asp:TextBox ID="txtTitle" check="1" tip="限20个字符！" MaxLength="20" runat="server"></asp:TextBox>
                <a href="/AdminPanlWorkArea/Flows/Mission/FL_PlanShow.aspx?MissionID=<%=Request["MissionID"] %>" target="_blank" <%=Request["MissionID"]==null?"style='display:none;'":"" %> >查看计划</a>
            </td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td style="white-space: nowrap;"  class="bolders"><span style="color:red">*</span>总结内容</td>
            <td colspan="2" style="vertical-align: bottom;">
                <asp:TextBox ID="txtSumUp" check="1" MaxLength="65535" tip="限65535个字符！" runat="server" Rows="12" TextMode="MultiLine" Width="800px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td colspan="2">
                <asp:Button ID="btnSaveChange" runat="server" Text="保存" OnClientClick="return CheckSuccess();" OnClick="btnSaveChange_Click" CssClass="btn btn-success" />
                <asp:Button ID="btnFinish" runat="server" Text="提交到部门主管" OnClientClick="return CheckSuccess();" OnClick="btnFinish_Click"  CssClass="btn btn-success" Visible="false" />
            </td>
        </tr>
    </table>
</asp:Content>
