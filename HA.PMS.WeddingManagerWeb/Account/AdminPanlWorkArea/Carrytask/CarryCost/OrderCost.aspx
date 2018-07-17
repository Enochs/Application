<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCost.aspx.cs" StylesheetTheme="Default" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost.OrderCost" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <style type="text/css">
        .tablestyle {
            border-width: 1px;
            border-style: solid;
            border-color: Black;
            border-collapse: collapse;
        }

        #table1 td {
            vertical-align: top;
            border-width: 0px;
        }

        #tlefttop {
            background-color: #ccc;
        }

        #trighttop {
            background-color: #ccc;
        }

        #tlefttop td {
            border-width: 1px;
            border-color: Black;
            border-style: solid;
            width: 100px;
            text-align: center;
            height: 20px;
        }

        #tleftbottom td {
            border-width: 1px;
            border-color: Black;
            border-style: solid;
            width: 100px;
            text-align: center;
            height: 20px;
        }

        #trighttop td {
            border-width: 1px;
            border-color: Black;
            border-style: solid;
            width: 100px;
            text-align: center;
            height: 20px;
        }

        #trightbottom td {
            border-width: 1px;
            border-color: Black;
            border-style: solid;
            width: 100px;
            text-align: center;
            height: 20px;
        }

        .tablestyle1 {
            width: 100%;
            height: 100%;
            border-width: 1px;
            border-style: solid;
            border-color: White;
            border-collapse: collapse;
            border-bottom-color: Black;
            border-right-color: Black;
        }
    </style>
    <table style="width: 100%;" border="0">
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">执行团队：
                            </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <th>姓名</th>
                            <th width="210" height="auto">说明</th>
                            <th width="80" runat="server" id="td_Output1">计划支出</th>
                            <th width="80">评价</th>
                            <th width="150">优点</th>
                            <th width="150">缺点</th>
                        </tr>
                        <asp:Repeater ID="repEmployeeCost" runat="server">
                            <ItemTemplate>
                                <tr>

                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("Content") %></td>
                                    <td <%=StatuHideViewInviteInfo() %>><%#Eval("Sumtotal") %>元</td>
                                    <td>
                                        <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                    </td>
                                    <td>
                                        <%#Eval("Advance") %></td>
                                    <td>
                                        <%#Eval("ShortCome") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>


                    </thead>

                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>

                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">供应商成本明细</td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <th>供应商</th>
                            <th width="210" height="auto">说明</th>
                            <th width="80" runat="server" id="td_Output2">计划支出</th>
                            <th width="80">评价</th>
                            <th width="150">优点</th>
                            <th width="150">缺点</th>
                        </tr>

                        <asp:Repeater ID="repSupplierCost" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("Content") %></td>
                                    <td <%=StatuHideViewInviteInfo() %>><%#Eval("Sumtotal") %>元</td>
                                    <td>
                                        <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                    </td>
                                    <td>
                                        <%#Eval("Advance") %></td>
                                    <td>
                                        <%#Eval("ShortCome") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                    </thead>
                </table>


            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">库房： </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <th>名称</th>
                            <th width="210" height="auto">说明</th>
                            <th width="80" runat="server" id="td_Output3">计划支出</th>
                            <th width="80">评价</th>
                            <th width="150">优点</th>
                            <th width="150">缺点</th>
                        </tr>
                        <asp:Repeater ID="rptStore" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("Content") %></td>
                                    <td <%=StatuHideViewInviteInfo() %>><%#Eval("Sumtotal") %>元</td>
                                    <td>
                                        <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                    </td>
                                    <td>
                                        <%#Eval("Advance") %></td>
                                    <td>
                                        <%#Eval("ShortCome") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">采购物料： </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <th>物料</th>
                            <th width="210" height="auto">说明</th>
                            <th width="80" runat="server" id="td_Output4">计划支出</th>
                            <th width="80">评价</th>
                            <th width="150">优点</th>
                            <th width="150">缺点</th>
                        </tr>
                        <asp:Repeater ID="repBuyCost" runat="server">
                            <ItemTemplate>
                                <tr>

                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("Content") %></td>
                                    <td <%=StatuHideViewInviteInfo() %>><%#Eval("Sumtotal") %>元</td>
                                    <td><%#GetNameByEvaulationId(Eval("Evaluation")) %></td>
                                    <td>
                                        <%#Eval("Advance") %></td>
                                    <td>
                                        <%#Eval("ShortCome") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">花艺成本： </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <th>鲜花名称</th>
                            <th width="210" height="auto">说明</th>
                            <th width="80" runat="server" id="td_Output5">计划支出</th>
                            <th width="80">评价</th>
                            <th width="150">优点</th>
                            <th width="150">缺点</th>
                        </tr>
                        <asp:Repeater ID="repFlowerCost" runat="server">
                            <ItemTemplate>
                                <tr>

                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("Content") %></td>
                                    <td <%=StatuHideViewInviteInfo() %>><%#Eval("Sumtotal") %>元</td>
                                    <td><%#GetNameByEvaulationId(Eval("Evaluation")) %></td>
                                    <td>
                                        <%#Eval("Advance") %></td>
                                    <td>
                                        <%#Eval("ShortCome") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">设计类清单： </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <th>名称</th>
                            <th width="210" height="auto">说明</th>
                            <th width="80" runat="server" id="Th1">计划支出</th>
                            <th width="80">评价</th>
                            <th width="150">优点</th>
                            <th width="150">缺点</th>
                        </tr>
                        <asp:Repeater ID="rptDesignClass" runat="server">
                            <ItemTemplate>
                                <tr>

                                    <td><%#Eval("Title") %></td>
                                    <td><%#Eval("Node") %></td>
                                    <td <%=StatuHideViewInviteInfo() %>><%#Eval("TotalPrice") %>元</td>
                                    <td><%#GetNameByEvaulationId(Eval("Evaluation")) %></td>
                                    <td>
                                        <%#Eval("Advance") %></td>
                                    <td>
                                        <%#Eval("ShortCome") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">

                    <tr>
                        <td style="font-weight: bold; background-color: #808080" width="150">其他费用： </td>
                        <td colspan="4"></td>
                    </tr>

                    <tr>
                        <th>项目</th>
                        <th width="240" height="auto">说明</th>
                        <th width="auto" runat="server" id="td_Output">计划支出</th>
                        <th>评价</th>
                        <th runat="server" id="td_Handle">操作</th>
                    </tr>

                    <tr runat="server" id="t_head">

                        <td>
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtNode" runat="server" TextMode="MultiLine" Width="260px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPlanCost" runat="server"></asp:TextBox>
                        </td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="保存成本" OnClick="btnSaveEdit_Click" />
                        </td>

                    </tr>

                    <asp:Repeater ID="repOtherCost" runat="server" OnItemCommand="repOtherCost_ItemCommand" OnItemDataBound="repEmployeeCost_ItemDataBound">
                        <ItemTemplate>
                            <tr>

                                <td><%#Eval("Name") %></td>
                                <td><%#Eval("Content") %></td>
                                <td <%=StatuHideViewInviteInfo() %>><%#Eval("Sumtotal") %>元</td>
                                <td><%#GetNameByEvaulationId(Eval("Evaluation")) %></td>
                                <td>
                                    <asp:Button ID="btnSaveEdit" CommandName="Delete" CssClass="btn btn-primary btn-mini" runat="server" Text="删除" CommandArgument='<%#Eval("CostSumId") %>' OnClientClick="return confirm('您确定要删除吗?')" /></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                    <tbody runat="server" id="td_body">
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <b style="color: brown; font-size: 15px;">合计:<asp:Label ID="lblSumMoney" runat="server" Text=""></asp:Label>元</b></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">总评： </td>
                            <td colspan="3"></td>
                        </tr>
                        <tr>
                            <th>总评项目</th>
                            <th width="210" height="auto">说明</th>
                            <th width="120">评价</th>
                            <th>备注</th>
                        </tr>
                        <asp:Repeater ID="rptSatisfaction" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("EvaluationName") %></td>
                                    <td>
                                        <%#Eval("EvaluationContent") %></td>
                                    <td>
                                        <%#GetNameByEvaulationId(Eval("EvaluationId")) %></td>
                                    <td>
                                        <%#Eval("EvaluationRemark") %></td>
                                </tr>
                                <%--<tr>
                                    <td><%#Eval("SatisfactionName") %></td>
                                    <td>
                                        <%#Eval("SatisfactionContent") %></td>
                                    <td>
                                        <%#GetNameByEvaulationId(Eval("SaEvaluationId")) %></td>
                                    <td>
                                        <%#Eval("SatisfactionRemark") %></td>
                                </tr>--%>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>
                </table>
            </td>
        </tr>

    </table>
    <div class="divSub">
        <asp:Button runat="server" ID="btn_OrderFinish" Text="项目完结" CssClass="btn btn-primary" OnClick="btn_OrderFinish_Click" />
    </div>

</asp:Content>
