<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="SupplierReport.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.WorkReport.SupplierReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Repeater ID="repTypeList" runat="server" OnItemCommand="repTypeList_ItemCommand">
        <ItemTemplate>
            <b>供应商:
                <asp:Label ID="lblKeyName" runat="server" Text='<%#Eval("KeyName") %>'></asp:Label></b>
            <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("Key") %>' />
            <asp:Button ID="Button2" runat="server" class="btn btn-primary btn-mini" Text="导出到Excel" CommandArgument='<%#Eval("KeyName")  %>' CommandName="ExporttoExcel" />
            <table class="table table-bordered table-striped">
                <tr>
                    <th width="100">类别</th>
                    <th width="100">项目</th>
                    <th width="100">产品服务内容</th>
                    <th width="100">具体要求</th>
                    <th width="100">图片</th>
                    <th width="100">单价</th>
                    <th width="100">数量</th>
                    <th width="100">小计</th>
                    <th width="100">备注</th>
                </tr>
                <asp:Repeater ID="repProductList" runat="server" OnItemCommand="repProductList_ItemCommand">
                    <ItemTemplate>
                        <tr>

                            <td><%#Eval("ParentCategoryName") %>
                                <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("ProeuctKey") %>' />
                            </td>
                            <td><%#Eval("CategoryName") %></td>
                            <td><%#GetProductByID(Eval("ProductID")) %></td>
                            <td><%#Eval("Requirement") %></td>
                            <td><a href="#">查看资料</a></td>
                            <td>
                                <asp:TextBox ID="txtUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtSubtotal" runat="server" Text='<%#Eval("Subtotal") %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtRemark" runat="server" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td>合计（计划支出）:<asp:Label ID="lblSumTotal" runat="server" Text=""></asp:Label></td>
                    <td>实际支出<asp:TextBox ID="txtFinishCost" runat="server"></asp:TextBox><asp:Button ID="btnSaveCost" CommandName="SaveCost" runat="server" Text="保存修改" CssClass="btn btn-primary" /></td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
