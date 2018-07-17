<%@ Page Title="WBMS—修改策划师" Language="C#" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FL_PlannerUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases.FL_PlannerUpdate" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/ecmascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "100%", "height": "600px", "background-color": "transparent" });
        });

        $(document).ready(function () {
            $("#btnConfirm").click(function () {
                if ($("#txtPlannerName").val() == "") {
                    alert("请输入姓名");
                    return false;
                    //} else if ($("#ddlPlannerJob").val() == "请选择") {
                    //    alert("请选择你的职务");
                    //    return false;
                    //} else if ($("#txtPlannerSpecial").val() == "") {
                    alert("请填写特长");
                    return false;
                } else if ($("#txtPlannerJobDescription").val() == "") {
                    alert("请填写工作介绍");
                    return false;
                } else if ($("#txtPlannerIntrodution").val() == "") {
                    alert("请填写个人介绍");
                    return false;
                }
            });
        });

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "SelectCase.aspx?PlannerID=<%=Request["PlannerID"] %>";
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));
        }
    </script>
    <style type="text/css">
        ul li {
            list-style-type: none;
        }

        .li_Title {
            float: left;
            width: 160px;
        }
        .li_State {
            float:left;
            width:80px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <div class="div">
        <div class="main">
            <table>
                <tr>
                    <td>姓名:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPlannerName" ClientIDMode="Static" /></td>
                </tr>
                <tr>
                    <td>性别:</td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rdoSex" RepeatColumns="2" CellPadding="10" CellSpacing="15">
                            <asp:ListItem Text="男" Value="0" Selected="True" />
                            <asp:ListItem Text="女" Value="1" />
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>职务:</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlPlannerJob" ClientIDMode="Static">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>照片:</td>
                    <td>
                        <asp:FileUpload runat="server" ID="ImageUpload" />
                        <asp:TextBox runat="server" ID="txtImage" Enabled="false" />
                    </td>
                </tr>
                <tr style="display: none;">
                    <td style="vertical-align: top;">特长:</td>
                    <td>
                        <%--<asp:TextBox runat="server" ID="txtPlannerSpecial" TextMode="MultiLine" Height="60px" Width="200px" ClientIDMode="Static" />--%>
                        <cc1:CKEditorTool ID="txtPlannerSpecial" Height="200px" Width="99%" runat="server" BasePath="~/Scripts/ckeditor/">
                        </cc1:CKEditorTool>
                    </td>

                </tr>
                <tr>
                    <td style="vertical-align: top;">个人介绍:</td>
                    <td>
                        <%--<asp:TextBox runat="server" ID="txtPlannerJobDescription" TextMode="MultiLine" Height="60px" Width="200px" ClientIDMode="Static" />--%>
                        <cc1:CKEditorTool ID="txtPlannerJobDescription" Height="200px" Width="99%" runat="server" BasePath="~/Scripts/ckeditor/">
                        </cc1:CKEditorTool>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td style="vertical-align: top;">个人介绍:</td>
                    <td>
                        <%--<asp:TextBox runat="server" ID="txtPlannerIntrodution" TextMode="MultiLine" Height="60px" Width="200px" ClientIDMode="Static" />--%>
                        <cc1:CKEditorTool ID="txtPlannerIntrodution" Height="200px" Width="99%" runat="server" BasePath="~/Scripts/ckeditor/">
                        </cc1:CKEditorTool>
                    </td>
                </tr>
                <tr>
                    <td>备注:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Height="200px" Width="99%" /></td>
                </tr>
                <tr>
                    <td>是否启用:</td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rdoIsDelete" RepeatColumns="2" CellPadding="10" CellSpacing="15">
                            <asp:ListItem Text="启用" Value="0" />
                            <asp:ListItem Text="禁用" Value="1" />
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>排序:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSort"/></td>
                </tr>
                <tr>
                    <td valign="top">代表作品:</td>
                    <td>
                        <a href="#" onclick="ShowUpdateWindows(1,this)" class="btn btn-primary">作品上传</a>
                        <h5>作品：</h5>
                        <asp:Repeater runat="server" ID="rptEvaulation" OnItemCommand="rptEvaulation_ItemCommand">
                            <ItemTemplate>
                                <ul>
                                    <li class="li_Title">
                                        <asp:Label runat="server" ID="lblEvaulation" Text='<%#Eval("EvalTitle").ToString().Length >= 16 ? Eval("EvalTitle").ToString().Substring(0,15) +"…" : Eval("EvalTitle") %>' ToolTip='<%#Eval("EvalTitle") %>' />
                                    </li>
                                    <li class="li_State">
                                        <asp:Label runat="server" ID="Label1" Text='<%#Eval("IsShow").ToString().ToInt32() == 0 ? "已禁用": "已启用" %>' ToolTip='<%#Eval("EvalTitle") %>' />
                                    </li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lbtnDisable" Text="禁用" CommandName="Disable" CommandArgument='<%#Eval("EvalID") %>' CssClass="btn btn-primary btn-mini" />
                                        <asp:LinkButton runat="server" ID="lbtnEnable" Text="启用" CommandName="Enable" CommandArgument='<%#Eval("EvalID") %>' CssClass="btn btn-primary btn-mini" />
                                        <asp:LinkButton runat="server" ID="lbtnDelete" Text="删除" CommandName="Delete" OnClientClick="return confirm('您确定要删除吗?');" CommandArgument='<%#Eval("EvalID") %>' CssClass="btn btn-primary btn-mini" />
                                    </li>
                                </ul>
                            </ItemTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnConfirm" Text="确定" ClientIDMode="Static" CssClass="btn btn-primary" Style="width: 50px; height: 25px;" OnClick="btnConfirm_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
