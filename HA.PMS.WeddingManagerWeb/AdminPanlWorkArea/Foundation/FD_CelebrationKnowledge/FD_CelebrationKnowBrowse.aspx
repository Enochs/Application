<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationKnowBrowse.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationKnowledge.FD_CelebrationKnowBrowse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script type="text/javascript">

            $(document).ready(function () {
                $("html,body").css({ "width": "1000px", "height": "500px", "background-color": "transparent" });
            });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
         
        <div class="widget-content">
            <div class="row-fluid">
                <div class="span4">
                    <div class="widget-box">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-ok"></i></span>
                            <h6>知识库目录</h6>
                        </div>
                        <div class="widget-content nopadding">
                            <asp:TreeView ID="TreeView1" runat="server"></asp:TreeView>
                        </div>

                    </div>

                </div>
                <div class="span7">
                    <div class="widget-box">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-th"></i></span>
                            <h5>相关文章</h5>
                           <%-- <span class="label label-info">知识库浏览</span>--%>
                        </div>
                        <div class="widget-content">
                            <div class="todo">
                                <asp:Literal ID="ltlContent" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
