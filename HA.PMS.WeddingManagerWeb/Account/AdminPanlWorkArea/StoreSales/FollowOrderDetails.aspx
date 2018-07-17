<%@ Page Language="C#" StylesheetTheme="Default" AutoEventWireup="true" CodeBehind="FollowOrderDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.FollowOrderDetails" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<asp:Content ContentPlaceHolderID="head" runat="server" ID="Content2">
    <link href="/App_Themes/Default/css/bootstrap.min.css" rel="stylesheet" />
    <%--<script src="/App_Themes/Default/js/bootstrap.min.js"></script>
    <script src="../../App_Themes/Default/js/jquery.min.js"></script>--%>

    <style type="text/css">
        .td_Collection {
            width: 75px;
        }
    </style>
    <script type="text/javascript">
        $(window).load(function () {
            $(".Upload").each(function () {
                showPopuWindows($(this).attr("href"), 700, 300, $(this));
            });
            ChangeStatu();
            $("html,body").css({ "background-color": "transparent" });
        });
        $(function () {
            $(".lose").hide();
            $(".Sucess").hide();
            if ($("#hiddIsSucess").val() == "1") {
                $("[type='text']").attr("disabled", "disabled");
                $("[type='button']").attr("disabled", "disabled");
                $("[type='submit']").hide();
            } else if ($("#hiddIsSucess").val() == "0") {
                $("[type='text']").attr("disabled", "disabled");
                $("[type='button']").attr("disabled", "disabled");
            }

        });
        function ChangeStatu() {
            $(".SucessTd").hide();
            $(".lose").hide();
            $(".Sure").hide();
            $(".tr_Collection").hide();
            $(".table_Collection").hide();
            $(".youxuan").hide();
            $("input[type='radio']").click(function () {
                if ($("input[type='radio']:checked").val() == 10) {
                    $(".NoLoss").hide();
                    $(".lose").show();
                    $(".Sure").hide();
                    $("#txtStateName").attr("value", "流失");
                    $(".table_Collection").hide();
                } else {
                    $(".Sure").hide();
                    $(".NoLoss").show();
                    $(".lose").hide();
                    $(".table_Collection").hide();
                }
                if ($("input[type='radio']:checked").val() == 202) {
                    $(".youxuan").show();
                    $(".lose").hide();
                    $(".Sure").hide();
                    $(".NoLoss").show();
                    $(".SucessTd").hide();
                    $(".NextSucessTd").show();
                    $("#txtStateName").attr("value", "优选");
                    $(".table_Collection").hide();
                }
                if ($("input[type='radio']:checked").val() == 204) {
                    $(".youxuan").hide();
                    $(".lose").hide();
                    $(".NoLoss").show();
                    $(".NextSucessTd").hide();
                    $("#txtStateName").attr("value", "确定");
                    $(".Sure").show();
                    $(".SucessTd").show();
                    $(".table_Collection").show();

                }
                if ($("input[type='radio']:checked").val() == 203) {
                    $(".youxuan").hide();
                    $(".lose").hide();
                    $(".NoLoss").hide();

                    $(".Sure").hide();
                    $(".NoLoss").show();
                    $(".SucessTd").hide();
                    $(".NextSucessTd").show();
                    $("#txtStateName").attr("value", "建立信任");
                    $(".table_Collection").hide();
                }
            });

        }

        $(document).ready(function () {
            $(".tr_Package").hide();
            $("#rdoTypes").click(function () {
                var index = $("#<%=rdoTypes.ClientID %>").find("input[type='radio']:checked").val();
                if (index == "1") {
                    $(".tr_Package").hide();
                }
                else if (index == "2") {
                    $(".tr_Package").show();
                }
            });

        });

            function ShowOrderMessAge() {
                var URI = "OrderMessAgeList.aspx?OrderID=" +<%=Request["OrderID"] %> +"";
                showPopuWindows(URI, 600, 300, "#SelectEmpLoyeeBythis");
                $("#SelectEmpLoyeeBythis").click();
                return false;
            }



            function ShowRef() {
                var URI = "OrderReferenceShow.aspx";
                showPopuWindows(URI, 600, 0, "#SelectEmpLoyeeBythis");
                $("#SelectEmpLoyeeBythis").click();
                return false;
            }
            $(window).load(function () {
                $("#ddlState").change(function () {
                    var SelectItem = $("#ddlState").find("option:selected").text();
                    if (SelectItem == "流失") {
                        $(".NoLoss").hide();
                        $(".lose").show();
                    } else {
                        $(".lose").hide();
                        $(".NoLoss").show();
                    }
                });
            });
            function ShowQuptedPopu(Parent) {
                var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ClassType=QuotedPriceWorkPanel&ALL=1";
                showPopuWindows(Url, 450, 700, "#SelectEmpLoyeeBythis");
                $("#SelectEmpLoyeeBythis").click();
            }

            //点击文本框 弹出部门人员列表
            function ShowPopu(Parent) {
                var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=1&ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
                showPopuWindows(Url, 480, 380, "#SelectEmpLoyeeBythis");
                $("#SelectEmpLoyeeBythis").click();
            }

            $(window).load(function () {
                BindString(2, 6, '<%=txtGroom.ClientID%>:<%=txtBride.ClientID%>:<%=txtOperator.ClientID%>:<%=lblReffer.ClientID%>');
                BindMobile('<%=txtGroomCellPhone.ClientID%>:<%=txtBrideCellPhone.ClientID%>:<%=txtOperatorPhone.ClientID%>:<%=txtBanquetSalesPhone.ClientID%>');
                BindTel('<%=txtGroomtelPhone.ClientID%>:<%=txtBridePhone.ClientID%>:<%=txtOperatorTelPhone.ClientID%>');
                BindEmail('<%=txtGroomEmail.ClientID%>:<%=txtBrideEmail.ClientID%>:<%=txtOperatorEmail.ClientID%>');
                BindQQ('<%=txtGroomQQ.ClientID%>:<%=txtBrideQQ.ClientID%>:<%=txtOperatorQQ.ClientID%>');
                BindString(20, '<%=txtGroomWeiBo.ClientID%>:<%=txtBrideWeiBo.ClientID%>:<%=txtOperatorWeiBo.ClientID%>');
                BindString(20, '<%=txtGroomteWeixin.ClientID%>:<%=txtBrideWeiXin.ClientID%>:<%=txtOperatorWeiXin.ClientID%>');
                BindDate('<%=txtGroomBirthday.ClientID%>:<%=txtBrideBirthday.ClientID%>:<%=txtOperatorBrithday.ClientID%>:<%=txtConvenientIinvitationTime.ClientID%>:<%=txtPartyDay.ClientID%>');
                BindMoney('<%=txtPartyBudget.ClientID%>');
                BindUInt('<%=txtTablenumber.ClientID%>');
                BindText(200, '<%=txtOther.ClientID%>');
                BindText(200, '<%=txtExperience.ClientID%>:<%=txtMemorable.ClientID%>:<%=txtTheProposal.ClientID%>:<%=txtAspirations.ClientID%>:<%=txtWatchingExperience.ClientID%>:<%=txtCustomerFlowContent.ClientID%>:<%=txtFirePoint.ClientID%>');
                BindCtrlEvent('input[check],textarea[check]');
            });
            function checksave(ctrl) {
                if (!ValidateForm('input[check],textarea[check]')) {
                    window.parent.document.body.scrollTop = 270;
                    window.parent.blur();
                    if ($(".tooltipinputerr").length > 0) {
                        if (!$("#collapseOne").hasClass("in")) {
                            if ($("#collapseOne").find(".tooltipinputerr").length > 0) {
                                $("#acollapseOne").click();
                            }
                        }
                        if (!$("#collapseTwo").hasClass("in")) {
                            if ($("#collapseTwo").find(".tooltipinputerr").length > 0) {
                                $("#acollapseTwo").click();
                            }
                        }
                        $(".tooltipinputerr")[0].focus();
                    }
                    return false;
                }
                if ($("input[type='radio']:checked").val() == 204) {

                    if ($("#txtAgainDate").val() == "") {
                        alert("请选择再次沟通时间");
                        return false;
                    }
                    var type = $("#<%=rdoMoneytypes.ClientID %>").find("input[type='radio']:checked").val();
                    if (type == 4) {
                    }


                    var index = $("#<%=rdoTypes.ClientID %>").find("input[type='radio']:checked").val();
                    if (index == "2") {
                        if ($("#<%=ddlPackgeName.ClientID %> option:selected").text() == "请选择") {
                            alert("请选择套餐名称");
                            return false;
                        }
                    }
                    if ($("#txtRealityAmount").val() == "") {
                        alert("请输入定金金额");
                        return false;
                    }
                }
                return true;
            }
            $(document).ready(function () {
                $("#lblCollaspe").click(function () {
                    if ($("#lblCollaspe").text() == "︽") {
                        $("#CollapseInOne").slideToggle("slow");
                        document.getElementById("lblCollaspe").innerText = "︾";
                    } else if ($("#lblCollaspe").text() == "︾") {

                        $("#CollapseInOne").slideToggle("slow");
                        document.getElementById("lblCollaspe").innerText = "︽";
                    }
                });
            });

            function ExpandOrHide(control) {
                if ($("#lblExpand").text() == "︽") {
                    $("#clooapseTwos").slideToggle("slow");
                    document.getElementById("lblExpand").innerText = "︾";
                } else if ($("#lblExpand").text() == "︾") {
                    $("#clooapseTwos").slideToggle("slow");
                    document.getElementById("lblExpand").innerText = "︽";
                }
            }

    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <a name="#top" id="#top"></a>
    <div style="overflow-x: scroll; height: 1000px;">
        <a f="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
        <asp:HiddenField ID="hiddIsSucess" runat="server" ClientIDMode="Static" />
        <div class="widget-box">
            <div class="widget-content">
                <div class="panel-group" id="accordion">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a id="acollapseOne" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">基础信息</a>&nbsp;&nbsp;
                                <asp:Label runat="server" ID="lblCollaspe" Text="︽" ClientIDMode="Static" Style="cursor: pointer; font-size: 16px;" />
                            </h4>

                        </div>
                        <div id="collapseOne" class="panel-collapse collapse in">
                            <div id="CollapseInOne" class="panel-body">
                                <table style="font-family: sans-serif; width: 970px">
                                    <tr>
                                        <td style="text-align: right">新&nbsp;&nbsp;郎&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtGroom" Style="margin: 0" check="0" runat="server" tip="限2~6个字符！" MaxLength="20" /></td>
                                        <td style="text-align: right"><span style="color: red">*</span>新&nbsp;&nbsp;娘&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtBride" Style="margin: 0" check="1" runat="server" tip="（必填）限2~6个字符！" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">经办人&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtOperator" Style="margin: 0" check="0" runat="server" tip="限2~6个字符！" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">来源渠道&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="lblChannel" Style="margin: 0" runat="server" Enabled="false"></asp:TextBox></td>
                                        <td style="text-align: right">渠道类型&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="lblChannelType" Style="margin: 0" runat="server" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">电&nbsp;&nbsp;话&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomCellPhone" Style="margin: 0" check="0" runat="server" tip="手机号码为11位数字！" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right"><span style="color: red">*</span>电&nbsp;&nbsp;话&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtBrideCellPhone" Style="margin: 0" check="1" runat="server" tip="（必填）手机号码为11位数字！" MaxLength="20" /></td>
                                        <td style="text-align: right">电&nbsp;&nbsp;话&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtOperatorPhone" Style="margin: 0" check="0" runat="server" tip="手机号码为11位数字！" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">录入人&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="lblCreateEmpLoyee" Style="margin: 0" runat="server" Enabled="false"></asp:TextBox></td>
                                        <td style="text-align: right">录入日期&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="lblCreateDate" Style="margin: 0" runat="server" Enabled="false"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">座&nbsp;&nbsp;机&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomtelPhone" Style="margin: 0" check="0" runat="server" tip="座机" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">座&nbsp;&nbsp;机&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtBridePhone" Style="margin: 0" check="0" runat="server" tip="座机" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">座&nbsp;&nbsp;机&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtOperatorTelPhone" Style="margin: 0" check="0" runat="server" tip="座机" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">婚&nbsp;&nbsp;期&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtPartyDay" Style="margin: 0" onclick="WdatePicker();" runat="server" tip="日期" check="0" MaxLength="20" /></td>
                                        <td style="text-align: right">时&nbsp;&nbsp;段&nbsp;</td>
                                        <td>
                                            <asp:DropDownList ID="ddlTimerSpan" Style="margin: 0; width: 134px" runat="server">
                                                <asp:ListItem>中午</asp:ListItem>
                                                <asp:ListItem>晚上</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">生&nbsp;&nbsp;日&nbsp;</td>
                                        <td>
                                            <asp:TextBox onclick="WdatePicker();" ID="txtGroomBirthday" Style="margin: 0" check="0" runat="server" tip="生日" MaxLength="20" /></td>
                                        <td style="text-align: right">生&nbsp;&nbsp;日&nbsp;</td>
                                        <td>
                                            <asp:TextBox onclick="WdatePicker();" ID="txtBrideBirthday" Style="margin: 0" check="0" runat="server" tip="生日" MaxLength="20" /></td>
                                        <td style="text-align: right">生&nbsp;&nbsp;日&nbsp;</td>
                                        <td>
                                            <asp:TextBox onclick="WdatePicker();" ID="txtOperatorBrithday" Style="margin: 0" check="0" runat="server" tip="生日" MaxLength="20" /></td>
                                        <td style="text-align: right">酒&nbsp;&nbsp;店&nbsp;</td>
                                        <td>
                                            <cc2:ddlHotel ID="ddlHotel" Style="margin: 0; width: 134px" runat="server" ToolTip="酒店"></cc2:ddlHotel>
                                        </td>
                                        <td style="text-align: right"><span style="color: red">*</span>桌&nbsp;&nbsp;数&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtTablenumber" Style="margin: 0" runat="server" check="1" tip="桌数只能为大于0的整数" MaxLength="20" /></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">E-Mail&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomEmail" Style="margin: 0" check="0" runat="server" tip="格式：example@mail.com" MaxLength="25"></asp:TextBox></td>
                                        <td style="text-align: right">E-Mail&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtBrideEmail" Style="margin: 0" check="0" runat="server" tip="格式：example@mail.com" MaxLength="25"></asp:TextBox></td>
                                        <td style="text-align: right">E-Mail&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtOperatorEmail" Style="margin: 0" check="0" runat="server" tip="格式：example@mail.com" MaxLength="25"></asp:TextBox></td>
                                        <td style="text-align: right">宴会销售&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtBanquetSales" Style="margin: 0" check="0" runat="server" tip="宴会销售" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">电&nbsp;&nbsp;话&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtBanquetSalesPhone" Style="margin: 0" check="0" runat="server" tip="手机号码为11位数字！" MaxLength="20"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">微&nbsp;&nbsp;信&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomteWeixin" Style="margin: 0" check="0" runat="server" tip="微信" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">微&nbsp;&nbsp;信&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtBrideWeiXin" Style="margin: 0" check="0" runat="server" tip="微信" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">微&nbsp;&nbsp;信&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtOperatorWeiXin" Style="margin: 0" check="0" runat="server" tip="微信" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">客户类型&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtCustomersType" Style="margin: 0" check="0" runat="server" tip="客户类型" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">宴会类型&nbsp;</td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtBanquetTypes" Style="margin: 0" check="0" runat="server" tip="宴会类型" MaxLength="20"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">微&nbsp;&nbsp;博&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomWeiBo" Style="margin: 0" check="0" runat="server" tip="微博" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">微&nbsp;&nbsp;博&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtBrideWeiBo" Style="margin: 0" check="0" runat="server" tip="微博" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">微&nbsp;&nbsp;博&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtOperatorWeiBo" Style="margin: 0" check="0" runat="server" tip="微博" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right"><span style="color: red">*</span>婚礼预算&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtPartyBudget" Style="margin: 0" check="1" runat="server" tip="婚礼预算" MaxLength="20" /></td>
                                        <td style="text-align: right">方便时间&nbsp;</td>
                                        <td>
                                            <asp:TextBox onclick="WdatePicker();" Style="margin: 0" ID="txtConvenientIinvitationTime" check="0" runat="server" tip="方便联系时间" MaxLength="20"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">Ｑ&nbsp;&nbsp;Ｑ&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomQQ" Style="margin: 0" check="0" runat="server" tip="QQ号码为5~11位数字！" MaxLength="20" /></td>
                                        <td style="text-align: right">Ｑ&nbsp;&nbsp;Ｑ&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtBrideQQ" Style="margin: 0" check="0" runat="server" tip="QQ号码为5~11位数字！" MaxLength="20" /></td>
                                        <td style="text-align: right">Ｑ&nbsp;&nbsp;Ｑ&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtOperatorQQ" Style="margin: 0" check="0" runat="server" tip="QQ号码为5~11位数字！" MaxLength="20" /></td>
                                        <td style="text-align: right">推荐人&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="lblReffer" Style="margin: 0" runat="server" Enabled="false"></asp:TextBox></td>
                                        <td style="text-align: right">餐&nbsp;&nbsp;标&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtReceptionStandards" Style="margin: 0" runat="server" tip="餐标" MaxLength="20"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">职&nbsp;&nbsp;业&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomJob" Style="margin: 0" runat="server" tip="职业" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">职&nbsp;&nbsp;业&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtBrideJob" Style="margin: 0" runat="server" tip="职业" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">关&nbsp;&nbsp;系&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtOperatorRelationship" Style="margin: 0" runat="server" tip="关系" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right" rowspan="3">说&nbsp;&nbsp;明&nbsp;</td>
                                        <td colspan="3" rowspan="3">
                                            <asp:TextBox ID="txtOther" check="0" Style="margin: 0; width: 95%; height: 100%" TextMode="MultiLine" Rows="2" runat="server" tip="限200个字符！" MaxLength="200"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">工作单位&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomJobCompany" Style="margin: 0" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">工作单位&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtBrideJobCompany" Style="margin: 0" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">工作单位&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtOperatorJob" Style="margin: 0" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>身份证号码</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtGroomIDCard" runat="server" tip="身份证号码" MaxLength="20"></asp:TextBox></td>
                                        <td>身份证号码</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtBrideIDCard" runat="server" tip="身份证号码" MaxLength="20"></asp:TextBox></td>
                                        <td>&nbsp;</td>

                                    </tr>
                                    <tr>
                                        <td>爱好特长</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtGroomHobby" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>
                                        <td>爱好特长</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtBrideHobby" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox></td>
                                        <td>&nbsp;</td>

                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a id="acollapseTwo" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">更多资料</a>&nbsp;&nbsp;
                                <asp:Label runat="server" ID="lblExpand" Text="︽" ClientIDMode="Static" Style="cursor: pointer; font-size: 16px;" onclick="ExpandOrHide(this)" />
                            </h4>
                        </div>
                        <div id="collapseTwo" class="panel-collapse collapse in">
                            <div id="clooapseTwos" class="panel-body">
                                <table style="font-family: sans-serif; width: 970px">
                                    <tr>
                                        <td style="text-align: right;">婚礼形式&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtFormMarriage" Style="margin: 0" runat="server" ToolTip="婚礼形式" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right; width: 121px">最难忘礼物&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtMemorableGift" Style="margin: 0" runat="server" ToolTip="最难忘礼物" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right; width: 121px">最喜欢的电影&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtLikeFilm" Style="margin: 0" runat="server" ToolTip="最喜欢的电影" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right; width: 121px">最喜欢的音乐&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtLikeMusic" Style="margin: 0" runat="server" ToolTip="最喜欢的音乐" MaxLength="20"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">幸运数字&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtLikeNumber" Style="margin: 0" runat="server" ToolTip="幸运数字" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">最喜欢的颜色&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtLikeColor" Style="margin: 0" runat="server" ToolTip="最喜欢的颜色" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">最想去的地方&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtMostwanttogo" Style="margin: 0" runat="server" ToolTip="最想去的地方" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">最喜欢的明星&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtLikePerson" Style="margin: 0" runat="server" ToolTip="最喜欢的明星" MaxLength="20"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">有无禁忌&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtNoTaboos" Style="margin: 0" runat="server" ToolTip="有无禁忌" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">最看重流程&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtImportantProcess" Style="margin: 0" runat="server" ToolTip="最看重流程" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">婚礼出资人&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtWeddingSponsors" Style="margin: 0" runat="server" ToolTip="婚礼出资人" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">最看重项目&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtWeddingServices" Style="margin: 0" runat="server" ToolTip="最看重项目" MaxLength="20"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">期望氛围&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtExpectedAtmosphere" Style="margin: 0" runat="server" ToolTip="期望氛围" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">婚礼筹备进度&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtPreparationsWedding" Style="margin: 0" runat="server" ToolTip="婚礼筹备进度" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">来宾结构&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtGuestsStructure" Style="margin: 0" runat="server" ToolTip="来宾结构" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">期望出场方式&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtDesiredAppearance" Style="margin: 0" runat="server" ToolTip="期望的出场方式" MaxLength="20"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>是否有喜欢布置风格</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtIntentStyle" runat="server" ToolTip=""></asp:TextBox></td>
                                        <td style="text-align: right">怎么认识</td>
                                        <td style="text-align: right">
                                            <asp:TextBox ID="txtHowKonw" runat="server" ToolTip=""></asp:TextBox></td>
                                        <td style="text-align: right">初次相识时间地点</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtKonwSite" runat="server" ToolTip=""></asp:TextBox></td>
                                        <td style="text-align: right">确定关系转折点</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtTurning" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">初次约会的时间</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtBlindDateSite" runat="server" ToolTip="" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">长辈有何要求</td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtEldersrequirements" runat="server" ToolTip="" MaxLength="20"></asp:TextBox></td>
                                        <td>&nbsp;</td>
                                        <td style="text-align: left">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td style="text-align: left">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">父母愿望&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtParentsWish" Style="margin: 0" runat="server" ToolTip="父母愿望" MaxLength="20"></asp:TextBox></td>
                                        <td style="text-align: right">爱好特长&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtHobbies" Style="margin: 0" runat="server" ToolTip="爱好特长" MaxLength="20"></asp:TextBox></td>
                                        <td colspan="4"></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">难忘的事&nbsp;</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtMemorable" Style="margin: 0; width: 96%" check="0" tip="限200个字符！" MaxLength="200" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                                        <td style="text-align: right">相识相恋的经历&nbsp;</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtExperience" Style="margin: 0; width: 96%" check="0" tip="限200个字符！" MaxLength="200" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">求婚过程&nbsp;</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtTheProposal" Style="margin: 0; width: 96%" check="0" tip="限200个字符！" MaxLength="200" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                                        <td style="text-align: right">没有实现的愿望&nbsp;</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtAspirations" Style="margin: 0; width: 96%" check="0" tip="限200个字符！" MaxLength="200" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">其他信息&nbsp;</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtCustomerFlowContent" Style="margin: 0; width: 96%" check="0" tip="限200个字符！" MaxLength="200" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                                        <td style="text-align: right">观摩他人婚礼感受&nbsp;</td>
                                        <td colspan="3">
                                            <asp:TextBox ID="txtWatchingExperience" Style="margin: 0; width: 96%" check="0" tip="限200个字符！" MaxLength="200" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">燃烧点&nbsp;</td>
                                        <td colspan="3">
                                            <asp:TextBox TextMode="MultiLine" Style="margin: 0; width: 96%" check="0" tip="限200个字符！" MaxLength="200" ID="txtFirePoint" runat="server"></asp:TextBox></td>
                                        <td></td>
                                        <td colspan="3"></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div id="divonlyviewhide" runat="server" class="panel panel-default">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                <a id="acollapseThree" data-toggle="collapse" data-parent="#accordion" href="#">沟通记录</a>
                            </h4>
                        </div>
                        <div id="collapseThree" class="panel-collapse collapse in">
                            <div class="panel-body">
                                <table style="font-family: sans-serif">
                                    <tr class="State">
                                        <td style="text-align: right;">沟通后的状态&nbsp;</td>
                                        <td>
                                            <asp:RadioButtonList Style="margin: 0" CellPadding="16" ID="rdoStateList" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="202" Selected="True">二选一</asp:ListItem>
                                                <asp:ListItem Value="203">多选一</asp:ListItem>
                                                <asp:ListItem Value="10">流失</asp:ListItem>
                                                <asp:ListItem Value="204">确定</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:TextBox Enabled="false" ID="txtStateName" Style="margin: 0" runat="server" Text="优选" ClientIDMode="Static"></asp:TextBox>&nbsp;
                                            <input id="Button3" class="btn btn-mini btn-info" type="button" value="获取参考方法" onclick="return ShowRef();" />&nbsp;
                                            <input id="Button4" class="btn btn-mini btn-info" type="button" value="查看上级辅导意见" onclick="return ShowOrderMessAge();" /></td>
                                    </tr>
                                    <%--<tr class="NoLoss">
                                        <td style="text-align: right">本次沟通时间&nbsp;</td>
                                        <td>
                                            <asp:TextBox Style="margin: 0" onclick='WdatePicker({dateFmt:"yyyy/M/d H:mm:ss"});' check="1" tip="本次沟通时间不能为空！" ID="txtLastFollowDate" runat="server" ClientIDMode="Static"></asp:TextBox><span style="color: red">*</span></td>
                                    </tr>--%>
                                    <tr class="NoLoss">
                                        <td style="text-align: right">本次沟通记录&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtFlowContent" Style="margin: 0" tip="不能超过200个字符，只能在200个字符以内！" runat="server" TextMode="MultiLine" Rows="2" Width="96%" ClientIDMode="Static" MaxLength="200" /></td>
                                    </tr>
                                    <tr class="NoLoss SucessTd">
                                        <td style="text-align: right;">提案时间&nbsp;</td>
                                        <td>
                                            <asp:TextBox Style="margin: 0" onclick='WdatePicker({dateFmt:"yyyy/M/d H:mm:ss"});' ID="txtAgainDate" runat="server" ClientIDMode="Static"></asp:TextBox></td>
                                    </tr>
                                    <tr class="NoLoss NextSucessTd">
                                        <td style="text-align: right;">下次沟通时间&nbsp;</td>
                                        <td>
                                            <asp:TextBox Style="margin: 0" onclick='WdatePicker({dateFmt:"yyyy/M/d H:mm:ss"});' ID="txtNextFollowDate" runat="server" ClientIDMode="Static"></asp:TextBox></td>
                                    </tr>
                                    <tr class="Sure">
                                        <td style="text-align: right" class="Sure">选择策划师&nbsp;</td>
                                        <td id="<%=Guid.NewGuid().ToString() %>">
                                            <asp:TextBox ID="txtEmpLoyee" MaxLength="20" Style="margin: 0" class="txtEmpLoyeeName" ClientIDMode="Static" runat="server" onclick="ShowPopu(this);"></asp:TextBox>
                                            <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='<%#GetQuotedEmpLoyeeID(Eval("OrderID")) %>' runat="server" />
                                            &nbsp;
                                            <a href="#" onclick="ShowPopu(this);" class="btn btn-primary">选择策划师</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table id="tbl_QuotedCollection" class="table_Collection" style="width: 100%;">
                                                <tr>
                                                    <td class="td_Collection">选择类别</td>
                                                    <td>
                                                        <asp:RadioButtonList runat="server" ID="rdoTypes" ClientIDMode="Static" RepeatDirection="Horizontal" CellPadding="15" CellSpacing="15">
                                                            <asp:ListItem Text="定制" Value="1" Selected="True" />
                                                            <asp:ListItem Text="套餐" Value="2" />
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr class="tr_Package">
                                                    <td class="td_Collection">套系名称</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPackgeName" runat="server" Style="width: auto;" class="NoAdd">
                                                        </asp:DropDownList>
                                                        <asp:TextBox runat="server" ID="txtPackageName" ClientIDMode="Static" Style="display: none;"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="td_Collection">定金金额</td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtRealityAmount" ClientIDMode="Static" /></td>
                                                </tr>
                                                <tr>
                                                    <td class="td_Collection">收款方式</td>
                                                    <td>
                                                        <asp:RadioButtonList ID="rdoMoneytypes" runat="server" RepeatDirection="Horizontal" CellPadding="10" CellSpacing="10">
                                                            <asp:ListItem Text="现金" Value="1" Selected="True">现金</asp:ListItem>
                                                            <asp:ListItem Text="刷卡" Value="2">刷卡</asp:ListItem>
                                                            <asp:ListItem Text="转账" Value="3">转账</asp:ListItem>
                                                            <asp:ListItem Text="其它" Value="4">其它</asp:ListItem>
                                                        </asp:RadioButtonList></td>
                                                </tr>
                                                <tr class="tr_Collection">
                                                    <td class="td_Collection">收款说明</td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtNode" Style="margin: 0" tip="不能超过200个字符，只能在200个字符以内！" TextMode="MultiLine" Rows="2" Width="96%" ClientIDMode="Static" MaxLength="200" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr class="lose" style="display: none;">
                                        <td style="text-align: right">选择流失原因&nbsp;</td>
                                        <td>
                                            <cc2:ddlLoseContent Style="margin: 0; width: 134px" ID="ddlLoseContent" runat="server" ClientIDMode="Static"></cc2:ddlLoseContent>
                                        </td>
                                    </tr>
                                    <tr class="lose">
                                        <td style="text-align: right">流失原因说明&nbsp;</td>
                                        <td style="text-align: left">
                                            <asp:TextBox Style="margin: 0" ID="txtLoseContent" runat="server" TextMode="MultiLine" Width="97%" ClientIDMode="Static" MaxLength="200"></asp:TextBox></td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td style="text-align: right">输入定金&nbsp;</td>
                                        <td>
                                            <asp:TextBox ID="txtMoney" Style="margin: 0" runat="server" ClientIDMode="Static"></asp:TextBox>
                                            <asp:Button ID="btnSaveMoney" runat="server" CssClass="btn btn-success" Text="确定" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;"></td>
                                        <td>
                                            <asp:Button ID="btnSaveChange" CssClass="btn btn-success" runat="server" Text="保存" OnClientClick="return checksave(this);" OnClick="btnSaveChange_Click" />
                                            <asp:Button ID="btnStar" Visible="false" runat="server" CssClass="btn" Text="确认到店" OnClick="btnStar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <script type="text/ecmascript">
                        $(document).ready(function () {
                            $(".a_history").click(function () {
                                $("#divInviteContent").slideToggle("slow");
                                $("#divQuotedPanel").slideToggle("slow");
                            });
                        });
                    </script>

                    <div class="panel panel-default" style="float: left;">

                        <a class="a_history" style="cursor: pointer;">
                            <h4>历史沟通记录</h4>
                        </a>

                        <div id="divInviteContent">
                            <table style="font-family: sans-serif;">
                                <tr>
                                    <td class="alert alert-success" style="width: 625px;"><b>历史沟通记录</b></td>
                                </tr>
                                <asp:Repeater ID="repContenList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="2" style="text-align: left">
                                                <%#GetShortDateString(Eval("CommunicationTime").ToString()) %>&nbsp;&nbsp;&nbsp;
                                    第<font style="color: red;"><%#Container.ItemIndex+1%></font>次沟通&nbsp;&nbsp;&nbsp;
                                    沟通后状态：<%#Eval("State") %>&nbsp;&nbsp;&nbsp;
                                    本次邀约人：<%#GetEmployeeName(Eval("EmployeeID").ToString()) %>&nbsp;&nbsp;&nbsp;
                                    下次沟通时间:<%#Eval("NextPlanDate") == null ? "无" : Eval("NextPlanDate").ToString().ToDateTime().ToShortDateString() %><p><%#Eval("CommunicationContent") %></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>----------------------------------------------------------------------------------------------</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="repFollowMessage" runat="server" OnItemDataBound="repFollowMessage_ItemDataBound">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:HiddenField runat="server" ID="HideDetailsID" Value='<%#Eval("DetailID") %>' />
                                                <%#GetShortDateString(Eval("FollowDate")) %>&nbsp;&nbsp;&nbsp;
                                                第<font style="color: red;"><%#Container.ItemIndex+1%></font>次沟通&nbsp;&nbsp;&nbsp;
                                                沟通后状态：　<%#Eval("StateName") %>&nbsp;&nbsp;&nbsp;
                                                婚礼顾问：<%#GetEmployeeName(Eval("CreateEmpLoyee")) %>&nbsp;&nbsp;&nbsp;
                                                <asp:Label runat="server" ID="lblOrderTimes" />:<%#Eval("NextPlanDate") == null ? "无" : Eval("NextPlanDate").ToString().ToDateTime().ToShortDateString() %><p /><%#Eval("FollowContent") %></td>
                                        </tr>
                                        <tr>
                                            <td>------------------------------------------------------------------------------------------------</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Repeater ID="RepQuoteContentList" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="2" style="text-align: left">
                                                <%#GetShortDateString(Eval("QuotedCreateDate").ToString()) %>&nbsp;&nbsp;&nbsp;
                                    第<font style="color: red;"><%#Container.ItemIndex+1%></font>次沟通&nbsp;&nbsp;&nbsp;
                                    沟通后状态：<%#GetCustomerState(Eval("CustomerID")) %>&nbsp;&nbsp;&nbsp;
                                    本次邀约人：<%#GetEmployeeName(Eval("CreateEmployee").ToString()) %>&nbsp;&nbsp;&nbsp;
                                    下次沟通时间:<%#Eval("NextFollowDate") == null ? "无" : Eval("NextFollowDate").ToString().ToDateTime().ToShortDateString() %><p><%#Eval("QuotedContent") %></p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>----------------------------------------------------------------------------------------------</td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr style="display: none">
                                    <td style="text-align: center">
                                        <cc1:AspNetPagerTool ID="AspNetPagerTool1" runat="server" PageSize="3" AlwaysShow="true"></cc1:AspNetPagerTool>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div runat="server" id="divQuotedPanel" class="div_Panel">
                        <br />
                        <br />

                        <table style="font-family: sans-serif; width: 40%;">
                            <tr>
                                <td class="alert alert-success">
                                    <b>策划报价沟通记录</b>
                                </td>
                            </tr>
                            <tr>
                                <td>本次沟通记录：<asp:TextBox runat="server" ID="txtQuotedContent" ClientIDMode="Static" TextMode="MultiLine" Height="80px" Width="320px" />
                                </td>
                            </tr>
                            <tr>
                                <td>下次沟通时间：<asp:TextBox runat="server" ID="txtNextFollowDates" ClientIDMode="Static" TextMode="MultiLine" Height="20px" onclick="WdatePicker();" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button runat="server" ID="btnSaveConfirm" ClientIDMode="Static" Text="保存" CssClass="btn btn-primary" OnClientClick="OnCheckSave()" OnClick="btnSaveConfirm_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <script type="text/javascript">
                        $(document).ready(function () {
                            $("#btnSaveConfirm").click(function () {
                                if ($("#txtQuotedContent").val() == "") {
                                    alert("请输入本次沟通记录");
                                    return false;
                                } else if ($("#txtNextFollowDates").val() == "") {
                                    alert("请选择下次沟通时间");
                                    return false;
                                }
                            });
                        });
                    </script>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
