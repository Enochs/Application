<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="searchCustomer.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.searchCustomer" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CellphoneSelector.ascx" TagPrefix="HA" TagName="CellphoneSelector" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .centerTxt {
            width: 120px;
            height: 25px;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "700px", "height": "700px", "overflow-y": "hidden", "margin": "0", "overflow-x": "hidden", "background-color": "transparent" });
        });
    </script>
    <div id="searchCustomers" style="text-align: left;">
        <div style="width: 700px; height: 700px; overflow: auto; overflow-y: auto; vertical-align: top;">

            <div class="widget-box">
                <div class="widget-title">
                    <span class="icon"><i class="icon-ok"></i></span>
                    <h5>查找新人</h5>
                </div>
                <div class="widget-content">
                    新人姓名：
                    <asp:TextBox ID="txtCustomer" runat="server" CssClass="centerTxt" MaxLength="10"></asp:TextBox>
                    <%--<HA:CstmNameSelector runat="server" ID="CstmNameSelector" />--%>
                    <br />
                    联系电话：
                    <asp:TextBox ID="txtCelPhone" runat="server" MaxLength="16"></asp:TextBox>
                    <%--<HA:CellphoneSelector runat="server" ID="CellphoneSelector" />--%>
                    <br />
                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-success" Text="查找" runat="server" />
                </div>
            </div>



            <asp:Repeater runat="server" ID="repCustomerList" Visible="false">
                <HeaderTemplate>
                    <table border="1" style="width: 100%;">
                        <tr>
                            <td>联系人姓名</td>
                            <td>婚期</td>
                            <td>查看</td>
                        </tr>
                    </ItemTemplate>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("ContactMan") %></td>
                        <td><%#Eval("PartyDate") %></td>
                        <td><a class="btn btn-success" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>" target="_blank">查看新人信息</a></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>

                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
