<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectDepartment.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SelectDepartment" %>
<link href="/App_Themes/Default/css/bootstrap.min.css" rel="stylesheet" />
<script src="/App_Themes/Default/js/bootstrap.min.js"></script>
<script type="text/javascript">
    $(function () {
        $("html,body").css({ "width": "320px", "height": "360px", "overflow": "hidden" });
    });
</script>
<div id="SelectEmployee" class="SelectEmpLoyee" style="overflow-y:auto;height:355px;overflow:scroll"" >
    <table style="width:315px;height:355px;overflow:scroll">
        <tr style="height:315px;vertical-align:top;text-align:left">
            <td>
                <asp:TreeView ID="treeDepartment" runat="server" OnSelectedNodeChanged="treeDepartment_SelectedNodeChanged"></asp:TreeView>
            </td>
        </tr>
        <tr>
            <td style="text-align:center;vertical-align:middle">
                <asp:TextBox ID="txtDepartmentName" style="margin:0" ClientIDMode="Static" runat="server"></asp:TextBox>
                <asp:Button ID="btnSaveSelect" runat="server" Text="确认" OnClientClick="return setselectempLoyee();" CssClass="btn btn-success" OnClick="btnSaveSelect_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hideControlKey" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hideDepartmentKey" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hiddDeparttxtKey" runat="server" ClientIDMode="Static" />
    <a style="display: none;" id="showSelectEmployee" href="#SelectEmployee" onclick="showselectemployeewindows();"><i class="icon-fullscreen"></i>选择部门</a>
    <script type="text/javascript">
        function setselectempLoyee() {
            var DepartmetnName = $("#txtDepartmentName").val();
            var DepartmentKey = $("#hideControlKey").val();
            var ParentKey = $("#hiddDeparttxtKey").val();
            if (DepartmetnName != "") {
                parent.$("#" + DepartmentKey).attr("value", $("#hideDepartmentKey").val());
                parent.$("#" + ParentKey).attr("value", DepartmetnName);
                parent.$.fancybox.close(1);
                return false;
            } else {
                alert("请选择部门！");
                return false;
            }
        }
        function showselectemployeewindows() {
            $("a#showSelectEmployee").fancybox();
        }
    </script>
</div>
