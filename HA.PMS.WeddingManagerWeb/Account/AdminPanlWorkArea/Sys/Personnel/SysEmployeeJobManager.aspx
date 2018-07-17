<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="SysEmployeeJobManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.SysEmployeeJobManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">
        function ShowWindowsUpdate(KeyID, Control) {
            var Url = "SysEmployeeJobUpdate.aspx?jobId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 250, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {

            showPopuWindows($("#ff").attr("href"), 500,250, "a#ff");
        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <a id="ff" class="btn btn-primary  btn-mini" href="SysEmployeeJobCreate.aspx">添加人员职务</a><br />
        <div class="widget-content">
            <table  class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>职务名称</th>
                        <th>创建时间</th>
                        <th>创建人</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptJob" runat="server" OnItemCommand="rptJob_ItemCommand">
                        <ItemTemplate>
                            <tr skey='PersonnelJobID<%#Eval("JobID") %>'>
                                <td><%#Eval("Jobname") %></td>
                                <td><%#Eval("createTime") %></td>
                                <td><%#GetEmployeeName(Eval("EmployeeId")) %></td>
                                <td><a  href="#" class="btn btn-primary  btn-mini" onclick='ShowWindowsUpdate(<%#Eval("JobID") %>,this);'>修改</a>&nbsp;
                        <asp:LinkButton CssClass="btn btn-danger btn-mini" style="display:none" ID="lkbtnDelete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("JobID") %>'>
                        删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="EmployeeJobPager" UrlPaging="true" AlwaysShow="true" OnPageChanged="EmployeeJobPager_PageChanged" runat="server">

            </cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
