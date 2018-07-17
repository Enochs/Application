<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.ProductList" Title="物料单" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script src="/Scripts/jquery.PrintArea.js"></script>

    <script type="text/javascript">
        $(function(){
            if('<%=Request["OnlyView"]%>')
            {
                $("input,textarea,select").attr("disabled", "disabled");
                $("input[type=button],.btn").hide();
            }
        });
       
        function PrintNode(Control)
        {

            var mode = "popup";
            var close = mode == "popup";

            var options = { mode: mode, popClose: close };

            $("#Node"+Control).printArea(options);
        }

        function PrintWarHouseNode()
        {
            var mode = "popup";
            var close = mode == "popup";

            var options = { mode: mode, popClose: close };

            $("#WarHouseContent").printArea(options);
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <br />
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <asp:Repeater ID="repTypeList" runat="server" OnItemDataBound="repTypeList_ItemDataBound" OnItemCommand="repTypeList_ItemCommand">
        <ItemTemplate>
            <asp:PlaceHolder ID="phSupplierItem" runat="server">
            <a style="display:none" href="/AdminPanlWorkArea/Carrytask/ProductListPrintView.aspx?Typer=1&CustomerID=<%=Request["CustomerID"].ToInt32()%>&DispatchingID=<%=Request["DispatchingID"].ToInt32()%>" target="_blank" class="btn btn-mini btn-primary">打印预览</a>
            <input id="Button1" type="button" value="打印" class="btn btn-primary btn-mini" onclick="PrintNode(<%#Container.ItemIndex %>);" />
            <asp:Button ID="Button2" runat="server" class="btn btn-primary btn-mini" Text="导出到Excel" CommandArgument='<%#Eval("KeyName")  %>' CommandName="ExporttoExcel" />
            <div id="Node<%#Container.ItemIndex %>">
                <b>供应商：<asp:Label ID="lblKeyName" runat="server" Text='<%#Eval("KeyName") %>'></asp:Label>　　供应商电话：<%#Eval("Phone") %></b><table class="table table-bordered table-striped">
                    <tr>
                        <th width="100">类别</th>
                        <th width="100">项目</th>
                        <th width="100">产品服务内容</th>
                        <th width="100">具体要求</th>
                        <th width="100">图片</th>
                        <th width="100">成本价</th>
                        <th width="100">数量</th>
                        <th width="100">小计</th>
                        <th width="100">责任人</th>
                        <th width="100">备注</th>
                    </tr>
                    <asp:Repeater ID="repProductList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("ParentCategoryName") %></td>
                                <td><%#Eval("CategoryName") %></td>
                                <td><%#Eval("ServiceContent") %></td>
                                <td><%#Eval("Requirement") %></td>
                                <td><a href="#">查看资料</a></td>
                                <td><%#Eval("PurchasePrice") %></td>
                                <td><%#Eval("Quantity") %></td>
                                <td><%#Eval("Subtotal") %></td>
                                <td><%# GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#Eval("Remark") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                合计：<asp:Label ID="lblPriceSum" runat="server" Text=""></asp:Label>
                结算价：<asp:TextBox ID="txtPlanCost" MaxLength="8" runat="server" Text='<%#GetPlanCostBySupplyName(Eval("KeyName").ToString()) %>'></asp:TextBox>
                <asp:Button ID="btnSavePlanCost" runat="server" CssClass="btn btn-success btn-mini" Text="保存" CommandArgument='<%#Eval("KeyName")  %>' CommandName="SavPlanCost" />
            </div>
                </asp:PlaceHolder>
        </ItemTemplate>
    </asp:Repeater>
    <a href="/AdminPanlWorkArea/Carrytask/ProductListPrintView.aspx?Typer=2&CustomerID=<%=Request["CustomerID"].ToInt32()%>&DispatchingID=<%=Request["DispatchingID"].ToInt32()%>" target="_blank" class="btn btn-mini btn-primary">打印预览</a>
    <input id="Button1" style="display:none" type="button"  value="打印领料单" class="btn btn-primary btn-mini" onclick="PrintWarHouseNode('WarHouseContent');" />
    <asp:Button ID="btnExportformWareHouse" runat="server" class="btn btn-primary btn-mini" Text="导出到Excel" OnClick="btnExportformWareHouse_Click" />
    <div id="WarHouseContent">
        <div id="carrytasktitle" style="display: none">
            <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
        </div>
        <b>库房：</b>
        <table class="table table-bordered table-striped">
            <tr>
                <th style="width: 100px;">类别</th>
                <th style="width: 100px;">项目</th>
                <th style="width: 100px;">产品服务内容</th>
                <th style="width: 100px;">具体要求</th>
                <th style="width: 100px;">图片</th>
                <th style="width: 100px;">单价</th>
                <th style="width: 100px;">数量</th>
                <th style="width: 100px;">小计</th>
                <th style="width: 100px;">责任人</th>
                <th style="width: 100px;">备注</th>
            </tr>
            <asp:Repeater ID="repWareHouseList" runat="server">
                <ItemTemplate>
                    <tr>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("ParentCategoryName") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("CategoryName") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("ServiceContent") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Requirement") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><a href="#">查看资料</a></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("PurchasePrice") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Quantity") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Subtotal") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %> id="Partd<%#Container.ItemIndex %>">

                            <%# GetEmployeeName(Eval("EmpLoyeeID")) %>    </td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Remark") %></td>
                    </tr>
                </ItemTemplate>

            </asp:Repeater>
            <tr>
                <td colspan="10">合计：<asp:Label ID="lblWareSumPrice" runat="server" Text=""></asp:Label>
                    计划支出：<asp:TextBox ID="txtPlanCost" MaxLength="8" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSavePlanCost" CssClass="btn btn-success btn-mini" runat="server" Text="保存" CommandArgument='<%#Eval("KeyName")  %>' CommandName="SavPlanCost" OnClick="btnSavePlanCost_Click1" />
                </td>
            </tr>
        </table>




    </div>
    <div id="DivNewADD">
        <asp:Button ID="btnEcportforNewadd" class="btn btn-primary btn-mini" runat="server" Text="导出到Excel" OnClick="btnEcportforNewadd_Click" />
        <br /><b>新购入：</b>
        <table class="table table-bordered table-striped">
            <tr>
                <th style="width: 100px;">类别</th>
                <th style="width: 100px;">项目</th>
                <th style="width: 100px;">产品服务内容</th>
                <th style="width: 100px;">具体要求</th>
                <th style="width: 100px;">图片</th>
                <th style="width: 100px;">单价</th>
                <th style="width: 100px;">数量</th>
                <th style="width: 100px;">小计</th>
                <th style="width: 100px;">责任人</th>
                <th style="width: 100px;">备注</th>
            </tr>
            <asp:Repeater ID="repNewadd" runat="server">
                <ItemTemplate>
                    <tr>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("ParentCategoryName") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("CategoryName") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("ServiceContent") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Requirement") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><a href="#">查看资料</a></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("PurchasePrice") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Quantity") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Subtotal") %></td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>>
                            <%# GetEmployeeName(Eval("EmpLoyeeID")) %>    </td>
                        <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Remark") %></td>
                    </tr>
                </ItemTemplate>

            </asp:Repeater>
            <tr>
                <td colspan="10">合计：<asp:Label ID="lblNewaddSum" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>




    </div>

</asp:Content>

