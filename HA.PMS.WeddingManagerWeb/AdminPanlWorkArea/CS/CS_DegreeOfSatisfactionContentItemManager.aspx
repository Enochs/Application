<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CS_DegreeOfSatisfactionContentItemManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_DegreeOfSatisfactionContentItemManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        
    </style>
    <script type="text/javascript">
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnQuery.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtSumDof.ClientID%>');
            BindText(200, '<%=txtDofContent.ClientID%>');
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>新人满意度调查界面</h5>
            <span class="label label-info"></span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <thead>

                    <tr>
                        <th>类别</th>
                        <th>满意度</th>
                        <th>原因及建议</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDegree" runat="server">

                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("CSContent")%></td>
                                <td><%#Eval("SumDof") %></td>
                                <td><%#Eval("DofContent")%></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="DegreePager" AlwaysShow="true" OnPageChanged="DegreePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
               <div class="row-fluid">
                <div class="span9">
                    <div class="widget-box">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-ok"></i></span>
                            <h5>填写操作</h5>
                        </div>
                        <div class="widget-content nopadding">
                            请选择用户: <asp:DropDownList ID="ddlCustomers" runat="server"></asp:DropDownList><br />
                            调查状态:<asp:DropDownList ID="ddlInvestigateState" runat="server"></asp:DropDownList><br />
                            处理满意度类型:<asp:DropDownList ID="ddlDegreeOfSatisfactionContent" runat="server"></asp:DropDownList><br />
                            <span style="color:red">*</span>总体满意度: <asp:TextBox ID="txtSumDof" check="1" tip="限20个字符！" runat="server"></asp:TextBox>
                            <br />
                            <span style="color:red">*</span>原因及建议：<asp:TextBox ID="txtDofContent" check="1" tip="限200个字符！" TextMode="MultiLine" Rows="20" runat="server" Width="305px" Height="136px"></asp:TextBox>
                            <br />
                            <asp:Button ID="btnQuery" CssClass="btn btn-success" runat="server" Text="保存" OnClick="btnQuery_Click" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
