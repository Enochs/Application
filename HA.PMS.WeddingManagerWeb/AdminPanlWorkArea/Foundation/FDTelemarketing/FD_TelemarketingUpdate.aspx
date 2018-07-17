<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_TelemarketingUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing.FD_TelemarketingUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtGroomBirthday.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd' });
            $("#<%=txtBrideirthday.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd' });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>基本信息修改界面</h5>
            <span class="label label-info">修改界面</span>
        </div>
        <div class="widget-content">

            <table class="table table-bordered table-striped">
                <tr>
                    <td>新郎姓名:</td>
                    <td>
                        <asp:TextBox ID="txtGroom" CssClass="{required:true}" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>新郎生日:</td>
                    <td>
                        <asp:TextBox ID="txtGroomBirthday" CssClass="{required:true,date:true}" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>新娘:</td>
                    <td>
                        <asp:TextBox ID="txtBride" CssClass="{required:true}" runat="server"></asp:TextBox>


                    </td>
                </tr>
                <tr>
                    <td>新娘生日：</td>
                    <td>
                        <asp:TextBox ID="txtBrideirthday" CssClass="{required:true,date:true}" runat="server"></asp:TextBox>


                    </td>
                </tr>
                <tr>
                    <td>新郎联系电话:</td>
                    <td>
                        <asp:TextBox ID="txtGroomCellPhone"  CssClass="{required:true,number:true,rangelength:[11,11],messages:{number:'你的电话未必不是数字?',required:'请输入电话号码',rangelength:'为了更准确的找到你的客户，你输入的手机号码必须是11位'}}" runat="server"></asp:TextBox>


                    </td>
                </tr>
                <tr>
                    <td>新娘联系电话:</td>
                    <td>
                        <asp:TextBox ID="txtBrideCellPhone"  CssClass="{required:true,number:true,rangelength:[11,11],messages:{number:'你的电话未必不是数字?',required:'请输入电话号码',rangelength:'为了更准确的找到你的客户，你输入的手机号码必须是11位'}}" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>经办人:</td>
                    <td>
                        <asp:TextBox ID="txtOperator" CssClass="{required:true}" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>经办人关系:</td>
                    <td>
                        <asp:TextBox ID="txtOperatorRelationship" CssClass="{required:true}" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>经办人联系电话:</td>
                    <td>
                        <asp:TextBox ID="txtOperatorPhone"  CssClass="{required:true,number:true,rangelength:[11,11],messages:{number:'你的电话未必不是数字?',required:'请输入电话号码',rangelength:'为了更准确的找到你的客户，你输入的手机号码必须是11位'}}" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>婚礼预算:</td>
                    <td>
                        <asp:TextBox ID="txtPartyBudget" CssClass="{required:true,number:true}" runat="server"></asp:TextBox>
                        （元）
                    </td>
                </tr>
                <tr>
                    <td>婚礼形式:</td>
                    <td>
                        <asp:TextBox ID="txtFormMarriage" CssClass="{required:true}" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>喜欢的颜色:</td>
                    <td>
                        <asp:TextBox ID="txtLikeColor" CssClass="{required:true}"  runat="server"></asp:TextBox>


                    </td>
                </tr>
                <tr>
                    <td>期望气氛:
                    </td>
                    <td>
                        <asp:TextBox ID="txtExpectedAtmosphere" CssClass="{required:true}" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>爱好特长:</td>
                    <td>
                        <asp:TextBox ID="txtHobbies" CssClass="{required:true}" runat="server"></asp:TextBox>


                    </td>
                </tr>
                <tr>
                    <td>有无禁忌:</td>
                    <td>
                        <asp:TextBox ID="txtNoTaboos" CssClass="{required:true}" runat="server"></asp:TextBox>


                    </td>
                </tr>
                <tr>
                    <td>最看重婚礼服务项目:</td>
                    <td>
                        <asp:TextBox ID="txtWeddingServices" CssClass="{required:true}" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>最看重的婚礼仪式流程:
                    </td>
                    <td>
                        <asp:TextBox ID="txtImportantProcess" CssClass="{required:true}" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>相识相恋的经历:</td>
                    <td>
                        <asp:TextBox ID="txtExperience"  CssClass="{required:true}" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>期望的出场方式:</td>
                    <td>
                        <asp:TextBox ID="txtDesiredAppearance" CssClass="{required:true}" runat="server"></asp:TextBox>


                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnTelemark" CssClass="btn btn-success" runat="server" Text="确认" OnClick="btnTelemark_Click" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
