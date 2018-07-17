<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Sys_EmpLoyeeHigherupsManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_EmpLoyeeHigherupsManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">

        function ShowWindowsUpdate(KeyID, Control) {
            var Url = "Sys_EmpLoyeeHigherupsUpdate.aspx?employeehigherupsid=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 250, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {

            showPopuWindows($("#createEmpLoyeeHigherups").attr("href"), 500,250, "a#createEmpLoyeeHigherups");


        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a id="createEmpLoyeeHigherups"  class="btn btn-primary  btn-mini" href="Sys_EmpLoyeeHigherupsCreate.aspx">添加人员上级信息</a>
    <div class="widget-box">

        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>员工ID</th>
                        <th>类型</th>
                        <th>Code</th>
                        <th>创建时间</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptEmpLoyeeHigherups" runat="server" OnItemCommand="rptEmpLoyeeHigherups_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("EmployeeID") %></td>
                                <td><%#Eval("Type") %></td>
                                <td><%#Eval("FunctionCode") %></td>

                                <td><%#Eval("createTime") %></td>
                                <td>
                                    <asp:LinkButton ID="lkbtnDelete" CssClass="btn btn-danger btn-mini" CommandName="Delete" CommandArgument='<%#Eval("UpKey") %>' runat="server">
                        删除</asp:LinkButton>&nbsp; 
                        <a class="btn btn-primary  btn-mini" href="#" onclick='ShowWindowsUpdate(<%#Eval("UpKey") %>,this);'>修改</a>
                                </td>
                            </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="EmpLoyeeHigherupsPager" AlwaysShow="true" OnPageChanged="EmpLoyeeHigherupsPager_PageChanged" runat="server">

            </cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
