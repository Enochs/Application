<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DispatchingEmpLoyeeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.DispatchingEmpLoyeeManager" Title="项目统筹" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc3" %>
<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hideIsDis").val() == "1") {
                $("[type='text']").attr("disabled", "disabled");
                $("[type='button']").attr("disabled", "disabled");
                $(".Requirement").attr("disabled", "disabled");
                $(".btndelete").hide();

                $(".SelectSG").hide();
                $(".SelectPG").hide();

                $("input,textarea,select").attr("disabled", "disabled");
                $("input[type=button],.btn").hide();
            }

            if ('<%=Request["OnlyView"]%>') {
                $("input,textarea,select").attr("disabled", "disabled");
                $("input[type=button],.btn").hide();
            }
        });
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 700, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function DeleteData() {
            if (confirm("确认删除！")) {
                return true;
            } else {
                return false;
            }

        }

        function SaveChange()
        {
            return ValidateForm('input[check],textarea[check]');
        }

        $(window).load(function () {
            BindString(20, '<%=txtEmployeeType.ClientID%>:<%=txtEmployeeName.ClientID%>');
            BindMoney('<%=txtAmount.ClientID%>');
            BindMobile('<%=txtTelPhone.ClientID%>');
            BindText(50, '<%=txtBulding.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <br />
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <asp:HiddenField ID="hideIsDis" ClientIDMode="Static" runat="server" />
    <table style="text-align: center; width: 93%;" border="1" class="table table-bordered table-striped">
        <tr>
            <th>人员类型</th>
            <th>电话</th>
            <th>姓名</th>
            <th>工作内容</th>
            <th>项目工资</th>
            <th>操作</th>

        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtEmployeeType" check="1" tip="限20个字符！" runat="server" Width="155px"  CssClass="NoneEmpty" MaxLength="20"></asp:TextBox><span style="color:red">*</span></td>
            <td>
                <asp:TextBox ID="txtTelPhone" check="1" tip="手机号码为11位数字！" runat="server" Width="155px"  CssClass="NoneEmpty" MaxLength="20"></asp:TextBox><span style="color:red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtEmployeeName" tip="姓名" check="1" runat="server" Width="155px"  CssClass="NoneEmpty" MaxLength="20"></asp:TextBox><span style="color:red">*</span></td>
            <td>
                <asp:TextBox ID="txtBulding" check="1" tip="限50个字符！" runat="server" Width="155px"  CssClass="NoneEmpty" MaxLength="50"></asp:TextBox><span style="color:red">*</span>
            </td>
            <td>
                <asp:TextBox ID="txtAmount" check="1" runat="server" Width="155px"  CssClass="NoneEmpty" MaxLength="20"></asp:TextBox><span style="color:red">*</span></td>

            <td>
                <asp:Button ID="Button1" runat="server" Text="保存" Height="29" OnClick="btnSaveChange_Click" CssClass="btn btn-info" OnClientClick="return SaveChange()" /></td>

        </tr>
        <asp:Repeater ID="repWeddingPlanning" runat="server" OnItemCommand="repWeddingPlanning_ItemCommand">

            <ItemTemplate>
                <tr>
                    <td>
                        <asp:TextBox ID="txtEmployeeType" runat="server" Width="155px" Text='<%#Eval("EmployeeType") %>'></asp:TextBox></td>

                    <td>
                        <asp:TextBox ID="txtTelPhone" runat="server" Width="155px" Text='<%#Eval("TelPhone") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtEmployeeName" runat="server" Width="155px" Text='<%#Eval("EmployeeName") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtBulding" runat="server" Text='<%#Eval("Bulding") %>' Width="155px"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" Text='<%#Eval("Amount") %>' Width="155px"></asp:TextBox></td>

                    <td style="white-space: nowrap;">
        
                        <asp:LinkButton ID="lnkbtnDelete" CssClass="btndelete btn btn-primary" CommandName="Delete" CommandArgument='<%#Eval("DeJey") %>' runat="server" OnClientClick="return DeleteData();">删除</asp:LinkButton>
                        
                    </td>

                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="6">
                
            </td>
        </tr>
    </table>
</asp:Content>


