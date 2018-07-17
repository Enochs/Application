<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="AllCarryTaskShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.AllCarryTaskShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tablestyle1 {
            width: 100%;
            height: 100%;
            border-width: 0px;
            border-style: solid;
            border-color: White;
            border-collapse: collapse;
            border-bottom-color: Black;
            border-right-color: Black;
        }

        .tablestyles {
            border-width: 1px;
            border-style: solid;
            border-color: Black;
            border-collapse: collapse;
            font-size: 13px;
            font-weight: 100;
            color: #2F4F4F;
        }

        table tr td a {
            /*color:#e05528;*/
        }

        table thead tr th input {
            background-color: #5A7DE7;
            color: white;
            cursor: pointer;
            border: 1px solid gray;
            height: 25px;
            width: auto;
        }

        .tbl_DataList a:visited {
            color: blue;
        }

        table thead tr th input:hover {
            background-color: #2050DF;
        }

        .divPrint input {
            background-color: #5A7DE7;
            color: white;
            cursor: pointer;
            border: 1px solid gray;
            height: 25px;
            width: auto;
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
        function ShowPrint(KeyID, Control, Sname) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/PrintTaskWork.aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&WorkType=<%=Request["WorkType"] %>&SupplierName=" + Sname + " &NeedPopu=1";
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 1000, 1500, "a#" + $(Control).attr("id"));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ui-menu-divider">
        <div runat="server">

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
                                        <td style="width: 100px;"><b>产品/服务内容</b></td>
                                        <td style="width: 300px;"><b>具体要求</b></td>
                                        <td style="width: 50px;"><b>单价</b></td>
                                        <td style="width: 50px; word-break: break-all; overflow: hidden; word-wrap: break-word;"><b>单位</b></td>
                                        <td style="width: 50px;"><b>数量</b></td>
                                        <td style="width: 50px;"><b>小计</b></td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptDataList">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#Eval("ServiceContent") %></td>
                                                <td><%#Eval("Requirement") %></td>
                                                <td><%#Eval("PurchasePrice") %></td>
                                                <td><%#Eval("Unit") %></td>
                                                <td><%#Eval("Quantity") %></td>
                                                <td><%#Eval("Subtotal") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <td>合计</td>
                                        <td></td>

                                        <td></td>
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
                    <!--endprint-->
                </ItemTemplate>
            </asp:Repeater>

        </div>
    </div>
</asp:Content>
