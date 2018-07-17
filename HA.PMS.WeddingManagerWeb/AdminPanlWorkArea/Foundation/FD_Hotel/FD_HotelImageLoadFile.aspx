<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_HotelImageLoadFile.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel.FD_HotelImageLoadFile" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/ServerFileUpLoad.ascx" TagPrefix="HA" TagName="ServerFileUpLoad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script type="text/javascript">

         $(document).ready(function () {
             $("html,body").css({ "width": "800px", "height": "550px", "background-color": "transparent" });
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <HA:ServerFileUpLoad runat="server"  ID="ServerFileUpLoad" />
     <table style="width:100%;">
        <tr>
            <td>
                <asp:Button ID="btnSaveImage" runat="server"  CssClass="btn btn-success" Text="保存" OnClick="btnSaveImage_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
