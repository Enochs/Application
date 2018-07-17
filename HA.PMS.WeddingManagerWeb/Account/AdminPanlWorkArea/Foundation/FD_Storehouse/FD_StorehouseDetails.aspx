<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>库存详细界面</h5>
            <span class="label label-info">详细界面</span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>产品名称</td>
                    <td>
                        <asp:Literal ID="ltlProductName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>项目</td>
                    <td>
                        <asp:Literal ID="ltlCategoryParent" runat="server"></asp:Literal>

                    </td>
                </tr>
                <tr>
                    <td>类别</td>
                    <td>
                        <asp:Literal ID="ltlCategoryID" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>尺寸</td>
                    <td>
                        <asp:Literal ID="ltlSpecifications" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>图片</td>
                    <td>
                        <asp:Image ImageUrl="#" ID="imgUrl" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>单价</td>
                    <td>
                        <asp:Literal ID="ltlPurchasePrice" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>销售价</td>
                    <td>
                        <asp:Literal ID="ltlSaleOrice" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>单位</td>
                    <td>
                        <asp:Literal ID="ltlUnit" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>数量</td>
                    <td>
                        <asp:Literal ID="ltlTotalQuantity" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>入库时间</td>
                    <td>
                        <asp:Literal ID="ltlAddTime" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>剩余数量
                    </td>
                    <td>

                        <asp:Literal Text="" ID="ltlSurplusCount" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>最近使用时间</td>
                    <td>

                        <asp:Literal Text="" ID="ltlRecently" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td>使用总次数
                    </td>
                    <td>

                        <asp:Literal Text="" ID="ltlMakeSum" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
