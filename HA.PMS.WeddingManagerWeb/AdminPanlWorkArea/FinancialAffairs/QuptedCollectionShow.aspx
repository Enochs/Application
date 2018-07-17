<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="QuptedCollectionShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs.QuptedCollectionShow" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CustomerTitle.ascx" TagPrefix="HA" TagName="CustomerTitle" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 16px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2">
    <div>

        <div>
            <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
            <table class="table table-bordered table-striped">
                <tr>
                    <th>计划收款时间</th>
                    <th>计划收款金额</th>
                    <th>实际收款时间</th>
                    <th>实际收款金额</th>

                </tr>
                <asp:Repeater ID="repDataKist" runat="server">
                    <ItemTemplate>
                        <tr style="height: 16px;">
                            <td>
                                <asp:HiddenField ID="hidekey" Value='<%#Eval("PlanID") %>' runat="server" />
                                <%#Eval("CreateDate") %>
                            </td>
                            <td>
                                <%#Eval("Amountmoney") %>
                            </td>
                            <td>
                              <%#Eval("CollectionTime") %></td>
                            <td>
                              <%#Eval("RealityAmount") %></td>
           
                        </tr>
                    </ItemTemplate>

                </asp:Repeater>
                <tr>
                    <td colspan="4">
                        余款:<asp:Label ID="lblyukuan" runat="server" Text=""></asp:Label>

                    </td>
                </tr>
 
            </table>
        </div>
    </div>
</asp:Content>

