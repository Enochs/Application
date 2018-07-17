<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InviteCommunicationContent.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer.InviteCommunicationContent" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <link href="/App_Themes/Default/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/App_Themes/Default/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function checksave() {
            if (!ValidateForm($('input[check]'))) {
                return false;
            }
            var SelectItem = $("#ddlInviteSate").find("option:selected").text();
            if (SelectItem == "邀约成功") {
                if ($("#hideDepartmetnKey").val() == "") {
                    // alert("请选择门店");
                    $("#hideDepartmetnKey").focus();
                    return false;
                }
                if ($("#txtComeDate").val() == "") {
                    // alert("请选择到店时间");
                    $("#txtComeDate").focus();
                    return false;
                }
            }
            if (SelectItem == "约定再沟通") {

                if ($("#txtCommunicationTime").val() == "") {
                    //alert("请输入本次沟通时间");
                    $("#txtCommunicationTime").focus();
                    return false;
                }
                if ($("#txtContent1").val() == "") {
                    //alert("请录入本次沟通记录");
                    $("#txtContent1").focus();
                    return false;
                }
                if ($("#txtPlandate").val() == "") {
                    //alert("请录入再次邀约时间");
                    $("#txtPlandate").focus();
                    return false;
                }
            }
            if (SelectItem == "流失") {
                var LoseContent = $("#ddlLoseContent1").find("option:selected").text();
                if (LoseContent == "请选择") {
                    //alert("请选择流失原因");
                    $("#ddlLoseContent1").focus();
                    return false;
                }
                if ($("#txtLoseContent").val() == "") {
                    // alert("请输入流失原因说明");
                    $("#txtLoseContent").focus();
                    return false;
                }
            }
        }
        $(document).ready(function () {
            $(".lose").hide();
            $(".Sucess").hide();
            if ($("#hiddIsSucess").val() == "1") {
                $("[type='text']").attr("disabled", "disabled");
                $("[type='button']").attr("disabled", "disabled");
                $("[type='text']").addClass("NoneBorder");
            }
            $("#ddlInviteSate").change(function () {
                var SelectItem = $("#ddlInviteSate").find("option:selected").text();
                if (SelectItem == "流失") {
                    $(".lose").show();
                    $(".SucessNone").hide();
                    $(".Sucess").hide();
                }
                if (SelectItem == "邀约成功") {
                    $(".lose").hide();
                    $(".SucessNone").hide();
                    $(".Sucess").show();
                }
                if (SelectItem == "无效信息") {
                    $(".lose").hide();
                    $(".SucessNone").hide();
                    $(".Sucess").hide();
                }
                if (SelectItem == "约定再沟通") {
                    $(".SucessNone").show();
                    $(".lose").hide();
                    $(".Sucess").hide();
                }
            });
            if ('<%=Request["OnlyView"]%>') {
                $("input,textarea,select").attr("disabled", "disabled");
                $("input[type=button],.btn").hide();
            }
        });

        $(window).load(function () {
            BindString(2, 6, '<%=txtGroom.ClientID%>:<%=txtBride.ClientID%>:<%=txtOperator.ClientID%>');
            BindMobile('<%=txtGroomCellPhone.ClientID%>:<%=txtBrideCellPhone.ClientID%>:<%=txtOperatorPhone.ClientID%>');
            BindEmail('<%=txtGroomEmail.ClientID%>:<%=txtBrideEmail.ClientID%>:<%=txtOperatorEmail.ClientID%>');
            BindQQ('<%=txtGroomQQ.ClientID%>:<%=txtBrideQQ.ClientID%>:<%=txtOperatorQQ.ClientID%>');
            BindString(20, '<%=txtGroomWeibo.ClientID%>:<%=txtBrideWeibo.ClientID%>:<%=txtOperatorWeibo.ClientID%>');
            BindString(20, '<%=txtGroomteWeixin.ClientID%>:<%=txtBrideWeiXin.ClientID%>:<%=txtOperatorWeiXin.ClientID%>');
            BindDate('<%=txtPartyDay.ClientID%>:<%=txtCommunicationTime.ClientID%>');
            BindText(200, '<%=txtOther.ClientID%>:<%=txtContent1.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSaveDate.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        //选择父级部门
        function SelectDepartmentPopu(Control) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Control).parent().attr("id") + "&ClassType=QuotedPriceWorkPanel&ALL=1";
            showPopuWindows(Url, 450, 700, "#SelectDepartmetnByThis");
            $("#SelectDepartmetnByThis").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <a href="#" id="SelectDepartmetnByThis" style="display: none;">选择部门</a>&nbsp;
    <asp:HiddenField ID="hiddIsSucess" runat="server" ClientIDMode="Static" />
    <div style="overflow:auto;height:900px">
        <table style="font-family: sans-serif">
            <tr>
                <td style="text-align: right">新&nbsp;&nbsp;郎&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtGroom" check="0" tip="限2~6个字符" MaxLength="8" runat="server"></asp:TextBox></td>
                <td style="text-align: right">新&nbsp;&nbsp;娘&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtBride" runat="server" tip="（必填）限2~6个字符" MaxLength="8" check="0" ClientIDMode="Static"></asp:TextBox></td>
                <td style="text-align: right">经办人&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtOperator" check="0" runat="server" tip="限2~6个字符" MaxLength="8"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">电&nbsp;&nbsp;话&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtGroomCellPhone" check="0" MaxLength="16" runat="server" tip="手机号码为11位数字"></asp:TextBox></td>
                <td style="text-align: right">电&nbsp;&nbsp;话&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtBrideCellPhone" MaxLength="16" runat="server" tip="（必填）手机号码为11位数字" check="0" ClientIDMode="Static"></asp:TextBox></td>
                <td style="text-align: right">电&nbsp;&nbsp;话&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtOperatorPhone" MaxLength="16" check="0" tip="手机号码为11位数字" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">E-Mail&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtGroomEmail" MaxLength="25" check="0" tip="格式：example@mail.com" runat="server"></asp:TextBox></td>
                <td style="text-align: right">E-Mail&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtBrideEmail" MaxLength="25" check="0" tip="格式：example@mail.com" runat="server"></asp:TextBox></td>
                <td style="text-align: right">E-Mail&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtOperatorEmail" MaxLength="25" check="0" tip="格式：example@mail.com" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">微&nbsp;&nbsp;信&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtGroomteWeixin" MaxLength="16" check="0" tip="微信" runat="server"></asp:TextBox></td>
                <td style="text-align: right">微&nbsp;&nbsp;信&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtBrideWeiXin" MaxLength="16" check="0" tip="微信" runat="server"></asp:TextBox></td>
                <td style="text-align: right">微&nbsp;&nbsp;信&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtOperatorWeiXin" MaxLength="16" check="0" tip="微信" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">微&nbsp;&nbsp;博&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtGroomWeibo" MaxLength="16" check="0" tip="微博" runat="server"></asp:TextBox></td>
                <td style="text-align: right">微&nbsp;&nbsp;博&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtBrideWeibo" MaxLength="16" check="0" tip="微博" runat="server"></asp:TextBox></td>
                <td style="text-align: right">微&nbsp;&nbsp;博&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtOperatorWeibo" MaxLength="16" check="0" tip="微博" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">Ｑ&nbsp;&nbsp;Ｑ&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtGroomQQ" MaxLength="16" check="0" tip="QQ号码为5~11位数字" runat="server"></asp:TextBox></td>
                <td style="text-align: right">Ｑ&nbsp;&nbsp;Ｑ&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtBrideQQ" MaxLength="16" check="0" tip="QQ号码为5~11位数字" runat="server"></asp:TextBox></td>
                <td style="text-align: right">Ｑ&nbsp;&nbsp;Ｑ&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtOperatorQQ" MaxLength="16" check="0" tip="QQ号码为5~11位数字" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">婚&nbsp;&nbsp;期&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="txtPartyDay" tip="举办婚礼的日期" MaxLength="16" check="0" runat="server" onclick="WdatePicker()"></asp:TextBox></td>
                <td style="text-align: right">时&nbsp;&nbsp;段&nbsp;</td>
                <td>
                    <asp:DropDownList Style="width: 174px; margin: 0" ID="ddlTimerSpan" runat="server">
                        <asp:ListItem>中午</asp:ListItem>
                        <asp:ListItem>晚上</asp:ListItem>
                    </asp:DropDownList></td>
                <td style="text-align: right">酒&nbsp;&nbsp;店&nbsp;</td>
                <td>
                    <cc1:ddlHotel Style="width: 174px; margin: 0" MaxLength="32" ID="ddlHotel" runat="server"></cc1:ddlHotel></td>
            </tr>
            <tr>
                <td style="text-align: right">推荐人&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="lblReffer" runat="server" Enabled="false"></asp:TextBox></td>
                <td style="text-align: right">来源渠道&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="lblChannel" runat="server" Enabled="false"></asp:TextBox></td>
                <td style="text-align: right">渠道类型&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="lblChannelType" runat="server" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: right">录入人&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="lblCreateEmpLoyee" runat="server" Enabled="false"></asp:TextBox></td>
                <td style="text-align: right">录入日期&nbsp;</td>
                <td>
                    <asp:TextBox Style="width: 160px; margin: 0" ID="lblCreateDate" runat="server" Enabled="false"></asp:TextBox></td>
                <td colspan="2"></td>
            </tr>
            <tr>
                <td style="text-align: right">说&nbsp;&nbsp;明&nbsp;</td>
                <td colspan="5">
                    <asp:TextBox Style="width: 98%; margin: 0" ID="txtOther" runat="server" check="0" TextMode="MultiLine" tip="限200个字符！" MaxLength="200"></asp:TextBox></td>
            </tr>
            <asp:PlaceHolder ID="plhonlyviewhide" runat="server">
                <tr>
                    <td colspan="6" class="alert alert-success"><b>沟通记录</b></td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table>
                            <tr>
                                <td style="text-align: right;"><span style="color: red; text-align: left;">*</span>本次沟通时间</td>
                                <td>
                                    <asp:TextBox Style="margin: 0; width: 160px" ID="txtCommunicationTime" onclick='WdatePicker({dateFmt:"yyyy/MM/dd HH:mm:ss"});' check="1" ClientIDMode="Static" CssClass="DateTimeTxt DataTextEditoer" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">沟通后状态</td>
                                <td>
                                    <asp:DropDownList ID="ddlInviteSate" Style="width: 174px; margin: 0" ClientIDMode="Static" runat="server">
                                        <asp:ListItem Value="5" Selected="True">约定再沟通</asp:ListItem>
                                        <asp:ListItem Value="6">邀约成功</asp:ListItem>
                                        <asp:ListItem Value="7">流失</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right"><span style="color: red;">*</span>本次沟通记录</td>
                                <td>
                                    <asp:TextBox ID="txtContent1" Style="width: 320px; margin: 0" check="1" tip="（必填）限200个字符！" runat="server" Rows="2" TextMode="MultiLine" ClientIDMode="Static" MaxLength="200" /></td>
                            </tr>
                            <tr class="SucessNone">
                                <td style="text-align: right;">再次沟通日期</td>
                                <td>
                                    <asp:TextBox ID="txtPlandate" Style="margin: 0; width: 160px" onclick='WdatePicker({dateFmt:"yyyy/M/d H:mm"});' ClientIDMode="Static" runat="server"></asp:TextBox></td>
                            </tr>
                            <tr class="lose">
                                <td style="text-align: right;">流失原因</td>
                                <td>
                                    <cc1:ddlLoseContent Style="width: 174px; margin: 0" ID="ddlLoseContent1" runat="server" ClientIDMode="Static"></cc1:ddlLoseContent></td>
                            </tr>
                            <tr class="lose">
                                <td style="text-align: right;">流失原因说明</td>
                                <td>
                                    <asp:TextBox Style="width: 320px; margin: 0" ID="txtLoseContent" ClientIDMode="Static" runat="server" TextMode="MultiLine" MaxLength="200"></asp:TextBox></td>
                            </tr>
                            <tr class="Sucess">
                                <td style="text-align: right;">选择婚礼顾问</td>
                                <td id="<%=Guid.NewGuid().ToString() %>">
                                    <asp:TextBox ID="txtEmpLoyee" MaxLength="20" Style="margin: 0" class="txtEmpLoyeeName" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='<%#GetQuotedEmpLoyeeID(Eval("OrderID")) %>' runat="server" />
                                    &nbsp;&nbsp;<a href="#" onclick="SelectDepartmentPopu(this);" class="btn btn-primary btn-mini SetState">选择</a> （如果后续销售也是你负责，则不必选择。）</td>
                            </tr>
                            <tr class="Sucess">
                                <td style="text-align: right;">约定到店日期</td>
                                <td>
                                    <asp:TextBox Style="margin: 0; width: 160px" ID="txtComeDate" onclick='WdatePicker({dateFmt:"yyyy/MM/dd HH:mm"});' runat="server" ClientIDMode="Static"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnSaveDate" runat="server" Text="保存" OnClick="btnSaveDate_Click" CssClass="btn btn-success" OnClientClick="return checksave();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </asp:PlaceHolder>
            <tr>
                <td colspan="6" class="alert alert-success"><b>历史沟通记录</b></td>
            </tr>
            <tr>
                <td colspan="6">
                    <%--<div style="overflow: auto;">--%>
                        <table style="font-family: sans-serif;">
                            <asp:Repeater ID="repContenList" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td colspan="2" style="text-align: left">
                                            <%#GetShortDateString(Eval("CommunicationTime").ToString()) %>&nbsp;&nbsp;&nbsp;
                                    第<font style="color: red;"><%#Container.ItemIndex+1%></font>次沟通&nbsp;&nbsp;&nbsp;
                                    沟通后状态：<%#Eval("State") %>&nbsp;&nbsp;&nbsp;
                                    本次邀约人：<%#GetEmployeeName(Eval("EmployeeID").ToString()) %>&nbsp;&nbsp;&nbsp;
                                    下次沟通时间:<%#Eval("NextPlanDate") == null ? "无" : Eval("NextPlanDate").ToString().ToDateTime().ToShortDateString() %>
                                            <p><%#Eval("CommunicationContent") %></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>----------------------------------------------------------------------------------------------</td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr style="display: none">
                                <td style="text-align: center">
                                    <cc2:AspNetPagerTool ID="CtrPageIndex" runat="server" PageSize="3" AlwaysShow="true" OnPageChanged="CtrPageIndex_PageChanged"></cc2:AspNetPagerTool>
                                </td>
                            </tr>
                        </table>
                    <%--</div>--%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
