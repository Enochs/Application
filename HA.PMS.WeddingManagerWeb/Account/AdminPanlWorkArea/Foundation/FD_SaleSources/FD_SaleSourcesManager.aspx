<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_SaleSourcesManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SaleSourcesManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowWindowsUpdate(KeyID, Control) {
            var Url = "FD_SaleSourcesUpdate.aspx?SourceID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }
        function ShowWindowsDetails(KeyID, Control) {
            var Url = "FD_SaleSourcesDetails.aspx?SourceID=" + KeyID;
            $(Control).attr("id", "detailsShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <table>

                <tr>
                    <td colspan="4"><a href="FD_SaleSourcesCreate.aspx" class="btn btn-primary btn-mini" id="createSaleSource">创建新渠道</a>
                        <a href="FD_ChannelTypeManager.aspx?NeedPopu=1" class="btn btn-mini btn-success">渠道类型管理</a>
                    </td>
                </tr>
                <tr>
                    <th>渠道名称</th>
                    <th>
                        <asp:TextBox ID="txtSourceName" runat="server"></asp:TextBox>
                    </th>
                    <th>渠道类型</th>
                    <th>
                        <cc2:ddlChannelType ID="ddlChannelType1" runat="server">
                        </cc2:ddlChannelType>
                        <asp:Button ID="benSerch" runat="server" Text="查询" OnClick="benSerch_Click" />
                    </th>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select">
                <tr>
                    <th>渠道名称</th>
                    <th>渠道类型</th>
                    <th>地址</th>
                    <th>操作</th>
                </tr>

                <tbody>
                    <asp:Repeater ID="rptSaleSources" runat="server" OnItemCommand="rptSaleSources_ItemCommand">
                        <ItemTemplate>
                            <tr skey='<%#Eval("SourceID") %>'>
                                <td><%#Eval("Sourcename") %></td>
                                <td><%#GetsChannelTypeName(Eval("ChannelTypeID")) %></td>
                                <td><%#Eval("Address") %></td>
                                <td><a href="FD_SaleSourcesUpdate.aspx?SourceID=<%#Eval("SourceID") %>" class="btn btn-primary  btn-mini">修改</a>
                                    <a href="FD_SaleSourcesDetails.aspx?SourceID=<%#Eval("SourceID") %>" class="btn btn-primary  btn-mini">详细信息</a>

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="4">
                            <cc1:AspNetPagerTool ID="SaleSourcesPager" runat="server" OnPageChanged="SaleSourcesPager_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
