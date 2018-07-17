<%@ Page Language="C#" StylesheetTheme="Default" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Test" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head id="Head1" runat="server">
    <title>HoldLove Admin</title>
    <script src="../Scripts/jquery.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".txtNums").change(function () {
                var num = $("#txtNum").attr("value");
                alert(num);
            });
        });

        function test() {
            var quantity = $("#lblQuantity").text();
            alert($("#lblQuantity").text() + "---" + $("#txtNum").attr("value"));
        }
    </script>

</head>

<body>
    <form runat="server" id="form1">
        <div style="margin-top: 20px; margin-left: 25px;">
            <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
            <br />
            <asp:Button runat="server" ID="btnFull" Text="全拼" OnClick="btnFull_Click" CssClass="btn btn-primary" />
            <asp:Button runat="server" ID="btnShou" Text="首写" OnClick="btnShou_Click" CssClass="btn btn-primary" />
            <asp:Button runat="server" ID="BtnFirstLetter" Text="首字母" OnClick="BtnFirstLetter_Click" CssClass="btn btn-primary" />
            <br />
            <asp:TextBox runat="server" ID="txtLetter" />
            <asp:TextBox runat="server" ID="txtNum" CssClass="txtNums" Text="2350" />
            <asp:Label runat="server" ID="lblQuantity" CssClass="lblNums" ClientIDMode="Static" Text="50" />
            <input type="button" value="test" onclick="test()" />

        </div>
        <br />
        <div style="margin-left: 50px;">

            <asp:Calendar ID="Calendar1" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged" Width="220px" ShowGridLines="True">
                <DayHeaderStyle BackColor="#FFCC66" Height="1px" Font-Bold="True" />
                <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                <OtherMonthDayStyle ForeColor="#CC9966" />
                <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                <SelectorStyle BackColor="#FFCC66" />
                <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
            </asp:Calendar>
            <table class="table" style="width: 98%;">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th>酒店</th>
                        <th>婚期</th>
                        <th>联系电话</th>
                        <th>电销</th>
                        <th>策划师</th>
                        <th>状态</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater runat="server" ID="RepCustomer">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("Bride") %></td>
                                <td><%#Eval("WineShop") %></td>
                                <td><%#Eval("PartyDate","{0:yyyy-MM-dd}") %></td>
                                <td><%#Eval("BrideCellPhone") %></td>
                                <td><%#GetEmployeeName(Eval("InviteEmployee")) %></td>
                                <td><%#GetEmployeeName(Eval("QuotedEmployee")) %></td>
                                <td><%#GetCustomerStateStr(Eval("State")) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </form>
</body>

</html>

