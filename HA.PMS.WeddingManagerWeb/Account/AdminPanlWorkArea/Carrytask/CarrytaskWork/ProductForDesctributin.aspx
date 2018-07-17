<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="ProductForDesctributin.aspx.cs" StylesheetTheme="Default" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.ProductForDesctributin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #lblSupplyName {
            font-weight:bolder;
            font-size:14px;
        }

        input {
            border: 1px solid gray;
            cursor: pointer;
            background-color: #5A7DE7;
            color: white;
            width: 50px;
            height: 25px;
        }

            input:hover {
                background-color: #2050DF;
            }
        .lblMessage {
            color:#312828;
            font-size:15px;
            font-weight:bold;
        }
    </style>
    <script type="text/javascript">
        ///弹出打印 导出界面
        function ShowPrint(KeyID, Control, Sname) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/PrintTaskWork.aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&WorkType=<%=Request["WorkType"] %>&SupplierName=" + Sname + " &NeedPopu=1";
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 1000, 1500, "a#" + $(Control).attr("id"));
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div runat="server" class="div div-destribution">
        <div class="divRefresh">
            <asp:Button runat="server" ID="btnRefresh" Text="刷新" CssClass="btn btn-primary" OnClick="btnRefresh_Click" /><br /><p></p>
            <asp:Label runat="server" ID="lblMessage" ClientIDMode="Static" CssClass="lblMessage" />
        </div>
        <asp:Repeater ID="rptProduct" runat="server" OnItemDataBound="rptProduct_ItemDataBound">
            <ItemTemplate>
                <!--startprint-->
                <table class="tablestyle1">
                    <thead>
                        <tr>
                            <th style="text-align: left; font-size: 15px; font-weight: bolder;">
                                <div runat="server" class="divPrint">
                                    <asp:Label runat="server" ID="lblSupplyName" Text='<%#Eval("Sname") %>' />
                                    <a href="#" onclick='ShowPrint(0,this,"<%#Eval("Sname") %>")'>
                                        <input id="btn_Caigou" runat="server" type="button" value="打印/导出" />
                                    </a>
                                </div>
                            </th>
                        </tr>
                    </thead>
                    <tr>
                        <td>
                            <table class="tablestyles" border="1" style="border-color: #000000; width: 100%;">
                                <tr>
                                    <%--<td style="width: 100px;"><b>类别</b></td>
                                    <td style="width: 100px;"><b>项目</b></td>--%>
                                    <td style="width: 100px;"><b>产品/服务内容</b></td>
                                    <td style="width: 300px;"><b>具体要求</b></td>
                                    <td style="width: 50px;"><b>单价</b></td>
                                    <td style="width: 50px; word-break: break-all; overflow: hidden; word-wrap: break-word;"><b>单位</b></td>
                                    <td style="width: 50px;"><b>数量</b></td>
                                    <td style="width: 50px;"><b>小计</b></td>
                                    <%--<td><b>责任单位</b></td>--%>
                                </tr>
                                <asp:Repeater runat="server" ID="rptDataList">
                                    <ItemTemplate>
                                        <tr>
                                            <%--<td><%#Eval("ParentCategoryName") %></td>
                                            <td><%#Eval("CategoryName") %></td>--%>
                                            <td><%#Eval("ServiceContent") %></td>
                                            <td><%#Eval("Requirement") %></td>
                                            <td><%#Eval("PurchasePrice") %></td>
                                            <td><%#Eval("Unit") %></td>
                                            <td><%#Eval("Quantity") %></td>
                                            <td><%#Eval("Subtotal") %></td>
                                            <%--<td><%#Eval("SupplierName") %></td>--%>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
                                    <td>合计</td>
                                    <td></td>

                                    <td>
                                        <%--<asp:Label ID="lblSumCostPrice" runat="server" Text="" />--%></td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblSumQuantity" runat="server" Text=""></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblSumMoney" runat="server" Text=""></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!--endprint-->
                    <tr>
                        <td style="height: 30px; vertical-align: top;">
                            <div runat="server">
                                <%-- <asp:Button runat="server" ID="btnPrint2" CssClass="btn btn_info" Text="打印" Style="background-color: #5A7DE7; color: white; cursor: pointer; border: 1px solid gray; height: 25px; width: auto;" OnClientClick="return preview()" />--%>
                            </div>
                        </td>
                    </tr>
                </table>

            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
