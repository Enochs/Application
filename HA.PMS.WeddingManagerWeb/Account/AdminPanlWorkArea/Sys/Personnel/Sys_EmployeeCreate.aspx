<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="Sys_EmployeeCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_EmployeeCreate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .rdo {
            float: right;
        }
    </style>
    <script type="text/javascript">
        $(window).load(function () {
            BindCtrlRegex();
        });
        function BindCtrlRegex() {
            BindString(2, 5, '<%=txtName.ClientID%>');
            BindUsername('<%=txtLoginName.ClientID%>');
            BindMobile('<%=txtPhone.ClientID%>');
            BindTel('<%=txtTellPhone.ClientID%>');;
            BindDate('<%=txtBirthday1.ClientID%>');
            BindQQ('<%=txtQQ.ClientID%>');
            BindEmail('<%=txtEmail.ClientID%>');
        }

        //上传图片
        function ShowFileUploadPopu(EmployeeID) {
            var Url = "/AdminPanlWorkArea/Sys/Personnel/Sys_EmployeeUpload.aspx?EmployeeID=" + EmployeeID;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function CheckSubmit() {
            if ($("#txtName").val() == "") {
                alert("请填写姓名");
                $("#txtName").focus();
                return false;
            } else if ($("#txtLoginName").val() == "") {
                alert("请填写登录帐号");
                $("#txtLoginName").focus();
                return false;
            } else if ($("#txtBirthday1").val() == "") {
                alert("请选择自己的生日");
                $("#txtBirthday1").focus();
                return false;
            } else if ($("#txtPhone").val() == "") {
                alert("请填写手机号码");
                $("#txtPhone").focus();
                return false;
            } else if ($("#txtCurrentLocation").val() == "") {
                alert("请填写自己的家庭住址");
                $("#txtCurrentLocation").focus();
                return false;
            } else if ($("#txtQQ").val() == "") {
                alert("请填写自己的QQ号码");
                $("#txtQQ").focus();
                return false;
            } else if ($("#txtCardId").val() == "") {
                alert("请填写自己的身份证号码");
                $("#txtCardId").focus();
                return false;
            }
            else if ($("#txtBankName").val() == "") {
                alert("请输入开户银行名称");
                return false;
            } else if ($("#txtBankCard").val() == "") {
                alert("请填写自己的银行卡号");
                $("#txtBankCard").focus();
                return false;
            //} else if ($("#txtPlanChecks").val() == "") {
            //    alert("请选择直接领导");
            //    $("#txtPlanChecks").focus();
            //    return false;
            } else if ($("#txtEntryTime").val() == "") {
                alert("请选择入职日期");
                $("#txtEntryTime").focus();
                return false;
            } else if ($("#imageUpload").val() == "") {
                alert("请选择个人头像");
                return false;
            } else if ($("#FileUpCardId").val() == "") {
                alert("请选择身份证复印件");
                return false;
            }
        }

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
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>添加新成员</h5>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>
                        <label>姓名:</label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" ClientIDMode="Static" check="1" tip="限2~5个字符！" CssClass="{required:true}" MaxLength="10"></asp:TextBox>
                        <span style="color: red">*</span></td>
                </tr>
                <tr>
                    <td>
                        <label>职位:</label></td>
                    <td>
                        <asp:DropDownList ID="ddlJob" runat="server" ClientIDMode="Static" CssClass="{required:true}"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>部门:</td>
                    <td>
                        <asp:Label ID="lblDepartment" runat="server" ClientIDMode="Static" Text=""></asp:Label></td>
                </tr>

                <tr style="display: none;">
                    <td>分组:</td>
                    <td>
                        <asp:DropDownList ID="ddlGroup" runat="server" ClientIDMode="Static" validate="required:true"></asp:DropDownList></td>
                </tr>

                <tr>
                    <td>员工类型:</td>
                    <td>
                        <asp:DropDownList ID="ddlEmployeeType" ClientIDMode="Static" validate="required:true" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>
                        <label>登陆名:</label></td>
                    <td>
                        <asp:TextBox ID="txtLoginName" check="1" ClientIDMode="Static" tip="3~12位，必须以字母开头，只能包含字母、下划线和数字！" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>密码:</label></td>
                    <td>
                        <asp:TextBox ID="txtPwd" TextMode="Password" check="1" ClientIDMode="Static" tip="密码为6~12位！" runat="server" MaxLength="20"></asp:TextBox>
                        (默认：123456)
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>性别:</label></td>
                    <td>
                        <table width="140">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rdoMan" Text="男" Checked="true" GroupName="sex" runat="server" /></td>
                                <td>
                                    <asp:RadioButton ID="rdoWoman" Text="女" GroupName="sex" runat="server" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>生日</td>
                    <td>
                        <asp:TextBox ID="txtBirthday1" onclick="WdatePicker()" ClientIDMode="Static" check="0" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>上传头像：</td>
                    <td>
                        <asp:FileUpload ID="imageUpload" runat="server" ClientIDMode="Static" ToolTip="上传头像" />
                        <%--<asp:TextBox runat="server" ID="txtImages" Enabled="false" />--%>
                    </td>
                </tr>
                <tr>
                    <td>手机</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtPhone" check="0" ClientIDMode="Static" tip="手机号码为11位数字！" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>座机号</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtTellPhone" check="0" tip="格式：0511-4405222，021-87888822" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>住址</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtCurrentLocation" ClientIDMode="Static" check="0" tip="个人家庭住址" runat="server" MaxLength="100" />
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>QQ</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtQQ" ClientIDMode="Static" check="0" tip="QQ号码为5~11位数字！" runat="server" MaxLength="20"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>微信</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtWeiXin" check="0" tip="个人微信帐号" runat="server" MaxLength="50"></asp:TextBox></td>
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
                        <asp:TextBox Style="margin: 0" ID="txtCardId" ClientIDMode="Static" check="0" tip="身份证号为19位数字" runat="server" MaxLength="50"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>身份证复印件</td>
                    <td>
                        <asp:FileUpload Style="margin: 0" ClientIDMode="Static" runat="server" ID="FileUpCardId" />
                        <%--<asp:TextBox runat="server" ID="txtFileCardId" Enabled="false" />--%>
                    </td>
                </tr>
                <tr>
                    <td>开户银行</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtBankName" ClientIDMode="Static" check="0" tip="请输入开户银行名称" runat="server" MaxLength="50"></asp:TextBox>
                        <span style="color: red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>银行卡号</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtBankCard" ClientIDMode="Static" check="0" tip="银行卡号尾数在19位树之内" runat="server" MaxLength="50"></asp:TextBox>
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
                        <asp:TextBox Style="margin: 0" ID="txtEntryTime" ClientIDMode="Static" check="0" tip="个人入职时间" runat="server" MaxLength="50" onclick="WdatePicker();"></asp:TextBox>
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
                        <asp:TextBox Style="margin: 0" ID="txtWorkNumber" check="0" tip="工号号码" runat="server" MaxLength="50" Text="0"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>档案备份</td>
                    <td>
                        <asp:TextBox Style="margin: 0" ID="txtBackUps" check="0" tip="档案备份" runat="server" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="rptDataFile">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("DataName") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

                <tr>
                    <td>备注</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 200px; height: 60px;" TextMode="MultiLine" ID="txtRemark" check="0" tip="备注信息" runat="server" MaxLength="500"></asp:TextBox></td>
                </tr>
                <%--                <tr style="display:none;">
                    <td><label>计划审批人:</label></td>
                    <td><asp:TextBox ID="txtPlanChecks" check="0" tip="审批人ID" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td><label>辅导人:</label></td>
                    <td><asp:TextBox ID="txtCoach" check="0" tip="辅导人ID" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display:none;">
                    <td><label>查看人:</label></td>
                    <td><asp:TextBox ID="txtLook" check="0" tip="查看人ID" runat="server" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <asp:Button CssClass="btn btn-success" ID="btnCreate" runat="server" Text="创建" OnClick="btnCreate_Click" OnClientClick="return CheckSubmit();" /></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
