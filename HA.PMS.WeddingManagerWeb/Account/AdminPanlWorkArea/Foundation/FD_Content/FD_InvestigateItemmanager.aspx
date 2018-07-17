<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_InvestigateItemmanager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.FD_InvestigateItemmanager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">

        <div class="widget-content">
            <table style="float:left;width:50%;">
                <tr>
                    <td style="vertical-align: top;">
                        <table class="table table-bordered table-striped table-select" style="width: 500px;">
                            <thead>
                                <tr>
                                    <th>回访</th>
                                    <th>操作</th>

                                </tr>
                            </thead>
                            <tbody style="vertical-align: top;">
                                <asp:Repeater ID="repItem" OnItemCommand="repItem_ItemCommand" runat="server">
                                    <ItemTemplate>

                                        <tr skey='<%#Eval("ItemKey") %>'>
                                            <td>
                                                <asp:TextBox ID="txtName" MaxLength="20" Text='<%#Eval("ItemTitle") %>' Width="130" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lkbtnSave" CssClass="btn btn-primary btn-mini" CommandName="Delete" CommandArgument='<%#Eval("ItemKey") %>' runat="server" OnClientClick='return confirm("你确定要删除该项吗")'>删除</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton1" OnClientClick="return ValidateThis(this);" CssClass="btn btn-primary btn-mini" CommandName="Edit" CommandArgument='<%#Eval("ItemKey") %>' runat="server">保存修改</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                </asp:Repeater>

                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtTitle" check="1" tip="限20个字符！" Width="130" MaxLength="20" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSave" OnClientClick="return CheckSuccess(this);" OnClick="btnSave_Click" runat="server" Text="保存" CssClass="btn  btn-success" />
                                    </td>

                                </tr>
                            </tfoot>
                        </table>
                    </td>

                </tr>
            </table>
            <table style="width:50%;">
                <tr>
                    <td style="vertical-align: top;">
                        <table class="table table-bordered table-striped table-select" style="width: 500px;">
                            <thead>
                                <tr>
                                    <th>回访状态</th>
                                    <th>操作</th>

                                </tr>
                            </thead>
                            <tbody style="vertical-align: top;">
                                <asp:Repeater ID="repItemState" OnItemCommand="repItemState_ItemCommand" runat="server">
                                    <ItemTemplate>

                                        <tr skey='<%#Eval("InviteStateID") %>'>
                                            <td>
                                                <asp:TextBox ID="txtName" MaxLength="20" Text='<%#Eval("Name") %>' Width="130" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lkbtnSave" CssClass="btn btn-primary btn-mini" CommandName="Delete" CommandArgument='<%#Eval("InviteStateID") %>' runat="server" OnClientClick='return confirm("你确定要删除该项吗")'>删除</asp:LinkButton>
                                                <asp:LinkButton ID="bntUpdates" OnClientClick="return ValidateThis(this);" CssClass="btn btn-primary btn-mini" CommandName="Edit" CommandArgument='<%#Eval("InviteStateID") %>' runat="server">保存修改</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>

                                </asp:Repeater>

                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtNames" check="1" tip="限20个字符！" Width="130" MaxLength="20" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_Saves" OnClientClick="return CheckSuccess(this);" OnClick="btn_Saves_Click" runat="server" Text="保存" CssClass="btn  btn-success" />
                                    </td>

                                </tr>
                            </tfoot>
                        </table>
                    </td>

                </tr>
            </table>
        </div>
    </div>
</asp:Content>
