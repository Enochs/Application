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

            //判读支付金额是否正确
            $("#btnSavePre").click(function () {
                if ($("#txtPrePayMent").val() == "") {
                    alert("支付金额不能为空");
                    return false;
                } else if ($("#txtPrePayMent").val() == "0") {
                    alert("支付金额不能为0");
                    return false;
                } else if ($("#txtPrePayMent").val() <= 0) {
                    alert("请输入正确的支付金额");
                    return false;
                }
            });

            $(function () {
                /*JQuery 限制文本框只能输入数字和小数点*/
                $("#txtPayFor").keyup(function () {
                    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
                }).bind("paste", function () {  //CTR+V事件处理    
                    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
                }).css("ime-mode", "disabled"); //CSS设置输入法不可用   














            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                            <th style="width: 8%">开户银行</th>
                            <th style="width: 8%">银行帐号</th>
                            <th style="width: 5%">结算金额</th>
                            <th style="width: 5%">已付款</th>
                            <th style="width: 10%">未付款</th>
                            <th style="width: 6%">支付</th>
                            <th style="width: 10%">说明</th>
                            <th style="width: 10%">操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptStatement" OnItemDataBound="rptStatement_ItemDataBound">
                            <ItemTemplate>
                                <tr id="<%#Container.ItemIndex %>">
                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("TypeName") %></td>
                                    <td><%#Eval("PartyDate","{0:yyyy-MM-dd}") %></td>
                                    <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#GetCustomerName(Eval("CustomerID")) %></a></td>
                                    <td><%#GetBankInfo(Eval("RowType"),Eval("SupplierID"),1) %></td>
                                    <td><%#GetBankInfo(Eval("RowType"),Eval("SupplierID"),2) %></td>
                                    <td><%#Eval("SumTotal") %></td>
                                    <td><%#Eval("PayMent").ToString().ToDecimal()+ GetPrePayMnet(Eval("PrePayMent")) %></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblShowNoPay" CssClass="SShowOrHide" Text="未付款:" Style="float: left;" />
                                        <asp:Label runat="server" ID="lblShowNoPayText" ClientIDMode="Static" CssClass="HShowOrHide" Text='<%#Eval("NoPayMent") %>' Style="width: 65%;" />
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPayFors" Text="付款完成" Visible="false" />
                                        <asp:TextBox runat="server" ID="txtPayFor" CssClass="CShowOrHide" Text="0" Style="float: left; width: 60px; height: 15px;" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtContent" /></td>
                                    <td>
                                        <asp:HiddenField runat="server" ID="HideStateID" Value='<%#Eval("StatementID") %>' />
                                        <a target="_blank" class="btn btn-primary btn-mini" href='../Carrytask/CarryCost/OrderCost.aspx?DispatchingID=<%#Eval("DispatchingID") %>&CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>&Type=Details&NeedPopu=1'>执行明细</a>
                                        <a href='PaymentRecord.aspx?CustomerID=<%#Eval("CustomerID") %>' target="_blank" class="btn btn-primary btn-mini">查看</a>
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

                            <td style="text-align: right;">本页合计:</td>
                            <td>
                                <asp:Label runat="server" ID="lblPageSumTotal" />元</td>
                            <td>
                                <asp:Label runat="server" ID="lblPagePayMent" />元</td>
                            <td>
                                <asp:Label runat="server" ID="lblPageNoPayment" />元</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox runat="server" ID="txtPrePayMent" ClientIDMode="Static" Style="float: left; width: 60px; height: 15px;" Enabled="false" />

                                        <asp:LinkButton runat="server" ID="btnSave" Text="保存" OnClick="btnSavePre_Click" CssClass="btn btn-primary btn-mini" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:LinkButton runat="server" ID="btnSavePre" ClientIDMode="Static" Text="支付" OnClick="btnSavePre_Click" CssClass="btn btn-primary btn-mini" />

                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td style="text-align: right;">本期合计:</td>
                            <td>
                                <asp:Label runat="server" ID="lblAllSumTotal" />元</td>
                            <td>
                                <asp:Label runat="server" ID="lblAllPayMent" />元</td>
                            <td>
                                <asp:Label runat="server" ID="lblAllNoPayMent" />元</td>
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnPayAll" Text="支付所有" OnClick="lbtnPayAll_Click" CssClass="btn btn-primary btn-mini" Visible="true" OnClientClick="return confirm('您确定支付所有付款吗?');" /></td>
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
