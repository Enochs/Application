<%@ Page Title="策划师管理" StylesheetTheme="Default" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FL_PlannerManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases.FL_PlannerManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FL_PlannerUpdate.aspx?PlannerID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 620, 1000, "a#" + $(Control).attr("id"));
        }

        function ShowCreateWindows(KeyID, Control) {
            var Url = "FL_PlannerCreate.aspx";
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 620, 800, "a#" + $(Control).attr("id"));
        }

        function ShowSelectWindows(KeyID, Control) {
            var Url = "SelectCase.aspx?PlannerID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div">
        <a href="#" onclick="ShowCreateWindows(1,this)" target="_blank" class="btn btn-primary btn-mini">新增</a>
        <div class="main">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>姓名</td>
                    <td>性别</td>
                    <td>职位</td>
                    <td>创建时间</td>
                    <td>创建人</td>
                    <td>状态</td>
                    <td>操作</td>
                </tr><%--<%#GetState(Eval("IsDelete")) %>--%>
                <asp:Repeater runat="server" ID="rptPlanner" OnItemCommand="rptPlanner_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("PlannerName") %></td>
                            <td><%#GetSex(Eval("PlannerSex")) %></td>
                            <td><%#GetJob(Eval("PlannerJob")) %></td>
                            <td><%#Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
                            <td><%#GetEmployeeName(Eval("CreateEmployee")) %></td>
                            <td>
                                <asp:ImageButton runat="server" ID="imgBtnOk" CommandName="imgBtnOk" CommandArgument='<%#Eval("PlannerID") %>'  ImageUrl="../../../Images/toolbar_ok.gif" />
                                <asp:ImageButton runat="server" ID="imgBtnNo" CommandName="imgBtnNo" CommandArgument='<%#Eval("PlannerID") %>' ImageUrl="../../../Images/toolbar_no.gif" />
                            </td>
                            <td>
                                <a href="#" onclick='ShowUpdateWindows(<%#Eval("PlannerID") %>,this)' target="_blank" class="btn btn-primary btn-mini">修改</a>
                                <a href="#" onclick='ShowSelectWindows(<%#Eval("PlannerID") %>,this)' class="btn btn-primary btn-mini">作品上传</a>
                                <asp:LinkButton runat="server" ID="lbtnDelete" CommandArgument='<%#Eval("PlannerID") %>' CommandName="lbtnDelete" Text="删除" OnClientClick="return confirm('您确定要删除吗?');" CssClass="btn btn-primary btn-mini" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tfoot>
                    <tr>
                        <td colspan="6">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" OnPageChanged="CtrPageIndex_PageChanged" runat="server" PageSize="20"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>
