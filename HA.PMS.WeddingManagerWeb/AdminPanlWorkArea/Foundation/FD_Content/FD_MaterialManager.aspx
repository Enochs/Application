<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FD_MaterialManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.FD_MaterialManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <style type="text/css">
        .table tbody tr td {
            text-align: center;
        }
    </style>
    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control,Type) {
            var Url = "FD_MaterialUpdate.aspx?MaterialId=" + KeyID + "&type=" + Type;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>
                    <td>材质名称:<asp:TextBox runat="server" ID="txtMaterialName" /></td>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <cc2:btnManager ID="btnSerch" runat="server" OnClick="btnSerch_Click" />
                        <cc2:btnReload ID="btnReload2" runat="server" />
                        <a href="#" onclick='ShowUpdateWindows(0,this,"Add");' class="SetState btn btn-danger">新增</a>
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>名称</th>
                        <th>单价</th>
                        <th>备注</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="rptMaterial">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("MaterialName") %></td>
                                <td><%#Eval("MaterialUnitPrice") %></td>
                                <td><%#Eval("MaterialRemark") %></td>
                                <td>
                                    <a href="#" onclick='ShowUpdateWindows(<%#Eval("MaterialId") %>,this,"Update");' class="SetState btn btn-primary">修改</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="12">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" PageSize="10" OnPageChanged="CtrPageIndex_PageChanged">
                            </cc1:AspNetPagerTool>
                        </td>
                    </tr>

                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>
