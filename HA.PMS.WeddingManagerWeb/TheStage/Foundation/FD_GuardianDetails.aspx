<%@ Page Language="C#" AutoEventWireup="true" Theme="PersonEmployee" CodeBehind="FD_GuardianDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.Foundation.FD_GuardianDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">
  <%--  <link rel="stylesheet" type="text/css" href="../../App_Themes/css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../../App_Themes/css/style.css" />
    <link rel="stylesheet" type="text/css" href="../../App_Themes/css/fancybox.css" />--%>

    <script type="text/javascript" src="../../App_Themes/PersonEmployee/js/jquery.min.js"></script>
    <script type="text/javascript" src="../../App_Themes/PersonEmployee/js/jquery.easytabs.min.js"></script>
    <script type="text/javascript" src="../../App_Themes/PersonEmployee/js/respond.min.js"></script>
    <script type="text/javascript" src="../../App_Themes/PersonEmployee/js/jquery.easytabs.min.js"></script>
    <script type="text/javascript" src="../../App_Themes/PersonEmployee/js/jquery.adipoli.min.js"></script>
    <script type="text/javascript" src="../../App_Themes/PersonEmployee/js/jquery.fancybox-1.3.4.pack.js"></script>
    <script type="text/javascript" src="../../App_Themes/PersonEmployee/js/jquery.isotope.min.js"></script>
    <script type="text/javascript" src="../../App_Themes/PersonEmployee/js/jquery.gmap.min.js"></script>
    <script type="text/javascript" src="../../App_Themes/PersonEmployee/js/custom.js"></script>

    <script type="text/javascript">
        function CloseWin() {
            if (confirm("你确定要关闭当前窗口")) {
                window.opener = null;
                //window.opener=top;    
                window.open("", "_self");
                window.close();
            }

        }


        $(document).ready(function () {
            $('.movie').fancybox({
                'transitionIn': 'elastic',
                'transitionOut': 'elastic',
                'speedIn': 200,
                'speedOut': 200,
                'overlayOpacity': 0.6,
                'type': 'iframe',
                'width': 720,
                'height': 485,
                'scrolling': "no"
            });

            $("#detailsInfo").children("span").css({ "margin-left": "0" });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- Container -->
        <div id="container">

            <!-- Top -->
            <div class="top">
                <!-- Logo -->
                <div id="logo">
                    <h2><%= ViewState["Name"] %></h2>
                    <h4><%=ViewState["Type"] %></h4>
                </div>
                <!-- /Logo -->

                <!-- Social Icons -->
                <ul class="socialicons">
                    <li><a href="#" class="social-text" onclick="CloseWin();">关闭</a></li>
                </ul>
                <!-- /Social Icons -->
            </div>
            <!-- /Top -->

            <!-- Content -->
            <div id="content">

                <!-- Profile -->
                <div id="profile">
                    <!-- About section -->
                    <div class="about">
                        <div class="photo-inner">

                            <asp:Image ID="imgHead" runat="server" Height="186" Width="153" />
                        </div>
                        <h1><%= ViewState["Name"] %></h1>
                        <h3><%=ViewState["Type"] %></h3>
                        <p>
                            <asp:Literal ID="ltlExplain" runat="server"></asp:Literal>
                        </p>
                    </div>
                    <!-- /About section -->

                    <!-- Personal info section -->
                    <ul class="personal-info" id="detailsInfo">
                        <li>
                            <label style="font-weight: bold;">姓名 :</label><label><%= ViewState["Name"] %></label></li>

                        <li>
                            <label style="font-weight: bold;">类型 :</label><label><%=ViewState["Type"] %></label></li>
                        <li>
                            <label style="font-weight: bold;">等级 :</label>
                            <label>
                                <asp:Literal ID="ltlLeven" runat="server"></asp:Literal>
                            </label>
                        </li>
                        <li>
                            <label style="font-weight: bold;">价格 :</label>
                            <label>
                                <asp:Literal ID="ltlPrice" runat="server"></asp:Literal>
                            </label>
                        </li>
                        <li style="display: none;">
                            <label>电话</label>
                            <span>
                                <asp:Literal ID="ltlTelPhone" runat="server"></asp:Literal>
                            </span></li>
                        <li style="display: none;">
                            <label>邮箱</label>
                            <span>
                                <asp:Literal ID="ltlEmail" runat="server"></asp:Literal>
                            </span></li>
                    </ul>
                    <!-- /Personal info section -->
                </div>
                <!-- /Profile -->

                <!-- Menu -->
                <div class="menu">
                    <ul class="tabs">
                        <li><a href="#profile" class="tab-profile">人员简介</a></li>
                        <li><a href="#resume" class="tab-resume">人员详情</a></li>
                        <li><a href="#portfolio" class="tab-portfolio">精彩瞬间</a></li>

                    </ul>
                </div>
                <!-- /Menu -->

                <!-- Resume -->
                <div id="resume">
                    <div class="timeline-section">
                        <!-- Timeline for Employment  -->
                        <h3 class="main-heading"><span><%= ViewState["Name"] %>的个人资料</span></h3>
                        <ul class="timeline">
                            <li>

                                <div class="timelineUnit">
                                    <h4><%= ViewState["Name"] %><span class="timelineDate">

                                        <asp:Literal ID="ltlDate" runat="server"></asp:Literal>
                                    </span></h4>
                                    <h5><%=ViewState["Type"] %></h5>
                                    <p>
                                        <asp:Literal ID="ltlPersonalDetails" runat="server"></asp:Literal>
                                    </p>
                                </div>
                            </li>

                            <div class="clear"></div>
                        </ul>
                        <!-- /Timeline for Employment  -->


                    </div>
                    <div class="skills-section">
                        <!-- Skills -->
                        <h3 class="main-heading"><span>其他</span></h3>
                        <ul class="skills">
                            <li>
                                <h4>等级</h4>
                                <%-- <span class="rat6"></span>--%>
                                <%=ViewState["leven"] %>
                            </li>


                        </ul>
                        <!-- /Skills -->
                    </div>
                </div>
                <!-- /Resume -->

                <!-- Portfolio -->
                <div id="portfolio">

                    <ul id="portfolio-filter">
                        <li><a href="" class="current" data-filter="*">全部</a></li>

                        <li><a href="" data-filter=".photoghraphy">图片</a></li>

                        <li><a href="" data-filter=".animation">视频</a></li>
                    </ul>
                    <div class="extra-text">精彩瞬间</div>
                    <ul id="portfolio-list">
                        <%--  图片--%>

                        <asp:Repeater ID="rptImg" runat="server">

                            <ItemTemplate>
                                <li class="photoghraphy" style="height:160px">
                                    <a href='<%#GetImgPath(Eval("ImagePath")) %>' title='' <%#Eval("ImageName") %> rel="portfolio" class="folio">
                                        <asp:Image ID="Image1" ImageUrl='<%#Eval("ImagePath") %>' runat="server" />
                                        <%--   <h2 class="title">图片可直接点击打开</h2>--%>
                                        <br />
                                        <span class="categorie">
                                            <%#Eval("ImageName") %>
                                        </span>
                                    </a>
                                </li>

                            </ItemTemplate>
                        </asp:Repeater>

                        <%--  视频--%>
                        <asp:Repeater ID="rptMovie" runat="server">
                            <ItemTemplate>
                                <li class="animation" style="height:160px">

                                    <%-- <a href="<%#GetImgPath(Eval("MovieTopImagePath")) %>" rel="portfolio" class="folio iframe">--%>

                                    <%--  <h2 class="title">打开一个帧结构页面</h2>--%>
                                    <%-- <span class="categorie">--%>
                                    <br />
                                    <a class="movie" href="GuardianMoviePlay.aspx?GuradinMovieID=<%#Eval("GuradinMovieID") %>">
                                        <asp:Image ID="Images" ImageUrl='<%#Eval("MovieTopImagePath") %>' runat="server" />
                                        <%#Eval("MovieName") %>
                                    </a>
                                    <%--</span>--%>
                                    <%--       </a>--%>
                                </li>

                            </ItemTemplate>
                        </asp:Repeater>


                    </ul>
                </div>
                <!-- /Portfolio -->


            </div>


            <!-- Footer -->
            <div class="footer">
                <div class="copyright">© 2013 H♥LD</div>
            </div>
            <!-- /Footer -->

        </div>
        <!-- /Container -->

    </form>
</body>
</html>
