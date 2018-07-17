<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_HotelManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel.FD_HotelManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_HotelUpdate.aspx?HotelId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 800, 300, "a#" + $(Control).attr("id"));
        }
        function ShowImgWindows(KeyID, Control) {
            var Url = "FD_HotelImageManager.aspx?HotelId=" + KeyID;
            $(Control).attr("id", "imgShow" + KeyID);
            showPopuWindows(Url, 800, 300, "a#" + $(Control).attr("id"));
        }
        function ShowOperImg(KeyID, Control) {
            var Url = "FD_HotelImageLoadFile.aspx?HotelId=" + KeyID + "&toOperPage=" + <%=ViewState["load"]%>;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 800, 300, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            showPopuWindows($("#createHotel").attr("href"), 800, 300, "a#createHotel");
            var availabletags=<%=ViewState["areatags"]%>;
            $("#<%=txtArea.ClientID%>").autocomplete({source:availabletags});
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="FD_HotelCreate.aspx" class="btn btn-primary  btn-mini" id="createHotel">创建酒店</a>
    <a href="FD_HotelLabelManager.aspx" class="btn btn-primary  btn-mini">管理场地标签</a>
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>
                    <td>酒店名称</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtHotelName" runat="server" /></td>
                    <td>酒店区域</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtArea" runat="server" ClientIDMode="Static" /></td>
                    <td>酒店星级</td>
                    <td>
                        <asp:DropDownList Style="margin: 0" ID="ddlStarLevel" runat="server">
                            <asp:ListItem Text="" Value="0" />
                            <asp:ListItem Text="五星级" Value="5" />
                            <asp:ListItem Text="四星级" Value="4" />
                            <asp:ListItem Text="特色餐厅" Value="3" />
                            <asp:ListItem Text="其他" Value="2" />
                        </asp:DropDownList></td>
                    <td>
                        <asp:Button ID="btnQuery" Text="查询" CssClass="btn btn-primary" runat="server" OnClick="btnQuery_Click" /></td>
                </tr>
            </table>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>酒店名称</th>
                        <th>酒店区域</th>
                        <th>酒店星级</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptHotel" runat="server" OnItemCommand="rptHotel_ItemCommand" OnItemDataBound="rptHotel_ItemDataBound">
                        <ItemTemplate>
                            <tr skey='FD_HotelHotelID<%#Eval("HotelID") %>'>
                                <td><%#Eval("HotelName") %></td>
                                <td><%#Eval("Area") %></td>
                                <td><%#Eval("HotelType") %></td>
                                <td><a href="#" class="btn btn-primary  btn-mini" onclick='ShowOperImg(<%#Eval("HotelID") %>,this);'>添加图片</a>
                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowImgWindows(<%#Eval("HotelID") %>,this);'>管理图片</a>
                                    <a href="FD_BanquetHallManager.aspx?HotelId=<%#Eval("HotelID") %>" class="btn btn-primary  btn-mini">管理宴会厅</a>
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lkbtnDelete" OnClientClick="return confirm('确认删除该酒店？')" CommandName="Delete" CommandArgument='<%#Eval("HotelID") %>' runat="server">删除</asp:LinkButton>&nbsp;
                                    <a target="_blank" href='FD_HotelUpdate.aspx?HotelId=<%#Eval("HotelID") %>' class="btn btn-primary  btn-mini"><%--onclick='ShowUpdateWindows(<%#Eval("HotelID") %>,this)';--%>修改</a>
                                    <asp:HiddenField runat="server" ID="HideHotelID" Value='<%#Eval("HotelID") %>' />
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="4">
                            <cc1:AspNetPagerTool ID="HotTelPager" AlwaysShow="true" OnPageChanged="HotTelPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoard runat="server" ID="MessageBoard" ClassType="FD_HotelManager" />
        </div>
    </div>
</asp:Content>
