<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_StrorehouseInuseProduct.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StrorehouseInuseProduct" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function ShowDetailsWindows(KeyID, Control) {
            var Url = "FD_ProductDetails.aspx?OrderCoder=" + KeyID;
            $(Control).attr("id", "detailsShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
      //$("#</%=txtStar.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
      //$("#</%=txtEnd.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
    
      //end
  }); </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>使用的中产品</h5>
            <span class="label label-info">管理页面</span>
        </div>
        <div class="widget-content">
            <!--查询操作 start -->
            <div class="row-fluid">
                <div class="span9">
                    <div class="widget-box">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-ok"></i></span>
                            <h5>查询操作</h5>
                        </div>
                        <div class="widget-content nopadding">
                          婚期  <asp:TextBox ID="txtStar" onclick="WdatePicker()" runat="server" MaxLength="20"></asp:TextBox>
                            至<asp:TextBox ID="txtEnd" onclick="WdatePicker()" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:Button ID="btnQuery" runat="server" CssClass="btn btn-success" Text="确定" OnClick="btnQuery_Click" />
                        </div>

                    </div>
                </div>
            </div>
            <!--查询操作end-->
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>订单编号</th>
                        <th>新人姓名</th>
                        <th>策划师</th>
                        <th>派工人</th>
                        <th>酒店</th>
                        <th>婚期</th>
                        <th>产品明细</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptStroe" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("OrderCoder") %></td>
                                <td><%#Eval("CustomerName") %></td>
                                <td><%#Eval("Planner") %></td>
                                <td><%#Eval("WorkersPerson") %></td>
                                <td><%#Eval("WinShop") %></td>
                                <td><%#Eval("PartyDate") %></td>
                                <td>              
                                    <a href="#"  onclick="ShowDetailsWindows('<%#Eval("OrderCoder") %>',this)"
                                         class="btn btn-primary  btn-mini">产品明细</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="InusePager" PageSize="10" AlwaysShow="true" OnPageChanged="InusePager_PageChanged" runat="server">
            </cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
