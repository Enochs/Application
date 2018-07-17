<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CommandbyAccordSupplierManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.CommandbyAccordSupplierManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable td").css({ "border": "0", "vertical-align": "top" });
            $("html").css({ "overflow-x": "hidden" });



        });
        function matrixingUint(indexs) {
            //计划有效率 转换单位 %
            var valideTargetRate = $("#tbodyContent tr").eq(indexs);
            valideTargetRate.children("td").each(function (indexs, values) {
                if (indexs >= 2 && indexs <= (2 +<%=ViewState["employeeCount"]%> +2)) {
                      $(this).text((parseFloat($(this).text()) * 100).toFixed(2) + " %");
                  }
              });
              }

              //两个数的结果比例换算 first,second,third 这三个参数是当前tbody元素下面 tr 所对应的下标 
              //multiplication 乘以
              function matrixing(first, second, third, multiplication) {

                  var firstTr = $("#tbodyContent tr").eq(first);

                  var secondTr = $("#tbodyContent tr").eq(second);

                  var thridTr = $("#tbodyContent tr").eq(third);
                  firstTr.children("td").each(function (indexs, values) {
                      if (indexs >= 2 && indexs <= (2 +<%=ViewState["employeeCount"]%> +2)) {
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--
        供应商管理按供应商统计分析 指挥台
        --->
    <div class="widget-box">
        <div class="widget-content">
            <div class="widget-box">
                <div class="widget-content">

                    <table class="queryTable">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>供应商</td>

                                        <td>
                                            <asp:TextBox ID="txtName" MaxLength="20" runat="server"></asp:TextBox>
                                        </td>

                                        <td>年份: 
                                         <cc1:ddlRangeYear ID="ddlChooseYear" Width="130" yearStar="2013" yearEnd="2020" runat="server"></cc1:ddlRangeYear>
                                        </td>

                                        <td>月份:
                                            <cc1:ddlMonth ID="ddlChooseMonth" Width="130" runat="server"></cc1:ddlMonth>
                                        </td>
                                        <td></td>
                                        <td>
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

                        <th>供应商名称</th>
                        <th>供应商类别</th>
                        <th>当期供货总次数</th>
                        <th>当期总结算额</th>
                        <th>当期差错次数</th>
                        <th>当期总满意度</th>
                    </tr>
                </thead>
                <tbody id="tbodyContent">

                    <asp:Repeater ID="rptSupplier" runat="server">

                        <ItemTemplate>
                            <tr>
                                <asp:HiddenField ID="hfValue" Value='<%#Eval("SupplierID") %>' runat="server" />
                                <td><%#Eval("Name") %></td>
                                <td><%#GetSupplierTypeName(Eval("CategoryID")) %></td>
                                <td><%#GetDeliveryCount(Eval("SupplierID")) %></td>
                                <td><%#GetRealityCostBySupplierId(Eval("SupplierID")) %></td>
                                <td><%#GetAppraiseBySupplier(Eval("SupplierID")) %></td>
                                <td><%#GetDegreeBySupplier(Eval("SupplierID")) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc2:AspNetPagerTool ID="SupplierPager" AlwaysShow="true" OnPageChanged="SupplierPager_PageChanged" runat="server"></cc2:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>

</asp:Content>
