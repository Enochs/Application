<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CS_TakeDiskManagerAllTake.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_TakeDiskManagerAllTake" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 20px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }
    </style>

    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "CS_TakeDiskUpdate.aspx?TakeID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 800, "a#" + $(Control).attr("id"));
        }


        $(document).ready(function () {

            showPopuWindows($("#createTakeDisk").attr("href"), 700, 1000, "a#createTakeDisk");
            //$("#<//%=txtStart.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            //$("#<//%=txtEnd.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });


            $("#trContent th").css({ "white-space": "nowrap" });

            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {


                if (indexs != 3) {
                    $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });

            $(".queryTable2").css({ "margin-left": "15px", "margin-top": "5px" });//98    24
            $(".queryTable2 td").each(function (indexs, values) {


                if (indexs != 2) {
                    $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                if (indexs == $(".queryTable2 td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });

            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");

            $("html").css({ "overflow-x": "hidden" });
            $("html,body").css({ "background-color": "transparent" });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">



            <div class="widget-box" style="height: 50px; border: 0px;">

                <table class="queryTable" style="border-bottom: 1px solid #c7d5de;">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>新人姓名:<asp:TextBox ID="txtGroom" runat="server"></asp:TextBox>
                                    </td>
                                    <td>电话:<asp:TextBox ID="txtGroomCellPhone" runat="server"></asp:TextBox>
                                    </td>
                                    <td>婚期:<asp:TextBox ID="txtStart" onclick="WdatePicker();" runat="server"></asp:TextBox>
                                    </td>
                                    <td>至
                                        <asp:TextBox ID="txtEnd" onclick="WdatePicker();" runat="server"></asp:TextBox>
                                    </td>

                                    <td>
                                        <asp:Button ID="btnFind" Height="27" OnClick="btnFind_Click" CssClass="btn btn-info" runat="server" Text="查找" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table class="queryTable2">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>实际取件日期：<asp:TextBox ID="txtRealityTimeStar" onclick="WdatePicker();" runat="server"></asp:TextBox>
                                    </td>
                                    <td>至<asp:TextBox ID="txtRealityTimeEnd" onclick="WdatePicker();" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnQuery" Height="27" CssClass="btn btn-primary" runat="server" Text="查询" OnClick="btnQuery_Click" />
                                        <cc2:btnReload ID="btnReload2" runat="server" />
                                        <asp:Button ID="BtnExport" runat="server" Text="导出到Excel" OnClick="BtnExport_Click" CssClass="btn btn-primary" />
                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </div>

            <br />
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>新人姓名</th>
                        <th>电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>收件时间</th>
                        <th>通知新人时间</th>
                        <th>取件时间</th>
                        <th>操作</th>
                    </tr>
                </thead>

                <tbody>
                    <asp:Repeater ID="rptTalkeDisk" runat="server">

                        <ItemTemplate>

                            <tr>
                                <td><a href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1" target="_blank"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("GroomCellPhone") %></td>
                                <td><%#ShowPartyDate( Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeName(GetOrderIdByCustomerID(Eval("CustomerID"))) %></td>
                                <td><%#GetQuotedEmpLoyeeName(GetOrderIdByCustomerID(Eval("CustomerID"))) %></td>
                                <td><%#GetMaxDate(Eval("ConsigneeTime"),Eval("GetFileTime")) %></td>
                                <td><%#ShowShortDate( Eval("NoticeTime")) %></td>
                                <td><%#GetShortDateString(Eval("realityTime")) %></td>
                                <td><a href="CS_TakeDiskCheck.aspx?Type=Look&TakeID=<%#Eval("TakeID") %>" class="btn btn-primary btn-mini">审核记录</a></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="TalkeDiskPager" OnPageChanged="TalkeDiskPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />

        </div>
    </div>
</asp:Content>
