<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_DeliveryScheduleDetailsList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_DeliveryScheduleDetailsList" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table>
                <tr>
                    <td>类别:</td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rdoType" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoType_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Text="供应商" Value="1" Selected="True" />
                            <asp:ListItem Text="四大金刚" Value="2" />
                            <asp:ListItem Text="内部人员" Value="3" />
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <div runat="server" id="div_Supplier">
                <div class="widget-box" style="height: auto; border: 0px;">
                    <table>
                        <tr>
                            <td>供应商类别：<asp:DropDownList ID="ddlSupplierType" runat="server"></asp:DropDownList></td>
                            <td>供应商名称：<asp:TextBox ID="txtSupplierName" runat="server" MaxLength="20"></asp:TextBox></td>
                            <td>
                                <HA:DateRanger runat="server" ID="DateRanger" Title="婚期：" />
                            </td>
                            <td>
                                <asp:Button ID="BtnQuery" CssClass="btn btn-primary" runat="server" Text="查询" OnClick="BinderData" />
                                <cc2:btnReload runat="server" ID="btnReload" />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnExport" Text="导出" OnClick="lbtnExport_Click" CssClass="btn btn-primary btn-mini" />
                                <%--<asp:LinkButton runat="server" ID="lbtnPrint" Text="打印" OnClick="lbtnPrint_Click" CssClass="btn btn-primary btn-mini"  />--%>
                            </td>
                        </tr>
                    </table>
                </div>

                <!--startprint-->
                <table class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th>类别</th>
                            <th>供应商名称</th>
                            <th>联系人</th>
                            <th>联系电话</th>
                            <th>账户信息</th>
                            <th>供货次数</th>
                            <th>差错次数</th>
                            <th>应付款</th>
                            <th>已付款</th>
                            <th>未付款</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptTestSupplier" runat="server" OnItemDataBound="rptTestSupplier_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td><%#GetSupplierTypeName(Eval("CategoryID")) %></td>
                                    <td><a href="FD_SupplierUpdate.aspx?OnlyView=1&SupplierID=<%#Eval("SupplierID") %>" target="_blank"><%#Eval("Name") %></a></td>
                                    <td><%#Eval("Linkman") %></td>
                                    <td><%#Eval("CellPhone") %></td>
                                    <td><%#Eval("AccountInformation") %></td>
                                    <td><%#GetSupplyCount(Eval("SupplierID"),1) %></td>
                                    <td><%#Eval("ErrorCount") %></a> </td>
                                    <td><%#GetSupplierByName(Eval("SupplierID"),1) %></td>
                                    <td><%#GetSupplierByName(Eval("SupplierID"),2) %></td>
                                    <td><%#GetSupplierByName(Eval("SupplierID"),3) %></td>
                                    <td>
                                        <a href='../../SysReport/StatementManager.aspx?RowType=1&SupplierID=<%#Eval("SupplierID") %>&NeedPopu=1' class="btn btn-primary btn-mini">查看</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>本页合计:</td>
                            <td>
                                <asp:Label runat="server" ID="lblSumTotl" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblPayMent" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblNoPayMent" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>本期合计:</td>
                            <td>
                                <asp:Label runat="server" ID="lblAllSumtotal" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblAllPayMent" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblAllNoPayMent" /></td>
                            <td></td>
                        </tr>
                    </tfoot>
                </table>
                <!--endprint-->
            </div>

            <div runat="server" id="div_Guardian">
                <div class="widget-box" style="height: auto; border: 0px;">
                    <table>
                        <tr>
                            <td>四大金刚类别：<asp:DropDownList ID="ddlGuardianType" runat="server"></asp:DropDownList></td>
                            <td>四大金刚名称：<asp:TextBox ID="txtGuardianName" runat="server" MaxLength="20"></asp:TextBox></td>
                            <td>
                                <HA:DateRanger runat="server" ID="DateRangers" Title="婚期：" />
                            </td>
                            <td>
                                <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="查询" OnClick="BinderData" />
                                <cc2:btnReload runat="server" ID="btnReload1" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnExports" Text="导出" OnClick="lbtnExport_Click" CssClass="btn btn-primary btn-mini" />
                                <%--<asp:LinkButton runat="server" ID="lbtnPrint" Text="打印" OnClick="lbtnPrint_Click" CssClass="btn btn-primary btn-mini" />--%>
                            </td>
                        </tr>
                    </table>
                </div>
                <table class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th>类别</th>
                            <th>四大金刚名称</th>
                            <th>联系人</th>
                            <th>联系电话</th>
                            <th>账户信息</th>
                            <th>供货次数</th>
                            <th>差错次数</th>
                            <th>应付款</th>
                            <th>已付款</th>
                            <th>未付款</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="repGuardian" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#GetGuardianTypeName(Eval("GuardianTypeId")) %></td>
                                    <td><a href='../FD_FourGuardian/FD_FourGuardianUpdate.aspx?GuardianId=<%#Eval("GuardianId") %>' target="_blank"><%#Eval("GuardianName") %></a></td>
                                    <td><%#Eval("GuardianName") %></td>
                                    <td><%#Eval("CellPhone") %></td>
                                    <td><%#Eval("AccountInformation") %></td>
                                    <td><%#GetSupplyCount(Eval("GuardianId"),4) %></td>
                                    <td><%#Eval("ErrorCount") %></a> </td>
                                    <td><%#GetGuardianByName(Eval("GuardianId"),1) %></td>
                                    <td><%#GetGuardianByName(Eval("GuardianId"),2) %></td>
                                    <td><%#GetGuardianByName(Eval("GuardianId"),3) %></td>
                                    <td><a href='../../SysReport/StatementManager.aspx?RowType=4&GuardianID=<%#Eval("GuardianId") %>&NeedPopu=1' class="btn btn-primary btn-mini">查看</a></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>

            <div runat="server" id="div_Employee">
                <div class="widget-box" style="height: auto; border: 0px;">
                    <table>
                        <tr>
                            <td>类别：<asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList></td>
                            <td>四大金刚名称：<asp:TextBox ID="TextBox1" runat="server" MaxLength="20"></asp:TextBox></td>
                            <td>
                                <HA:DateRanger runat="server" ID="DateRanger1" Title="婚期：" />
                            </td>
                            <td>
                                <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="查询" OnClick="BinderData" />
                                <cc2:btnReload runat="server" ID="btnReload2" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton runat="server" ID="LinkButton1" Text="导出" OnClick="lbtnExport_Click" CssClass="btn btn-primary btn-mini" />
                                <%--<asp:LinkButton runat="server" ID="lbtnPrint" Text="打印" OnClick="lbtnPrint_Click" CssClass="btn btn-primary btn-mini" />--%>
                            </td>
                        </tr>
                    </table>
                </div>
                <table class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <%--<th>类别</th>--%>
                            <th>内部人员名称</th>
                            <th>联系人</th>
                            <th>联系电话</th>
                            <th>账户信息</th>
                            <th>供货次数</th>
                            <th>差错次数</th>
                            <th>应付款</th>
                            <th>已付款</th>
                            <th>未付款</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptEmployee" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <%--<td><%#GetGuardianTypeName(Eval("GuardianTypeId")) %></td>--%>
                                    <td><a href='../../Sys/Personnel/Sys_EmployeeDetails.aspx?employeeId=<%#Eval("EmployeeID") %>' target="_blank"><%#Eval("EmployeeName") %></a></td>
                                    <td><%#Eval("EmployeeName") %></td>
                                    <td><%#Eval("CellPhone") %></td>
                                    <td><%#GetBankInfo(Eval("EmployeeID")) %></td>
                                    <td><%#GetSupplyCount(Eval("EmployeeId"),5) %></td>
                                    <td>0</td>
                                    <td><%#GetEmployeeByName(Eval("EmployeeId"),1) %></td>
                                    <td><%#GetEmployeeByName(Eval("EmployeeId"),2) %></td>
                                    <td><%#GetEmployeeByName(Eval("EmployeeId"),3) %></td>
                                    <td><a href='../../SysReport/StatementManager.aspx?RowType=5&EmployeeID=<%#Eval("EmployeeId") %>&NeedPopu=1' class="btn btn-primary btn-mini">查看</a></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
        <cc1:AspNetPagerTool ID="DeliveryPager" PageSize="10" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>
