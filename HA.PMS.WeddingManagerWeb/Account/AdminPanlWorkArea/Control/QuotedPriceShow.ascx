<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceShow.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.QuotedPriceShow" %>
<script type="text/javascript">
    function ShowPrice()
    {
        $("#QuotedShow").show();
    }

    function HidePrice() {
        $("#QuotedShow").hide();
    }
</script>
<table style="width:100%">
    <tr>
        <td>
            <input id="btnShowPrice" onclick="ShowPrice();" type="button" value="显示报价单" /></td>
        <td>
            <input id="btnShowPrice0" onclick="HidePrice();" type="button" value="隐藏报价单" /></td>
    </tr>
</table>
<div id="QuotedShow" style="display:none;">


    <asp:Repeater ID="reppgfirst" runat="server" OnItemCommand="reppgfirst_ItemCommand" OnItemDataBound="reppgfirst_ItemDataBound">
        <ItemTemplate>
            <table width="1100" border="0" cellspacing="0" cellpadding="0">
                <thead>
                    <tr>
                        <td style="text-align: center;" width="100">类别</td>
                        <td style="text-align: center;" width="100">项目</td>
                        <td style="text-align: center;" width="100">产品</td>
                        <td style="text-align: center;" width="100">产品服务内容</td>
                        <td style="text-align: center;" width="100">具体要求</td>
                        <td style="text-align: center;" width="100">图片</td>
                        <td style="text-align: center;" width="100">单价</td>
                        <td style="text-align: center;" width="100">数量</td>
                        <td style="text-align: center;" width="100">小计</td>
                        <td style="text-align: center;" width="100">备注</td>

                    </tr>
                </thead>
                <tr>
                    <td class="auto-style1"><%#Eval("CategoryName") %>
                        <asp:HiddenField ID="hideCategoryID" runat="server" Value='<%#Eval("CategoryID") %>' />
                    </td>
                    <td class="auto-style1" colspan="10">
                        <asp:Repeater ID="repCGFirst" runat="server">
                            <ItemTemplate>
                                <table border="1" class="auto-style2">
                                    <tr>
                                        <td style="text-align: center;" width="100">无 </td>
                                        <td style="text-align: center;" width="100">无</td>
                                        <td style="text-align: center;" width="100">
                                            <asp:Label ID="txtServiceContent" runat="server" Text='<%#Eval("ServiceContent") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center;" width="100">
                                            <asp:Label ID="txtRequirement" runat="server" Text='<%#Eval("Requirement") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center;" width="100">暂无</td>
                                        <td style="text-align: center;" width="100">
                                            <asp:Label ID="txtUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center;" width="100">
                                            <asp:Label ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center;" width="100">
                                            <asp:Label ID="txtSubtotal" runat="server" Text='<%#Eval("Subtotal") %>'></asp:Label>
                                        </td>
                                        <td style="text-align: center;" width="100">
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
                                        <td style="text-align: center;" width="100"><%#Eval("CategoryName") %>
                                            <asp:HiddenField ID="hideThiredCategoryID" runat="server" Value='<%#Eval("CategoryID") %>' />
                                        </td>
                                        <td style="text-align: center;" width="100">
                                            <asp:Repeater ID="repProduct" runat="server">
                                                <ItemTemplate>
                                                    <table border="1" class="auto-style2">
                                                        <tr>
                                                            <td style="text-align: center;" width="100">
                                                                <asp:Label ID="txtProductName" runat="server" Text='<%#Eval("ProductID") %>'></asp:Label>
                                                            </td>
                                                            <asp:HiddenField ID="hideProductID" runat="server" Value='<%#Eval("ProductID") %>' />
                                                            <td style="text-align: center;" width="100">
                                                                <asp:Label ID="txtServiceContent0" runat="server" Text='<%#Eval("ServiceContent") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: center;" width="100">
                                                                <asp:Label ID="txtRequirement0" runat="server" Text='<%#Eval("Requirement") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: center;" width="100">暂无</td>
                                                            <td style="text-align: center;" width="100">
                                                                <asp:Label ID="txtUnitPrice0" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: center;" width="100">
                                                                <asp:Label ID="txtQuantity0" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: center;" width="100">
                                                                <asp:Label ID="txtSubtotal0" runat="server" Text='<%#Eval("Subtotal") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: center;" width="100">
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
                                                            <td style="text-align: center;" width="100"><a id="SelectSG0" categoryid='<%#Eval("CategoryID") %>' class="SelectSG" href='../ControlPage/SelectProduct.aspx?CustomerID=<%=Request["CustomerID"] %>&CategoryID=<%#Eval("CategoryID") %>&amp;ControlKey=hideThirdValue&amp;Callback=btnCreateThired'>选择产品</a></td>
                                                            <td style="text-align: center;" width="100">
                                                                <asp:Label ID="txtServiceContent1" runat="server" Text='<%#Eval("ServiceContent") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: center;" width="100">
                                                                <asp:Label ID="txtRequirement1" runat="server" Text='<%#Eval("Requirement") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: center;" width="100">暂无</td>
                                                            <td style="text-align: center;" width="100">
                                                                <asp:Label ID="txtUnitPrice1" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: center;" width="100">
                                                                <asp:Label ID="txtQuantity1" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: center;" width="100">
                                                                <asp:Label ID="txtSubtotal1"
                                                                    runat="server" Text='<%#Eval("Subtotal") %>'></asp:Label>
                                                            </td>
                                                            <td style="text-align: center;" width="100">
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
 </div>