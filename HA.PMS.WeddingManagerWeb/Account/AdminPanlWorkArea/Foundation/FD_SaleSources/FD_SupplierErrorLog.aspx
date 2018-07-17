<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_SupplierErrorLog.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SupplierErrorLog" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>供应差错记录界面</h5>
            <span class="label label-info">记录界面</span>
        </div>
        <div class="widget-content">
               <!--查询操作 start -->
            
            <!--查询操作end-->
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>订单ID</th>
                        <th>评价对象</th>
                        <th>评价结果</th>
                        <th>有无差错</th>
                        <th>评价说明</th>
                        <th>改进建议</th>
                        <th>评价人</th>
                        <th>评价时间</th>
                         
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptSupplierErrorLog" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("OrderID") %></td>
                                <td>供应商</td>
                                <td><%#Eval("Point") %>分</td>
                                <td>有</td>
                                <td><%#Eval("Remark") %></td>
                                <td><%#Eval("Suggest") %></td>
                                <td><%#GetEmployeeName(Eval("CreateEmpLoyee")) %></td>
                                <td><%#GetDateStr(Eval("CreateDate")) %></td>
                                 
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            
            <cc1:AspNetPagerTool ID="SupplierErrorLogPager" AlwaysShow="true" OnPageChanged="SupplierErrorLogPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
