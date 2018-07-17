<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Sys_EmployeeManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Personnel.Sys_EmployeeManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Scripts/trselection.js"></script>

    <script type="text/javascript">

        function ShowWindowsUpdate(KeyID, Control) {
            var Url = "Sys_EmployeeUpdate.aspx?employeeId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 600, 800, "a#" + $(Control).attr("id"));

        }
        function ShowWindowsDetails(KeyID, Control) {
            var Url = "Sys_EmployeeDetails.aspx?employeeId=" + KeyID;
            $(Control).attr("id", "detailsShow" + KeyID);
            showPopuWindows(Url, 600, 600, "a#" + $(Control).attr("id"));

        }

        function ShowEmployeeAuthorization(KeyID, Control) {

            var Url = "/AdminPanlWorkArea/Sys/Jurisdiction/Sys_EmployeeAuthorization.aspx?employeeId=" + KeyID;
            $(Control).attr("id", "authShow" + KeyID);
            showPopuWindows(Url, 900, 1000, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {

            showPopuWindows($("#createEmployee").attr("href"), 700, 1000, "a#createEmployee");

        });
        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSaveEdit.ClientID%>").click(function () {
                return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(6, 12, '<%=txtPassWord.ClientID%>');
        }
        //上传图片
        function ShowFileUploadPopu(EmployeeID) {
            var Url = "/AdminPanlWorkArea/Sys/Personnel/Sys_EmployeeUpload.aspx?EmployeeID=" + EmployeeID;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <br />
    <a href="#" class="btn btn-mini btn-primary" onclick="window.location.href=document.referrer;return false;">返回</a>
    <a id="createEmployee" <%=SetIsClosedVisibleStyle() %> class="btn btn-primary  btn-mini" href="Sys_EmployeeCreate.aspx?DepartmentID=<%=Request["DepartmentID"] %>">添加新成员</a>
    <asp:LinkButton ID="btnEditAdmin" CssClass="btn btn-primary btn-mini" runat="server" OnClick="btnEditAdmin_Click" Visible="false">修改管理员密码</asp:LinkButton>
    <asp:Label ID="Label1" runat="server" Text="新密码：" Visible="false"></asp:Label><asp:TextBox ID="txtPassWord" check="1" tip="密码为6~12位" runat="server" Visible="false" MaxLength="20"></asp:TextBox>
    <asp:LinkButton ID="btnSaveEdit" CssClass="btn btn-primary btn-mini" runat="server" Visible="false" OnClick="btnSaveEdit_Click">确认修改</asp:LinkButton>
    <div class="widget-box" style="height: 920px; overflow: auto;">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5></h5>
            <span class="label label-info"></span>
        </div>
        <div class="widget-content">
            <table>
                <tr>
                    <td runat="server" id="td_states">员工状态：</td>
                    <td runat="server" id="td_statesVal">
                        <asp:DropDownList runat="server" ID="ddlIsDelete">
                            <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="已删除" Value="1" />
                            <asp:ListItem Text="未删除" Value="2" />
                        </asp:DropDownList>
                    </td>
                    <td>姓名</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtEmployeeName" /></td>
                    <td>部门</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDepartment" /></td>
                    <td>生日</td>
                    <td>
                        <%--<asp:TextBox runat="server" ID="txtBirthday" onclick="WdatePicker();" />--%>
                        <HA:DateRanger runat="server" ID="DateRangerBirtyDay" />
                    </td>
                </tr>
                <tr>
                    <td>入职日期</td>
                    <td colspan="4">
                        <%--<asp:TextBox runat="server" ID="txtEntryTime" onclick="WdatePicker();" />--%>
                        <HA:DateRanger runat="server" ID="DateRangerEntryTime" />
                    </td>
                    <td colspan="2">
                        <asp:Button runat="server" ID="btnLook" Text="查询" CssClass="btn btn-primary" OnClick="btnLook_Click" />
                        <cc2:btnReload runat="server" ID="btnReload" Text="重置条件" />
                    </td>

                </tr>
            </table>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th>员工ID</th>
                        <th>姓名</th>
                        <th>帐号</th>
                        <th>部门</th>
                        <th>职位</th>
                        <th>性别</th>
                        <th>创建时间</th>
                        <th>入职日期</th>
                        <th>生日</th>
                        <th>手机</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptEmployes" runat="server" OnItemDataBound="rptEmployes_ItemDataBound" OnItemCommand="rptEmployes_ItemCommand">
                        <ItemTemplate>
                            <tr skey='<%#Eval("EmployeeID") %>' <%#Eval("IsDelete").ToString()=="True"?"style='color:red;'":"" %>>
                                <td><%#Eval("EmployeeID") %></td>
                                <td><%#Eval("EmployeeName") %></td>
                                <td><%#Eval("LoginName") %></td>
                                <td><%#GetDepartmentNameByID(Eval("DepartmentID")) %></td>
                                <td><%#GetJobName(Eval("JobID")) %></td>
                                <td><%#GetSex(Eval("Sex")) %></td>
                                <td><%#Eval("CreateDate","{0:yyyy-MM-dd}") %></td>
                                <td><%#Eval("EntryTime","{0:yyyy-MM-dd}") %></td>
                                <td><%#Eval("BornDate","{0:yyyy-MM-dd}") %></td>
                                <td><%#Eval("CellPhone") %></td>
                                <td><a href='#' class="btn btn-primary  btn-mini" onclick='ShowWindowsDetails(<%#Eval("EmployeeID") %>,this);'>详细信息</a>
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lkbtnDelete" OnClientClick="$(this).parents('tr').remove();" CommandName="Delete" CommandArgument='<%#Eval("EmployeeID") %>' runat="server">停用</asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lnkbtnOpen" OnClientClick="$(this).parents('tr').remove();" CommandName="Open" CommandArgument='<%#Eval("EmployeeID") %>' runat="server">启用</asp:LinkButton>
                                    <a href='#' class="btn btn-primary  btn-mini" <%=SetIsClosedVisibleStyle() %> onclick="ShowWindowsUpdate(<%#Eval("EmployeeID") %>,this)">修改</a>
                                    <a href='#' class="btn btn-primary  btn-mini" <%=SetIsClosedVisibleStyle() %> onclick="ShowEmployeeAuthorization(<%#Eval("EmployeeID") %>,this)">用户授权</a>
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lnkbtnStar" CommandName="Star" CommandArgument='<%#Eval("EmployeeID") %>' runat="server">权限初始化</asp:LinkButton>
                                    <a class="btn btn-primary  btn-mini" <%=SetIsClosedVisibleStyle() %> href="/AdminPanlWorkArea/Foundation/SysTarget/FL_SetEmployeeTarget.aspx?EmployeeID=<%#Eval("EmployeeID") %>">指标设置</a>
                                    <a href="#" onclick="ShowFileUploadPopu('<%#Eval("EmployeeID") %>')" class="btn btn-mini   btn-primary">上传</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="10">
                            <cc1:AspNetPagerTool ID="CtrPageIndex" OnPageChanged="CtrPageIndex_PageChanged" runat="server" PageSize="20"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Label runat="server" ID="lblEmployeeCount" Style="font-size: 14px; font-weight: bold; color: brown;" />
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>
