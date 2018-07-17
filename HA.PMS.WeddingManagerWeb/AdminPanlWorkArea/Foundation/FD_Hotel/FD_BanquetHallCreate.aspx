<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_BanquetHallCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel.FD_BanquetHallCreate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "512px", "height": "500px", "background-color": "transparent" });
        });
        $(window).load(function () {
            BindString(20, '<%=txtHallName.ClientID%>:<%=txtFloorName.ClientID%>');
            BindMoney('<%=txtFloorHeight.ClientID%>:<%=txtEmptyHeight.ClientID%>:<%=txtArea.ClientID%>');
            BindUInt('<%=txtDeskCount.ClientID%>:<%=txtMeal.ClientID%>');
            BindText(50, '<%=txtBanquetExplain.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                  return ValidateForm('input[check],textarea[check]');
              });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">

        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>酒店</td>
                    <td>
                        <asp:Literal ID="ltlHotel" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>宴会厅名称
                    </td>
                    <td>
                        <asp:TextBox ID="txtHallName" check="1" tip="限20个字符！" MaxLength="20" runat="server"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>楼层</td>
                    <td>
                        <asp:TextBox ID="txtFloorName" check="1" tip="限20个字符！" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>层高</td>
                    <td>
                        <asp:TextBox ID="txtFloorHeight" check="1" Width="70" tip="请填写整数或小数（只保留一位）" runat="server" MaxLength="10"></asp:TextBox>米
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>空高</td>
                    <td>
                        <asp:TextBox ID="txtEmptyHeight" check="1" Width="70" tip="请填写整数或小数（只保留一位）" runat="server" MaxLength="10"></asp:TextBox>米
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>桌数</td>
                    <td>
                        <asp:TextBox ID="txtDeskCount" Width="70" tip="大于0的整数！" check="1" runat="server" MaxLength="10"></asp:TextBox>桌
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>餐标</td>
                    <td>
                        <asp:TextBox ID="txtMeal" Width="70" tip="大于0的整数！" check="1" runat="server" MaxLength="10"></asp:TextBox>元
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>面积</td>
                    <td>
                        <asp:TextBox ID="txtArea" Width="70" tip="请填写整数或小数（只保留一位）！" check="1" runat="server" MaxLength="10"></asp:TextBox> m²
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>椅套颜色</td>
                    <td>
                        <asp:TextBox ID="txtFloorName0" check="1" tip="限20个字符！" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>桌布颜色</td>
                    <td>
                        <asp:TextBox ID="txtFloorName1" check="1" tip="限20个字符！" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>口布颜色</td>
                    <td>
                        <asp:TextBox ID="txtFloorName2" check="1" tip="限20个字符！" CssClass="{required:true}" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>说明
                    </td>
                    <td>
                        <asp:TextBox ID="txtBanquetExplain" check="0" tip="限50个字符！" TextMode="MultiLine" Rows="3" CssClass="{required:true}" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" OnClick="btnSave_Click" runat="server" CssClass="btn btn-success" Text="确定" />
                    </td>
                    <td></td>
                </tr>

            </table>
        </div>

    </div>

</asp:Content>
