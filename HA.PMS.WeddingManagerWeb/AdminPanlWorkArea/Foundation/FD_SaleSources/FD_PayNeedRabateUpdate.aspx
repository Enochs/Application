<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_PayNeedRabateUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_PayNeedRabateUpdate" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 300px;
        }
    </style>
    <script type="text/javascript">
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(5, '<%=ddlTactcontacts.ClientID%>');
            BindString(20, '<%=txtPaypolicy.ClientID%>');
            BindDate('<%=txtPayDate.ClientID%>');
            BindMobile('<%=txtMoneyCell.ClientID%>');
            BindMoney('<%=txtPayMoney.ClientID%>:<%=txtOrderMoney.ClientID%>');
        }

    </script>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>返利核算修改</h5>

        </div>
        <div class="widget-content" style="overflow-x: scroll;">
            <div class="widget-box">
                <div class="widget-content">

                    <table class="table table-bordered table-striped with-check">
                        <tr>
                            <td style="width:200px">渠道类型</td>
                            <td>

                                <cc2:ddlChannelType ID="ddlChannelTypeCreate" AutoPostBack="true" OnSelectedIndexChanged="ddlChannelTypeCreate_SelectedIndexChanged"
                                     Width="110" runat="server"></cc2:ddlChannelType>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">渠道名称</td>
                            <td>
                                  <asp:DropDownList ID="ddlChannelCreate" AutoPostBack="true" OnSelectedIndexChanged="ddlChannelCreate_SelectedIndexChanged" Width="110" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                       
                        
                        <tr>
                            <td style="width:200px">支付日期</td>
                            <td>
                                 <asp:TextBox ID="txtPayDate" check="1" onclick="WdatePicker()" MaxLength="20" ClientIDMode="Static" Width="110"  runat="server"></asp:TextBox>
                                <span style="color:red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">支付金额</td>
                            <td>
                                <asp:TextBox ID="txtPayMoney" check="1" Width="110" runat="server"></asp:TextBox>
                                <span style="color:red">*</span>
                            </td>
                        </tr>
                         <tr>
                            <td style="width:200px">订单金额</td>
                            <td>
                                 <asp:TextBox ID="txtOrderMoney" check="1" Width="110" runat="server"></asp:TextBox>
                                <span style="color:red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">收款人</td>
                            <td>
                                <asp:DropDownList ID="ddlTactcontacts" check="1" Width="110" runat="server"></asp:DropDownList>
                                <span style="color:red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">收款人电话</td>
                            <td>
                                 <asp:TextBox ID="txtMoneyCell" check="1"   Width="110" runat="server"></asp:TextBox>
                                <span style="color:red">*</span>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:200px">支付政策</td>
                            <td>
                                  <asp:TextBox ID="txtPaypolicy" check="0" tip="限20个字符！" Width="110" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        
                         <tr>
                            <td>
                                <asp:Button ID="btnSave" CssClass="btn"  OnClick="btnSave_Click" runat="server" Text="保存" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
