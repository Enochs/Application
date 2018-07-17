<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_SaleSourcesEmployeeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SaleSourcesEmployeeManager" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hiddeEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>渠道所有者</th>
                        <th>登录名</th>
                        <th>联系方式</th>
                        <th>职位</th>
                        <th>部门</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" OnItemCommand="repCustomer_ItemCommand" runat="server">
                        <ItemTemplate>
                            <tr skey='SaleSources<%#Eval("SourceID") %>'>
                                <td><%#GetEmployee(Eval("ProlongationEmployee")).EmployeeName %></td>
                                <td><%#GetEmployee(Eval("ProlongationEmployee")).LoginName %></td>
                                <td><%#GetEmployee(Eval("ProlongationEmployee")).CellPhone %></td>
                                <td><%#GetJobName(GetEmployee(Eval("ProlongationEmployee")).JobID) %></td>
                                <td><%#GetDepartmentName(GetEmployee(Eval("ProlongationEmployee")).DepartmentID) %></td>
                                <td id="<%=Guid.NewGuid() %>" style="width:165px">
                                    <input runat="server" id="txtEmpLoyee" style="padding: 0; margin: 0;width:65px" readonly="readonly" class="txtEmpLoyeeName" type="text" value='<%#GetEmployeeName(Eval("ProlongationEmployee")) %>' />
                                    <a class="btn btn-primary btn-mini" onclick="ShowPopu(this)">选择</a>
                                    <asp:LinkButton CssClass="btn btn-success btn-mini" CommandArgument='<%#Eval("ProlongationEmployee") %>' CommandName="Save" Text="保存" runat="server" />
                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hiddeEmpLoyeeID" Value='<%#Eval("ProlongationEmployee") %>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="6"><cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BindData"></cc1:AspNetPagerTool></td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
