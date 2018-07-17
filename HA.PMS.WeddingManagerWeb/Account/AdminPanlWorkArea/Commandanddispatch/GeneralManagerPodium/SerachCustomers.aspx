<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="SerachCustomers.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.GeneralManagerPodium.SerachCustomers" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "width": "270px", "height": "170px" });
            $(".queryTable td").css({ "border": "0", "vertical-align": "top" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="queryTable">
        <tr>
            <td>部门:<cc1:DepartmentDropdownList Width="130" ID="DepartmentDropdownList1" AutoPostBack="true" OnSelectedIndexChanged="DepartmentDropdownList1_SelectedIndexChanged" runat="server"></cc1:DepartmentDropdownList></td>
        </tr>
        <tr>
            <td>姓名:<asp:DropDownList ID="ddlEmployee" Width="130" runat="server"></asp:DropDownList></td>

        </tr>
        <tr>
            <td>
                <asp:Button ID="btnFind" CssClass="btn" runat="server" Text="查找" OnClick="btnFind_Click" /></td>

        </tr>
    </table>



</asp:Content>
