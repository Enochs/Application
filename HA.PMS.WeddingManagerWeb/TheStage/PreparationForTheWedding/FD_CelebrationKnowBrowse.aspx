<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationKnowBrowse.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.PreparationForTheWedding.FD_CelebrationKnowBrowse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <script type="text/javascript">

           
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="widget-box">
         
        <div class="widget-content">
            <div class="row-fluid">
                <div class="span4">
                    <div class="widget-box">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-ok"></i></span>
                            <h6>婚礼筹备</h6>
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
