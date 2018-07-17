<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseDisposibleProductPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseDisposibleProductPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () { $("#lfdsol1").click(); });
        function Settabs(ControlID, Index, Name) {
            var URI = "/AdminPanlWorkArea/Foundation/FD_Storehouse/" + Name + ".aspx?Index=" + Index + "&NeedPopu=1";
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li class="HAtab" id="lfdsol1" style="background-color: #2E363F;" onclick="Settabs(this,1,'FD_StorehouseDisposibleProductList');"><a data-toggle="tab" href="#111">入库记录</a></li>
                    <li class="HAtab" id="lfdsol2" style="background-color: #2E363F;" onclick="Settabs(this,2,'FD_StorehouseDisposibleProductOutList');"><a data-toggle="tab" href="#111">出库记录</a></li>
                </ul>
            </div>
            <div class="widget-content tab-content" style="height:800px; overflow:scroll">
                <div class="tab-pane active" id="Div1" >
                    <iframe class="framchild " id="Iframe1" width="100%"  height="1080" frameborder="0" name="table"></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
