<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FL_SetEmployeeTarget.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget.FL_SetEmployeeTarget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            $(".XXXA").each(function () {
                if ($(this).val() == $("#hideFinishKey").val()) {
                    $(this).attr("checked", true);
                }
            }
        );

            $(".XXXA").click(function () {
                $("#hideFinishKey").attr("value", $(this).val());
            });
        });
     

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hideFinishKey" runat="server" ClientIDMode="Static" Value="3" />
     <asp:HiddenField ID="hideOldKey" runat="server" ClientIDMode="Static" Value="3" />
    <table class="table table-bordered table-striped">
        <tr>
            <td>设置关键指标</td>
            <td>模块名称</td>
            <td>关键指标</td>
        </tr>

        <asp:Repeater ID="repEmployeeTargetList" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <input id="rdoActive" type="radio" name="xxxx"  value="<%#Eval("FinishKey") %>" class="XXXA" />
                    </td>
                    <td><%#GetChannelNameByID(Eval("TargetID")) %>
                        <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("FinishKey") %>' />
                        <asp:HiddenField ID="TargetKey" runat="server" Value='<%#Eval("TargetID") %>' />
                        <asp:HiddenField ID="EmployeeKey" runat="server" Value='<%#Eval("EmployeeID") %>' />
                    </td>
                    <td><%#Eval("TargetTitle") %></td>

                </tr>
            </ItemTemplate>
        </asp:Repeater>

        <tr>
            <td colspan="3">
                <asp:Button ID="btnSaveChange" runat="server" Text="保存" OnClick="btnSaveChange_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
