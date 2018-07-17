<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FL_CustomerReturnVisitManagerNot.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.ReturnVisit.FL_CustomerReturnVisitManagerNot" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>


<%@ Register Src="../../../Control/DateRanger.ascx" TagName="DateRanger" TagPrefix="uc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>

    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 25px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }

        #tbl_Back tr td {
            text-align: center;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {
                if (indexs != 3) {
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


            $("html").css({ "overflow-x": "hidden" });

        });

        //点击文本框 弹出部门人员列表
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=1&ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 480, 380, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div style="overflow-x: auto;">
        <div class="widget-box">

            <div class="widget-box" style="height: 75px; border: 0px;">

                <table class="table">
                    <tr>

                        <td>姓名:
                            <asp:TextBox ID="txtGroom" runat="server"></asp:TextBox>
                        </td>
                        <td>电话:<asp:TextBox ID="txtGroomCellPhone" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <uc1:DateRanger ID="DateRanger" Title="到店时间:" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <HA:MyManager runat="server" ID="MyManager" Title="录入人:" />
                        </td>
                        <td>状态:<cc2:ddlCustomersState runat="server" ID="ddlCustomerState" /></td>
                        <td>
                            <asp:Button ID="btnQuery" ClientIDMode="Static" runat="server" CssClass="btn btn-primary" Text="查找" OnClick="btnQuery_Click" />
                            <cc2:btnReload ID="btnReload" runat="server" />
                        </td>
                    </tr>
                </table>


            </div>

            <table id="tbl_Back" class="table table-bordered table-striped table-select">
                <thead>
                    <tr id="trContent">
                        <th>姓名</th>
                        <th>联系方式</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>到店时间</th>
                        <th>跟单人</th>
                        <th>新人状态</th>
                        <th>下次回访时间</th>

                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptReturn" runat="server" OnItemCommand="rptReturn_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                <td><a href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>" target="_blank"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#Eval("OrderCreateDate","{0:yyyy-MM-dd}") %></td>
                                <td><%#GetEmployeeName(Eval("OrderEmployee")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtNextReturnDate" onclick="WdatePicker();" Text='<%#Eval("NextReturnDate","{0:yyy-MM-dd}") == null ? "" : Eval("NextReturnDate","{0:yyy-MM-dd}") %>' ClientIDMode="Static" />
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnSave" CssClass="btn btn-primary btn-mini" CommandArgument='<%#Eval("CustomerID") %>' CommandName="Save" Text="保存" ClientIDMode="Static" />
                                    <a target="_blank" href="FL_ReturnVisitManagerUpdate.aspx?CustomerID=<%#Eval("CustomerID") %>" class="btn btn-primary">记录回访信息</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <script type="text/javascript">
                $(document).ready(function () {
                    $("#btnSave").click(function () {
                        if ($("#txtNextReturnDate").val() == "") {
                            alert("请选择下次回访时间");
                            return false;
                        }
                    });
                });
            </script>

            <cc1:AspNetPagerTool ID="ReturnPager" OnPageChanged="ReturnPager_PageChanged" runat="server">
            </cc1:AspNetPagerTool>

            <HA:MessageBoard runat="server" ClassType="FL_CustomerReturnVisitManager" ID="MessageBoard" />
        </div>
    </div>

</asp:Content>
