<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_TelemarketingManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing.FD_TelemarketingManager" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">
        function ShowWindows(KeyID, Control) {
            var Url = "FD_TelemarketingUpdate.aspx?CustomerID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }
        function ShowWindows2(KeyID, Control) {
            var Url = "FD_TelemarketingDetails.aspx?CustomerID=" + KeyID;
            $(Control).attr("id", "detailsShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {

            //创建新渠道。
            showPopuWindows($("#createNewFD_Telemarketing").attr("href"), 1200, 1000, "a#createNewFD_Telemarketing");
            //新人信息录入
            showPopuWindows($("#createFD_Telemarketing").attr("href"), 700, 1000, "a#createFD_Telemarketing");
            $("#<%=txtRecorderDateStar.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            $("#<%=txtRecorderDateEnd.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });

        });


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a id="createFD_Telemarketing" class="btn btn-primary  btn-mini" href="FD_TelemarketingCreate.aspx">添加新人</a>
    <a href="../FD_SaleSources/FD_SaleSourcesCreate.aspx" id="createNewFD_Telemarketing" class="btn btn-primary  btn-mini">添加新渠道</a>
    <a href="#" id="FD_TelemarketingRebate" class="btn btn-primary  btn-mini">渠道返利统计</a>
    <a href="#" id="FD_TelemarketingSum" class="btn btn-primary  btn-mini">渠道汇总统计</a>
    <a href="#" id="FD_TelemarketingDetails" class="btn btn-primary  btn-mini">邀约分派明细</a>
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>基本信息展示</h5>
            <span class="label label-info">信息展示</span>
        </div>
        <div class="widget-content">
            <div class="widget-box">
                <div class="widget-content">

                    <table class="table table-bordered table-striped with-check">
                        <tr>
                            <td style="white-space: nowrap; width: 25%;">渠道名称：</td>
                            <td style="white-space: nowrap; width: 25%;">
                                <asp:DropDownList ID="ddlChanneName" runat="server"></asp:DropDownList>
                            </td>
                            <td style="white-space: nowrap; width: 25%; text-align: right;">渠道类型:</td>
                            <td style="white-space: nowrap; width: 25%;">
                                <asp:DropDownList ID="ddlChanneType" runat="server"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td style="white-space: nowrap;">录入日期:</td>
                            <td>
                                <asp:TextBox ID="txtRecorderDateStar" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: center;">到</td>
                            <td>
                                <asp:TextBox ID="txtRecorderDateEnd" runat="server"></asp:TextBox>

                            </td>
                        </tr>

                        <tr>
                            <td style="white-space: nowrap;">推荐人:</td>
                            <td>
                                <asp:DropDownList ID="ddlReferee" runat="server"></asp:DropDownList>
                            </td>
                            <td style="text-align: right;">新人状态:</td>
                            <td>
                              
                                <cc2:ddlCustomersState ID="ddlCustomerStatus" runat="server"></cc2:ddlCustomersState>
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

            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>渠道名称</th>
                        <th>渠道类型</th>
                        <th>新人姓名</th>
                        <th>联系电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>录入日期</th>
                        <th>录入人</th>
                        <th>新人状态</th>
                        <th>邀约负责人</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptTelemarketingManager" runat="server" OnItemCommand="rptTelemarketingManager_ItemCommand">
                        <ItemTemplate>
                            <tr skey='<%#Eval("CustomerID") %>'>
                                <td><%#Eval("Channel") %></td>
                                <td><%#Eval("ChannelType") %></td>
                                <td><%#Eval("Groom") %></td>
                                <td><%#Eval("GroomCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetDateStr(Eval("RecorderDate")) %></td>
                                <td><%#GetEmployeeName(Eval("Recorder")) %></td>
                                <td><%#Eval("CustomerStatus") %></td>
                                <td><%#Eval("Operator") %></td>
                                <td>
                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowWindows2(<%#Eval("CustomerID") %>,this);'>详细</a>&nbsp;
                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowWindows(<%#Eval("CustomerID") %>,this);'>修改</a>&nbsp;
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lbbtnDelete" CommandArgument='<%#Eval("CustomerID") %>' CommandName="Delete" runat="server">
                                    删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>

            </table>
            <cc1:AspNetPagerTool ID="TelemarketingPager" AlwaysShow="true" OnPageChanged="TelemarketingPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
