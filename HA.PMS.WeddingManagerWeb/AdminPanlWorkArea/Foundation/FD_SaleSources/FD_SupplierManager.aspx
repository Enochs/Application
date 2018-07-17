<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_SupplierManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SupplierManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">

        function ShowWindowsUpdate(KeyID, Control) {
            var Url = "FD_SupplierUpdate.aspx?SupplierID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 900, 1000, "a#" + $(Control).attr("id"));
        }


        $(document).ready(function () {

            showPopuWindows($("#createSupplier").attr("href"), 900, 1000, "a#createSupplier");
            //$("#</%=txtStarDate.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            //$("#</%=txtEndDate.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });

        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="FD_SupplierCreate.aspx" class="btn btn-primary  btn-mini" id="createSupplier">创建供应商</a>
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>供应商管理页面</h5>
            <span class="label label-info">管理页面</span>
        </div>
        <div class="widget-content">
               <!--查询操作 start -->
            <div class="row-fluid">
                <div class="span9">
                    <div class="widget-box">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-ok"></i></span>
                            <h5>查询操作</h5>
                        </div>
                        <div class="widget-content nopadding">
                         产品类别:<asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList>
                         供应商名称:<asp:DropDownList ID="ddlSupplierName" runat="server"></asp:DropDownList>
                            <br />
                         日期:<asp:TextBox ID="txtStarDate" onclick="WdatePicker()" runat="server" MaxLength="20"></asp:TextBox>
                         至<asp:TextBox ID="txtEndDate" onclick="WdatePicker()" runat="server" MaxLength="20"></asp:TextBox>
                            <asp:Button ID="btnQuery"  CssClass="btn btn-success"  runat="server" Text="查询" OnClick="btnQuery_Click" />
                        </div>
                        
                    </div>
                </div>
            </div>
            <!--查询操作end-->
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>供应商名</th>
                        <th>地址</th>
                        <th>开始合作时间</th>
                        <th>联系人</th>
                        <th>联系电话</th>
                        <th>QQ</th>
                        <th>Email</th>
                        <th>座机</th>
                        <th>传真</th>
                        <th>类别</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptSupplier" runat="server" OnItemCommand="rptSupplier_ItemCommand">
                        <ItemTemplate>
                            <tr skey='<%#Eval("SupplierID") %>'>
                                <td><%#Eval("Name") %></td>
                                <td><%#Eval("Address") %></td>
                                <td><%#Eval("StarDate") %></td>
                                <td><%#Eval("Linkman") %></td>
                                <td><%#Eval("TelPhone") %></td>
                                <td><%#Eval("QQ") %></td>
                                <td><%#Eval("Email") %></td>
                                <td><%#Eval("CellPhone") %></td>
                                <td><%#Eval("Fax") %></td>
                                <td><%#Eval("CategoryName") %></td>
                                <td>
                                    <a href="#"  class="btn btn-primary  btn-mini"  onclick='ShowWindowsUpdate(<%#Eval("SupplierID") %>,this)'>修改</a>
                                    <asp:LinkButton  CssClass="btn btn-danger btn-mini" ID="lbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("SupplierID") %>' runat="server">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="SupplierPager" AlwaysShow="true" OnPageChanged="SupplierPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
            
        </div>
    </div>
</asp:Content>
