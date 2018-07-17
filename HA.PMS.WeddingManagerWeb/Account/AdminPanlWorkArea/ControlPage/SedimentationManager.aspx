<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="SedimentationManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SedimentationManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "SedimentationUpdate.aspx?FileDetailsId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 500, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {

            showPopuWindows($("#createSediment").attr("href"), 800, 800, "a#createSediment");

            $(".grouped_elements").each(function (indexs, values) {
                if ($.trim($(this).html()) == "") {
                    $(this).remove();
                }
            });
            //加载对应图片浏览效果 begin
            $(".grouped_elements").each(function (indexs, values) {
                var imgChildren = $(this).children("img");
                $(this).attr("href", imgChildren.attr("src"));

            });

            $("a.grouped_elements").fancybox();
        });

        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtCategoryName.ClientID%>');
        }
        function CheckSuccess() {
            <%if (ViewState["parentId"] == null) { %>
            alert("请你选择要操作的节点"); return false;
            <%}%>
            return ValidateForm('input[check],textarea[check]');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-content">
        <div class="row-fluid">
            <div class="span4">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-ok"></i></span>
                        <h6><%--提示：选择一个节点就会对当前节点添加对应的子类，不选择默认是父类--%>
                                沉淀库树形列表
                        </h6>
                    </div>
                    <div class="widget-content nopadding">
                        <br />
                        <asp:TreeView ID="TreeView1" ShowLines="true" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged" runat="server" ImageSet="XPFileExplorer" NodeIndent="15">
                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                            <ParentNodeStyle Font-Bold="False" />
                            <SelectedNodeStyle BackColor="#73dbdb" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
                        </asp:TreeView>
                    </div>

                </div>

            </div>
            <div class="span7">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-th"></i></span>
                        <h5>分类操作</h5>

                    </div>
                    <div class="widget-content">
                        <div class="todo" >
                            <table>
                                <tr>
                                    <td>分类名</td>
                                    <td>
                                        <asp:TextBox ID="txtCategoryName" check="1" tip="添加和修改操作时为必填项" runat="server" MaxLength="20"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="btnCategory" OnClientClick="return CheckSuccess();" OnClick="btnCategory_Click" runat="server" CssClass="btn btn-success" Text="添加子项" />
                                        &nbsp;
                                            <asp:Button ID="btnCategoryUpdate" OnClientClick="return CheckSuccess();" OnClick="btnCategoryUpdate_Click" runat="server" CssClass="btn btn-primary" Text="修改名称" />&nbsp;
                                         
                                        <asp:Button ID="lkbtnDelete" runat="server" OnClick="lkbtnDelete_Click" CssClass="btn btn-danger" Text="删除" />
                                    </td>

                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="span7">
                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-th"></i></span>
                        <h5>
                            <a id="createSediment" class="btn btn-primary btn-mini" href='SedimentationCreate.aspx?FileCategoryId=<%=ViewState["parentId"]  %> '>添加文件
                            </a>
                        </h5>

                    </div>
                    <div class="widget-box" style="font-weight: bold;">
                        <asp:Literal ID="ltlCategory" runat="server"></asp:Literal>
                    </div>
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th>文件名</th>
                                <th>文件</th>

                                <th>操作</th>
                            </tr>

                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptFileDetails" OnItemDataBound="rptFileDetails_ItemDataBound" OnItemCommand="rptFileDetails_ItemCommand" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("FileDetailsName") %></td>
                                        <td>
                                            <asp:PlaceHolder ID="phImg" runat="server">
                                                <a class="grouped_elements" href="#" rel="group1">
                                                    <img width="100"  style="width:100px;  height:70px;" height="70" src='<%#GetServerPath(Eval("FileDetailsPath")) %>' />
                                                </a>
                                            </asp:PlaceHolder>
                                            <asp:PlaceHolder ID="phPPT" runat="server">
                                                <asp:LinkButton ID="lkbtnDownLoad" CssClass="btn btn-primary  btn-mini" CommandArgument='<%#Eval("FileDetailsId") %>' CommandName="DownLoad" runat="server">下载</asp:LinkButton>

                                            </asp:PlaceHolder>
                                            <asp:PlaceHolder ID="phText" runat="server">
                                                <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("FileDetailsId") %>,this);'>预览</a>
                                                <asp:LinkButton ID="lktbtnText" CssClass="btn btn-primary  btn-mini" CommandArgument='<%#Eval("FileDetailsId") %>' CommandName="DownLoad" runat="server">下载</asp:LinkButton>
                                            </asp:PlaceHolder>
                                            <asp:PlaceHolder ID="phMovie" runat="server">
                                                <asp:LinkButton ID="lkbtnDownLoad2" CssClass="btn btn-primary  btn-mini" CommandArgument='<%#Eval("FileDetailsId") %>' CommandName="DownLoad" runat="server">下载</asp:LinkButton>

                                            </asp:PlaceHolder>

                                        </td>

                                        <td>
                                            <asp:LinkButton ID="lkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("FileDetailsId") %>' runat="server" CssClass="btn btn-danger btn-mini">删除</asp:LinkButton>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="3">
                                    <cc1:AspNetPagerTool ID="DetailsPager" AlwaysShow="true" OnPageChanged="DetailsPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
                                </td>
                            </tr>
                        </tfoot>
                    </table>


                </div>
            </div>

        </div>
    </div>


</asp:Content>
