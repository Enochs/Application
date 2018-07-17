<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="Sys_EmployeeDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_EmployeeDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>员工详细信息界面</h5>
            <span class="label label-info">详细界面</span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">

                <tr>
                    <td>姓名:</td>
                    <td>
                        <asp:Literal ID="ltlName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>职位:</td>
                    <td>
                        <asp:Literal ID="ltlJob" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>部门:</td>
                    <td>
                        <asp:Literal ID="ltlDepartment" runat="server"></asp:Literal>

                    </td>
                </tr>

                <tr>
                    <td>分组:</td>
                    <td>
                        <asp:Literal ID="ltlGroup" runat="server"></asp:Literal>

                    </td>
                </tr>

                <tr>
                    <td>员工类型:</td>
                    <td>
                        <asp:Literal ID="ltlEmplyoeeType" runat="server"></asp:Literal>

                    </td>
                </tr>


                <tr>
                    <td>性别:</td>
                    <td>
                        <asp:Literal ID="ltlSex" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>手机:</td>
                    <td>
                        <asp:Literal ID="ltlPhone" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>座机号:</td>
                    <td>
                        <asp:Literal ID="ltlCellPhone" runat="server"></asp:Literal>

                    </td>
                </tr>
                <tr>
                    <td>QQ:</td>
                    <td>
                        <asp:Literal ID="ltlQQ" runat="server"></asp:Literal>

                    </td>
                </tr>
                <tr>
                    <td>微信:</td>
                    <td>
                        <asp:Literal ID="ltlWeiXin" runat="server"></asp:Literal>

                    </td>
                </tr>
                <tr>
                    <td>微博:</td>
                    <td>
                        <asp:Literal ID="ltlWeiBo" runat="server"></asp:Literal>

                    </td>
                </tr>
                <tr>
                    <td>Email:</td>
                    <td>
                        <asp:Literal ID="ltlEmail" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>身份证号:</td>
                    <td>
                        <asp:Literal ID="ltlCardId" runat="server"></asp:Literal>

                    </td>
                </tr>
                <tr>
                    <td>开户银行:</td>
                    <td>
                        <asp:Literal ID="ltlBankName" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>银行卡号:</td>
                    <td>
                        <asp:Literal ID="ltlBankCardId" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>直接领导:</td>
                    <td>
                        <asp:Literal ID="ltlPlanChecks" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>入职时间:</td>
                    <td>
                        <asp:Literal ID="ltlEntryTime" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>转正时间:</td>
                    <td>
                        <asp:Literal ID="ltlPosiTime" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>工号:</td>
                    <td>
                        <asp:Literal ID="ltlWorkNumber" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>头像</td>
                    <td>
                        <asp:Image runat="server" ID="imgPerson" />
                        <asp:LinkButton CssClass="btn btn-primary btn-mini" CommandName="DownLoad1" runat="server" ID="lbtnDownLoad1" Text="下载" OnClick="lbtnDownLoad1_Click" />
                    </td>
                </tr>
                <tr>
                    <td>身份证复印件</td>
                    <td>
                        <asp:Image runat="server" ID="imgCardId" />
                        <asp:LinkButton CssClass="btn btn-primary btn-mini" CommandName="DownLoad2" runat="server" ID="lbtnDownLoad2" Text="下载" OnClick="lbtnDownLoad1_Click" />
                    </td>
                </tr>
                <tr>
                    <td>档案备份</td>
                    <td>名称
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="rptDataFile" OnItemCommand="rptDataFile_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label runat="server" ID="lblDataname" ToolTip='<%#Eval("DataName") %>' Text='<%#GetDataName(Eval("DataName").ToString()) %>' Width="160px" />
                                <asp:TextBox runat="server" ID="txtDataname" oolTip='<%#Eval("DataName") %>' Text='<%#GetDataName(Eval("DataName").ToString()) %>' Width="160px" />
                                <asp:LinkButton runat="server" ID="lbtnDownLoad" Text="下载" CommandName="DownLoad" CommandArgument='<%#Eval("DataId") %>' CssClass="btn btn-primary btn-mini" />
                                <asp:LinkButton runat="server" ID="lbtnDelete" Text="删除" CommandName="Delete" CommandArgument='<%#Eval("DataId") %>' CssClass="btn btn-primary btn-mini" OnClientClick="return confirm('你确定要删除吗?');" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </table>
        </div>
    </div>
</asp:Content>
