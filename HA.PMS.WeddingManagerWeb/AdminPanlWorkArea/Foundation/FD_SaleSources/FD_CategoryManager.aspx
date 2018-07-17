<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_CategoryManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_CategoryManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>

    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_CategoryUpdate.aspx?CategoryId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));
        }
        function ShowParentUpdateWindows(KeyID, Control) {
            var Url = "FD_CategoryParentUpdate.aspx?CategoryId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));
        }

        function ShowCreateChildWindows(KeyID, Control) {
            var Url = "FD_CategoryCreateChild.aspx?CategoryId=" + KeyID;
            $(Control).attr("id", "CreateChildShow" + KeyID);
            showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));
        }

        function ShowFirstCreateChildWindows() {
            var Url = "FD_CategoryCreate.aspx";

            showPopuWindows(Url, 500, 1000, "#createCategory");
        }
        $(document).ready(function () {

            showPopuWindows($("#createCategory").attr("href"), 500, 1000, "a#createCategory");

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="#" class="btn btn-primary  btn-mini" id="createCategory" onclick="ShowFirstCreateChildWindows();">创建顶级分类</a>
    <div class="widget-box">

        <div class="widget-content ">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>产品类别</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCategory" runat="server" OnItemDataBound="rptCategory_ItemDataBound">

                        <ItemTemplate>

                            <tr skey='<%#Eval("CategoryID") %>'>
                                <td><%#Eval("CategoryName") %></td>
                                <td>

                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowParentUpdateWindows(<%#Eval("CategoryID") %>,this);'>修改</a>

                                    <asp:LinkButton ID="lkbtnDelete" CssClass="btn btn-danger btn-mini" Style="display: none" CommandName="Delete" CommandArgument='<%#Eval("CategoryID") %>' runat="server">删除</asp:LinkButton>

                                    <asp:Literal ID="ltlCreateChild" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <asp:Repeater ID="rptChildCategory" OnItemDataBound="rptCategory_ItemDataBound" OnItemCommand="rptChildCategory_ItemCommand" runat="server">
                                <ItemTemplate>

                                    <tr skey='<%#Eval("CategoryID") %>'>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("CategoryName") %></td>
                                        <td>

                                            <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("CategoryID") %>,this);'>修改</a>


                                            <asp:LinkButton ID="lbtnDelete" CssClass="btn btn-primary btn-mini" CommandName="Delete" CommandArgument='<%#Eval("CategoryID") %>' runat="server">隐藏</asp:LinkButton>
                                            <asp:Label runat="server" ID="lblStateShow" Text="" />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
