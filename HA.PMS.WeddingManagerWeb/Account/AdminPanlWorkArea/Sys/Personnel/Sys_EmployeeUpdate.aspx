<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="Sys_EmployeeUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_EmployeeUpdate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("html,body").css({ "background-color": "transparent", "scroll": "no" });
            BindString(2, 5, '<%=txtName.ClientID%>');
            BindString(6, 12, '<%=txtPassWord.ClientID%>');
            BindMobile('<%=txtPhone.ClientID%>');
            BindUsername('<%=txtLoginName.ClientID%>');
            BindTel('<%=txtTellPhone.ClientID%>');
            BindDate('<%=txtBirthday.ClientID%>');
            BindQQ('<%=txtQQ.ClientID%>');
            BindEmail('<%=txtEmail.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnUpdate.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
            $('#<%=txtBirthday.ClientID%>').change(function () { $(this).blur(); });
        });

        //点击文本框 弹出部门人员列表
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=1&ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 480, 380, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div class="widget-box" style="width: 97%">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>修改成员信息</h5>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>姓名</td>
                    <td>
                        <asp:TextBox Style="margin: 0;" ID="txtName" check="1" tip="限2~5个字符！" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span></td>
                </tr>
                <tr>
                    <td>职位</td>
                    <td>
                        <asp:DropDownList Style="margin: 0" ID="ddlJob" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>部门</td>
                    <td>
                        <asp:DropDownList Style="margin: 0" ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged"></asp:DropDownList></td>
                </tr>

                <tr style="display: none">
                    <td>分组</td>
                    <td>
                        <asp:DropDownList Style="margin: 0" ID="ddlGroup" runat="server"></asp:DropDownList></td>
                </tr>

                <tr>
                    <td>员工类型</td>
                    <td>
                        <asp:DropDownList Style="margin: 0" ID="ddlEmployeeType" runat="server"></asp:DropDownList></td>
                </tr>

                <tr>
                    <td>性别</td>
                    <td>
                        <asp:RadioButton ID="rdoMan" Width="20" Text="男" Checked="true" GroupName="sex" runat="server" />&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoWoman" Width="20" Text="女" GroupName="sex" runat="server" /></td>

                </tr>

                <tr>
                    <td>生日</td>
                    <td>
                        <asp:TextBox Style="margin: 0" runat="server" ID="txtBirthday" check="0" MaxLength="20" onclick="WdatePicker()" />
                    </td>
                </tr>
                <tr>
                    <td>头像</td>
                    <td>
                        <asp:FileUpload Style="margin: 0" runat="server" ID="imageUpload" />
                        <asp:TextBox runat="server" ID="txtImage" Enabled="false" />
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr id="trLoginName" runat="server">
                    <td>登录名</td>
                    <td>
                        <asp:TextBox ID="txtLoginName" check="1" tip="3~12位，必须以字母开头，只能包含字母、下划线和数字！" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr id="trPassWord" runat="server">
                    <td>密码</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtPassWord" check="0" tip="密码为6~12位！" runat="server" MaxLength="20"></asp:TextBox>（不需修改则不填写）</td>
                </tr>
                <tr>
                    <td>手机</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtPhone" check="0" tip="手机号码为11位数字！" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>座机号</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtTellPhone" check="0" tip="格式：0511-4405222，021-87888822" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>住址</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtCurrentLocation" check="0" tip="个人家庭住址" runat="server" MaxLength="100" />
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>QQ</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtQQ" check="0" tip="QQ号码为5~11位数字！" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>微信</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtWeiXin" check="0" tip="个人微信帐号" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>微博</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtWeiBo" check="0" tip="个人微博帐号" runat="server" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtEmail" check="0" tip="格式：example@mail.com" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>身份证号</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtCardId" check="0" tip="身份证号为19位数字" runat="server" MaxLength="50"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>身份证复印件</td>
                    <td>
                        <asp:FileUpload Style="margin: 0" runat="server" ID="FileUpCardId" />
                        <asp:TextBox runat="server" ID="txtFileCardId" Enabled="false" />
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>开户银行</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtBankName" check="0" tip="开户银行名称" runat="server" MaxLength="50"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>银行卡号</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtBankCard" check="0" tip="银行卡号尾数在19位树之内" runat="server" MaxLength="50"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>直接领导</td>
                    <td runat="server" id="td_planChecks">
                        <asp:HiddenField runat="server" ClientIDMode="Static" Value="-1" ID="hideEmpLoyeeID" />
                        <asp:TextBox runat="server" ID="txtEmpLoyee" CssClass="txtEmpLoyeeName" onclick="ShowPopu(this);" ClientIDMode="Static" Style="margin: 0; width: 90px;" />
                        <a href="#" onclick="ShowPopu(this);" class="SetState btn btn-primary">选择</a>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>入职时间</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtEntryTime" check="0" tip="个人入职时间" runat="server" MaxLength="50" onclick="WdatePicker();"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>转正时间</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtPositiveTime" check="0" tip="个人转正时间" runat="server" MaxLength="50" onclick="WdatePicker();"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>工号</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtWorkNumber" check="0" tip="工号号码" runat="server" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>档案备份</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtBackUps" check="0" tip="档案备份" runat="server" MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>备注</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 200px; height: 60px;" TextMode="MultiLine" ID="txtRemark" check="0" tip="备注信息" runat="server" MaxLength="500"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnUpdate" CssClass="btn btn-success" runat="server" Text="确认" OnClick="btnUpdate_Click" /></td>
                </tr>
                <%--<tr>
                    <td>计划审批人</td>
                    <td><asp:TextBox style="margin:0" ID="TextBox1" check="1" tip="审批人ID" runat="server" MaxLength="10"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>辅导人</td>
                    <td><asp:TextBox style="margin:0" ID="txtCoach" check="0" tip="辅导人ID" runat="server" MaxLength="10"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>查看人</td>
                    <td><asp:TextBox style="margin:0" ID="txtLook" check="0" tip="查看人ID" runat="server" MaxLength="10"></asp:TextBox></td>
                </tr>--%>
            </table>
        </div>
    </div>
</asp:Content>
