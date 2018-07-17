<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDirectShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.OrderDirectShow" Title="填写成本明细" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">

    <script type="text/javascript">
        function HideCtable(ControlID) {
            $(".WorkTable").show();
            $(ControlID).hidden();

        }

    </script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <br />

    <asp:Repeater ID="repEmpLoyeeCostList" runat="server">
        <HeaderTemplate>
            <table style="width: 100%;" id="Emplpyeetable" class="WorkTable table table-bordered table-striped">

                <tr>
                    <td>人员姓名</td>

                    <td>评价</td>

                    <td>支出科目</td>

                    <td>金额</td>


                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                <td>
                    <%#Eval("InsideRemark") %></td>
                <td>
                    <%#Eval("AccountClass") %></td>
                <td>
                    <%#Eval("Cost") %></td>

            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>

        </FooterTemplate>
    </asp:Repeater>
    <asp:Repeater ID="repTypeList" runat="server" OnItemDataBound="repTypeList_ItemDataBound" OnItemCommand="repTypeList_ItemCommand">
        <ItemTemplate>
            供应商:<%#Eval("KeyName") %><asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("Key") %>' />
            <table class="table table-bordered table-striped">
                <tr>
                    <th width="100">类别</th>
                    <th width="100">项目</th>

                    <td>扣款说明</td>
                    <th>计划支出</th>
                    <th>实际支出</th>
                </tr>
                <asp:Repeater ID="repProductList" runat="server">
                    <ItemTemplate>
                        <tr <%#GetBorderStyle(Eval("NewAdd")) %>>
                            <td><%#Eval("ParentCategoryName") %></td>
                            <td><%#Eval("CategoryName") %></td>

                            <td>
                                <%#Eval("InsideRemark") %></td>
                            <td>
                                <%#Eval("PlanCost") %></td>
                            <td>
                                <%#Eval("RealityCost") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </table>


        </ItemTemplate>
    </asp:Repeater>

    <table>
        <tr>
            <td>合计
            </td>
            <td>
                &nbsp;</td>
            <td>利润率
             
               
            </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
</asp:Content>
