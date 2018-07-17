<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="Sys_ControlsManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_ControlsManager" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
    <script type="text/javascript">
        //修改数据
        function ShowCreate(KeyID, Control) {
            var Url = "Sys_ControlsCreate.aspx?ChannelID=" + KeyID;
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        } 

        $(document).ready(function () {
            $("html,body").css({ "width": "500px", "height": "400px", "background-color": "transparent" });
        });

 
    </script>
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5><a id="ff" href="#" onclick="ShowCreate(<%=Request["ChannelID"] %>,this);">频道控件管理</a></h5>
            <a id="createControls" href="Sys_ControlsCreate.aspx?ChannelID=<%=Request["ChannelID"] %>" onclick="ShowCreate(<%=Request["ChannelID"] %>,this);" class="btn btn-primary btn-mini">创建控件</a>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>功能名称</th>
                        <th>控件ID</th>
                        <th>控件key</th>
                        <th>控件位置</th>
                        <th>创建时间</th>
                        <th>操作</th>

                    </tr>
                </thead>
                <asp:Repeater ID="rptControls" runat="server" OnItemCommand="rptControls_ItemCommand">

                    <ItemTemplate>
                        <tr skey='<%#Eval("ControlID") %>'>
                            <td><%#Eval("ControlName") %></td>
                            <td><%#Eval("ControlID") %></td>
                            <td><%#Eval("ConrolKey") %></td>
                            <td><%#Eval("BelongByControl") %></td>
                            <td><%#Eval("CreateTime") %></td>
                            <td>
                                <a href="#" onclick="ShowCreate(<%#Eval("ControlID") %>,this)">修改</a>
                                &nbsp;
                        <asp:LinkButton ID="lkbtnControl" runat="server" CommandName="Delete" CommandArgument='<%#Eval("ControlID") %>' class="btn btn-danger btn-mini">删除</asp:LinkButton>

                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>


            </table>
        </div>
    </div>
</asp:Content>
