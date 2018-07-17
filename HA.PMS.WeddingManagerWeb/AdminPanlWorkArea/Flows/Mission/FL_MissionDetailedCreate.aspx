<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionDetailedCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionDetailedCreate" Title="任务计划详情添加" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">


    <script type="text/javascript">
        $(function () {
            //YearofYear半年
            //jidu 季度
            //Month
            //TimerSpan
            //页面初始化 默认是周计划
            $(".TimerSpan").hide();
            $(".NeedHide").hide();
            $(".TimerSpan").hide();

            $(".PostSubmit").show();
            $("input[type='radio']").click(function () {
                var RadioValue = $("input[type='radio']:checked").val();
                RadioValue = parseInt(RadioValue);
                switch (RadioValue) {
                    case 60:
                        $("#txtMissiontitle").attr("value", $("#hideEmployeeName").val() + "临时任务");
                        $(".PostCheck").hide();
                        $(".NeedHide").hide();
                        $(".TimerSpan").hide();
                        $(".PostSubmit").show();
                        //$(".TimerSpan").show();
                        break;
                    case 61:
                        $("#txtMissiontitle").attr("value", $("#hideEmployeeName").val() + "周计划");
                        $(".PostCheck").show();
                        $(".NeedHide").hide();
                        $(".TimerSpan").show();
                        break;
                    case 62:

                        $("#txtMissiontitle").attr("value", $("#hideEmployeeName").val() + "月计划");
                        $(".PostCheck").show();
                        $(".NeedHide").hide();
                        $(".Month").show();
                        break;
                    case 63:
                        $("#txtMissiontitle").attr("value", $("#hideEmployeeName").val() + "季度计划");
                        $(".PostCheck").show();
                        $(".NeedHide").hide();
                        $(".jidu").show();
                        break;
                    case 64:
                        $("#txtMissiontitle").attr("value", $("#hideEmployeeName").val() + "年计划");
                        $(".NeedHide").hide();
                        $(".PostCheck").show();
                        break;
                    case 65:
                        $("#txtMissiontitle").attr("value", $("#hideEmployeeName").val() + "半年计划");
                        $(".PostCheck").show();
                        $(".NeedHide").hide();
                        $(".TimerSpan").hide();
                        $(".niandu").show();
                        break;
                    default:

                        break;
                }


            });
        });

        $(document).ready(function () {

            $(".PostCheck").click(function () {
                $(this).hide();
            });

            if (getQueryString('Action') == "Check") {
                $(".btnCheck").show();

            } else {
                $(".btnCheck").hide();
            }
            $("html,body").css({ "background-color": "transparent" });
        });


        function PostChecks() {
            return confirm("你确认已经编制好了需要提交审核么！");
        }
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

        //上传图片
        function ShowFileUploadPopu(DetailedID,MissionID) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPricefileUpload.aspx?DetailedID=" + DetailedID + "&MissionID=" + MissionID + "&Kind=0&FinishType=2";
            showPopuWindows(Url, 800, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FL_MissionUpdate.aspx?DetailedID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 500, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            showPopuWindows($("#createMission").attr("href"), 700, 500, "a#createMission");
            $(".SGEmpLoyee").hide();
            if ($("#hideIsmanager").val() == "1") {
                $(".SGEmpLoyee").show();
            }
        });
        function ShowPopu(Parent) {
            if ($(":radio:checked").val() == "60" && "<%=IsMissionPackDispatchOpening%>".toLowerCase() == "true") {
                var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeesBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ClassType=QuotedPriceWorkPanel&ALL=1";
                showPopuWindows(Url, 450, 700, "#SelectEmpLoyeeBythis");
                $("#SelectEmpLoyeeBythis").click();
            }
            else {
                var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ClassType=QuotedPriceWorkPanel&ALL=1";
                showPopuWindows(Url, 450, 700, "#SelectEmpLoyeeBythis");
                $("#SelectEmpLoyeeBythis").click();
            }
        }

        function CheckPage() {
            return ValidateForm('input[check],textarea[check]');
        }

        $(window).load(function () {
            BindString(10, '<%=txtEmpLoyee.ClientID%>');
            BindString(50, '<%=txtMissionName.ClientID%>:<%=txtFinishStandard.ClientID%>');
            BindString(20, '<%=txtMissiontitle.ClientID%>');
            BindDate('<%=txtStarDate.ClientID%>:<%=txtFinishDate.ClientID%>');
            BindText(200, '<%=txtWorkNode.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
            $('#<%=txtStarDate.ClientID%>').change(function () { $(this).blur(); });
            $('#<%=txtFinishDate.ClientID%>').change(function () { $(this).blur(); });
        });
    </script>
    <style type="text/css">
        #ContentPlaceHolder1_rdoMissionList {
            height: 35px;
        }

        .bolders {
            font-weight: bolder;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <asp:HiddenField ID="hideIsmanager" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hideEmployeeName" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hideMisioniID" runat="server" />


    <div class="widget-box" id="bodyDiv">
        <div class="widget-content " style="overflow-x: auto;">
            <table class="table table-bordered table-striped with-check table-select" style="border: 1px  solid #eae2e2;">
                <thead>
                    <tr style="height: 55px;">
                        <td style="white-space: nowrap;" class="bolders">任务/计划类型</td>
                        <td colspan="9" style="white-space: nowrap;">
                            <asp:RadioButtonList ID="rdoMissionList" runat="server" RepeatDirection="Horizontal" BorderStyle="None">
                                <asp:ListItem Value="60" Selected="True">临时任务</asp:ListItem>
                                <asp:ListItem Value="61">周计划</asp:ListItem>
                                <asp:ListItem Value="62">月计划</asp:ListItem>
                                <asp:ListItem Value="63">季度计划</asp:ListItem>
                                <asp:ListItem Value="65">半年计划</asp:ListItem>
                                <asp:ListItem Value="64">年计划</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="TimerSpan NeedHide">
                        <td style="white-space: nowrap; text-align: right;" class="bolders">时间段</td>
                        <td colspan="9" style="white-space: nowrap;">
                            <cc1:DateEditTextBox ID="txttimerStar" onclick='WdatePicker({dateFmt:"yyyy/M/d HH:m"})' Width="130" runat="server"></cc1:DateEditTextBox>&nbsp;至&nbsp;<cc1:DateEditTextBox ID="txttimerEnd" onclick='WdatePicker({dateFmt:"yyyy/M/d HH:m"})' Width="130" runat="server"></cc1:DateEditTextBox>
                        </td>

                    </tr>
                    <tr style="display: none;" class="Month NeedHide">
                        <td style="white-space: nowrap; text-align: right; font-weight: bolder;">第</td>
                        <td colspan="3" style="white-space: nowrap;">
                            <asp:DropDownList ID="ddlMonth" Width="53" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                                <asp:ListItem>8</asp:ListItem>
                                <asp:ListItem>9</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                            </asp:DropDownList>月</td>
                        <td colspan="3" style="white-space: nowrap;">&nbsp;</td>
                        <td colspan="3">&nbsp;</td>
                    </tr>

                    <tr style="display: none;" class="jidu NeedHide">
                        <td style="white-space: nowrap; display: none;">第</td>
                        <td colspan="3" style="white-space: nowrap;">
                            <asp:DropDownList Width="53" ID="ddljidu" runat="server">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>季度
                        </td>

                        <td style="white-space: nowrap;" colspan="6"></td>

                    </tr>
                    <tr style="display: none;" class="niandu NeedHide">
                        <td style="text-align: right;" class="bolders">半年</td>
                        <td colspan="3" style="white-space: nowrap;">
                            <asp:DropDownList ID="ddlniandu" Width="80" runat="server">
                                <asp:ListItem>上半年</asp:ListItem>
                                <asp:ListItem>下半年</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="white-space: nowrap;" colspan="6"></td>
                    </tr>
                    <tr>
                        <td style="white-space: nowrap;">任务/计划名称</td>
                        <td colspan="9" style="text-align: left;">
                            <asp:TextBox ID="txtMissiontitle" runat="server" tip="限20个字符！" check="1" Width="90%" ClientIDMode="Static" CssClass="{required:true}"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th style="white-space: nowrap;">任务名称</th>
                        <th style="white-space: nowrap;">工作内容</th>
                        <th style="white-space: nowrap;">完成标准</th>
                        <th style="white-space: nowrap;">附件</th>
                        <th style="white-space: nowrap;">计划开始时间</th>
                        <th style="white-space: nowrap;">计完成时间</th>
                        <th style="white-space: nowrap;">倒计时</th>
                        <th style="white-space: nowrap;">紧急程度</th>
                        <th style="white-space: nowrap;" width="65">责任人</th>
                        <th style="white-space: nowrap; <%= ViewState["style"]%>">操作</th>
                    </tr>

                    <tr>

                        <td>
                            <asp:TextBox ID="txtMissionName" tip="限50个字符！（不能包含回车符）" check="1" MaxLength="50" runat="server" Width="75px" CssClass="NeedCheck" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtWorkNode" TextMode="MultiLine" tip="限200个字符！" MaxLength="200" check="1" runat="server" Width="100" CssClass="NeedCheck"></asp:TextBox>
                        </td>

                        <td>
                            <asp:TextBox ID="txtFinishStandard" TextMode="MultiLine" tip="限50个字符！（不能包含回车符）" check="1" MaxLength="50" runat="server" Width="100" CssClass="NeedCheck"></asp:TextBox></td>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtStarDate" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" check="1" MaxLength="20" runat="server" CssClass="DateTimeTxt" Width="75px" ClientIDMode="Static"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtFinishDate" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" check="1" MaxLength="20" runat="server" CssClass="DateTimeTxt" Width="75px" ClientIDMode="Static"></asp:TextBox></td>
                        <td>

                            <asp:DropDownList ID="ddlCotown" runat="server" Width="50">
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                                <asp:ListItem>5</asp:ListItem>
                                <asp:ListItem>6</asp:ListItem>
                                <asp:ListItem>7</asp:ListItem>
                            </asp:DropDownList>
                            天</td>
                        <td>
                            <asp:DropDownList ID="ddlEmergency" runat="server" Width="125px">
                                <asp:ListItem>重要且紧急</asp:ListItem>
                                <asp:ListItem>重要不紧急</asp:ListItem>
                                <asp:ListItem>紧急不重要</asp:ListItem>
                                <asp:ListItem>不重要不紧急</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td id="<%=Guid.NewGuid().ToString() %>" style="white-space: nowrap;">
                            <input runat="server" id="txtEmpLoyee" maxlength="10" check="1" style="width: 65px;" onmouseover="this.title=this.value" class="txtEmpLoyeeName" type="text" /><br />
                            <a href="#" onclick="ShowPopu(this);" class="SGEmpLoyee btn btn-primary btn-mini">选择</a>
                            <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" runat="server" />

                        </td>
                        <td>
                            <asp:Button ID="btnCreate" runat="server" Text="保存" OnClick="btnAdd_Click" CssClass="btn btn-info" OnClientClick="return CheckPage(this);" />
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptMission" OnItemCommand="rptMission_ItemCommand" runat="server">
                        <ItemTemplate>
                            <tr skey='<%#Eval("DetailedID") %>'>
                                <td>
                                    <asp:TextBox ID="txtMissionName" MaxLength="20" CssClass="{required:true}" runat="server" Text='<%#Eval("MissionName") %>' Width="75px"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtWorkNode" MaxLength="200" runat="server" CssClass="{required:true}" Text='<%#Eval("WorkNode") %>' Width="180"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtFinishStandard" MaxLength="50" runat="server" CssClass="{required:true}" Text='<%#Eval("FinishStandard") %>' Width="180"></asp:TextBox></td>
                                <td style="white-space: nowrap;">
                                    <a href="#" class="btn btn-primary btn-mini" onclick="ShowFileUploadPopu(<%#Eval("DetailedID") %>,<%#Eval("MissionID") %>);">上传</a>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStarDate" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" runat="server" CssClass="DateTimeTxt" Text='<%#Eval("StarDate","{0:yyyy-MM-dd HH:mm:ss}") %>' Width="75px"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtFinishDate" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" runat="server" CssClass="DateTimeTxt" Text='<%#Eval("PlanDate","{0:yyyy-MM-dd HH:mm:ss}") %>' Width="75px"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtCountdown" runat="server" CssClass="{digits:true}" Text='<%#Eval("Countdown") %>' Width="48px" Enabled="false"></asp:TextBox>天</td>
                                <td>
                                    <asp:TextBox ID="txtEmergency" Enabled="false" runat="server" CssClass="{required:true}" Text='<%#Eval("Emergency") %>' Width="75px"></asp:TextBox></td>
                                <td id="<%#Guid.NewGuid().ToString() %>">
                                    <input runat="server" id="txtEmpLoyee" style="width: 80px;" class="txtEmpLoyeeName" value='<%#GetMissionEmployeeNames(Eval("DetailedID")) %>' title='<%#GetMissionEmployeeNames(Eval("DetailedID")) %>' type="text" /><br />
                                    <a href="#" onclick="ShowPopu(this);" class="SGEmpLoyee btn btn-primary btn-mini">选择</a>
                                    <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" runat="server" Value='<%#GetMissionEmployeeIDs(Eval("DetailedID")) %>' />
                                    <asp:HiddenField ID="hideMissionID" runat="server" Value='<%#Eval("MissionID") %>' />
                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbtnDelete" CssClass="btn btn-danger btn-mini" CommandArgument='<%#Eval("DetailedID") %>' CommandName="Delete" runat="server">删除</asp:LinkButton>
                                    <asp:LinkButton ID="lnkbtnSaveChange" CssClass="btn btn-success  btn-mini" CommandArgument='<%#Eval("DetailedID") %>' runat="server" CommandName="Change">保存编辑</asp:LinkButton>
                                    <asp:LinkButton ID="lkbtnSaveChecks" CssClass="btn btn-success  btn-mini btnCheck" CommandArgument='<%#Eval("DetailedID") %>' runat="server" CommandName="Checks">保存变更</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <th colspan="10">
                            <cc1:ClickOnceButton ID="btnSavetoChecks" runat="server" Text="提交审核" OnClick="btnSavetoChecks_Click" OnClientClick="return PostChecks();" CssClass="btn btn-success PostCheck" />

                            <asp:Button ID="btnSavetable" runat="server" Text="保存" OnClick="btnSavetable_Click" CssClass="btn btn-success PostCheck PostSubmit" />
                        </th>
                    </tr>
                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
