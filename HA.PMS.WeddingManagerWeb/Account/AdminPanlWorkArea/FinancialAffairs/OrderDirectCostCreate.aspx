<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Default" CodeBehind="OrderDirectCostCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.OrderDirectCostCreate" Title="填写成本明细" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>


<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">

    <script type="text/javascript">
        function CheckPage() {
            return ValidateForm('input[check],textarea[check]');
        }

        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtServiceContent.ClientID%>:<%=txtInsideRemark.ClientID%>');
            BindMoney('<%=txtPlannedExpenditure.ClientID%>:<%=txtActualExpenditure.ClientID%>');
        }

        function HideCtable(ControlID) {
            $(".WorkTable").show();
            $(ControlID).hidden();

        }

        $(document).ready(function () {

            $(".NeedCost").change(function () {
                GetTotal();
            });
        });


        function GetTotal() {
            var TotalSum = 0;
            $(".NeedCost").each(function () {
                if ($(this).val() != "") {
                    TotalSum += parseFloat($(this).val());
                }
            });
            $("#txtCost").attr("value", TotalSum);
            var GetOnlyData = (TotalSum / parseFloat($("#hideTotal").val())).toFixed(2);
            $("#txtProfitMargin").attr("value", ((1 - GetOnlyData) * 100).toFixed(2) + "%");
        }

        function GoSubmit() {
            if (confirm("请注意！确认成本后将无法进行修改!")) {
                return true;
            } else {
                return false;
            }
        }


    </script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div style="overflow-y: auto; height: 1000px;">
        <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
        <br />

        <table class="WorkTable table table-bordered table-striped" style="width: 85%;">


            <tbody>
                <tr>
                    <td>
                        <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                            <thead>
                                <tr>
                                    <td style="font-weight: bold; background-color: #808080" width="150">执行团队：
                                    </td>
                                    <td colspan="6"></td>
                                </tr>
                                <tr>
                                    <th>姓名</th>
                                    <th width="250" height="auto">说明</th>
                                    <th width="80">计划支出</th>
                                    <th width="80">实际支出</th>
                                    <th width="150">优点</th>
                                    <th width="150">缺点</th>
                                    <th width="150">备注</th>
                                </tr>
                                <asp:Repeater ID="repEmployeeCost" runat="server">
                                    <ItemTemplate>
                                        <tr>

                                            <td><%#Eval("Name") %>
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" Width="200px" ID="txtContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtPlanSumtotal" Text='<%#Eval("Sumtotal") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtActualSumtotal" Text='<%#Eval("ActualSumTotal") == null ? Eval("Sumtotal") : Eval("ActualSumTotal") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtAdvance" Text='<%#Eval("Advance") %>' Enabled="false"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtShortCome" Text='<%#Eval("ShortCome") %>' Enabled="false"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemark" Text='<%#Eval("Remark") %>'></asp:TextBox></</td>
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
                                    <td colspan="6"></td>
                                </tr>
                                <tr>
                                    <th>供应商</th>
                                    <th width="250" height="auto">说明</th>
                                    <th width="80">计划支出</th>
                                    <th width="80">实际支出</th>
                                    <th width="150">优点</th>
                                    <th width="150">缺点</th>
                                    <th width="150">备注</th>
                                </tr>

                                <asp:Repeater ID="repSupplierCost" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("Name") %>
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" Width="200px" ID="txtContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtPlanSumtotal" Text='<%#Eval("Sumtotal") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtActualSumtotal" Text='<%#Eval("ActualSumTotal") == null ? Eval("Sumtotal") : Eval("ActualSumTotal") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtAdvance" Text='<%#Eval("Advance") %>' Enabled="false"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtShortCome" Text='<%#Eval("ShortCome") %>' Enabled="false"></asp:TextBox></</td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemark" Text='<%#Eval("Remark") %>'></asp:TextBox></</td>
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
                                    <td colspan="6"></td>
                                </tr>
                                <tr>
                                    <th>名称</th>
                                    <th width="250" height="auto">说明</th>
                                    <th width="80">计划支出</th>
                                    <th width="80">实际支出</th>
                                    <th width="150">优点</th>
                                    <th width="150">缺点</th>
                                    <th width="150">备注</th>
                                </tr>
                                <asp:Repeater ID="rptStore" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("Name") %>
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" Width="200px" ID="txtContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtPlanSumtotal" Text='<%#Eval("Sumtotal") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtActualSumtotal" Text='<%#Eval("ActualSumTotal") == null ? Eval("Sumtotal") : Eval("ActualSumTotal") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtAdvance" Text='<%#Eval("Advance") %>' Enabled="false"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtShortCome" Text='<%#Eval("ShortCome") %>' Enabled="false"></asp:TextBox></</td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemark" Text='<%#Eval("Remark") %>'></asp:TextBox></</td>
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
                                    <td colspan="6"></td>
                                </tr>
                                <tr>
                                    <th>物料</th>
                                    <th width="250" height="auto">说明</th>
                                    <th width="80" runat="server" id="td_Output4">计划支出</th>
                                    <th width="80">实际支出</th>
                                    <th width="150">优点</th>
                                    <th width="150">缺点</th>
                                    <th width="150">备注</th>
                                </tr>
                                <asp:Repeater ID="repBuyCost" runat="server">
                                    <ItemTemplate>
                                        <tr>

                                            <td><%#Eval("Name") %>
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" Width="200px" ID="txtContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtPlanSumtotal" Text='<%#Eval("Sumtotal") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtActualSumtotal" Text='<%#Eval("ActualSumTotal") == null ? Eval("Sumtotal") : Eval("ActualSumTotal") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtAdvance" Text='<%#Eval("Advance") %>' Enabled="false"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtShortCome" Text='<%#Eval("ShortCome") %>' Enabled="false"></asp:TextBox></</td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemark" Text='<%#Eval("Remark") %>'></asp:TextBox></</td>
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
                                    <td colspan="6"></td>
                                </tr>
                                <tr>
                                    <th>鲜花名称</th>
                                    <th width="250" height="auto">说明</th>
                                    <th width="80">计划支出</th>
                                    <th width="80">实际支出</th>
                                    <th width="150">优点</th>
                                    <th width="150">缺点</th>
                                    <th width="150">备注</th>
                                </tr>
                                <asp:Repeater ID="repFlowerCost" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#Eval("Name") %>
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" Width="200px" ID="txtContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtPlanSumtotal" Text='<%#Eval("Sumtotal") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtActualSumtotal" Text='<%#Eval("ActualSumTotal") == null ? Eval("Sumtotal") : Eval("ActualSumTotal") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtAdvance" Text='<%#Eval("Advance") %>' Enabled="false"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtShortCome" Text='<%#Eval("ShortCome") %>' Enabled="false"></asp:TextBox></</td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemark" Text='<%#Eval("Remark") %>'></asp:TextBox></</td>
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
                                    <th width="250" height="auto">说明</th>
                                    <th width="80">计划支出</th>
                                    <th width="80">实际支出</th>
                                    <th width="150">优点</th>
                                    <th width="150">缺点</th>
                                    <th width="150">备注</th>
                                </tr>
                                <asp:Repeater ID="rptDesignClass" runat="server">
                                    <ItemTemplate>
                                        <tr>

                                            <td><%#Eval("Title") %>
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("DesignclassID") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" Width="200px" ID="txtNode" TextMode="MultiLine" Text='<%#Eval("Node") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtTotalPrice" Text='<%#Eval("TotalPrice") %>'></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtActualSumtotal" Text='<%#Eval("ActualSumTotal") == null ? Eval("TotalPrice") : Eval("ActualSumTotal") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtAdvance" Text='<%#Eval("Advance") %>' Enabled="false"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtShortCome" Text='<%#Eval("ShortCome") %>' Enabled="false"></asp:TextBox></</td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemark" Text='<%#Eval("Remark") %>'></asp:TextBox></</td>
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
            </tbody>
        </table>

        <table class="table table-bordered table-striped" style="width: 100%;">
            <thead>
                <tr>
                    <td style="font-weight: bold; background-color: #808080" width="100">其他：
                    </td>
                    <td colspan="6"><a href="/AdminPanlWorkArea/Carrytask/ProductListforWareHouse.aspx?DispatchingID=<%=Request["DispatchingID"] %>&CustomerID=<%=Request["CustomerID"] %>&NeedPopu=1" target="_blank" class="btn btn-success">查看本订单库房使用产品</a></td>
                </tr>
                <tr>
                    <th>类别</th>
                    <th>说明</th>
                    <th>计划支出</th>
                    <th>实际支出</th>
                    <th width="150">优点</th>
                    <th width="150">缺点</th>
                    <th>操作</th>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtServiceContent" check="1" MaxLength="20" runat="server" Width="100" CssClass="NoEmpty"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtInsideRemark" check="1" tip="限20个字符！" TextMode="MultiLine" MaxLength="20" runat="server" Width="200px" CssClass="NoEmpty"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPlannedExpenditure" MaxLength="10" runat="server" Width="100" CssClass="NoEmpty"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtActualExpenditure" MaxLength="10" runat="server" Width="100" CssClass="NoEmpty"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAdvances" MaxLength="10" runat="server" Width="100" CssClass="NoEmpty"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtShortComes" MaxLength="10" runat="server" Width="100" CssClass="NoEmpty"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="新增成本" OnClick="btnSave_Click" CssClass="btn btn-info" /></td>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repOther" runat="server" OnItemCommand="RepItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("Name") %>
                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                            </td>
                            <td>
                                <asp:TextBox runat="server" Width="200px" ID="txtContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtPlanSumtotal" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtActualSumTotal" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtAdvance" Text='<%#Eval("Advance") %>' Enabled="false"></asp:TextBox></td>
                            <td>
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtShortCome" Text='<%#Eval("ShortCome") %>' Enabled="false"></asp:TextBox></</td>
                            <td>
                                <asp:LinkButton ID="lnkbtnSave" runat="server" CssClass="btn btn-primary btn-mini" CommandName="SaveItem" CommandArgument='<%#Eval("CostSumId") %>'>保存</asp:LinkButton>
                                <asp:LinkButton ID="btnSaveEdit" CommandName="Delete" CssClass="btn btn-primary btn-mini" runat="server" Text="删除" CommandArgument='<%#Eval("CostSumId") %>' OnClientClick="return confirm('您确定要删除吗?')" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </tbody>
        </table>

        <table class="table table-bordered table-striped" style="width: 100%; margin-top: 0px;">
            <thead>
                <tr>
                    <td style="font-weight: bold; background-color: #808080">销售费用： </td>
                    <td colspan="6">
                        <asp:Button ID="btnSaveSale" runat="server" Text="录入销售成本" OnClick="btnSaveSale_Click" />
                        <asp:Button ID="btnSaveallsale" runat="server" Text="保存全部销售成本" OnClick="btnSaveallsale_Click" /></td>
                </tr>
                <tr>
                    <th>项目</th>
                    <th width="250" height="auto">说明</th>
                    <th width="80">计划支出</th>
                    <th width="80">实际支出</th>
                    <th width="150">优点</th>
                    <th width="150">缺点</th>
                    <th></th>
                </tr>
                <asp:Repeater ID="repSaleMoney" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("Name") %>
                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                            </td>
                            <td>
                                <asp:TextBox runat="server" Width="200px" ID="txtContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtPlanSumtotal" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtActualSumTotal" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtAdvance" Text='<%#Eval("Advance") %>' Enabled="false"></asp:TextBox></td>
                            <td>
                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtShortCome" Text='<%#Eval("ShortCome") %>' Enabled="false"></asp:TextBox></</td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </thead>
        </table>

        <table class="table table-bordered table-striped" style="width: 85%;">
            <tr>
                <td width="100">订单总金额</td>
                <td>
                    <asp:TextBox ID="txtTotal" runat="server" Enabled="false"></asp:TextBox></td>
                <td>&nbsp;</td>
                <td width="100">合计</td>
                <td>
                    <asp:TextBox ID="txtCost" ClientIDMode="Static" runat="server" Enabled="false"></asp:TextBox></td>
                <td>毛利</td>
                <td>
                    <asp:TextBox ID="txtMaoLi" ClientIDMode="Static" runat="server" Enabled="false"></asp:TextBox></td>
                <td>毛利率</td>
                <td>
                    <asp:TextBox ID="txtProfitMargin" ClientIDMode="Static" runat="server" Enabled="false"></asp:TextBox></td>
            </tr>

            <tr>
                <td colspan="9" style="text-align: center;">
                    <asp:Button ID="btnSaveChange" runat="server" Text="保存全部" OnClick="btnSaveChange_Click" CssClass="btn btn-success" />
                    <asp:HiddenField ID="hideTotal" runat="server" ClientIDMode="Static" />

                    <asp:Button ID="btnFinish" runat="server" Text="确认成本明细" OnClick="btnFinish_Click" OnClientClick="return GoSubmit();" CssClass="btn btn-danger" />

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
