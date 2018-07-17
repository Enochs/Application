<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CompanySystemBrowse.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.CompanySystemBrowse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function() {
            $("html,body").css({ "height": "450px" });
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
                            <h6>制度树形目录</h6>
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
                            <h5>相关信息</h5>
                           
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
