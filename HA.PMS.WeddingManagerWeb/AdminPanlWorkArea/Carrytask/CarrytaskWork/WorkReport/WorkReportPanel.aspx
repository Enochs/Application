<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="WorkReportPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.WorkReport.WorkReportPanel" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_MissionMananger.aspx";
        $(document).ready(function () {
<%--            var type = '<%=Request["Type"]%>';
            
            if (type == "Prop") {
                URI = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/WorkReport/PurchaseReport.aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%#Request["OrderID"]%>&NeedPopu=1";
                ("#Iframe1").attr("src", URI);
            } else {
                URI = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/FlowerReport/FlowerReport.aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%#Request["OrderID"]%>&NeedPopu=1";
                ("#Iframe1").attr("src", URI);
            }
        --%>
        });

        function Settabs(ControlID, Uri) {
            URI = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/WorkReport/" + Uri + ".aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%#Request["OrderID"]%>&NeedPopu=1";
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }

        function Settabs1(ControlID, Uri) {
            URI = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/FlowerReport/" + Uri + ".aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%#Request["OrderID"]%>&NeedPopu=1";
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }
        function Settabs2(ControlID, Uri) {
            URI = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/" + Uri + ".aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%#Request["OrderID"]%>&NeedPopu=1";
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }

        function Settabs3(ControlID, Uri) {

            URI = "/AdminPanlWorkArea/Carrytask/CarryCost/" + Uri + ".aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%#Request["OrderID"]%>&NeedPopu=1";
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }


    </script>
    <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
<%--    <div class="row-fluid">--%>
        <div class="widget-box">
            <div  class="widget-title">
                <ul class="nav nav-tabs">
                    <%--<li class="HAtab" id="tab1" style="background-color: #2E363F;" id="DefaultTab" onclick="Settabs(this,'StorehouseReport');"><a data-toggle="tab" href="#111">领料单</a></li>
                    <li class="HAtab" id="tab3" onclick="Settabs(this,'TeamReport');"><a data-toggle="tab" href="#111">专业人员</a></li>--%>
                    <li class="HAtab" id="Li1" runat="server" onclick="Settabs(this,'PurchaseReport');"><a data-toggle="tab" href="#111">采购清单</a></li>
                    <li class="HAtab" id="Li3" runat="server" onclick="Settabs1(this,'FlowerReport');"><a data-toggle="tab" href="#111">花艺采购单</a></li>

                </ul>
            </div>
            <div class="widget-content tab-content" style="max-height: 550px;">
                <div class="tab-pane active" id="tab9">
                    <iframe class="framchild" id="Iframe1" runat="server"  width="100%" style="min-height: 500px" frameborder="0" name="table"></iframe>
                </div>
            </div>
        </div>
    <%--</div>--%>
</asp:Content>
