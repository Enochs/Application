<%@ Page Title="WBMS—添加策划师" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FL_PlannerCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases.FL_PlannerCreate" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/ecmascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "600px", "height": "800px", "background-color": "transparent" });
        });

        $(document).ready(function () {
            $("#btnConfirm").click(function () {
                if ($("#txtPlannerName").val() == "") {
                    alert("请输入姓名");
                    return false;
                    //} else if ($("#ddlPlannerJob").val() == "请选择") {
                    //    alert("请选择你的职务");
                    //    return false;
                }
                else if ($("#txtPlannerSpecial").val() == "") {
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

        function onFileChange(sender) {
            //var path = $("#ImageUpload").val()
            //var paths = path.substring(12, path.length);

            //document.getElementById("imgDimensionalPic").ImageUrl = "/AdminPanlWorkArea/Sys/Personnel/PersonnelImage/" + paths;

            //document.getElementById("imgDimensionalPic").src = window.URL.createObjectURL(sender.files[0]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                        <asp:FileUpload runat="server" ClientIDMode="Static" ID="ImageUpload" />
                       <%-- <asp:Image ID="imgDimensionalPic" ClientIDMode="Static" runat="server" Style="width: 160px; height: 100px;" />--%>
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
                            <asp:ListItem Text="启用" Value="0" Selected="True" />
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
                    <td>
                        <asp:Button runat="server" ID="btnConfirm" Text="确定" ClientIDMode="Static" Style="width: 50px; height: 25px;" OnClick="btnConfirm_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
