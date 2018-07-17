<%@ Page Title="设计清单" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="DesignList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass.DesignList" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .table-striped tr td {
            border: 1px solid none;
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

            //改派其他
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


        //上传图片
        function ShowFileUploadPopu(Control) {
            var Url = Control;
            showPopuWindows(Url, 400, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        $(document).ready(function () {
            $("#trContent th").css({ "white-space": "nowrap" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
            <table>
                <tr>
                    <td runat="server" id="td_Type">用户类型:
                        <asp:DropDownList runat="server" ID="ddlEmployeeTypes">
                            <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="设计师" Value="DesignerEmployee" />
                            <asp:ListItem Text="策划师" Value="EmployeeID" />
                        </asp:DropDownList>
                    </td>
                    <td runat="server" id="td_Type1">
                        <HA:MyManager runat="server" ID="MyManager" Title="责任人" />
                    </td>
                    <td>新人姓名:<asp:TextBox ID="txtBride" runat="server" MaxLength="10"></asp:TextBox>
                    </td>
                    <td>酒店:<cc2:ddlHotel ID="ddlHotel" runat="server">
                    </cc2:ddlHotel>
                    </td>
                    <td>联系电话:
                        <asp:TextBox ID="txtCellPhone" runat="server"></asp:TextBox>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" Title="婚期:" />
                    </td>
                    <td>状态:
                        <asp:DropDownList runat="server" ID="ddlState">
                            <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="未完成" Value="1" />
                            <asp:ListItem Text="已完成" Value="2" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <cc2:btnManager ID="btnSerch" runat="server" OnClick="btnSerch_Click" />
                        <cc2:btnReload ID="btnReload" runat="server" />
                    </td>
                </tr>
            </table>


            <table class="table table-bordered table-striped">
                <thead>
                    <tr style="border: none;">
                        <td colspan="10">
                            <div style="text-align: center; display: none;">
                                <asp:Button runat="server" ID="btn_Save" CssClass="btn btn-info" Text="保存" OnClick="btn_Save_Click" />
                                <asp:Button runat="server" ID="btn_Own" CssClass="btn btn-info" Text="派给自己" OnClientClick="return SaveCheck();" OnClick="btn_Own_Click" />
                            </div>
                        </td>
                    </tr>
                    <tr id="trContent">
                        <th style="white-space: nowrap;">
                            <input id="chkall" type="checkbox" onclick="Checkall(this);" class="input btnSaveSubmit<%#Eval("DesignerEmployee") %> btnSumbmit" /></th>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>婚礼顾问</th>
                        <th>婚礼策划</th>
                        <th>接收时间</th>
                        <th>计划完成时间</th>
                        <th>完成时间</th>
                        <th>设计师</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repCustomer" runat="server" OnItemDataBound="repCustomer_ItemDataBound">
                        <ItemTemplate>
                            <tr skey='QuotedPriceQuotedID<%#Eval("QuotedID") %>' <%#ChangeColorForMoney(Eval("QuotedID")) %>>
                                <td style="text-align: center">
                                    <input id="chkSinger" class="CustomerCheckBox chkbox btnSaveSubmit<%#Eval("DesignerEmployee") %> btnSumbmit" type="checkbox" value="<%#Eval("CustomerID") %>" /></td>
                                <td><a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a>
                                    <asp:Image runat="server" ID="ImgIcon" ImageUrl="~/Images/vipIcon.jpg" />
                                    <asp:HiddenField runat="server" ID="HideCustomerID" Value='<%#Eval("CustomerID") %>' />
                                </td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetOrderEmpLoyeeNameByCustomerID(Eval("CustomerID")) %></td>
                                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                                <td><%#Eval("DesignCreateDate") == null ? "无" : Eval("DesignCreateDate","{0:yyyy-MM-dd}") %></td>
                                <td><%#string.Format("{0:yyyy-MM-dd}",Eval("PlanFinishDate")) %></td>
                                <td><%#string.Format("{0:yyyy-MM-dd}",Eval("AutualFinishDate")) %></td>
                                <td id="Partd<%#Container.ItemIndex %>">
                                    <%--<input runat="server" style="margin: 0; width: 90px;" id="txtEmpLoyee" class="txtEmpLoyeeName" type="text" value='<%#GetEmployeeName(Eval("DesignerEmployee")) %>' readonly="true" />--%>
                                    <asp:TextBox runat="server" ID="txtEmployees" CssClass="txtEmpLoyeeName" onclick="ShowPopu(this);" ClientIDMode="Static" Text='<%#GetEmployeeName(Eval("DesignerEmployee")) %>' Style="margin: 0; width: 90px;" />
                                    <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='' runat="server" />
                                    <asp:HiddenField ID="hideCustomerHide" Value='<%#Eval("CustomerID") %>' runat="server" />
                                </td>
                                <td style="white-space: nowrap;">
                                    <a target="_blank" class="btn btn-primary btn-mini <%--btnSaveSubmit<%#Eval("DesignerEmployee") %> btnSumbmit--%> " href="/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignCreate.aspx?OrderID=<%#Eval("OrderID") %>&IsFinish=<%#Eval("IsDispatching") %>&QuotedID=<%#Eval("QuotedID") %>&CustomerID=<%#Eval("CustomerID") %>&Type=1">设计清单</a>
                                    <a target="_blank" href="/AdminPanlWorkArea/QuotedPrice/QuotedPriceShow.aspx?QuotedID=<%#GetQuotedID(Eval("OrderID")) %>&OrderID=<%#Eval("OrderID") %>&CustomerID=<%#Eval("CustomerID") %>" class="btn btn-primary  btn-mini <%#GetRemoveClassByOrderID(Eval("OrderID")) %>">报价单</a>
                                    <a target="_blank" href='../../DesignTaskWork.aspx?DispatchingID=<%#GetDisID(Eval("CustomerID")) %>&CustomerID=<%#Eval("CustomerID") %>&OrderID=<%#Eval("OrderID") %>&QuotedID=<%#Eval("QuotedID") %>&NeedPopu=1&PageName=ProductList' class="btn btn-primary btn-mini">派工单</a>
                                </td>
                            </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="12">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
                            <div style="text-align: center;">
                                <asp:Button runat="server" ID="btnSaveDesigner" CssClass="btn btn-info" Text="保存" OnClick="btn_Save_Click" />
                                <asp:Button runat="server" ID="btnSaveOwner" CssClass="btn btn-info" Text="派给自己" OnClick="btn_Own_Click" OnClientClick="return SavetoOtherEmpLoyee();" />
                                <asp:HiddenField ID="hideKeyList" ClientIDMode="Static" runat="server" />
                            </div>
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
                    <asp:Button ID="btnSaveOther" ClientIDMode="Static" CssClass="btn btn-info" runat="server" Text="派给其他人" OnClick="btnSaveOther_Click" OnClientClick="return SavetoOtherEmpLoyee();" />
                    选择其他:<input runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowPopu(this);" type="text" />
                    <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
                </div>
            </div>



            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
