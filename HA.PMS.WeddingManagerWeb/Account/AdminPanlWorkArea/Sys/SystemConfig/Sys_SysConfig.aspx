<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sys_SysConfig.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.SystemConfig.Sys_SysConfig" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script src="/Scripts/trselection.js"></script>
 
    <script>
        function CheckChange(statu) {
            var msg = statu == 0 ? "停止" : "启动";
            return confirm("确认要" + msg + "该功能吗？");
        }
    </script>

    <table class="table table-bordered table-striped table-select">
        <thead>
            <tr style="border: none;">
                <td style="border: none;" colspan="3">
                    <div style="margin-left: 79%;">
                    </div>
                </td>
                <td style="border: none;"></td>
            </tr>
            <tr>
                <th style="white-space: nowrap;">配置函数</th>
                <th style="white-space: nowrap;">说明</th>

                <th style="white-space: nowrap;">操作</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repSysConfig" runat="server" OnItemCommand="repSysConfig_ItemCommand">
                <ItemTemplate>
                    <tr skey='<%#Eval("ConFigID") %>'>
                        <td>
                            <%#Eval("ConfigName") %>  
                        </td>
                        <td><%#Eval("ConfigMarke") %></td>
                        <td>
                            <label class="<%#Eval("IsClose").ToString()=="True"?"RemoveClass":"" %>">
                                <asp:LinkButton ID="lnkbtnDelete" CssClass="btn btn-warning btn-mini" CommandName="End" CommandArgument='<%#Eval("ConFigID") %>' runat="server" OnClientClick="return CheckChange(0);">停止</asp:LinkButton>
                            </label>
                            <label class="<%#Eval("IsClose").ToString()=="False"?"RemoveClass":"" %>">
                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-success btn-mini" CommandName="Star" CommandArgument='<%#Eval("ConFigID") %>' runat="server" OnClientClick="return CheckChange(1);">启动</asp:LinkButton>
                            </label>
                        </td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
 
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
        </tfoot>
    </table>

</asp:Content>
