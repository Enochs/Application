<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FD_QuotedCatgoryProduct.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.FD_QuotedCatgoryProduct" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Src="../../Control/SelectProduct.ascx" TagName="SelectProduct" TagPrefix="uc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <script>

        $(document).ready(function () {

            $(":checkbox").click(function () {
                if ($(this).is(":checked")) {
                    var Productname = $(this).parent().parent("tr").children("td").eq(1).text();
                    $("#txtProductList").text($("#txtProductList").text() + Productname + "\r\n");
                    $("#hideSelectProduct").attr("value", $("#hideSelectProduct").val() + "," + $(this).val());


                } else {
                    var Productname = $(this).parent().parent("tr").children("td").eq(1).text();
                    $("#txtProductList").text($("#txtProductList").text().replace(Productname + "\r\n", ""));
                    $("#hideSelectProduct").attr("value", $("#hideSelectProduct").val().replace("," + $(this).val(), ""));

                }
            })

        });
    </script>
    <table>
        <tr style="vertical-align: top; text-align: left;">
            <td>
                <asp:TreeView ID="treeCatogryList" runat="server" OnSelectedNodeChanged="treeCatogryList_SelectedNodeChanged" ShowLines="True" ExpandDepth="0"></asp:TreeView>
            </td>
            <td>
                <table id="TablerepProductByCatogryforWarehouseList" border="1" cellpadding="5" cellspacing="1" class="Productdiv">
                    <tr>
                        <td>已选产品</td>
                        <td colspan="2">
                            <asp:TextBox ID="txtProductList" runat="server" ClientIDMode="Static" Height="80" TextMode="MultiLine" Width="310"></asp:TextBox>
                            <asp:HiddenField ID="hideSelectProduct" runat="server" ClientIDMode="Static" />
                        </td>
                    </tr>
                    <tr>
                        <td>选择</td>
                        <td>产品</td>

                        <td>价格</td>

                    </tr>
                    <asp:Repeater ID="repProductByCatogryforWarehouseList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td bgcolor="#FFFFEE" style="width: 30px;">
                                    <input runat="server" id="chkProduct" type="checkbox" value='<%#Eval("Keys") %>' /></td>
                                <td bgcolor="#FFFFEE" class="ProductName" style="width: 100px;"><%#Eval("ProductName") %></td>

                                <td><%#Eval("SalePrice") %></td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <asp:Button ID="btnSaveSelect" runat="server" ClientIDMode="Static" CssClass="btn btn-success" OnClick="btnSaveSelect_Click" Text="保存" />
            </td>
        </tr>
    </table>

</asp:Content>
