<%@ Page Title="" Language="C#" AutoEventWireup="true" StylesheetTheme="Default" CodeBehind="TaskWorkPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.TaskWorkPanel" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_MissionMananger.aspx";
        $(document).ready(function () {
            $("#tab1").click();
        });

        function Settabs(ControlID, Index, Uri) {
            URI = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/" + Uri + ".aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&WorkType=" + Index + "&OrderID=<%=Request["OrderID"]%>&QuotedID=<%=Request["QuotedID"]%>&NeedPopu=1";
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }

        function Settabs3(ControlID, Uri) {

            URI = "/AdminPanlWorkArea/QuotedPrice/" + Uri + ".aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%=Request["OrderID"]%>&QuotedID=<%=Request["QuotedID"]%>&NeedPopu=1";


            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }

        function Settabs4(ControlID, Uri) {
            URI = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/" + Uri + ".aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%=Request["OrderID"]%>&QuotedID=<%=Request["QuotedID"]%>&NeedPopu=1";
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }

        function Settabs5(ControlID, Uri) {

            URI = "/AdminPanlWorkArea/Carrytask/CarryCost/" + Uri + ".aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%=Request["OrderID"]%>&QuotedID=<%=Request["QuotedID"]%>&Type=Look&NeedPopu=1";
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }

        function Settabs6(ControlID, Uri) {

            URI = "/AdminPanlWorkArea/Carrytask/" + Uri + ".aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%=Request["OrderID"]%>&QuotedID=<%=Request["QuotedID"]%>&Type=Look&NeedPopu=1";
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }


        function ShowThis(KeyID, Control) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceShowOrPrint.aspx?CustomerID=<%=Request["CustomerID"]%>&NeedPopu=1";
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 1280, 1500, "a#" + $(Control).attr("id"));
        }

        //点击文本框 弹出部门人员列表
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=1&ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 480, 380, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

    </script>
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
    <div class="look_order">
        <table class="table table-bordered">
            <tr>
                <td><a target="_blank" class="btn btn-primary" href="/AdminPanlWorkArea/QuotedPrice/QuotedPriceShowOrPrint.aspx?CustomerID=<%=Request["CustomerID"]%>&OrderID=<%=Request["OrderID"]%>&QuotedID=<%=Request["QuotedID"]%>&IsFirstMake=0&NeedPopu=1">查看原始订单</a>
                    <a <%=HideChange() %> target="_blank" class="btn btn-primary" href="/AdminPanlWorkArea/QuotedPrice/QuotedManager/QuotedPriceChange.aspx?CustomerID=<%=Request["CustomerID"]%>&OrderID=<%=Request["OrderID"]%>&QuotedID=<%=Request["QuotedID"]%>&Type=Look">查看变更单</a>
                </td>

                <td class="widget-content nopadding" id="paretntSelect">
                    <input runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowPopu(this);" type="text" />
                    <a href="#" onclick="ShowPopu(this);" class="btn btn-primary">改派设计师</a>
                    <asp:Button runat="server" ID="btnSaveDesigner" CssClass="btn btn-info" Text="确定" OnClick="btnSaveDesigner_Click" />
                    <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
                </td>
                <%--<td>
                    <asp:LinkButton runat="server" ID="lbtnQuotedEmployee" Text="改派策划师" CssClass="btn btn-primary" /></td>--%>
            </tr>
        </table>
    </div>
    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li class="HAtab" id="tab1" style="background-color: #2E363F;" id="DefaultTab" onclick="Settabs(this,'花艺','TaskWork');"><a data-toggle="tab" href="#111">花艺</a></li>
                    <li class="HAtab" id="tab2" onclick="Settabs(this,'道具','TaskWork');"><a data-toggle="tab" href="#111">道具</a></li>
                    <li class="HAtab" id="tab3" onclick="Settabs(this,'灯光','TaskWork');"><a data-toggle="tab" href="#111">灯光</a></li>
                    <li class="HAtab" id="tab4" onclick="Settabs(this,'人员','TaskWork');"><a data-toggle="tab" href="#111">人员</a></li>
                    <li class="HAtab" id="tab5" onclick="Settabs(this,'其它','TaskWork');"><a data-toggle="tab" href="#111">其它</a></li>
                    <%--<li class="HAtab" id="Li6" onclick="Settabs3(this,'QuotedPriceChangeList');"><a data-toggle="tab" href="#111">项目变更单</a></li>--%>
                    <li class="HAtab" id="Li5" onclick="Settabs4(this,'DesignclassReports');"><a data-toggle="tab" href="#111">设计类清单</a></li>
                    <li class="HAtab" id="Li4" onclick="Settabs5(this,'OrderCost');"><a data-toggle="tab" href="#111">成本明细</a></li>
                    <li class="HAtab" id="Li3" onclick="Settabs6(this,'AllCarryTaskShow');"><a data-toggle="tab" href="#111">类型</a></li>
                </ul>
            </div>
            <div class="widget-content tab-content" style="height: 800px; overflow: auto">

                <div class="tab-pane active" id="tab9">
                    <iframe class="framchild " id="Iframe1" width="100%" style="min-height: 700px" frameborder="0" name="table"></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
