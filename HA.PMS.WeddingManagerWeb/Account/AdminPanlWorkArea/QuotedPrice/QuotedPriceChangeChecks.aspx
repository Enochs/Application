<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceChangeChecks.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceChangeChecks" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" Title="审核变更单" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <asp:Repeater ID="reppgfirst" runat="server" OnItemDataBound="reppgfirst_ItemDataBound">
        <ItemTemplate>
            <table border="1" style="width: 100%; border: double;">
                <tr>
                    <td>类别</td>
                    <td>项目</td>
                    <td>产品</td>
                    <td>产品服务内容</td>
                    <td>具体要求</td>
                    <td>图片</td>
                    <td>单价</td>
                    <th width="50">单位</th>
                    <th width="50">规格</th>
                    <td>数量</td>
                    <td>小计</td>
                    <td>备注</td>

                </tr>
                <tr>
                    <td class="auto-style1"><%#Eval("CategoryName") %>
                        <asp:HiddenField ID="hideCategoryID" runat="server" Value='<%#Eval("CategoryID") %>' />
                    </td>
                    <td class="auto-style1" colspan="10">
                        <asp:Repeater ID="repCGFirst" runat="server">
                            <ItemTemplate>
                                <table border="1" class="auto-style2">
                                    <tr>
                                        <td>无 </td>
                                        <td>无</td>
                                        <td>
                                            <asp:Label ID="txtServiceContent" runat="server" Text='<%#Eval("ServiceContent") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtRequirement" runat="server" Text='<%#Eval("Requirement") %>'></asp:Label>
                                        </td>
                                        <td>暂无</td>
                                        <td>
                                            <asp:Label ID="txtUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:Label>
                                        </td>
                                        <td width="50">
                                            <asp:TextBox ID="txtUnit" Width="30" runat="server" Text='<%#Eval("Unit") %>'></asp:TextBox>
                                        </td>
                                        <td width="50">
                                            <asp:TextBox ID="txtspecifications" Width="30" runat="server" Text='<%#Eval("Specifications") %>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtSubtotal" runat="server" Text='<%#Eval("Subtotal") %>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtRemark" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                                        </td>

                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="repCgSecondList" runat="server" OnItemDataBound="repCgSecondList_ItemDataBound">
                            <ItemTemplate>
                                <table border="1" class="auto-style2">
                                    <tr>
                                        <td><%#Eval("CategoryName") %>
                                            <asp:HiddenField ID="hideThiredCategoryID" runat="server" Value='<%#Eval("CategoryID") %>' />
                                        </td>
                                        <td>
                                            <asp:Repeater ID="repProduct" runat="server">
                                                <ItemTemplate>
                                                    <table border="1" class="auto-style2">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="txtProductName" runat="server" Text='<%#GetProductByID(Eval("ProductID")) %>'></asp:Label>
                                                            </td>
                                                            <asp:HiddenField ID="hideProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                                                            <td>
                                                                <asp:Label ID="txtServiceContent0" runat="server" Text='<%#Eval("ServiceContent") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtRequirement0" runat="server" Text='<%#Eval("Requirement") %>'></asp:Label>
                                                            </td>
                                                            <td>暂无</td>
                                                            <td>
                                                                <asp:Label ID="txtUnitPrice0" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:Label>
                                                            </td>
                                                            <td width="50">
                                                                <asp:TextBox ID="txtUnit" Width="30" runat="server" Text='<%#Eval("Unit") %>'></asp:TextBox>
                                                            </td>
                                                            <td width="50">
                                                                <asp:TextBox ID="txtspecifications" Width="30" runat="server" Text='<%#Eval("Specifications") %>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtQuantity0" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtSubtotal0" runat="server" Text='<%#Eval("Subtotal") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtRemark0" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                                                            </td>

                                                        </tr>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Repeater ID="repThiredList" runat="server">
                                                <ItemTemplate>
                                                    <table border="1" class="auto-style2">
                                                        <tr>
                                                            <td><a id="SelectSG0" categoryid='<%#Eval("CategoryID") %>' class="SelectSG" href='../ControlPage/SelectProduct.aspx?CustomerID=<%=Request["CustomerID"] %>&CategoryID=<%#Eval("CategoryID") %>&amp;ControlKey=hideThirdValue&amp;Callback=btnCreateThired'>选择产品</a></td>
                                                            <td>
                                                                <asp:Label ID="txtServiceContent1" runat="server" Text='<%#Eval("ServiceContent") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtRequirement1" runat="server" Text='<%#Eval("Requirement") %>'></asp:Label>
                                                            </td>
                                                            <td>暂无</td>
                                                            <td>
                                                                <asp:Label ID="txtUnitPrice1" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:Label>
                                                            </td>
                                                            <td width="50">
                                                                <asp:TextBox ID="txtUnit" Width="30" runat="server" Text='<%#Eval("Unit") %>'></asp:TextBox>
                                                            </td>
                                                            <td width="50">
                                                                <asp:TextBox ID="txtspecifications" Width="30" runat="server" Text='<%#Eval("Specifications") %>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtQuantity1" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtSubtotal1" runat="server" Text='<%#Eval("Subtotal") %>'></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="txtRemark1" runat="server" Text='<%#Eval("Remark") %>'></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
            <br />

        </ItemTemplate>
    </asp:Repeater>
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Button ID="btnChecks" runat="server" Text="审核" OnClick="btnChecks_Click" />
                <asp:Button ID="btnEdit" runat="server" Text="我要修改" OnClick="btnEdit_Click" />
            </td>
        </tr>
        <tr>
            <td>审核意见</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtChecksContent" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>审核说明</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtChecktitle" runat="server"></asp:TextBox></td>
        </tr>
    </table>
    <HA:MessageBoard runat="server" ClassType="QuotedPriceChecks" ID="MessageBoard" />
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }

        .auto-style2 {
            width: 100%;
            border-style: solid;
            border-width: 1px;
        }
    </style>
</asp:Content>
