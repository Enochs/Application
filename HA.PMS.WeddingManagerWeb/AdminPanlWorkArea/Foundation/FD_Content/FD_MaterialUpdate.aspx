<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_MaterialUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.FD_MaterialUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "450px", "height": "350px", "background-color": "#24cff6" });
        });

        $(document).ready(function () {
            $("#btnConfrim").click(function () {
                if ($("#txtMaterialName").val() == "") {
                    alert("请输入名称");
                    $("#txtMaterialName").focus();
                    return false;
                } else if ($("#txtUnitPrice").val() == "") {
                    alert("请输入单价");
                    $("#txtUnitPrice").focus();
                    return false;
                }
            });
        });
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <div class="widget-content">
                <table class="table table-bordered table-striped">
                    <tr>
                        <td  runat="server" id="tr_td1"><asp:Label runat="server" ID="lblMarerialId">编号:</asp:Label></td>
                        <td  runat="server" id="tr_td2">
                            <asp:TextBox runat="server" ID="txtMaterialId" ReadOnly="true" /></td>
                        <td>名称:</td>
                        <td>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMaterialName" /></td>
                    </tr>
                    <tr>
                        <td>单价:</td>
                        <td>
                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtUnitPrice"  onkeyup="value=value.replace(/[^\d\.]/g,'')" /></td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top">备注</td>
                        <td colspan="3">
                            <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine"
                                Height="103px" Width="326px" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button runat="server" ID="btnConfrim" Text="确定" ClientIDMode="Static"
                                CssClass="btn btn-primary" OnClick="btnConfrim_Click" /></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
