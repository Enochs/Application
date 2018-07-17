<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskList" Title="所有订单执行状态" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>


<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>

<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
    <script src="/Scripts/trselection.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div style="overflow-y: auto; height: 1000px;">
        <div class="widget-box">
            <div class="widget-box" style="height: auto; border: 0px;">
                <table>
                    <tr>
                        <td colspan="2">
                            <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
                        </td>
                        <td>日期：<asp:DropDownList ID="ddltype" Width="90" runat="server">
                            <asp:ListItem Value="1" Text="婚期" />
                            <asp:ListItem Value="2" Text="订单日期" />
                        </asp:DropDownList>
                        </td>
                        <td>
                            <HA:DateRanger runat="server" ID="QueryDateRanger" />
                        </td>
                        <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                        <td>部门：<cc2:DepartmentDropdownList ID="DepartmentDropdownList1" runat="server"></cc2:DepartmentDropdownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">用户类型:
                            <asp:DropDownList runat="server" ID="ddlEmployeeType">
                                <asp:ListItem Text="请选择" Value="-1" />
                                <asp:ListItem Text="录入人" Value="Recorder" />
                                <asp:ListItem Text="电销" Value="InviteEmployee" />
                                <asp:ListItem Text="婚礼顾问" Value="OrderEmployee" />
                                <asp:ListItem Text="策划师" Value="EmployeeID" />
                            </asp:DropDownList></td>
                        <td>
                            <HA:MyManager runat="server" ID="MyManager" />
                        </td>
                        <td colspan="2">金额区间:
                            <asp:DropDownList runat="server" ID="ddlStartPayMent">
                                <asp:ListItem Text="请选择" Value="-1" />
                                <asp:ListItem Text="9999" Value="1" />
                                <asp:ListItem Text="19999" Value="2" />
                                <asp:ListItem Text="29999" Value="3" />
                                <asp:ListItem Text="39999" Value="4" />
                                <asp:ListItem Text="49999" Value="5" />
                                <asp:ListItem Text="59999" Value="6" />
                                <asp:ListItem Text="69999" Value="7" />
                                <asp:ListItem Text="79999" Value="8" />
                                <asp:ListItem Text="89999" Value="9" />
                                <asp:ListItem Text="99999" Value="10" />
                                <asp:ListItem Text="199999" Value="11" />
                                <asp:ListItem Text="299999" Value="12" />
                                <asp:ListItem Text="399999" Value="13" />
                                <asp:ListItem Text="499999" Value="14" />
                                <asp:ListItem Text="599999" Value="15" />
                                <asp:ListItem Text="699999" Value="16" />
                                <asp:ListItem Text="799999" Value="17" />
                                <asp:ListItem Text="899999" Value="18" />
                                <asp:ListItem Text="999999" Value="19" />
                            </asp:DropDownList>
                            至
                        <asp:DropDownList runat="server" ID="ddlEndPayMent">
                            <asp:ListItem Text="请选择" Value="-1" />
                            <asp:ListItem Text="9999" Value="1" />
                            <asp:ListItem Text="19999" Value="2" />
                            <asp:ListItem Text="29999" Value="3" />
                            <asp:ListItem Text="39999" Value="4" />
                            <asp:ListItem Text="49999" Value="5" />
                            <asp:ListItem Text="59999" Value="6" />
                            <asp:ListItem Text="69999" Value="7" />
                            <asp:ListItem Text="79999" Value="8" />
                            <asp:ListItem Text="89999" Value="9" />
                            <asp:ListItem Text="99999" Value="10" />
                            <asp:ListItem Text="199999" Value="11" />
                            <asp:ListItem Text="299999" Value="12" />
                            <asp:ListItem Text="399999" Value="13" />
                            <asp:ListItem Text="499999" Value="14" />
                            <asp:ListItem Text="599999" Value="15" />
                            <asp:ListItem Text="699999" Value="16" />
                            <asp:ListItem Text="799999" Value="17" />
                            <asp:ListItem Text="899999" Value="18" />
                            <asp:ListItem Text="999999" Value="19" />
                        </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td>渠道类型：</td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <cc2:ddlChannelType CssClass="ddlChannel" ID="ddlChannelType1" ClientIDMode="Static" runat="server" Width="134px" AutoPostBack="True" OnSelectedIndexChanged="ddlChannelType1_SelectedIndexChanged">
                                    </cc2:ddlChannelType>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>渠道名称：</td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <cc2:ddlChannelName CssClass="ddlChannel" ID="ddlChannelName2" ClientIDMode="Static" runat="server" Width="134px" AutoPostBack="true">
                                                </cc2:ddlChannelName>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>


                        </td>
                        <td>
                            <asp:Button ID="BtnQuery" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="BindData" />
                            <cc2:btnReload runat="server" ID="btnReload" />
                        </td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <%--<tr>
                        <th>新人</th>
                        <th>婚期</th>
                        <th>酒店</th>

                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>订单状态</th>
                        <th>订单金额</th>

                        <th>订单日期</th>
                        <th>未收款</th>
                        <th>毛利率</th>
                        <th>总成本</th>

                        <th>查看</th>
                        <%#Eval("ParentDispatchingID")!=null?Eval("ParentDispatchingID").ToString()!="0"?"(变更)":"":"" %>
                    </tr>--%>
                    <tr>
                        <th>新人</th>
                        <th>婚期</th>
                        <th>签单日期</th>
                        <th>酒店</th>
                        <th>电销</th>
                        <th>婚礼顾问</th>
                        <th>婚礼策划</th>
                        <th>邀约类型</th>
                        <th>新人状态</th>
                        <th>已收款</th>
                        <th>订单金额</th>
                        <th>成本</th>
                        <th>毛利率</th>
                        <th>客户满意度</th>
                        <th>内部评价</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan").ToString().Length > 6 ? Eval("ContactMan").ToString().Substring(0,6) : Eval("ContactMan") %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#ShowShortDate(Eval("OrderCreateDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetEmployeeName(Eval("InviteEmployee")) %></td>
                                <td><%#GetEmployeeName(Eval("OrderEmployee")) %></td>
                                <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                                 <td><%#GetApplyName(Eval("ApplyType")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetQuotedDispatchingFinishMoney(Eval("CustomerID")) %></td>
                                <td><%#Eval("FinishAmount") %></td>
                                <td><%#GetCostSum(Eval("CustomerID")) %></td>
                                <td><%#GetRates(Eval("CustomerID"),Eval("FinishAmount")) %></td>
                                <td><a <%#GetSacNameByCustomernId(Eval("CustomerID")).ToString() == "很糟糕" ? "style='color:red;'" : "" %> style="cursor: pointer;" href="../CS/CS_DegreeOfSatisfactionShow.aspx?DofKey=<%#GetDofKeyByCustomernId(Eval("CustomerID")) %>" target="_blank"><%#GetSacNameByCustomernId(Eval("CustomerID")) %></a></td>
                                <td><a <%#GetSacNameByCustomernId(Eval("CustomerID")).ToString() == "很糟糕" ? "style='color:red;'" : "" %> style="cursor: pointer;" href="../Carrytask/CarryCost/OrderCost.aspx?DispatchingID=<%#GetDispachingIDByCustomerID(Eval("CustomerID")) %>&CustomerID=<%#Eval("CustomerID") %>&Type=Details&NeedPopu=1" target="_blank"><%#GetNameByEvaulationId(Eval("EvaluationId")) %><a></td>
                                <td><a class="btn btn-primary btn-mini" target="_blank" href="/AdminPanlWorkArea/QuotedPrice/QuotedPriceShow.aspx?QuotedID=<%#GetQuotedidByCustomerID(Eval("CustomerID")) %>&OrderID=<%#Eval("OrderID") %>&CustomerID=<%#Eval("CustomerID") %>&NeedPopu=1">报价单</a>
                                    <%-- <a class="btn btn-primary btn-mini" target="_blank" href="CarryCost/OrderCost.aspx?DispatchingID=<%#GetDispachingIDByCustomerID(Eval("CustomerID")) %>&CustomerID=<%#Eval("CustomerID") %>&OrderID=&Type=Details&NeedPopu=1">执行评价</a>
                                    <a class="btn btn-primary btn-mini" target="_blank" href="../CS/CS_DegreeOfSatisfactionShow.aspx?DofKey=<%#GetDofKeyByCustomernId(Eval("CustomerID")) %>">满意度</a>--%>
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
                        <td></td>
                        <td>本页合计</td>
                        <td>
                            <asp:Label runat="server" ID="lblPageFinishMoney" /></td>
                        <td>
                            <asp:Label runat="server" ID="lblPageFinishAmount" /></td>
                        <td>
                            <asp:Label runat="server" ID="lblPageCostSum" /></td>
                        <td></td>
                    </tr>
                    <tr>

                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>本期合计</td>
                        <td>
                            <asp:Label runat="server" ID="lblSumFinishMoney" /></td>
                        <td>
                            <asp:Label runat="server" ID="lblSumFinishAmount" /></td>
                        <td>
                            <asp:Label runat="server" ID="lblSumCostSum" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="11">
                            <asp:Label runat="server" ID="lblTotalSums" Text="" Style="font-size: 14px; font-weight: bolder; color: red;" />
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BindData" PageSize="20"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>

        </div>
        <div style="margin-top: 21px;">
            <strong>汇总统计</strong><asp:Button ID="btnExportoExcel" CssClass="btn btn-mini btn-primary" runat="server" Text="导出到Excel" OnClick="btnExportoExcel_Click" />
        </div>
        <div id="tongji" style="">
            <table class="table table-bordered table-striped" style="width: auto;">
                <thead>
                    <tr id="trSum">
                        <th style="white-space: nowrap;">&nbsp;</th>
                        <th style="white-space: nowrap;">订单数量</th>
                        <th style="white-space: nowrap;">订单总金额</th>
                        <th style="white-space: nowrap;">已收款</th>
                        <th style="white-space: nowrap;">平均消费</th>
                        <th style="white-space: nowrap;">总成本</th>
                        <th style="white-space: nowrap;">毛利率</th>
                        <th style="white-space: nowrap;">满意度</th>
                        <th style="white-space: nowrap;">超越期望值</th>
                        <th style="white-space: nowrap;">达到期望值</th>
                        <th style="white-space: nowrap;">未达到期望值</th>
                        <th style="white-space: nowrap;">未评价</th>
                        <th style="white-space: nowrap;">未查询到</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>

                        <td>当期</td>
                        <td>
                            <asp:Label ID="lblOrderCount" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Literal ID="ltlCurrentExcuteOrderCount" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Label runat="server" ID="lblFinishAmount"></asp:Label></td>
                        <td>
                            <asp:Label runat="server" ID="lblAvgCost"></asp:Label></td>
                        <td>
                            <asp:Label ID="lblTotalCost" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Literal ID="ltlCurrentExcuteApp" runat="server"></asp:Literal></td>
                        <td>
                            <asp:Label runat="server" ID="lblSumSatisfaction" Text="" /></td>
                        <td>
                            <asp:Label runat="server" ID="lblOverSatisfaction" Text="" /></td>
                        <td>
                            <asp:Label runat="server" ID="lblCarrySataisfaction" Text="" /></td>
                        <td>
                            <asp:Label runat="server" ID="lblNotCarrySatisfacion" Text="" /></td>
                        <td>
                            <asp:Label runat="server" ID="lblNotEvaulation" Text="" /></td>
                        <td>
                            <asp:Label runat="server" ID="lblNoCustomer" Text="" />
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
    <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
</asp:Content>
