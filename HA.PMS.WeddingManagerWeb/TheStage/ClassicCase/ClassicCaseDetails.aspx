<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassicCaseDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.ClassicCase.ClassicCaseDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- CSS -->
    <link rel="stylesheet" type="text/css" media="all" href="style.css" />
    <link rel="stylesheet" type="text/css" media="screen and (max-width: 980px)" href="css/lessthen980.css" />
    <link rel="stylesheet" type="text/css" media="screen and (max-width: 600px)" href="css/lessthen600.css" />
    <link rel="stylesheet" type="text/css" media="screen and (max-width: 480px)" href="css/lessthen480.css" />
    <link rel="stylesheet" href="css/shop.css" type="text/css" media="all" />
    <link rel="stylesheet" href="css/prettyPhoto.css" type="text/css" media="all" />
    <link rel="stylesheet" href="css/tipsy.css" type="text/css" media="all" />
    <link rel='stylesheet' href='css/jquery-rotating.css' type='text/css' media='all' />
    <link rel='stylesheet' href='css/slider-cycle.css' type='text/css' media='all' />
    <!-- SCRIPTS -->
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery.prettyPhoto.js"></script>
    <script type="text/javascript" src="js/jquery.tipsy.js"></script>
    <script type="text/javascript" src="js/jquery.nivo.slider.pack.js"></script>
    <script type="text/javascript" src="js/jquery.cycle.min.js"></script>
    <script type="text/javascript" src="js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="js/comment-reply.js"></script>
    <script type="text/javascript" src="../../App_Themes/PersonEmployee/js/jquery.fancybox-1.3.4.pack.js"></script>
    <link href="../../App_Themes/PersonEmployee/css/fancybox.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" media="all" href="css/home.css" />


    <script src="../../Scripts/jquery.js"></script>
    <script src="../../Scripts/vtip-min.js"></script>

    <style type="text/css">
        .btncls {
            background-color: #6bcdf8;
            color: #fff;
            width: 90px;
            height: 30px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.movie').fancybox({
                'transitionIn': 'elastic',
                'transitionOut': 'elastic',
                'speedIn': 200,
                'speedOut': 200,
                'overlayOpacity': 0.6,
                'type': 'iframe',
                'height': 600,
                'width': 1024
            });
            /* $("#designnotes").mouseenter(function () { $(this).css("overflow-y", "scroll"); })*/
            /* .mouseleave(function () { $(this).css("overflow-y", "hidden"); });*/
        });
    </script>
