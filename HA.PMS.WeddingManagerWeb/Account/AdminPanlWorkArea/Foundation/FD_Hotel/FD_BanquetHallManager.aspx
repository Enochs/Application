<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_BanquetHallManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel.FD_BanquetHallManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
     <script type="text/javascript">
         function ShowImgWindows(KeyID, Control) {
             var Url = "FD_FD_BanquetHallImgManager.aspx?BanquetHallID=" + KeyID;
             $(Control).attr("id", "imgShow" + KeyID);
             showPopuWindows(Url, 800, 300, "a#" + $(Control).attr("id"));
         }

         function ShowOperImg(KeyID, Control) {
             var Url = "FD_HotelImageLoadFile.aspx?BanquetHallID=" + KeyID + "&toOperPage=" + <%=ViewState["load"]%>;
              $(Control).attr("id", "updateShow" + KeyID);
              showPopuWindows(Url, 800, 300, "a#" + $(Control).attr("id"));
         }

         function ShowUpdateWindows(KeyID,hotelID, Control) {
             var Url = "FD_BanquetHallUpdate.aspx?BanquetHallID=" + KeyID + "&hotelID=" + hotelID;
             $(Control).attr("id", "updateShow" + KeyID);
             showPopuWindows(Url, 400, 300, "a#" + $(Control).attr("id"));
         }

         $(document).ready(function () {

             showPopuWindows($("#createHotel").attr("href"), 500, 300, "a#createHotel");

         });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
          <a href="FD_BanquetHallCreate.aspx?HotelId=<%= ViewState["HotelId"] %>" class="btn btn-primary  btn-mini" id="createHotel">添加宴会厅</a>
        &nbsp; <a href="FD_HotelManager.aspx" class="btn btn-info btn-mini">返回酒店管理</a>
        <div class="widget-content">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>宴会厅名称</th>
                        <th>楼层名</th>
                        <th>层高（米）</th>
                        <th>桌数</th>
                        <th>操作</th>

                    </tr>

                </thead>
                <tbody>

                    <asp:Repeater ID="rptBanquetHall" OnItemCommand="rptBanquetHall_ItemCommand" runat="server">
                        <ItemTemplate>
                            <tr skey='<%#Eval("BanquetHallID") %>'>
                                <td><%#Eval("HallName") %></td>
                                <td><%#Eval("FloorName") %></td>
                                <td><%#string.Format("{0:f1}", Eval("FloorHeight")) %></td>
                                <td><%#Eval("DeskCount") %></td>
                                <td>
                                     <a href="#" class="btn btn-primary  btn-mini" onclick='ShowOperImg(<%#Eval("BanquetHallID") %>,this);'>添加图片</a>&nbsp;
                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowImgWindows(<%#Eval("BanquetHallID") %>,this);'>管理图片</a>&nbsp;
                                     <asp:LinkButton CssClass="btn btn-danger btn-mini"  ID="lkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("BanquetHallID") %>' runat="server">删除</asp:LinkButton>
                                   <a  href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("BanquetHallID") %>,<%#Eval("HotelId") %>,this);'>修改</a>

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                </tbody>
                <tfoot>
                    <tr><td colspan="5">
                        <cc1:AspNetPagerTool ID="BanquetHallPager" AlwaysShow="true" OnPageChanged="BanquetHallPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
                        </td></tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>
