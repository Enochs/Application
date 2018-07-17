<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_FD_BanquetHallImgManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel.FD_FD_BanquetHallImgManager" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $("html,body").css({ "width": "800px", "height": "550px", "background-color": "transparent" });
            $("a.grouped_elements").fancybox();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">

        <div class="widget-content">
            <table class="table table-bordered table-striped" style="width: 500px;">
                <thead>
                    <tr>
                        <th>图片名称</th>
                        <th>图片</th>
                        <th>操作</th>
                    </tr>

                </thead>
                <tbody>
                    <asp:Repeater ID="rptImg" OnItemCommand="rptImg_ItemCommand" runat="server">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfImgsValue" Value='<%#Eval("BanquetHallPath") %>' runat="server" />
                            <tr>

                                <td><%#Eval("BanquetHallImgName") %></td>
                                <td><a class="grouped_elements" href='<%#Eval("BanquetHallPath") %>' rel="group1">
                                    <img width="120" height="70" src='<%#Eval("BanquetHallPath") %>' alt="" />

                                </a>
                                </td>

                                <td>
                                    <asp:LinkButton ID="lkbtnDelete" CssClass="btn btn-danger btn-mini" CommandName="Delete" CommandArgument='<%#Eval("BanquetHallImgId") %>' runat="server">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="3">
                            <cc1:AspNetPagerTool ID="ImgListPager" AlwaysShow="true" OnPageChanged="ImgListPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>
