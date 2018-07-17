<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sys_ChannelManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Jurisdiction.Sys_ChannelManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" StylesheetTheme="Default" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
    <%--<script src="/Scripts/liselection.js"></script>--%>
 
    <script type="text/javascript">

        //修改数据
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "Sys_ChannelUpdate.aspx?ChannelID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 600, "a#" + $(Control).attr("id"));
        }

        //添加数据
        function ShowCreateWindows(Control, KeyID) {
            var Url = "Sys_ChannelCreate.aspx?Parent=" + KeyID
            $(Control).attr("id", "createShow" + KeyID);
            showPopuWindows(Url, 500, 500, "a#" + $(Control).attr("id"));
        }


        //频道控件管理
        function ShowCreateControl(Control, KeyID) {

            var Url = "Sys_ControlsManager.aspx?ChannelID=" + KeyID
            $(Control).attr("id", "createShow" + KeyID);
            showPopuWindows(Url, 500, 500, "a#" + $(Control).attr("id"));

        }



    </script>

    <div class="widget-box">
        <a id="ff" href="#" class="btn btn-primary btn-mini" onclick="ShowCreateWindows(this,'')">添加顶级目录</a>
        <asp:LinkButton ID="btnCreateBase" CssClass="btn btn-primary btn-mini" runat="server" OnClick="btnCreateBase_Click">生成频道备份</asp:LinkButton><br />
        
        <div class="widget-content" style="overflow:auto;height:900px;">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>频道名称</th>
                        <th>频道地址</th>
                        <th>序号</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <asp:Repeater ID="RepChannelList" runat="server" OnItemCommand="RepChannelList_ItemCommand" OnItemCreated="RepChannelList_ItemCreated" OnItemDataBound="RepChannelList_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("ChannelName") %></td>
                            <td>
                                <asp:HiddenField runat="server" ID="hidekey" Value='<%#Eval("ChannelID") %>' />
                                <ul class="ul-select">
                                    <asp:Repeater ID="repSecond" runat="server" OnItemCommand="repSecond_ItemCommand">
                                        <ItemTemplate>
                                            <li  skey='<%#Eval("ChannelID") %>'><%#Eval("ChannelName") %>

                                                <a href="#" onclick="ShowUpdateWindows(<%#Eval("ChannelID") %>,this);" class="btn btn-primary btn-mini">修改排序</a>

                                                <span style="text-align: right; display: none;">
                                                    <asp:LinkButton ID="lnkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("ChannelID") %>' runat="server" CssClass="btn btn-danger btn-mini">删除频道</asp:LinkButton>

                                                </span>序号：<%#Eval("OrderCode") %></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </td>
                            <td>
                                <%#Eval("OrderCode") %>
                            </td>
                            <td>
                                <a href="#" onclick="ShowUpdateWindows(<%#Eval("ChannelID") %>,this);" class="btn btn-primary btn-mini">修改排序</a>
                       
                                    <a href="#" onclick="ShowCreateWindows(this,<%#Eval("ChannelID") %>);" class="btn btn-primary btn-mini" >添加子频道</a>
                                         <label style="display: none;">
                                    <a href="#" onclick="ShowCreateControl(this,<%#Eval("ChannelID") %>);" class="btn btn-danger btn-mini" style="display:none;">频道控件管理</a>
                                   
                                </label>
                                 <asp:LinkButton Visible="false" ID="lnkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("ChannelID") %>' runat="server" CssClass="btn btn-danger btn-mini">删除频道</asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </table>
        </div>
    </div>
</asp:Content>

