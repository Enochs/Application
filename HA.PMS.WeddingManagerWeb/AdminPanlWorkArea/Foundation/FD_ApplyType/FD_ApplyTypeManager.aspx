<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_ApplyTypeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_ApplyType.FD_ApplyTypeManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="widget-box">
        <table class="table" style="width: 50%;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Repeater runat="server" ID="repApplyType" OnItemCommand="repApplyType_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td class="li_ApplyTitle">
                                    <%--<input type="text" name='SaleName<%#Eval("ID") %>' class="form-control" value='<%#Eval("ApplyName") %>' />--%>
                                    <asp:TextBox runat="server" ID="txtApplyName" Text='<%#Eval("ApplyName") %>' />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnModify" CommandName="ModifyStatus" CommandArgument='<%#Eval("ID") %>' CssClass="btn btn-primary btn-sm" Text="修改" title='修改'></asp:Button>
                                    <asp:Button runat="server" ID="btnDelete" CommandName="EnableStatus" CommandArgument='<%#Eval("ID") %>' CssClass='<%#Eval("Status").ToString() == "1" ? "btn btn-danger danger btn-sm" : "btn btn-success danger btn-sm" %>' Text='<%#Eval("Status").ToString() == "1" ? "禁用" : "启用" %>'></asp:Button>

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
            </asp:UpdatePanel>

            <tr>
                <td class="li_Saletitle">
                    <input runat="server" type="text" class="form-control" id="txtTypeName" /></td>
                <td>
                    <asp:Button runat="server" ID="bntAddApplyType" Text="添加" CssClass="btn btn-success btn-sm" OnClick="bntAddApplyType_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
