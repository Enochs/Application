<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_ImageWarehouseManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_ImageWarehouse.FD_ImageWarehouseManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">
        function ShowOperImg(KeyID, Control) { 
           
          
            var Url = "/AdminPanlWorkArea/Foundation/FD_Hotel/FD_HotelImageLoadFile.aspx?ImageID=" + KeyID + "&toOperPage=" + <%=ViewState["load"]%>;
             $(Control).attr("id", "updateShow" + KeyID);
             showPopuWindows(Url, 800, 300, "a#" + $(Control).attr("id"));
           
           
        }

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_ImageWarehouseUpdate.aspx?ImageID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {

            showPopuWindows($("#createImageWarehouse").attr("href"), 700, 1000, "a#createImageWarehouse");
            //加载对应图片浏览效果 begin
            $(".grouped_elements").each(function (indexs, values) {
                var imgChildren = $(this).children("img");
                $(this).attr("href", imgChildren.attr("src"));

            });
            $("a.grouped_elements").fancybox();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
  


    <div class="widget-box">
        
        <div class="widget-content">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>图片名</th>
                        <th>图片</th>
                        <th>图片类型</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptImageWarehouse" runat="server" OnItemCommand="rptImageWarehouse_ItemCommand">
                        <ItemTemplate>

                            <tr skey='<%#Eval("ImageID") %>'>
                                <td><%#Eval("ImageTitle") %></td>

                                <td>
                                    <a class="grouped_elements"   href="#" rel="group1">
                                        <asp:Image ID="imgStore" ImageUrl='<%#Eval("ImageUrl") %>' Width="80" Height="50" runat="server" />
                                    </a>
                                </td>
                                <td><%#Eval("TypeName") %></td>
                                <td>
                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("ImageID") %>,this);'>修改</a>

                                    <asp:LinkButton CssClass="btn btn-danger btn-mini"  ID="lkbtnDelete" CommandName="Delete" runat="server">删除</asp:LinkButton>


                                    <a href="#" class="btn btn-primary  btn-mini" onclick="ShowOperImg(<%#Eval("ImageID") %>,this)">上传图片</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="ImagePager" AlwaysShow="true" OnPageChanged="ImagePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>

</asp:Content>
