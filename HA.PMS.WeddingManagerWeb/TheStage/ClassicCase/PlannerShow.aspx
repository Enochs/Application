<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlannerShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.ClassicCase.PlannerShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        body {
            color: white;
            background-color: black;
            font-size: 15px;
            margin: 50px 10px 10px 10px;
            text-align: center;
        }

        .div_Main {
            width: 100%;
            height: 550px;
        }

        .main_Left {
            width: 100%;
            height: 550px;
        }

        .tableStyles tr td {
            vertical-align: top;
        }

        .tr_Styles {
            width: 550px;
            height: 405px;
            margin: 0px;
            padding: 0px;
            line-height: 30px;
            font-size: 14px;
            color: #b4a9a9;
        }

        .imgPlanner {
            width: 358px;
            height: 450px;
            border: 1px solid white;
        }

        .img_planners {
            border: 1px solid gray;
            cursor: pointer;
            height: 130px;
            width: 100px;
        }




        .div_Foot {
            width: 100%;
            height: auto;
        }

        .div_ShowEval {
            width: 100%;
        }

        .tb_Style {
            text-align: left;
            width:900px;
        }

        .tbl_in {
            text-align: center;
            width: 150px;
        }

        .imgEval {
            border: 1px solid white;
            width: 150px;
            height: 90px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_Main" align="center">
            <div class="main_Left">
                <table class="table tableStyles">
                    <tr>
                        <td rowspan="3">
                            <img runat="server" id="img_planner" alt="#" class="imgPlanner" /></td>
                        <td width="10px"></td>
                        <td colspan="2">
                            <asp:Label runat="server" ID="lblPlannerName" Style="font-size: 18px; font-weight: bolder;" /></td>
                        <td colspan="3"></td>
                        <td rowspan="3">
                            <asp:DataList runat="server" ID="dl_PlanerList" RepeatColumns="3">
                                <ItemTemplate>

                                    <a href='PlannerShow.aspx?PlannerID=<%#Eval("PlannerID") %>'>
                                        <img class="img_planners" src='<%#Eval("PlannerImagePath") %>' title='<%#Eval("PlannerName") %>' />
                                    </a>

                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                    <tr>
                        <td width="10px"></td>
                        <td colspan="2" width="550px">
                            <asp:Label runat="server" ID="lblTypeName" Style="font-size: 12px; font-weight: bolder; color: #c2bbbb" /></td>
                    </tr>
                    <tr class="tr_Styles" style="display: none;">
                        <td width="10px"></td>
                        <td>姓名</td>
                        <td>
                            <asp:Label runat="server" ID="lblPlannerNames" /></td>
                        <td width="50px;"></td>
                        <td>职务</td>
                        <td>
                            <asp:Label runat="server" ID="lblTypeNames" /></td>
                    </tr>
                    <tr class="tr_Styles">
                        <td width="10px"></td>
                        <td colspan="5" width="550px">
                            <asp:Label runat="server" ID="lblPlannerJobDescription" Text='<%#Eval("PlannerJobDescription") %>' Width="545px" />
                        </td>
                    </tr>
                </table>
                <%--<table class="table tableStyles" style="border:1px solid white;width:80%;">
                    <tr>
                        <td style="font-size:15px; float:left;">
                            <h4>
                                <asp:Label runat="server" ID="lblEvalPro">代表作品</asp:Label></h4>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                            <h5>
                                <asp:Label runat="server" ID="lblNoEval" Visible="false">暂无作品</asp:Label></h5>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataList runat="server" ID="dlEval" RepeatDirection="Horizontal" RepeatColumns="5">
                                <ItemTemplate>

                                    <table class="tbl_in">
                                        <tr>
                                            <td>
                                                <label style="font-size: 13px; color: #f3600b;"><%#Eval("EvalTitle") %></label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <a target="_blank" href='ClassicCaseDetails.aspx?CaseID=<%#Eval("CaseID") %>'>
                                                    <img src='<%#GetImagePath(Eval("EvalImagePath")) %>' class="imgEval" title='<%#Eval("EvalTitle") %>' /></a>
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>

                                </ItemTemplate>
                            </asp:DataList>
                        </td>
                    </tr>
                </table>--%>
            </div>
        </div>
        <div class="div_Foot" align="center">
            <div class="div_ShowEval">
                <table class="tb_Style">
                    <tr>
                        <td>
                            <h4 style="font-size: 15px; text-align: left; float: left;">
                                <asp:Label runat="server" ID="lblEvalPro">代表作品</asp:Label></h4>
                        </td>
                    </tr>
                    <tr runat="server" id="tr_NoEval" visible="false">
                        <td style="text-align: center;">
                            <h5>
                                <asp:Label runat="server" ID="lblNoEval" Visible="false">暂无作品</asp:Label></h5>
                        </td>
                    </tr>
                    <tr>
                        <asp:DataList runat="server" ID="dlEval" RepeatDirection="Horizontal" RepeatColumns="5">
                            <ItemTemplate>
                                <td>
                                    <table class="tbl_in">
                                        <tr>
                                            <td>
                                                <label style="font-size: 13px; color: #f3600b;"><%#Eval("EvalTitle") %></label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <a target="_blank" href='ClassicCaseDetails.aspx?CaseID=<%#Eval("CaseID") %>'>
                                                    <img src='<%#GetImagePath(Eval("EvalImagePath")) %>' class="imgEval" title='<%#Eval("EvalTitle") %>' /></a>
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </ItemTemplate>
                        </asp:DataList>
                    </tr>
                </table>
            </div>
        </div>


    </form>
</body>
</html>
