<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_TargetByDepartment.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget.FL_TargetByDepartment" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">
    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 20px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
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
    <div class="widget-box">


        <asp:HiddenField ID="hideNeedShow" ClientIDMode="Static" runat="server" />
        <!---策划指挥台  -->
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

                                <asp:ListItem Text="2013年" Value="2013-01-01,2013-12-31"></asp:ListItem>
                                <asp:ListItem Text="2014年" Value="2014-01-01,2014-12-31"></asp:ListItem>
                                <asp:ListItem Text="2015年" Value="2015-01-01,2015-12-31"></asp:ListItem>
                                <asp:ListItem Text="2016年" Value="2016-01-01,2016-12-31"></asp:ListItem>
                                <asp:ListItem Text="2017年" Value="2017-01-01,2017-12-31"></asp:ListItem>
                                <asp:ListItem Text="2018年" Value="2018-01-01,2018-12-31"></asp:ListItem>
                                <asp:ListItem Text="2019年" Value="2019-01-01,2019-12-31"></asp:ListItem>
                                <asp:ListItem Text="2020年" Value="2020-01-01,2020-12-31"></asp:ListItem>
                                <asp:ListItem Text="2021年" Value="2021-01-01,2021-12-31"></asp:ListItem>
                                <asp:ListItem Text="2022年" Value="2022-01-01,2022-12-31"></asp:ListItem>
                                <asp:ListItem Text="2023年" Value="2023-01-01,2023-12-31"></asp:ListItem>
                                <asp:ListItem Text="2024年" Value="2024-01-01,2024-12-31"></asp:ListItem>
                            </asp:DropDownList>
                            </td>

                            <td>
                                <asp:Button ID="btnQuery" Height="27" CssClass="btn btn-primary" OnClick="btnQuery_Click" runat="server" Text="查询" />

                            </td>
                        </tr>

                    </table>

                </td>
            </tr>
        </table>



        <asp:LinkButton ID="btnSerchEmployee" runat="server" CssClass="btn btn-primary btn-mini NeedHide" OnClick="btnSerchEmployee_Click">查看部门员工指标</asp:LinkButton>
        <asp:Label ID="lblEmpLoyeeName" runat="server" Text="Label"></asp:Label>
        <table class="table table-bordered table-striped">
            <thead>
                <tr id="trContent">
                    <th>目标</th>
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
                <asp:Repeater ID="repList" runat="server" OnItemDataBound="repList_ItemDataBound">
                    <ItemTemplate>
                        <tr id="trContent">
                            <td><%#Eval("TargetTitle") %></td>
                            <td>计划</td>
                            <td><%#Eval("MonthPlan1") %></td>
                            <td><%#Eval("MonthPlan2") %></td>
                            <td><%#Eval("MonthPlan3") %></td>
                            <td><%#Eval("MonthPlan4") %></td>
                            <td><%#Eval("MonthPlan5") %></td>
                            <td><%#Eval("MonthPlan6") %></td>
                            <td><%#Eval("MonthPlan7") %></td>
                            <td><%#Eval("MonthPlan8") %></td>
                            <td><%#Eval("MonthPlan9") %></td>
                            <td><%#Eval("MonthPlan10") %></td>
                            <td><%#Eval("MonthPlan11") %></td>
                            <td><%#Eval("MonthPlan12") %></td>
                            <td><asp:Label runat="server" ID="lblPanSum" Text="" /></td>
                        </tr>
                        <tr id="tr1">
                            <th style="border-bottom-style: none; border-top-style: none"></th>
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
                            <td><asp:Label runat="server" ID="lblFinishSum" Text="" /></td>
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
                            <td><asp:Label runat="server" ID="lblFinishRates" Text="" /></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </thead>
            <tfoot>
                <tr>
                    <td colspan="17">
                        <a href="FL_SetFinishTargetPlan.aspx" class="btn btn-info" style="display: block; height: 25px; width: 100px;">设定计划目标</a>
                    </td>
                </tr>
            </tfoot>
        </table>

    </div>


</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .btn {
            height: 21px;
        }
    </style>
</asp:Content>
