<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OtherPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Other.OtherPanel" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_MissionMananger.aspx";
        $(function () { $("#tab1").click(); });
        function Settabs(ControlID, Index, Uri) {
            URI = "/AdminPanlWorkArea/CS/Member/Other/" + Uri + ".aspx?TypeID=" + Index;
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }
    </script>
    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li class="HAtab" id="Li5" style="background-color: #2E363F;" onclick="Settabs(this,5,'OtherallCreate');"><a data-toggle="tab" href="#111">制定延伸服务计划</a></li>
                    <li class="HAtab" id="tab1" style="background-color: #2E363F;" onclick="Settabs(this,1,'Otherall');"><a data-toggle="tab" href="#111">本周完成</a></li>
                    <li class="HAtab" id="Li1" style="background-color: #2E363F;" onclick="Settabs(this,2,'Otherall');"><a data-toggle="tab" href="#111">下周完成</a></li>
                    <li class="HAtab" id="Li2" style="background-color: #2E363F;" onclick="Settabs(this,3,'Otherall');"><a data-toggle="tab" href="#111">本月完成</a></li>
                    <li class="HAtab" id="Li3" style="background-color: #2E363F;" onclick="Settabs(this,4,'Otherall');"><a data-toggle="tab" href="#111">下月完成</a></li>
                    <li class="HAtab" id="Li4" style="background-color: #2E363F;" onclick="Settabs(this,5,'Otherall');"><a data-toggle="tab" href="#111">延伸服务明细</a></li>
                </ul>
            </div>
            <div class="widget-content tab-content" style="height: 800px; overflow: scroll">
                <div class="tab-pane active" id="Div1">
                    <iframe class="framchild " id="Iframe1" width="100%" height="1900" frameborder="0" name="table"></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

