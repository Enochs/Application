<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CS_DegreeOfSatisfactionCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_DegreeOfSatisfactionCreate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindText(200, '<%=txtDofContent.ClientID%>');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>客户满意度调查</h5>
            <span class="label label-info"></span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td>调查项目</td>
                    <td>满意度</td>
                    <td>建议
                    </td>
                </tr>
                <asp:Repeater ID="repItemList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("Title") %>
                                <asp:HiddenField runat="server" ID="HideKey" Value='<%#Eval("ItemKey") %>' />

                            </td>
                            <td>
                                <cc1:ddlDegreeAssessResult ID="DdlDegreeAssessResult1" runat="server"></cc1:ddlDegreeAssessResult></td>
                            <td>
                                <asp:TextBox ID="txtContent" MaxLength="50" runat="server" Width="600px" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td>总体满意度</td>
                    <td>
                        <cc1:ddlDegreeAssessResult ID="ddlSumDof" runat="server"></cc1:ddlDegreeAssessResult></td>
                    <td>&nbsp;</td>

                </tr>

                <tr>
                    <td><span style="color: red">*</span>总体建议
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDofContent" check="1" tip="限20个字符！" runat="server" Rows="3" TextMode="MultiLine" Width="700px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="确定" OnClick="btnSave_Click" />
                    </td>
                </tr>
            </table>

        </div>
    </div>
</asp:Content>
