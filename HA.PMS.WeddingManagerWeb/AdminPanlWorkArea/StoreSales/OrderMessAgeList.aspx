<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderMessAgeList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.OrderMessAgeList" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
 <script>
     
     $(document).ready(function () {
         $("html,body").css({ "width": "600px", "height": "300px", "background-color": "transparent" });

     });

    </script>
    <div id="tab1" class="tab-pane active">
        <div class="alert">
            辅导意见
        </div>
        <div class="widget-box">
        </div>
    </div>
    <table class="table table-bordered table-striped with-check">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <asp:Repeater ID="repOrderMessage" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="word-break:break-all;">
                        标题：<%#Eval("Title") %></td>
                </tr>
                <tr>
                    <td>
                        <%#Eval("Message") %>
                        <br />
                        辅导时间：<%#Eval("CreateDate") %></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>

</asp:Content>


