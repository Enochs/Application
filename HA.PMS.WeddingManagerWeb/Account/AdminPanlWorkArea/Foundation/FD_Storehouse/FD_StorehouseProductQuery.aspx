<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseProductQuery.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseProductQuery" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Scripts/trselection.js"></script>

    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 20px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }

        .widthStyle {
            width: 650px;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            $(".grouped_elements").each(function (indexs, values) {
                if ($.trim($(this).html()) == "") {
                    $(this).remove();
                }
            });
            //加载对应图片浏览效果 begin
            $(".grouped_elements").each(function (indexs, values) {
                var imgChildren = $(this).children("img");
                $(this).attr("href", imgChildren.attr("src"));

            });

            $("a.grouped_elements").fancybox();

            $(".remainder").each(function (indexs, values) {
                var currentText = parseInt($.trim($(this).text()));
                if (currentText <= 3) {
                    $(this).parent("tr").css({ "color": "red" });
                }

            });

            //  $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {

                $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");


                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });
            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");

            //
            $("html").css({ "overflow-x": "hidden", "background-color": "transparent" });

            if (window.screen.width >= 1280 && window.screen.width <= 1366) {
                //width:1120px;


                $("#trContent th").each(function (indexs, values) {
                    $(this).attr("style", "");
                });
                $("#tbMain").attr("width", "90%");
            }

        });
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_StorehouseProductUpdate.aspx?SourceProductId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }


        function StorehouseScrapLogCreate(KeyID, Control) {
            var Url = "FD_StorehouseScrapLogCreate.aspx?SourceProductId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 420, 550, "a#" + $(Control).attr("id"));
        }

        function StorehouseProductSourceCountCreate(KeyID, Control) {
            var Url = "FD_StorehouseProductSourceCount.aspx?SourceProductId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 320, 160, "a#" + $(Control).attr("id"));
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnbinderdata" Style="display: none" Text="btnbinderdata" OnClick="BinderData" runat="server" />
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" style="height: 50px; border: 0px;">
                <table class="queryTable">
                    <tr>
                        <td>选择类别：
                           <cc2:CategoryDropDownList ID="ddlCategory" ParentID="0" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" runat="server"></cc2:CategoryDropDownList>
                            <cc2:CategoryDropDownList ID="ddlProject" runat="server"></cc2:CategoryDropDownList>
                        </td>
                        <td>产品名称：<asp:TextBox ID="txtProductName" runat="server" MaxLength="20"></asp:TextBox></td>
                        <%--<td>
                            <HA:DateRanger runat="server" ID="PartyDateRanger" Title="婚期：" />
                        </td>--%>
                        <td>
                            <asp:Button ID="btnQuery" CssClass="btn btn-primary" OnClick="btnQuery_Click" runat="server" Text="查找" /></td>
                    </tr>
                </table>
            </div>

            <table id="tbMain" class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <td colspan="12" style="text-align: right;">
                            <a href="FD_StorehouseProductCreate.aspx" class="btn btn-primary  btn-mini">新增产品</a>
                            &nbsp; <a href="StorehouseStatistics.aspx" class="btn btn-primary  btn-mini">查看库房产品统计</a>
                        </td>
                        <td colspan="2"></td>
                    </tr>
                    <tr id="trContent">
                        <th style="white-space: nowrap;">产品名称</th>
                        <th style="white-space: nowrap;">入库时间</th>
                        <th style="white-space: nowrap;">产品类别</th>
                        <th style="white-space: nowrap;">项目</th>
                        <th style="white-space: nowrap;">产品\服务描述</th>
                        <th style="white-space: nowrap;">资料</th>
                        <th style="white-space: nowrap;">采购单价</th>
                        <th style="white-space: nowrap;">销售单价</th>
                        <th style="white-space: nowrap;">数量</th>
                        <th style="white-space: nowrap;">单位</th>
                        <th style="white-space: nowrap;">剩余数量</th>
                        <th style="white-space: nowrap;">最近使用时间</th>
                        <th style="white-space: nowrap;">使用总次数</th>
                        <th style="white-space: nowrap;">仓位</th>
                        <th style="white-space: nowrap;">操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptStorehouse" OnItemCommand="rptStorehouse_ItemCommand" OnItemDataBound="rptStorehouse_ItemDataBound" runat="server">
                        <ItemTemplate>
                            <tr skey='<%#Eval("SourceProductId") %>'>
                                <td><span style="cursor: default" title='<%#Eval("SourceProductName") %>'><%#ToInLine(Eval("SourceProductName"),6) %></span></td>
                                <td style="word-break: break-all;"><%#GetDateStr(Eval("PutStoreDate")) %></td>
                                <td><%#GetCategoryName(Eval("ProductCategory")) %></td>
                                <td style="word-break: break-all;"><%#GetCategoryName(Eval("ProductProject")) %></td>
                                <td><span style="cursor: default" title='<%#Eval("Specifications") %>'><%#ToInLine(Eval("Specifications"),8) %></span></td>
                                <td style="word-break: break-all;">
                                    <a class="grouped_elements" href="#" rel="group1">
                                        <asp:Image ID="imgStore" ImageUrl='<%#Eval("Data") %>' Width="100" Height="50" runat="server" /></a>
                                    <asp:LinkButton ID="lkbtnDownLoad" CssClass="btn btn-primary  btn-mini" CommandArgument='<%#Eval("SourceProductId") %>' CommandName="DownLoad" runat="server"></asp:LinkButton>
                                </td>
                                <td style="word-break: break-all;"><%#Eval("PurchasePrice") %></td>
                                <td><%#Eval("SaleOrice") %></td>
                                <td style="word-break: break-all;"><%#Eval("SourceCount") %></td>
                                <td style="word-break: break-all;"><%#Eval("Unit") %></td>
                                <td class="remainder" style="word-break: break-all;"><%#GetLeaveCount(Eval("SourceProductId")) %></td>
                                <td style="word-break: break-all;"><%#GetLastUsedDate(Eval("SourceProductId")) %></td>
                                <td style="word-break: break-all;"><%#GetUsedTimes(Eval("SourceProductId")) %></td>
                                <td><span style="cursor: default" title='<%#Eval("Position") %>'><%#ToInLine(Eval("Position"),4) %></span></td>
                                <td style="width: 160px"><a href="#" class="btn btn-mini btn-primary" onclick='StorehouseScrapLogCreate(<%#Eval("SourceProductId") %>,this);'>报损</a>
                                    <a href="#" class="btn btn-mini btn-primary" onclick='StorehouseProductSourceCountCreate(<%#Eval("SourceProductId") %>,this);'>添加数量</a>
                                    <asp:PlaceHolder ID="phEditOper" runat="server">
                                        <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("SourceProductId") %>,this);'>修改</a>
                                    </asp:PlaceHolder>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="15">
                            <asp:Button runat="server" ID="btnExport" Text="导出Excel" CssClass="btn btn-primary" OnClick="btnExport_Click" />
                        </td>
                    </tr>
                </tfoot>
            </table>

            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                    <table id="tbMain" class="table table-bordered table-striped table-select">
                        <thead>
                            <tr id="trContent">
                                <th style="white-space: nowrap;">产品名称</th>
                                <th style="white-space: nowrap;">入库时间</th>
                                <th style="white-space: nowrap;">产品类别</th>
                                <th style="white-space: nowrap;">项目</th>
                                <th style="white-space: nowrap;">产品\服务描述</th>
                                <th style="white-space: nowrap;">采购单价</th>
                                <th style="white-space: nowrap;">销售单价</th>
                                <th style="white-space: nowrap;">数量</th>
                                <th style="white-space: nowrap;">单位</th>
                                <th style="white-space: nowrap;">剩余数量</th>
                                <th style="white-space: nowrap;">最近使用时间</th>
                                <th style="white-space: nowrap;">使用总次数</th>
                                <th style="white-space: nowrap;">仓位</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptStorehouse" runat="server">
                                <ItemTemplate>
                                    <tr skey='<%#Eval("SourceProductId") %>'>
                                        <td><span style="cursor: default" title='<%#Eval("SourceProductName") %>'><%#ToInLine(Eval("SourceProductName"),6) %></span></td>
                                        <td style="word-break: break-all;"><%#GetDateStr(Eval("PutStoreDate")) %></td>
                                        <td><%#GetCategoryName(Eval("ProductCategory")) %></td>
                                        <td style="word-break: break-all;"><%#GetCategoryName(Eval("ProductProject")) %></td>
                                        <td><span style="cursor: default" title='<%#Eval("Specifications") %>'><%#ToInLine(Eval("Specifications"),8) %></span></td>
                                        <td style="word-break: break-all;"><%#Eval("PurchasePrice") %></td>
                                        <td><%#Eval("SaleOrice") %></td>
                                        <td style="word-break: break-all;"><%#Eval("SourceCount") %></td>
                                        <td style="word-break: break-all;"><%#Eval("Unit") %></td>
                                        <td class="remainder" style="word-break: break-all;"><%#GetLeaveCount(Eval("SourceProductId")) %></td>
                                        <td style="word-break: break-all;"><%#GetLastUsedDate(Eval("SourceProductId")) %></td>
                                        <td style="word-break: break-all;"><%#GetUsedTimes(Eval("SourceProductId")) %></td>
                                        <td><span style="cursor: default" title='<%#Eval("Position") %>'><%#ToInLine(Eval("Position"),4) %></span></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </ItemTemplate>
            </asp:Repeater>

            <cc1:AspNetPagerTool ID="StorePager" PageSize="10" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
