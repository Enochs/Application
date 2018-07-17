<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompanyDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.CompanyIntroduction.CompanyDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            //加载对应图片浏览效果 begin
            $(".grouped_elements").each(function (indexs, values) {
                var imgChildren = $(this).children("img");
                $(this).attr("href", imgChildren.attr("src"));
            });
            $("a.grouped_elements").fancybox();
            $("html,body").css({ "width": "100%", "height": "100%", "background-color": "#fff" });
        });
    </script>
    <style type="text/css">
        #ulImg li {width: 320px;height: 170px;margin-right: 10px;list-style:none;float:left;margin-bottom:10px;margin-top:10px}
        #ulImg {height:100%}body {height:100%;background-color:#fff}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
<span style="color:green;">公司介绍中的图片存放地址是  &nbsp; 软件安装\Files\CompanyIntroduction\</span><br />
<div style="margin-left: auto;margin-right: auto; text-align:center;"><ul id="ulImg"><asp:Repeater ID="rptLists" runat="server"><ItemTemplate><li><a class="grouped_elements" href="#" rel="group1"><img style="width:300px; height:160px;" src='/Files/CompanyIntroduction/<%#Eval("Name") %>' width="300" height="160" alt="Alternate Text" /></a></li></ItemTemplate></asp:Repeater></ul></div>
</asp:Content>
