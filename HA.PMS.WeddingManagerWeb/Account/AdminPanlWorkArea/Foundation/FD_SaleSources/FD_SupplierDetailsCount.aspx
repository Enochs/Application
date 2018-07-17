<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_SupplierDetailsCount.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SupplierDetailsCount" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>供货明细表</h5>
            
        </div>
        <div class="widget-content">
               <!--查询操作 start -->
            
            <!--查询操作end-->
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>订单ID</th>
                        <th>姓名查询</th>
                        <th>酒店</th>
                        <th>电话</th>
                        <th>操作</th>
                        
                         
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
                              
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            
            <cc1:AspNetPagerTool ID="SupplierErrorLogPager" AlwaysShow="true" 
                OnPageChanged="SupplierErrorLogPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
