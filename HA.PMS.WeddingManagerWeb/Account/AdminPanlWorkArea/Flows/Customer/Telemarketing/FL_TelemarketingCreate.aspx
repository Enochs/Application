<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_TelemarketingCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.Telemarketing.FL_TelemarketingCreate" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>部门</h5>
        </div>
        <div class="widget-content ">
            <table border="1">
                <tr>
                    <td>部门</td>
                    <td>
                        <asp:DropDownList ID="ddlDepartment" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>人员</td>
                    <td>
                        <asp:DropDownList ID="ddlEmpLoyee" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button CssClass="btn btn-danger" ID="btnSaveDate" runat="server" Text="保存" OnClick="btnSaveDate_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>
