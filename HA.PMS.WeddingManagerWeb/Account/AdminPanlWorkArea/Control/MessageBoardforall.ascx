<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBoardforall.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.MessageBoardforall" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<script type="text/javascript">

    $(document).ready(
        function () {
            $(".NeedHide").hide();
            $("#txtCreate").hide();
        }
        );

    function RreturnMessage(Control) {
        $("#btnSaveReturn").show();
        $("#txtReturn").show();

        $("#txtCreate").hide();
        $("#btnSaveMessage").hide();

        $("#hideReturnKey").attr("value", $(Control).attr("messageboardid"));

    }


    function CreateMessage() {
        $("#txtCreate").show();
        $("#btnSaveMessage").show();

        $("#btnSaveReturn").hide();
        $("#txtReturn").hide();
    }
    $(window).load(function () {
        BindCtrlRegex();
        BindCtrlEvent('input[check],textarea[check]');
    });
    function BindCtrlRegex() {
        BindText(200, '<%=txtReturn.ClientID%>:<%=txtCreate.ClientID%>');
    }
    function CheckReturn(ctrl)
    { if ($('#txtReturn').val()) { return ValidateForm('#txtReturn'); } else { return false; } }
    function CheckCreate(ctrl)
    { if ($('#txtCreate').val()) { return ValidateForm('#txtCreate'); } else { return false; } }
</script>
<div class="row-fluid">
    <div class="span7">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-file"></i></span>
                <h5>我的留言</h5>
                <asp:HiddenField ID="hideReturnKey" runat="server" ClientIDMode="Static" />
            </div>
            <div class="widget-content nopadding">
                <ul class="recent-posts">
                    <asp:Repeater ID="repMessage" runat="server" OnItemCommand="repMessage_ItemCommand">

                        <ItemTemplate>
                            <li>
                                <div class="user-thumb">
                                    <img width="40" height="40" alt="User" src="/Images/av1.jpg">
                                </div>
                                <div class="article-post">
                                    <div class="fr">
                                        <a href="#" class="btn btn-success btn-mini" onclick="RreturnMessage(this);" MessageBoardID="<%#Eval("MessageBoardID") %>">回复</a>
                                        <asp:LinkButton ID="lnkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("MessageBoardID") %>' runat="server" CssClass="btn btn-danger btn-mini">删除</asp:LinkButton>
                                        
                                    </div>
                                    <span class="user-info"><%#Eval("CreateEmpLoyeeName") %> / <%#Eval("CreateDate") %> </span>
                                    <p><a href="#"><%#Eval("MessAgeContent") %></a> </p>
                                    <p><%#GetReturnMessage(Eval("MessageBoardID")) %></a> </p>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <li>
                        <a href="#" class="btn btn-warning btn-mini" onclick="CreateMessage();" style="display:none;">给他留言</a>
                        <asp:Button CssClass="NeedHide" OnClientClick="return CheckCreate(this);" ID="btnSaveMessage" runat="server" Text="保存留言" ClientIDMode="Static" OnClick="btnSaveMessage_Click"  />
                        <asp:Button  CssClass="NeedHide" OnClientClick="return CheckReturn(this);" ID="btnSaveReturn" runat="server" Text="保存回复" ClientIDMode="Static" OnClick="btnSaveReturn_Click" />
                        <asp:TextBox  CssClass="NeedHide" check="0" ID="txtReturn" runat="server" ClientIDMode="Static" Rows="3" TextMode="MultiLine" Width="150px"></asp:TextBox>
                        <asp:TextBox ID="txtCreate" check="0" runat="server" ClientIDMode="Static"></asp:TextBox>
                </ul>
            </div>
        </div>
    </div>
    <div class="span6">
        <cc1:AspNetPagerTool ID="MessagePager" PageSize="3" AlwaysShow="true" OnPageChanged="MessagePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
    </div>
</div>
