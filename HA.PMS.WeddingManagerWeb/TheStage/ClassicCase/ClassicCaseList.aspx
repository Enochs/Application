<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassicCaseList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.ClassicCase.ClassicCaseList" %>

<!DOCTYPE html>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>经典案例</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width" />
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
    <!-- [favicon] begin -->
    <link rel="shortcut icon" type="image/x-icon" href="favicon.ico" />
    <link rel="icon" type="image/x-icon" href="favicon.ico" />
    <!-- [favicon] end -->
    <!-- FONT -->

    <!-- SCRIPTS -->
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery.prettyPhoto.js"></script>
    <script type="text/javascript" src="js/jquery.tipsy.js"></script>
    <script type="text/javascript" src="js/jquery.tweetable.js"></script>
    <script type="text/javascript" src="js/jquery.nivo.slider.pack.js"></script>
    <script type="text/javascript" src="js/jquery.cycle.min.js"></script>
    <script type="text/javascript" src="js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="js/comment-reply.js"></script>
    <script type='text/javascript' src="js/jquery.RotateImageMenu.js"></script>
    <style type="text/css">
        #nav.elegant ul li a:hover {
            color: #d7cc96;
        }

        #nav.elegant ul.sub-menu li a:hover, #nav.elegant ul.children li a:hover {
            color: #fff;
        }

        #nav.elegant ul.sub-menu, #nav.elegant ul.children {
            background-color: #624e2f;
        }

        #nav.elegant .megamenu ul.sub-menu li {
            border-left-color: #745d39;
        }

            #nav.elegant ul.sub-menu li:hover, #nav.elegant ul.children li:hover, #nav.elegant .megamenu ul.sub-menu li ul li:hover {
                background-color: #745d39;
            }

        #nav.elegant .megamenu > ul.sub-menu > li > a {
            color: #d7cc96;
        }

        span.onsale {
            background-color: #ba7601;
        }

        .products li .buttons a.add-to-cart {
            background-color: #967d57;
        }

        .products li .buttons a.details {
            background-color: #6b512d;
        }

        .products li .buttons a.add-to-cart:hover {
            background-color: #b29b78;
        }

        .products li .buttons a.details:hover {
            background-color: #8d6e42;
        }

        #newsletter-form .newsletter-section form ul li input.text-field {
            background-color: #d5c6a1;
        }

        #newsletter-form .newsletter-section form ul li input.submit-field {
            background-color: #b4a58a;
        }

            #newsletter-form .newsletter-section form ul li input.submit-field:hover {
                background-color: #b69a68;
            }

        #footer .footer-sidebar, #copyright .inner {
            border-color: #D1D2D2;
        }

        #slider ul li .slider-caption h2 a, #slider ul li .slider-caption h2 a:hover {
            color: #fff;
        }

        #content {
            width: 750px;
        }

        #sidebar {
            width: 170px;
        }

            #sidebar.shop {
                width: 170px;
            }

        .products li {
            width: 164px !important;
        }

            .products li a strong {
                width: 120px !important;
            }

                .products li a strong.inside-thumb {
                    bottom: 0px !important;
                }

            .products li.shadow a strong.inside-thumb {
                bottom: 21px !important;
            }

            .products li.border a strong.inside-thumb {
                bottom: 7px !important;
            }

            .products li.border.shadow a strong.inside-thumb {
                bottom: 28px !important;
            }

            .products li a img {
                width: 150px !important;
                height: 150px !important;
            }

        div.product div.images {
            width: 56.6666666667%;
        }

            div.product div.images img {
                width: 530px;
            }

        .layout-sidebar-no div.product div.summary {
            width: 41.25%;
        }

        .layout-sidebar-right div.product div.summary, .layout-sidebar-left div.product div.summary {
            width: 24.8%;
        }

        body, .stretched-layout .bgWrapper {
            background: #ffffff url('images/backgrounds/backgrounds/020.jpg') fixed center center;
        }

        #header {
            background: #000000 url('images/backgrounds/backgrounds/008.jpg') no-repeat top center;
        }

        .wrapper-content {
            width: 750px;
        }

        #logo .logo-title, .logo {
            font-family: 'Lobster' !important;
        }

        h1, h2, h3, h4, h5, h6, .special-font {
            font-family: 'Yanone Kaffeesatz' !important;
        }

        #slogan h1 {
            font-family: 'Yanone Kaffeesatz' !important;
        }

        p, li {
            font-family: 'Trebuchet MS', Helvetica, sans-serif !important;
        }

        form div table tr td .btn {
            color: white;
            background-color: #5084d8;
            border: 1px solid gray;
            width: auto;
            height: 25px;
            cursor: pointer;
        }

            form div table tr td .btn:hover {
                color: white;
                background-color: #2570e8;
            }

        /* 作品列表 */
        .imgEval {
            width: 140px;
            height: 100px;
            border: 1px solid black;
        }
    </style>
