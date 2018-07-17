<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CS_ComplainManagerAll.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_ComplainManagerAll" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
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
            var Url = "CS_ComplainUpdate.aspx?ComplainID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }

        function ShowDetailsWindows(KeyID, Control) {
            var Url = "CS_ComplainDetails.aspx?ComplainID=" + KeyID;
            $(Control).attr("id", "detailsShow" + KeyID);
            showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {

            // showPopuWindows($("#createComplain").attr("href"), 700, 1000, "a#createComplain");
            //$("#<//%=txtPartyDateStar.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            //$("#<//%=txtPartyDateEnd.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });

            $("#trContent th").css({ "white-space": "nowrap" });

            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {

                $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");
                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }


            });

            $(".queryTable2").css({ "margin-left": "15px", "margin-top": "5px" });//98    24
            $(".queryTable2 td").each(function (indexs, values) {

                $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");

                if (indexs == $(".queryTable2 td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });

            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");

            $("html").css({ "overflow-x": "hidden" });
            //针对小屏幕分辨率
            if (window.screen.width >= 1280 && window.screen.width <= 1366) {
                //width:1120px;


                //$("#queryBox").css({ "height": "110px" });
                //$(".queryTable3").show();
                //$(".queryTable3").css({ "margin-left": "15px", "margin-top": "5px" });//98    24
                //$(".queryTable3 td").each(function (indexs, values) {
                //    $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");

                //    if (indexs == $(".queryTable3 td").length - 1) {
                //        $(this).css("vertical-align", "top");
                //    }

                //});

                //for (var i = 0; i < 3; i++) {
                //    $("#queryTable2Content td").last().append("&nbsp;&nbsp;&nbsp;&nbsp;").insertBefore($("#queryTable3Content td").first());
                //}
                // $("#trContent th").css({ "white-space": "normal" });
                //$("#trContent").html();
                $("#trContent").hide();
                $("#trSmallContent").show();
                $("#tblMain").css({ "width": 962 + "px" });

            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <%--   <a href="CS_ComplainCreate.aspx" class="btn btn-primary  btn-mini" id="createComplain">添加客户投诉</a>--%>

        <div class="widget-box">


            <div class="widget-box" id="queryBox" >


                <table class="queryTable">
                    <tr>
                        <td>
                            <table id="queryTableContent">
                                <tr>

                                    <td>姓名:
                  
                                        <asp:TextBox ID="txtCustomrer" runat="server" MaxLength="10"></asp:TextBox>

                                    </td>
                                    <td>电话:
                                        <asp:TextBox ID="txtCelPhone" runat="server" MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td>婚期
                                    <asp:TextBox ID="txtPartyDateStar" onclick="WdatePicker();" runat="server" MaxLength="20"></asp:TextBox>
                                        到<asp:TextBox ID="txtPartyDateEnd" onclick="WdatePicker();" runat="server" MaxLength="20"></asp:TextBox>
                                    </td>


                                    <td>&nbsp;</td>

                                    <td>
                                        新人状态: <cc2:ddlCustomersState ID="ddlCustomerState" runat="server"></cc2:ddlCustomersState>

                                        <asp:Button ID="btnQuery" CssClass="btn btn-primary" runat="server" Text="查找" OnClick="btnQuery_Click" />

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            
            </div>
         

            <table class="table table-bordered table-striped" id="tblMain">
                <thead>
                    <tr id="trContent">
                        <th>订单</th>
                        <th>订单总金额</th>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>派工人</th>
                        <th>订单状态</th>
                        <th>订单时间</th>
                        <th>投诉受理人</th>
                        <th>受理时间</th>
                        <th>受理状态</th>
                        <th>操作</th>
                    </tr>
                    <tr id="trSmallContent" style="display: none;">
                        <th>订单</th>
                        <th>订单总金额</th>
                        <th>新人姓名</th>
                        <th width="12">联系电话</th>
                        <th width="10">婚期</th>
                        <th width="12">酒店</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>派工人</th>
                        <th>订单状态</th>
                        <th>订单时间</th>
                        <th>投诉受理人</th>
                        <th>受理时间</th>
                        <th>受理状态</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptComplain" runat="server" OnItemCommand="rptComplain_ItemCommand">

                        <ItemTemplate>

                            <tr>
                                <td><%# GetOrderCodeByCustomerID(Eval("CustomerID")) %></td>
                                <td><%#GetAggregateAmount(Eval("CustomerID"))%></td>
                                <td><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></td>
                                <td style="word-break: break-all;"><%#Eval("BrideCellPhone") %></td>
                                <td style="word-break: break-all;"><%#GetDateStr( Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeName(GetOrderIdByCustomerID(Eval("CustomerID"))) %></td>
                                <td><%#GetQuotedEmpLoyeeName(GetOrderIdByCustomerID(Eval("CustomerID"))) %></td>
                                <td><%#GetEmployeeID(Eval("CustomerID")) %> </td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetLastFollowDateByCustomerId(Eval("CustomerID")) %></td>
                                <td><%#GetEmployeeName(Eval("ComplainEmployeeId")) %></td>
                                <td style="word-break: break-all;"><%#GetDateStr( Eval("ReturnDate")) %></td>
                                <td><%#GetComplainStateContent(Eval("ReturnContent")) %></td>
                                <td>

                                    <a href="CS_ComplainCreate.aspx?ComplainID=<%#Eval("ComplainID") %>" class="btn btn-primary  btn-mini <%#Eval("ReturnContent").ToString().Trim()!=string.Empty?"RemoveClass":"" %>" <%#Eval("ComplainEmployeeId").ToString()==User.Identity.Name?"":"style='display:none;'" %> >处理投诉</a>
                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowDetailsWindows(<%#Eval("ComplainID") %>,this);'>查看处理结果</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="ComplainPager"  OnPageChanged="ComplainPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
