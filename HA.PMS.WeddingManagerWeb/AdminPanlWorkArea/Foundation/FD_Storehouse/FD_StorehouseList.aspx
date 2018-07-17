<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ShowEmployeePopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function ShouwCreateUpdate() {
            var Url = "/AdminPanlWorkArea/Foundation/FD_Storehouse/FD_StorehouseCreateUpdate.aspx";
            showPopuWindows(Url, 300, 64, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <a href="#" class="btn btn-primary" onclick="ShouwCreateUpdate();">添加库房</a>
    <table class="table table-bordered table-striped">
        <thead>
            <tr>
                <th>库房</th>
                <th>设置库管</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repStoreHouse" runat="server" OnItemCommand="rptDepartment_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("HouseName") %></td>
                        
                        <td id="<%#Guid.NewGuid().ToString() %>">
                            <asp:HiddenField ID="hiddDepartmentManager" runat="server" Value='<%#Eval("HouseName") %>' />
                            <asp:HiddenField ID="hiddDepartmentID" runat="server" Value='<%#Eval("StorehouseID") %>' />
                            <input runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowEmployeePopu(this);" type="text" value='<%#GetEmployeeName(Eval("EmpLoyeeID")) %>' />
                            <a href="#" onclick="ShowEmployeePopu(this);" class="SetState btn btn-primary">选择库管</a>
                            <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='' runat="server" />
                            <asp:Button ID="Button1" CommandArgument='<%#Eval("StorehouseID") %>' CommandName="SaveChange" runat="server" Text="保存" CssClass="btn btn-success" />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Content>
