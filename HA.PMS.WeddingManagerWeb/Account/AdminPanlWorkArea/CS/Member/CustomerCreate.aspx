<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.CustomerCreate" Title="到店新人信息录入" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        $(window).load(function () {
            $("#tblContent").css({ "border": "3px groove #a9a6a6" });
            $("#tblContent td").css({ "border": "1px solid #a9a6a6" });
            $("#lastTd").css({ "border": "none" });
        });
        $()
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $('#<%=txtPartyDay.ClientID%>').change(function () { $(this).blur(); });
        });
        function BindCtrlRegex() {
            BindString(5, '<%=txtGroom.ClientID%>:<%=txtBride.ClientID%>:<%=txtOperator.ClientID%>');
            BindMobile('<%=txtGroomCellPhone.ClientID%>:<%=txtBrideCellPhone.ClientID%>:<%=txtOperatorPhone.ClientID%>');
            BindEmail('<%=txtGroomEmail.ClientID%>:<%=txtBrideEmail.ClientID%>:<%=txtOperatorEmail.ClientID%>');
            BindQQ('<%=txtGroomQQ.ClientID%>:<%=txtBrideQQ.ClientID%>:<%=txtOperatorqq.ClientID%>');
            BindString(20, '<%=txtFroomWeibo.ClientID%>:<%=txtBrideweibo.ClientID%>:<%=txtOperatorweibo.ClientID%>');
            BindString(20, '<%=txtGroomteWeixin.ClientID%>:<%=txtBrideWeiXin.ClientID%>:<%=txtOperatorWeiXin.ClientID%>');
            BindDate('<%=txtPartyDay.ClientID%>');
            BindText(200, '<%=txtOther.ClientID%>');
        }
        function CheckSucces() {
            return ValidateForm('input[check],textarea[check]');
        }
    </script>

    <style type="text/css">
        #TextArea1 {
            width: 413px;
        }
    </style>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div class="widget-box">

        <div class="widget-content ">

            <table id="tblContent">
                <tr>
                    <td>新郎</td>
                    <td><asp:TextBox ID="txtGroom" runat="server" check="0" tip="限5个字符！" /></td>
                    <td><span style="color:red">*</span>新娘</td><td>
                        <asp:TextBox ID="txtBride" check="1" runat="server" tip="（必填）限5个字符！"/>
                    </td>
                    <td>经办人</td>
                    <td><asp:TextBox ID="txtOperator" check="0" runat="server" tip="限5个字符！"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>电话</td>
                    <td><asp:TextBox ID="txtGroomCellPhone" check="0" runat="server" tip="手机号码为11位数字！"></asp:TextBox></td>
                    <td><span style="color:red">*</span>电话</td>
                    <td><asp:TextBox ID="txtBrideCellPhone" check="1" runat="server" tip="（必填）手机号码为11位数字！"></asp:TextBox></td>
                    <td>电话</td>
                    <td><asp:TextBox ID="txtOperatorPhone" runat="server" check="0" tip="手机号码为11位数字！"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>EMail</td>
                    <td><asp:TextBox ID="txtGroomEmail" check="0" runat="server" tip="格式：example@mail.com"></asp:TextBox></td>
                    <td>EMail</td>
                    <td><asp:TextBox ID="txtBrideEmail" check="0" runat="server" tip="格式：example@mail.com"></asp:TextBox></td>
                    <td>EMail</td>
                    <td><asp:TextBox ID="txtOperatorEmail" check="0" runat="server" tip="格式：example@mail.com"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>微信</td>
                    <td><asp:TextBox ID="txtGroomteWeixin" check="0" runat="server" tip="微信"></asp:TextBox></td>
                    <td>微信</td>
                    <td><asp:TextBox ID="txtBrideWeiXin" check="0" runat="server" tip="微信"></asp:TextBox></td>
                    <td>微信</td>
                    <td><asp:TextBox ID="txtOperatorWeiXin" check="0" runat="server" tip="微信"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>微博</td>
                    <td><asp:TextBox ID="txtFroomWeibo" check="0" runat="server" tip="微博"></asp:TextBox></td>
                    <td>微博</td>
                    <td><asp:TextBox ID="txtBrideweibo" check="0" runat="server" tip="微博"></asp:TextBox></td>
                    <td>微博</td>
                    <td><asp:TextBox ID="txtOperatorweibo" check="0" runat="server" tip="微博"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>QQ</td>
                    <td><asp:TextBox ID="txtGroomQQ" check="0" runat="server" tip="QQ号码为5~11个数字！"></asp:TextBox></td>
                    <td>QQ</td>
                    <td><asp:TextBox ID="txtBrideQQ" check="0" runat="server" tip="QQ号码为5~11个数字！"></asp:TextBox></td>
                    <td>QQ</td>
                    <td><asp:TextBox ID="txtOperatorqq" check="0" runat="server" tip="QQ号码为5~11个数字！"></asp:TextBox></td>
                </tr>
                <tr>
                    <td><span style="color:red">*</span>婚期</td>
                    <td><cc1:DateEditTextBox ID="txtPartyDay" tip="举办婚礼的日期！" runat="server" check="1" onclick="WdatePicker();" ></cc1:DateEditTextBox></td>
                    <td>时段</td>
                    <td>
                        <asp:DropDownList ID="ddlTimerSpan" runat="server">
                            <asp:ListItem>中午</asp:ListItem>
                            <asp:ListItem>晚上</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>酒店</td>
                    <td>
                        <cc1:ddlHotel ID="ddlHotel" runat="server"></cc1:ddlHotel></td>
                </tr>
                <tr>
                    <td>合同金额</td>
                    <td>
                        <asp:TextBox ID="txtFinishAmount" runat="server"></asp:TextBox>
                    </td>
                    <td>定金</td>
                    <td>
                        <asp:TextBox ID="txtEMoney" runat="server"></asp:TextBox>
                    </td>
                    <td>首期预付款</td>
                    <td>
                        <asp:TextBox ID="txtdingjin" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>说明</td>
                    <td colspan="5">
                        <asp:TextBox ID="txtOther" runat="server" check="0" Rows="10" Width="98%" TextMode="MultiLine" tip="限200个字符！"></asp:TextBox>
                    </td>
                </tr>
               <tr style="border:none; display:none;">
                    <td  style="border:none;" colspan="5" id="lastTd">&nbsp;</td>
                    <td style="border:none;">                        
                    </td>
                </tr>
            </table>
            <div >
                <asp:Button ID="btnSave" OnClientClick="return CheckSucces();" Text="保存" runat="server" CssClass="btn btn-success" OnClick="btnCreate_Click"  />
                <%--<cc1:ClickOnceButton ID="btnCreate" runat="server" Text="保存" CssClass="btn btn-success" OnClick="btnCreate_Click" />--%>
            </div>
        </div>
    </div>
</asp:Content>


