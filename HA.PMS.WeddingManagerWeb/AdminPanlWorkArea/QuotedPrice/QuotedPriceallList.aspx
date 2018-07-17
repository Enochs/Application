<%@ Page Language="C#" Title="订单明细" AutoEventWireup="true" CodeBehind="QuotedPriceallList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceallList" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script src="/Scripts/trselection.js"></script>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table>
                <tr>
                    <td>新人姓名：</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtContactMan" />&nbsp;&nbsp;</td>
                    <td>联系电话：
                        <asp:TextBox runat="server" ID="txtContactPhone" />&nbsp;&nbsp;</td>
                    <td>责任人：
                        <asp:DropDownList runat="server" ID="ddlEmployeeType">
                            <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="录入人" Value="Recorder" />
                            <asp:ListItem Text="电销" Value="InviteEmployee" />
                            <asp:ListItem Text="婚礼顾问" Value="OrderEmployee" />
                            <asp:ListItem Text="婚礼策划" Value="QuotedEmployee" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" Title="" />
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
                    <td>酒店：
                        <cc2:ddlHotel runat="server" ID="ddlHotel" />
                    </td>
                </tr>
                <tr>
                    <td>新人状态：</td>
                    <td>
                        <cc2:ddlCustomersState runat="server" ID="ddlCustomersState1" Width="134px">
                        </cc2:ddlCustomersState></td>
                    <td>时间排序：
                        <asp:DropDownList runat="server" ID="ddlSortName" Width="134px">
                            <asp:ListItem Text="婚期" Value="PartyDate" />
                            <asp:ListItem Text="订单日期" Value="QuotedCreateDate" />
                            <asp:ListItem Text="签约日期" Value="QuotedDateSucessDate" />
                        </asp:DropDownList>
                    </td>
                    <td>状态：</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlState">
                            <asp:ListItem Text="请选择" Value="0" Selected="True" />
                            <asp:ListItem Text="已签约" Value="1" />
                            <asp:ListItem Text="未签约" Value="2" />
                            <asp:ListItem Text="尾款差额" Value="3" />
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="2">查询条件：
                        <asp:DropDownList runat="server" ID="ddlTimeType" Width="134px" ClientIDMode="Static">
                            <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="婚期" Value="PartyDate" />
                            <asp:ListItem Text="订单日期" Value="QuotedCreateDate" />
                            <asp:ListItem Text="签约日期" Value="QuotedDateSucessDate" />
                            <asp:ListItem Text="订单金额" Value="FinishAmount" />
                        </asp:DropDownList>
                    </td>
                    <td id="td_timeSpan" class="td_timeSpan">
                        <HA:DateRanger runat="server" ID="DateRangers" />
                    </td>
                    <td id="td_FinishAmount" class="td_FinishAmount" style="display: none">
                        <asp:TextBox runat="server" ID="txtStartFinishAmount" Width="64px" />
                        －
                        <asp:TextBox runat="server" ID="txtEndFinishAmount" Width="64px" />
                    </td>
                    <td>
                        <cc2:btnManager ID="BtnQuery" Text="查询" Visible="true" runat="server" OnClick="BtnQuery_Click" />
                        <cc2:btnReload runat="server" ID="btnReload" />
                    </td>
                </tr>
            </table>

            <script type="text/javascript">

                $(document).ready(function () {
                    var text = $("#<%=ddlTimeType.ClientID %> option:selected").text();
                    if (text == "订单金额") {
                        $("#td_timeSpan").css("display", "none");
                        $("#td_FinishAmount").css("display", "block");
                    } else {
                        $("#td_timeSpan").css("display", "block");
                        $("#td_FinishAmount").css("display", "none");
                    }

                    $("#ddlTimeType").change(function () {
                        var text = $("#<%=ddlTimeType.ClientID %> option:selected").text();
                        if (text == "订单金额") {
                            $("#td_timeSpan").css("display", "none");
                            $("#td_FinishAmount").css("display", "block");
                            document.getElementById("<%=txtStartFinishAmount.ClientID%>").value = "";
                            document.getElementById("<%=txtEndFinishAmount.ClientID%>").value = "";
                        } else {
                            $("#td_timeSpan").css("display", "block");
                            $("#td_FinishAmount").css("display", "none");
                        }
                    });
                });

            </script>

            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>新人</th>
                        <th>电话</th>
                        <th>婚期</th>
                        <th>订单日期</th>
                        <th>签约日期</th>
                        <th>酒店</th>
                        <th>渠道</th>
                        <th>录入人</th>
                        <th>婚礼顾问</th>
                        <th>婚礼策划</th>
                        <th>邀约类型</th>
                        <th>新人状态</th>
                        <th>已收款</th>
                        <th>订单金额</th>
                        <th>未收款</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride").ToString().Length > 6 ? Eval("Bride").ToString().Substring(0,6) : Eval("Bride"),Eval("Groom").ToString().Length > 6 ? Eval("Groom").ToString().Substring(0,6) : Eval("Groom")) %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#ShowShortDate(Eval("QuotedCreateDate")) %></td>
                                <td><%#ShowShortDate(Eval("QuotedDateSucessDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#Eval("Channel") %></td>
                                <td><%#GetEmployeeName(Eval("Recorder")) %></td>
                                <td><%#GetEmployeeName(Eval("InviteEmployee")) %></td>
                                <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                                <td><%#GetApplyName(Eval("ApplyType")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetQuotedDispatchingFinishMoney(Eval("OrderID")) %></td>
                                <td><%#Eval("FinishAmount") %></td>
                                <td><%#GetNoFinishAmount(Eval("OrderID"),Eval("FinishAmount")) %></td>
                                <td id="tbOper">
                                    <a class="btn btn-primary btn-mini" href="QuotedPriceListCreateEdit.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>" <%#ShowUpdate(Eval("IsFirstCreate"))%> target="_blank">制作报价单</a>
                                    <a class="btn btn-primary btn-mini" href="QuotedPriceShow.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>" <%#HideCreate(Eval("IsFirstCreate"))%> target="_blank">查看报价单</a>
                                    <a class="btn btn-primary btn-mini" href="QuotedPriceChangeList.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>" <%#HideChecks(Eval("IsChecks"))%> target="_blank">制作变更报价单</a>
                                    <a class="btn btn-primary btn-mini" href="QuotedPriceListCreateEdit.aspx?OrderID=<%#Eval("OrderID") %>&IsFinish=1&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>" <%#HideChecks(Eval("IsChecks"))%> target="_blank">确定订单</a>
                                    <asp:HiddenField ID="hideEmpLoyeeID" Value="1" runat="server" />
                                    <asp:HiddenField ID="hideCustomerHide" Value='<%#Eval("CustomerID") %>' runat="server" />
                                    <a target="_blank" class="btn btn-primary btn-mini <%#SetClass(Eval("QuotedID")) %>" href="QuotedPriceDispatchingUpdate.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&QuotedEmployee=<%#Eval("EmployeeID") %>">执行明细</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>本页合计：</td>
                        <td>
                            <asp:Label runat="server" ID="lblPageFinishMoney" Text="" /></td>
                        <td>
                            <asp:Label runat="server" ID="lblPageFinishAmount" Text="" /></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label runat="server" ID="lblTotalSums" Text="" Style="font-size: 14px; font-weight: bolder; color: red;" /></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>本期合计：</td>
                        <td>
                            <asp:Label runat="server" ID="lblSumFinishMoney" Text="" /></td>
                        <td>
                            <asp:Label runat="server" ID="lblSumFinishAmount" Text="" /></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="10">
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

