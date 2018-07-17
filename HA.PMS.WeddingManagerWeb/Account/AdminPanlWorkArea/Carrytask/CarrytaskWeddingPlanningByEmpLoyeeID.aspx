<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskWeddingPlanningByEmpLoyeeID.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWeddingPlanningByEmpLoyeeID" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc3" %>
<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        //查看附件
        function ShowFileShowPopu(PlanningID) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWeddingFilesList.aspx?PlanningID=" + PlanningID + "&OnlyView=1";
            showPopuWindows(Url, 700, 800, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <br />
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <table style="text-align: center; width: 100%;" border="1">
        <tr>
            <td>类别</td>
            <td>项目</td>
            <td>任务内容</td>
            <td>任务要求</td>
            <td>附件</td>
            <td>责任人</td>
            <td>备注</td>
            <td>计划完成时间</td>
            <td>实际完成时间</td>

        </tr>
        <asp:Repeater ID="repWeddingPlanning" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <cc3:CategoryDropDownList ID="CategoryDropDownList1" BindByNow="1" runat="server" ParentID="0"></cc3:CategoryDropDownList></td>
                    <td>
                        <cc3:CategoryDropDownList ID="CategoryDropDownList2" BindByNow="2" runat="server" ParentID="1"></cc3:CategoryDropDownList></td>
                    <td>
                        <%#Eval("ServiceContent") %>'</td>
                    <td>
                        <%#Eval("Requirement") %></td>
                    <td><a href='#' onclick="ShowFileShowPopu('<%#Eval("PlanningID") %>')" class="btn btn-mini">查看</a></td>
                    <td id="Partd<%#Container.ItemIndex %>">
                        <input runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowPopu(this);" type="text" />
                    </td>
                    <td>
                        <%#Eval("Remark") %></td>
                    <td>
                        <%#Eval("PlanFinishDate") %>
                    </td>
                    <td>
                        <cc3:DateEditTextBox ID="txtFinishDate" MaxLength="20" onclick="WdatePicker();" runat="server"></cc3:DateEditTextBox></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="8">
                <asp:Button ID="btnAdd" runat="server" Text="添加一行" OnClick="btnSaveChange_Click" Style="height: 21px" />
            </td>
        </tr>
    </table>
</asp:Content>



