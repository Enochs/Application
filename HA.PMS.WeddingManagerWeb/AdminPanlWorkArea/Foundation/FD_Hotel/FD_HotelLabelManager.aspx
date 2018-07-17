<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_HotelLabelManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel.FD_HotelLabelManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
      <script type="text/javascript">

          function ShowUpdateWindows(KeyID, Control) {
              var Url = "FD_HotelLabelUpdate.aspx?HotelLabelID=" + KeyID;
              $(Control).attr("id", "updateShow" + KeyID);
              showPopuWindows(Url, 400, 300, "a#" + $(Control).attr("id"));
          }

          $(document).ready(function () {

              showPopuWindows($("#createHotel").attr("href"), 400, 300, "a#createHotel");

          });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <a href="FD_HotelLabelCreate.aspx" class="btn btn-primary  btn-mini" id="createHotel">创建标签</a>

     &nbsp; <a href="FD_HotelManager.aspx" class="btn btn-info btn-mini">返回酒店管理</a>
    <div class="widget-box">

        <div class="widget-content">
            <table class="table table-bordered table-striped table-select" style="width: 300px;">
                <thead>
                    <tr>
                        <th>场地标签名</th>

                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptLabel" OnItemCommand="rptLabel_ItemCommand" runat="server">
                        <ItemTemplate>
                            <tr skey='<%#Eval("HotelLabelID") %>'>
                                <td><%#Eval("HotelLabelName") %>
                                    
                                </td>
                                <td>
                                   
                                     <asp:LinkButton CssClass="btn btn-danger btn-mini"  ID="lkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("HotelLabelID") %>' runat="server">删除</asp:LinkButton>
                                   <a  href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("HotelLabelID") %>,this);'>修改</a>

                                </td>
                            </tr>
                        </ItemTemplate>
                      
                    </asp:Repeater>


                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2">
                            
                            
                            <cc1:AspNetPagerTool ID="LabelPager" PageSize="10" AlwaysShow="true" runat="server" OnPageChanged="LabelPager_PageChanged">
                            </cc1:AspNetPagerTool>
                            
                            
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>

</asp:Content>
