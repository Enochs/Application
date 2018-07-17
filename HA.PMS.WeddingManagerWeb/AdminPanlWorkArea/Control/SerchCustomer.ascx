<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SerchCustomer.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SerchCustomer" %>
<div id="searchCustomers">
    <div class="widget-content ">
        <div class="row-fluid">
            <div class="span9">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-ok"></i></span>
                        <h5>渠道查询操作</h5>
                    </div>
                    <div class="widget-content nopadding">
                        新人姓名:<span style="color: green;">(新郎 新娘 经办人都可)</span>
                        <asp:TextBox ID="txtCustomer" runat="server"></asp:TextBox>

                    </div>
                    <div class="widget-content nopadding">
                        电&nbsp;&nbsp; 话:<span style="color: green;">(新郎 新娘 经办人都可)</span>
                        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                        <asp:LinkButton CssClass="btn btn-success" Text="查找" OnClick="lkbtnQuery_Click" ID="lkbtnQuery" runat="server"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
