<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CommandbyGatherSupplierManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.CommandbyGatherSupplierManager" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable td").css({ "border": "0", "vertical-align": "top" });
            $("html").css({ "overflow-x": "hidden" });



        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--
        供应商管理汇总统计分析
        --->
    <div class="widget-box">
        <div class="widget-content">
            <div class="widget-box">
                <div class="widget-content">

                    <table class="queryTable">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>供应商名:<asp:DropDownList ID="ddlSupplierName" Width="130" runat="server"></asp:DropDownList>
                                        </td>

                                        <td>年份: 
                                         <cc1:ddlRangeYear ID="ddlChooseYear" Width="130" yearStar="2013" yearEnd="2020" runat="server"></cc1:ddlRangeYear>
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="btnQuery" CssClass="btn btn-primary" OnClick="btnQuery_Click" runat="server" Text="查询" />

                                        </td>
                                    </tr>

                                </table>

                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr id="trContent">

                        <th></th>
                        <th>1</th>
                        <th>2</th>
                        <th>3</th>
                        <th>4</th>
                        <th>5</th>
                        <th>6</th>
                        <th>7</th>
                        <th>8</th>
                        <th>9</th>
                        <th>10</th>
                        <th>11</th>
                        <th>12</th>
                        <th>当年合计</th>
                        <th>上年合计</th>
                        <th>历史累计</th>
                    </tr>
                </thead>
                <tbody id="tbodyContent">
                    <tr>
                        <td>当期供货总次数</td>
                       <%=ViewState["sbDelivery"] %>
                    </tr>

                    <tr>
                        <td>当期总结算额</td>
                       <%=ViewState["sbRealityCost"] %>
                    </tr>

                    <tr>
                        <td>当期差错次数</td>
                        <%=ViewState["sbAppraise"] %>
                    </tr>

                    <tr>
                        <td>当期总满意度</td>
                        <%=ViewState["sbDegree"] %>
                    </tr>
                </tbody>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>

</asp:Content>
