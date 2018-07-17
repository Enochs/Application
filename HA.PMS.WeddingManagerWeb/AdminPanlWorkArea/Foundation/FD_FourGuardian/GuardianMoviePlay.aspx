<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="GuardianMoviePlay.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian.GuardianMoviePlay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--    <link href="http://vjs.zencdn.net/4.0/video-js.css" rel="stylesheet" />
    <script src="http://vjs.zencdn.net/4.0/video.js"></script>--%>



    <script type="text/javascript">

        $(document).ready(function () {
            $("#videoPlay").dblclick(function () {
                // $(this).attr("", "");
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    你当前正在观看的是: <span style="color: green;">
        <asp:Literal ID="ltlName" runat="server"></asp:Literal></span>
    <br />
<%--    <video id="my_video_1" class="video-js vjs-default-skin" controls="controls" autoplay="autoplay" controlspreload="auto" width="640" height="264" poster="my_video_poster.png" data-setup="{}">

        <source src='<%=ViewState["MoviePath"] %>' type='video/mp4' />



    </video>--%>
    <video id="videoPlay" controls="controls" autoplay="autoplay" class="projekktor" width="500" height="450" poster='<%=ViewState["MovieTopImagePath"] %>'>
            <source src='<%=ViewState["MoviePath"] %>' />

        </video>
</asp:Content>