</head>
<body class="responsive boxed-layout no_js">
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
                            <p class="logo-description"></p>
                        </div>
                        <!-- END LOGO -->
                        <div class="clear"></div>
                    </div>
                    <!-- end .inner -->
                </div>
                <!-- END HEADER -->
                <div id="rotating-slider">
                    <!-- ROTAING SLIDER -->
                    <div class="content">
                        <div style="font-size: 12px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;友情版块>>  <a href="../Hotel/HotelList.aspx">酒店列表</a>
                        </div>
                        <div class="rm_wrapper">
                            <div id="rm_container">
                                <table style="border: 0px solid white;">
                                    <tr>
                                        <td>
                                            <h1>最新案例精选</h1>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ul>
                                                <asp:Repeater ID="rptCelePackageTop" runat="server">
                                                    <ItemTemplate>
                                                        <li style="background-color: transparent; list-style: none; float: left; width: 50%">
                                                            <a href="ClassicCaseDetails.aspx?CaseID=<%#Eval("CaseID") %>" target="_blank" class="add-to-cart">
                                                                <asp:Image ID="Image1" Style="-webkit-border-radius: 6px; -moz-border-radius: 6px; border-radius: 6px; border: 1px solid blue;" Width="450" Height="318" ImageUrl='<%#Eval("CasePath") %>' runat="server" /></a>&nbsp;&nbsp;
                                                        </li>

                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </ul>
                                        </td>
                                    </tr>

                                </table>
                                <!--<div id="rm_mask_left" class="rm_mask_left"></div>
 <div id="rm_mask_right" class="rm_mask_right"></div>
 <div id="rm_corner_left" class="rm_corner_left"></div>
 <div id="rm_corner_right" class="rm_corner_right"></div>-->



                            </div>
                            <!--<div class="rm_controls">
<a id="rm_small_next" href="#" class="rm_small_next"></a>
<a id="rm_small_prev" href="#" class="rm_small_prev"></a>

<a id="rm_play" href="#" class="rm_play">Play</a>
<a id="rm_pause" href="#" class="rm_pause">Pause</a>
</div> 

