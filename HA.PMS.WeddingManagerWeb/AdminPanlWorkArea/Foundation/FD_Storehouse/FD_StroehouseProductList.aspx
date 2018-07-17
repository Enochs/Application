<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FD_StroehouseProductList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StroehouseProductList" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>
 <%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_StorehouseProductUpdate.aspx?SourceProductId=" + KeyID + "&PageIndex="+<%=StorePager.CurrentPageIndex%>;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 450, 1000, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            $(".grouped_elements").each(function (indexs, values) {if ($.trim($(this).html()) == "") {$(this).remove();}});
            $(".grouped_elements").each(function (indexs, values) {
                var imgChildren = $(this).children("img");
                $(this).attr("href", imgChildren.attr("src"));
            });
            $("a.grouped_elements").fancybox({
                "changeFade": "fast",
                'transitionIn': 'none',
                'transitionOut': 'none',
                'topRatio': 0,
            });
        });

        $(window).load(function () {
 
            BindCtrlEvent('input[check],textarea[check]');
   
        });
 
        function uploadonchange(ctrl)
        {
            if($(ctrl).val()=="")
            {
                $("#uploadpic").html("上传").attr("title","上传图片");
            }
            else
            {
                $("#uploadpic").html("已选").attr("title",$(ctrl).val());
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnbinderdata" style="display:none" Text="btnbinderdata" OnClick="BinderData" runat="server" />
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table class="table table-bordered table-select">
                <thead>
                    <tr>
                        <th>产品关键字<br /><asp:TextBox ID="txtProductName" Width="65" runat="server" MaxLength="20" OnTextChanged="txtProductName_TextChanged"></asp:TextBox></th>
                        <th>产品类别<br /><cc2:CategoryDropDownList Width="75" ID="ddlCategorySerch" ParentID="0" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorySerch_SelectedIndexChanged" runat="server"></cc2:CategoryDropDownList></th>
                        <th>项目<br /><cc2:CategoryDropDownList ID="ddlProjectSerch" runat="server" Width="75"></cc2:CategoryDropDownList></th>
                        <th style="text-align: center; vertical-align: middle;"><asp:Button ID="btnSerch" CssClass="btn btn-success btn-mini" runat="server" Text="查询" OnClick="btnSerch_Click" Width="90" /></th>
                        <th colspan="2" style="text-align: center; vertical-align: middle;">
                            &nbsp;</th>
                        <th colspan="2"></th>
                    </tr>
                    <tr>
                        <th style="white-space:nowrap">产品名称</th>
                        <th style="white-space:nowrap">产品类别</th>
                        <th style="white-space:nowrap">项目</th>
                        <th style="white-space:nowrap">产品\服务描述</th>
                        <th style="white-space:nowrap">图片</th>
                    
                        <th style="white-space:nowrap">数量</th>
                        <th style="white-space:nowrap">单位</th>
                        <th style="white-space:nowrap">备注</th>
                    
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptStorehouse" OnItemCommand="rptStorehouse_ItemCommand" OnItemDataBound="rptStorehouse_ItemDataBound" runat="server">
                        <ItemTemplate>
                            <tr skey='FD_Storehouse<%#Eval("Keys") %>'>
                                <td><span style="cursor:default" title='<%#Eval("ProductName") %>'><%#ToInLine(Eval("ProductName"),6) %></span></td>
                                <td><span style="cursor:default" title='<%#GetCategoryName(Eval("ProductCategory")) %>'><%#ToInLine(GetCategoryName(Eval("ProductCategory")),5) %></span></td>
                                <td><span style="cursor:default" title='<%#GetCategoryName(Eval("ProjectCategory")) %>'><%#ToInLine(GetCategoryName(Eval("ProjectCategory")),5) %></span></td>
                                <td><span style="cursor:default" title='<%#Eval("Specifications") %>'><%#ToInLine(Eval("Specifications"),8) %></span></td>
                                <td><a class="grouped_elements" href="#" rel="group1"><asp:Image ID="imgStore" ImageUrl='<%#Eval("Data") %>' Width="100" Height="50" runat="server" /></a>
 
                                </td>
                                
                                <td><%#Eval("Count") %></td>
                                <td><%#Eval("Unit") %></td>
                                <td><span style="cursor:default" title='<%#Eval("Remark") %>'><%#ToInLine(Eval("Remark"),8) %></span></td>
                            
                                
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="StorePager" PageSize="8" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
        </div>
 
    </div>
</asp:Content>
