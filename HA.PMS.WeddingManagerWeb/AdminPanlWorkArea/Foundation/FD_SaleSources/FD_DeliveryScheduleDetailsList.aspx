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
        <div class="widget-box" style="width: 99%;">
            <table>
                <tr>
                    <td>类别:</td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rdoType" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoType_SelectedIndexChanged" AutoPostBack="True" CellPadding="10" CellSpacing="10">
                            <asp:ListItem Text="供应商" Value="1" Selected="True" />
                            <asp:ListItem Text="四大金刚" Value="2" />
                            <asp:ListItem Text="内部人员" Value="3" />
                            <asp:ListItem Text="外部人员" Value="4" />
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <!---供应商-->
            <div runat="server" id="div_Supplier" style="max-height: 720px; overflow: auto;">
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
                <table class="table table-bordered table-striped" style="width: 98%;">
                    <thead>
                        <tr>
                            <th>类别</th>
                            <th>供应商名称</th>
                            <th>联系人</th>
                            <th>联系电话</th>
                            <th>账户信息</th>
                            <th>供货次数</th>
                            <%--<th>差错次数</th>--%>
                            <th>结算金额</th>
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
                                    <td><%#GetBankInfo(1,Eval("SupplierID"),1) %></td>
                                    <td><%#GetSupplyCount(Eval("SupplierID"),1) %>
                                    </td>
                                    <%--<td><%#Eval("ErrorCount") %></a> </td>--%>
                                    <td><%#GetSupplierByName(Eval("SupplierID"),1) %></td>
                                    <td><%#GetSupplierByName(Eval("SupplierID"),2) %></td>
                                    <td><%#GetSupplierByName(Eval("SupplierID"),3) %></td>
                                    <td>
                                        <a href='../../SysReport/StatementManager.aspx?RowType=1&SupplierID=<%#Eval("SupplierID") %>&DateRanger=<%=GetStartToEnd() %>&NeedPopu=1' class="btn btn-primary btn-mini">查看</a>
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

            <!---四大金刚-->
            <div runat="server" id="div_Guardian" style="max-height: 720px; overflow: auto;">
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
                <table class="table table-bordered table-striped" style="width: 98%;">
                    <thead>
                        <tr>
                            <th>类别</th>
                            <th>四大金刚名称</th>
                            <th>联系人</th>
                            <th>联系电话</th>
                            <th>账户信息</th>
                            <th>供货次数</th>
                            <%--<th>差错次数</th>--%>
                            <th>应付款</th>
                            <th>已付款</th>
                            <th>未付款</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="repGuardian" runat="server" OnItemDataBound="rptTestSupplier_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td><%#GetGuardianTypeName(Eval("GuardianTypeId")) %></td>
                                    <td><a href='../FD_FourGuardian/FD_FourGuardianUpdate.aspx?GuardianId=<%#Eval("GuardianId") %>' target="_blank"><%#Eval("GuardianName") %></a></td>
                                    <td><%#Eval("GuardianName") %></td>
                                    <td><%#Eval("CellPhone") %></td>
                                    <td><%#GetBankInfo(4,Eval("GuardianId"),1) %></td>
                                    <td><%#GetSupplyCount(Eval("GuardianId"),4) %></td>
                                    <%--<td><%#Eval("ErrorCount") %></a> </td>--%>
                                    <td><%#GetGuardianByName(Eval("GuardianId"),1) %></td>
                                    <td><%#GetGuardianByName(Eval("GuardianId"),2) %></td>
                                    <td><%#GetGuardianByName(Eval("GuardianId"),3) %></td>
                                    <td><a href='../../SysReport/StatementManager.aspx?RowType=4&GuardianID=<%#Eval("GuardianId") %>&NeedPopu=1' class="btn btn-primary btn-mini">查看</a></td>
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
                            <td>本页合计:</td>
                            <td>
                                <asp:Label runat="server" ID="lblGuardPageSumTotal" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblGuardPagePayMent" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblGuardPageNoPayMent" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>本期合计:</td>
                            <td>
                                <asp:Label runat="server" ID="lblGuardSumTotal" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblGuardPayMent" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblGuardNoPayMent" /></td>
                            <td></td>
                        </tr>
                    </tfoot>
                </table>
            </div>

            <!---内部人员-->
            <div runat="server" id="div_Employee" style="max-height: 720px; overflow: auto;">
                <div class="widget-box" style="height: auto; border: 0px;">
                    <table>
                        <tr>
                            <td>姓名：<asp:TextBox ID="txtEmployeeName" runat="server" MaxLength="20"></asp:TextBox></td>
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
                <table class="table table-bordered table-striped" style="width: 98%;">
                    <thead>
                        <tr>
                            <%--<th>类别</th>--%>
                            <th>内部人员名称</th>
                            <th>联系人</th>
                            <th>联系电话</th>
                            <th>账户信息</th>
                            <th>供货次数</th>
                            <%--<th>差错次数</th>--%>
                            <th>应付款</th>
                            <th>已付款</th>
                            <th>未付款</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptEmployee" runat="server" OnItemDataBound="rptTestSupplier_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <%--<td><%#GetGuardianTypeName(Eval("GuardianTypeId")) %></td>--%>
                                    <td><a href='../../Sys/Personnel/Sys_EmployeeDetails.aspx?employeeId=<%#Eval("EmployeeID") %>' target="_blank"><%#Eval("EmployeeName") %></a></td>
                                    <td><%#Eval("EmployeeName") %></td>
                                    <td><%#Eval("CellPhone") %></td>
                                    <td><%#GetBankInfo(5,Eval("EmployeeID"),1) %></td>
                                    <td><%#GetSupplyCount(Eval("EmployeeId"),5) %></td>
                                    <%--<td>0</td>--%>
                                    <td><%#GetEmployeeByName(Eval("EmployeeId"),1) %></td>
                                    <td><%#GetEmployeeByName(Eval("EmployeeId"),2) %></td>
                                    <td><%#GetEmployeeByName(Eval("EmployeeId"),3) %></td>
                                    <td><a href='../../SysReport/StatementManager.aspx?RowType=5&EmployeeID=<%#Eval("EmployeeId") %>&NeedPopu=1' class="btn btn-primary btn-mini">查看</a></td>
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
                            <td>本页合计:</td>
                            <td>
                                <asp:Label runat="server" ID="lblEmpPageSumTotal" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblEmpPagePayMent" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblEmpPageNoPayMent" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>本期合计:</td>
                            <td>
                                <asp:Label runat="server" ID="lblEmployeeSumTotal" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblEmployeePayMent" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblEmployeeNoPayMent" /></td>
                            <td></td>
                        </tr>
                    </tfoot>
                </table>
            </div>

            <!---外部人员-->
            <div runat="server" id="div_OutPerson" style="max-height: 720px; overflow: auto;">
                <div class="widget-box" style="height: auto; border: 0px;">
                    <table>
                        <tr>
                            <td>姓名：<asp:TextBox ID="txtOutPersonName" runat="server" MaxLength="20"></asp:TextBox></td>
                            <td>
                                <HA:DateRanger runat="server" ID="DateRanger2" Title="婚期：" />
                            </td>
                            <td>
                                <asp:Button ID="Button3" CssClass="btn btn-primary" runat="server" Text="查询" OnClick="BinderData" />
                                <cc2:btnReload runat="server" ID="btnReload3" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:LinkButton runat="server" ID="lbtnOutExport" Text="导出" OnClick="lbtnExport_Click" CssClass="btn btn-primary btn-mini" />
                                <%--<asp:LinkButton runat="server" ID="lbtnPrint" Text="打印" OnClick="lbtnPrint_Click" CssClass="btn btn-primary btn-mini" />--%>
                            </td>
                        </tr>
                    </table>
                </div>
                <table class="table table-bordered table-striped" style="width: 98%;">
                    <thead>
                        <tr>
                            <th>外部人员名称</th>
                            <th>联系人</th>
                            <th>联系电话</th>
                            <th>账户信息</th>
                            <th>供货次数</th>
                            <th>应付款</th>
                            <th>已付款</th>
                            <th>未付款</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="retOutPerson" runat="server" OnItemDataBound="rptTestSupplier_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("Name") %></td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td>0</td>
                                    <td><%#Eval("SumTotal") %></td>
                                    <td><%#Eval("PayMent").ToString().ToDecimal()+Eval("PrePayMent").ToString().ToDecimal() %></td>
                                    <td><%#Eval("NoPayMent") %></td>
                                    <td><a href='../../SysReport/StatementManager.aspx?RowType=13&Name=<%#Eval("Name") %>&NeedPopu=1' class="btn btn-primary btn-mini">查看</a></td>
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
                            <td>本页合计:</td>
                            <td>
                                <asp:Label runat="server" ID="lblOutPageSumTotal" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblOutPageOutPayMent" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblOutPageNoPayMent" /></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>本期合计:</td>
                            <td>
                                <asp:Label runat="server" ID="lblOutSumTotal" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblOutPayMent" /></td>
                            <td>
                                <asp:Label runat="server" ID="lblOutNoPayMent" /></td>
                            <td></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
        <cc1:AspNetPagerTool ID="DeliveryPager" PageSize="10" AlwaysShow="true" OnPageChanged="BinderData" runat="server"></cc1:AspNetPagerTool>
        <asp:Label runat="server" ID="lblSupplierSum" Style="font-size: 13px; color: red;" />
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>
