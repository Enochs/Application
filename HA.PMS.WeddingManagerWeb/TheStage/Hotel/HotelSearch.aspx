<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelSearch.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.Hotel.HotelSearch" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>酒店搜索</title>
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
        .btncls { background-color: #6bcdf8; color: #fff; width: 90px; height: 30px; }
        select { height: 30px; }
        .txtCls { height: 27px; }
    </style>
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
                    <!-- END HEADER -->
                    <!-- START PRIMARY SECTION -->
                    <div class="layout-sidebar-no group">
                        <div id="container">
                            <div id="content" role="main">
                                <div id="primary" class="inner group">
                                    <div id="slogan" class="inner"><a href="#" style="display: none;" class="large red sc-button">酒店查询</a></div>
                                    <div class="layout-sidebar-right group">
                                        <!-- START CONTENT -->
                                        <div id="Div1" role="main" class="group">
                                            <div class="box notice-box">区域 :
                                                <asp:DropDownList ID="ddlArea" CssClass="txtCls" runat="server"></asp:DropDownList>星级:<asp:DropDownList ID="ddlStarLevel" CssClass="txtCls" runat="server">
                                                    <asp:ListItem Text="请选择" Value="0" />
                                                    <asp:ListItem Text="五星级" Value="5" />
                                                    <asp:ListItem Text="四星级" Value="4" />
                                                    <asp:ListItem Text="三星级" Value="3" />
                                                    <asp:ListItem Text="二星级" Value="2" />
                                                    <asp:ListItem Text="一星级" Value="1" />
                                                    <asp:ListItem Text="其他" Value="6" />
                                                </asp:DropDownList>桌数:<asp:TextBox CssClass="txtCls" ID="txtDeskStar" runat="server"></asp:TextBox>至<asp:TextBox CssClass="txtCls" ID="txtDeskEnd" runat="server"></asp:TextBox><%--餐标:--%><%-- <select name="餐标"> <option>餐标</option> </select>特色:<select name="特色"> <option>特色</option> </select>--%><asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="btncls" Text="查找" ClientIDMode="Static" /><%--搜索条件.--%></div>
                                            <div id="post-557" class="post-557 page type-page status-publish hentry group"><%--<h3>人员/策划师</h3>--%><table style="width: 100%" cellpadding="0" cellspacing="0" class="short-table blue">
                                                <thead>
                                                    <tr>
                                                        <th style="width: 25%">酒店名</th>
                                                        <th style="width: 20%">区域 </th>
                                                        <th style="width: 20%">星级 </th>
                                                        <th style="width: 10%">桌数</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="rptList" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <th class="features"><a target="_blank" href='HotelDetails.aspx?HotelID=<%#Eval("HotelID") %>'><%#Eval("HotelName") %></a></th>
                                                                <td><%#Eval("Area") %></td>
                                                                <td><%#GetStarLevelString(Eval("StarLevel")) %></td>
                                                                <td><%#Eval("DeskCount") %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td colspan="4">
                                                            <cc2:AspNetPagerTool ID="HotelPager" runat="server" AlwaysShow="true" OnPageChanged="HotelPager_PageChanged"></cc2:AspNetPagerTool>
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                            </div>
                                            <%-- <div class="general-pagination group"><a href="blog.html" class="selected">1</a><a href="#">2</a><a href="#">&rsaquo;</a></div>--%><div id="comments">
                                                <!--<p class="nocomments">&nbsp;</p>-->
                                            </div>
                                            <!-- #comments -->
                                        </div>
                                        <!-- END CONTENT -->
                                        <!-- START SIDEBAR -->
                                        <!-- END SIDEBAR -->
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
                                            <label for="fullname">用户名：</label><asp:TextBox ID="txtLoginName" runat="server"></asp:TextBox></li>
                                        <li style="list-style: none;">
                                            <label for="password">密&nbsp;&nbsp;&nbsp;&nbsp;码：</label><asp:TextBox TextMode="Password" ID="txtpassword" runat="server"></asp:TextBox></li>
                                        <li style="list-style: none;">
                                            <asp:Button ID="btnLogin" CssClass="btncls" OnClick="btnLogin_Click" runat="server" Text="登陆" /></li>
                                    </ul>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    <!-- ENDSTART NEWSLETTER FORM -->
                    <!-- START COPYRIGHT -->
<%--                    <div id="copyright" class="group two-columns">
                        <div class="inner group">
                            <p class="left"><span style="padding-left: 10px;"></span></p>
                            <p class="right">版权所有：<a href="http://www.holdlove.cn"><strong>重庆好爱信息技术发展有限公司</strong></a></p>
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
