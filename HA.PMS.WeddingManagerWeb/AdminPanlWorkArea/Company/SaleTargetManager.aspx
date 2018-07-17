<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="SaleTargetManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.SaleTargetManager" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">
        //添加
        function ShowCreateWindows(Control) {

            var Url = "SaleTargetCreate.aspx";
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));

        }
        //修改
        function ShowUpdateWindows(key, Control) {

            var Url = "SaleTargetUpdate.aspx?TargetKey=" + key;
            $(Control).attr("id", "updateShow" + key);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }
         
        $(document).ready(function () {

        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="#" id="crateSaleTarget"  class="btn btn-primary  btn-mini"  onclick="ShowCreateWindows(this)">添加销售目标</a>
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>销售目标管理</h5>
            <span class="label label-info"></span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>计划目标(单位:万)</th>
                        <th>月度</th>
                        <th>季度</th>
                        <th>年度</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptSaleTarget" runat="server">
                        <ItemTemplate>
                            <tr skey='<%#Eval("TargetKey") %>'>
                                <td><%#Eval("Target") %></td>
                                <td><%#Eval("Month") %></td>
                                <td><%#Eval("Quarter") %></td>
                                <td><%#Eval("Year") %></td>
                                <td>

                                     

                                    <a href="#" onclick='ShowUpdateWindows(<%#Eval("TargetKey") %>,this)'  class="btn btn-primary  btn-mini">修改</a>
                                </td>
                            </tr>

                        </ItemTemplate>

                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="SaleTargetPager" AlwaysShow="true" OnPageChanged="SaleTargetPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
