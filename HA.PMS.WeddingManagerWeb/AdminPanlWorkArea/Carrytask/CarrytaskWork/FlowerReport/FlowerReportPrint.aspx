<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowerReportPrint.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.FlowerReport.FlowerReportPrint" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" StylesheetTheme="None" %>


<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <script src="/Scripts/jquery.PrintArea.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnPrint").click(function () {

                var mode = "popup";
                var close = mode == "popup";

                var options = { mode: mode, popClose: close };

                $("#PrintNode").printArea(options);
            });

        });
    </script>
    <asp:Button ID="btnExport" runat="server" Text="导出" Height="29" OnClick="btnExport_Click" CssClass="btn btn-info" />

    <asp:Button ID="btnPrint" ClientIDMode="Static" runat="server" Text="打印" Height="29" CssClass="btn btn-info" />
    <table id="PrintNode" style="text-align: center; width: 100%; border-color: black;" border="1" class="table table-bordered table-striped">

        <tr>
            <th>鲜花名称</th>
            <th>采购单价</th>
            <th>单位</th>
            <th style="display: none;">销售单价</th>
            <th>数量</th>
            <th>总成本</th>
            <th style="display: none;">销售总价</th>
            <th>说明</th>
            

        </tr>
        <asp:Repeater ID="repFlowerPlanning" runat="server">

            <ItemTemplate>
                <tr>
                    <td><%#Eval("FLowername") %>
                      
                    </td>

                    <td>
                        <%#Eval("CostPrice") %></td>
                    <td>
                        <%#Eval("Unite") %></td>
                    <td style="display: none;">
                        <%#Eval("SalePrice") %></td>
                    <td>
                        <%#Eval("Quantity") %></td>
                    <td>
                        <%#Eval("CostSumPrice") %></td>
                    <td style="display: none;">
                        <%#Eval("SaleSumPrice") %></td>
                    <td>
                        <%#Eval("Node") %></td>


                </tr>
            </ItemTemplate>

        </asp:Repeater>
        <tr>
            <td colspan="8">成本合计:<asp:Label ID="lblSumMoney" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>

</asp:Content>
