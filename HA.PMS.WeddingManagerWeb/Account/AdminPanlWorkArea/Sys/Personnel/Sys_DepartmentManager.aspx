<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Sys_DepartmentManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_DepartmentManager" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>

    <script type="text/javascript">

        function ShowWindows(KeyID, parentId, Control) {
            var Url = "Sys_DepartmentUpdate.aspx?deparId=" + KeyID + "&parent=" + parentId;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 600, 800, "a#" + $(Control).attr("id"));
        }


        function ShowEmployeePopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        $(document).ready(function () {
            showPopuWindows($("#ff").attr("href"), 700, 1000, "a#ff");
            $("html,body").css({ "background-color": "transparent" });
        });


        function ShowPopu(Parent) {
            var Url = "Sys_DepartmentCreate.aspx?Paretnt=" + Parent;
            showPopuWindows(Url, 700, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box" style="height:900px;overflow:auto;">
        <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
        <a id="ff" class="btn btn-primary  btn-mini" href="Sys_DepartmentCreate.aspx">添加顶级部门</a>
        <a id="A1" class="btn btn-primary  btn-mini" href="Sys_EmployeeManager.aspx?DepartmentID=-1&NeedPopu=1">已停用的员工</a><br />
        <div class="widget-content">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>部门名称</th>
                        <th>操作</th>
                        <th>设置部门主管</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDepartment" runat="server" OnItemCommand="rptDepartment_ItemCommand">
                        <ItemTemplate>
                            <tr skey='<%#Eval("DepartmentID") %>'>
                                <td><%#GetItemNbsp(Eval("ItemLevel")) %><%#Eval("DepartmentName") %></td>
                                <td>
                                    <a href="#" class="btn btn-mini btn-primary" onclick='ShowPopu(<%#Eval("DepartmentID") %>);'>添加子部门</a>
                                    <a href="Sys_EmployeeManager.aspx?DepartmentID=<%#Eval("DepartmentID") %>&NeedPopu=1" class="btn btn-mini btn-primary">部门员工管理</a>
                                    <a href="#" class="btn btn-mini btn-primary" onclick='ShowWindows(<%#Eval("DepartmentID") %>,<%#Eval("Parent") %>,this);'>修改</a>
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lkbtnDelete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("DepartmentID") %>'>删除</asp:LinkButton>
                                </td>
                                <td id="<%#Guid.NewGuid().ToString() %>">
                                    <asp:HiddenField ID="hiddDepartmentManager" runat="server" Value='<%#Eval("DepartmentManager") %>' />
                                    <asp:HiddenField ID="hiddDepartmentID" runat="server" Value='<%#Eval("DepartmentID") %>' />
                                    <input style="margin: 0" runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowEmployeePopu(this);" type="text" value='<%#GetEmployeeName(Eval("DepartmentManager")) %>' />
                                    <a href="#" class="btn btn-mini btn-primary" onclick="ShowEmployeePopu(this);" class="SetState">选择部门负责人</a>
                                    <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='' runat="server" />
                                    <asp:Button ID="Button1" CommandArgument='<%#Eval("DepartmentID") %>' CommandName="SaveChange" runat="server" Text="保存设置" CssClass="btn btn-success btn-mini" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="DepartmentPager" AlwaysShow="true" OnPageChanged="DepartmentPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>

</asp:Content>
