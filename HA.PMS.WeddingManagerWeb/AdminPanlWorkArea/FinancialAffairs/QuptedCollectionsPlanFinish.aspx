<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuptedCollectionsPlanFinish.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs.QuptedCollectionsPlanFinish" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CustomerTitle.ascx" TagPrefix="HA" TagName="CustomerTitle" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        function OnSubmit() {
            var ReturnValuue = true;
            $(".Timer").each(function () {
                if ($(this).val() == "") {
                    alert("请输入收款时间！");
                    $(this).focus();
                    ReturnValuue = false;
                    return false;
                }

            });
            if (!ReturnValuue) {
                return false;
            }


            $(".Money").each(function () {

                if (parseFloat($(this).val()) == 0) {
                    alert("请输入收款金额！");
                    $(this).focus();
                    ReturnValuue = false;
                    return false;
                }
            });

            return ReturnValuue;
        }
    </script>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div>

        <div>
            <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
            <table class="table table-bordered table-striped">
                <tr>
                    <th>计划收款时间</th>
                    <th>计划收款金额</th>
                    <th>实际收款时间</th>
                    <th>实际收款金额</th>
                    <th>确认</th>
                </tr>
                <asp:Repeater ID="repDataKist" runat="server" OnItemCommand="repDataKist_ItemCommand" OnItemDataBound="repDataKist_ItemDataBound">
                    <ItemTemplate>
                        <tr style="height: 16px;">
                            <td>
                                <asp:HiddenField ID="hidekey" Value='<%#Eval("PlanID") %>' runat="server" />
                                <%#Eval("CreateDate") %>
                            </td>
                            <td>
                                <%#Eval("Amountmoney") %>
                            </td>
                            <td>
                                <cc1:DateEditTextBox ID="txtTimer" onclick="WdatePicker();" Enabled="false" runat="server" Height="18" Text='<%#Eval("CollectionTime") %>' Width="90"></cc1:DateEditTextBox></td>
                            <td>
                                <asp:TextBox ID="txtAmount" MaxLength="10" runat="server"  Enabled="false" Height="18" Text='<%#Eval("Amountmoney") %>' Width="90" CssClass="Money"></asp:TextBox></td>
                            <td>
                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-info" CommandName="Finish" runat="server">确认收款</asp:LinkButton></td>
                        </tr>
                    </ItemTemplate>

                </asp:Repeater>
                <tr>
                    <td colspan="5">
                        余款:<asp:Label ID="lblyukuan" runat="server" Text=""></asp:Label>

                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <asp:Button ID="btnCreate" runat="server" Text="全部确认" OnClick="btnCreate_Click" CssClass="btn btn-success" OnClientClick="return OnSubmit();" />
                    </td>

                </tr>
            </table>
        </div>
    </div>
</asp:Content>

