<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBoardforEmpLoyee.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.MessageBoardforEmpLoyee" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<script type="text/javascript">

    $(document).ready(
        function () {
            $(".NeedHide").hide();
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
                <h5>给<asp:Label ID="lblEmployeeName" runat="server" Text=""></asp:Label>的留言</h5>
                <asp:HiddenField ID="hideReturnKey" runat="server" ClientIDMode="Static" />
            </div>
            <div class="widget-content nopadding">
   
                    <asp:Repeater ID="repMessage" runat="server">

                        <ItemTemplate>
                            <li>
                                <div class="user-thumb">
                                    <img width="40" height="40" alt="User" src="img/demo/av1.jpg">
                                </div>
                                <div class="article-post">
                                    <div class="fr">
                                        <a href="#" class="btn btn-success btn-mini" onclick="RreturnMessage(this);" MessageBoardID="<%#Eval("MessageBoardID") %>" <%#Eval("CreateEmpLoyeeID").ToString()!=CreateEmpLoyeeID.ToString()?"":"style='display:none;'" %>>回复</a>
                                    </div>
                                    <span class="user-info"><%#Eval("CreateEmpLoyeeName") %> / <%#Eval("CreateDate") %> </span>
                                    <p><a href="#"><%#Eval("MessAgeContent") %></a> </p>
                                    <p><%#GetReturnMessage(Eval("MessageBoardID")) %></a> </p>
                                </div>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                  
                        <a href="#" class="btn btn-warning btn-mini" onclick="CreateMessage();" style="display:none;">给留言</a>
                        <br />
                        
                        <asp:Button  CssClass="NeedHide" ID="btnSaveReturn" OnClientClick="return CheckReturn(this);" runat="server" Text="保存回复" ClientIDMode="Static" OnClick="btnSaveReturn_Click" />
                        <asp:TextBox  CssClass="NeedHide" check="0" tip="限200个字符！" MaxLength="200" ID="txtReturn" runat="server" ClientIDMode="Static" ></asp:TextBox>
                        <asp:TextBox  CssClass="" ID="txtCreate" check="0" tip="限200个字符！" MaxLength="200" runat="server" ClientIDMode="Static" Rows="3" TextMode="MultiLine" Width="500px"></asp:TextBox>
                         <br />
                        <asp:Button  CssClass="btn btn-success" ID="btnSaveMessage" OnClientClick="return CheckCreate(this);" runat="server" Text="保存留言"  ClientIDMode="Static" OnClick="btnSaveMessage_Click"  />
 
            </div>
        </div>
    </div>
    <div class="span6"></div>
</div>
