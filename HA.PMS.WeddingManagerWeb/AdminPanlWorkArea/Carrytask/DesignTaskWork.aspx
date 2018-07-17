<%@ Page Title="" Language="C#" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="DesignTaskWork.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.DesignTaskWork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
    <%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
    <div class="look_order">
        <table>
            <tr>
                <td><a target="_blank" class="btn btn-primary" href="/AdminPanlWorkArea/QuotedPrice/QuotedPriceShowOrPrint.aspx?CustomerID=<%=Request["CustomerID"]%>&OrderID=<%=Request["OrderID"]%>&QuotedID=<%=Request["QuotedID"]%>&NeedPopu=1">查看原始订单</a></td>
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
