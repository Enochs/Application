<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="CelebrationPackageSinger.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.Foundation.CelebrationPackageSinger" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
        <head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .imgbg {background: url(/App_Themes/FourGuardian/images/slider2/1.jpg);}
    </style>
</head>
<body>
    <div id="pxs_container" class="pxs_container">
        <div class="top-l">
            <div class="top-l-c"></div>
            <div class="logo">
                <a href="#"><img src="/App_Themes/FourGuardian/images/buy.png" width="140" height="68" alt=""  style="display:none;"/></a>
            </div>
            <div class="light-t"></div>
        </div>
        <div class="top-r">
            <div class="top-r-c"></div>
        </div>
        <div class="pxs_bg">
            <div class="pxs_bg1"></div>
            <div class="pxs_bg2"></div>
            <div class="pxs_bg3"></div>
        </div>
        <div class="pxs_loading">图片装载中...</div>
        <div class="pxs_slider_wrapper">
            <ul class="pxs_slider">
                <!-- 大图轮播区域 -->
                <li id="for_slider">
                    <div id="carousel-container">
                        <div id="carousel" class="carousel">
                            <asp:Repeater ID="rptList" runat="server">

                                <ItemTemplate>
                                    <div class="carousel-feature">
                                        <a href="#">
                                            <asp:Image ID="img" ImageUrl='<%#Eval("ImageUrl") %>' CssClass="imgbg" runat="server"  Width="800"  Height="450"/>
                                         <%--   <img alt="Image Caption" class="carousel-image" src="images/space.gif" style="background: url(images/slider2/1.jpg);" width="800" height="450" />--%>
                                        </a>
                                        <div class="mask">
                                            <asp:Label id="lblExplain" runat="server">
                                                <div class="desc_outer_outer">
                                                    <div class="desc_outer">
                                                        <div class="desc">
                                                            <div class="desc-c">
                                                                <a href="#">
                                                                    <h2>名称：<asp:Literal ID="ltlTopTitle" runat="server"></asp:Literal></h2>
                                                                </a>
                                                                <div class="desc_text">
                                                                    <%--价格：<asp:Literal ID="ltlPackagePrice" runat="server"></asp:Literal>--%>
                                                                    优惠价<asp:Literal ID="ltlPackagePreferentiaPrice" runat="server"></asp:Literal>
                                                                    <br />
                                                                    风格：
                                                                <asp:Literal ID="ltlPackageStyle" runat="server"></asp:Literal>
                                                                    <br />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Label>
                                            <div class="nav_s">
                                                <div class="nav_s_inner"><!--计时小点-->
                                                </div>
                                                <div class="slide_right"><span class="pxs_next"></span></div>
                                                <div class="slide_left"><span class="pxs_prev"></span></div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </li>
                <!-- 大图轮播区域结束 -->
                <!-- 图片展示区域图片展示区域图片展示区域图片展示区域图片展示区域 -->
                <li id="for_portfolio">
                    <div class="holder post horiz">
                        <img style="" src="/App_Themes/FourGuardian/images/space.gif" width="980" height="450" alt="" />
                        <div class="mask">
                            <div class="article">
                                <div class="shadow-l"></div>
                                <div id="scroll-box" class="scroll-box">
                                    <div class="gallery clearfix">
                                        <asp:Repeater ID="rptCeleImgPlay" runat="server">
                                            <ItemTemplate>
                                                <div class="gallery_item">
                                                    <a href='<%#GetStaticUrl( Eval("ImageUrl")) %>' title='<%#Eval("Message") %>' rel='prettyPhoto[gallery2]' target="_blank" class="shadow_light" >
                                                        <asp:Image ID="Image1" style="width:287px;height:167px;margin-left:8px;margin-top:7px" ImageUrl='<%#GetStaticUrl(Eval("ImageUrl")) %>' runat="server" />
                                                        <div class="i">
                                                            <div class="h-i">
                                                                <div class="gallery-cont">
                                                                    <h2>图片展示区域</h2>
                                                                    <p><div style="width:250px;height:80px;word-wrap:break-word;text-overflow:ellipsis;overflow-y:hidden"><%#Eval("Message") %></div></p>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </a>
                                                </div>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div class="shadow-r"></div>
                            </div>
                        </div>
                    </div>
                </li>
                <!-- 图片展示区域结束-->
                <!-- 套系文字介绍区域-->
                <li id="for_post">
                    <div class="holder post">
                        <img style="" src="/App_Themes/FourGuardian/images/space.gif" width="980" height="450" alt="" />
                        <div class="mask">
                            <div class="article">
                                <div class="shadow-t"></div>
                                <div class="scroll-wrap">
                                    <div class="scroll-pane">
                                        <h1>
                                            <asp:Literal ID="ltlPackPackageTitle" runat="server"></asp:Literal>
                                        </h1>
                                        <div class="article_link">
                                            <a href="#" class="ico_link date" style="display:none">
                                                <asp:Literal ID="ltlPackPackageDate" runat="server"></asp:Literal></a>
                                        </div>
                                        <a href="#" class="alignleft">
                                                <asp:Image ID="imgPackageImgTop" runat="server" width="274" height="274" />
                                            <i></i>
                                        </a>
                                        <asp:Literal ID="ltlPackerDatails" runat="server"></asp:Literal>
                                        <div id="form_prev_holder">
                                            <div id="form_holder">
                                                <div class="comment-form">
                                                    <div class="but" style="display:none">
                                                        <a href="#" class="button" title="Add comment"><span>立即购买<i class="comment-i"></i></span></a>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="shadow-b"></div>
                            </div>
                        </div>
                    </div>
                </li>
                <!-- 套系文字介绍区域结束-->
                <!-- 推荐最佳拍档区域-->
                <li id="for_blog">
                    <div class="holder post">
                        <img style="" src="/App_Themes/FourGuardian/images/space.gif" width="980" height="450" alt="" />
                        <div class="mask">
                            <div class="article">
                                <div class="shadow-t"></div>
                                <div class="scroll-wrap">
                                    <div class="scroll-pane">
                                        <div class="posts">
                                            <asp:Repeater ID="rptPackage" runat="server">
                                                <ItemTemplate>
                                                    <div class="post-item">
                                                        <div class="post-i">
                                                            <a href="#" class="alignleft">
                                                                <asp:Image ID="img" ImageUrl='<%#Eval("Data") %>' Width="140" Height="140" runat="server" />
                                                                <div class="m">
                                                                    <i></i>
                                                                </div>
                                                            </a>
                                                            <a class="inf"></a>
                                                        </div>
                                                        <div class="info-block">
                                                            <div class="arrow"></div>
                                                            鼠标显示产品介绍部分
                                                        </div>
                                                        <h2><a href="#"><%#Eval("ProductName") %></a></h2>
                                                        <span>原价:<%#Eval("SalePrice") %></span>
                                                        <span>
                                                            <h1>套餐优惠价:<%#Eval("PackagePreferentiaPrice") %></h1>
                                                        </span>
                                                        <span>产品简单介绍</span>
                                                        <div class="buttons" style="display:none">
                                                            <a href="#" class="button"><span>购买</span></a>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                                </div>
                                <div class="shadow-b"></div>
                            </div>
                        </div>
                    </div>
                </li>
            </ul>
            <div class="pxs_thumbnails_holder">
                <div class="menu_bot">
                    <div class="light_bot"></div>
                    <ul class="pxs_thumbnails navigatable">
                        <li><a href="slider.html" id="link_slider"><asp:Literal ID="ltlTopNo1" runat="server"></asp:Literal></a></li>
                        <li><a href="gallery.html" id="link_portfolio">套系展示</a><div><img src="/App_Themes/FourGuardian/images/space.gif" alt="Third Image" /></div></li>
                        <li><a href="post.html" id="link_post">套系详情</a></li>
                        <li><a href="blog.html" id="link_blog">最佳拍档</a><div><img src="/App_Themes/FourGuardian/images/space.gif" alt="Third Image" /></div></li>
                    </ul>
                </div>
            </div>
        </div>
    </div> 
    <link href="/App_Themes/FourGuardian/css/html5reset.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/FourGuardian/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/FourGuardian/css/slider.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/FourGuardian/css/custom.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/FourGuardian/css/highslide-custom.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/FourGuardian/css/skin_red.css" rel="stylesheet" type="text/css" />
    <!--[if lte IE 7]><link href="css/old_ie.css" rel="stylesheet" type="text/css"><![endif]-->
    <script src="/App_Themes/FourGuardian/js/jquery-1.5.min.js" type="text/javascript"></script>
    <script src="/App_Themes/FourGuardian/js/jquery.easing.1.3.js" type="text/javascript"></script>
    <script type="text/javascript" src="/App_Themes/FourGuardian/js/PIE.js"></script>
    <script src="/App_Themes/FourGuardian/js/cufon-yui.js" type="text/javascript"></script>
    <!--<script src="js/Crimson.font.js" type="text/javascript"></script>-->
    <script src="/App_Themes/FourGuardian/js/slider.js" type="text/javascript"></script>
    <script type="text/javascript" src="/App_Themes/FourGuardian/js/jquery.mousewheel.min.js"></script>
    <script type="text/javascript" src="/App_Themes/FourGuardian/js/jScrollHorizontalPane.min.js"></script>
    <script type="text/javascript" src="/App_Themes/FourGuardian/js/jScrollPane.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="/App_Themes/FourGuardian/js/jScrollPane.css" />
    <link rel="stylesheet" type="text/css" media="all" href="/App_Themes/FourGuardian/css/jScrollHorizontalPane.css" />
    <script src="/App_Themes/FourGuardian/js/plugins/placeholder/jquery.placeholder.js" type="text/javascript"></script>
    <script src="/App_Themes/FourGuardian/js/plugins/validator/jquery.validationEngine.js" type="text/javascript"></script>
    <script src="/App_Themes/FourGuardian/js/plugins/validator/z.trans.en.js" type="text/javascript"></script>
    <link href="/App_Themes/FourGuardian/js/plugins/validator/validationEngine.jquery.css" rel="stylesheet" type="text/css" />
    <script src="/App_Themes/FourGuardian/js/plugins/cycle/jquery.cycle.all.js" type="text/javascript"></script>
    <script src="/App_Themes/FourGuardian/js/plugins/drag.js" type="text/javascript"></script>
    <script src="/App_Themes/FourGuardian/js/jquery-css-transform.js" type="text/javascript"></script>
    <script src="/App_Themes/FourGuardian/js/jquery-animate-css-rotate-scale.js" type="text/javascript"></script>
    <script src="/App_Themes/FourGuardian/js/sc-slider.js" type="text/javascript"></script>
    <script type="text/javascript" src="js/plugins/highslide/highslide-full.js"></script>
    <script type="text/javascript" src="/App_Themes/FourGuardian/js/plugins/highslide/highslide.config.js" charset="utf-8"></script>
    <link rel="stylesheet" type="text/css" href="/App_Themes/FourGuardian/js/plugins/highslide/highslide.css" />
    <!--[if lt IE 7]>
<link rel="stylesheet" type="text/css" href="js/plugins/highslide/highslide-ie6.css" />
<![endif]-->
    <script src="/App_Themes/FourGuardian/js/vt.js" type="text/javascript"></script>
    <link href="css/prettyPhoto.css" rel="stylesheet" media="screen" />
    <script type="text/javascript" src="js/jquery.prettyPhoto.js"></script>
    <script type="text/javascript">
        $(function () {
            $(".gallery a[rel^='prettyPhoto']").prettyPhoto({ animation_speed: 'normal', theme: 'light_rounded', slideshow: 4000, autoplay_slideshow: false });
            $(".gallery:gt(0) a[rel^='prettyPhoto']").prettyPhoto({ animation_speed: 'fast', slideshow: 10000, hideflash: true });
        });
    </script>
    <form id="form1" runat="server"></form>
</body> 
</html>
