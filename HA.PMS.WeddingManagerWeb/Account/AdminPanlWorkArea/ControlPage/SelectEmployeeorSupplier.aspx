<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="SelectEmployeeorSupplier.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SelectEmployeeorSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("html,body").css({ "width": "480px", "height": "360px", "overflow": "hidden" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td colspan="2">
                <input id="Button1" type="button" value="员工" /><input id="Button4" type="button" value="供应商" />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <div style="width: 480px; height: 320px; overflow-y: scroll;">
                    <table style="height: 100%">
                        <tr style="vertical-align: top; text-align: left">
                            <td style="width: 220px; vertical-align: top; text-align: left">
                                <asp:TreeView ID="treeDepartment" runat="server" OnSelectedNodeChanged="treeDepartment_SelectedNodeChanged"></asp:TreeView>
                            </td>
                            <td style="width: auto; vertical-align: top; text-align: center">
                                <table class="table table-bordered table-striped">
                                    <asp:Repeater ID="repEmpLoyeeList" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 22px;">
                                                    <input id="rdoEmpLoyee" type="radio" value='<%#Eval("EmployeeID") %>' name="radioEmpLoyee" ref="<%#Eval("EmployeeName") %>" /></td>
                                                <td style="width: 100px;"><%#Eval("EmployeeName") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="text-align: center">
                </div>
                <a style="display: none;" id="showSelectEmployee" href="#SelectEmployee" onclick="ShowSelectEmployeeWindows();"><i class="icon-fullscreen"></i>选择用户</a></td>
            <td style="vertical-align: top;">

                <table style="width: 500px;">
                    <tr>
                        <td style="width: 250px; vertical-align: top;">&nbsp;<asp:ListBox ID="lstTyperBox" AutoPostBack="true" runat="server" Height="250px" Width="100%" OnSelectedIndexChanged="lstTyperBox_SelectedIndexChanged"></asp:ListBox>
                        </td>
                        <td>
                            <table border="1" cellpadding="5" cellspacing="1" style="width: 250px; height: 250px;">
               
                                <asp:Repeater ID="repSupplier" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="width: 30px;" bgcolor="#FFFFEE">
                                                <input id="rdoSupplier" type="radio" value='<%#Eval("SupplierID") %>' name="radioEmpLoyee" ref="<%#Eval("Name") %>" /></td>
                                            <td style="width: 100px;" bgcolor="#FFFFEE"><%#Eval("Name") %>
                        

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td colspan="2">
                                        <input id="Button3" type="button" value="button" style="display: none;" onclick="return SetSelectEmpLoyee();" />

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>




                <a style="display: none;" id="A1" href="#SelectEmployee" onclick="ShowSelectEmployeeWindows();"><i class="icon-fullscreen"></i>选择供应商</a>

            </td>
        </tr>

        <tr>
            <td>
                <asp:Button ID="btnSetSelectValue" runat="server" Text="确认选择" CssClass="btn btn-success" Style="width: 78px" OnClick="btnSetSelectValue_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
