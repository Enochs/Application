<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceFinish.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceFinish" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        //上传图片
        function ShowFileUploadPopu(Control) {
            var Url = Control;
            showPopuWindows(Url, 400, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        $(document).ready(function () {
            $("#trContent th").css({ "white-space": "nowrap" });
        });
    </script>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
            <table>
                <tr>

                    <td>
                        <HA:MyManager runat="server" ID="MyManager" />

                    </td>
                    <td>
                        <cc2:btnManager ID="btnSerch" runat="server" OnClick="btnSerch_Click" />
                    </td>
                </tr>
            </table>


            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr id="trContent">
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>婚礼策划</th>
                        <th>新人状态</th>
                        <th>接收时间</th>
                        <th>定金</th>
                        <th>婚礼预算</th>
                        <th>新人提案时间</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                            <tr skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>'>
                                <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmployee(Eval("CustomerID")) %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#ShowShortDate(Eval("CreateDate")) %></td>
                                <td><%#GetOrderMoney(Eval("OrderID")) %></td>
                                <td><%#Eval("RealAmount") %></td>
                                <td><%#ShowShortDate(Eval("NextFlowDate")) %></td>
                                <td style="white-space: nowrap;">

                                    <a target="_blank" class="btn btn-success btn-mini btnSaveSubmit<%#Eval("EmployeeID")%> btnSumbmit" href="QuotedPriceListCreateEdit.aspx?OrderID=<%#Eval("OrderID") %>&IsFinish=<%#Eval("IsDispatching") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>" <%#HideChecks(Eval("CheckState"))%>>确认/打印</a>
                                    <a class="btn btn-success btn-mini btnSaveSubmit<%#Eval("EmployeeID")%> btnSumbmit" href="#" <%#HideChecks(Eval("CheckState"))%> onclick="ShowFileUploadPopu('QuotedPriceCheckNode.aspx?QuotedID=<%#Eval("QuotedID") %>');">查看审核说明</a>

                                    <asp:HiddenField ID="hideEmpLoyeeID" Value="1" runat="server" />
                                    <asp:HiddenField ID="hideCustomerHide" Value='<%#Eval("CustomerID") %>' runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="12">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged">
                            </cc1:AspNetPagerTool>
                        </td>

                    </tr>

                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />



        </div>
    </div>
</asp:Content>
