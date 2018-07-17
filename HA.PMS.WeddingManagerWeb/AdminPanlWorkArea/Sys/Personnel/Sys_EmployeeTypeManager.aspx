<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Sys_EmployeeTypeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_EmployeeTypeManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">

        function ShowWindowsUpdate(KeyID, Control) {
            var Url = "Sys_EmployeeTypeUpdate.aspx?employeetypeid=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 250, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {

            showPopuWindows($("#createEmployeeType").attr("href"), 500, 250, "a#createEmployeeType");

        });


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <a id="createEmployeeType" class="btn btn-primary  " href="Sys_EmployeeTypeCreate.aspx">添加人员类型</a><br />
        <div class="widget-content">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>类型名称</th>
                        <th>创建时间</th>
                        <th>创建人</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptEmployeeType" runat="server" OnItemCommand="rptEmployeeType_ItemCommand">


                        <ItemTemplate>
                            <tr skey='PersonnelEmployeeTypeID<%#Eval("EmployeeTypeID") %>'>
                                <td><%#Eval("Type") %></td>
                                <td><%#Eval("CreateTime") %></td>
                                <td><%#GetEmployeeName(Eval("EmployeeId")) %></td>
                                <td>
                                    <asp:LinkButton ID="lkbtnDelete" style="display:none" CssClass="btn btn-danger " CommandName="Delete" CommandArgument='<%#Eval("EmployeeTypeID") %>' runat="server">
                        删除</asp:LinkButton>&nbsp; 
                        <a href="#" class="btn btn-primary  " onclick='ShowWindowsUpdate(<%#Eval("EmployeeTypeID") %>,this);'>修改</a>
                                </td>
                            </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="EmployeeTypePager" UrlPaging="true" AlwaysShow="true" OnPageChanged="EmployeeTypePager_PageChanged" runat="server">

            </cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
