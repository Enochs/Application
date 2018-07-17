<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceFinishNextPage.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceSplit.QuotedPriceFinishNextPage" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">

    <script type="text/javascript">

        function ShowEmployeePopu1(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmployeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function ShowEmployeePopu2(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideMissionManager&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        $(document).ready(function () {
            $("#btnSaveChange").click(function () {
                if ($("#hideEmployeeID").val() == "") {
                    alert("请选择总调度人" + $("#hideEmployeeID").val());
                    return false;
                }
            });
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <a id="SelectEmpLoyeeBythis" href="#" style="display: none;"></a>
    <table style="width: 100%;">
        <tr>
            <td>婚礼管家</td>
            <td id="<%=Guid.NewGuid().ToString() %>">
                <input style="margin: 0" runat="server" id="txtMissionManager" class="txtEmpLoyeeName" onclick="ShowEmployeePopu2(this);" type="text" />
                <a href="#" class="btn btn-mini btn-primary" onclick="ShowEmployeePopu2(this);" class="SetState">选择负责人</a>
                <asp:HiddenField ID="hideMissionManager" ClientIDMode="Static" Value='' runat="server" />
            </td>
        </tr>
        <tr>
            <td>选择总调度</td>
            <td id="<%=Guid.NewGuid().ToString() %>">
                <%--<input style="margin: 0" runat="server" id="Text1" class="txtEmpLoyeeName" onclick="ShowEmployeePopu1(this);" type="text" />--%>
                <asp:TextBox runat="server" ID="Text1" CssClass="txtEmpLoyeeName" onclick="ShowEmployeePopu1(this);" />
                <a runat="server" id="btnSelect" href="#" class="btn btn-mini btn-primary" onclick="ShowEmployeePopu1(this);">选择负责人</a>
                <asp:HiddenField ID="hideEmployeeID" ClientIDMode="Static" Value='' runat="server" />
            </td>
        </tr>
        <tr>
            <td>确认保存</td>
            <td>
                <asp:Button ID="btnSaveChange" runat="server" Text="保存" ClientIDMode="Static" OnClick="btnSaveChange_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

