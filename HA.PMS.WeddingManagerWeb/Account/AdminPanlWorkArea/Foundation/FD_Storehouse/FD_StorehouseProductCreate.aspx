<%@ Page Title="库房产品类别/项目" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_StorehouseProductCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Storehouse.FD_StorehouseProductCreate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_StorehouseProductUpdate.aspx?SourceProductId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 450, 1000, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            $(".grouped_elements").each(function (indexs, values) { if ($.trim($(this).html()) == "") { $(this).remove(); } });
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
            BindString(20, '<%=txtSourceProductName.ClientID%>:<%=txtUnit.ClientID%>:<%=txtPosition.ClientID%>');
            BindMoney('<%=txtPurchasePrice.ClientID%>:<%=txtSaleOrice.ClientID%>');
            BindUInt('<%=txtSourceCount.ClientID%>');
            BindText(200, '<%=txtSpecifications.ClientID%>:<%=txtRemark.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnCreate.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function uploadpic() {
            $("#<%=flieUp.ClientID%>").click();
        }
        function uploadonchange(ctrl) {
            if ($(ctrl).val() == "") {
                $("#uploadpic").html("上传").attr("title", "上传图片");
            }
            else {
                $("#uploadpic").html("已选").attr("title", $(ctrl).val());
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Button ID="btnbinderdata" Style="display: none" Text="btnbinderdata" OnClick="BinderData" runat="server" />
    <table class="table table-bordered table-select">
        <thead>
            <tr>
                <th>产品关键字<br />
                    <asp:TextBox ID="txtProductName" Width="65" runat="server" MaxLength="20" OnTextChanged="txtProductName_TextChanged"></asp:TextBox></th>
                <th>产品类别<br />
                    <cc2:CategoryDropDownList Width="75" ID="ddlCategorySerch" ParentID="0" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorySerch_SelectedIndexChanged" runat="server"></cc2:CategoryDropDownList></th>
                <th>项目<br />
                    <cc2:CategoryDropDownList ID="ddlProjectSerch" runat="server" Width="75"></cc2:CategoryDropDownList></th>
                <th style="text-align: center; vertical-align: middle;">
                    <asp:Button ID="btnSerch" CssClass="btn btn-success btn-mini" runat="server" Text="查询" OnClick="btnSerch_Click" Width="90" /></th>
                <th colspan="2" style="text-align: center; vertical-align: middle;">
                    <span <%=SetStyleforSystemControlKey("ExportProduct",1) %>>
                        <asp:Button ID="btnExport" Style="padding: 0 4px;" CssClass="btn btn-success btn-mini" runat="server" Text="导出到Excel" OnClick="btnExport_Click" />
                    </span>
                </th>
                <th colspan="7"></th>
            </tr>
            <tr>
                <th style="white-space: nowrap">产品名称</th>
                <th style="white-space: nowrap">产品类别</th>
                <th style="white-space: nowrap">项目</th>
                <th style="white-space: nowrap">产品\服务描述</th>
                <th style="white-space: nowrap">图片</th>
                <th style="white-space: nowrap">采购单价</th>
                <th style="white-space: nowrap">销售单价</th>
                <th style="white-space: nowrap">数量</th>
                <th style="white-space: nowrap">单位</th>
                <th style="white-space: nowrap">备注</th>
                <th style="white-space: nowrap">仓位</th>
                <th style="white-space: nowrap">产品属性</th>
                <th style="white-space: nowrap">操作</th>
            </tr>
            <tr>
                <td>
                    <asp:TextBox Style="margin: 0" ID="txtSourceProductName" Width="65" check="1" tip="限20个字符！" runat="server" MaxLength="20"></asp:TextBox></td>
                <td>
                    <cc2:CategoryDropDownList Style="margin: 0" Width="75" ID="ddlCategory" ParentID="0" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" runat="server"></cc2:CategoryDropDownList></td>
                <td>
                    <cc2:CategoryDropDownList Style="margin: 0" Width="75" ID="ddlProject" runat="server"></cc2:CategoryDropDownList></td>
                <td>
                    <asp:TextBox Style="margin: 0" ID="txtSpecifications" check="0" tip="限200个字符！" runat="server" MaxLength="200" Width="95%" TextMode="MultiLine"></asp:TextBox></td>
                <td><a style="margin-left: 2px" href="#" id="uploadpic" class="btn btn-primary btn-mini" title="上传图片" onclick="uploadpic();return false;">上传</a>
                    <asp:FileUpload onchange="uploadonchange(this)" Style="display: none" ID="flieUp" runat="server" /></td>
                <td>
                    <asp:TextBox Style="margin: 0; width: 48px" ID="txtPurchasePrice" check="1" tip="（必填）采购单价" runat="server" MaxLength="20"></asp:TextBox></td>
                <td>
                    <asp:TextBox Style="margin: 0; width: 48px" ID="txtSaleOrice" check="1" tip="（必填）销售单价" runat="server" MaxLength="20"></asp:TextBox></td>
                <td>
                    <asp:TextBox Style="margin: 0; width: 48px" ID="txtSourceCount" check="1" tip="(必填)数量必须大于0" ClientIDMode="Static" runat="server" MaxLength="20"></asp:TextBox></td>
                <td>
                    <asp:TextBox Style="margin: 0; width: 48px" ID="txtUnit" check="1" tip="（必填）产品数量的计量，如：个、套" ClientIDMode="Static" runat="server" MaxLength="10"></asp:TextBox></td>
                <td>
                    <asp:TextBox Style="margin: 0" ID="txtRemark" check="0" tip="限200个字符！" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                <td>
                    <asp:TextBox Style="margin: 0" ID="txtPosition" check="0" tip="限10个字符！" runat="server" MaxLength="10" Width="32"></asp:TextBox></td>
                <td>
                    <asp:DropDownList Style="margin: 0; width: 80px" ID="ddlDisposible" runat="server">
                        <asp:ListItem>一次性</asp:ListItem>
                        <asp:ListItem>循环</asp:ListItem>
                        <asp:ListItem>虚拟</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button Text="保存" ID="btnCreate" ClientIDMode="Static" CssClass="btn btn-success" OnClick="btnSave_Click" runat="server" /></td>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="rptStorehouse" OnItemCommand="rptStorehouse_ItemCommand" OnItemDataBound="rptStorehouse_ItemDataBound" runat="server">
                <ItemTemplate>
                    <tr skey='FD_Storehouse<%#Eval("Keys") %>'>
                        <td><span style="cursor: default" title='<%#Eval("ProductName") %>'><%#ToInLine(Eval("ProductName"),6) %></span></td>
                        <td><span style="cursor: default" title='<%#GetCategoryName(Eval("ProductCategory")) %>'><%#ToInLine(GetCategoryName(Eval("ProductCategory")),5) %></span></td>
                        <td><span style="cursor: default" title='<%#GetCategoryName(Eval("ProjectCategory")) %>'><%#ToInLine(GetCategoryName(Eval("ProjectCategory")),5) %></span></td>
                        <td><span style="cursor: default" title='<%#Eval("Specifications") %>'><%#ToInLine(Eval("Specifications"),8) %></span></td>
                        <td><a class="grouped_elements" href="#" rel="group1">
                            <asp:Image ID="imgStore" ImageUrl='<%#Eval("Data") %>' Width="100" Height="50" runat="server" /></a>
                            <asp:LinkButton ID="lkbtnDownLoad" CssClass="btn btn-primary  btn-mini" CommandArgument='<%#Eval("KindID") %>' CommandName="DownLoad" runat="server"></asp:LinkButton>
                        </td>
                        <td><%#Eval("PurchasePrice") %></td>
                        <td><%#Eval("SalePrice") %></td>
                        <td><%#Eval("Count") %></td>
                        <td><%#Eval("Unit") %></td>
                        <td><span style="cursor: default" title='<%#Eval("Remark") %>'><%#ToInLine(Eval("Remark"),8) %></span></td>
                        <td><%#Eval("Position") %></td>
                        <td></td>
                        <td style="width: 90px">

                            <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("KindID") %>,this);'>修改</a>
                            <asp:LinkButton ID="lkbtnDelete" CommandName="Delete" CssClass="btn btn-danger btn-mini" CommandArgument='<%#Eval("KindID") %>' runat="server" OnClientClick="return confirm('确认删除此产品!')">删除</asp:LinkButton>

                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
    <cc1:AspNetPagerTool ID="StorePager" PageSize="10" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>

    <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />

</asp:Content>
