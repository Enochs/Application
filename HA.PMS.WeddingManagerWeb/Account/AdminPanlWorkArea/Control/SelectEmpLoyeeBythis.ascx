<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectEmpLoyeeBythis.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SelectEmpLoyeeBythis" %>
<script type="text/javascript">
    $(function () {
        $("html,body").css({ "width": "480px", "height": "360px", "overflow": "hidden" });
    });
</script>
<div style="width:480px; height: auto; overflow-y: scroll;">
    <table style="height:100%">
        <tr style="vertical-align: top; text-align: left">
            <td style="width: 220px; vertical-align: top;text-align:left">
                <asp:TreeView ID="treeDepartment" runat="server" OnSelectedNodeChanged="treeDepartment_SelectedNodeChanged"></asp:TreeView>
            </td>
            <td style="width: auto; vertical-align: top;text-align:center">
                <table class="table table-bordered table-striped">
                    <asp:Repeater ID="repEmpLoyeeList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="width: 22px;">
                                    <input id="rdoEmpLoyee" type="radio" value='<%#Eval("EmployeeID") %>' name="radioEmpLoyee" ref="<%#Eval("EmployeeName") %>" /></td>
                                <td style="width: 100px;"><%#Eval("EmployeeName") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </td>
        </tr>
    </table>
</div>
<div style="text-align:center">
    <asp:Button ID="btnSaveSelect" runat="server" Text="确认" OnClientClick="return setselectempLoyee();" CssClass="btn btn-success" OnClick="btnSaveSelect_Click" />
</div>
<asp:HiddenField ID="hideControlKey" runat="server" ClientIDMode="Static" />
<asp:HiddenField ID="hideParentKey" ClientIDMode="Static" runat="server" />
<asp:HiddenField ID="hideSetEmployeeName" ClientIDMode="Static" runat="server" />
<a style="display: none;" id="showSelectEmployee" href="#SelectEmployee" onclick="ShowSelectEmployeeWindows();"><i class="icon-fullscreen"></i>选择用户</a>
<script type="text/javascript">
    function setselectempLoyee() {
        var _checkradioval = $('input:radio:checked').val();
        var _emplyeename = $('input:radio:checked').attr("ref");
        var _parentid = $("#hideParentKey").val();
        var _controlid = "#" + $("#hideControlKey").val();
        var SetEmployeeName = $("#hideSetEmployeeName").val();
        if (_parentid != "") {
            parent.$("#" + _parentid).children(_controlid).attr("value", _checkradioval);
            parent.$("#" + _parentid).children("." + SetEmployeeName).attr("value", _emplyeename);
        } else {
            parent.$("#" + _controlid).attr("value", _checkradioval);
            parent.$("#" + SetEmployeeName).attr("value", _emplyeename);
        }
        if (typeof (_checkradioval) != "undefined") {
            parent.$("#<%=Request["Callback"]%>").click();
            parent.$.fancybox.close(1);
        } else {
            alert("请选择责任人！");
            return false;
        }
    }
    function ShowSelectEmployeeWindows() {
        $("a#showSelectEmployee").fancybox();
    }
</script>
