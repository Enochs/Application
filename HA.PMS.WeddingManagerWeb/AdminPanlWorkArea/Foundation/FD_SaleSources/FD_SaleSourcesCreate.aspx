<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_SaleSourcesCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SaleSourcesCreate" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/Default/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/App_Themes/Default/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            BindString(20, '<%=txtSourcename.ClientID%>');
            BindDate('<%=txtProlongationDate.ClientID%>')
            BindText(50, '<%=txtAddress.ClientID%>');
            BindString(20, '<%=txtTactcontactsType1.ClientID%>:<%=txtTactcontactsType2.ClientID%>:<%=txtTactcontactsType3.ClientID%>');
            BindString(20, '<%=txtTactcontactsJob1.ClientID%>:<%=txtTactcontactsJob2.ClientID%>:<%=txtTactcontactsJob3.ClientID%>');
            BindString(20, '<%=txtWeibo1.ClientID%>:<%=txtWeibo2.ClientID%>:<%=txtWeibo3.ClientID%>');
            BindString(20, '<%=txtWenXin1.ClientID%>:<%=txtWenXin2.ClientID%>:<%=txtWenXin3.ClientID%>');
            BindQQ('<%=txtQQ1.ClientID%>:<%=txtQQ3.ClientID%>:<%=txtQQ3.ClientID%>');
            BindString(2, 6, '<%=txtTactcontacts1.ClientID%>:<%=txtTactcontacts2.ClientID%>:<%=txtTactcontacts3.ClientID%>');
            BindMobile('<%=txtTactcontactsPhone1.ClientID%>:<%=txtTactcontactsPhone2.ClientID%>:<%=txtTactcontactsPhone3.ClientID%>');
            BindEmail('<%=txtEmail1.ClientID%>:<%=txtEmail2.ClientID%>:<%=txtEmail3.ClientID%>');
            BindText(200, '<%=txtSynopsis.ClientID%>:<%=txtPreferentialpolicy.ClientID%>:<%=txtRebatepolicy.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
        });
 
        function checksave() {
            if (!ValidateForm('input[check],textarea[check]')) {
                if ($(".tooltipinputerr").length > 0) {
                    $(".tooltipinputerr")[0].focus();
                }
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>
                    <td style="text-align: right;">渠道类型&nbsp;</td>
                    <td><asp:DropDownList ID="ddlChannelType" Style="margin: 0; width: 174px" runat="server"></asp:DropDownList></td>
                    <td colspan="4">&nbsp;&nbsp;<a href="FD_ChannelTypeManager.aspx?NeedPopu=1" class="btn btn-mini btn-success">添加新渠道类型</a></td>
                </tr>
                <tr>
                    <td style="text-align: right;"><span style="color: red">*</span>渠道名称&nbsp;</td>
                    <td><asp:TextBox ID="txtSourcename" Style="width: 160px; margin: 0" runat="server" ata-title="" data-content="（必填）限20个字符" CssClass="overtip" check="1" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right;">是否返利&nbsp;</td>
                    <td><asp:DropDownList ID="ddlNeedRebate" Width="135" Style="margin: 0; width: 174px" runat="server"><asp:ListItem Value="0">是</asp:ListItem><asp:ListItem Value="1">否</asp:ListItem></asp:DropDownList></td>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td style="text-align: right;">&nbsp;地&nbsp;&nbsp;址&nbsp;</td>
                    <td colspan="5"><asp:TextBox Style="width: 636px; margin: 0" ID="txtAddress" check="0" data-title="" data-content="限50个字符" CssClass="overtip" runat="server" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right;">拓展人&nbsp;</td>
                    <td><asp:DropDownList ID="ddlProlongationEmployee" Style="width: 174px; margin: 0" runat="server"></asp:DropDownList></td>
                    <td style="text-align: right;">维护人&nbsp;</td>
                    <td><asp:DropDownList ID="ddlMaintenanceEmployee" Style="width: 174px; margin: 0" runat="server"></asp:DropDownList></td>
                    <td style="text-align: right;"><span style="color: red">*</span>拓展时间&nbsp;</td>
                    <td><asp:TextBox ID="txtProlongationDate" Style="width: 160px; margin: 0" onclick="WdatePicker();" check="1" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right"><span style="color: red">*</span>渠道联系人</td>
                    <td><asp:TextBox ID="txtTactcontacts1" check="1" Style="width: 160px; margin: 0;" runat="server" data-title="" data-content="（必填）限2~6个字符" CssClass="overtip" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">渠道联系人</td>
                    <td><asp:TextBox ID="txtTactcontacts2" check="0" Style="width: 160px; margin: 0" runat="server" data-title="" data-content="限2~6个字符" CssClass="overtip" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">渠道联系人</td>
                    <td><asp:TextBox ID="txtTactcontacts3" check="0" Style="width: 160px; margin: 0" runat="server" data-title="" data-content="限2~6个字符" CssClass="overtip" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">生日</td>
                    <td><asp:TextBox ID="txtTactcontactsType1" onclick="WdatePicker();" check="0" Style="width: 160px; margin: 0" runat="server" data-title="" data-content="限20个字符" CssClass="overtip" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">生日</td>                                                      
                    <td><asp:TextBox ID="txtTactcontactsType2" onclick="WdatePicker();" check="0" Style="width: 160px; margin: 0" runat="server" data-title="" data-content="限20个字符" CssClass="overtip" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">生日</td>                                                      
                    <td><asp:TextBox ID="txtTactcontactsType3" onclick="WdatePicker();" check="0" Style="width: 160px; margin: 0" runat="server" data-title="" data-content="限20个字符" CssClass="overtip" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">联系人职务</td>
                    <td><asp:TextBox ID="txtTactcontactsJob1" check="0" Style="width: 160px; margin: 0" runat="server" data-title="" data-content="限20个字符" CssClass="overtip" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">联系人职务</td>                                                   
                    <td><asp:TextBox ID="txtTactcontactsJob2" check="0" Style="width: 160px; margin: 0" runat="server" data-title="" data-content="限20个字符" CssClass="overtip" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">联系人职务</td>
                    <td><asp:TextBox ID="txtTactcontactsJob3" check="0" Style="width: 160px; margin: 0" runat="server" data-title="" data-content="限20个字符" CssClass="overtip" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right"><span style="color: red">*</span>联系人手机</td>
                    <td><asp:TextBox ID="txtTactcontactsPhone1" Style="width: 160px; margin: 0" check="1" data-title="" data-content="（必填）手机号码为11位数字" CssClass="overtip" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">联系人手机</td>
                    <td><asp:TextBox ID="txtTactcontactsPhone2" Style="width: 160px; margin: 0" check="0" data-title="" data-content="手机号码为11位数字" CssClass="overtip" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">联系人手机</td>
                    <td><asp:TextBox ID="txtTactcontactsPhone3" Style="width: 160px; margin: 0" check="0" data-title="" data-content="手机号码为11位数字" CssClass="overtip" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">Email</td>
                    <td><asp:TextBox ID="txtEmail1" check="0" Style="width: 160px; margin: 0" data-title="" data-content="格式：example@mail.com" CssClass="overtip" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td style="text-align: right">Email</td>
                    <td><asp:TextBox ID="txtEmail2" check="0" Style="width: 160px; margin: 0" data-title="" data-content="格式：example@mail.com" CssClass="overtip" runat="server" MaxLength="50"></asp:TextBox></td>
                    <td style="text-align: right">Email</td>
                    <td><asp:TextBox ID="txtEmail3" check="0" Style="width: 160px; margin: 0" data-title="" data-content="格式：example@mail.com" CssClass="overtip" runat="server" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">QQ</td>
                    <td><asp:TextBox ID="txtQQ1" check="0" Style="width: 160px; margin: 0" data-title="" data-content="QQ号码为5~11位数字" CssClass="overtip" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">QQ</td>
                    <td><asp:TextBox ID="txtQQ2" check="0" Style="width: 160px; margin: 0" data-title="" data-content="QQ号码为5~11位数字" CssClass="overtip" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">QQ</td>
                    <td><asp:TextBox ID="txtQQ3" check="0" Style="width: 160px; margin: 0" data-title="" data-content="QQ号码为5~11位数字" CssClass="overtip" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">微信</td>
                    <td><asp:TextBox ID="txtWeibo1" check="0" Style="width: 160px; margin: 0" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">微信</td>   
                    <td><asp:TextBox ID="txtWeibo2" check="0" Style="width: 160px; margin: 0" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">微信</td>   
                    <td><asp:TextBox ID="txtWeibo3" check="0" Style="width: 160px; margin: 0" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>                                         
                <tr>                                          
                    <td style="text-align: right">微博</td>   
                    <td><asp:TextBox ID="txtWenXin1" check="0"  Style="width: 160px; margin: 0" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">微博</td>   
                    <td><asp:TextBox ID="txtWenXin2" check="0"  Style="width: 160px; margin: 0" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">微博</td>   
                    <td><asp:TextBox ID="txtWenXin3" check="0"  Style="width: 160px; margin: 0" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>                                          
                    <td style="text-align: right">备注</td>   
                    <td><asp:TextBox ID="txtNode1" check="0"  Style="width: 160px; margin: 0" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">备注</td>   
                    <td><asp:TextBox ID="txtNode2" check="0"  Style="width: 160px; margin: 0" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right">备注</td>   
                    <td><asp:TextBox ID="txtNode3" check="0"  Style="width: 160px; margin: 0" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">开户银行</td>
                    <td colspan="5">
                        <asp:TextBox ID="txtBankName" check="0" data-title="" data-content="请输入开户银行的名称" Style="width: 160px; margin: 0" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">银行卡号</td>
                    <td colspan="5">
                        <asp:TextBox ID="txtBankCard" check="0" data-title="" data-content="请输入你的银行卡号" Style="width: 160px; margin: 0" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">渠道简介</td>
                    <td colspan="5"><asp:TextBox ID="txtSynopsis" check="0" data-title="" data-content="限200个字符" CssClass="overtip" Style="width: 637px; margin: 0" TextMode="MultiLine" runat="server" MaxLength="200"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">优惠政策</td>
                    <td colspan="5"><asp:TextBox ID="txtPreferentialpolicy" check="0" data-title="" data-content="限200个字符" CssClass="overtip" Style="width: 637px; margin: 0" TextMode="MultiLine" runat="server" MaxLength="200"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">返利政策</td>
                    <td colspan="5"><asp:TextBox ID="txtRebatepolicy" data-title="" data-content="限200个字符" CssClass="overtip" check="0" Style="width: 637px; margin: 0" TextMode="MultiLine" runat="server" MaxLength="200"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="5">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" OnClientClick="return checksave()" runat="server" Text="保存" OnClick="btnSave_Click" Height="35" />
                        <asp:Button ID="btnCreateCustomer" CssClass="btn btn-success" runat="server" Text="为渠道添加新人信息" Visible="false" OnClick="btnCreateCustomer_Click" />
                        <asp:HiddenField ID="hiddKey" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
