<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StarOrder.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.StarOrder" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="../Control/SetIntiveSerch.ascx" TagName="SetIntiveSerch" TagPrefix="uc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="uc1" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="uc1" TagName="MessageBoardforall" %>


<%@ Register Src="../Control/DateRanger.ascx" TagName="DateRanger" TagPrefix="uc2" %>


<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content3">
    <script type="text/javascript">

        //选择责任人
        function SavetoOtherEmpLoyee() {
            if ($("#hideEmpLoyeeID").val() == "-1") {
                alert("请先选择责任人！");
                $(".txtEmpLoyeeName").focus();
                return false;
            }
            return true;
        }


        var KeyList = "";
        //批量派工
        function SaveCheck() {
            $('input[type="checkbox"]:checked').each(function () {
                if ($(this).val() != "0" && $(this).val() != "on") {
                    KeyList += $(this).val() + ",";
                }
            });
            $("#hideKeyList").attr("value", KeyList);
            return true;
        }

        //派单
        $(function () {
            $(".SetState").each(function () {
                if ($(this).parents(".txtEmpLoyeeName").attr("value") == "") {
                    $(this).text("分派");
                } else {
                    $(this).text("改派");
                }
            });
            $("html").css({ "overflow-x": "hidden" });
            $("#chkall").click(function () {
                //var chked = false;
                //$("#tblContent :checkbox").attr("checked", $(this).is(":checked"));
                var chks = document.getElementsByTagName("input");
                if (document.getElementById('chkall').checked) {
                    for (var i = 0; i < chks.length; i++) {
                        chks[i].checked = true;
                    }
                }
                else {
                    for (var i = 0; i < chks.length; i++) {
                        chks[i].checked = false;
                    }
                }
            });
            $('.chkbox').click(function () {
                var subBox = $(".chkbox");
                $("#chkall").attr("checked", subBox.length == $(".chkbox:checked").length ? true : false);
            });
            $("#btnSaveOther").click(function () {
                SaveCheck();
            });

        });
        //点击文本框 弹出部门人员列表
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=1&ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 480, 380, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>
                    <td>渠道名称:<cc2:ddlChannelName ID="DdlChannelName1" runat="server"></cc2:ddlChannelName></td>
                    <td>跟单人:<asp:DropDownList ID="ddlEmployee" runat="server"></asp:DropDownList></td>
                    <td>新人姓名:
                        <asp:TextBox ID="txtContactMan" runat="server"></asp:TextBox>
                    </td>
                    <td>联系电话:
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>时间:<asp:DropDownList runat="server" ID="ddlDateRanger">
                        <asp:ListItem Text="请选择" Value="请选择" />
                        <asp:ListItem Text="婚期" Value="PartyDate" />
                        <asp:ListItem Text="到店时间" Value="ComeDate" />
                        <asp:ListItem Text="添加时间" Value="CreateDate" />
                    </asp:DropDownList>
                    </td>
                    <td>
                        <uc2:DateRanger ID="DateRanger" runat="server" />
                    </td>
                    <td>状态:<cc2:ddlCustomersState runat="server" ID="ddlCustomerStates" /></td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnSerch" CssClass="btn btn-primary" runat="server" Text="查询" OnClick="btnSerch_Click" />
                        <cc2:btnReload ID="btnReload2" runat="server" />
                    </td>

                </tr>
            </table>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr style="border: none;">
                        <td style="border: none;" colspan="9">
                            <div style="margin-left: 79%;">
                                <asp:Button ID="Button1" runat="server" Text="保存" CssClass="btn btn-info" OnClick="btnSaveDate_Click" />
                                <asp:Button ID="Button2" runat="server" Text="派给自己" CssClass="btn btn-info" OnClientClick="return SaveCheck();" OnClick="btnSvaeforme_Click" />
                            </div>
                        </td>
                        <td style="border: none;"></td>
                    </tr>
                    <tr>
                        <th style="white-space: nowrap;">
                            <input id="chkall" type="checkbox" onclick="Checkall(this);" /></th>
                        <th style="white-space: nowrap;">渠道名称</th>
                        <th style="white-space: nowrap;">推荐人</th>
                        <th style="white-space: nowrap;">新人</th>
                        <th style="white-space: nowrap;">联系电话</th>
                        <th style="white-space: nowrap;">婚期</th>
                        <th style="white-space: nowrap;">添加时间</th>
                        <th style="white-space: nowrap;">状态</th>
                        <th style="white-space: nowrap;">酒店</th>
                        <th style="white-space: nowrap;">邀约人</th>
                        <th style="white-space: nowrap;">预约到店时间</th>
                        <th style="white-space: nowrap;">跟单人</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td style="text-align: center">
                                    <input id="chkSinger" class="CustomerCheckBox chkbox" type="checkbox" value="<%#Eval("CustomerID") %>" /></td>
                                <asp:HiddenField ID="hideCustomerID" runat="server" Value='<%#Eval("CustomerID") %>' />
                                <td><%#Eval("Channel") %></td>
                                <td><%#Eval("Referee") %></td>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>"><%#ShowCstmName(Eval("Bride"),Eval("Groom"),Eval("OldB")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("CreateDate") %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                                <td><%#Eval("ComeDate") %></td>
                                <td id="Partd<%#Container.ItemIndex %>">
                                    <input runat="server" style="margin: 0" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowPopu(this);" type="text" value='<%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %>' />
                                    <a href="#" onclick="ShowPopu(this);" class="SetState btn btn-primary">派工</a>
                                    <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='' runat="server" />
                                    <asp:HiddenField ID="hideCustomerHide" Value='<%#Eval("CustomerID") %>' runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="10">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                            <asp:Button ID="btnSaveDate" runat="server" Text="保存" CssClass="btn btn-info" OnClick="btnSaveDate_Click" />
                            <asp:Button ID="btnSvaeforme" runat="server" Text="派给自己" CssClass="btn btn-info" OnClientClick="return SaveCheck();" OnClick="btnSvaeforme_Click" />
                        </td>
                        <asp:HiddenField ID="hideKeyList" ClientIDMode="Static" runat="server" />
                    </tr>
                </tfoot>
            </table>
            <div class="widget-box">
                <div class="widget-title">
                    <span class="icon"><i class="icon-align-justify"></i></span>
                    <h5>操作栏</h5>
                </div>
                <div class="widget-content nopadding" id="paretntSelect">

                    <asp:Button ID="btnSaveOther" ClientIDMode="Static" CssClass="btn btn-info" runat="server" Text="派给其他人" OnClick="btnSaveOther_Click" OnClientClick="return SavetoOtherEmpLoyee();" />
                    选择其他:<input runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowPopu(this);" type="text" />
                    <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
                </div>
            </div>
            <uc1:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>

