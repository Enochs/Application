<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_SupplierTypeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.FD_SupplierTypeManager" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
      <script type="text/javascript">

          function ShowWindows(KeyID, Control) {
              var Url = "FD_SupplierTypeUpdate.aspx?typeId=" + KeyID ;
              $(Control).attr("id", "updateShow" + KeyID);
              showPopuWindows(Url, 500, 800, "a#" + $(Control).attr("id"));
          }


          $(document).ready(function () {

              showPopuWindows($("#ff").attr("href"), 400, 1000, "a#ff");

              $("html,body").css({ "width": "700px", "height": "500px", "background-color": "transparent" });

          });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <a id="ff" class="btn btn-primary  btn-mini" href="FD_SupplierTypeCreate.aspx">添加供应商类别</a>
    <asp:Button ID="Button1" runat="server" Text="退出" class="btn btn-primary  btn-mini" OnClick="Button1_Click"  />
    <div class="widget-box">

        <div class="widget-content">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>类别编号</th>
                        <th>类别名</th>
                        <th>操作栏</th>
                    </tr>
                </thead>
                <tbody>

                    <asp:Repeater ID="rptSupplierType" OnItemCommand="rptSupplierType_ItemCommand" runat="server">

                        <ItemTemplate>

                            <tr skey='<%#Eval("SupplierTypeId") %>'>
                                <td><%#Eval("SupplierTypeId") %></td>
                                <td><%#Eval("TypeName") %></td>
                                <td>
                                    <a class="btn btn-primary  btn-mini" href="#" onclick='ShowWindows(<%#Eval("SupplierTypeId") %>,this);'>修改</a>
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lkbtnDelete" runat="server" CommandName="Delete" CommandArgument='<%#Eval("SupplierTypeId") %>'>删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="SupplierTypePager"  AlwaysShow="true" OnPageChanged="SupplierTypePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
