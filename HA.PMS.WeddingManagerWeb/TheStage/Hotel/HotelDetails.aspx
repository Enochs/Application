<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.Hotel.HotelDetails" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width" />
    <title>酒店</title>
    <link rel="stylesheet" type="text/css" media="all" href="style.css" />
    <link rel="stylesheet" type="text/css" media="screen and (max-width: 980px)" href="css/lessthen980.css" />
    <link rel="stylesheet" type="text/css" media="screen and (max-width: 600px)" href="css/lessthen600.css" />
    <link rel="stylesheet" type="text/css" media="screen and (max-width: 480px)" href="css/lessthen480.css" />
    <link rel="stylesheet" href="css/shop.css" type="text/css" media="all" />
    <link rel="stylesheet" href="css/prettyPhoto.css" type="text/css" media="all" />
    <link rel="stylesheet" href="css/tipsy.css" type="text/css" media="all" />
    <link rel='stylesheet' href='css/jquery-rotating.css' type='text/css' media='all' />
    <link rel='stylesheet' href='css/slider-cycle.css' type='text/css' media='all' />
    <script type="text/javascript" src="js/jquery.js"></script>
    <script type="text/javascript" src="js/jquery.prettyPhoto.js"></script>
    <script type="text/javascript" src="js/jquery.tipsy.js"></script>
    <script type="text/javascript" src="js/jquery.nivo.slider.pack.js"></script>
    <script type="text/javascript" src="js/jquery.cycle.min.js"></script>
    <script type="text/javascript" src="js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="js/comment-reply.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="css/home.css" />
    <style type="text/css">
        .btncls {
            background-color: #6bcdf8;
            color: #fff;
            width: 90px;
            height: 30px;
        }


        ul li {
            list-style-type: none;
            line-height: 35px;
        }

        .lblTitles {
            color: #999999;
        }

        .lblTexts {
            color: #333333;
        }

        .imgScore {
            width: 80px;
        }

        .table_Hall {
            text-align: center;
            border: 1px solid none;
            width: 250px;
            height: 400px;
        }

        .td_label {
            text-align: left;
            float: left;
            width: 40px;
        }

        .td_text {
            text-align: left;
            width: 60px;
        }

        .td_LastText {
            text-align: left;
            vertical-align: top;
        }
    </style>
