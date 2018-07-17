<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_SupplierProductCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SupplierProductCreate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_ProductUpdate.aspx?ProductID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            $(".grouped_elements").each(function (indexs, values) {
                if ($.trim($(this).html()) == "") {
                    $(this).remove();
                }
            });
            $(".grouped_elements").each(function (indexs, values) {
                var imgChildren = $(this).children("img");
                $(this).attr("href", imgChildren.attr("src"));
            });
            $("a.grouped_elements").fancybox();
        });
        $(window).load(function () {
            BindString(20, '<%=txtSourceProductName.ClientID%>:<%=txtUnit.ClientID%>:<%=txtSpecifications.ClientID%>');
            BindMoney('<%=txtSaleOrice.ClientID%>:<%=txtPurchasePrice.ClientID%>');
            BindText(50, '<%=txtRemark.ClientID%>:<%=txtExplain.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnCreate.ClientID%>").click(function () {
                if (ValidateForm('input[check],textarea[check]')) {
                    if ($("#<%=ddlCategory.ClientID%>").val() != -1) {
                        return true;
                    }
                    else { alert("请选择产品类别！"); return false; }
                }
                else { return false;}
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
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5><asp:Literal ID="ltlTitle" runat="server" Text="录入供应商产品 - "></asp:Literal></h5>
        </div>
        <div class="widget-content" style="overflow-x: scroll;">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>产品名称</th>
                        <th>产品类别</th>
                        <th>项目</th>
                        <th>产品\服务描述</th>
                        <th>资料</th>
                        <th>采购单价</th>
                        <th>销售单价</th>
                        <th>单位</th>
                        <th>备注</th>
                        <th>说明</th>
                        <th>操作</th>
                    </tr>
                    <asp:PlaceHolder ID="phcreate" runat="server">
                        <tr>
                            <td><asp:TextBox style="margin:0" ID="txtSourceProductName" Width="85" MaxLength="20" check="1" tip="（必填）限20个字符！" runat="server"></asp:TextBox></td>
                            <td><cc2:CategoryDropDownList  style="margin:0" Width="75" ID="ddlCategory" ParentID="0" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" runat="server"></cc2:CategoryDropDownList></td>
                            <td><cc2:CategoryDropDownList style="margin:0" Width="75" ID="ddlProject" runat="server"></cc2:CategoryDropDownList></td>
                            <td><asp:TextBox  style="margin:0;width:95%" ID="txtSpecifications" MaxLength="200" check="0" tip="限200个字符！" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                            <td><a style="margin-left:2px" href="#" id="uploadpic" class="btn btn-primary btn-mini" title="上传图片" onclick="uploadpic();return false;">上传</a>
                            <asp:FileUpload onchange="uploadonchange(this)" style="display:none" ID="flieUp" runat="server" /></td>
                            <td><asp:TextBox style="margin:0;width:48px" ID="txtPurchasePrice" MaxLength="8" check="1" tip="（必填）采购单价" CssClass="{required:true,number:true}" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox style="margin:0;width:48px" ID="txtSaleOrice" MaxLength="8" check="1" tip="（必填）销售单价" CssClass="{number:true}" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox style="margin:0;width:48px" ID="txtUnit" MaxLength="10" check="1" tip="（必填）产品计量如：个、套" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="txtRemark" style="margin:0" MaxLength="200" check="0" tip="限200个字符！" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                            <td><asp:TextBox ID="txtExplain" style="margin:0" MaxLength="200" check="0" tip="限200个字符！" TextMode="MultiLine" runat="server"></asp:TextBox></td>
                            <td><asp:Button ID="btnCreate" ClientIDMode="Static" CssClass="btn btn-success" OnClick="btnSave_Click" runat="server" Text="保存" /></td>
                        </tr>
                    </asp:PlaceHolder>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptStorehouse" OnItemCommand="rptStorehouse_ItemCommand" OnItemDataBound="rptStorehouse_ItemDataBound" runat="server">
                        <ItemTemplate>
                            <tr skey='FD_SaleSourcesProductID<%#Eval("ProductID") %>'>
                                <td><span style="cursor:default" title='<%#Eval("ProductName") %>'><%#ToInLine(Eval("ProductName"),8) %></span></td>
                                <td><span style="cursor:default" title='<%#GetCategoryName(Eval("CategoryID")) %>'><%#ToInLine(GetCategoryName(Eval("CategoryID")),5) %></span></td>
                                <td><span style="cursor:default" title='<%#GetCategoryName(Eval("ProductProject")) %>'><%#ToInLine(GetCategoryName(Eval("ProductProject")),5) %></span></td>
                                <td><span style="cursor:default" title='<%#Eval("Specifications") %>'><%#ToInLine(Eval("Specifications"),12) %></span></td>
                                <td><a class="grouped_elements" href="#" rel="group1"><asp:Image ID="imgStore" ImageUrl='<%#Eval("Data") %>' Width="100" Height="70" runat="server" /></a>
                                    <asp:LinkButton ID="lkbtnDownLoad" CssClass="btn btn-primary  btn-mini" CommandArgument='<%#Eval("ProductID") %>' CommandName="DownLoad" runat="server"></asp:LinkButton>
                                </td>
                                <td><%#GetMoneyDouble(Eval("ProductPrice")) %></td>
                                <td><%#GetMoneyDouble(Eval("SalePrice")) %></td>
                                <td><%#Eval("Unit") %></td>
                                <td><span style="cursor:default" title='<%#Eval("Remark") %>'><%#ToInLine(Eval("Remark"),8) %></span></td>
                                <td><span style="cursor:default" title='<%#Eval("Explain") %>'><%#ToInLine(Eval("Explain"),8) %></span></td>
                                <td><a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("ProductID") %>,this);'>修改</a>
                                    <asp:LinkButton ID="lkbtnDelete" CommandName="Delete" CssClass="btn btn-danger btn-mini" CommandArgument='<%#Eval("ProductID") %>' runat="server">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr><td colspan="11"><cc1:AspNetPagerTool ID="StorePager" AlwaysShow="true" OnPageChanged="StorePager_PageChanged" runat="server"></cc1:AspNetPagerTool></td></tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>
