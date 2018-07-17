<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationKnowledgeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationKnowledge.FD_CelebrationKnowledgeManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">

        function ShowWindows(KeyID, Control) {
            var Url = "FD_CelebrationKnowledgeUpdate.aspx?KnowledgeID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 900, 800, "a#" + $(Control).attr("id"));
        }


        $(document).ready(function () {

            showPopuWindows($("#createKnow").attr("href"), 900, 800, "a#createKnow");

            showPopuWindows($("#BrowseKnow").attr("href"), 900, 800, "a#BrowseKnow");

        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <a class="btn btn-primary " href="FD_CelebrationKnowBrowse.aspx" id="BrowseKnow">浏览知识文库</a>
    <asp:PlaceHolder ID="phContent" runat="server">
        <a class="btn btn-primary " href="FD_CelebrationKnowledgeCreate.aspx" id="createKnow">新建栏目</a>

        <div class="widget-box">
           
            <div class="widget-content">
                <table class="table table-bordered table-striped table-select" style="width:600px;">
                    <thead>
                        <tr>
                            <th>栏目名称</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptKnow" runat="server" OnItemCommand="rptKnow_ItemCommand">

                            <ItemTemplate>

                                <tr skey='<%#Eval("KnowledgeID") %>'>
                                    <td><%#Eval("KnowledgeTitle") %></td>
                                    <td>

                                        <a href="#" class="btn btn-primary " onclick="ShowWindows(<%#Eval("KnowledgeID") %>,this)">修改</a>
                                        <asp:LinkButton CssClass="btn btn-danger " CommandName="Delete" CommandArgument='<%#Eval("KnowledgeID") %>' ID="lkbtnDelete" runat="server">删除</asp:LinkButton>
                                    </td>
                                </tr>

                            </ItemTemplate>
                        </asp:Repeater>

                    </tbody>
                </table>
                <cc1:AspNetPagerTool ID="KnowPager"  AlwaysShow="true" OnPageChanged="KnowPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
