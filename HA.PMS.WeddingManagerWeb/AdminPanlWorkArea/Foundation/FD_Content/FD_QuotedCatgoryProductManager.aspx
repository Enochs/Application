<%@ Page Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_QuotedCatgoryProductManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.FD_QuotedCatgoryProductManager" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID,ProductCategory,ProjectCategory, Control) {
            var Url ="";
     
            Url= "FD_QuotedCatgoryProductCreateUpdate.aspx?SourceProductId=" + KeyID + "&ProductCategory="+ProductCategory+"&ProjectCategory="+ProjectCategory+"&PageIndex="+<%=StorePager.CurrentPageIndex%>;
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

 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnbinderdata" Style="display: none" Text="btnbinderdata" OnClick="BinderData" runat="server" />
    <div style="overflow-x: auto;">
        <div class="widget-box">

            <table class="table">
                <tr>
                    <td>产品关键字
                        <asp:TextBox ID="txtProductName" Width="65" runat="server" MaxLength="20" OnTextChanged="txtProductName_TextChanged"></asp:TextBox></td>
                    <td>产品类别
                        <asp:DropDownList ID="ddlParentCatogry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlParentCatogry_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>项目<asp:DropDownList ID="ddlSecondCatgory" runat="server"></asp:DropDownList>
                        <br />
                    </td>
                    <td style="text-align: center; vertical-align: middle;">
                        <asp:Button ID="btnSerch" CssClass="btn btn-success btn-mini" runat="server" Text="查询" OnClick="btnSerch_Click" Width="90" /></td>
                    <td style="text-align: center; vertical-align: middle;">
                        <span>
                            <asp:Button ID="btnExport" Style="padding: 0 4px;" CssClass="btn btn-success btn-mini" runat="server" Text="导出到Excel" OnClick="btnExport_Click" />
                        </span>
                    </td>
                    <td style="text-align: left; vertical-align: middle;">
                        <a href="#" id="ACreateProduct" class="btn btn-mini btn-success" onclick="ShowUpdateWindows('',0,0,this);">添加产品</a>
                    </td>
                </tr>
            </table>

            <table class="table table-bordered table-select">
                <thead>
                    <tr>
                        <th style="white-space: nowrap;">产品名称</th>
                        <th style="white-space: nowrap">产品类别</th>
                        <th style="white-space: nowrap">项目</th>
                        <th style="white-space: nowrap">产品\服务描述</th>
                        <th style="white-space: nowrap">图片</th>

                        <th style="white-space: nowrap">销售单价</th>
                        <th style="white-space: nowrap">数量</th>
                        <th style="white-space: nowrap">单位</th>
                        <th style="white-space: nowrap">备注</th>
                        <th style="white-space: nowrap">仓位</th>
                        <th style="white-space: nowrap">产品属性</th>
                        <th style="white-space: nowrap">操作</th>
                    </tr>
                </thead>

                <tbody>
                    <asp:Repeater ID="rptStorehouse" OnItemCommand="rptStorehouse_ItemCommand" OnItemDataBound="rptStorehouse_ItemDataBound" runat="server">
                        <ItemTemplate>
                            <tr skey='FD_Storehouse<%#Eval("Keys") %>'>
                                <td><span style="cursor: default" title='<%#Eval("ProductName") %>'><%#ToInLine(Eval("ProductName"),6) %></span></td>
                                <td><span style="cursor: default" title='<%#GetQuotedCategoryName(Eval("ProductCategory")) %>'><%#ToInLine(GetQuotedCategoryName(Eval("ProductCategory")),5) %></span></td>
                                <td><span style="cursor: default" title='<%#GetQuotedCategoryName(Eval("ProjectCategory")) %>'><%#ToInLine(GetQuotedCategoryName(Eval("ProjectCategory")),5) %></span></td>
                                <td><span style="cursor: default" title='<%#Eval("Specifications") %>'><%#ToInLine(Eval("Specifications"),8) %></span></td>
                                <td><a class="grouped_elements" href="#" rel="group1">
                                    <asp:Image ID="imgStore" ImageUrl='<%#Eval("Data") %>' Width="100" Height="50" runat="server" /></a>
                                    <asp:LinkButton ID="lkbtnDownLoad" CssClass="btn btn-primary  btn-mini" CommandArgument='<%#Eval("KindID") %>' CommandName="DownLoad" runat="server"></asp:LinkButton>
                                </td>
                                <td><%#Eval("SalePrice") %></td>
                                <td><%#Eval("Count") %></td>
                                <td><%#Eval("Unit") %></td>
                                <td><span style="cursor: default" title='<%#Eval("Remark") %>'><%#ToInLine(Eval("Remark"),8) %></span></td>
                                <td><%#Eval("Position") %></td>
                                <td><%#Eval("Classification") %></td>
                                <td style="width: 90px">
                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("KindID") %>,<%#Eval("ProductCategory") %>,<%#Eval("ProjectCategory") %>,this);'>修改</a>
                                    <asp:LinkButton ID="lkbtnDelete" CommandName="Delete" CssClass="btn btn-danger btn-mini" CommandArgument='<%#Eval("KindID") %>' runat="server" OnClientClick="return confirm('确认删除此产品!')">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="10">
                            <cc1:AspNetPagerTool ID="StorePager" PageSize="15" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="10">
                            <asp:Label runat="server" ID="lblProductSum" Style="font-weight: bold; font-size: 13px; color: #808080; outline-width: initial;" /></td>
                    </tr>
                </tfoot>
            </table>

        </div>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>
