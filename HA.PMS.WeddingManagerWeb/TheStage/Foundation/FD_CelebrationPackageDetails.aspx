<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationPackageDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.Foundation.FD_CelebrationPackageDetails" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable td").css({ "border": "0", "vertical-align": "top" });
            $("html").css({ "overflow-x": "hidden" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>套系展示</h5>

        </div>
        <div class="widget-content">

            <table class="queryTable">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>品牌:<asp:DropDownList ID="ddlBrand" Width="130" runat="server">
                                    <asp:ListItem Text="请选择" Value="0" />
                                    <asp:ListItem Text="精品奢华" Value="1" />
                                    <asp:ListItem Text="爱情甜美" Value="2" />
                                    <asp:ListItem Text="野性狂野" Value="3" />
                                </asp:DropDownList></td>
                                <td>风格:
                                    <asp:DropDownList ID="ddlPackageStyle" runat="server"></asp:DropDownList>
                                </td>
                                <td>价格区间:
                                    <asp:DropDownList ID="ddlPriceSpan" runat="server"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnQuery"  OnClick="btnQuery_Click" CssClass="btn" runat="server" Text="查找" />

                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>

            <table style="width: 1300px;" class="table table-bordered table-striped">
                <thead>
                    <tr id="trContent">
                        <th>套系名称</th>
                        <th>风格</th>
                        <th>价格</th>
                        <th>简介</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptPackage" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><a href='CelebrationPackageSinger.aspx?PackageID=<%#Eval("PackageID") %>' target="_blank"><%#Eval("PackageTitle") %></a> </td>
                                <td><%#GetPackage(Eval("PackageSkip")) %></td>
                                <td><%#Eval("PackagePrice") %></td>
                                <td><%#Eval("PackageDirections") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>


                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="PackagePager" AlwaysShow="true" OnPageChanged="PackagePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
