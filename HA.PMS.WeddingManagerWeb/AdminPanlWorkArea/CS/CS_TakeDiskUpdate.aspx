<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CS_TakeDiskUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_TakeDiskUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
           // $("#<//%=txtTakePlanTime.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd' });
             // $("#<//%=txtConsigneeTime.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd' });
              //$("#<//%=txtNoticeTime.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd' });
            //$("#<//%=txtrealityTime.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd' });
            $("html,body").css({ "width": "500px", "height": "500px" });
          });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>取件修改界面</h5>
            <span class="label label-info">修改界面</span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">

                <tr>
                    <td>客户</td>
                    <td>
                        <asp:DropDownList ID="ddlCustomer" runat="server">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td>计划取件时间</td>
                    <td>
                        <asp:TextBox ID="txtTakePlanTime" onclick="WdatePicker();"  runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>收件时间</td>
                    <td>
                        <asp:TextBox ID="txtConsigneeTime" onclick="WdatePicker();" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>通知时间</td>
                    <td>
                        <asp:TextBox ID="txtNoticeTime" onclick="WdatePicker();" CssClass="{required:true,date:true}"    runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>实际取件时间</td>
                    <td>
                        <asp:TextBox ID="txtrealityTime" onclick="WdatePicker();" CssClass="{required:true,date:true}"   runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                    </td>
                </tr>
                <tr>
                    <td>是否已取件</td>
                    <td>
                        <asp:DropDownList ID="ddlTakeState" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="确定" OnClick="btnSave_Click" /></td>
                    <td></td>
                </tr>
            </table>

        </div>
    </div>
</asp:Content>
