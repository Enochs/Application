<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectFourGuardian.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SelectFourGuardian" %>
<%--选择四大金刚--%>
<div style="width: 450px; height: 500px;">
    <table style="width: 100%; border: 1px solid gray;">
        <tr>
            <td></td>
            <td>
                <asp:Button ID="Button2" runat="server" Text="确认" OnClientClick="return SetSelectEmpLoyee();" CssClass="btn btn-success" /></td>
        </tr>
        <tr>
            <td>
                <ul>
                    <asp:Repeater ID="repType" runat="server" OnItemCommand="repType_ItemCommand">
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton ID="lnkbtnSelect" CommandName="lnkbtnSelect" CommandArgument='<%#Eval("TypeId") %>' runat="server"><%#Eval("TypeName") %></asp:LinkButton>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </td>
            <td>


                <div style="max-height: 350px; overflow: auto; margin-top: 20px;">
                    <table>
                        <thead>
                            <tr>
                                <th>名称</th>
                                <th width="50px"></th>
                                <th>价格</th>
                            </tr>
                        </thead>
                        <asp:Repeater ID="repContent" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>

                                        <input id="chkContent" GroupName="chkContent" onclientclick="return SetSelectEmpLoyee();" runat="server" type="checkbox" value='<%#Eval("GuardianId") %>' /><%#Eval("GuardianName") %>
                                        

                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPrices" Text='<%#Eval("CooperationPrice") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>

            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 10px;"></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnSaveSelect" runat="server" Text="确认" OnClientClick="return SetSelectEmpLoyee();" CssClass="btn btn-success" />
                <input id="Button1" type="button" value="button" style="display: none;" onclick="return SetSelectEmpLoyee();" />

                <asp:HiddenField ID="hideControlKey" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hideParentKey" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hideSetEmployeeName" ClientIDMode="Static" runat="server" />
            </td>
        </tr>
    </table>

    <a style="display: none;" id="showSelectEmployee" href="#SelectEmployee" onclick="ShowSelectEmployeeWindows();"><i class="icon-fullscreen"></i>选择四大金刚</a>
    <script type="text/javascript">
        function SetSelectEmpLoyee() {
            var CheckRadioVal = $('input:checkbox:checked').val();
            var EmpLoyeeName = $('input:checkbox:checked').attr("ref");
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
                //alert(typeof (CheckRadioVal));
                parent.$("#<%=Request["Callback"]%>").click();

                parent.$.fancybox.close(1);
            } else {
                alert("请选择四大金刚！");
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
