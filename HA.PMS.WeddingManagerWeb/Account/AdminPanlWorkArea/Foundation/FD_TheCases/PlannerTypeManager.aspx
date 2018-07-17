<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="PlannerTypeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases.PlannerTypeManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnConfirmSave").click(function () {
                if ($("#txtTypeName").val() == "") {
                    alert("请输入类型名称");
                    $("#txtTypeName").focus();
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div">
        <table>
            <tr>
                <th width="100">名称</th>
                <th width="100">状态</th>
            </tr>
            <asp:Repeater runat="server" ID="rptPlannerType" OnItemCommand="rptPlannerType_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td style="text-align:center">
                            <asp:TextBox runat="server" ID="txtTypeNames" Text='<%#Eval("TypeName") %>' /></td>
                        <td style="text-align:center;">
                            <asp:Label runat="server" ID="lblSates" Text='<%#GetState(Eval("IsDelete")) %>' /></td>
                        <td>
                            <asp:LinkButton runat="server" ID="lbtnUpdate" CommandName="lbtnUpdate" CommandArgument='<%#Eval("TypeID") %>' Text="修改" CssClass="btn btn-primary btn-mini"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbtnDisble" CommandName="lbtnDisble" CommandArgument='<%#Eval("TypeID") %>' Text="禁用" CssClass="btn btn-primary btn-mini"></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lbtnEnable" CommandName="lbtnEnable" CommandArgument='<%#Eval("TypeID") %>' Text="启用" CssClass="btn btn-primary btn-mini"></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td>
                    <asp:TextBox runat="server" ID="txtTypeName" ClientIDMode="Static" /></td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:Button runat="server" ID="btnConfirmSave" ClientIDMode="Static" Text="保存" OnClick="btnConfirmSave_Click" CssClass="btn btn-primary" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
