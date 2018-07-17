<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FL_TelemarketingOper.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.Telemarketing.FL_TelemarketingOper" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "height": "660px", "width": "500px" ,"background-color":"transparent","overflow":"hidden"});
        });
        function SetSelectEmpLoyee() {
            var CheckRadioVal = $('input[type=radio]:checked').val();
            $("#hiddEmployeeKey").attr("value", CheckRadioVal);
            if (CheckRadioVal) {
                return true;
            }
            alert("请选择责任人！");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="SelectEmployee" class="SelectEmpLoyee">
        <table style="width:96%;height:400px;overflow:scroll">
            <tr>
                <td style="width: 38%;vertical-align:top">
                    <asp:TreeView ID="treeDepartment" runat="server" Width="100%" OnSelectedNodeChanged="treeDepartment_SelectedNodeChanged"></asp:TreeView>
                </td>
                <td style="vertical-align: top;">
                    <table style="width:128px" class="table table-bordered">
                        <asp:Repeater ID="repEmpLoyeeList" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td style="width:16px">
                                        <input id="rdoEmpLoyee" type="radio" value='<%#Eval("EmployeeID") %>' name="radioEmpLoyee" ref="<%#Eval("EmployeeName") %>" /></td>
                                    <td style="width:64px"><%#Eval("EmployeeName") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </td>
            </tr>
        </table>
        <div style="text-align:center">
            <asp:Button ID="btnSaveSelect" runat="server" Text="确认" OnClientClick="return SetSelectEmpLoyee();" CssClass="btn btn-success" Style="width: 78px" OnClick="btnSaveSelect_Click" />
            <asp:HiddenField ID="hiddEmployeeKey" runat="server" ClientIDMode="Static" />
        </div>
    </div>
</asp:Content>
