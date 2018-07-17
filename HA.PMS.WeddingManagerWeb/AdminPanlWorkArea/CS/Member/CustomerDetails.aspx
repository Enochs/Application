<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CustomerDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.CustomerDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("td").css({ "white-space": "nowrap" });
            $("span").css({ "font-weight": "bold", "margin-right": "15px" });
            $("table").css({ "border": "1px" });
        });

    </script>
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>新人详细档案</h5>

        </div>

        <div class="widget-box">

            <table class="table table-bordered table-striped" border="1">
                <tr>
                    <%--<td style="white-space: nowrap;" rowspan="12" width="25"><span>联系方式</span>
                    </td>--%>
                    <td><span>新郎:</span><asp:Literal ID="ltlGroom" runat="server"></asp:Literal>
                    </td>
                    <td><span>新娘:</span><asp:Literal ID="ltlBride" runat="server"></asp:Literal>
                    </td>
                    <td><span>经办人:</span><asp:Literal ID="ltlOperator" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <%-- <td colspan="3"><span>联系方式</span></td>--%>
                </tr>
                <tr>
                    <td><span>手机1:</span><asp:Literal ID="ltlGroomCellPhone" runat="server"></asp:Literal></td>
                    <td><span>手机2:</span><asp:Literal ID="ltlBrideCellPhone" runat="server"></asp:Literal></td>
                    <td><span>手机3:</span><asp:Literal ID="ltlOperatorPhone" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td><span>e-mail1:</span><asp:Literal ID="ltlGroomEmail" runat="server"></asp:Literal></td>
                    <td><span>e-mail2:</span><asp:Literal ID="ltlBrideEmail" runat="server"></asp:Literal></td>
                    <td><span>e-mail3:</span><asp:Literal ID="ltlOperatorEmail" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td><span>微信1:</span><asp:Literal ID="ltlGroomteWeixin" runat="server"></asp:Literal></td>
                    <td><span>微信2:</span><asp:Literal ID="ltlBrideWeiXin" runat="server"></asp:Literal></td>
                    <td><span>微信3:</span><asp:Literal ID="Literal2" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td><span>微博1:</span><asp:Literal ID="ltlGroomWeiBo" runat="server"></asp:Literal></td>
                    <td><span>微博2:</span><asp:Literal ID="ltlBrideWeiBo" runat="server"></asp:Literal></td>
                    <td><span>微博3:</span><asp:Literal ID="Literal3" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td><span>QQ1:</span><asp:Literal ID="ltlGroomQQ" runat="server"></asp:Literal></td>
                    <td><span>QQ2:</span><asp:Literal ID="ltlBrideQQ" runat="server"></asp:Literal></td>
                    <td><span>QQ3:</span><asp:Literal ID="Literal4" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td><span>生日1:</span><asp:Literal ID="ltlGroomBirthday" runat="server"></asp:Literal></td>
                    <td><span>生日2:</span><asp:Literal ID="ltlBrideBirthday" runat="server"></asp:Literal></td>
                    <td><a href="/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?Sucess=1&OnlyView=1&CustomerID=<%=ViewState["CustomerID"] %>" target="_blank" class="btn  btn-success btn-mini" style="display: none;">查看邀约沟通记录</a>
                        <a target="_blank" href="/AdminPanlWorkArea/StoreSales/FollowOrderDetails.aspx?OnlyView=1&CustomerID=<%=ViewState["CustomerID"] %>" class="btn  btn-success btn-mini">查看沟通记录</a>
                    </td>
                </tr>
                <tr>
                    <td><span>身份证号码1:</span><asp:Literal ID="ltlGroomIDCard" runat="server"></asp:Literal></td>
                    <td><span>身份证号码2:</span><asp:Literal ID="ltlBrideIDCard" runat="server"></asp:Literal></td>
                    <td></td>
                </tr>
                <tr>
                    <td><span>职业1:</span><asp:Literal ID="ltlGroomJob" runat="server"></asp:Literal></td>
                    <td><span>职业2:</span><asp:Literal ID="ltlBrideJob" runat="server"></asp:Literal></td>
                    <td></td>
                </tr>
                <tr>
                    <td><span>单位1:</span><asp:Literal ID="ltlGroomJobCompany" runat="server"></asp:Literal></td>
                    <td><span>单位2:</span><asp:Literal ID="ltlBrideJobCompany" runat="server"></asp:Literal></td>
                    <td></td>
                </tr>
                <tr>
                    <td colspan="3"><span>说明:</span><asp:Label ID="lblOther" runat="server" Text=""></asp:Label>
                    </td>

                </tr>
                <tr style="display: none;">
                    <td colspan="3" runat="server" id="td_Rebates">返点说明:<asp:Label ID="lblRebates" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>

        </div>
        <div class="widget-box" style="height: 100px;">
            <table class="table table-bordered table-striped" border="1">

                <tr>
                    <td rowspan="2" style="white-space: nowrap;" width="25">
                        <span>服务概况</span></td>
                    <td style="white-space: nowrap;"><span>客户类型</span><asp:Literal ID="ltlCustomersType" runat="server"></asp:Literal>
                    </td>
                    <td style="white-space: nowrap;"><span>新人状态:</span><asp:Literal ID="ltlState" runat="server"></asp:Literal></td>
                    <td><span>策划师:</span><asp:Literal ID="ltlPlanner" runat="server"></asp:Literal></td>
                    <td><span>流失原因:</span><asp:Literal ID="ltlLoseMessage" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <%--<td><span>是否投诉</span><asp:Literal ID="ltkComplain" runat="server"></asp:Literal></td>--%>
                    <td><span>订单金额:</span><asp:Literal ID="ltlMoney" runat="server"></asp:Literal></td>
                    <td><span>利润率:</span><asp:Literal ID="ltllv" runat="server"></asp:Literal></td>
                    <td><span>满意度:</span><asp:Literal ID="ltlDegree" runat="server"></asp:Literal></td>
                    <td><span>取件时间:</span><asp:Literal ID="ltlTakeDate" runat="server"></asp:Literal></td>
                </tr>

            </table>
        </div>
        <div class="widget-box" style="height: 100px;">
            <table class="table table-bordered table-striped" border="1">
                <tr>
                    <td rowspan="3" style="white-space: nowrap;" width="25">
                        <span>婚宴概况</span></td>
                    <td runat="server" id="td_source"><span>来源渠道:</span><asp:Literal ID="ltlChannel" runat="server"></asp:Literal></td>
                    <td runat="server" id="td_channel"><span>渠道类型:</span><asp:Literal ID="ltlChannelType" runat="server"></asp:Literal></td>
                    <td runat="server" id="td_employee"><span>推荐人:</span><asp:Literal ID="ltlReferee" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td><span>婚期:</span><asp:Literal ID="ltlPartDate" runat="server"></asp:Literal></td>
                    <td><span>时段:</span><asp:Literal ID="ltlTimeSpan" runat="server"></asp:Literal>
                    </td>
                    <td><span>酒店:</span><asp:Literal ID="ltlWineShop" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td><span>桌数:</span><asp:Literal ID="ltlTableNumber" runat="server"></asp:Literal></td>
                    <td><span>婚礼预算:</span><asp:Literal ID="ltlPartyBudget" runat="server"></asp:Literal></td>
                    <td>
                        <img src="../../../Images/vips.jpg" /><asp:Literal ID="ltlIsVip" runat="server" /></td>
                </tr>
            </table>
        </div>
        <div class="widget-box" style="height: 170px;">
            <table class="table table-bordered table-striped" border="1">
                <tr>
                    <td rowspan="4" style="white-space: nowrap;" width="25">
                        <span>服务详细</span></td>
                    <td><span>录入人:</span><asp:Literal ID="ltlRecoder" runat="server"></asp:Literal></td>
                    <td><span>录入日期:</span><asp:Literal ID="ltlRecoderDate" runat="server"></asp:Literal></td>
                    <td><span>邀约人:</span><asp:Literal ID="ltlEmployee" runat="server"></asp:Literal></td>
                    <td></td>
                </tr>
                <tr>
                    <td><span>到店时间:</span><asp:Literal ID="ltlFirstCome" runat="server"></asp:Literal>
                    </td>
                    <td><span>跟单销售人:</span><asp:Literal ID="ltlOrderEmployee" runat="server"></asp:Literal></td>
                    <td><span>跟单次数:</span><asp:Literal ID="ltlOrderCount" runat="server"></asp:Literal></td>
                    <td><span>订单时间:</span><asp:Literal ID="ltlOrderDate" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td><span>签约时间:</span><asp:Literal ID="ltlQuotedSuccessDate" runat="server"></asp:Literal></td>
                    <td><span>回访时间:</span><asp:Literal ID="ltlReturnDate" runat="server"></asp:Literal></td>
                    <td><span>回访结果:</span><asp:Literal ID="ltlResult" runat="server"></asp:Literal></td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>

        <div class="widget-box">
        </div>



    </div>
</asp:Content>
