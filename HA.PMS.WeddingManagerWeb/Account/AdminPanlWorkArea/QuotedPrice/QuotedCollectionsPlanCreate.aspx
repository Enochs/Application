<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedCollectionsPlanCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedCollectionsPlanCreate" Title="创建计划" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CustomerTitle.ascx" TagPrefix="HA" TagName="CustomerTitle" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <script src="../../App_Themes/Default/js/jquery.min.js"></script>
    <script type="text/javascript">
        function CheckPage() {
            if ($("#txtTimer").val() == "") {
                alert("收款时间不能为空!");
                $("#txtTimer").focus();
                return false;
            }

            if ($("#txtAmount").val() == "") {
                alert("收款金额不能为空!");
                $("#txtAmount").focus();
                return false;


            }
        }
        $(document).ready(function () {
            $("#txtNode").hide();
        });

        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnCreate.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString('<%=txtTimer.ClientID%>');
            BindMoney('<%=txtAmount.ClientID%>');
        }

        function RadioSelect() {
            var values = $("#<%=rdoNodes.ClientID %>").find("input[type='radio']:checked").val();
            if (values == 4) {
                $("#txtNode").show();
            } else {
                $("#txtNode").hide();
            }
        }

    </script>
</asp:Content>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div>

        <div>
            <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />

            <table class="table table-bordered table-striped" style="width: 100%;">
                <thead>
                    <tr>
                        <td colspan="4">
                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btn_Updates" Text="修改" OnClick="btn_Updates_Click" Visible="false" />
                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btn_SaleAmount" Text="合同金额" OnClick="btn_Updates_Click" Visible="false" />
                        </td>
                    </tr>
                    <tr style="background-color: aliceblue;">
                        <td>合同金额:<asp:Label ID="lblSaleAmount" runat="server" Text="Label"></asp:Label>
                            <asp:TextBox ID="txtSaleAmount" runat="server" Text="Label" Visible="false"></asp:TextBox>
                        </td>
                        <td>已付款:<asp:Label ID="lblFinishMoney" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td colspan="2">余款:<asp:Label ID="lblHaveMoney" runat="server" Text="Label"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <th width="20%">收款时间</th>
                        <th width="20%">收款金额</th>
                        <th width="20%">付款方式</th>
                        <th width="25%">收款理由</th>
                        <th runat="server" id="thHandle">操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repDataKist" runat="server" OnItemCommand="repDataKist_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td style="height: 16px;"><%--<%#GetShortDateString(Eval("CollectionTime"))%>--%>
                                    <asp:Label runat="server" ID="lblCollectionTime" Text='<%#GetShortDateString(Eval("CollectionTime")) %>' />
                                    <asp:TextBox runat="server" ID="txtCollectionTime" Text='<%#GetShortDateString(Eval("CollectionTime")) %>' onclick="WdatePicker();" Visible="false" Enabled="false" />
                                    <asp:HiddenField runat="server" ID="HidePlanId" Value='<%#Eval("PlanID") %>' />
                                </td>
                                <td style="height: 16px;">
                                    <asp:Label runat="server" ID="lblRealityAmount" Text='<%#Eval("RealityAmount") %>' />
                                    <asp:TextBox runat="server" ID="txtRealityAmount" Text='<%#Eval("RealityAmount") %>' Visible="false" />
                                </td>
                                <td style="height: 16px;"><%--<%#Eval("MoneyType") %>--%>
                                    <asp:Label runat="server" ID="lblMoneyType" Text='<%#Eval("MoneyType") %>' />
                                    <asp:RadioButtonList ID="rdoMoneytypes" runat="server" RepeatDirection="Horizontal" Width="100%" Visible="false">
                                        <asp:ListItem Text="现金" Value="1">现金</asp:ListItem>
                                        <asp:ListItem Text="刷卡" Value="2">刷卡</asp:ListItem>
                                        <asp:ListItem Text="转账" Value="3">转账</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>

                                <td style="height: 16px;"><%--<%#Eval("Node") %>--%>
                                    <asp:Label runat="server" ID="lblNode" Text='<%#Eval("Node") %>' />
                                    <asp:TextBox runat="server" ID="txtNode" Text='<%#Eval("Node") %>' TextMode="MultiLine" Visible="false" />
                                    <asp:RadioButtonList runat="server" ID="rdoNode" RepeatDirection="Horizontal" Width="100%" Visible="false">
                                        <asp:ListItem Text="定金" Value="1" Selected="True" />
                                        <asp:ListItem Text="中期款" Value="2" />
                                        <asp:ListItem Text="余款" Value="3" />
                                        <asp:ListItem Text="其他" Value="5" />
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btn_del" CommandName="Del" Text="删除" OnClientClick="return confirm('你确定要删除吗?');" CssClass="btn btn-danger btn-mini" Visible="false" />
                                </td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>

                    <tr runat="server" id="tr_modify" style="height: 16px;">
                        <td>
                            <cc1:DateEditTextBox ID="txtTimer" onclick="WdatePicker();" check="1" runat="server" Width="90" ClientIDMode="Static" Enabled="false"></cc1:DateEditTextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtAmount" check="1" runat="server" Width="90" ClientIDMode="Static"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdoMoneytype" runat="server" RepeatDirection="Horizontal" Width="100%">
                                <asp:ListItem Text="现金" Selected="True">现金</asp:ListItem>
                                <asp:ListItem Text="刷卡">刷卡</asp:ListItem>
                                <asp:ListItem Text="转账">转账</asp:ListItem>
                            </asp:RadioButtonList></td>
                        <td>

                            <asp:RadioButtonList runat="server" ID="rdoNodes" RepeatDirection="Horizontal" Width="100%" ClientIDMode="Static" onclick="RadioSelect()">
                                <asp:ListItem Text="定金" Value="1" Selected="True" />
                                <asp:ListItem Text="中期款" Value="2" />
                                <asp:ListItem Text="余款" Value="3" />
                                <asp:ListItem Text="其他" Value="5" />
                            </asp:RadioButtonList>

                            <asp:TextBox ID="txtNode" runat="server" TextMode="MultiLine" ClientIDMode="Static" Width="281px"></asp:TextBox>
                        </td>

                    </tr>

                </tbody>


                <tfoot>
                    <tr>
                        <td colspan="4">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnCreate" runat="server" Text="保存" OnClick="btnCreate_Click" CssClass="btn btn-success" />
                                    </td>
                                    <td>
                                        <a style="display: block; width: 25px; height: 20px;" class="btn btn-primary" href="javascript:history.go(-1);">返回</a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>

