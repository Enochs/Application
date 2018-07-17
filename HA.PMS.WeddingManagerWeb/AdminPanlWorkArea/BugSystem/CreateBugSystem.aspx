<%@ Page Title="" StylesheetTheme="Default" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CreateBugSystem.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.BugSystem.CreateBugSystem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //上传图片
        function ShowFileUploadPopu(BugID) {
            var Url = "/AdminPanlWorkArea/BugSystem/BugSystemUpLoad.aspx?BugID=" + BugID;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        $(document).ready(function () {

            showPopuWindows($("#createKnow").attr("href"), 310, 180, "a#createKnow");

            showPopuWindows($("#BrowseKnow").attr("href"), 1000, 700, "a#BrowseKnow");

            //获取默认状态  保存/确认完成的显示/隐藏
            if ($("#<%=rdoState.ClientID %>").find("input[type='radio']:checked").val() == 2) {
                $("#btnSaveFinish").css("display", "block");
                $("#btnFinish").css("display", "none");
            } else {
                $("#btnSaveFinish").css("display", "none");
                $("#btnFinish").css("display", "block");
            }

            //添加 bug保存
            $("#btnGetConfirm").click(function () {
                if ($("#txtTitle").val() == "") {
                    alert("请输入标题");
                    return false;
                }
                if ($("#txtContent").val() == "") {
                    alert("请填写说明");
                    return false;
                }

            });

            //技术人员建议
            $("#btnFinish").click(function () {
                if ($("#txtSuggest").val() == "") {
                    alert("请填写建议");
                    return false;
                }
            });

            //保存 / 确认完成切换


        });

        function show() {
            if ($("#<%=rdoState.ClientID %>").find("input[type='radio']:checked").val() == 2) {
                $("#btnSaveFinish").css("display", "block");
                $("#btnFinish").css("display", "none");
            } else {
                $("#btnSaveFinish").css("display", "none");
                $("#btnFinish").css("display", "block");
            }
        }

    </script>

    <style type="text/css">
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>

    <div class="div_Main">
        <div class="div_CreateModel" style="text-align: center;">
            <table class="table talbe-bordered" style="width: 55%; text-align: center; border: 1px solid #DDDDDD;">
                <tr>
                    <td style="text-align: center;" colspan="2">
                        <h3>
                            <asp:Label runat="server" ID="lblGetTitle" Text="Bug提交" /></h3>
                    </td>
                </tr>
                <tr>
                    <td width="100px">标题</td>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Width="220px" ClientIDMode="Static"></asp:TextBox>
                        <asp:Label ID="lblTitle" runat="server" Text="lblTitle" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>说明</td>
                    <td>
                        <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="220px" Height="120px" ClientIDMode="Static"></asp:TextBox>
                        <asp:Label ID="lblContent" runat="server" Text="lblContent" Width="320px" Style="white-space: normal;" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="tr_FileShow" visible="false">
                    <td colspan="2">
                        <table>
                            <tr>
                                <asp:Repeater runat="server" ID="repBugFile">
                                    <ItemTemplate>
                                        <td>
                                            <a target="_blank" href='<%#Eval("FileUrl") %>'>
                                                <img src="<%#Eval("FileUrl") %>" style="width: 150px; height: 100px;" /></a>
                                        </td>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="tr_Get">
                    <td colspan="2">
                        <asp:Button ID="btnGetConfirm" runat="server" Text="提交" CssClass="btn btn-primary" OnClick="btnGetConfirm_Click" ClientIDMode="Static" />
                        <input type="reset" runat="server" value="重置" id="btnReset" class="btn btn-primary" />(若想上传图片，请点击提交)
                    </td>
                </tr>
            </table>

            <table runat="server" id="tblFinish" class="table talbe-bordered" style="width: 55%; margin-top: -10px; text-align: center; border: 1px solid #DDDDDD;">
                <tr>
                    <td width="100px">上传人</td>
                    <td>
                        <asp:Label runat="server" ID="lblCreateEmployee" /></td>
                </tr>
                <tr>
                    <td>上传时间</td>
                    <td>
                        <asp:Label runat="server" ID="lblCreateDate" /></td>
                </tr>
                <tr>
                    <td>解决情况</td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rdoState" onclick="show()" RepeatDirection="Horizontal" RepeatColumns="4">
                            <asp:ListItem Text="未解决" Value="1" />
                            <asp:ListItem Text="处理中" Value="2" />
                            <asp:ListItem Text="已解决" Value="3" />
                            <asp:ListItem Text="无效信息" Value="4" />
                        </asp:RadioButtonList>
                        <asp:Label runat="server" ID="lblState" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td>建议</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSuggest" TextMode="MultiLine" Width="220px" Height="120px" ClientIDMode="Static" />
                        <asp:Label runat="server" ID="lblSuggest" Width="320px" Style="white-space: normal;"  Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button runat="server" ID="btnBack" Text="返回" CssClass="btn btn-info" OnClientClick="javascript:history.go(-1);" Visible="false" />
                        <asp:Button runat="server" ID="btnSaveFinish" Text="保存" CssClass="btn btn-primary" ClientIDMode="Static" OnClick="btnFinish_Click" />
                        <asp:Button runat="server" ID="btnFinish" Text="确认完成" CssClass="btn btn-primary" ClientIDMode="Static" OnClick="btnFinish_Click" OnClientClick="return confirm('您确定确认完成吗?')" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
