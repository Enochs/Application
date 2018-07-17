<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoticeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.NoticeManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
 

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        //频道控件管理
        function ShowCreateWindows(Control) {
         
            var Url = "NoticeCreate.aspx";
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));

        }
        //修改
        function ShowUpdateWindows(key, Control) {
        
            var Url = "NoticeUpdate.aspx?NoticeKey=" + key;
            $(Control).attr("id", "updateShow" + key);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }
        //详细
        function ShowDetailsWindows(key,Control) {
            var Url = "NoticeContent.aspx?NoticeKey=" + key;
            $(Control).attr("id", "detailsShow" + key);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div class="widget-box">
         <a id="ff"  class="btn btn-primary  btn-mini"  href="#" onclick="ShowCreateWindows(this)">添加消息</a>
        <div class="widget-content">

            <table class="table table-bordered table-striped" style="width:700px;">

                <thead>
                    <tr>
                        <th>标题</th>
                        <th>发布时间</th>
                        <th>操作</th>
                    </tr>
                </thead>

                <asp:Repeater ID="repNoticelist" runat="server" OnItemCommand="repNoticelist_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Title") %> </td>
                            <td>
                                
                                <%#GetDateStr(Eval("CreateDate")) %></td>
                            <td>
                                 <a href="#" onclick='ShowUpdateWindows(<%#Eval("NoticeKey") %>,this)'  class="btn btn-primary  btn-mini" >修改</a>
                                 <a href="#" onclick='ShowDetailsWindows(<%#Eval("NoticeKey") %>,this)'  class="btn btn-danger  btn-mini" >详细</a>
                                 <asp:LinkButton runat="server" class="btn btn-primary btn-mini" Text="删除" CommandArgument='<%#Eval("NoticeKey") %>' CommandName="Del" OnClientClick="return confirm('你确定要删除吗?')" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <cc1:AspNetPagerTool ID="NoticePager" AlwaysShow="true" OnPageChanged="NoticePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>

</asp:Content>

