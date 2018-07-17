<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBoard.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.MessageBoard" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<script type="text/javascript">

    $(document).ready(function () {

        $(".user-info").css({ "display": "none" });
        $("#spMsg").show();
        var urlHref = top.location.href;
        if (urlHref.indexOf("EmployeeID=") > 0) {
            var arrStr = urlHref.split('=');
            $("#hfParameter").val(arrStr[1]);
            $("#myMsg").hide();

        } else {
            $("#myMsg").show();
            $("#createMsg").hide();


        }
        //$(".user-info").eq(0).css({ "display": "block" });
        //$("label[id$='Msg']").click(function () {
        //    alert($(this).html());
        //    $("#operContent span").eq($("#operContent label").index($(this)));
        //    $("#operContent span").not($(this)).hidden();
        //});
        $("#myMsg").click(function () {
            $(".user-info").css({ "display": "none" });
            $("#spMsg").show();

        });
        $("#createMsg").click(function () {
            $(".user-info").css({ "display": "none" });
            $("#createSpMsg").show();
        });
        $("#replyMsg").click(function () {
            $(".user-info").css({ "display": "none" });
            $("#replySpMsg").show();
        });
        $("#delMsg").click(function () {
            $(".user-info").css({ "display": "none" });
            $("#delSpMsg").show();
        });
        //
    });

</script>



<div class="row-fluid">
    <asp:HiddenField ID="hfParameter" Value="0" ClientIDMode="Static" runat="server" />
    <div style="width:805px;">
        <div class="widget-box">
            <div class="widget-title">
                <span class="icon"><i class="icon-file"></i></span>
                <h5>我的留言</h5>
            </div>
            <div class="widget-content nopadding">
                <ul class="recent-posts">
                    <li>
                        <div class="user-thumb">
                            <img width="40" height="40" alt="User" src="img/demo/av1.jpg" />
                        </div>
                        <div id="mainContent" class="article-post">
                            <div id="operContent" class="fr">
                                <asp:PlaceHolder ID="phmyMsg" runat="server">
                                   <%-- <label id="myMsg" class="btn btn-primary btn-mini">列表</label>--%>
                                </asp:PlaceHolder>
                                <asp:PlaceHolder ID="phcreateMsg" runat="server">
                                    <label id="createMsg" class="btn btn-primary btn-mini">留言给Ta</label>
                                </asp:PlaceHolder>
 
                                <asp:PlaceHolder ID="phdelMsg" Visible="false" runat="server">
                                    <label id="delMsg" class="btn btn-danger btn-mini">删除</label>
                                </asp:PlaceHolder>

                            </div>
                            <span class="user-info" id="spMsg">
                               
                                <asp:Repeater ID="rptMessList" OnItemCommand="rptMessList_ItemCommand" runat="server">

                                    <ItemTemplate>
                                        <%#GetEmployeeName(Eval("CreateEmpLoyee")) %> &nbsp;&nbsp;<%# GetDateStr(Eval("CreateDate")) %>说:
                                        <div>
                                            <%#Eval("MessAgeContent") %>
                                        </div>
                                        <asp:PlaceHolder ID="phReplyTo" Visible="false" runat="server">
                                            <asp:TextBox ID="txtReplyMessage" runat="server"></asp:TextBox>
                                            <asp:HiddenField ID="hfCreateEmployee" Value='<%#Eval("CreateEmpLoyee")%>' runat="server" />
                                            <asp:LinkButton ID="btnSub" CommandName="btnSub" CssClass="btn" runat="server" Text="回复" />
                                         </asp:PlaceHolder>
                                        <asp:LinkButton ID="lkbtnReply" CommandName="Reply" CssClass="btn btn-success btn-mini" CommandArgument='<%#Eval("CreateEmpLoyee") %>' runat="server">回复</asp:LinkButton>
                                        <hr />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <cc1:AspNetPagerTool ID="MessagePager" AlwaysShow="true" PageSize="3" OnPageChanged="MessagePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
                            </span>

                            <span class="user-info" id="createSpMsg">留言内容:<asp:TextBox ID="txtCreateContent" MaxLength="200" runat="server"></asp:TextBox>
                                <asp:Button CssClass="btn btn-warning btn-mini" ID="btnCreae" runat="server" Text="给他留言" OnClick="btnCreae_Click" />
                            </span>

                            <span class="user-info" id="delSpMsg">删除
                            </span>
                        </div>
                    </li>
                    <li></li>
                </ul>
            </div>
        </div>
    </div>
    <div class="span6"></div>
</div>
