<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceManagerMission.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceSplit.QuotedPriceManagerMission" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Src="../../Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc1" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div style="overflow-x: auto;">
        <div class="widget-box">

            <uc1:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />
            <asp:Button ID="btnReturn" CssClass="btn" runat="server" Text="返回" OnClick="btnReturn_Click" />
            <table class="table table-bordered table-striped table-select">


                <tr>
                    <th>类别</th>
                    <th>项目</th>
                    <th>任务内容</th>
                    <th>任务要求</th>

                    <td>责任人</td>
                    <th>备注</th>
                    <th>计划完成时间</th>
                    <th>任务状态</th>
                    <th>确认完成</th>

                </tr>
                <asp:Repeater ID="repManager" runat="server" OnItemCommand="repWeddingPlanning_ItemCommand" OnDataBinding="repManager_DataBinding" OnItemDataBound="repManager_ItemDataBound">
                    <ItemTemplate>

                        <tr>
                            <td>
                                <%#Eval("Category") %></td>
                            <td>
                                <%#Eval("CategoryItem") %></td>
                            <td>
                                <%#Eval("ServiceContent") %></td>
                            <td>
                                <%#Eval("Requirement") %></td>

                            <td id="Partd<%#Container.ItemIndex %>">
                                <%#GetEmployeeName(Eval("EmpLoyeeID")) %>
                            </td>
                            <td>
                                <%#Eval("Remark") %></td>
                            <td>
                                <%#GetShortDateString(Eval("PlanFinishDate")) %>
                            </td>
                            <td>
                                <%#Eval("State") %>
                            </td>
                            <td>
                                <asp:LinkButton ID="lnkbtnMission" runat="server" CommandArgument='<%#Eval("PlanningID") %>' CssClass="btn  btn-success" CommandName="Finish">确认完成</asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnReturn" runat="server" CommandArgument='<%#Eval("PlanningID") %>' CssClass="btn mini" CommandName="Return">重新完成</asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnDelete" Visible="false" runat="server" CommandArgument='<%#Eval("PlanningID") %>' CssClass="btn  btn-danger" CommandName="Delete">删除</asp:LinkButton>

                            </td>
                        </tr>
                    </ItemTemplate>

                </asp:Repeater>

            </table>
        </div>
    </div>
</asp:Content>
