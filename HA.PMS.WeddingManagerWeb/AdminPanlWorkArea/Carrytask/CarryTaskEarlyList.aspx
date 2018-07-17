<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CarryTaskEarlyList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryTaskEarlyList" %>


<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //点击文本框 弹出部门人员列表
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=1&ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 480, 380, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-box" style="height: 30px; border: 0px;">
                <table>
                    <tr>
                        <td>
                            <HA:MyManager runat="server" ID="MyManager" Title="策划师" />
                        </td>
                        <td>
                            <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
                        </td>
                        <td>
                            <HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" />
                        </td>
                        <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                        <td>联系电话：
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="BinderData" runat="server" Text="查询" />
                            <cc2:btnReload ID="btnReload" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped" style="border: 1px solid gray; width: 98%;">
                <thead>
                    <tr id="trContent">
                        <th style="white-space: nowrap;">
                            <input id="chkall" type="checkbox" onclick="Checkall(this);" /></th>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>分派时间</th>
                        <th>计划完成时间</th>
                        <th>完成时间</th>
                        <th>前期设计</th>
                        <th>制作/查看婚礼统筹</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemCommand="repCustomer_ItemCommand">
                        <ItemTemplate>
                            <tr skey='<%#Eval("CustomerID") %>' <%#Eval("DesignerState").ToString().ToInt32() == 1 ? "style='color:red'" : "" %>>
                                <td style="text-align: center">
                                    <input id="chkSinger" class="CustomerCheckBox chkbox" type="checkbox" value="<%#Eval("CustomerID") %>" /></td>
                                <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#GetShortDateString(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeName(Eval("OrderID")) %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#Eval("WorkCreateDates") == null ? "无" : Eval("WorkCreateDates","{0:yyyy-MM-dd}") %></td>
                                <td>
                                    <%#string.Format("{0:yyyy-MM-dd}",Eval("PlanFinishDate")) %>
                                </td>
                                <td><%#string.Format("{0:yyyy-MM-dd}",Eval("AutualFinishDate")) %></td>
                                <td id="Partd<%#Container.ItemIndex %>">
                                    <input runat="server" style="margin: 0; width: 90px;" id="txtEmployee" class="txtEmpLoyeeName" type="text" value='<%#GetEmployeeName(Eval("EarlyEmployee")) %>' onclick="ShowPopu(this);" />
                                    <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='' runat="server" />
                                    <asp:HiddenField ID="hideCustomerHide" Value='<%#Eval("CustomerID") %>' runat="server" />
                                    <asp:HiddenField ID="HideOrderID" Value='<%#Eval("OrderID") %>' runat="server" />
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" ID="lbtnSave" Text="保存" CommandArgument='<%#Eval("QuotedID") %>' CommandName="Save" CssClass="btn btn-primary btn-mini" />
                                    <a href='EarylDesingerModify.aspx?QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&Type=Edit' class="btn btn-primary btn-mini" <%#ShowOrHide(Eval("CustomerID"),1) %>>前期设计</a>
                                    <a href='EarylDesingerModify.aspx?QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&Type=Look' class="btn btn-primary btn-mini" <%#ShowOrHide(Eval("CustomerID"),2) %>>查看</a>
                                    <a target="_blank" href="/AdminPanlWorkArea/QuotedPrice/QuotedPriceShow.aspx?QuotedID=<%#Eval("QuotedID") %>&OrderID=<%#Eval("OrderID") %>&CustomerID=<%#Eval("CustomerID") %>" class="btn btn-primary  btn-mini <%#GetRemoveClassByOrderID(Eval("OrderID")) %>">报价单</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="8">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BinderData"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
