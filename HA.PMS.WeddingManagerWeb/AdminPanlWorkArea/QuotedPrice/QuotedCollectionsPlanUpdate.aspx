<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedCollectionsPlanUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedCollectionsPlanUpdate" Title="修改计划" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CustomerTitle.ascx" TagPrefix="HA" TagName="CustomerTitle" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div>

        <div>
            <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
            <table class="table table-bordered table-striped" style="width:90%;">
                <thead>
                    <tr>
                        <th>计划收款时间</th>
                        <th>计划收款金额</th>
                        <th>实际收款时间</th>
                        <th>实际收款金额</th>
                    </tr>
                </thead>
                <tbody>
                <asp:Repeater ID="repDataKist" runat="server">
                    <ItemTemplate>
                        <tr style="height: 16px;">
                            <td>
                                <asp:HiddenField ID="hidekey" Value='<%#Eval("PlanID") %>' runat="server" />
                                <div <%#Eval("RowLock").ToString()=="False"?"":"style='display:none'" %>>
                                    <cc1:DateEditTextBox onclick="WdatePicker();" MaxLength="20" ID="txtTimer" runat="server" Text=' <%#GetShortDateString(Eval("CreateDate")) %>' Width="90"></cc1:DateEditTextBox>
                                </div>
                                <div <%#Eval("RowLock").ToString()=="True"?"":"style='display:none'" %>>
                                    <%#GetShortDateString(Eval("CreateDate")) %>
                                </div>
                            </td>
                            <td>
                                <div <%#Eval("RowLock").ToString()=="False"?"":"style='display:none'" %>>
                                    <asp:TextBox ID="txtAmount" MaxLength="8"  runat="server" Text='<%#Eval("Amountmoney") %>' Width="90"></asp:TextBox>
                                </div>
                                <div <%#Eval("RowLock").ToString()=="True"?"":"style='display:none'" %>>
                                    <%#Eval("Amountmoney") %>
                                </div>
                            </td>
                            <td><%#GetShortDateString(Eval("CollectionTime")) %></td>
                            <td><%#Eval("RealityAmount") %></td>
                        </tr>
                    </ItemTemplate>

                </asp:Repeater>
                    </tbody>
                <tfoot>
                <tr>
                    <td colspan="4">
                        <asp:Button ID="btnCreate" runat="server" Text="保存修改" OnClick="btnCreate_Click" CssClass="btn btn-success" />
                    </td>

                </tr>
                    </tfoot>
            </table>
        </div>
    </div>
</asp:Content>

