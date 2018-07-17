<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_SaleSourcesCustomerUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SaleSourcesCustomerUpdate" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "background-color": "transparent", "width": "600px", "height": "300px", "overflow": "hidden" });
            BindString(2, 6, "<%=txtBride.ClientID%>");
            BindQQ("<%=txtBrideQQ.ClientID%>");
            BindMobile("<%=txtBrideCellPhone.ClientID%>");
            BindDate("<%=txtPartyDate.ClientID%>");
            BindText(200, "<%=txtOther.ClientID%>");
            BindCtrlEvent('input[check],textarea[check]');
        });
        function checksave(ctrl) {
            if (ValidateForm('input[check],textarea[check]')) {
                $(ctrl).hide();
                return true;
            }
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box" style="width:97%;margin:0">
        <div class="widget-content">
            <table>
                <tr>
                    <td style="text-align:right">渠道类型</td>
                    <td><asp:DropDownList style="margin:0;width:134px" ID="ddlChannelType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChannelType_SelectedIndexChanged"></asp:DropDownList></td>
                    <td style="text-align:right">渠道名称</td>
                    <td><cc2:ddlChannelName style="margin:0;width:134px" ID="ddlChannelName" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChannelName_SelectedIndexChanged"></cc2:ddlChannelName></td>
                    <td style="text-align:right">推荐人</td>
                    <td><cc2:ddlReferee style="margin:0;width:134px" ID="ddlReferee"  runat="server"></cc2:ddlReferee></td>
                </tr>
                <tr>
                    <td style="text-align:right">新人姓名</td>
                    <td><asp:TextBox ID="txtBride" tip="（必填）限2~6个字符" check="1" runat="server" /></td>
                    <td style="text-align:right">联系电话</td>
                    <td><asp:TextBox ID="txtBrideCellPhone" check="1" tip="（必填）手机号码为11位数字" runat="server" /></td>
                    <td style="text-align:right">联系QQ</td>
                    <td><asp:TextBox ID="txtBrideQQ"  check="0" tip="QQ号码为5~11个位数字" runat="server" /></td>
                </tr>
                <tr>
                    <td style="text-align:right">婚期</td>
                    <td><asp:TextBox onclick="WdatePicker();" ID="txtPartyDate" check="0" tip="举办婚礼日期" runat="server" /></td>
                    <td style="text-align:right">时段</td>
                    <td><asp:DropDownList style="margin:0;width:134px"  ID="ddlTimerSpan" ToolTip="时段" runat="server"><asp:ListItem Value="0" Selected="True">中午</asp:ListItem><asp:ListItem Value="1">晚上</asp:ListItem></asp:DropDownList></td>
                    <td style="text-align:right">酒店</td>
                    <td><cc2:ddlHotel style="margin:0;width:134px" ID="ddlHotel" ToolTip="酒店" runat="server"></cc2:ddlHotel></td>
                </tr>
                <tr>
                    <td style="text-align:right">说明</td>
                    <td colspan="5"><asp:TextBox Rows="4" style="width:97%" TextMode="MultiLine" MaxLength="200" check="0" tip="限200个字符" runat="server"  ID="txtOther"/></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="5"><asp:Button OnClientClick="return checksave(this)" CssClass="btn btn-success" runat="server" Text="保存" ID="btnSave" OnClick="btnSave_Click" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
