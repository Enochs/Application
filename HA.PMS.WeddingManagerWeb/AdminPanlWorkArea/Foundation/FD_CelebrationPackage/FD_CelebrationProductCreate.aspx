<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationProductCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage.FD_CelebrationProductCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>套系产品选取页面</h5>
         
        </div>
        <div class="widget-content">
            <table  class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>选择</th>
                        <th>产品/服务名称</th>
                        <th>具体要求</th>
                        <th>图片</th>
                        <th>建议销售价</th>
                        <th>使用次数</th>
                        <th>供应商</th>
                        <th>库存数量</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCelebrationPackageMakeQuotedPrice" runat="server">

                        <ItemTemplate>

                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkChoose" ToolTip='<%#Eval("ProductID") %>' runat="server" /></td>
                                <td><%#Eval("ProductName") %></td>
                                <td><%#Eval("Description") %></td>
                                <td><%#Eval("Image") %></td>
                                <td><%#Eval("SaleOrice") %></td>
                                <td>暂无</td>
                                <td><%#Eval("Name") %></td>
                                <td><%#Eval("TotalQuantity") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <br />
            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="确定" OnClick="btnSave_Click" />
        </div>
    </div>
</asp:Content>
