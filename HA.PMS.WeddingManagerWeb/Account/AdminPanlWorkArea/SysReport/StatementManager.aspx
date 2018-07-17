<%@ Page Title="" Language="C#" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="StatementManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysReport.StatementManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
    <%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>


    <style type="text/css">
        /*.tableStateMent {
            width: 90%;
            border: 1px solid gray;
        }

            .tableStateMent tr th {
                border: 1px solid gray;
                border-collapse: collapse;
            }
             .tableStateMent tr td {
                border: 1px solid gray;
                border-collapse: collapse;
            }*/

        .btn-key {
            width: auto;
            height: 28px;
            color: white;
            border: 1px solid gray;
            background-color: #136fe0;
        }

            .btn-key:hover {
                background-color: #133cdc;
            }
    </style>


    <script type="text/javascript">
        $(document).ready(function () {


        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMain">
        <div class="ui-menu-divider" style="width: 100%">
            <table class="tablesLook">
                <tr>
                    <td>
                        <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
                    </td>
                    <td>供应商:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" /></td>
                    <td>类别:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlCategory" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                            <asp:ListItem Text="请选择" Value="0" Selected="True" />
                            <asp:ListItem Text="供应商" Value="1" />
                            <asp:ListItem Text="四大金刚" Value="2" />
                        </asp:DropDownList></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlTypeCategory" Style="width: 120px" /></td>
                    <td>
                        <HA:DateRanger runat="server" ID="DatePartyDate" Title="婚期:" />
                    </td>
                </tr>
                <tr>
                    <td>完成状态:
                        <asp:DropDownList runat="server" ID="ddLPayState">
                            <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="未完成" Value="1" />
                            <asp:ListItem Text="已完成" Value="2" />
                        </asp:DropDownList>
                    </td>

                    <td>
                        <asp:Button runat="server" ID="btnLook" CssClass="bnt btn-key" Text="查找" OnClick="btnLook_Click" />
                        <cc2:btnReload ID="btnReload1" runat="server" Visible="false" />
                        <asp:Button runat="server" ID="btnReload" OnClick="btnReload_Click" Text="重置条件" CssClass="btn btn-primary" />
                    </td>
                </tr>
            </table>
            <div style="width: 95%; overflow-x: auto;">
                <table class="table table-bordered tableStateMent">
                    <thead>
                        <tr>
                            <th style="width: 10%">供应商</th>
                            <th style="width: 8%">类别</th>
                            <th style="width: 8%">婚期</th>
                            <th style="width: 10%">新人</th>
                            <th style="width: 8%">结算金额</th>
                            <th style="width: 8%">完成情况</th>
                            <th style="width: 6%">已付款</th>
                            <th style="width: 12%">付款</th>
                            <th style="width: 10%">未付款</th>
                            <th style="width: 10%">备注</th>
                            <th style="width: 9%">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptStatement" OnItemCommand="rptStatement_ItemCommand" OnItemDataBound="rptStatement_ItemDataBound">
                            <ItemTemplate>
                                <tr id="<%#Container.ItemIndex %>">
                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("TypeName") %></td>
                                    <td><%#Eval("PartyDate","{0:yyyy-MM-dd}") %></td>
                                    <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#GetCustomerName(Eval("CustomerID")) %></a></td>
                                    <td><%#Eval("SumTotal") %></td>
                                    <td><%#Eval("Finishtation") %></td>
                                    <td><%#Eval("PayMent") %></td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="lbtnPaysFor" ClientIDMode="Static" CommandName="lbtnPaysFor" Text="付款" />
                                        <asp:TextBox runat="server" ID="txtPayFor" ClientIDMode="Static" CssClass="CShowOrHide" Text="0" Style="float: left; width: 60px; height: 15px;" Visible="false" />
                                        <asp:LinkButton runat="server" ID="lbtnCancel" ClientIDMode="Static" CommandName="lbtnCancel" Text="取消" Visible="false" Style="color: red; text-decoration: underline;" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblShowNoPay" CssClass="SShowOrHide" Text="未付款:" Style="float: left;" />
                                        <asp:Label runat="server" ID="lblShowNoPayText" ClientIDMode="Static" CssClass="HShowOrHide" Text='<%#Eval("NoPayMent") %>' Style="width: 65%;" />
                                    </td>
                                    <td><%#Eval("Remark") %></td>
                                    <td>
                                        <asp:HiddenField runat="server" ID="HideStateID" Value='<%#Eval("StatementID") %>' />
                                        <a target="_blank" class="btn btn-primary btn-mini" href='../Carrytask/CarryCost/OrderCost.aspx?DispatchingID=<%#Eval("DispatchingID") %>&CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>&Type=Details&NeedPopu=1'>执行明细</a>
                                        <asp:LinkButton runat="server" ID="btnRowSave" ClientIDMode="Static" CommandName="RowSave" CommandArgument='<%#Eval("StatementID") %>' Text="保存" CssClass="btn btn-primary btn-mini" Visible="false" />
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
                            <td style="text-align: right;">本页合计:</td>
                            <td>
                                <asp:Label runat="server" ID="lblPageSumTotal" />元</td>
                            <td></td>
                            <td>
                                <asp:Label runat="server" ID="lblPagePayMent" />元</td>
                            <td>
                                <asp:TextBox runat="server" ID="txtPrePayMent" Style="float: left; width: 60px; height: 15px;" Visible="false" />
                                <asp:LinkButton runat="server" ID="btnSavePre" Text="保存" OnClick="btnSavePre_Click" CssClass="btn btn-primary btn-mini" Visible="false" />
                                <asp:LinkButton runat="server" ID="lbtnPayAll" Text="所有付款" OnClick="lbtnPayAll_Click" CssClass="btn btn-primary btn-mini" Visible="true" OnClientClick="return confirm('您确定支付所有付款吗?');" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblPageNoPayment" />元</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td style="text-align: right;">本期合计:</td>
                            <td>
                                <asp:Label runat="server" ID="lblAllSumTotal" />元</td>
                            <td></td>
                            <td>
                                <asp:Label runat="server" ID="lblAllPayMent" />元</td>
                            <td></td>
                            <td>
                                <asp:Label runat="server" ID="lblAllNoPayMent" />元</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="11">
                                <cc1:AspNetPagerTool runat="server" ID="CtrPageIndex" PageSize="10" OnPageChanged="CtrPageIndex_PageChanged" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>

        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>
