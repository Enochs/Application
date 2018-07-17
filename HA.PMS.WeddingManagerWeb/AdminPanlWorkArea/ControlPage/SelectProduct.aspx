<%@ Page Language="C#" AutoEventWireup="true" Debug="true" CodeBehind="SelectProduct.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.ControlPage.SelectProduct" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master"%>
 
<%@ Register src="../Control/SelectProduct.ascx" tagname="SelectProduct" tagprefix="uc1" %>
 
<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".Productdiv").hide();
            $("html,body").css({ "width": "900px", "height": "600px", "background-color": "transparent" });
            $("#TablerepProductByCatogryforWarehouseList").show();
            $("#TablerepProductByCatogryforWarehouseList").addClass("active");
        });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
 

    <uc1:SelectProduct ID="SelectProduct1" runat="server" />
 

</asp:Content>