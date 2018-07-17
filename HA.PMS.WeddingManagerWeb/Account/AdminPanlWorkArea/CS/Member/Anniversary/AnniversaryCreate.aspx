<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="AnniversaryCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.Anniversary.AnniversaryCreate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
            $(document).ready(function () {
         
                $("html,body").css({ "width": "800px", "height": "560px", "background-color": "transparent" });


                $("#ddltype").change(function () {

                    var SelectItem = $("#ddltype").find("option:selected").val();
 
                    if (SelectItem.indexOf("True")>=0) {
                       
                        ShowSendMessAge(SelectItem);
                    }

                });

            });
            $(window).load(function () {
                BindCtrlRegex();
                BindCtrlEvent('input[check],textarea[check]');
                $("#<%=btnSaveChange.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });

        function ShowSendMessAge(TypeID) {
            var Url = "/AdminPanlWorkArea/CS/Member/SendMessage.aspx?TypeID=" + TypeID.split(",")[1] + "&CustomerID=" + getQueryString("CustomerID");
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function BindCtrlRegex() {
            BindText(100, '<%=txtContent.ClientID%>');
        }


        ///当选择SP业务时 弹出效果



        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }
            </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <table>
        <tr>
            <td><span style="color:red">*</span>方式:</td>
            <td>
                <asp:DropDownList ID="ddltype" ClientIDMode="Static" runat="server"  OnSelectedIndexChanged="ddltype_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label ID="lblSp" runat="server" Text="" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>服务说明</td>
            <td>
                <asp:TextBox ID="txtSpNode" runat="server" TextMode="MultiLine" Width="496px" Rows="3"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblPhone" runat="server" Text="" Enabled="false" Visible="false"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server" Enabled="false" Visible="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><span style="color:red">*</span>备注说明:</td>
            <td>
                <asp:TextBox ID="txtContent" check="1" MaxLength="100" tip="限100个字符！" runat="server" Rows="10" TextMode="MultiLine" Width="500"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>服务人员:</td>
            <td>
                <asp:Label ID="lblEmployeeName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSaveChange" CssClass="btn btn-success" runat="server" Text="保存" OnClick="btnSaveChange_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
