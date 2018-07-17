<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_ProductManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_ProductManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">

        function ShowWindowsUpdate(KeyID, Control) {
            var Url = "FD_ProductUpdate.aspx?ProductID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }


        $(document).ready(function () {

            showPopuWindows($("#createProduct").attr("href"), 800, 1000, "a#createProduct");

        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="FD_ProductCreate.aspx" class="btn btn-primary  btn-mini" id="createProduct">录入产品</a>


    <div class="widget-box">

        <div class="widget-content">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>产品名</th>
                        <th>渠道供应商</th>
                        <th>分类名称</th>
                        <th>单价</th>
                        <th>销售价</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptProduct" runat="server" OnItemCommand="rptProduct_ItemCommand">
                        <ItemTemplate>
                            <tr skey='<%#Eval("ProductID") %>'>
                                <td><%#Eval("ProductName") %></td>
                                <td><%#Eval("Name") %></td>
                                <td><%#Eval("CategoryName") %></td>
                                <td><%#Eval("ProductPrice") %></td>
                                <td><%#Eval("SalePrice") %></td>
                                <td>


                                    <asp:LinkButton ID="lkbtnDelete" CssClass="btn btn-danger btn-mini" CommandName="Delete" CommandArgument=' <%#Eval("ProductID") %>' runat="server">删除</asp:LinkButton>

                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowWindowsUpdate(<%#Eval("ProductID") %>, this);'>修改</a>


                                </td>
                            </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
