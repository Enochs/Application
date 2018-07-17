<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_TheCaseManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases.FD_TheCaseManager" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_TheCaseUpdate.aspx?CaseID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 800, 600, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            $("html,body").css({ "background-color": "transparent" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <table class="table table-borderd" style="width:85%;">
                <tr>
                    <td>案例名称:</td>
                    <td><asp:TextBox runat="server" ID="txtName" /></td>
                    <td>酒店:</td>
                    <td><asp:TextBox runat="server" ID="txtWineShop" /></td>
                    <td>风格:</td>
                    <td><asp:TextBox runat="server" ID="txtStyle" /></td>
                    <td>
                        <asp:Button runat="server" ID="btnLook" CssClass="btn btn-primary" Text="查询" OnClick="btnLook_Click" />
                        <cc2:btnReload runat="server" ID="btnReload" CssClass="btn btn-primary" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select">
                <thead><tr><th>序号</th><th>案例名称</th><th>新人名称</th><th>酒店</th><th>风格</th><th>操作</th></tr></thead>
                <tbody>
                    <asp:Repeater ID="rptTheCase" OnItemCommand="rptTheCase_ItemCommand" runat="server">
                        <ItemTemplate>
                            <tr skey='<%#Eval("CaseID") %>'><td><%#Eval("CaseOrder") %></td>
                                <td><%#Eval("CaseName") %></td>
                                <td><%#Eval("CaseGroom") %></td>
                                <td><%#Eval("CaseHotel") %></td>
                                <td><%#Eval("CaseStyle") %></td>
                                <td><a href="FD_TheCaseFileManager.aspx?FileType=2&CaseID=<%#Eval("CaseID") %>" class="btn btn-primary  btn-mini">管理图片</a>&nbsp;
                                    <a href="FD_TheCaseFileManager.aspx?FileType=1&CaseID=<%#Eval("CaseID") %>" class="btn btn-primary  btn-mini">管理视频</a>&nbsp; 
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lkbtnDelete" CommandName="Delete" OnClientClick="return confirm('是否真的要删除该项？');" CommandArgument='<%#Eval("CaseID") %>' runat="server">删除</asp:LinkButton>&nbsp;
                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("CaseID") %>,this);'>修改</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr><td colspan="5"><cc1:AspNetPagerTool ID="TheCasePager" AlwaysShow="true" OnPageChanged="TheCasePager_PageChanged" runat="server"></cc1:AspNetPagerTool></td></tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
