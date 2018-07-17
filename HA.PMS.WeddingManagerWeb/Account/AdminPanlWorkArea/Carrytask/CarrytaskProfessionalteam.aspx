<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskProfessionalteam.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskProfessionalteam" Title="专业技术团队"  MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <asp:Button ID="btnExporttoExcel" CssClass="btn btn-primary" runat="server" Text="导出到Excel" OnClick="btnExporttoExcel_Click" />
    <table class="table table-bordered table-striped">
        <tr>
            <th style="width: 100px;">类别</th>
            <th style="width: 100px;">项目</th>
            <th style="width: 100px;">产品服务内容</th>
            <th style="width: 100px;">具体要求</th>
            <th style="width: 100px;">图片</th>
            <th style="width: 100px;">成本价</th>
            <th style="width: 100px;">数量</th>
            <th style="width: 100px;">小计</th>
            <th style="width: 100px;">责任人</th>
            <th style="width: 100px;">备注</th>
        </tr>
        <asp:Repeater ID="repProductList" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("ParentCategoryName") %></td>
                    <td><%#Eval("CategoryName") %></td>
                    <td><%#GetProductByID(Eval("ProductID")) %></td>
                    <td><%#Eval("Requirement") %></td>
                    <td><a href="#" class="btn btn-info btn-mini">查看资料</a></td>
                    <td><%#Eval("PurchasePrice") %></td>
                    <td><%#Eval("Quantity") %></td>
                    <td><%#Eval("Subtotal") %></td>
                    <td id="Partd<%#Container.ItemIndex %>">
                        <%#GetEmployeeName(Eval("EmpLoyeeID")) %>
                    </td>
                    <td><%#Eval("Remark") %></td>
                </tr>
            </ItemTemplate> 
        </asp:Repeater>
        <tr>
            <td colspan="10">
                合计:<asp:Label ID="lblMoneySum" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
</asp:Content>

