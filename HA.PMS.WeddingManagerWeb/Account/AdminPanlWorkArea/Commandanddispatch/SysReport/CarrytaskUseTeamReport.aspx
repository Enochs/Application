<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CarrytaskUseTeamReport.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.SysReport.CarrytaskUseTeamReport" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <table class="table table-bordered table-striped">
        <tr>
            <th style="width: 100px;">婚期</th>
            <th style="width: 100px;">
                <cc2:DateEditTextBox ID="txtPartyDay" runat="server"></cc2:DateEditTextBox>
            </th>
            <th style="width: 100px;">
                <asp:Button ID="btnSerch" runat="server" Text="查询" CssClass="btn" OnClick="btnSerch_Click" />
            </th>
     
    
        </tr>
        <tr>
            <th style="width: 100px;">类别</th>
            <th style="width: 100px;">项目</th>
            <th style="width: 100px;">专业团队</th>
     
    
        </tr>
        <asp:Repeater ID="repProductList" runat="server">
            <ItemTemplate>
                <tr>
                    <td><%#Eval("ParentCategoryName") %></td>
                    <td><%#Eval("CategoryName") %></td>
                    <td><%#GetProductByID(Eval("ProductID")) %></td>
          
             
                </tr>
            </ItemTemplate> 
        </asp:Repeater>
        <tr>
            <td colspan="3">
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>

