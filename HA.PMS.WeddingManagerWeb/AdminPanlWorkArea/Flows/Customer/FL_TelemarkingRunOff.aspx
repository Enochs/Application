<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FL_TelemarkingRunOff.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.FL_TelemarkingRunOff" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FL_CustomersUpdate.aspx?CustomerID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }
        function ShowDetailsWindows(KeyID, Control) {
            var Url = "FL_CustomersDetails.aspx?CustomerID=" + KeyID;
            $(Control).attr("id", "detailsShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {
            showPopuWindows($("#createCustomers").attr("href"), 700, 1000, "a#createCustomers");
            //$("#<//%=txtStar.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            //$("#<//%=txtEnd.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            $("a[rel=group]").fancybox();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>新人信息录入</h5>
        </div>

        <div class="widget-content ">
            <a href="#" class="btn btn-primary  btn-mini" id="newMessage">新消息</a>
            <a href="#" class="btn btn-primary  btn-mini" id="NotTelemarkting">未邀约</a>
            <a href="#" class="btn btn-primary  btn-mini" id="TelemarkingIng">邀约中</a>
            <a href="#" class="btn btn-primary  btn-mini" id="SuccessTelemark">邀约成功</a>
            <a href="#" class="btn btn-primary  btn-mini" id="TelemarkingRunOff">流失</a>
            <a href="#" class="btn btn-primary  btn-mini" id="newPersonDetails">新人明细</a>
            <a class="btn btn-primary  btn-mini" href="FL_CustomersCreate.aspx" id="createCustomers">录入新人信息 </a>
            <a class="btn btn-primary  btn-mini" id="TelemarkSum" href="#">邀约统计分析</a>
            <a class="btn btn-primary  btn-mini" id="createMission" href="#">创建新任务</a>
            <div class="widget-box">
                <div class="widget-content">

                    <table class="table table-bordered table-striped with-check">
                        <tr>
                            <td style="white-space: nowrap; width: 25%;">渠道名称:  </td>
                            <td style="white-space: nowrap; width: 25%;">
                                <asp:DropDownList ID="ddlChannel" runat="server"></asp:DropDownList>

                            </td>
                            <td style="white-space: nowrap; width: 25%; text-align: right;">流失原因:</td>
                            <td style="white-space: nowrap; width: 25%;">

                                <asp:DropDownList ID="ddlReasons" runat="server"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td style="white-space: nowrap;">接受时间:</td>
                            <td>
                                <asp:TextBox ID="txtStar" onclick="WdatePicker();" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: center;">到</td>
                            <td>
                                <asp:TextBox ID="txtEnd" onclick="WdatePicker();" runat="server"></asp:TextBox>

                            </td>
                        </tr>

                        <tr>
                            <td>
                                <asp:Button ID="btnQuery" runat="server" CssClass="btn" Text="查找" OnClick="btnQuery_Click" /></td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td></td>
                        </tr>

                    </table>
                </div>
            </div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th>联系方式</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>来源渠道</th>
                        <th>推荐人</th>
                        <th>接受日期</th>

                        <th>分派人</th>
                        <th>流失时间</th>
                        <th>流失原因</th>

                        <th>查看详细</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCustomers" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("Groom") %></td>
                                <td><%#Eval("GroomCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#Eval("Channel") %></td>
                                <td><%#Eval("Referee") %></td>
                                <td><%#GetDateStr(Eval("Todate")) %></td>
                                <td><%#Eval("Recorder") %></td>
                                <td><%#GetDateStr(Eval("ReasonsDate")) %></td>
                                <td><%#Eval("Reasons") %></td>

                                <td>
                                    <a href='#divWid<%#Eval("CustomerID") %>' rel="group" class="btn btn-primary btn-mini">查看</a>
                                    <div class="widget-box" id='divWid<%#Eval("CustomerID") %>' style="display: none; width: 500px;">
                                        <div class="widget-content">
                                            <div class="row-fluid">
                                                <div class="span7">
                                                    <div class="widget-box">
                                                        <div class="widget-title">
                                                            <span class="icon"><i class="icon-ok"></i></span>
                                                            <h6>详细信息</h6>
                                                        </div>
                                                        <div class="widget-content nopadding">
                                                            流失原因:<%#Eval("Reasons") %><br />说明:<%#Eval("ReasonsExplain") %></div>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </td>
                            </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="CustomerPager" AlwaysShow="true" OnPageChanged="CustomerPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>



</asp:Content>
