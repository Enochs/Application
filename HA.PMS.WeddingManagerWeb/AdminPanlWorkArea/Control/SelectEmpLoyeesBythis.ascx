<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectEmpLoyeesBythis.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SelectEmpLoyeesBythis" %>
<div id="SelectEmployee" class="SelectEmpLoyee" style="width: 900px; height: 900px;">

    <table style="width:450px;" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
        <tr>
            <td>
                <table  style="width:450px;" border="1" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF">
                      <tr>
                        <td colspan="3">
                            <asp:Button ID="Button1" runat="server" Text="确认" OnClientClick="return SetSelectEmpLoyee();" CssClass="btn" Style="width: 78px" OnClick="btnSaveSelect_Click" />
                            

                        </td>
                    </tr>
                    <tr>
                        <td style="width: 38%;">
                            <asp:TreeView ID="treeDepartment" runat="server" Width="100%" OnSelectedNodeChanged="treeDepartment_SelectedNodeChanged" >
  
                            </asp:TreeView>
                        </td>
                        <td style="width:50px;">
                         >>></td>
                        <td style="vertical-align:top;">
                            <table border="0" cellpadding="1" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td>
                                        <table width="0px;" border="1" cellpadding="5" cellspacing="1">
                                            <asp:Repeater ID="repEmpLoyeeList" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width:30px;"   bgcolor="#FFFFEE">
                                                            <input id="rdoEmpLoyee" type="checkbox" value='<%#Eval("EmployeeID") %>' name="radioEmpLoyee" ref="<%#Eval("EmployeeName") %>" /></td>
                                                        <td style="width:100px;"  bgcolor="#FFFFEE"><%#Eval("EmployeeName") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnSaveSelect" runat="server" Text="确认" OnClientClick="return SetSelectEmpLoyee();" CssClass="btn" Style="width: 78px" OnClick="btnSaveSelect_Click" />
                            <asp:HiddenField ID="hideControlKey" runat="server" ClientIDMode="Static" />
                            <asp:HiddenField ID="hideParentKey" ClientIDMode="Static" runat="server" />
                            <asp:HiddenField ID="hideSetEmployeeName" ClientIDMode="Static" runat="server" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <a style="display: none;" id="showSelectEmployee" href="#SelectEmployee" onclick="ShowSelectEmployeeWindows();"><i class="icon-fullscreen"></i>选择用户</a>
 
    <script type="text/javascript">
        function SetSelectEmpLoyee() {
            var _EmpIDs = "";
            var _EmpNames = "";
            $('input:checkbox:checked').each(function () {
                _EmpIDs = _EmpIDs + $(this).val() + ",";
                _EmpNames = _EmpNames + $(this).attr("ref") + ",";
            });
            _EmpIDs = _EmpIDs.substr(0, _EmpIDs.length - 1);
            _EmpNames = _EmpNames.substr(0, _EmpNames.length - 1);

            var ParentKey = $("#hideParentKey").val();
            var ControlKey = "#" + $("#hideControlKey").val();
            var SetEmployeeName = $("#hideSetEmployeeName").val();

            if (ParentKey != "") {
                parent.$("#" + ParentKey).children(ControlKey).attr("value", _EmpIDs);
                parent.$("#" + ParentKey).children("." + SetEmployeeName).attr("value", _EmpNames);
            }
            else {
                parent.$("#" + ControlKey).attr("value", _EmpIDs);
                parent.$("#" + SetEmployeeName).attr("value", _EmpNames);
            }
            if (_EmpIDs != "") {
                parent.$.fancybox.close(1);
            }
            else {
                alert("请选择责任人！");
                return false;
            }
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
