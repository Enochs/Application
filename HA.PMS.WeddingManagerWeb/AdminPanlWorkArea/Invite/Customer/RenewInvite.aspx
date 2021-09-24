<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RenewInvite.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer.RenewInvite" Title="邀约恢复" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>


<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="uc1" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="uc1" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="uc1" TagName="MyManager" %>




<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content1">

    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 20px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }

        /*小屏幕分辨率*/
        .centerSmallTxt {
            width: 55px;
            height: 20px;
        }

        .centerSmallSelect {
            width: 88px;
            height: 30px;
        }
    </style>

    <script type="text/javascript">

        function SavetoOtherEmpLoyee() {
            if ($("#hideEmpLoyeeID").val() == "-1") {
                alert("请先选择责任人！");
                $(".txtEmpLoyeeName").focus();
                return false;
            }
            return true;
        }

        //KeyID: 对应MarkID，Control: 当前使用JS对象，IsAssign:是否是分派标识 0代表 分派，1 代表改派
        function ShowWindowsPopu(KeyID, Control, IsAssign) {
            var Url = "FL_TelemarketingOper.aspx?MarkId=" + KeyID + "&IsAssign=" + IsAssign;
            $(Control).attr("id", "updateShow" + KeyID);

            //这里的设置高度不行，因此在FL_TelemarketingOper 页面中设置本身的高度
            showPopuWindows(Url, 500, 460, "a#" + $(Control).attr("id"));
        }
        //但点击批量批量修改分派的时候返回弹出选择人员框
        function BatchChoose() {
            //这里的判断是
            var counts = 0;
            $('input[type=checkbox]').each(function (index, value) {
                if ($(this).is(":checked")) {
                    counts = 1;
                }
            });
            //当选择之后重新刷新页面时进入是否选择了新人
            if (counts > 0) {
                $("#choose").fancybox({
                    'titlePosition': 'inside',
                    'transitionIn': 'none',
                    'transitionOut': 'none'
                }).trigger('click');

            }

        }
        $(document).ready(function () {
            //BatchChoose();

            $("#<%=chkAll.ClientID%>").click(function () {
                //var chked = false;
                //$("#tblContent :checkbox").attr("checked", $(this).is(":checked"));
                var chks = document.getElementsByTagName("input");
                if (document.getElementById('<%=chkAll.ClientID%>').checked) {
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
                $("#<%=chkAll.ClientID%>").attr("checked", subBox.length == $(".chkbox:checked").length ? true : false);
            });

            //检查被选中的个数
            $("#tblContent :checkbox").click(function () {
                checks();
            });
        });

        function checks() {
            //保存按钮禁用标识
            var state = "disabled";

            //如果此时还有其他的被选中那么对应的， 
            /* 
            */
            //保存按钮将不会被禁用
            var checkedCount = $("#tblContent :checked").length;
            if (checkedCount > 0) {
                state = false;
            } else {
                state = "disabled";
            }
        }




        $(document).ready(function () {
            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {
                if (indexs != 4) {
                    $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });
            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");

            //
            $("html").css({ "overflow-x": "hidden" });

            if (window.screen.width >= 1280 && window.screen.width <= 1366) {

                $(":text").each(function (indexs, values) {
                    $(this).addClass("centerSmallTxt");
                });
                $("select").addClass("centerSmallSelect");
            }


            $(".popuLoseContent").fancybox({
                'topRatio': 0.2
            });
            $(".popuLoseContent").hover(function () {
                $(this).css({ "color": "#ff0000" });
            }, function () {
                $(this).css({ "color": "#0094ff" });
            });
        });


        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=1&ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 480, 720, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div style="overflow-x: auto;">
        <div class="widget-box">

            <div class="widget-box" style="height: 60px; border: 0px;">
                <table class="queryTable">
                    <tr>
                        <td>渠道类型:</td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <cc2:ddlChannelType ID="ddlChanneltype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChanneltype_SelectedIndexChanged" Width="75"></cc2:ddlChannelType>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>渠道名称:<cc2:ddlChannelName ID="ddlChannelname" runat="server"></cc2:ddlChannelName>
                        </td>
                        <td>流失原因:<cc2:ddlLoseContent ID="ddlLoseContent" runat="server"></cc2:ddlLoseContent>
                        </td>
                        <td>流失时间:
                                        <cc2:DateEditTextBox runat="server" onclick="WdatePicker();" ID="txtStar"></cc2:DateEditTextBox>
                        </td>
                        <td>到: 
                                        <cc2:DateEditTextBox runat="server" onclick="WdatePicker();" ID="txtEnd"></cc2:DateEditTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>新人姓名:</td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="75"></asp:TextBox>
                        </td>
                        <td>联系电话:<asp:TextBox ID="txtCellphone" runat="server" Width="75"></asp:TextBox>
                        </td>
                        <td>
                            <uc1:MyManager runat="server" ID="MyManager" Title="邀约人" />
                        </td>
                        <td>
                            <asp:Button ID="btnSerch" runat="server" CssClass="btn btn-primary" Text="查询" OnClick="btnSerch_Click" />
                            <cc2:btnReload ID="btnReload2" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>

            <table class="table table-bordered table-striped" style="display: none;">
                <thead>
                    <tr id="trContent">
                        <th style="white-space: nowrap;">新人姓名</th>
                        <th style="white-space: nowrap;">联系电话</th>
                        <th style="white-space: nowrap;">婚期</th>
                        <th style="white-space: nowrap;">酒店</th>
                        <th style="white-space: nowrap;">来源渠道</th>
                        <th style="white-space: nowrap;">邀约类型</th>
                        <th style="white-space: nowrap;">邀约人</th>
                        <th style="white-space: nowrap;">接受日期</th>
                        <th>流失时间</th>
                        <th>流失原因</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repTelemarketingManager" runat="server" OnItemCommand="repTelemarketingManager_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><a target="_blank" href="/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?Sucess=1&OnlyView=1&CustomerID=<%#Eval("CustomerID") %>"><%#Eval("ContactMan") %></a>

                                </td>
                                <td><%#Eval("ContactPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetChannelTypeName(Eval("ChannelType")) %></td>
                                <td><%#GetApplyName(Eval("ApplyType")) %></td>
                                <td>
                                    <asp:Label runat="server" ID="lblInviteEmployee" Text='<%#GetEmployeeName(Eval("InviteEmployee")) %>' /></td>
                                <td><%#ShowShortDate(Eval("CreateDate")) %></td>
                                <td>
                                    <asp:Label runat="server" ID="lblLastFollowDate" Text='<%#ShowShortDate(Eval("LastFollowDate")) %>' />
                                </td>
                                <td>
                                    <a class="popuLoseContent " style="text-decoration: underline; color: #0094ff;" href='#Details<%#Eval("CustomerID") %>'><%#GetLoseContent(Eval("ContentID")) %></a>
                                    <div id='Details<%#Eval("CustomerID") %>' style="display: none; width: 250px; width: 250px; text-align: center;">
                                        <span style="font-weight: bold;">流失具体原因说明</span>
                                        <br />
                                        <%#GetLoseContentDetails(Eval("CustomerID")) %>
                                    </div>
                                </td>
                                <td>
                                    <asp:LinkButton ID="ReInvite" Text="恢复邀约" OnClientClick="return confirm('确认恢复该新人到邀约中？')" CommandArgument='<%#Eval("CustomerID") %>' CommandName="ReInvite" CssClass="btn btn-mini btn-primary" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9" style="text-align: left;">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" AlwaysShow="true" PageSize="10" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>

            <table class="table table-bordered table-striped with-check table-select" border="1">
                <thead>
                    <tr>
                        <th>
                            <asp:CheckBox ID="chkAll" runat="server" /></th>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>渠道类型</th>
                        <th>渠道名称</th>
                        <th>流失时间</th>
                        <th>流失原因</th>
                        <th>邀约人</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RptTelemarketing" runat="server" OnItemDataBound="RepTelemarketing_ItemDataBound" OnItemCommand="RptTelemarketing_ItemCommand">
                        <ItemTemplate>
                            <tr skey='TelemarketingCustomerID<%#Eval("CustomerID") %>'>

                                <td style="text-align: center">
                                    <asp:CheckBox ID="chkSinger" CssClass="chkbox" runat="server" />
                                    <asp:HiddenField ID="hideCustomerID" runat="server" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#Eval("ContactMan") %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HiddenField1" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetChannelTypeName(Eval("ChannelType")) %></td>
                                <td style="width: 130px">
                                    <lable style="cursor: default" title='<%#Eval("Channel") %>'><%#ToInLine(Eval("Channel")) %></lable>
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblLastFollowDate" Text='<%#ShowShortDate(Eval("LastFollowDate")) %>' />
                                </td>
                                <td>
                                    <a class="popuLoseContent " style="text-decoration: underline; color: #0094ff;" href='#Details<%#Eval("CustomerID") %>'><%#GetLoseContent(Eval("ContentID")) %></a>
                                    <div id='Details<%#Eval("CustomerID") %>' style="display: none; width: 250px; width: 250px; text-align: center;">
                                        <span style="font-weight: bold;">流失具体原因说明</span>
                                        <br />
                                        <%#GetLoseContentDetails(Eval("CustomerID")) %>
                                    </div>
                                </td>

                                <td id="Partd<%#Container.ItemIndex %>">
                                    <asp:TextBox runat="server" ID="txtEmpLoyee" class="txtEmpLoyeeName" Text='<%#GetEmployeeName(Eval("InviteEmployee")) %>' OnClick="ShowPopu(this)" />
                                    <asp:LinkButton ID="lkbtnAssign" CssClass="btn btn-danger" CommandArgument='<%#Eval("CustomerID") %>' runat="server">恢复邀约</asp:LinkButton>
                                    <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value="0" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="9" style="text-align: left;">
                            <cc1:AspNetPagerTool ID="CtrPager" AlwaysShow="true" runat="server" PageSize="10" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>

            <div class="widget-box">
                <div class="widget-title">
                    <span class="icon"><i class="icon-align-justify"></i></span>
                    <h5>操作栏</h5>
                </div>
                <div class="widget-content nopadding" id="paretntSelect">
                    <asp:Button ID="btnMine" CssClass="btn btn-info" runat="server" Text="派给自己" OnClientClick="" OnClick="btnMine_Click" />&nbsp;&nbsp;&nbsp;
                    选择其他人:<input runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowPopu(this);" type="text" />
                    <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
                    <asp:Button ID="btnOther" CssClass="btn btn-info" runat="server" Text="保存" OnClientClick="return SavetoOtherEmpLoyee();" OnClick="btnOther_Click" />
                </div>
            </div>

        </div>
        <uc1:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>