<div class="rm_nav">
<a id="rm_next" href="#" class="rm_next"></a>
<a id="rm_prev" href="#" class="rm_prev"></a>
</div>-->
                        </div>
                    </div>
                </div>
                <div class="slider-mobile">
                    <!-- START SLIDER -->
                    <div id="slider" class="group mobile inner fixed-image">
                        <img src="images/slider/001.jpg" width="960" height="338" alt="" />
                    </div>
                    <!-- END SLIDER -->
                    <script type="text/javascript">
                        var yiw_slider_type = 'rotating',
                            yiw_slider_rotating_npanels = 4000,
                            yiw_slider_rotating_timeDiff = 200,
                            yiw_slider_rotating_slideshowTime = 7000;
                    </script>
                </div>
                <!-- START PRIMARY SECTION -->
                <div id="primary" class="inner group">
                    <div id="slogan" class="inner" style="text-align: center;">
                        <%--<h2>客户经典案例</h2>--%>
                        <script type="text/javascript">

                            $(document).ready(function () {
                                var types = "<%=Request["Type"] %>"
                                if (types == "") {
                                    $("#div_Panel2").hide();
                                } else {
                                    $("#div_Panel1").hide();
                                }

                                $("#tdPanel1").mouseover(function () {
                                    $(this).css("background-color", "gray");
                                    $("#div_Panel1").show();
                                    $("#div_Panel2").hide();
                                });
                                $("#tdPanel1").mouseleave(function () {
                                    $(this).css("background-color", "white");
                                });
                                $("#tdPanel2").mouseover(function () {
                                    $(this).css("background-color", "gray");
                                    $("#div_Panel2").show();
                                    $("#div_Panel1").hide();
                                });
                                $("#tdPanel2").mouseleave(function () {
                                    $(this).css("background-color", "white");
                                });
                            });
                        </script>
                        <table style="text-align: center; width: 100%">
                            <tr>
                                <td id="tdPanel1" style="border: 1px solid black; cursor: pointer; width: 300px;">
                                    <h2 id="Panel1">婚礼作品</h2>
                                </td>
                                <td width="25px;"></td>
                                <td id="tdPanel2" style="border: 1px solid black; cursor: pointer; width: 300px;">
                                    <a href="ClassicCaseList.aspx">
                                        <h2 id="Panel2">婚礼统筹</h2>
                                    </a>
                                </td>
                            </tr>
                        </table>



                    </div>
                    <div runat="server" id="div_Panel1" class="layout-sidebar-left">
                        <!-- START CONTENT -->
                        <div style="width: 100%; text-align: center;">
                            <h2>客户经典案例</h2>
                        </div>
                        <ul class="products">
                            <asp:Repeater ID="rptCelePackageList" runat="server">
                                <ItemTemplate>
                                    <li class="product border shadow first">
                                        <a href="ClassicCaseDetails.aspx?CaseID=<%#Eval("CaseID") %>">
                                            <div class="thumbnail">
                                                <asp:Image Width="150" Height="150" ID="Image1" ImageUrl='<%#Eval("CasePath") %>' runat="server" />
                                                <div class="thumb-shadow"></div>
                                                <strong class="below-thumb"><%#Eval("CaseName") %></strong>
                                            </div>
                                        </a>
                                        <div class="buttons">
                                            <a href="ClassicCaseDetails.aspx?CaseID=<%#Eval("CaseID") %>" target="_blank" class="add-to-cart">详情</a>
                                        </div>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                        <div class="clear"></div>
                        <div class="clear"></div>
                        <div class="border-line"></div>
                        <!-- END CONTENT -->
                        <!-- START SIDEBAR -->
                        <form id="form1" runat="server">
                            <div>
                                <table>
                                    <tr>
                                        <td>案例名称:</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtTitle" Width="100px" /></td>
                                        <td>酒店:</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtHotel" Width="100px" /></td>
                                        <td>风格:</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtStyle" Width="100px" /></td>
                                        <td>
                                            <asp:Button ID="btnSerch" runat="server" Text="查询" CssClass="btn btn-primary" OnClick="btnSerch_Click" />
                                            <cc2:btnReload CssClass="btn btn-primary" ID="btnReload2" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </form>
                        <div style="width: 100%" id="sidebar" class="group">
                            <div id="product_categories-3" class="widget-1 widget-first widget widget_product_categories">
                                <h3>经典目录</h3>
                                <ul style="">
                                    <li class="cat-item cat-item-22"><%--<a href="#" title="View all posts filed under Brand">特事特办</a>--%>
                                        <ul class="children">
                                            <asp:Repeater ID="rptListTree" runat="server">
                                                <ItemTemplate>
                                                    <%--<li style="float: left; margin: 1px 8px; width: 200px" class="cat-item cat-item-28"><i class="icon icon-file"></i><a href="ClassicCaseDetails.aspx?CaseID=<%#Eval("CaseID") %>" target="_blank"><%#Eval("CaseName") %></a></li>--%>
                                                    <li style="float: left; text-align: center; margin: 1px 8px; width: 200px" class="cat-item cat-item-28">
                                                        <a href="ClassicCaseDetails.aspx?CaseID=<%#Eval("CaseID") %>" target="_blank"><%#Eval("CaseName") %></a><br />
                                                        <a class="img" target="_blank" href='ClassicCaseDetails.aspx?CaseID=<%#Eval("CaseID") %>'>
                                                            <%--<img src='<%#Eval("CasePath") %>' width="140" height="100" class="imgEval" alt='<%#Eval("CaseName") %>' />--%>
                                                            <img src='<%#GetImgPath(Eval("CaseID")) %>' width="140" height="100" class="imgEval" alt='<%#Eval("CaseName") %>' />
                                                            <%--<asp:Image ID="img" ImageUrl='<%#Eval("CaseFilePath") %>' Width="90" Height="90" CssClass="attachment-90x90" runat="server" />--%>
                                                        </a>

                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </li>
                                </ul>
                            </div>

                        </div>
                        <!-- END SIDEBAR -->
                    </div>
                    <!-- START EXTRA CONTENT -->
                    <style type="text/css">
                        .div_Wrap {
                            position: relative;
                        }

                        .div_ImageWrap {
                            width: 150px;
                            height: 200px;
                        }

                        .div_TextWrap {
                            width: 150px;
                            height: 30px;
                            margin-top: -38px;
                            line-height: 30px;
                            background: Black;
                            text-align: left;
                            filter: alpha(opacity=60);
                            opacity: 0.4;
                            -moz-opacity: 0.4;
                        }

                        .imgPlanner {
                            /*width: 305px;
                            height: 405px;*/
                            width: 150px;
                            height: 200px;
                            border: 1px solid Black;
                        }

                        .APlannerName {
                            color: white;
                            margin-top: 8px;
                            margin-left: 10px;
                            font-size: 13px;
                            font-weight: 500;
                            font-style: normal;
                        }

                            .APlannerName :hover {
                                font-weight: bold;
                                color: #de5b18;
                            }
                    </style>
                    <!-- END EXTRA CONTENT -->
                    <div runat="server" id="div_Panel2" class="layOut_sidebar-bottom" align="center">
                        <div id="div_Panel2_Main">
                            <table>
                                <tr>
                                    <asp:DataList runat="server" ID="DataListPlanner" RepeatDirection="Horizontal" RepeatColumns="4">
                                        <ItemTemplate>
                                            <td>
                                                <div class="div_Wrap">
                                                    <div class="div_ImageWrap">
                                                        <a target="_blank" href='PlannerShows.aspx?PlannerID=<%#Eval("PlannerID") %>'>
                                                            <img src='<%#Eval("PlannerImagePath") %>' alt="#" class="imgPlanner" /></a>
                                                    </div>
                                                    <div class="div_TextWrap">
                                                        <h6>
                                                            <a target="_blank" href='PlannerShows.aspx?PlannerID=<%#Eval("PlannerID") %>' class="APlannerName">
                                                                <asp:Label runat="server" ID="lblPlannerName" CssClass="lblPlannerName" Text='<%#Eval("PlannerName") %>' />
                                                            </a>
                                                            <a target="_blank" href='ClassicCaseList.aspx?Type=<%#Eval("PlannerJob") %>' class="APlannerName">
                                                                <asp:Label runat="server" ID="lblPlannerType" CssClass="lblPlannerType" Text='<%#GetTypeName(Eval("PlannerJob")) %>' /></a>
                                                        </h6>
                                                    </div>
                                                </div>
                                            </td>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <!-- END PRIMARY SECTION -->
                <!-- START NEWSLETTER FORM -->
                <!-- ENDSTART NEWSLETTER FORM -->
                <!-- START FOOTER -->
                <div id="footer" class="group footer-sidebar-right columns-3"></div>
                <!-- END FOOTER -->
                <!-- START COPYRIGHT -->
                <div id="copyright" class="group two-columns">
                    <div class="inner group">
                        <p class="right">
                            Powered by <a target="_blank" href="http://www.holdlove.cn"><strong>Hold love</strong></a>
                        </p>
                    </div>
                </div>
                <!-- END COPYRIGHT -->
            </div>
            <!-- END BG WRAPPER -->
        </div>
        <!-- END WRAPPER -->
    </div>
    <!-- END LIGHT WRAPPER -->
    <script type="text/javascript" src="js/jquery.custom.js"></script>
    <script type="text/javascript" src="js/contact.js"></script>

</body>
</html>
