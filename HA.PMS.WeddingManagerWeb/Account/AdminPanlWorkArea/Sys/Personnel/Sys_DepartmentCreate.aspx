<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="Sys_DepartmentCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_DepartmentCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //验证信息
        function validateMsg() {
            if ($.trim($("#txtDeparName").val()) == "") {
                return false;
            } else {
                return true;
            }
        }

        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }



        //选择父级部门
        function SelectDepartmentPopu() {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectDepartmentBythis.aspx?ControlKey=hideDepartmetnKey&TxtControl=txtUpdepartmetnname&ALL=True";
            showPopuWindows(Url, 700, 700, "#SelectDepartmetnByThis");
            $("#SelectDepartmetnByThis").click();
        }

        //选择父级部门
        function SelectDepartmentPopuSource() {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectDepartmentBythis.aspx?ControlKey=hiddDataSource&TxtControl=txtDataSource&ALL=True";
            showPopuWindows(Url, 700, 700, "#SelectDepartmetnByThis");
            $("#SelectDepartmetnByThis").click();
        }

        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSend.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtDeparName.ClientID%>:<%=txtUpdepartmetnname.ClientID%>');
        }
        $(document).ready(function () { $("html,body").css({ "background-color": "transparent", "height": 300 }); });
    </script>

    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <a href="#" id="SelectDepartmetnByThis" style="display: none;">选择部门</a>
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>创建新部门</h5>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>部门名称</td>
                    <td>
                        <asp:TextBox ID="txtDeparName" check="1" tip="限20个字符！" runat="server" MaxLength="20" />
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>上级部门</td>
                    <td>
                        <asp:Label ID="txtUpdepartmetnname" ClientIDMode="Static" runat="server" MaxLength="20"></asp:Label>
                        <%--<asp:TextBox ID="txtUpdepartmetnname" check="0" tip="若不填，则为添加顶级部门" ClientIDMode="Static" runat="server" MaxLength="20"></asp:TextBox>--%>
                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideDepartmetnKey" />
                    </td>
                </tr>

                <tr style="display:none;">
                    <td>数据来源</td>
                    <td>
                        <asp:TextBox ID="txtDataSource" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hiddDataSource" />
                        <a href="#" onclick="SelectDepartmentPopuSource();" class="SetState">选择</a> </td>
                </tr>

                <tr>
                    <td>

                        <asp:Button ID="btnSend" runat="server" CssClass="btn btn-success" OnClientClick="validateMsg();" Text="添加" OnClick="btnSend_Click" />

                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