</head>
<body class="responsive boxed-layout no_js">
    <form id="form1" runat="server">
        <!-- START LIGHT WRAPPER -->
        <div class="bgLight group">
            <!-- START WRAPPER -->
            <div class="wrapper group">
                <!-- START BG WRAPPER -->
                <div class="bgWrapper group">
                    <!-- START HEADER -->
                    <div id="header" class="group">
                        <!-- .inner -->
                        <div class="inner group">
                            <!-- START LOGO -->
                            <div id="logo" class="group">
                                <p class="logo-description">&nbsp;</p>
                            </div>
                            <!-- END LOGO -->
                            <div class="clear"></div>
                        </div>
                        <!-- end .inner -->
                    </div>
                    <!-- END HEADER -->
                    <!-- START PRIMARY SECTION -->
                    <div id="primary" class="inner group" />
                    <div class="layout-sidebar-no group">
                        <div id="container">
                            <div id="content" role="main">
                                <div id="breadcrumb">
                                    <a class="home" href="ClassicCaseList.aspx">返回 &rsaquo; 经典案例</a>
                                </div>
                                <div class="product type-product status-publish hentry">
                                    <div class="images">
                                        <a href='<%=ViewState["imgTop"] %>' class="zoom" rel="prettyphoto[gallery]">
                                            <img src="<%=ViewState["imgTop"] %>" class="attachment-530x345 wp-post-image" width="530" height="345" />
                                        </a>
                                        <strong style="color: green;">精彩图片</strong>
                                        <div class="thumbnails" style="width: 1024px;">
                                            <asp:Repeater ID="rptList" runat="server">
                                                <ItemTemplate>
                                                    <a href='<%#GetImgPath(Eval("CaseFilePath")) %>' title="albatros_niwa_001_big" rel="prettyphoto[gallery]" class="zoom first">
                                                        <asp:Image ID="img" ImageUrl='<%#Eval("CaseFilePath") %>' Width="90" Height="90" CssClass="attachment-90x90" runat="server" />
                                                    </a>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>

                                    <div class="summary">
                                        <h1 class="product_title page-title">
                                            <asp:Literal ID="ltlName" runat="server"></asp:Literal>
                                        </h1>
                                        <p>
                                            <asp:Literal ID="ltlPackageDetails" runat="server"></asp:Literal>
                                            <em></em>
                                        </p>
                                        <ul>
                                            <li>风格：<asp:Literal ID="ltlStyle" runat="server"></asp:Literal></li>
                                            <li>酒店：<asp:Literal ID="ltlHotel" runat="server"></asp:Literal></li>
                                            <li>新人：<asp:Literal ID="ltlGroom" runat="server"></asp:Literal></li>
                                            <li>策划手记：<br />
                                                <div id="designnotes">
                                                    <asp:Label ID="lblNotes" runat="server"></asp:Label>
                                                </div>
                                            </li>
                                        </ul>

                                    </div>
                                    <div class="summary">
                                        <ul>
                                            <li>视频：
                                                <ul>
                                                    <asp:Repeater ID="rptMovieList" runat="server">
                                                        <ItemTemplate>
                                                            <li style="width: 250px">
                                                                <a class="movie" href="ClassicMoviePlay.aspx?CaseFileId=<%#Eval("CaseFileId") %>">
                                                                    <%#Eval("CaseFileName").ToString().Length > 15 ? Eval("CaseFileName").ToString().Substring(0,15) : Eval("CaseFileName").ToString() %>
                                                                </a>
                                                                <a href='/Files/TheCase/TheCaseMovie/<%#Eval("CaseFileName") %>'>下载</a>
                                                                <br />
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </li>
                                        </ul>
                                    </div>
                                    <div runat="server" id="div_Details" class="summary">
                                        <ul>
                                            <li>

                                                <a id="a_details" class="movie" href='/TheStage/ClassicCase/ClassicMoviePlay.aspx?Action=More&CaseID=<%=Request["CaseID"]%>' style="color: burlywood;">
                                                    <strong>更多详情</strong></a>
                                                <asp:Label runat="server" ID="lblDetails"></asp:Label>

                                            </li>
                                        </ul>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- END PRIMARY SECTION -->
                        <!-- START NEWSLETTER FORM -->
                        <div id="newsletter-form" class="group">
                            <div class="inner">
                                <div class="newsletter-section group">
                                    <fieldset>
                                        <ul class="group">
                                            <li style="list-style: none;">
                                                <label for="fullname">用户名：</label>
                                                <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox>
                                            </li>
                                            <li style="list-style: none;">
                                                <label for="password">密&nbsp;&nbsp;&nbsp;&nbsp;码：</label>
                                                <asp:TextBox TextMode="Password" ID="txtpassword" runat="server"></asp:TextBox>
                                            </li>
                                            <li style="list-style: none;">
                                                <asp:Button ID="btnLogin" CssClass="btncls" OnClick="btnLogin_Click" runat="server" Text="登陆" />
                                            </li>
                                        </ul>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                        <!-- ENDSTART NEWSLETTER FORM -->
                        <!-- START COPYRIGHT -->
                        <%--<div id="copyright" class="group two-columns">
<div class="inner group">
<p class="right">
版权所有：<a href="http://www.holdlove.cn" target="_blank"><strong>重庆好爱信息技术发展有限公司</strong></a>
</p>
</div>
</div>--%>
                        <!-- END COPYRIGHT -->
                    </div>
                    <!-- END BG WRAPPER -->
                </div>
                <!-- END WRAPPER -->
            </div>
            <!-- END LIGHT WRAPPER -->
            <script type="text/javascript" src="js/jquery.custom.js"></script>
            <script type="text/javascript" src="js/contact.js"></script>
    </form>
</body>
</html>
