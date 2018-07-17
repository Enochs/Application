<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Targetfinish.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.Targetfinish" %>
<script>

    function HideTarget(Control) {
        var TagetValue = $(Control).attr("TagetValue");
        if (TagetValue == "Show") {
            $(".TargetDiv").hide();
            $(Control).attr("TagetValue", "Hide");

        } else {
            $(".TargetDiv").show();
            $(Control).attr("TagetValue", "Show");
        }
    }
</script>
<div class="span7">
    <div class="widget-box">
        <div class="widget-title" tagetvalue="Show" onclick="HideTarget(this);" title="点击此处隐藏\显示目标" style="cursor: help;">
            <span class="icon"><i class="icon-ok"></i></span>
            <h5>关键指标：<asp:Label ID="lblTargetName" runat="server" Text=""></asp:Label>
            </h5>
        </div>
        <div class="widget-content nopadding TargetDiv">
            <table class="table table-bordered table-striped">
                <thead>
                    <tr style="text-align: center;">
                        <th>
                            <div>目标</div>
                        </th>
                        <th>
                            <%--<table>
                                <tr>
                                    <td>月</td>
                                    <td>
                                        <select runat="server" id="Option" style="border: 0px solid white; width: 25px; margin-top: 20px;">
                                            <option>1</option>
                                            <option>2</option>
                                            <option>3</option>
                                            <option>4</option>
                                            <option>5</option>
                                            <option>6</option>
                                            <option>7</option>
                                            <option>8</option>
                                            <option>9</option>
                                            <option>10</option>
                                            <option>11</option>
                                            <option>12</option>
                                        </select></td>
                                    </tr>
                            </table>--%>
                            <div>
                                <asp:DropDownList runat="server" ID="ddlMonthSelect" OnSelectedIndexChanged="ddlMonthSelect_SelectedIndexChanged" Style="border: 0px solid white; width: 55px; background-color: #EFEFEF; margin-top: 10px;" cellpading="0" cellspacing="0" AutoPostBack="true">
                                    <asp:ListItem Text="1" Value="1" />
                                    <asp:ListItem Text="2" Value="2" />
                                    <asp:ListItem Text="3" Value="3" />
                                    <asp:ListItem Text="4" Value="4" />
                                    <asp:ListItem Text="5" Value="5" />
                                    <asp:ListItem Text="6" Value="6" />
                                    <asp:ListItem Text="7" Value="7" />
                                    <asp:ListItem Text="8" Value="8" />
                                    <asp:ListItem Text="9" Value="9" />
                                    <asp:ListItem Text="10" Value="10" />
                                    <asp:ListItem Text="11" Value="11" />
                                    <asp:ListItem Text="12" Value="12" />
                                </asp:DropDownList>
                                月
                            </div>
                        </th>
                        <th>
                            <div>季度</div>
                        </th>
                        <th>
                            <div>年度</div>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="odd gradeX">
                        <td style="text-align: center;">计划目标</td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblMonthPlan" runat="server" Text=""></asp:Label></td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblQPlan" runat="server" Text=""></asp:Label></td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblYearPlan" runat="server" Text=""></asp:Label></td>
                    </tr>

                    <tr class="odd gradeA">
                        <td style="text-align: center;">实际完成</td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblMonthFinish" runat="server" Text=""></asp:Label></td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblQfinish" runat="server" Text=""></asp:Label></td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblYearfinish" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr class="odd gradeA">
                        <td style="text-align: center;">完成率</td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblMonth" runat="server" Text="暂无数据"></asp:Label></td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblQ" runat="server" Text="暂无数据"></asp:Label></td>
                        <td style="text-align: center;">
                            <asp:Label ID="lblYearQ" runat="server" Text="暂无数据"></asp:Label></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</div>
