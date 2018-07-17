<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_StorehouseUpdate.aspx?StorehouseID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }

        function ShowDetailsWindows(KeyID, Control) {
            var Url = "FD_StorehouseDetails.aspx?StorehouseID=" + KeyID;
            $(Control).attr("id", "detailsShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }
        
        $(document).ready(function () {
            //$("#</%=txtStar.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            //$("#</%=txtEnd.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            showPopuWindows($("#createStore").attr("href"), 700, 1000, "a#createStore");
            //加载对应图片浏览效果 begin
            $(".grouped_elements").each(function (indexs, values) {
                var imgChildren = $(this).children("img");
                $(this).attr("href", imgChildren.attr("src"));

            });
            $("a.grouped_elements").fancybox();
            //end
        });
        $(window).load(function () {
            $("#<%=txtSurplusCount.ClientID%>").change(function () {
                var num = parseFloat($(this).val());
                if (num) {
                    $(this).val(num);
                } else { $(this).val(0); }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="FD_StorehouseCreate.aspx" class="btn btn-primary  btn-mini" id="createStore">添加库存</a>

    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>库存管理</h5>
            <span class="label label-info"></span>
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
                            产品项目:<asp:DropDownList ID="ddlCategoryProject" runat="server" AutoPostBack="True"></asp:DropDownList>
                            产品类别:<asp:DropDownList ID="ddlCategoryType" runat="server" OnSelectedIndexChanged="ddlCategoryType_SelectedIndexChanged"></asp:DropDownList>
                            <br />
                            剩余数量:<asp:TextBox ID="txtSurplusCount" MaxLength="10" runat="server"></asp:TextBox>
                            产品关键字:<asp:TextBox ID="txtProjectName" runat="server" MaxLength="20"></asp:TextBox>
                            <br />
                            入库时间:<asp:TextBox ID="txtStar" onclick="WdatePicker()" runat="server" MaxLength="20"></asp:TextBox>
                            至
                            <asp:TextBox ID="txtEnd" onclick="WdatePicker()" runat="server" MaxLength="20"></asp:TextBox>
                            <br />
                            <asp:Button ID="btnQuery" CssClass="btn btn-success" runat="server" Text="查询" OnClick="btnQuery_Click" />
                                                        <asp:Button ID="btnExport" CssClass="btn btn-success" runat="server" Text="导出到Excel" OnClick="btnExport_Click" />
                        
                        </div>

                    </div>
                </div>
            </div>
            
            <!--查询操作end-->
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>产品名称</th>
                        <th>入库时间</th>
                        <th>产品类别</th>
                        <th>项目</th>
                        <th>尺寸</th>
                        <th>图片</th>
                        <th>单价</th>
                        <th>销售价</th>
                        <th>数量</th>
                        <th>单位</th>
                        <th>使用次数</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptStoreHouse" OnItemCommand="rptStoreHouse_ItemCommand" runat="server">
                        <ItemTemplate>

                            <tr>
                                <td><%#Eval("ProductName") %></td>
                                <td><%#Eval("AddTime") %></td>
                                <td><%#Eval("CategoryName") %></td>
                                <td><%#Eval("CategoryParent") %></td>
                                <td><%#Eval("Specifications") %></td>
                                <td>

                                    <a class="grouped_elements"   href="#" rel="group1">
                                        <asp:Image ID="imgStore" ImageUrl='<%#Eval("Image") %>' Width="100" Height="70" runat="server" />
                                    </a>

                                </td>
                                <td><%#Eval("PurchasePrice") %></td>
                                <td><%#Eval("SaleOrice") %></td>
                                <td><%#Eval("TotalQuantity") %></td>
                                <td><%#Eval("Unit") %></td>
                                <td><%#Eval("TotalQuantity") %></td>
                                <td>
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lkbtnDelete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("StorehouseID") %>'>删除</asp:LinkButton>

                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("StorehouseID") %>,this);'>修改</a>

                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowDetailsWindows(<%#Eval("StorehouseID") %>,this);'>明细</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="StorePager" AlwaysShow="true" OnPageChanged="StorePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
