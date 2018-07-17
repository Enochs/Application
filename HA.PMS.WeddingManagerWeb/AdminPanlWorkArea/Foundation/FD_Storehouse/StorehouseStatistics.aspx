<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="StorehouseStatistics.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.StorehouseStatistics" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforEmpLoyee.ascx" TagPrefix="HA" TagName="MessageBoardforEmpLoyee" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-y: scroll;">
        <div class="widget-box">
            <table>
                <tr>
                    <td><HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" /></td>
                    <td><asp:Button ID="btnQuery" CssClass="btn btn-primary" runat="server" OnClick="BinderData" Text="查询" /></td>
                </tr>
            </table>
            <div style="overflow-x: auto; height: 900px; width: 100%">
                <table cellpadding="8" cellspacing="0" border="0">
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Repeater ID="rptSaleTop10" runat="server">
                                <HeaderTemplate>
                                    <table border="0" class="table table-bordered">
                                        <tr><th colspan="3"><strong>销售排名（TOP10）</strong></th></tr>
                                        <tr>
                                            <th>产品名称</th>
                                            <th>销售数量</th>
                                            <th>销售次数</th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr <%#Container.ItemIndex<=2?"style='background-color:#c5dcef'":"" %>>
                                        <td style="width: 180px; text-overflow: ellipsis"><%#GetProductName(Eval("SourceProductId")) %></td>
                                        <td style="text-align: right; padding-right: 8px"><%#Eval("SaleCount") %></td>
                                        <td style="text-align: right; padding-right: 8px;"><%#Eval("SaleTimes") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></table></FooterTemplate>
                            </asp:Repeater>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Repeater ID="rptSaleTop20" runat="server">
                                <HeaderTemplate>
                                    <table border="0" class="table table-bordered">
                                        <tr><th colspan="3"><strong>销售排名（TOP20）</strong></th></tr>
                                        <tr>
                                            <th>产品名称</th>
                                            <th>销售数量</th>
                                            <th>销售次数</th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 180px; text-overflow: ellipsis"><%#GetProductName(Eval("SourceProductId")) %></td>
                                        <td style="text-align: right; padding-right: 8px"><%#Eval("SaleCount") %></td>
                                        <td style="text-align: right; padding-right: 8px"><%#Eval("SaleTimes") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></table></FooterTemplate>
                            </asp:Repeater>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Repeater ID="rptSaleTop30" runat="server">
                                <HeaderTemplate>
                                    <table border="0" class="table table-bordered">
                                        <tr><th colspan="3"><strong>销售排名（TOP30）</strong></th></tr>
                                        <tr>
                                            <th>产品名称</th>
                                            <th>销售数量</th>
                                            <th>销售次数</th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 180px; text-overflow: ellipsis"><%#GetProductName(Eval("SourceProductId")) %></td>
                                        <td style="text-align: right; padding-right: 8px"><%#Eval("SaleCount") %></td>
                                        <td style="text-align: right; padding-right: 8px"><%#Eval("SaleTimes") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></table></FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">
                            <asp:Repeater ID="rptColumn1" runat="server">
                                <HeaderTemplate>
                                    <table border="0" class="table table-bordered">
                                        <tr>
                                            <th>产品名称</th>
                                            <th>销售数量</th>
                                            <th>销售次数</th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 180px; text-overflow: ellipsis"><%#GetProductName(Eval("SourceProductId")) %></td>
                                        <td style="text-align: right; padding-right: 8px"><%#Eval("SaleCount") %></td>
                                        <td style="text-align: right; padding-right: 8px"><%#Eval("SaleTimes") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></table></FooterTemplate>
                            </asp:Repeater>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Repeater ID="rptColumn2" runat="server">
                                <HeaderTemplate>
                                    <table border="0" class="table table-bordered">
                                        <tr>
                                            <th>产品名称</th>
                                            <th>销售数量</th>
                                            <th>销售次数</th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 180px; text-overflow: ellipsis"><%#GetProductName(Eval("SourceProductId")) %></td>
                                        <td style="text-align: right; padding-right: 8px"><%#Eval("SaleCount") %></td>
                                        <td style="text-align: right; padding-right: 8px"><%#Eval("SaleTimes") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></table></FooterTemplate>
                            </asp:Repeater>
                        </td>
                        <td style="vertical-align: top">
                            <asp:Repeater ID="rptColumn3" runat="server">
                                <HeaderTemplate>
                                    <table border="0" class="table table-bordered">
                                        <tr>
                                            <th>产品名称</th>
                                            <th>销售数量</th>
                                            <th>销售次数</th>
                                        </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 180px; text-overflow: ellipsis"><%#GetProductName(Eval("SourceProductId")) %></td>
                                        <td style="text-align: right; padding-right: 8px"><%#Eval("SaleCount") %></td>
                                        <td style="text-align: right; padding-right: 8px"><%#Eval("SaleTimes") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate></table></FooterTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table class="table table-bordered">
                                <tr>
                                    <th>销售概况</th>
                                    <th>数量</th>
                                    <th>百分比</th>
                                </tr>
                                <tr>
                                    <td>销售10次及以上的产品</td>
                                    <td><asp:Literal ID="ltlTop10" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="ltlTop10Rate" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td>销售5~10次的产品</td>
                                    <td><asp:Literal ID="ltlTop5" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="ltlTop5Rate" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td>销售1~5次的产品</td>
                                    <td><asp:Literal ID="ltlTop1" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="ltlTop1Rate" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td>没有销售的产品</td>
                                    <td><asp:Literal ID="ltlTop0" runat="server"></asp:Literal></td>
                                    <td><asp:Literal ID="ltlTop0Rate" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="table table-bordered">
                                <tr>
                                    <th>新入库产品情况(一月之内)</th>
                                    <th></th>
                                </tr>
                                <tr>
                                    <td>新入库产品总数</td>
                                    <td><asp:Literal ID="ltlNewProductCount" runat="server"></asp:Literal></td>
                                </tr>

                                <tr>
                                    <td>总使用次数</td>
                                    <td><asp:Literal ID="ltlNewProductOperCount" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td>采购总价</td>
                                    <td><asp:Literal ID="ltlNewPriceSum" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td>建议市场销售总价</td>
                                    <td><asp:Literal ID="ltlSalePriceSum" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align: top">
                            <table class="table table-bordered">
                                <tr>
                                    <th>报废拆用情况</th>
                                    <th></th>
                                </tr>
                                <tr>
                                    <td>报废数量</td>
                                    <td><asp:Literal ID="ltlWasteCount" runat="server"></asp:Literal></td>
                                </tr>
                                <tr>
                                    <td>拆用数量</td>
                                    <td><asp:Literal ID="ltlOperCount" runat="server"></asp:Literal></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
            </div>
        </div>
    </div>
</asp:Content>
