<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderSelector.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.OrderSelector" %>
<span id="msg" onclick="orderstylechange(this)">降序↓</span>
<asp:HiddenField ID="hiddenStyle" runat="server" Value="1" />
<script>
    function orderstylechange() {
        if ($("#<%=hiddenStyle.ClientID%>").val() == "1") {
            $("#msg").html("升序↑");
            $("#<%=hiddenStyle.ClientID%>").val("0");
        }
        else {
            $("#msg").html("降序↓");
            $("#<%=hiddenStyle.ClientID%>").val("1");
        }
    }
</script>