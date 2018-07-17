<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="SelectCategoryProject.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SelectCategoryProject" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "454px", "height": "500px", "background-color": "transparent" });
        });
        function CheckSave() {
            if ($("input[type=radio]:checked").length > 0) {
                if ($("#<%=CheckBoxList1.ClientID %>").find("input[type='checkbox']:checked").next().text() == "") {
                    alert("请选择产品属性");
                    return false;
                } else {
                    return true;
                }
            }
            else { alert("请选择一项！"); return false; }
        }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<div>
        <input type="button" id="btnStoresCategorys" class="btn btn-primary" value="库房" />
        <input type="button" id="btnQuotedCategorys" class="btn btn-primary" value="报价单" />
    </div>--%>
    <div runat="server" id="SelectEmployee" class="SelectEmpLoyee" style="width: 900px;">
        <table style="width: 420px;" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
            <tr>
                <td>
                    <table style="width: 420px;" border="1" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF">
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnConfrim" runat="server" Text="确认" OnClientClick="return CheckSave()" CssClass="btn  btn-primary" Style="width: 78px" OnClick="btnSaveSelect_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38%;">
                                <asp:TreeView ID="treeCategory" runat="server" Width="100%" OnSelectedNodeChanged="treeCategory_SelectedNodeChanged">
                                </asp:TreeView>
                            </td>
                            <td style="width: 50px;">>>></td>
                            <td style="vertical-align: top;">
                                <table border="0" cellpadding="1" cellspacing="1" bgcolor="#CCCCCC">
                                    <tr>
                                        <td>
                                            <table width="0px;" border="1" cellpadding="5" cellspacing="1">
                                                <asp:Repeater ID="rptProjectList" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="width: 30px;" bgcolor="#FFFFEE">


                                                                <input type="radio" name="rdo" id="rdoProject" value='<%#Eval("CategoryID") %>' ref='<%#Eval("CategoryName") %>' />
                                                            </td>
                                                            <td style="width: 100px;" bgcolor="#FFFFEE"><%#Eval("CategoryName") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnSaveSelect" runat="server" Text="确认" OnClientClick="return CheckSave()" CssClass="btn btn-primary" Style="width: 78px" OnClick="btnSaveSelect_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

    <div runat="server" id="divSelectQuotedCategory" class="SelectEmpLoyee" style="width: 900px;">
        <table style="width: 420px;" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
            <tr>
                <td>
                    <table style="width: 420px;" border="1" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF">
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnConfirmQuoted" runat="server" Text="确认" OnClientClick="return CheckSave()" CssClass="btn  btn-primary" Style="width: 78px" OnClick="btnSaveSelect_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 38%;">
                                <asp:TreeView ID="treeQuotedCategory" runat="server" Width="100%" OnSelectedNodeChanged="treeQuotedCategory_SelectedNodeChanged">
                                </asp:TreeView>
                            </td>
                            <td style="width: 50px;">>>></td>
                            <td style="vertical-align: top;">
                                <table border="0" cellpadding="1" cellspacing="1" bgcolor="#CCCCCC">
                                    <tr>
                                        <td>
                                            <table width="0px;" border="1" cellpadding="5" cellspacing="1">
                                                <asp:Repeater ID="RepQuotedCategory" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="width: 30px;" bgcolor="#FFFFEE">
                                                                <input type="radio" name="rdo" id="rdoProject" value='<%#Eval("QCKey") %>' ref='<%#Eval("Title") %>' />
                                                            </td>
                                                            <td style="width: 100px;" bgcolor="#FFFFEE"><%#Eval("Title") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>产品属性</td>
                            <td style="width: 50px;">>>></td>
                            <td>
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem>道具</asp:ListItem>
                                    <asp:ListItem>花艺</asp:ListItem>
                                    <asp:ListItem>灯光</asp:ListItem>
                                    <asp:ListItem>人员</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Button ID="btnConfirmQuoteds" runat="server" Text="确认" OnClientClick="return CheckSave()" CssClass="btn btn-primary" Style="width: 78px" OnClick="btnSaveSelect_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
