<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlannerShows.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.ClassicCase.PlannerShows" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="css/styles.css" rel="stylesheet" />
    <link href="css/home.css" rel="stylesheet" />
    <title></title>
    <!-- CSS -->
    <script src="js/jquery-1.4.2.min.js"></script>
    <script src="js/jquery.slide.js"></script>
    <script type="text/javascript">
        $(function () {

            /* 用按钮控制图片左右滚动 */
            $(".hotPic .JQ-slide").Slide({
                effect: "scroolLoop",
                autoPlay: false,
                speed: "normal",
                timer: 3000,
                steps: 1
            });

        });
    </script>

    <style type="text/css">
        .hotPic .JQ-slide-nav a {
            display: block;
            z-index: 99;
            width: 48px;
            height: 48px;
            overflow: hidden;
            text-indent: -999em;
            text-decoration: none;
            position: absolute;
            top: 40px;
            background: url(images/arrow_pic.png) no-repeat;
        }

            .hotPic .JQ-slide-nav a.prev {
                left: -20px;
                background-position: 0 0;
            }

                .hotPic .JQ-slide-nav a.prev:hover {
                    background-position: -100px 0;
                }

            .hotPic .JQ-slide-nav a.next {
                right: -20px;
                background-position: -50px 0;
            }

                .hotPic .JQ-slide-nav a.next:hover {
                    background-position: -150px 0;
                }

        table {
            border: 1px solid none;
        }

        .div_Main {
            height: 600px;
            width: 100%;
        }


        .tableShow {
            width: 100%;
            height: 520px;
        }

        .imgPlanner {
            width: 385px;
            height: 500px;
            border: 1px solid #080808;
        }

        .tableShow tr td {
            vertical-align: top;
            text-align: left;
        }

        .tr_Styles {
            width: 520px;
            margin: 0px;
            padding: 0px;
            line-height: 40px;
            font-size: 13px;
        }

        .td_Styles {
            width: 520px;
            text-align: left;
        }

        #lblPlannerJobDescription {
            width: 520px;
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
    <form id="form1" runat="server">

        <div class="bgLight group">

            <div class="wrapper group">

                <div class="bgWrapper group">


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

                    <div id="primary" class="inner group" />
                    <div class="layout-sidebar-no group">
                        <div id="container">
                            <div id="content" role="main">
                                <div id="breadcrumb">
                                    <a class="home" href="ClassicCaseList.aspx">返回 <%-- &rsaquo;--%>>> 婚礼统筹</a>
                                </div>
                                <br />
                                <div class="div_Main">
                                    <table class="tableShow">
                                        <tr>
                                            <td rowspan="3">
                                                <a href="#">
                                                    <img src="#" runat="server" id="img_planner" alt="#" class="imgPlanner" />
                                                </a>
                                            </td>
                                            <td>
                                                <asp:Label runat="server" CssClass="lblPlaner" ID="lblPlannerName" Style="font-size: 18px; font-weight: bolder;" Text="测试师" />
                                            </td>
                                        </tr>
                                        <tr class="tr_Styles">
                                            <td>
                                                <asp:Label runat="server" CssClass="lblPlaner" ID="lblTypeName" Style="font-size: 12px; font-weight: bolder; color: #808080" Text="Nana" /></td>
                                        </tr>
                                        <tr class="tr_Styles">
                                            <td class="td_Styles">
                                                <asp:Label runat="server" ID="lblPlannerJobDescription" Text='<%#Eval("PlannerJobDescription") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <strong style="color: green;">精彩作品案例</strong>&nbsp;&nbsp;<asp:Label runat="server" ID="lblEvalCount" Style="color: #3d3633; font-weight: bold;" />
                                <table class="tableEval">
                                    <tr runat="server" id="tr_NoEval"
                                        visible="false">
                                        <td style="text-align: center;">
                                            <h5>
                                                <asp:Label runat="server"
                                                    ID="lblNoEval" Visible="false">暂无作品</asp:Label></h5>
                                        </td>
                                    </tr>
                                </table>
                                <div>
                                    <div class="hotPic">
                                        <div class="JQ-slide">
                                            <div class="JQ-slide-nav"><a class="prev" href="javascript:void(0);">&#8249;</a><a class="next" href="javascript:void(0);">&#8250;</a></div>
                                            <div class="wrap">
                                                <ul class="JQ-slide-content imgList">
                                                    <asp:DataList CssClass="rptPlanner" runat="server" ID="dlEval" RepeatDirection="Horizontal" RepeatColumns="100">
                                                        <ItemTemplate>
                                                            <li style="text-align: center;">
                                                                <a style="text-align: center;" class="img" target="_blank" href='ClassicCaseDetails.aspx?CaseID=<%#Eval("CaseID") %>'>
                                                                    <img src='<%#Eval("EvalImagePath") %>' width="140" height="100" class="imgEval" alt='<%#Eval("EvalTitle") %>' />
                                                                </a>
                                                                <a class="txt" href='ClassicCaseDetails.aspx?CaseID=<%#Eval("CaseID") %>'>
                                                                    <asp:Label runat="server" ID="lblEvalTitle" Text='<%#Eval("EvalTitle") %>' /></a>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </ul>
                                                <asp:HiddenField runat="server" ClientIDMode="Static" ID="HideCount" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


    </form>
</body>
</html>

