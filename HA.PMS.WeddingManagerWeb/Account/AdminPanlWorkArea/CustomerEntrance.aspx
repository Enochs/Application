<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CustomerEntrance.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CustomerEntrance" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<asp:Content ContentPlaceHolderID="head" runat="server" ID="Content2">
    <link href="/App_Themes/Default/css/bootstrap.min.css" rel="stylesheet" />
    <script src="/App_Themes/Default/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $(window).load(function () {
            BindString(2, 6, '<%=txtCustomerName.ClientID%>');
            BindTel('<%=txtGroomtelPhone.ClientID%>');
            BindEmail('<%=txtGroomEmail.ClientID%>');
            BindQQ('<%=txtGroomQQ.ClientID%>');
            BindString(20, '<%=txtGroomWeibo.ClientID%>');
            BindString(20, '<%=txtGroomteWeixin.ClientID%>');
            BindDate('<%=txtGroomBirthday.ClientID%>');
            BindText(200, '<%=txtOther.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
            $(".CustomerType").hide();
            $(".yaosu").hide();
            $(".Emoney").hide();
            $(".MessageDiv").hide();


            $($("#hidePageState").val()).show();

            $(".Upload").each(function () {
                showPopuWindows($(this).attr("href"), 700, 300, $(this));
            });


            if ($("#ddlState").find('option:selected').text() == "未邀约") {
                $(".panel-heading").hide();
            }
            if ($("#ddlState").find('option:selected').text() == "电话邀约") {
                $(".panel-heading").hide();
            }


            $("#ddlState").change(function () {
                if ($(this).find('option:selected').text() == "未邀约") {
                    $(".panel-heading").hide();
                    $("#btnSaveAll").hide();

                }

                if ($(this).find('option:selected').text() == "电话邀约") {
                    $(".panel-heading").hide();
                }

                if ($(this).find('option:selected').text() == "确认到店") {
                    $(".panel-heading").show();
                }
            });

        });


        function checksave(ctrl) {
            $("#hiddIsSucess").attr("value", "1");
            return checksavepage();
        }


        ///选择跟单责任人(婚礼顾问)
        function ShowQuptedPopu(Parent, HideControl) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=" + HideControl + "&ParentControl=" + $(Parent).parent().attr("id") + "&ClassType=QuotedPriceWorkPanel&ALL=1";
            showPopuWindows(Url, 450, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
        ///判断
        function checksavepage() {
            if ($("#ddlState").find('option:selected').text() == "请选择") {
                $("#ddlState").focus();
                alert("请选择客户状态");
                return false;
            }

            if ($("#ddlChannelType1").find('option:selected').text() == "无") {

                alert("请选择渠道类型");
                $("#DateEditTextBox1").focus();
                return false;

            }

            if ($("#ddlChannelName2").find('option:selected').text() == "请选择") {

                alert("请选择渠道名称");
                $("#DateEditTextBox1").focus();
                return false;

            }


        }
    </script>

    <style type="text/css">
        .auto-style1 {
            height: 24px;
        }

        .auto-style2 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <asp:HiddenField ID="hiddIsSucess" runat="server" ClientIDMode="Static" Value="0" />
        <asp:HiddenField ID="hidePageState" runat="server" ClientIDMode="Static" />
        <div class="widget-box">
            <div class="widget-content">
                <div class="panel-group" id="accordion">
                    <h4 class="panel-title">
                        <a id="a4" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">客户状态</a>
                        <asp:DropDownList ID="ddlState" runat="server" ClientIDMode="Static">

                            <asp:ListItem>未邀约</asp:ListItem>
                            <asp:ListItem>电话邀约</asp:ListItem>
                            <asp:ListItem>确认到店</asp:ListItem>
                        </asp:DropDownList>
                    </h4>

                    <div class="panel panel-default">


                        <div id="Div6" class="panel-collapse collapse in">
                            <div class="panel-body">

                                <table style="font-family: sans-serif; width: 100%;" border="1" class="table table-bordered table-striped">

                                    <tr>
                                        <td style="width: 15%">联系人</td>
                                        <td>
                                            <asp:TextBox ID="txtCustomerName" runat="server"></asp:TextBox>
                                        </td>
                                        <td>电话</td>
                                        <td>
                                            <asp:TextBox ID="txtCustomerPhone" runat="server"></asp:TextBox>
                                        </td>
                                        <td>身份</td>
                                        <td>
                                            <asp:RadioButtonList ID="rdoCustomertype" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True">新娘</asp:ListItem>
                                                <asp:ListItem>新郎</asp:ListItem>
                                                <asp:ListItem>经办人</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>婚期</td>
                                        <td>
                                            <asp:TextBox ID="txtCustomerPartydate" runat="server" check="0" MaxLength="20" onclick="WdatePicker();" Style="margin: 0" tip="结婚日期" />
                                        </td>
                                        <td>时段</td>
                                        <td>
                                            <asp:RadioButtonList ID="rdotimerSpan" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True">中午</asp:ListItem>
                                                <asp:ListItem>晚上</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                        <td>酒店</td>
                                        <td>
                                            <cc2:ddlHotel ID="ddlHotel1" runat="server">
                                            </cc2:ddlHotel>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>渠道类型</td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <cc2:ddlChannelType CssClass="ddlChannel" ID="ddlChannelType1" ClientIDMode="Static" runat="server" Width="120px" AutoPostBack="True"  OnSelectedIndexChanged="ddlChannelType1_SelectedIndexChanged">
                                                    </cc2:ddlChannelType>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>渠道名称</td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <cc2:ddlChannelName CssClass="ddlChannel" ID="ddlChannelName2" ClientIDMode="Static" runat="server" Width="120px" AutoPostBack="true" OnSelectedIndexChanged="ddlChannelName2_SelectedIndexChanged">
                                                    </cc2:ddlChannelName>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                        <td>推荐人</td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <cc2:ddlReferee ID="ddlReferee1" ClientIDMode="Static" runat="server">
                                                    </cc2:ddlReferee>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <input id="txtRef" type="text" runat="server" visible="false" /></td>
                                    </tr>


                                    <tr>
                                        <td>备注</td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtOther" runat="server" check="0" Rows="1" Style="margin: 0; width: 98%" TextMode="MultiLine" tip="限200个字符"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr runat="server" visible="false">
                                        <td>返点说明</td>
                                        <td colspan="5">
                                            <asp:TextBox ID="txtRebates" runat="server" check="0" Rows="1" Style="margin: 0; width: 98%" TextMode="MultiLine" tip="填写返点比例"></asp:TextBox>
                                        </td>
                                    </tr>


                                </table>
                            </div>
                        </div>
                        <div id="CustomerSys" class="panel-collapse collapse in" style="display: none;">
                            <div class="panel-body">
                                <table style="font-family: sans-serif; width: 100%;" border="1" class="table table-bordered table-striped">
                                    <tr>
                                        <td style="text-align: right; width: 15%;">座机</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomtelPhone" Style="margin: 0" check="0" runat="server" tip="座机" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">生日</td>
                                        <td>
                                            <asp:TextBox onclick="WdatePicker();" ID="txtGroomBirthday" Style="margin: 0" check="0" runat="server" tip="生日" MaxLength="20" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">Email</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomEmail" Style="margin: 0" check="0" runat="server" tip="格式：example@mail.com" MaxLength="25"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" class="auto-style1">微信</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomteWeixin" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="text-align: right" class="auto-style2">微博</td>
                                        <td class="auto-style2">
                                            <asp:TextBox ID="txtGroomWeibo" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">QQ</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomQQ" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">职务 </td>
                                        <td>
                                            <asp:TextBox ID="txtGroomJob" Style="margin: 0" runat="server" tip="职业" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>工作单位</td>
                                        <td>
                                            <asp:TextBox ID="txtGroomJobCompany" Style="margin: 0" runat="server" tip="工作单位" MaxLength="20"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>时段</td>
                                        <td>
                                            <asp:TextBox ID="txtTimerSpan" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>备注说明</td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

                <hr style="width: 100%; border-color: deepskyblue;" />
                <div class="panel-heading" style="white-space: nowrap;" id="<%=Guid.NewGuid().ToString() %>">
                    <a href="#" name="SelectEmpLoyeeBythis" id="SelectEmpLoyeeBythis" style="color: #584e4e">选择</a>
                    <asp:TextBox ID="txtOrderMessage" MaxLength="20" Style="margin: 0" class="txtEmpLoyeeName" ClientIDMode="Static" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hideOrderEmployee" ClientIDMode="Static" runat="server" />
                    <a href="#SelectEmpLoyeeBythis" onclick="ShowQuptedPopu(this,'hideOrderEmployee');" class="btn btn-primary">跟单责任人</a>
                    (如若不选，默认就是自己)
                </div>
            </div>
            <div id="Div7" class="panel-collapse collapse in">
                <div class="panel-body" style="height: 50px;">
                    <asp:Button ID="btnSaveChange" runat="server" CssClass="btn btn-primary" Text="保存客户信息" OnClick="btnSaveChange_Click" OnClientClick="return checksavepage(this);" />
                    <asp:Button ID="btnSaveAll" runat="server" CssClass="btn btn-primary" Text="保存并录入沟通记录" OnClick="btnSaveChange_Click" OnClientClick="return checksave(this);" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
