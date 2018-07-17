<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer.CustomerCreate" Title="邀约创建人员"  MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        $(window).load(function () {
            BindString(2, 6, '<%=txtGroom.ClientID%>:<%=txtBride.ClientID%>:<%=txtOperator.ClientID%>');
            BindMobile('<%=txtGroomCellPhone.ClientID%>:<%=txtBrideCellPhone.ClientID%>:<%=txtOperatorPhone.ClientID%>');
            BindEmail('<%=txtGroomEmail.ClientID%>:<%=txtBrideEmail.ClientID%>:<%=txtOperatorEmail.ClientID%>');
            BindQQ('<%=txtGroomQQ.ClientID%>:<%=txtBrideQQ.ClientID%>:<%=txtOperatorQQ.ClientID%>');
            BindString(20, '<%=txtGroomWeibo.ClientID%>:<%=txtBrideWeibo.ClientID%>:<%=txtOperatorWeibo.ClientID%>');
            BindString(20, '<%=txtGroomteWeixin.ClientID%>:<%=txtBrideWeiXin.ClientID%>:<%=txtOperatorWeiXin.ClientID%>');
            BindDate('<%=txtPartyDay.ClientID%>');
            BindText(200, '<%=txtOther.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
        });
        function checksave() {
            return ValidateForm('input[check],textarea[check]');
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>
                    <td style="width:60px;text-align:right">新&nbsp;&nbsp;郎&nbsp;</td>
                    <td style="width:164px"><asp:TextBox style="width:160px;margin:0" ID="txtGroom" check="0" tip="限2~6个字符" MaxLength="8" runat="server"></asp:TextBox></td>
                    <td style="width:60px;text-align:right"><span style="color: red">*</span>新&nbsp;&nbsp;娘&nbsp;</td>
                    <td style="width:164px"><asp:TextBox style="width:160px;margin:0" ID="txtBride" runat="server" tip="（必填）限2~6个字符" MaxLength="8" check="1" ClientIDMode="Static"></asp:TextBox></td>
                    <td style="width:60px;text-align:right">经办人&nbsp;</td>
                    <td style="width:164px"><asp:TextBox style="width:160px;margin:0" ID="txtOperator" check="0" runat="server" tip="限2~6个字符" MaxLength="8"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:right">电&nbsp;&nbsp;话&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtGroomCellPhone" check="0" MaxLength="16" runat="server" tip="手机号码为11位数字"></asp:TextBox></td>
                    <td style="text-align:right"><span style="color: red">*</span>电&nbsp;&nbsp;话&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtBrideCellPhone" MaxLength="16" runat="server" tip="（必填）手机号码为11位数字" check="1" ClientIDMode="Static"></asp:TextBox></td>
                    <td style="text-align:right">电&nbsp;&nbsp;话&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtOperatorPhone" MaxLength="16" check="0" tip="手机号码为11位数字" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:right">E-Mail&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtGroomEmail" MaxLength="25" check="0" tip="格式：example@mail.com" runat="server"></asp:TextBox></td>
                    <td style="text-align:right">E-Mail&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtBrideEmail" MaxLength="25" check="0" tip="格式：example@mail.com" runat="server"></asp:TextBox></td>
                    <td style="text-align:right">E-Mail&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtOperatorEmail" MaxLength="25" check="0" tip="格式：example@mail.com" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:right">微&nbsp;&nbsp;信&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtGroomteWeixin" MaxLength="16" check="0" tip="微信" runat="server"></asp:TextBox></td>
                    <td style="text-align:right">微&nbsp;&nbsp;信&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtBrideWeiXin" MaxLength="16" check="0" tip="微信" runat="server"></asp:TextBox></td>
                    <td style="text-align:right">微&nbsp;&nbsp;信&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtOperatorWeiXin" MaxLength="16" check="0" tip="微信" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:right">微&nbsp;&nbsp;博&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtGroomWeibo" MaxLength="16" check="0" tip="微博" runat="server"></asp:TextBox></td>
                    <td style="text-align:right">微&nbsp;&nbsp;博&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtBrideWeibo" MaxLength="16" check="0" tip="微博" runat="server"></asp:TextBox></td>
                    <td style="text-align:right">微&nbsp;&nbsp;博&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtOperatorWeibo" MaxLength="16" check="0" tip="微博" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:right">Ｑ&nbsp;&nbsp;Ｑ&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtGroomQQ" MaxLength="16" check="0" tip="QQ号码为5~11位数字" runat="server"></asp:TextBox></td>
                    <td style="text-align:right">Ｑ&nbsp;&nbsp;Ｑ&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtBrideQQ" MaxLength="16" check="0" tip="QQ号码为5~11位数字" runat="server"></asp:TextBox></td>
                    <td style="text-align:right">Ｑ&nbsp;&nbsp;Ｑ&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtOperatorQQ" MaxLength="16" check="0" tip="QQ号码为5~11位数字" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align:right">婚&nbsp;&nbsp;期&nbsp;</td>
                    <td><asp:TextBox style="width:160px;margin:0" ID="txtPartyDay" tip="举办婚礼的日期" MaxLength="16" check="0" runat="server" onclick="WdatePicker()"></asp:TextBox></td>
                    <td style="text-align:right">时&nbsp;&nbsp;段&nbsp;</td>
                    <td><asp:DropDownList style="width:174px;margin:0" ID="ddlTimerSpan" runat="server">
                            <asp:ListItem>中午</asp:ListItem>
                            <asp:ListItem>晚上</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="text-align:right">酒&nbsp;&nbsp;店&nbsp;</td>
                    <td><cc1:ddlHotel style="width:174px;margin:0" MaxLength="32" ID="ddlHotel" runat="server"></cc1:ddlHotel>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right">说&nbsp;&nbsp;明&nbsp;</td>
                    <td colspan="5"><asp:TextBox style="margin:0;width:98%" ID="txtOther" check="0" tip="限200个字符" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="5"><asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" OnClientClick="return checksave()" CssClass="btn btn-success" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>


