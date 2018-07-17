<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="PrintTaskWork.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.PrintTaskWork" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tablestyle {
            border-width: 1px;
            border-style: solid;
            border-color: #2F4F4F;
            border-collapse: collapse;
            font-size: 12px;
            font-weight: 100;
            width: auto;
            color: #2F4F4F;
        }

        .tablestyle1 {
            width: 100%;
            height: 100%;
            border-width: 0px;
            border-style: solid;
            border-color: white;
            border-collapse: collapse;
            border-bottom-color: Black;
            border-right-color: Black;
            width: auto;
        }

        btn_info {
            background-color: #5A7DE7;
            color: white;
            cursor: pointer;
            border: 1px solid gray;
            height: 25px;
            width: auto;
        }

            btn_info:hover {
                background-color: #2050DF;
            }
    </style>
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
    <div runat="server">
        <!--startprint-->
        <asp:Repeater ID="rptProduct" runat="server" OnItemDataBound="rptProduct_ItemDataBound">
            <ItemTemplate>
                <table class="tablestyle1">
                    <thead>
                        <tr>
                            <th style="text-align: left; font-size: 15px; font-weight: bolder;">
                                <asp:Label runat="server" ID="lblSupplyName" Text='<%#Eval("Sname") %>' />
                            </th>
                        </tr>
                    </thead>
                    <tr>
                        <td>
                            <table class="tablestyle" border="1" style="border-color: #000000; width: 100%;">
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
                                <tr style="border: none;">
                                    <td></td>
                                    <td></td>

                                    <td>
                                        <asp:Label ID="lblSumUnitPrice" runat="server" Text="" /></td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblSumQuantity" runat="server" Text=""></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblSumMoney" runat="server" Text=""></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
        <!--endprint-->
        <div runat="server" style="height:35px;">
            <asp:Button runat="server" ID="btnPrint2" CssClass="btn btn_info" Text="打印" Style="background-color: #5A7DE7; color: white; cursor: pointer; border: 1px solid gray; height: 25px; width: auto;" OnClientClick="return preview()" />
            <asp:Button ID="btnExcel" runat="server" CssClass="btn btn_info" Text="导出" Style="background-color: #5A7DE7; color: white; cursor: pointer; border: 1px solid gray; height: 25px; width: auto;" OnClick="btnExcel_Click" />
        </div>
        
    </div>
</asp:Content>
