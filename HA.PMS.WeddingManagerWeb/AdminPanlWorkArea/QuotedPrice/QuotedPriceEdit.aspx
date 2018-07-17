<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceEdit.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceEdit" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        $(document).ready(function () {
            findDimensions();                  //调用函数，获取数值
            window.onresize = findDimensions;
            showPopuWindows($("#SelectPG").attr("href"), 800, 600, "a#SelectPG");

        });

        $(document).ready(function () {
            $(".SelectSG").each(function () {
                findDimensions();                  //调用函数，获取数值
                window.onresize = findDimensions;
                showPopuWindows($(this).attr("href"), 800, 600, $(this));
                $("#hideSecondCategoryID").attr("value", $(this).attr("CategoryID"));
            });
        });


        function tests() {


        }
    </script>
    <table>
        <tr>
            <td>客户基本信息</td>
        </tr>
    </table>
    <table style="width: 100%; border: solid;" border="1">
        <tr>
            <td><a id="SelectPG" class="SelectPG" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=0&ControlKey=hidePgValue&Callback=btnStarFirstpg">选择类别</a>
                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hidePgValue" />
            </td>
            <td>
                <asp:Button ID="btnStarFirstpg" ClientIDMode="Static" runat="server" Text="生成一级分类" OnClick="btnStarFirstpg_Click" />&nbsp;</td>
            <td>
                <asp:Button ID="btnCreateSecond" ClientIDMode="Static" runat="server" Text="生成二级分类" OnClick="btnCreateSecond_Click" />
                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSecondValue" />
                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSecondCategoryID" />
            </td>
            <td>
                <asp:Button ID="btnCreateThired" ClientIDMode="Static" runat="server" Text="生成产品" OnClick="btnCreateThired_Click" />
                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdValue" />

            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>

    <asp:Repeater ID="reppgfirst" runat="server" OnItemCommand="reppgfirst_ItemCommand" OnItemDataBound="reppgfirst_ItemDataBound">
        <ItemTemplate>
            <table style="width: 100%; border: double;" border="1">
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
                    <td>操作</td>
                </tr>
                <tr>
                    <td class="auto-style1"><%#Eval("CategoryName") %><asp:HiddenField ID="hideCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                    </td>
                    <td class="auto-style1" colspan="10">
                        <asp:Repeater ID="repCGFirst" runat="server">
                            <ItemTemplate>
                                <table class="auto-style2" border="1">
                                    <tr>
                                        <td>
                                            <a id="SelectSG" categoryid="<%#Eval("CategoryID") %>" class="SelectSG" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=<%#Eval("CategoryID") %>&ControlKey=hideSecondValue&Callback=btnCreateSecond">选择项目</a>
                                        </td>
                                        <td>无</td>

                                        <td>
                                            <asp:TextBox ID="txtServiceContent" runat="server" Text='<%#Eval("ServiceContent") %>'></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRequirement" runat="server" Text='<%#Eval("Requirement") %>'></asp:TextBox></td>
                                        <td>暂无</td>
                                        <td>
                                            <asp:TextBox ID="txtUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:TextBox></td>

                                        <td width="50">
                                            <asp:TextBox ID="txtUnit" Width="30" runat="server" Text='<%#Eval("Unit") %>'></asp:TextBox>
                                        </td>
                                        <td width="50">
                                            <asp:TextBox ID="txtspecifications" Width="30" runat="server" Text='<%#Eval("Specifications") %>'></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtSubtotal" runat="server" Text='<%#Eval("Subtotal") %>'></asp:TextBox></td>
                                        <td>
                                            <asp:TextBox ID="txtRemark" runat="server" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                                        <td>
                                            <asp:LinkButton ID="LinkButton1" runat="server">删除</asp:LinkButton></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="repCgSecondList" runat="server" OnItemDataBound="repCgSecondList_ItemDataBound">
                            <ItemTemplate>
                                <table class="auto-style2" border="1">
                                    <tr>
                                        <td><%#Eval("CategoryName") %><asp:HiddenField runat="server" ID="hideThiredCategoryID" Value='<%#Eval("CategoryID") %>' />
                                        </td>
                                        <td>

                                            <asp:Repeater ID="repProduct" runat="server">
                                                <ItemTemplate>
                                                    <table class="auto-style2" border="1">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="txtProductName" runat="server" Text='<%#GetProductByID(Eval("ProductID")) %>'></asp:TextBox></td>
                                                            <asp:HiddenField ID="hideProductID" Value='<%#Eval("ProductID") %>' runat="server" />
                                                            <td>
                                                                <asp:TextBox ID="txtServiceContent" runat="server" Text='<%#Eval("ServiceContent") %>'></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtRequirement" runat="server" Text='<%#Eval("Requirement") %>'></asp:TextBox></td>
                                                            <td>暂无</td>
                                                            <td>
                                                                <asp:TextBox ID="txtUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:TextBox></td>

                                                            <td width="50">
                                                                <asp:TextBox ID="txtUnit" Width="30" runat="server" Text='<%#Eval("Unit") %>'></asp:TextBox>
                                                            </td>
                                                            <td width="50">
                                                                <asp:TextBox ID="txtspecifications" Width="30" runat="server" Text='<%#Eval("Specifications") %>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtSubtotal" runat="server" Text='<%#Eval("Subtotal") %>'></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtRemark" runat="server" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton2" runat="server">删除</asp:LinkButton></td>
                                                        </tr>

                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Repeater ID="repThiredList" runat="server">
                                                <ItemTemplate>
                                                    <table class="auto-style2" border="1">
                                                        <tr>
                                                            <td><a id="SelectSG" categoryid="<%#Eval("CategoryID") %>" class="SelectSG" href="/AdminPanlWorkArea/ControlPage/SelectProduct.aspx?CategoryID=<%#Eval("CategoryID") %>&ControlKey=hideThirdValue&Callback=btnCreateThired&CustomerID=<%=Request["CustomerID"] %>">选择产品</a></td>
                                                            <td>
                                                                <asp:TextBox ID="txtServiceContent" runat="server" Text='<%#Eval("ServiceContent") %>'></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtRequirement" runat="server" Text='<%#Eval("Requirement") %>'></asp:TextBox></td>
                                                            <td>暂无</td>
                                                            <td>
                                                                <asp:TextBox ID="txtUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:TextBox></td>
                                                            <td width="50">
                                                                <asp:TextBox ID="txtUnit" Width="30" runat="server" Text='<%#Eval("Unit") %>'></asp:TextBox>
                                                            </td>
                                                            <td width="50">
                                                                <asp:TextBox ID="txtspecifications" Width="30" runat="server" Text='<%#Eval("Specifications") %>'></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtSubtotal" runat="server" Text='<%#Eval("Subtotal") %>'></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="txtRemark" runat="server" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                                                            <td>
                                                                <asp:LinkButton ID="LinkButton1" runat="server">删除</asp:LinkButton></td>
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
        </ItemTemplate>
    </asp:Repeater>
    <table style="width: 100%;" border="1">
        <tr>
            <td>
                <asp:Button ID="btnSaveallChange" runat="server" Text="保存报价单" OnClick="btnSaveallChange_Click" />
            </td>
        </tr>
    </table>
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

