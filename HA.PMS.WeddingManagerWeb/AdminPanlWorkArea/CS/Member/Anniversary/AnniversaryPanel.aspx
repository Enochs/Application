<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AnniversaryPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Anniversary.AnniversaryPanel" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_MissionMananger.aspx";
        $(function () { $("#tab1").click(); });
        function Settabs(ControlID, Index, Uri) {
            URI = "/AdminPanlWorkArea/CS/Member/Anniversary/" + Uri + ".aspx?Typer=" + Index;
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }
    </script>
    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li class="HAtab" id="tab1" style="background-color: #2E363F;" id="DefaultTab" onclick="Settabs(this,1,'AnniversarybyAll');"><a data-toggle="tab" href="#111">一周内的会员周年服务</a></li>
                    <li class="HAtab"  id="Li1" style="background-color: #2E363F;display:none;" id="DefaultTab" onclick="Settabs(this,2,'AnniversarybyAll');"><a data-toggle="tab" href="#111">下周周年服务</a></li>
                    <li class="HAtab" id="tab2" style="display:none;" onclick="Settabs(this,3,'AnniversarybyAll');"><a data-toggle="tab" href="#111">所有会员周年服务</a></li>
                    <li class="HAtab" id="tab3" style="display:none;" onclick="Settabs(this,1,'AnniversaryContentList');"><a data-toggle="tab" href="#111">服务明细</a></li>
                </ul>
            </div>
            <div class="widget-content tab-content" style="height:800px; overflow:scroll">
                <div class="tab-pane active" id="Div1" >
                    <iframe class="framchild " id="Iframe1" width="100%"  height="1900"  frameborder="0" name="table"></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content> 
