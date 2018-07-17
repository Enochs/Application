<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyCarryTask.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.MyCarryTask" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<script>
    function ShowPopu(Parent) {
        var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
        showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
        $("#SelectEmpLoyeeBythis").click();
    }
</script>
<div>
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <asp:Repeater ID="repTypeList" runat="server" OnItemDataBound="repTypeList_ItemDataBound">
        <ItemTemplate>
           <b> 供应商:<%#Eval("KeyName") %></b>
            <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("Key") %>' />
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
                <asp:Repeater ID="repProductList" runat="server">
                    <ItemTemplate>
                        <tr  <%#GetBorderStyle(Eval("NewAdd")) %>>
                            <td><%#Eval("ParentCategoryName") %></td>
                            <td><%#Eval("CategoryName") %></td>
                            <td><%#GetProductByID(Eval("ProductID")) %></td>
                            <td><%#Eval("Requirement") %></td>
                            <td><a href="#">查看资料</a></td>
                            <td><%#Eval("UnitPrice") %></td>
                            <td><%#Eval("Quantity") %></td>
                            <td><%#Eval("Subtotal") %></td>
                            <td><%#Eval("Remark") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </table>
        </ItemTemplate>
    </asp:Repeater>

      库房：
    <table class="table table-bordered table-striped">
        <tr>
            <th style="width: 100px;">类别</th>
            <th style="width: 100px;">项目</th>
            <th style="width: 100px;">产品</th>
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
                <tr >
                    <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("ParentCategoryName") %></td>
                    <td  <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("CategoryName") %></td>
                    <td  <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("ProductID") %></td>
                    <td  <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("ServiceContent") %></td>
                    <td  <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Requirement") %></td>
                    <td  <%#GetBorderStyle(Eval("NewAdd")) %>><a href="#">查看资料</a></td>
                    <td  <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("UnitPrice") %></td>
                    <td  <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Quantity") %></td>
                    <td  <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Subtotal") %></td>
                    <td  <%#GetBorderStyle(Eval("NewAdd")) %> id="Partd<%#Container.ItemIndex %>">
                        <input style="width: 45px;" runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowPopu(this);" type="text" value='<%#Eval("EmpLoyeeID") %>' />
                        <a href="#" onclick="ShowPopu(this);" class="SetState">改派</a>
                        <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
                    </td>
                    <td <%#GetBorderStyle(Eval("NewAdd")) %>><%#Eval("Remark") %></td>
                </tr>
            </ItemTemplate>

        </asp:Repeater>
        <tr>
            <td colspan="12">
                <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server"   OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
            </td>
        </tr>
    </table>
</div>
