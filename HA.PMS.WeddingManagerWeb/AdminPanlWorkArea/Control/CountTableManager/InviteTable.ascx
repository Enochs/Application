<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InviteTable.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountTableManager.InviteTable" %>
<script type="text/javascript">
    $(document).ready(function () {
        $("#trContent th").css({ "white-space": "nowrap" });
        $(".queryTable td").css({ "border": "0", "vertical-align": "top" });
        $("html").css({ "overflow-x": "hidden" });
        //邀约成功率
        matrixing(0, 1, 2, 1);
        //成功 实际完成率
        matrixing(6, 1, 4, 1);

        //成功 完成率
        matrixing(3, 4, 5, 100);

        //有效率
        matrixing(6, 8, 9, 1);
        //邀约中比例
        matrixing(6, 10, 11, 1);
        //流失比例
        matrixing(6, 12, 13, 1);
        //14  6  15  1
        //未邀约比例
        matrixing(6, 14, 15, 1);
    });
    //两个数的结果比例换算 first,second,third 这三个参数是当前tbody元素下面 tr 所对应的下标 
    // 第一个参数是 第二个除数     第二个参数是第一个除数  
    //multiplication 乘以
    function matrixing(first, second, third, multiplication) {
        var firstTr = $("#tbodyContent tr").eq(first);

        var secondTr = $("#tbodyContent tr").eq(second);

        var thridTr = $("#tbodyContent tr").eq(third);
        firstTr.children("td").each(function (indexs, values) {
            if (indexs >= 2 && indexs <= 16) {
                //当前月份计划完成目标
                var targetMonth = parseFloat($(this).text()) * multiplication;
                var validMonth = parseFloat(secondTr.children("td").eq(indexs).text()) * multiplication;
                if (targetMonth == 0) {
                    thridTr.children("td").eq(indexs).text("0 %");
                } else {
                    var result = (validMonth / targetMonth).toFixed(2);
                    if (result >= 1) {
                        thridTr.children("td").eq(indexs).text((result * 100).toFixed(2) + " %");
                    } else {
                        thridTr.children("td").eq(indexs).text(result + " %");
                    }

                }

            }
        });
    }
    </script>

    <div class="widget-box">
        <div class="widget-content">
            <div class="widget-box">
                <div class="widget-content">
                    <!---邀约指挥台  -->
                    <table class="queryTable">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>部门： 
                                         <asp:DropDownList ID="ddlDepartment" Width="130" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>年份:<asp:DropDownList ID="ddlChooseYear" Width="130" runat="server">
 
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
                                            
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Button ID="btnQuery" CssClass="btn btn-primary" OnClick="btnQuery_Click" runat="server" Text="查询" />

                                        </td>
                                    </tr>

                                </table>

                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <table class="table table-bordered table-striped">
                <thead>
                    <tr id="trContent">
                        <th></th>
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
                        <th>上年合计</th>
                        <th>历史累计</th>

                    </tr>
                </thead>
                <tbody id="tbodyContent">
                     <tr>
                        <td>邀约成功数</td>
                        <td>计划</td>
                        <%=ViewState["TargetInviteSuccess"] %>

                    </tr>
                      <tr>
                        <td>&nbsp;</td>
                        <td>实际完成</td>
                       <%=ViewState["sbInviteSuccess"] %>
                    </tr>
                      <tr>
                        <td>&nbsp;</td>
                        <td>完成率</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                      <tr>
                        <td>成功率</td>
                        <td>计划</td>
                      <%= ViewState["TargetInviteRate"] %> 

                    </tr>
                        <tr>
                        <td>&nbsp;</td>
                        <td>实际完成</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                      <tr>
                        <td>&nbsp;</td>
                        <td>完成率</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                      <tr>
                        <td>客源总数</td>
                        <td>&nbsp;</td>
                        <%=ViewState["sbCustomer"] %>

                    </tr>
                      <tr>
                        <td>订单总数</td>
                        <td>&nbsp;</td>
                        <%= ViewState["sbOrder"] %>

                    </tr>
                        <tr>
                        <td>有效信息数</td>
                        <td>&nbsp;</td>
                        <%=ViewState["sbValid"] %>

                    </tr>
                      <tr>
                        <td>有效率</td>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                      <tr>
                        <td>邀约中数</td>
                        <td>&nbsp;</td>
                       <%=ViewState["sbOngoing"]  %>

                    </tr>
                      <tr>
                        <td >比例</td>
                        <td >&nbsp;</td>
                        <td ></td>
                        <td ></td>
                        <td ></td>
                        <td ></td>
                        <td ></td>
                        <td ></td>
                        <td ></td>
                        <td ></td>
                        <td ></td>
                        <td ></td>
                        <td ></td>
                        <td ></td>
                        <td ></td>
                        <td ></td>
                        <td ></td>

                    </tr>
                        <tr>
                        <td>流失数</td>
                        <td>&nbsp;</td>
                         <%= ViewState["sbLose"]  %>

                    </tr>
                      <tr>
                        <td>流失率</td>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                      <tr>
                        <td>未邀约数</td>
                        <td>&nbsp;</td>
                        <%=ViewState["sbNot"] %>

                    </tr>
                      <tr>
                        <td>比例</td>
                        <td>&nbsp;</td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                      
                </tbody>
            </table>
            </div>
         </div>
    
