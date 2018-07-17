<%@ Page Title="Bug管理" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="BugSystemManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.BugSystem.BugSystemManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //上传图片
        function ShowFileUploadPopu(BugID) {
            var Url = "/AdminPanlWorkArea/BugSystem/BugSystemUpLoad.aspx?BugID=" + BugID;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        $(document).ready(function () {

            showPopuWindows($("#createKnow").attr("href"), 310, 180, "a#createKnow");

            showPopuWindows($("#BrowseKnow").attr("href"), 1000, 700, "a#BrowseKnow");

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>

    <div class="div_Main">
        <div class="ui-menu-divider">
            <table class="table table-bordered" style="width: 85%;">
                <tr>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" Title="上传人" />
                    </td>

                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" Title="上传时间" />
                    </td>

                    <td>状态
                        <asp:DropDownList runat="server" ID="ddlState">
                            <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="未解决" Value="1" />
                            <asp:ListItem Text="处理中" Value="2" />
                            <asp:ListItem Text="已解决" Value="3" />
                            <asp:ListItem Text="无效信息" Value="4" />
                        </asp:DropDownList>
                    </td>

                    <td>
                        <asp:Button runat="server" ID="btnQuery" Text="查找" />

                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select" style="width: 85%;">
                <thead>
                    <tr>
                        <th>标题</th>
                        <th>内容</th>
                        <th>上传人</th>
                        <th>上传时间</th>
                        <th>状态</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="RepBugSystem">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("BugTitle") %></td>
                                <td><%#Eval("BugContent").ToString().Length >=10 ? Eval("BugContent").ToString().Substring(0,9)+"……" : Eval("BugContent").ToString() %></td>
                                <td><%#GetEmployeeName(Eval("CreateEmployee")) %></td>
                                <td><%#Eval("CreateDate") %></td>
                                <td><%#GetState(Eval("State")) %></td>
                                <td>
                                    <a <%#UploadShow(Eval("BugID")) %> href="#" class="btn btn-primary btn-mini" onclick='ShowFileUploadPopu(<%#Eval("BugID") %>)'>上传</a>
                                    <a href='CreateBugSystem.aspx?BugID=<%#Eval("BugID") %>&Type=查看' class="btn btn-primary btn-mini">查看</a>
                                    <a <%#Handel(Eval("BugID")) %> href='CreateBugSystem.aspx?BugID=<%#Eval("BugID") %>&Type=处理' class="btn btn-primary btn-mini">处理</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="6">
                            <cc1:AspNetPagerTool ID="ctrPager" AlwaysShow="true" PageSize="10" OnPageChanged="BinderData" runat="server" />
                        </td>
                    </tr>
                </tfoot>

            </table>
        </div>
    </div>
</asp:Content>
