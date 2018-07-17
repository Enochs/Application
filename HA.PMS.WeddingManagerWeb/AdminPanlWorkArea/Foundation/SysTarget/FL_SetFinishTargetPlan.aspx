<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FL_SetFinishTargetPlan.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget.FL_SetFinishTargetPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
    <%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
    <style>
        .txtPlan {
            width:50px;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            if ($("#hideNeedShow").val() == "1") {

                $(".NeedHide").hide();

            }

            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {

                if (indexs != 5) {
                    $(this).css({
                        "border": "0", "vertical-align": "middle"

                    }).after("");
                }

                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });
            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");
        });
        $("html,body").css({ "background-color": "transparent" });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table class="queryTable">
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>部门
                            </td>
                            <td>
                                <cc1:DepartmentDropdownList ID="ddlDepartment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                </cc1:DepartmentDropdownList>
                            </td>
                            <td>员工</td>
                            <td>
                                <cc1:ddlEmployee ID="ddlEmployee1" runat="server">
                                </cc1:ddlEmployee></td>
                            <td>&nbsp;&nbsp; 年份<asp:DropDownList ID="ddlChooseYear" Width="130" runat="server">

                                <asp:ListItem Text="2013" Value="2013-01-01,2013-12-31"></asp:ListItem>
                                <asp:ListItem Text="2014" Value="2014-01-01,2014-12-31"></asp:ListItem>
                                <asp:ListItem Text="2015" Value="2015-01-01,2015-12-31"></asp:ListItem>
                                <asp:ListItem Text="2016" Value="2016-01-01,2016-12-31"></asp:ListItem>
                                <asp:ListItem Text="2017" Value="2017-01-01,2017-12-31"></asp:ListItem>
                                <asp:ListItem Text="2018" Value="2018-01-01,2018-12-31"></asp:ListItem>
                                <asp:ListItem Text="2019" Value="2019-01-01,2019-12-31"></asp:ListItem>
                                <asp:ListItem Text="2020" Value="2020-01-01,2020-12-31"></asp:ListItem>
                                <asp:ListItem Text="2021" Value="2021-01-01,2021-12-31"></asp:ListItem>
                                <asp:ListItem Text="2022" Value="2022-01-01,2022-12-31"></asp:ListItem>
                                <asp:ListItem Text="2023" Value="2023-01-01,2023-12-31"></asp:ListItem>
                                <asp:ListItem Text="2024" Value="2024-01-01,2024-12-31"></asp:ListItem>
                            </asp:DropDownList>
                            </td>

                            <td>
                                <asp:Button ID="btnQuery" Height="27" CssClass="btn btn-primary" OnClick="btnQuery_Click" runat="server" Text="查询" />
                                <cc1:btnReload runat="server" ID="BtnReload" />
                            </td>
                        </tr>

                    </table>

                </td>
            </tr>
        </table>

        <table class="table table-bordered table-striped">
            <thead>
                <tr id="trContent">
                    <th>计划目标</th>
                    <th></th>
                    <th>1</th>
                    <th>2</th>
                    <th>3</th>
                    <th>4</th>
                    <th>5</th>
                    <th>6</th>
                    <th>7</th>
                    <th>8</th>
                    <th>9</th>
                    <th>10</th>
                    <th>11</th>
                    <th>12</th>
                    <th>当年合计</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater runat="server" ID="rptTargetFinish" OnItemCommand="rptTargetFinish_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("TargetTitle") %>
                                <asp:HiddenField runat="server" ID="HideKey" Value='<%#Eval("FinishKey") %>' />
                            </td>
                            <td>计划</td>
                            <td>
                                <asp:TextBox runat="server" CssClass="txtPlan" ID="txtTargetPlan1" Text='<%#Eval("MonthPlan1") %>' /></td>
                            <td>
                                <asp:TextBox runat="server" CssClass="txtPlan" ID="txtTargetPlan2" Text='<%#Eval("MonthPlan2") %>'/></td>
                            <td>
                                <asp:TextBox runat="server" CssClass="txtPlan" ID="txtTargetPlan3" Text='<%#Eval("MonthPlan3") %>'/></td>
                            <td>
                                <asp:TextBox runat="server" CssClass="txtPlan" ID="txtTargetPlan4" Text='<%#Eval("MonthPlan4") %>' /></td>
                            <td>
                                <asp:TextBox runat="server" CssClass="txtPlan" ID="txtTargetPlan5" Text='<%#Eval("MonthPlan5") %>'/></td>
                            <td>
                                <asp:TextBox runat="server" CssClass="txtPlan" ID="txtTargetPlan6" Text='<%#Eval("MonthPlan6") %>'/></td>
                            <td>
                                <asp:TextBox runat="server" CssClass="txtPlan" ID="txtTargetPlan7" Text='<%#Eval("MonthPlan7") %>' /></td>
                            <td>
                                <asp:TextBox runat="server" CssClass="txtPlan" ID="txtTargetPlan8" Text='<%#Eval("MonthPlan8") %>' /></td>
                            <td>
                                <asp:TextBox runat="server" CssClass="txtPlan" ID="txtTargetPlan9" Text='<%#Eval("MonthPlan9") %>' /></td>
                            <td>
                                <asp:TextBox runat="server" CssClass="txtPlan" ID="txtTargetPlan10" Text='<%#Eval("MonthPlan10") %>' /></td>
                            <td>
                                <asp:TextBox runat="server" CssClass="txtPlan" ID="txtTargetPlan11" Text='<%#Eval("MonthPlan11") %>' /></td>
                            <td>
                                <asp:TextBox runat="server" CssClass="txtPlan" ID="txtTargetPlan12" Text='<%#Eval("MonthPlan12") %>' /></td>
                            <td>
                                <asp:Label runat="server" CssClass="lblPlan" ID="lblYearPlanSum" />
                            </td>
                            <td>
                                <asp:Button runat="server" ID="btnSave" Text="保存" CommandName="Save" CommandArgument='<%#Eval("FinishKey") %>' CssClass="btn btn-primary" /></td>
                        </tr>
                        <tr id="tr1">
                            <th style="border-bottom-style: none; border-top-style: none">(<%#GetEmployeeName(Eval("EmployeeID")) %>)</th>
                            <td>实际完成</td>
                            <td><%#Eval("MonthFinsh1") %></td>
                            <td><%#Eval("MonthFinish2") %></td>
                            <td><%#Eval("MonthFinish3") %></td>
                            <td><%#Eval("MonthFinish4") %></td>
                            <td><%#Eval("MonthFinish5") %></td>
                            <td><%#Eval("MonthFinish6") %></td>
                            <td><%#Eval("MonthFinish7") %></td>
                            <td><%#Eval("MonthFinish8") %></td>
                            <td><%#Eval("MonthFinish9") %></td>
                            <td><%#Eval("MonthFinish10") %></td>
                            <td><%#Eval("MonthFinish11") %></td>
                            <td><%#Eval("MonthFinish12") %></td>
                            <td><asp:Label runat="server" ID="lblFinishYear" /></td>
                            <td></td>

                        </tr>
                        <tr id="tr2">
                            <td style="border-bottom-style: none; border-top-style: none"></td>
                            <td>完成率</td>
                            <td><%#GetFiinishAvg(Eval("MonthPlan1"),Eval("MonthFinsh1")) %></td>
                            <td><%#GetFiinishAvg(Eval("MonthPlan2"),Eval("MonthFinish2")) %></td>
                            <td><%#GetFiinishAvg(Eval("MonthPlan3"),Eval("MonthFinish3")) %></td>
                            <td><%#GetFiinishAvg(Eval("MonthPlan4"),Eval("MonthFinish4")) %></td>
                            <td><%#GetFiinishAvg(Eval("MonthPlan5"),Eval("MonthFinish5")) %></td>
                            <td><%#GetFiinishAvg(Eval("MonthPlan6"),Eval("MonthFinish6")) %></td>
                            <td><%#GetFiinishAvg(Eval("MonthPlan7"),Eval("MonthFinish7")) %></td>
                            <td><%#GetFiinishAvg(Eval("MonthPlan8"),Eval("MonthFinish8")) %></td>
                            <td><%#GetFiinishAvg(Eval("MonthPlan9"),Eval("MonthFinish9")) %></td>
                            <td><%#GetFiinishAvg(Eval("MonthPlan10"),Eval("MonthFinish10")) %></td>
                            <td><%#GetFiinishAvg(Eval("MonthPlan11"),Eval("MonthFinish11")) %></td>
                            <td><%#GetFiinishAvg(Eval("MonthPlan12"),Eval("MonthFinish12")) %></td>
                            <td><asp:Label runat="server" ID="lblFinishRates"  Text="" /></td>
                            <td>&nbsp;</td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="16">
                        <cc1:AspNetPagerTool ID="CtrPageIndex" OnPageChanged="CtrPageIndex_PageChanged" runat="server" PageSize="10" CustomInfoHTML="那又怎么样"></cc1:AspNetPagerTool>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>
