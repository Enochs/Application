<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Salestrends.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.Salestrends" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforEmpLoyee.ascx" TagPrefix="HA" TagName="MessageBoardforEmpLoyee" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/Scripts/Function/jquery.fancybox.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.metadata.js"></script>
    <script type="text/javascript" src="/Scripts/jquery.contextmenu.r2.js"></script>
    <script type="text/javascript" src="/Scripts/Function/jquery.fancybox.pack.js"></script>
    <script type="text/javascript" src="/Scripts/Function/masterFunction.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".Right1").contextMenu('myMenu1', {
                bindings: {

                    'open': function (t) {
                        window.open(($(t).parent().attr("href")));
                    },

                    'fudao': function (t) {
                        ShowOrderMessAgeOrderID(($(t).attr("orderid")));
                    }
                }
            });
        });


        function ShowOrderMessAgeOrderID(OrderID) {
            var URI = "/AdminPanlWorkArea/StoreSales/OrderMessAge.aspx?OrderID=" + OrderID;
            showPopuWindows(URI, 700, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
            return false;
        }


    </script>
    <style type="text/css">
        body, div, dl, dt, dd, ul, ol, li, h1, h2, h3, h4, h5, h6, pre, code, form, textarea, select, optgroup, option, fieldset, legend, p, blockquote, th, td {
            margin: 0;
            padding: 0;
        }

        .out {
            position: relative;
            top: 50px;
            background: url(loudou.jpg) no-repeat 50% 0;
            width: 1039px;
            margin: 0 auto;
            height: 527px;
            overflow: hidden;
        }

        .area0, .area1, .area2, .area3, .area4, .area5 {
            position: absolute;
            overflow: hidden;
            border: 0px solid #ccc;
        }

        .area0 {
            left: 0px;
            top: 0px;
            width: 720px;
            height:65px;
        }

        .area1 {
            left: 87px;
            top: 74px;
            width: 504px;
            height: 56px;
        }

        .area2 {
            left: 166px;
            top: 130px;
            width: 345px;
            height: 94px;
        }

        .area3 {
            left: 236px;
            top: 224px;
            width: 196px;
            height: 142px;
        }

        .area4 {
            left: 256px;
            top: 366px;
            width: 176px;
            height: 118px;
        }

        .area5 {
            left: 285px;
            top: 486px;
            width: 109px;
            height: 29px;
        }
    </style>
</head>

<body>

    <form id="form1" runat="server">
        <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
        <div class="contextMenu" id="myMenu1">
            <ul>
                <li id="open">查看</li>
                <li id="fudao">辅导</li>
            </ul>
        </div>
        <span style="color: red;">操作说明:点击客户头像可以查看客户信息，对客户头像点击右键可以进行辅导！</span>
        <div class="out">
            <div class="area0">
                <asp:DataList ID="daltop" runat="server" RepeatDirection="Horizontal" RepeatColumns="35" BorderStyle="None">
                    <ItemTemplate>
                        <a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?OnlyView=1&CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>" title="客户:<%#Eval("Bride") %>&#10;跟单人:<%#GetEmployeeName(Eval("EmployeeID")) %> &#10;跟踪次数：<%#Eval("FlowCount") %>次 &#10;电话:<%#Eval("BrideCellPhone") %> &#10;婚期：<%#DateTime.Parse( Eval("PartyDate").ToString()).ToShortDateString() %> 请注意：点右键辅导">
                            <%#GetImageString(Eval("FlowCount"),Eval("OrderID")) %>
                        </a>
                    </ItemTemplate>
                </asp:DataList>

            </div>
            <!--框0-->
            <div class="area1">
                <asp:DataList ID="daltopEmpLoyeeName" runat="server" RepeatDirection="Horizontal" RepeatColumns="20" BorderStyle="None">
                    <ItemTemplate>
                        <a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?OnlyView=1&CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>" title="客户:<%#Eval("Bride") %>&#10; 跟单人:<%#GetEmployeeName(Eval("EmployeeID")) %> &#10;跟踪次数：<%#Eval("FlowCount") %>次&#10; 电话:<%#Eval("BrideCellPhone") %> &#10;婚期：<%#DateTime.Parse( Eval("PartyDate").ToString()).ToShortDateString() %> &#10;请注意：点右键辅导">
                            <%#GetImageString(Eval("FlowCount"),Eval("OrderID")) %>
                        </a>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <!--框1-->
            <div class="area2">

                <asp:DataList ID="dalFirepoint" runat="server" RepeatDirection="Horizontal" RepeatColumns="12" BorderStyle="None">
                    <ItemTemplate>
                        <a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>" title="客户:<%#Eval("Bride") %> &#10;跟踪次数：<%#Eval("FlowCount") %>次 &#10;电话:<%#Eval("BrideCellPhone") %> &#10;婚期：<%#DateTime.Parse( Eval("PartyDate").ToString()).ToShortDateString() %> &#10;请注意：点右键辅导">
                            <%#GetImageString(Eval("FlowCount"),Eval("OrderID")) %>
                        </a>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <!--框2-->
            <div class="area3">
                <asp:DataList ID="dalyouxuan" runat="server" RepeatDirection="Horizontal" RepeatColumns="9" BorderStyle="None">
                    <ItemTemplate>
                        <a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>" title="客户:<%#Eval("Bride") %> &#10;跟踪次数：<%#Eval("FlowCount") %>次 &#10;电话:<%#Eval("BrideCellPhone") %> &#10;婚期：<%#DateTime.Parse( Eval("PartyDate").ToString()).ToShortDateString() %> &#10;请注意：点右键辅导">
                            <%#GetImageString(Eval("FlowCount"),Eval("OrderID")) %>
                        </a>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <!--框3-->
            <div class="area4">
                <asp:DataList ID="dalSucess" runat="server" RepeatDirection="Horizontal" RepeatColumns="6" BorderStyle="None">
                    <ItemTemplate>
                        <a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>" title="客户:<%#Eval("Bride") %>&#10; 跟踪次数：<%#Eval("FlowCount") %>次 &#10;电话:<%#Eval("BrideCellPhone") %> &#10;婚期：<%#DateTime.Parse( Eval("PartyDate").ToString()).ToShortDateString() %> &#10;请注意：点右键辅导">
                            <%#GetImageString(Eval("FlowCount"),Eval("OrderID")) %>
                        </a>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <!--框4-->
            <div class="area5">
                <asp:DataList ID="dtlSucessOrder" runat="server" RepeatDirection="Horizontal" RepeatColumns="10" BorderStyle="None">
                    <ItemTemplate>
                        <a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>&FlowOrder=1" title="客户:<%#Eval("Bride") %> &#10;跟踪次数：<%#Eval("FlowCount") %>次 &#10;请注意：点右键辅导">
                            <img alt="x" style="width: 25px; height: 25px;" src="/AdminPanlWorkArea/Commandanddispatch/ManagerImage/zhengchang.jpg" />
                        </a>
                    </ItemTemplate>
                </asp:DataList>
            </div>
            <!--框5-->

        </div>
    </form>

</body>
</html>
