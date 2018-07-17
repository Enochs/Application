<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_ImageTypeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_ImageWarehouse.FD_ImageTypeManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_ImageTypeUpdate.aspx?TypeId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {

            showPopuWindows($("#createImageType").attr("href"), 700, 1000, "a#createImageType");




        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="FD_ImageTypeCreate.aspx" class="btn btn-primary  btn-mini" id="createImageType">创建图片库类型</a>
    <div class="widget-box">

        <div class="widget-content">
            <table  class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>图片名称</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptImageType" runat="server" OnItemCommand="rptImageType_ItemCommand">

                        <ItemTemplate>
                            <tr skey='<%#Eval("TypeId") %>'>
                                <td><%#Eval("TypeName") %></td>
                                <td>
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini"  ID="lkbtnDelete"  CommandName="Delete" CommandArgument='<%#Eval("TypeId") %>' runat="server">删除</asp:LinkButton>
                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("TypeId") %>,this);'>修改</a>
                                </td>
                            </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>


            </table>

        </div>
    </div>
</asp:Content>
