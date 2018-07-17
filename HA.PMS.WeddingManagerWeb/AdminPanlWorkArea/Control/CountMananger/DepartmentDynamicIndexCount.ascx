<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DepartmentDynamicIndexCount.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CountMananger.DepartmentDynamicIndexCount" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
 <script type="text/javascript">
     $(document).ready(function () {
         $("#trContent th").css({ "white-space": "nowrap" });
         $(".queryTable td").css({ "border": "0", "vertical-align": "top" });
         $("html").css({ "overflow-x": "hidden" });

     });
     //两个数的结果比例换算 first,second,third 这三个参数是当前tbody元素下面 tr 所对应的下标 
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

                    <table class="queryTable">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>年份: 
                                         <cc1:ddlRangeYear ID="ddlChooseYear" Width="130" yearStar="2013" yearEnd="2020" runat="server"></cc1:ddlRangeYear>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnQuery" CssClass="btn" OnClick="btnQuery_Click" runat="server" Text="查询" />

                                        </td>
                                    </tr>

                                </table>

                            </td>
                        </tr>
                    </table>
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
                    <asp:Repeater ID="rptDepart" OnItemDataBound="rptDepart_ItemDataBound" runat="server">

                        <ItemTemplate>
                            <asp:HiddenField ID="hfDepartId" Value='<%#Eval("DepartmentID") %>' runat="server" />
                            
                            
                                <tr>
                                    <td><%#Eval("DepartmentName") %></td>
                                    <td>计划</td>
                                    <asp:PlaceHolder ID="phTarget" runat="server">
                                        <asp:Literal ID="ltlTargetContent" runat="server"></asp:Literal>
                                    </asp:PlaceHolder>
                                </tr>
                            
                           
                                 <tr>
                                    <td></td>
                                    <td>实际完成</td>
                                    <asp:PlaceHolder ID="phReality" runat="server">
                                        <asp:Literal ID="ltlReality" runat="server"></asp:Literal>
                                     </asp:PlaceHolder>
                                </tr>

                                 <tr>
                                    <td></td>
                                    <td>完成率</td>
                                    <asp:PlaceHolder ID="phRate" runat="server">
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
                                     </asp:PlaceHolder>
                                </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            </div>
            </div>
      </div>

