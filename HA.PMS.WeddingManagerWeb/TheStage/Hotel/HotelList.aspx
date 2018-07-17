<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotelList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.Hotel.HotelList" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>酒店搜索</title>
    <script src="../../App_Themes/Default/js/jquery.min.js"></script>
    <style type="text/css">
        .cb td {
            padding: 10px;
        }

        .cb label {
            display: inline-block;
            margin-left: 5px;
        }

        .TableSort tr td {
            border: 1px solid gray;
            font-size: 13px;
        }

        .body_main {
            margin-top: 10px;
            background-image: url('images/backgrounds/backgrounds/002.jpg');
        }

        .table_Top {
            text-align: center;
            margin: 10px 10px 10px 10px;
        }

        .TableSort {
            border: 1px solid gray;
            font-size: 13px;
            margin: 10px 10px 10px 10px;
        }

        .input_chk {
            margin-left: 10px;
        }

        .lblTitle {
            font-size: 18px;
            font-weight: bold;
            color: #5C5958;
        }

        .h4Price {
            color: #FF5684;
            font-size: 18px;
        }

        .table_Hotel tr td {
            border: 0px solid gray;
        }

        .td_details1 {
            height: 35px;
            text-align: left;
            margin-left: 80px;
            font-size: 13px;
            width: auto;
        }

        .td_details2 {
            text-align: left;
            margin-left: 80px;
            height: 35px;
        }

        .lblColor {
            color: #FF5684;
            width: 25px;
        }

        .table_Ting {
            width: 100%;
            height: auto;
        }

            .table_Ting tr td {
                height: 35px;
                vertical-align: top;
                /*border-top: 1px solid #808080;*/
                border-bottom: 1px solid #999999;
            }

        .TableSort tr td {
            text-align: center;
            width: 60px;
        }

        .lbtnSort {
            width: 120px;
        }

        a:hover {
            text-decoration: underline;
            /*color: #804421;*/
            /*font-weight: bolder;*/
        }
    </style>

    <script type="text/javascript">
        function checkAll(Type, Chk) {
            //var ChkTypeList = document.getElementById("ChkType");
            var chkInput = null;
            if (Type == 1) {
                chkInput = ChkType.getElementsByTagName("input");
            }
            else if (Type == 2) {
                chkInput = ChkDeskCount.getElementsByTagName("input");
            }
            else if (Type == 3) {
                chkInput = ChkPrice.getElementsByTagName("input");
            }
            else if (Type == 4) {
                chkInput = ChkDistrinct.getElementsByTagName("input");
            } else if (Type == 5) {
                chkInput = ChkLabel.getElementsByTagName("input");
            }
            var checkall = document.getElementById(Chk).value;

            if (checkall == "全选") {
                for (var i = 0; i < chkInput.length; i++) {
                    chkInput[i].checked = true;
                }
                if (Type == 4) {
                    chkInput = ChkDistrincts.getElementsByTagName("input");
                    for (var i = 0; i < chkInput.length; i++) {
                        chkInput[i].checked = true;
                    }
                }
                if (Type == 5) {
                    chkInput = ChkLabels.getElementsByTagName("input");
                    for (var i = 0; i < chkInput.length; i++) {
                        chkInput[i].checked = true;
                    }
                }
                document.getElementById(Chk).value = "反选";
            } else if (checkall == "反选") {
                for (var i = 0; i < chkInput.length; i++) {
                    chkInput[i].checked = false;
                }
                if (Type == 4) {
                    chkInput = ChkDistrincts.getElementsByTagName("input");
                    for (var i = 0; i < chkInput.length; i++) {
                        chkInput[i].checked = false;
                    }
                }

                if (Type == 5) {
                    chkInput = ChkLabels.getElementsByTagName("input");
                    for (var i = 0; i < chkInput.length; i++) {
                        chkInput[i].checked = false;
                    }
                }
                document.getElementById(Chk).value = "全选";
            }
        }


    </script>
