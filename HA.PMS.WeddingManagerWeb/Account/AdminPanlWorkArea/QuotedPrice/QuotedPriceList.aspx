<%@ Page Language="C#" Title="订单列表" AutoEventWireup="true" CodeBehind="QuotedPriceList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceList" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="../Control/MyManager.ascx" TagName="MyManager" TagPrefix="uc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function Opens() {

            location.href = "http://www.baidu.com";
        }
    </script>
    <style type="text/css">
        .table-striped tr td {
        }
    </style>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" style="height: auto; border: 0px;">
                <table class="queryTable">
                    <tr>
                        <td>
                            <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
                        </td>
                        <td>时间：<asp:DropDownList Width="85" ID="ddltimerType" runat="server">
                            <asp:ListItem Value="-1">请选择</asp:ListItem>
                            <asp:ListItem Value="PartyDate">婚期</asp:ListItem>
                            <asp:ListItem Value="QuotedCreateDate">订单时间</asp:ListItem>
                            <asp:ListItem Value="QuotedDateSucessDate">签约时间</asp:ListItem>
                            <asp:ListItem Value="OrderNextFollowDate">提案时间</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                        <td>
                            <HA:DateRanger runat="server" ID="DateRanger" />
                        </td>
                        <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                        <td>
                            <uc1:MyManager ID="MyManager1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>联系电话
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                            &nbsp;
                        </td>
                        <td>排序
                        <asp:DropDownList ID="ddlSort" runat="server">
                            <asp:ListItem Text="婚期" Value="PartyDate" />
                            <asp:ListItem Text="订单时间" Value="QuotedCreateDate" Selected="True" />
                            <asp:ListItem Text="签约时间" Value="QuotedDateSucessDate" />
                            <asp:ListItem Text="提案时间" Value="OrderNextFollowDate" />
                        </asp:DropDownList>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="BinderData" runat="server" Text="查询" />
                            <cc2:btnReload ID="btnReload" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th width="85px">酒店</th>
                        <th width="75px">婚礼策划</th>
                        <th width="75px">婚礼顾问</th>
                        <th>状态</th>
                        <th>已付款</th>
                        <th>订单总金额</th>
                        <th>订单时间</th>
                        <th width="70px">提案时间</th>
                        <th>签约时间</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemCommand="repCustomer_ItemCommand">
                        <ItemTemplate>
                            <tr <%#IsChange(Eval("OrderNextFollowDate"),Eval("IsChecks")) %> skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetEmployeeName(Eval("OrderEmployee")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetFinishMoney(Eval("OrderID")) %></td>
                                <td><%#Eval("FinishAmount") %></td>
                                <td>
                                    <asp:Label runat="server" ID="lblNextFollowDate" Text='<%#ShowShortDate(Eval("QuotedCreateDate")) %>' ToolTip='<%#Eval("Remark") %>' /></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtOrderFollowDate" Text='<%#Eval("OrderNextFollowDate","{0:yyyy-MM-dd}") %>' onclick="WdatePicker();" Width="70px" /></td>
                                <td><%#ShowShortDate(Eval("QuotedDateSucessDate")) %></td>
                                <td><a target="_blank" class="btn btn-primary btn-mini <%#GetRemoveClassByOrderID(Eval("OrderID")) %>  btnSaveSubmit<%#Eval("EmployeeID")%> btnSumbmit" href="QuotedPriceListCreateEdit.aspx?SaleEmployee=<%#Request["SaleEmployee"] %>&OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&PartyDate=<%#Eval("PartyDate") %>">制作\查看报价单</a>
                                    <a class="btn btn-success btn-mini   <%#GetRemoveClassByOrderID(Eval("OrderID")) %> btnSaveSubmit<%#Eval("EmployeeID")%> btnSumbmit" href="QuotedCollectionsPlanCreate.aspx?SaleEmployee=<%#Request["SaleEmployee"] %>&OrderID=<%#Eval("OrderID") %>&IsFinish=<%#Eval("IsDispatching") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&NeedPopu=1">收款</a>
                                    <asp:HiddenField ID="hideEmpLoyeeID" Value="1" runat="server" />
                                    <asp:HiddenField ID="hideCustomerHide" Value='<%#Eval("CustomerID") %>' runat="server" />
                                    <a class="btn btn-primary btn-mini" href="QuotedPriceShow.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>" target="_blank">查看</a>
                                    <a href="/AdminPanlWorkArea/Sys/Customer/CustomerInfoModify.aspx?CustomerID=<%#Eval("CustomerID") %>&Type=QuotedPrice" class="btn btn-primary btn-mini">修改</a>
                                    <asp:LinkButton runat="server" ID="btnSave" Text="保存" CommandName="Save" CommandArgument='<%#Eval("OrderID") %>' CssClass="btn btn-primary btn-mini" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9">
                            <asp:Label runat="server" ID="lblTotalSums" Text="" Style="font-size: 14px; font-weight: bolder; color: red;" />
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BinderData"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
