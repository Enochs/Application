<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectSupplier.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SelectSupplier" %>
<div id="SelectSuppLier" class="SelectEmpLoyee" style="width: 400px; height: 900px;">
    <br />
    <table style="width: 500px;">
        <tr>
            <td style="width: 250px; vertical-align: top;">供应商类别：
                <asp:ListBox ID="lstTyperBox" AutoPostBack="true" runat="server" Height="250px" Width="100%" OnSelectedIndexChanged="lstTyperBox_SelectedIndexChanged"></asp:ListBox>

            </td>
            <td>
                <table border="1" cellpadding="5" cellspacing="1" style="width: 250px; height: 250px;">
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="Button2" runat="server" Text="确认选择" OnClientClick="return SetSelectEmpLoyee();" CssClass="btn btn-success" Style="width: 78px" />
                        </td>
                    </tr>
                    <asp:Repeater ID="repSupplier" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="width: 30px;" bgcolor="#FFFFEE">
                                    <input id="rdoSupplier" type="radio" value='<%#Eval("SupplierID") %>' name="radioEmpLoyee" ref="<%#Eval("Name") %>" /></td>
                                <td style="width: 100px;" bgcolor="#FFFFEE"><%#Eval("Name") %>
                        

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnSaveSelect" runat="server" Text="确认选择" OnClientClick="return SetSelectEmpLoyee();" CssClass="btn btn-success" Style="width: 78px" />
                            <input id="Button1" type="button" value="button" style="display: none;" onclick="return SetSelectEmpLoyee();" />

                            <asp:HiddenField ID="hideControlKey" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hideParentKey" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="hideSetEmployeeName" ClientIDMode="Static" runat="server" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>




    <a style="display: none;" id="showSelectEmployee" href="#SelectEmployee" onclick="ShowSelectEmployeeWindows();"><i class="icon-fullscreen"></i>选择供应商</a>

    <script type="text/javascript">
        function SetSelectEmpLoyee() {
            var CheckRadioVal = $('input:radio:checked').val();
            var EmpLoyeeName = $('input:radio:checked').attr("ref");
            var ParentKey = $("#hideParentKey").val();
            var ControlKey = "#" + $("#hideControlKey").val();
            var SetEmployeeName = $("#hideSetEmployeeName").val();
            if (ParentKey != "") {
                parent.$("#" + ParentKey).children(ControlKey).attr("value", CheckRadioVal);
                parent.$("#" + ParentKey).find("." + SetEmployeeName).attr("value", EmpLoyeeName);
            } else {
                parent.$("#" + ControlKey).attr("value", CheckRadioVal);
                parent.$("#" + SetEmployeeName).attr("value", EmpLoyeeName);
            }
            if (typeof (CheckRadioVal) != "undefined") {
                parent.$("#<%=Request["Callback"]%>").click();

                parent.$.fancybox.close(1);
            } else {
                alert("请选择供应商！");
                return false;
            }
            return false;
        }

        function ShowSelectEmployeeWindows() {
            $("a#showSelectEmployee").fancybox();
        }

        //$("#a标签的ID").fancybox({
        //    'titlePosition': 'inside',
        //    'transitionIn': 'none',
        //    'transitionOut': 'none'
        //}).trigger('click');

        //function ShowSelectEmpLoyee() {
        //    alert();
        //    $("#SelectEmployee").fancybox({
        //        'showCloseButton': false,
        //        'titlePosition': 'inside',
        //        'titleFormat': formatTitle
        //    });
        //    return false;
        //}
    </script>
</div>

