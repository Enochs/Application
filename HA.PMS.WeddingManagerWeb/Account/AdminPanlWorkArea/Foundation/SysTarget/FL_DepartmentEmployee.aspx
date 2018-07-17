<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FL_DepartmentEmployee.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget.FL_DepartmentEmployee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">

 
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>姓名
                        </th>

                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptEmployes" runat="server">

                        <ItemTemplate>

                            <tr>
                                <td><%#Eval("EmployeeName") %></td>

                                <td>
                                    <a href='FL_TargetByDepartment.aspx?EmployeeID=<%#Eval("EmployeeID") %>' class="btn btn-primary  btn-mini">查看指标完成情况</a>

                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
   
</asp:Content>
