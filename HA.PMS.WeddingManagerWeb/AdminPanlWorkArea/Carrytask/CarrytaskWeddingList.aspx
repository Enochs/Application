<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Default" CodeBehind="CarrytaskWeddingList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWeddingList" Title="婚礼统筹" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CstmNameSelector.ascx" TagPrefix="HA" TagName="CstmNameSelector" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <style type="text/css">
        #trContent th {
            text-align: left;
        }

        .tableStyle tr td {
            border: none;
        }
    </style>
    <script src="/Scripts/trselection.js"></script>
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
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div style="overflow-x: auto;">
        <div class="widget-box">

            <div class="widget-box" style="height: 30px; border: 0px;">
                <table>
                    <tr>
                        <td>
                            <HA:MyManager runat="server" ID="MyManager" Title="策划师" />
                        </td>
                        <td>
                            <HA:CstmNameSelector runat="server" ID="CstmNameSelector" />
                        </td>
                        <td>
                            <HA:DateRanger Title="婚期：" runat="server" ID="PartyDateRanger" />
                        </td>
                        <td>酒店：<cc2:ddlHotel ID="ddlHotel" runat="server"></cc2:ddlHotel></td>
                        <td>联系电话：
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="BtnQuery" CssClass="btn btn-primary" OnClick="BinderData" runat="server" Text="查询" />
                            <cc2:btnReload ID="btnReload" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr style="border: none; display: none">
                        <td style="border: none;" colspan="8">
                            <div style="text-align: center;">
                                <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-info" Text="保存" OnClick="btn_Save_Click" />
                                <asp:Button runat="server" ID="btn_Own" CssClass="btn btn-info" Text="派给自己" OnClientClick="return SaveCheck();" OnClick="btn_Own_Click" />
                            </div>
                        </td>
                    </tr>
                    <tr id="trContent">
                        <th style="white-space: nowrap;">
                            <input id="chkall" type="checkbox" onclick="Checkall(this);" /></th>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>分派时间</th>
                        <th>计划完成时间</th>
                        <th>完成时间</th>
                        <th>前期设计</th>
                        <th>执行设计</th>
                        <th>制作/查看婚礼统筹</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr skey='<%#Eval("CustomerID") %>' <%#Eval("DesignerState").ToString().ToInt32() == 1 ? "style='color:red'" : "" %>>
                                <td style="text-align: center">
                                    <input id="chkSinger" class="CustomerCheckBox chkbox" type="checkbox" value="<%#Eval("CustomerID") %>" /></td>
                                <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#GetShortDateString(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeName(Eval("OrderID")) %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#Eval("WorkCreateDates") == null ? "无" : Eval("WorkCreateDates","{0:yyyy-MM-dd}") %></td>
                                <td>
                                    <asp:Label runat="server" ID="lblPlanDate" onclick="WdatePicker();" Width="80px" Text='<%#string.Format("{0:yyyy-MM-dd}",Eval("PlanFinishDate")) %>' />
                                </td>
                                <td><%#string.Format("{0:yyyy-MM-dd}",Eval("AutualFinishDate")) %></td>
                                <td>
                                    <asp:Label runat="server" ID="lblEarlyDesigner" Text='<%#GetEarlyEmployee(Eval("CustomerID")) %>' />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="lblDesignEmployee" Text='<%#GetEmployeeName(Eval("DesignerEmployee")) %>' />
                                </td>
                                <td>
                                    <asp:LinkButton runat="server" ID="lbtnSave" Text="保存" CommandArgument='<%#Eval("QuotedID") %>' CommandName="Save" CssClass="btn btn-primary btn-mini" />
                                    <a href="CarrytaskWeddingPlanningCreate.aspx?OrderID=<%#Eval("OrderID") %>&CustomerID=<%#Eval("CustomerID") %>" class="btn btn-primary  btn-mini">制作/查看</a>
                                    <%-- <a href="CarrytaskWeddingPlanning.aspx?OrderID=<%#Eval("OrderID") %>&CustomerID=<%#Eval("CustomerID") %>" class="btn btn-primary  btn-mini">导出Excel</a>--%>
                                    <a target="_blank" href=" /AdminPanlWorkArea/QuotedPrice/QuotedPriceShow.aspx?QuotedID=<%#Eval("QuotedID") %>&OrderID=<%#Eval("OrderID") %>&CustomerID=<%#Eval("CustomerID") %>" class="btn btn-primary  btn-mini">报价单</a>
                                    <a target="_blank" class="btn btn-primary btn-mini" href="/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignCreate.aspx?OrderID=<%#Eval("OrderID") %>&IsFinish=<%#Eval("IsDispatching") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&Type=2">设计单</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="8">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="BinderData"></cc1:AspNetPagerTool>

                            <div style="text-align: center; display: none">
                                <asp:Button runat="server" ID="btnSaveDesigner" CssClass="btn btn-info" Text="保存" OnClick="btn_Save_Click" />
                                <asp:Button runat="server" ID="btnSaveOwner" CssClass="btn btn-info" Text="派给自己" OnClick="btn_Own_Click" OnClientClick="return SaveCheck()" />
                                <asp:HiddenField ID="hideKeyList" ClientIDMode="Static" runat="server" />
                            </div>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <div class="widget-box" style="display: none;">
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
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
