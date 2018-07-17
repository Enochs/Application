<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sys_EmployeeAuthorization.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Jurisdiction.Sys_EmployeeAuthorization" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".chklist").click(function () {
                CheckParent(this);
            });

            $(".chkparent").click(function () {
                CheckChilden(this);
            });
            $('input:not(#chkall)').click(function () {
                var subcount = $(".chkparent").length + $(".chklist").length
                $("#chkall").attr("checked", $(".chkbox:checked").length == subcount ? true : false);
            });
        });

        //弹出
        function ShowPopuWindown(ChannelID, EmployeeID, Control) {

            var Url = "/AdminPanlWorkArea/Sys/Jurisdiction/Sys_JurisdictionforButtonManager.aspx?ChannelID=" + ChannelID + "&EmployeeID=" + EmployeeID;
            showPopuWindows(Url, 800, 900, Control);

        }

        function Checkall(Control) {
            //$("input[type='checkbox']").attr("checked", $(Control).val());
            var ctrls = document.getElementsByTagName("input");
            if (document.getElementById("chkall").checked) {
                for (var i = 0; i < ctrls.length; i++) {
                    ctrls[i].checked = true;
                    $(ctrls[i]).attr("checked", true);
                }
            }
            else {
                for (var i = 0; i < ctrls.length; i++) {
                    ctrls[i].checked = false;
                    $(ctrls[i]).attr("checked", false);
                }
            }
        }

        function ShowPopu(Parent, Childer, ClassName) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=" + Childer + "&ParentControl=" + $(Parent).parent().attr("id") + "&SetEmployeeName=" + ClassName;
            showPopuWindows(Url, 700, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }



        function CheckParent(Control) {
            $(Control).parent().parent().parent().find("#chkChannel").attr("checked", true);
        }

        function CheckChilden(Control) {

            if ($(Control).children().attr('checked') == undefined) {
                $(Control).attr("checked", true);
                $(Control).children().attr('checked', true);
                var ctrls = $(Control).parent().children(".repSecondTreeUL").find("input[type='checkbox']");
                for (var i = 0; i < ctrls.length; i++) {
                    ctrls[i].checked = true;
                    $(ctrls[i]).attr("checked", true);
                }
                //$(Control).parent().children(".repSecondTreeUL").find("input[type='checkbox']").removeAttr("checked");
            }
            else {
                $(Control).attr("checked", false);
                $(Control).children().attr('checked', false);
                var ctrls = $(Control).parent().children(".repSecondTreeUL").find("input[type='checkbox']");
                for (var i = 0; i < ctrls.length; i++) {
                    ctrls[i].checked = false;
                    $(ctrls[i]).attr("checked", false);
                }
            }
            //$(Control).parent().children(".repSecondTreeUL").find("input[type='checkbox']").attr("checked", true);
            //  $(Control).parent().children().children().children().find("#ChkSecond").attr("checked", true);
        }
    </script>


    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5><a id="ff" href="#" onclick="ShowCreateWindows(this,'')">用户授权</a></h5>
        </div>
        <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
        <div class="widget-content">
            <table class="table table-bordered table-striped">

                <tr>
                    <td>
                        <cc1:ClickOnceButton ID="ClickOnceButton2" CssClass="btn btn-success" runat="server" Text="保存用户权限" OnClick="btnCreateDate_Click" />
                    </td>

                </tr>
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <input id="chkall" type="checkbox" class="style123" onclick="Checkall(this);" />全选/取消  频道名称</td>

                            </tr>
                            <asp:Repeater ID="RepChannelList" runat="server" OnItemDataBound="RepChannelList_ItemDataBound">
                                <ItemTemplate>
                                    <tr>
                                        <td colspan="4">
                                            <asp:CheckBox ID="chkChannel" ClientIDMode="Static" CssClass="chkparent" runat="server" />
                                            <asp:HiddenField ID="hideChannel" runat="server" Value='<%#Eval("ChannelID") %>' />
                                            <a href="#"><%#Eval("ChannelName") %></a>
                                            &nbsp&nbsp&nbsp&nbsp<a href="#" style="display: none" onclick="ShowPopuWindown('<%#Eval("ChannelID") %>','<%#Request["EmployeeID"] %>',this);" <%#HideOrShow(Eval("ChannelID")) %>>控件授权</a>
                                            <ul class="repSecondTreeUL" id='repSecondTree<%#Eval("ChannelID") %>'>
                                                <asp:Repeater ID="repSecondTree" runat="server" OnItemDataBound="repSecondTree_ItemDataBound">
                                                    <ItemTemplate>
                                                        <li id="PartdE<%#Eval("ChannelID") %>" style="border: initial;">
                                                            <asp:CheckBox ID="ChkSecond" ClientIDMode="Static" runat="server" CssClass="chklist" ParentID='<%#Eval("Parent") %>' /><%#Eval("ChannelName") %>
                                                            <asp:HiddenField ID="hideScondChannel" runat="server" Value='<%#Eval("ChannelID") %>' />
                                                            <asp:HiddenField ID="hideChecksEmployee" ClientIDMode="Static" Value='<%#Eval("ChecksEmployee") %>' runat="server" />
                                                            <label style="display: none;">
                                                                上级审核人
                                                            <input runat="server" style="width: 60px;" id="txtChecksEmployee" class="txtEmpLoyeeName" onclick="ShowPopu(this, 'hideChecksEmployee', 'txtEmpLoyeeName');" type="text" value='<%#GetEmpLoyeeNameByID(Eval("ChecksEmployee")) %>' />
                                                                <asp:HiddenField ID="hideDispatching" ClientIDMode="Static" Value='<%#Eval("Dispatching") %>' runat="server" />
                                                                上级派工人
                                                            <input runat="server" style="width: 60px;" id="txtDispatching" class="txtDispatching" onclick="ShowPopu(this, 'hideDispatching', 'txtDispatching');" type="text" value='<%#GetEmpLoyeeNameByID(Eval("Dispatching")) %>' />
                                                                数据权限
                                                            <asp:DropDownList ID="ddlDataPower" Width="55px" runat="server">
                                                                <asp:ListItem Text="锁定" Value="1" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="解锁" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="全锁定" Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                                &nbsp&nbsp&nbsp&nbsp <a href="#" style="display: none" onclick="ShowPopuWindown('<%#Eval("ChannelID") %>','<%#Request["EmployeeID"] %>',this);" <%#HideOrShow(Eval("ChannelID")) %>>控件授权</a>
                                                            </label>
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </td>

                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <cc1:ClickOnceButton ID="ClickOnceButton1" CssClass="btn btn-success" runat="server" Text="保存用户权限" OnClick="btnCreateDate_Click" />
                    </td>

                </tr>
            </table>

        </div>

    </div>


</asp:Content>


