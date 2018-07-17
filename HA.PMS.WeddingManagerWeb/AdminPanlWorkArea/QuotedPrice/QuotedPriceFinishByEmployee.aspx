<%@ Page Language="C#" Title="已完成订单" AutoEventWireup="true" CodeBehind="QuotedPriceFinishByEmployee.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceFinishByEmployee" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <style type="text/css">
        .table thead tr th {
            text-align: left;
        }

        .tableStyle tr td {
            border-top: none;
            border-left: none;
            border-right: none;
            border-top: 1px solid #c4bdbd;
        }
    </style>
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btn_looks").click(function () {

            });
        });


        //上传图片
        function ShowFileUploadPopu(QuotedID) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/PraiseUpload.aspx?QuotedID=" + QuotedID;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>

    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table>
                <tr>
                    <td>新人姓名:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtContactMan" /></td>
                    <td>联系电话:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtContactPhone" /></td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" />
                    </td>
                    <td>
                        <HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" />
                    </td>
                    <td>状态<asp:DropDownList runat="server" ID="ddlState">
                        <asp:ListItem Text="请选择" Value="0" />
                        <asp:ListItem Text="未评价" Value="1" />
                        <asp:ListItem Text="已评价" Value="2" />
                    </asp:DropDownList></td>
                    <td>
                        <cc2:btnManager ID="BtnQuery" Text="查询" Visible="true" runat="server" OnClick="BtnQuery_Click" />
                        <cc2:btnReload ID="BtnReload" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="table table-bordered table-striped tableStyle">
                <thead>
                    <tr>
                        <th width="100">新人</th>
                        <th>电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>婚礼策划</th>
                        <th>已收款</th>
                        <th>订单金额</th>
                        <th>客户满意度</th>
                        <th>内部评价</th>
                        <th>毛利率</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>' <%#GetSacNameByCustomernId(Eval("CustomerID")).ToString() == "很糟糕" ? "style='color:red;'" : "" %>>
                                <td><a <%#GetSacNameByCustomernId(Eval("CustomerID")).ToString() == "很糟糕" ? "style='color:red;'" : "" %> target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride").ToString().Length > 6 ? Eval("Bride").ToString().Substring(0,6) : Eval("Bride"),Eval("Groom").ToString().Length > 6 ? Eval("Groom").ToString().Substring(0,6) : Eval("Groom")) %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' /></td>
                                </td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                                <td><%#GetEmployeeName(Eval("QuotedEmployee")) %></td>
                                <td><%#GetQuotedDispatchingFinishMoney(Eval("OrderID")) %></td>
                                <td><%#Eval("FinishAmount") %></td>
                                <td><a <%#GetSacNameByCustomernId(Eval("CustomerID")).ToString() == "很糟糕" ? "style='color:red;'" : "" %> style="cursor: pointer;" href="../CS/CS_DegreeOfSatisfactionShow.aspx?DofKey=<%#GetDofKeyByCustomernId(Eval("CustomerID")) %>" target="_blank"><%#GetSacNameByCustomernId(Eval("CustomerID")) %></a></td>
                                <td><a <%#GetSacNameByCustomernId(Eval("CustomerID")).ToString() == "很糟糕" ? "style='color:red;'" : "" %> style="cursor: pointer;" href="../Carrytask/CarryCost/OrderCost.aspx?DispatchingID=<%#GetDispatchingID(Eval("CustomerID")) %>&CustomerID=<%#Eval("CustomerID") %>&Type=Details&NeedPopu=1" target="_blank"><%#GetNameByEvaulationId(Eval("EvaluationId")) %><a></td>
                                <td><%#GetRates(Eval("CustomerID"),Eval("FinishAmount")) %></td>
                                <td id="tbOper">

                                    <a href="#" onclick="ShowFileUploadPopu('<%#Eval("QuotedID") %>')" class="btn btn-mini   btn-primary">好评</a>
                                    <a class="btn btn-danger btn-mini" id="btn_looks" href="QuotedPriceShow.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>" target="_blank">查看报价单</a>
                                    <a target="_blank" href="../FinancialAffairs/OrderDirectCostShow.aspx?OrderID=<%#Eval("OrderID") %>&DispatchingID=<%#GetDispatchingID(Eval("CustomerID")) %>&CustomerID=<%#Eval("CustomerID") %>" <%#GetIsLock(Eval("CustomerID"),2) %> class="btn btn-success btn-mini">执行明细</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td style="text-align: right;">本页合计：</td>
                        <td>
                            <asp:Label runat="server" ID="lblPageFinishAmount" /></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td style="text-align: right;">本期合计：</td>
                        <td>
                            <asp:Label runat="server" ID="lblAllFinishAmount" /></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="12">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
</asp:Content>

