<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EarnestMoneyManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairs.EarnestMoneyManager" Title="定金处理" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ContentPlaceHolderID="head" runat="server" ID="Content1">
    <script src="/Scripts/trselection.js"></script>
 
    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 20px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }

        .widthStyle {
            width: 650px;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {




            //  $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {

                $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");


                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });
            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");

            //
            $("html").css({ "overflow-x": "hidden" });



        });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="Content2">
    <div class="widget-box">


        <div class="widget-box" style="height: 50px; border: 0px;">


            <table class="queryTable">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>时间:  
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rdoTimerSpan" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem>婚期</asp:ListItem>
                                        <asp:ListItem>预定时间</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>时间:<cc2:DateEditTextBox onclick="WdatePicker();" ID="txtTimerStar" runat="server"></cc2:DateEditTextBox>到
                                        <cc2:DateEditTextBox onclick="WdatePicker();" ID="txtTimerEnd" runat="server"></cc2:DateEditTextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnserch" runat="server" Text="查找" CssClass="btn btn-primary"   Height="27" OnClick="btnserch_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </div>


        <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
        <table class="table table-bordered table-striped table-select">
            <thead>
                <tr>
                    <th>新人姓名</th>
                    <th>联系电话</th>
                    <th>婚期</th>
                    <th>酒店</th>
                    <th>预定时间</th>
                    <th>婚礼顾问</th>
                    <th>定金</th>

                    <th>确认定金</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="repCustomer" runat="server" OnItemCommand="repCustomer_ItemCommand">
                    <ItemTemplate>
                        <tr skey='<%#Eval("OrderID") %>'>
                            <td><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></td>
                            <td><%#Eval("BrideCellPhone") %></td>
                            <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                            <td><%#Eval("Wineshop") %></td>
                            <td><%#GetShortDateString(Eval("LastFollowDate")) %></td>
                            <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                            <td><%#Eval("EarnestMoney") %></td>
                            <td id="Partd<%#Container.ItemIndex %>">
                                <label <%#Eval("EarnestFinish").ToString()=="True"?"style='display:none;'":"" %> >
                                    <asp:LinkButton ID="lnkbtnfinish" CommandArgument='<%#Eval("OrderID") %>' CommandName="Finish" runat="server" CssClass="btn btn-primary btn-mini">确认定金</asp:LinkButton>
                                </label>
                                
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="8">
                        <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged">
                        </cc1:AspNetPagerTool>
                        <asp:Button ID="btnSaveDate" runat="server" Text="保存" OnClick="btnSaveDate_Click"  Height="29" CssClass="btn btn-success" />
                    </td>

                </tr>
            </tfoot>
        </table>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>