</head>
<body class="body_main">
    <form id="form1" runat="server">
        <div class="main" style="background-color: white; width: 85%; margin-left: auto; margin-right: auto; text-align: center; margin-bottom: 50px">
            <div class="top" style="margin-bottom: 50px;">
                <br />
                <table class="table_Top" style="border: 1px solid gray; width: 85%">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblTypeTitle" Text="类型" />
                        </td>
                        <td>
                            <input type="checkbox" class="input_chk" id="chkAll" value="全选" onclick="checkAll(1, 'chkAll')" title="全选" />全选</td>
                        <td>
                            <asp:CheckBoxList runat="server" CssClass="cb" ID="ChkType" ClientIDMode="Static" RepeatDirection="Horizontal" RepeatColumns="10">
                                <asp:ListItem Value="2">五星级酒店</asp:ListItem>
                                <asp:ListItem Value="2">四星级酒店</asp:ListItem>
                                <asp:ListItem Value="1">特色餐厅</asp:ListItem>
                                <asp:ListItem Value="2">其他</asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblHotelNameTitle" Text="酒店名称:" />
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtHotelName" Style="margin-top: 8px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; border-bottom: 1px solid gray; height: 1px;" colspan="5"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblDeskCountTitle" Text="桌数" />
                        </td>
                        <td>
                            <input type="checkbox" id="chkAllDesk" value="全选" onclick="checkAll(2, 'chkAllDesk')" />全选</td>
                        <td colspan="3">
                            <asp:CheckBoxList runat="server" CssClass="cb" ID="ChkDeskCount" RepeatDirection="Horizontal" RepeatColumns="10">
                                <asp:ListItem Text=" 10桌以下" Value="0.10" />
                                <asp:ListItem Text=" 10-20桌" Value="10.20" />
                                <asp:ListItem Text=" 20-30桌" Value="20.30" />
                                <asp:ListItem Text=" 30-40桌" Value="30.40" />
                                <asp:ListItem Text=" 40-50桌" Value="40.50" />
                                <asp:ListItem Text=" 50-60桌" Value="50.60" />
                                <asp:ListItem Text=" 60桌以上" Value="60.50000" />
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; border-bottom: 1px solid gray; height: 1px;" colspan="5"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblPriceTitle" Text="价格" />
                        </td>
                        <td>
                            <input type="checkbox" id="ChkAllPrice" value="全选" onclick="checkAll(3, 'ChkAllPrice')" />全选</td>
                        <td colspan="3">
                            <asp:CheckBoxList runat="server" CssClass="cb" ID="ChkPrice" RepeatDirection="Horizontal" RepeatColumns="10">
                                <asp:ListItem Text=" 1500以下" Value="0.1500" />
                                <asp:ListItem Text=" 1500-2000" Value="1500.2000" />
                                <asp:ListItem Text=" 2000-2500" Value="2000.2500" />
                                <asp:ListItem Text=" 2500-3000" Value="2500.3000" />
                                <asp:ListItem Text=" 3000-3500" Value="3000.3500" />
                                <asp:ListItem Text=" 3500-4000" Value="3500.4000" />
                                <asp:ListItem Text=" 4000元以上" Value="4000.50000" />
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; border-bottom: 1px solid gray; height: 1px;" colspan="5"></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblLabel" Text="标签" />
                        </td>
                        <td>
                            <input type="checkbox" id="ChkAllLabel" value="全选" onclick="checkAll(5, 'ChkAllLabel')" />全选</td>
                        <td class="td_Distrinct" colspan="3">
                            <div runat="server" id="div_ShowPartLabel">
                                <asp:CheckBoxList runat="server" CssClass="cb" ID="ChkLabel" RepeatDirection="Horizontal" RepeatColumns="9">
                                </asp:CheckBoxList>
                            </div>
                            <div runat="server" id="div_ShowLabel" class="divShowAll" style="display: none;">
                                <asp:CheckBoxList runat="server" CssClass="cb" ClientIDMode="Static" ID="ChkLabels" RepeatDirection="Horizontal" RepeatColumns="9">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td style="text-align: center;" colspan="3">
                            <asp:HiddenField runat="server" ID="HiddenField1" ClientIDMode="Static" Value="0" />
                            <input type="button" id="LabelExpand" style="background-color: white; border: 1px solid white" value="︾" onclick="ChangeHeights()" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; border-bottom: 1px solid gray; height: 1px;" colspan="5"></td>
                    </tr>
                    <tr runat="server" class="tr_Distrinct" id="trDistrinct" style="height: 60px;">
                        <td class="td_Distrinct" id="tdname">
                            <asp:Label runat="server" ID="lblDistrinctTitle" Text="区域" />
                        </td>
                        <td class="td_Distrinct">
                            <input type="checkbox" id="ChkAllDistrinct" value="全选" onclick="checkAll(4, 'ChkAllDistrinct')" />全选</td>
                        <td class="td_Distrinct" colspan="3">
                            <div runat="server" id="div_ShowPart">
                                <asp:CheckBoxList runat="server" CssClass="cb" ID="ChkDistrinct" RepeatDirection="Horizontal" RepeatColumns="9">
                                </asp:CheckBoxList>
                            </div>
                            <div runat="server" id="div_ShowAll" class="divShowAll" style="display: none;">
                                <asp:CheckBoxList runat="server" CssClass="cb" ClientIDMode="Static" ID="ChkDistrincts" RepeatDirection="Horizontal" RepeatColumns="9">
                                </asp:CheckBoxList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td style="text-align: center;" colspan="3">
                            <asp:HiddenField runat="server" ID="HideIsShow" ClientIDMode="Static" Value="0" />
                            <input type="button" id="inputExpand" style="background-color: white; border: 1px solid white" value="︾" onclick="ChangeHeight()" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; border-bottom: 1px solid gray; height: 1px;" colspan="5">
                            <span style="border-width: 1px; margin-left: 60px;">
                                <asp:Button runat="server" ID="btnConfirm" Text="确定" OnClick="btnGet_Click" Style="color: white; background-color: #F82800; border: 0px solid white" />
                                <asp:Button runat="server" ID="btnReset" Text="重置条件" OnClick="btnGet_Click" Style="color: white; background-color: #F82800; border: 0px solid white" /></span>
                        </td>
                    </tr>
                </table>

                <script type="text/javascript">
                    $(document).ready(function () {
                        if (sessionStorage["show"] == "0") {
                            $("#div_ShowAll").css("display", "block");
                            document.getElementById('inputExpand').value = "︽";
                        } else {
                            $("#div_ShowAll").css("display", "none");
                            document.getElementById('inputExpand').value = "︾";
                        }

                        $("#btnReset").click(function () {
                            sessionStorage.clear();
                        });
                    });

                    function ChangeHeight() {
                        sessionStorage.clear();
                        var zhi = document.getElementById('inputExpand').value;
                        if (zhi == "︾") {
                            $("#div_ShowAll").slideToggle("slow");

                            document.getElementById('inputExpand').value = "︽";
                            sessionStorage["show"] = "0";

                        }
                        else if (zhi == "︽") {
                            $("#div_ShowAll").slideToggle("slow");

                            document.getElementById('inputExpand').value = "︾";
                            sessionStorage["show"] = "1";
                        }
                    }


                    $(document).ready(function () {
                        if (sessionStorage["shows"] == "0") {
                            $("#div_ShowLabel").css("display", "block");
                            document.getElementById('LabelExpand').value = "︽";
                        } else {
                            $("#div_ShowLabel").css("display", "none");
                            document.getElementById('LabelExpand').value = "︾";
                        }

                        $("#btnReset").click(function () {
                            sessionStorage.clear();
                        });
                    });

                    function ChangeHeights() {
                        sessionStorage.clear();
                        var zhi = document.getElementById('LabelExpand').value;
                        if (zhi == "︾") {
                            $("#div_ShowLabel").slideToggle("slow");

                            document.getElementById('LabelExpand').value = "︽";
                            sessionStorage["shows"] = "0";

                        }
                        else if (zhi == "︽") {
                            $("#div_ShowLabel").slideToggle("slow");

                            document.getElementById('LabelExpand').value = "︾";
                            sessionStorage["shows"] = "1";
                        }
                    }


                    // onbeforeunload 关闭之前执行 onunload 关闭之后执行
                    window.onunload = function () {
                        //return "确定离开页面吗？";
                        //sessionStorage.clear();
                    }

                </script>

                <br />
                <table class="TableSort" style="text-align: center;">
                    <tr>
                        <td>
                            <asp:LinkButton CssClass="lbtnSort" ClientIDMode="Static" runat="server" ID="lbtnSort" Text="默认排序" OnClick="lbtnSort_Click" /></td>
                        <td>
                            <asp:LinkButton CssClass="lbtnSort" ClientIDMode="Static" runat="server" ID="lbtnDeskSort" Text="桌数" OnClick="lbtnSort_Click" /></td>
                        <td>
                            <asp:LinkButton CssClass="lbtnSort" ClientIDMode="Static" runat="server" ID="lbtnPriceSort" Text="价格" OnClick="lbtnSort_Click" /></td>
                        <td>
                            <asp:CheckBox runat="server" CssClass="cb" ID="ChkReCommand" ForeColor="#ee6b1f" Text="有推荐" AutoPostBack="true" OnCheckedChanged="ChkReward_CheckedChanged" /></td>
                        <td>
                            <asp:CheckBox runat="server" CssClass="cb" ID="ChkOnSale" ForeColor="#ee6b1f" Text="有优惠" AutoPostBack="true" OnCheckedChanged="ChkReward_CheckedChanged" /></td>
                    </tr>
                </table>


            </div>
            <div class="show">
                <table class="table_Hotel">
                    <asp:Repeater runat="server" ID="rptHotel" OnItemDataBound="rptHotel_ItemDataBound">
                        <ItemTemplate>
                            <tr class="tr_row1">
                                <td rowspan="3">
                                    <a href='HotelDetails.aspx?HotelID=<%#Eval("HotelID") %>' target="_blank" class="lblTitle">
                                        <img src='<%#Eval("HotelImagePath") %>' title='<%#Eval("HotelName") %>' style="width: 385px; height: 240px;" /></a>

                                </td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td class="td_details1">
                                    <table>
                                        <tr>
                                            <td><a href='HotelDetails.aspx?HotelID=<%#Eval("HotelID") %>' target="_blank" class="lblTitle"><%#Eval("HotelName") %></a></td>
                                            <td>
                                                <img src="images/ReCommand.png" <%#IsShow(Eval("ReCommand"),1) %> /></td>
                                            <td>
                                                <img src="images/OnSale.png" <%#IsShow(Eval("OnSale"),2) %> /></td>
                                        </tr>
                                    </table>
                                </td>

                                <td>
                                    <h4 class="h4Price">￥<asp:Label runat="server" CssClass="lblColor" ID="lblStartPrice" Text='<%#Eval("PriceStar") %>' /><asp:Label Visible="false" runat="server" ID="lblEndPrice" Text='<%#Eval("PriceEnd") %>' /></h4>
                                </td>
                            </tr>
                            <tr class="tr_row2">
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                <td class="td_details2">
                                    <img runat="server" id="imgEvalScore" src='<%#GetImgScore(Eval("EvalScore")) %>' style="width: 95px;" />&nbsp;&nbsp;
                                    <asp:Label runat="server" CssClass="lblColor" Width="25px" ID="lblFen" Text='<%#Eval("EvalScore").ToString() == "0" ? "0.0" :  Eval("EvalScore") %>' />&nbsp;&nbsp;
                                    <asp:Label runat="server" ID="lblHotelType" Text='<%#Eval("HotelType") %>' Width="65px" />&nbsp;|&nbsp;
                                    <asp:Label runat="server" ID="lblAddress" ToolTip='<%#Eval("Address") %>' Style="max-width: 210px; white-space: nowrap;" Text='<%#Eval("Address").ToString().Length >=25 ? Eval("Address").ToString().Substring(0,21)+"…" : Eval("Address").ToString() %>' />(<asp:Label runat="server" ID="lblArea" Style="white-space: nowrap;" Text='<%#Eval("Area") %>' />)
                                </td>
                                <td class="td_details2">可容纳<asp:Label runat="server" CssClass="lblColor" ID="lblDeskCount" Text='<%#Eval("DeskCount") %>' />桌
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; height: 165px;">

                                    <asp:HiddenField runat="server" ID="HideHotelID" Value='<%#Eval("HotelID") %>' />
                                    <table class="table_Ting">
                                        <tr>
                                            <td style="border-top: 1px solid #999999;">宴会厅</td>
                                            <td style="border-top: 1px solid #999999;">桌数</td>
                                            <td style="border-top: 1px solid #999999;">层高</td>
                                        </tr>
                                        <asp:Repeater runat="server" ID="rptBanquetHall">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblHallName" Text='<%#Eval("HallName") %>' /></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblDeskCount" Text='<%#Eval("DeskCount") %>' /></td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblHeight" Text='<%#Eval("FloorHeight") %>' /></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr <%#GetVisible(Eval("HotelID")) %>>
                                            <td colspan="3" align="center" style="border-bottom: none;">
                                                <a target="_blank" href='HotelDetails.aspx?HotelID=<%#Eval("HotelID") %>'>
                                                    <asp:Label runat="server" ID="lblLook" Text="查看宴会厅" /></a>                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tfoot>
                        <tr>
                            <td>
                                <cc1:AspNetPagerTool ID="CtrPageIndex" OnPageChanged="CtrPageIndex_PageChanged" runat="server" PageSize="10"></cc1:AspNetPagerTool>
                            </td>
                        </tr>
                    </tfoot>
                </table>

            </div>

        </div>

    </form>
</body>
</html>
