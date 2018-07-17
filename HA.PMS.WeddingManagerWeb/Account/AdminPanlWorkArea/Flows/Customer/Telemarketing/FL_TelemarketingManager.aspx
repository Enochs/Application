<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_TelemarketingManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.Telemarketing.FL_TelemarketingManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>


<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="head">
    <script src="/Scripts/trselection.js"></script>
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
            //页面初始化时 默认禁用保存按钮
            $("#<%=btnSaveUpdate.ClientID%>").attr("disabled", "disabled");

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
            $("#<%=btnSaveUpdate.ClientID%>").attr("disabled", state);
        }

        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=1&ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 480, 720, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }



    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>
                    <td>渠道类型:<cc2:ddlChannelType ID="ddlChannelType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlChanneltype_SelectedIndexChanged"></cc2:ddlChannelType></td>
                    <td>渠道名称:<cc2:ddlChannelName ID="ddlChannelName" runat="server" AutoPostBack="false"></cc2:ddlChannelName></td>
                    <td>新人姓名:<asp:TextBox ID="txtBrideName" runat="server" Width="50px"></asp:TextBox>
                    </td>
                    <td>状态:<cc2:ddlCustomersState ID="ddlCustomersState" runat="server">
                    </cc2:ddlCustomersState>
                    </td>
                    <td>邀约人:<cc2:ddlMyManagerEmployee ID="ddlMyManagerEmployee" runat="server">
                    </cc2:ddlMyManagerEmployee>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>


                    <td>录入人:<cc2:ddlEmployee ID="ddlCreateEmployee" runat="server">
                    </cc2:ddlEmployee>
                    </td>
                    <td>时间：<asp:DropDownList Width="85" ID="ddltimerType" runat="server">
                        <asp:ListItem Value="-1">请选择</asp:ListItem>
                        <asp:ListItem Value="0">婚期</asp:ListItem>
                        <asp:ListItem Value="1">录入时间</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" Title="录入时间" />
                    </td>
                    <td></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btnQuery" runat="server" Height="27" CssClass="btn btn-primary btn-query" Text="查找" OnClick="btnQuery_Click" />
                        <cc2:btnReload ID="btnReload1" runat="server" />
                    </td>

                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table class="table table-bordered table-striped with-check table-select" border="1">
                <thead>
                    <tr>
                        <th>
                            <asp:CheckBox ID="chkAll" runat="server" /></th>
                        <th>渠道类型</th>
                        <th>渠道名称</th>
                        <th>新人</th>
                        <th>联系电话</th>
                        <th>录入日期</th>
                        <th>录入人</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>新人状态</th>
                        <th>沟通次数</th>
                        <th>上次沟通时间</th>
                        <th>说明</th>
                        <th>邀约人</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="RptTelemarketing" runat="server" OnItemDataBound="RepTelemarketing_ItemDataBound">
                        <ItemTemplate>
                            <tr skey='TelemarketingCustomerID<%#Eval("CustomerID") %>'>
                                <td style="text-align: center">
                                    <asp:CheckBox ID="chkSinger" CssClass="chkbox" runat="server" />
                                    <asp:HiddenField ID="hideCustomerID" runat="server" Value='<%#Eval("MarkeID") %>' />
                                </td>
                                <td><%#GetChannelTypeName(Eval("ChannelType")) %></td>
                                <td style="width: 130px">
                                    <lable style="cursor: default" title='<%#Eval("Channel") %>'><%#ToInLine(Eval("Channel")) %></lable>
                                </td>

                                <td><a target="_blank" href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#Eval("ContactMan") %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#GetDateStr(Eval("RecorderDate")) %></td>
                                <td><%#GetEmployeeName(Eval("CreateEmpLoyee")) %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td><%#GetFollowCount(Eval("CustomerID")) %></td>
                                <td><%#GetLastFollowdDate(Eval("CustomerID")) %></td>
                                <td>
                                    <lable style="cursor: default" title='<%#Eval("Other") %>'><%#ToInLine(Eval("Other")) %></lable>
                                </td>
                                <td><%#GetEmployeeName(Eval("EmployeeID")) %>
                                    <asp:LinkButton ID="lkbtnAssign" CssClass="btn btn-danger" CommandArgument='<%#Eval("MarkeID") %>' runat="server">分派</asp:LinkButton>
                                    <asp:LinkButton ID="lkbtnReassignment" CssClass="btn btn-primary" runat="server" CommandArgument='<%#Eval("CustomerID") %>'>改派</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <div style="width:100%;">
                <asp:Label runat="server" ID="lblCustomerSum" Text="" Style="font-weight: normal;color:#d74516;font-size:13px;float:left;margin-top:10px;" />
                <cc1:AspNetPagerTool ID="TelemarketingPager" AlwaysShow="true" runat="server" OnPageChanged="TelemarketingPager_PageChanged" ></cc1:AspNetPagerTool>
            </div>
            <br />

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
                    <asp:Button ID="btnBatchAssign" CssClass="btn btn-info" runat="server" Text="批量" OnClientClick="" OnClick="btnBatchAssign_Click" Visible="false" />&nbsp;
                    <asp:Button ID="btnSaveUpdate" Visible="false" runat="server" CssClass="btn btn-danger" Text="保存" OnClick="btnSaveUpdate_Click" />
                </div>
            </div>
            <a href="#employeeChoose" style="display: none;" id="choose"></a>
            <div id="employeeChoose" style="display: none;">
                批量操作框：
             <br />
                请选择员工
                <asp:DropDownList ID="ddlEmployee" CssClass="stat-boxes2" runat="server"></asp:DropDownList>
                <br />
                <asp:LinkButton ID="lkbtnBatch" runat="server" CssClass="btn btn-primary btn-mini" OnClick="lkbtnBatch_Click">确定</asp:LinkButton>
            </div>
        </div>
        <HA:MessageBoard runat="server" ClassType="FL_TelemarketingManager" ID="MessageBoard" />
    </div>
</asp:Content>
