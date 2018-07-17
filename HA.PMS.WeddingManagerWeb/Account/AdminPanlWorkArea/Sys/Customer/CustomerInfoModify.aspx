<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" StylesheetTheme="Metro" CodeBehind="CustomerInfoModify.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Customer.CustomerInfoModify" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        td {
            text-align: right;
            padding: 0px;
            margin: 0px;
            font-family: Arial;
            font-size: 14px;
            font-weight: 400;
        }

        .btn-save {
            height: 2.5em;
            width: 4em;
            border-color: #ffffff;
            color: white;
            vertical-align: middle;
            text-align: center;
            background-color: #3764e0;
        }

            .btn-save:hover {
                background-color: #1544c7;
                font-weight: 800;
            }

        * {
            color: #000 !important;
            text-shadow: none !important;
            background: transparent !important;
            box-shadow: none !important;
        }
    </style>
    <script>
        $(function () {
            BindString(2, 10, '<%=txtGroom.ClientID%>:<%=txtBride.ClientID%>:<%=txtOperator.ClientID%>:<%=txtReffer.ClientID%>');
            BindMobile('<%=txtGroomCellPhone.ClientID%>:<%=txtBrideCellPhone.ClientID%>:<%=txtOperatorPhone.ClientID%>:<%=txtBanquetSalesPhone.ClientID%>');
            BindTel('<%=txtGroomtelPhone.ClientID%>:<%=txtBridePhone.ClientID%>:<%=txtOperatorTelPhone.ClientID%>');
            BindEmail('<%=txtGroomEmail.ClientID%>:<%=txtBrideEmail.ClientID%>:<%=txtOperatorEmail.ClientID%>');
            BindQQ('<%=txtGroomQQ.ClientID%>:<%=txtBrideQQ.ClientID%>:<%=txtOperatorQQ.ClientID%>');
            BindString(20, '<%=txtGroomWeiBo.ClientID%>:<%=txtBrideWeiBo.ClientID%>:<%=txtOperatorWeiBo.ClientID%>');
            BindString(20, '<%=txtGroomteWeixin.ClientID%>:<%=txtBrideWeiXin.ClientID%>:<%=txtOperatorWeiXin.ClientID%>');
            BindDate('<%=txtGroomBirthday.ClientID%>:<%=txtBrideBirthday.ClientID%>:<%=txtOperatorBrithday.ClientID%>:<%=txtPartyDay.ClientID%>');
            BindMoney('<%=txtPartyBudget.ClientID%>');
            BindUInt('<%=txtTablenumber.ClientID%>');
            BindText(200, '<%=txtOther.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
        });
        function ValidateForm() {
            return ValidateForm('input[check],textarea[check]');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="height: 950px; overflow: auto;">
        <table style="font-family: sans-serif; width: 100%;" border="1" class="table table-bordered table-striped">
            <tr>
                <td>设置新人</td>
                <td style="text-align: left">
                    <asp:RadioButtonList ID="rdoCustomertype" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem>新娘</asp:ListItem>
                        <asp:ListItem>新郎</asp:ListItem>
                        <asp:ListItem>经办人</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>录入人:<asp:Label ID="lblCreateEmployee" runat="server" Text="Label"></asp:Label>
                    &nbsp;&nbsp;</td>
                <td style="text-align: left">录入日期
                <asp:Label ID="lblCreateDate" runat="server" Text="Label"></asp:Label>
                </td>
                <td>&nbsp;</td>
                <td style="text-align: left">&nbsp;</td>

            </tr>
            <tr>
                <td style="width: 16%;">新郎</td>
                <td style="text-align: left; width: 16%">
                    <asp:TextBox ID="txtGroom" check="0" runat="server" tip="限2~10个字符！" MaxLength="20" /></td>
                <td style="text-align: right; width: 16%"><span style="color: red">*</span>新娘</td>
                <td style="text-align: left; width: 16%">
                    <asp:TextBox ID="txtBride" runat="server" tip="（必填）限2~10个字符！" MaxLength="20"></asp:TextBox></td>
                <td style="text-align: right; width: 16%">经办人</td>
                <td style="text-align: left; width: 16%">
                    <asp:TextBox ID="txtOperator" check="0" runat="server" tip="限2~10个字符！" MaxLength="20"></asp:TextBox></td>

            </tr>
            <tr>
                <td>电话</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGroomCellPhone" check="0" runat="server" tip="手机号码为11位数字！" MaxLength="20"></asp:TextBox></td>
                <td><span style="color: red">*</span>电话</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBrideCellPhone" runat="server" tip="（必填）手机号码为11位数字！" MaxLength="20" /></td>
                <td>电话</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOperatorPhone" check="0" runat="server" tip="手机号码为11位数字！" MaxLength="20"></asp:TextBox></td>

            </tr>
            <tr>
                <td>电话2</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGroomtelPhone" check="0" runat="server" tip="座机" MaxLength="20"></asp:TextBox></td>
                <td>电话2</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBridePhone" check="0" runat="server" tip="座机" MaxLength="20"></asp:TextBox></td>
                <td>电话2</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOperatorTelPhone" check="0" runat="server" tip="座机" MaxLength="20"></asp:TextBox></td>

            </tr>
            <tr>
                <td>生日</td>
                <td style="text-align: left">
                    <asp:TextBox onclick="WdatePicker();" ID="txtGroomBirthday" check="0" runat="server" tip="生日" MaxLength="20" /></td>
                <td>生日</td>
                <td style="text-align: left">
                    <asp:TextBox onclick="WdatePicker();" ID="txtBrideBirthday" check="0" runat="server" tip="生日" MaxLength="20" /></td>
                <td>生日</td>
                <td style="text-align: left">
                    <asp:TextBox onclick="WdatePicker();" ID="txtOperatorBrithday" check="0" runat="server" tip="生日" MaxLength="20" /></td>

            </tr>
            <tr>
                <td>EMail</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGroomEmail" check="0" runat="server" tip="格式：example@mail.com" MaxLength="20"></asp:TextBox></td>
                <td>EMail</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBrideEmail" check="0" runat="server" tip="格式：example@mail.com" MaxLength="20"></asp:TextBox></td>
                <td>EMail</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOperatorEmail" check="0" runat="server" tip="格式：example@mail.com" MaxLength="20"></asp:TextBox></td>

            </tr>
            <tr>
                <td>微信</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGroomteWeixin" check="0" runat="server" tip="微信" MaxLength="20"></asp:TextBox></td>
                <td>微信</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBrideWeiXin" check="0" runat="server" tip="微信" MaxLength="20"></asp:TextBox></td>
                <td>微信</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOperatorWeiXin" check="0" runat="server" tip="微信" MaxLength="20"></asp:TextBox></td>

            </tr>
            <tr>
                <td>微博</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGroomWeiBo" check="0" runat="server" tip="微博" MaxLength="20"></asp:TextBox></td>
                <td>微博</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBrideWeiBo" check="0" runat="server" tip="微博" MaxLength="20"></asp:TextBox></td>
                <td>微博</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOperatorWeiBo" check="0" runat="server" tip="微博" MaxLength="20"></asp:TextBox></td>

            </tr>
            <tr>
                <td>QQ</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGroomQQ" check="0" runat="server" tip="QQ号码为5~11位数字！" MaxLength="20" /></td>
                <td>QQ</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBrideQQ" check="0" runat="server" tip="QQ号码为5~11位数字！" MaxLength="20" /></td>
                <td>QQ</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOperatorQQ" check="0" runat="server" tip="QQ号码为5~11位数字！" MaxLength="20" /></td>

            </tr>
            <tr>
                <td>职业</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGroomJob" runat="server" tip="职业" MaxLength="20"></asp:TextBox></td>
                <td>职业</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBrideJob" runat="server" tip="职业" MaxLength="20"></asp:TextBox></td>
                <td>关系</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOperatorRelationship" runat="server" tip="关系" MaxLength="20"></asp:TextBox></td>

            </tr>
            <tr>
                <td>工作单位</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGroomJobCompany" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>
                <td>工作单位</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBrideJobCompany" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>
                <td>工作单位</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOperatorJob" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>
            </tr>
            <tr>
                <td>职务</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGroomJobs" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>
                <td>职务</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBirdeJobs" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>
                <td>职务</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOperatorJobs" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>

            </tr>
            <tr>
                <td>通讯地址</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGroomContact" runat="server" tip="通讯地址" MaxLength="20"></asp:TextBox></td>
                <td>通讯地址</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBrideContact" runat="server" tip="通讯地址" MaxLength="20"></asp:TextBox></td>
                <td>通讯地址</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOperatorContact" runat="server" tip="通讯地址" MaxLength="20"></asp:TextBox></td>

            </tr>
            <tr>
                <td>身份证号</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGroomIDCard" runat="server" tip="身份证号" MaxLength="20"></asp:TextBox></td>
                <td>身份证号</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBrideIDCard" runat="server" tip="身份证号" MaxLength="20"></asp:TextBox></td>
                <td>身份证号</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtOperatorIDCard" runat="server" tip="身份证号" MaxLength="20"></asp:TextBox></td>

            </tr>
            <tr>
                <td>爱好特长</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGroomHobby" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>
                <td>爱好特长</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBrideHobby" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>
                <td>&nbsp;</td>
                <td style="text-align: left">&nbsp;</td>

            </tr>
        </table>
        <table border="0" style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="10" style="text-align: left;">
                    <hr style="width: 100%;" />
                    庆典要素
                </td>
            </tr>
            <tr>
                <td colspan="10">

                    <table style="width: 100%;">
                        <tr>
                            <td>渠道类型</td>
                            <td style="text-align: left">
                                <cc2:ddlChannelType ID="ddlChannelType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChannelType_SelectedIndexChanged">
                                </cc2:ddlChannelType>
                            </td>
                            <td>来源渠道</td>
                            <td style="text-align: left">
                                <cc2:ddlChannelName ID="ddlChannelName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChannelName_SelectedIndexChanged">
                                </cc2:ddlChannelName>
                            </td>

                            <td style="text-align: left">推荐人</td>
                            <td style="text-align: left">
                                <cc2:ddlReferee ID="ddlReferee" runat="server" Visible="false">
                                </cc2:ddlReferee>
                                <asp:TextBox ID="txtReffer" runat="server" Visible="false"></asp:TextBox></td>
                            <td style="text-align: left">宴会类型</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtCustomersType" check="0" runat="server" tip="客户类型" MaxLength="20"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td><span style="color: red">*</span>婚期</td>
                            <td style="text-align: left">
                                <asp:TextBox onclick="WdatePicker();" ID="txtPartyDay" runat="server" tip="日期" check="1" MaxLength="20" /></td>
                            <td>时段</td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlTimerSpan" Width="153" runat="server">
                                    <asp:ListItem>中午</asp:ListItem>
                                    <asp:ListItem>晚上</asp:ListItem>
                                </asp:DropDownList></td>
                            <td style="text-align: left">酒店</td>
                            <td style="text-align: left">
                                <cc2:ddlHotel ID="ddlHotel" runat="server" Width="153" ToolTip="酒店"></cc2:ddlHotel></td>
                            <td style="text-align: left">
                                <span style="color: red">*</span>桌数</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtTablenumber" runat="server" tip="桌数只能为大于0的整数" MaxLength="20" /></td>
                        </tr>
                        <tr>
                            <td>宴会销售</td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtBanquetSales" check="0" runat="server" tip="宴会销售" MaxLength="20"></asp:TextBox></td>
                            <td>电话</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtBanquetSalesPhone" check="0" runat="server" tip="手机号码为11位数字！" MaxLength="20"></asp:TextBox></td>
                            <td style="text-align: left;">
                                <span style="color: red">*</span>婚礼预算</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtPartyBudget" check="0" runat="server" tip="婚礼预算" MaxLength="20" /></td>
                            <td style="text-align: left;">餐标</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtReceptionStandards" runat="server" tip="餐标" MaxLength="20"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="10" style="text-align: left;">
                    <hr style="width: 100%;" />
                    访谈纪要
                </td>
            </tr>
            <tr>
                <td>最难忘礼物</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtMemorableGift" runat="server" ToolTip="最难忘礼物" MaxLength="20"></asp:TextBox></td>
                <td>最看重项目</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtWeddingServices" runat="server" ToolTip="最看重项目" MaxLength="20"></asp:TextBox></td>
                <td>最喜欢的电影</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtLikeFilm" runat="server" ToolTip="最喜欢的电影" MaxLength="20"></asp:TextBox></td>
                <td>最喜欢的音乐</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtLikeMusic" runat="server" ToolTip="最喜欢的音乐" MaxLength="20"></asp:TextBox></td>
                <td>最喜欢的颜色</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtLikeColor" runat="server" ToolTip="最喜欢的颜色" MaxLength="20"></asp:TextBox></td>
            </tr>
            <tr>
                <td>最想去的地方</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtMostwanttogo" runat="server" ToolTip="最想去的地方" MaxLength="20"></asp:TextBox></td>
                <td>幸运数字</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtLikeNumber" runat="server" ToolTip="幸运数字" MaxLength="20"></asp:TextBox></td>
                <td>最喜欢的明星</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtLikePerson" runat="server" ToolTip="最喜欢的明星" MaxLength="20"></asp:TextBox></td>
                <td>最看重流程</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtImportantProcess" runat="server" ToolTip="最看重流程" MaxLength="20"></asp:TextBox></td>
                <td>婚礼出资人</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtWeddingSponsors" runat="server" ToolTip="婚礼出资人" MaxLength="20"></asp:TextBox></td>
            </tr>
            <tr>
                <td>婚礼形式</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtFormMarriage" runat="server" ToolTip="婚礼形式" MaxLength="20"></asp:TextBox></td>
                <td>有无禁忌</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtNoTaboos" runat="server" ToolTip="有无禁忌" MaxLength="20"></asp:TextBox></td>
                <td>婚礼筹备进度</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtPreparationsWedding" runat="server" ToolTip="婚礼筹备进度" MaxLength="20"></asp:TextBox></td>
                <td>来宾结构</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtGuestsStructure" runat="server" ToolTip="来宾结构" MaxLength="20"></asp:TextBox></td>
                <td>期望出场方式</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtDesiredAppearance" runat="server" ToolTip="期望的出场方式" MaxLength="20"></asp:TextBox></td>
            </tr>
            <tr>
                <td>期望氛围</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtExpectedAtmosphere" runat="server" ToolTip="期望氛围" MaxLength="20"></asp:TextBox></td>
                <td>父母愿望</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtParentsWish" runat="server" ToolTip="父母愿望" MaxLength="20"></asp:TextBox></td>
                <td>爱好特长</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtHobbies" runat="server" ToolTip="爱好特长" MaxLength="20"></asp:TextBox></td>
                <td>燃烧点</td>
                <td style="text-align: left" colspan="3">
                    <asp:TextBox ID="txtGuestsStructure0" runat="server" ToolTip="来宾结构" MaxLength="20" Width="90%"></asp:TextBox></td>
            </tr>
            <tr>
                <td>是否有喜欢的布置风格</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtIntentStyle" runat="server" ToolTip=""></asp:TextBox></td>
                <td>怎么认识的</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtHowKonw" runat="server" ToolTip=""></asp:TextBox></td>
                <td>初次相识的时间地点</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtKonwSite" runat="server" ToolTip=""></asp:TextBox></td>
                <td>从朋友到确定恋人关系的转折点</td>
                <td style="text-align: left" colspan="3">
                    <asp:TextBox ID="txtTurning" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>初次约会的时间</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtBlindDateSite" runat="server" ToolTip="" MaxLength="20"></asp:TextBox></td>
                <td>长辈有何要求</td>
                <td style="text-align: left">
                    <asp:TextBox ID="txtEldersrequirements" runat="server" ToolTip="" MaxLength="20"></asp:TextBox></td>
                <td>&nbsp;</td>
                <td style="text-align: left">&nbsp;</td>
                <td>&nbsp;</td>
                <td style="text-align: left" colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td>备注</td>
                <td style="text-align: left" colspan="9">
                    <asp:TextBox ID="txtOther" check="0" TextMode="MultiLine" Height="45" Width="92%" Rows="2" runat="server" tip="限200个字符！" MaxLength="200"></asp:TextBox></td>
            </tr>
            <tr style="display: none;">
                <td>返点说明</td>
                <td style="text-align: left" colspan="9">
                    <asp:TextBox ID="txtRebates" check="0" TextMode="MultiLine" Height="45" Width="92%" Rows="2" runat="server" tip="请输入返点说明！" MaxLength="200"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="8" style="text-align: center; padding-top: 2em">
                    <asp:Button ID="BtnSave" CssClass="btn-save" Text="保存" OnClientClick="return ValidateForm();" OnClick="BtnSave_Click" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