</head>
<body class="responsive boxed-layout no_js">
    <form id="form2" runat="server">
        <!-- START LIGHT WRAPPER -->
        <div class="bgLight group">
            <!-- START WRAPPER -->
            <div class="wrapper group">
                <!-- START BG WRAPPER -->
                <div class="bgWrapper group">
                    <!-- START HEADER -->
                    <!-- END HEADER -->
                    <!-- START PRIMARY SECTION -->
                    <div id="primary" class="inner group" />
                    <div class="layout-sidebar-no group">
                        <div id="container">
                            <div id="content" role="main">
                                <div id="breadcrumb"><a href="HotelSearch.aspx">返回 &rsaquo; 酒店列表</a></div>
                                <div class="product type-product status-publish hentry">
                                    <div class="images">
                                        <a href='<%=ViewState["HotelImagePath"] %>' target="_blank">
                                            <img src="#" runat="server" id="imgHotel" width="530" height="345" alt="#" /></a>
                                    </div>
                                    <div class="summary">
                                        <h1 class="product_title page-title">
                                            <asp:Literal ID="lblHotelName" runat="server"></asp:Literal></h1>
                                        <p>
                                            <asp:Literal ID="lblHotel" runat="server"></asp:Literal><em></em>
                                        </p>
                                        <ul>
                                            <li>
                                                <label class="lblTitles">类型：</label>
                                                <asp:Label runat="server" ID="lblHotelType" CssClass="lblTexts" /></li>
                                            <li>
                                                <label class="lblTitles">评价：</label>
                                                <img runat="server" id="imgScore" class="imgScore" src="#" alt="#" />&nbsp;&nbsp;
                                                <asp:Label ID="lblScore" runat="server" CssClass="lblTexts" Style="color: #f55d10; font-size: 15px; font-weight: bold;"></asp:Label>
                                            </li>
                                            <li>
                                                <label class="lblTitles">婚宴价格：</label><strong>
                                                    <asp:Label ID="lblPriceStar" runat="server"></asp:Label>
                                                    <label>-</label>
                                                    <asp:Label ID="lblPriceEnd" runat="server"></asp:Label>
                                                </strong><em id="mon_desk" style="font-style: normal;">元/桌</em></li>
                                            <li>
                                                <label class="lblTitles">容纳桌数：</label><asp:Label ID="lblDeskCount" runat="server" CssClass="lblTexts"></asp:Label>桌</li>
                                            <li>
                                                <label class="lblTitles">酒店地址：</label><asp:Label ID="lblAddress" runat="server" CssClass="lblTexts"></asp:Label></li>
                                            <li>
                                                <label class="lblTitles">婚宴热线：</label><asp:Label ID="lblTel" runat="server" CssClass="lblTexts"></asp:Label></li>
                                            <li>
                                                <label class="lblTitles">场地标签：</label><asp:Repeater ID="rptLabel" runat="server">
                                                    <ItemTemplate>
                                                        <a href="#"><%#GetLabelByID(Eval("LableID")) %></a>&nbsp;
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </li>
                                        </ul>
                                        <div class="quantity"></div>
                                        <input type="hidden" id="_n" name="_n" value="a93bb4a4c2" />
                                        <input type="hidden" name="_wp_http_referer" value="/demo/sommerce/shop/gold-mahibo/" /><%-- <div class="product_meta"><span class="sku">风格： <a href="#" rel="tag">田园风格</a></span></div>--%>
                                    </div>

                                    <div id="product-tabs">
                                        <ul class="tabs">
                                            <li class="active"><a href="#related-products">宴会厅</a></li>
                                            <li><a href="#tab-description">酒店图片</a></li>
                                        </ul>
                                        <div class="containers">
                                            <!--宴会厅-->
                                            <div class="panel" id="related-products">
                                                <div class="related products">
                                                    <br />
                                                    <br />
                                                    <div class="item_area banquet_hall">
                                                        <div class="bd" align="center">
                                                            <asp:DataList ID="dlBanquetHall" runat="server" RepeatDirection="Horizontal" RepeatColumns="2">
                                                                <ItemTemplate>
                                                                    <table class="table table_Hall" align="center">
                                                                        <tr height="155">
                                                                            <td colspan="4"><a href='<%#GetBanquetHallPath(Eval("BanquetHallID")) %>' rel='prettyphoto[BH<%#Eval("BanquetHallID") %>]' class="zoom first">
                                                                                <img width="220" height="180" src='<%#GetBanquetHallPath(Eval("BanquetHallID")) %>' class="attachment" style="border: 1px solid gray;" /></a></td>
                                                                        </tr>
                                                                        <tr height="35">
                                                                            <td colspan="4">
                                                                                <h5 class="floor_hall"><%#Eval("HallName") %> <%#Eval("FloorName") %></h5>
                                                                            </td>
                                                                        </tr>
                                                                        <tr height="35">
                                                                            <td class="td_label">桌数：</td>
                                                                            <td class="td_text">
                                                                                <asp:Label runat="server" ID="lblDeskCount" Text='<%#Eval("DeskCount") %>' /></td>
                                                                            <td class="td_label">面积：</td>
                                                                            <td class="td_text">
                                                                                <asp:Label runat="server" ID="lblArea" Text='<%#Eval("Area") %>' /></td>
                                                                        </tr>
                                                                        <tr height="35">
                                                                            <td class="td_label">层高：</td>
                                                                            <td class="td_text">
                                                                                <asp:Label runat="server" ID="lblFloorHeight" Text='<%#Eval("FloorHeight") %>' /></td>
                                                                            <td class="td_label">空高：</td>
                                                                            <td class="td_text">
                                                                                <asp:Label runat="server" ID="lblEmptyHeight" Text='<%#Eval("EmptyHigh") %>' /></td>
                                                                        </tr>
                                                                        <tr height="35">
                                                                            <td class="td_label">餐标：</td>
                                                                            <td class="td_text">
                                                                                <asp:Label runat="server" ID="lblMeal" Text='<%#Eval("Meal") %>' /></td>
                                                                        </tr>
                                                                        <tr height="85">
                                                                            <td class="td_label" style="vertical-align: top;">说明：</td>
                                                                            <td colspan="3" class="td_LastText">
                                                                                <asp:Label runat="server" ID="lblExplain" Text='<%#Eval("BanquetExplain") %>' Width="200px" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:DataList>
                                                        </div>
                                                    </div>

                                                    <div class="clear"></div>
                                                </div>
                                            </div>

                                            <!--精彩图片-->
                                            <div class="panel" id="tab-description">
                                                <br />
                                                <asp:Label ID="lblHotelDetails" runat="server" Visible="false"></asp:Label>
                                                <div class="hotelImages" style="margin-left:13px;">
                                                    <strong style="color: green;">酒店图片</strong>
                                                    <div class="thumbnails" style="width: 1024px;">
                                                        <asp:DataList runat="server" ID="dlImages" RepeatDirection="Horizontal" RepeatColumns="7">
                                                            <ItemTemplate>

                                                                <a href='<%#GetImgPath(Eval("HotelImagePath")) %>' title="albatros_niwa_001_big" rel="prettyphoto[gallery]" class="zoom first">
                                                                    <img src='<%#Eval("HotelImagePath") %>' alt="#" width="91" height="91" />
                                                                </a>

                                                            </ItemTemplate>
                                                        </asp:DataList>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


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
                                            <asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox></li>
                                        <li style="list-style: none;">
                                            <label for="password">密&nbsp;&nbsp;&nbsp;&nbsp;码：</label>
                                            <asp:TextBox TextMode="Password" ID="txtpassword" runat="server"></asp:TextBox></li>
                                        <li style="list-style: none;">
                                            <asp:Button ID="btnLogin" CssClass="btncls" OnClick="btnLogin_Click" runat="server" Text="登陆" /></li>
                                    </ul>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    <!-- ENDSTART NEWSLETTER FORM -->
                    <!-- START COPYRIGHT -->
                    <div id="copyright" class="group two-columns">
                        <div class="inner group">
                            <p class="right">版权所有：<a href="http://www.holdlove.cn"><strong>重庆好爱信息技术发展有限公司</strong></a></p>
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
    </form>
</body>
</html>
