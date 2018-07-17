<%@ Page Title="" StylesheetTheme="Default" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="QuotedLossReason.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager.QuotedLossReason" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../../App_Themes/Default/js/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "450px", "height": "305px", "background-color": "#24cff6" });
        });

        $(document).ready(function () {
            $("#btnSave").click(function () {
                if ($("#ddlLoseReason").find(":selected").text() == "请选择") {
                    alert("请选择流失原因");
                    $("#txtLossMoney").focus();
                    return false;
                } else if ($("#txtLostContent").val() == "") {
                    alert("请输入流失内容");
                    return false;
                }
            });
        });
        $("#txtLossMoney").focus(function () {
            alert("测试成功");
            //if ($("#txtLossMonty").val() == "0") {
            //    document.getElementById("txtLossMonty").innerText = "";
            //}
        });

        $(input).focus(function () {
            alert("测试成功");

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="dvi_main">
        <table>
            <thead>
                <tr>
                    <th colspan="2" align="left">
                        <h4>订单退订</h4>
                    </th>
                </tr>
            </thead>
            <tr>
                <td>退单原因</td>
                <td>
                    <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlLoseReason" Width="150px" /></td>
            </tr>
            <tr>
                <td>退单内容</td>
                <td>
                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtLostContent" TextMode="MultiLine" Style="width: 220px; height: 100px;"></asp:TextBox></td>
            </tr>
            <tr>
                <td>
                    <asp:Button runat="server" ClientIDMode="Static" ID="btnSave" Text="保存" CssClass="btn btn-primary" OnClick="btnSave_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
