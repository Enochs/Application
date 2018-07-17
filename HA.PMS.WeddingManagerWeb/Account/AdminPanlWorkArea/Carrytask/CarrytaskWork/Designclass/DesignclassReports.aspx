<%@ Page Title="个人清单" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="DesignclassReports.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Designclass.DesignclassReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tablestyle {
            border-width: 1px;
            border-style: solid;
            border-color: #2050DF;
            border-collapse: collapse;
            font-size: 13px;
            font-weight: 100;
            width: auto;
            color: #2F4F4F;
        }

            .tablestyle tr th {
                background-color: #a19c9c;
                height: 35px;
            }

            .tablestyle tr td {
                height: 30px;
                background-color: #efe1e1;
            }

        .btnprints {
            background-color: #5A7DE7;
            color: white;
            cursor: pointer;
            border: 1px solid gray;
            font-size: 13px;
            width: auto;
        }

        .btn-primary {
            color: white;
            background-color: #1e92de;
            cursor: pointer;
        }

            .btn-primary:hover {
                background-color: #2050DF;
                color: white;
            }

        .btnprints:hover {
            background-color: #2050DF;
            color: white;
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

        function method2(tableid) //读取表格中每个单元到EXCEL中 
        {
            alert("信息q")
            var curTbl = document.getElementById(tableid);
            alert("信息5")
            var oXL = new ActiveXObject("Excel.Application");
            alert("信息4")
            //创建AX对象excel 
            var oWB = oXL.Workbooks.Add();
            alert("信息3")
            //获取workbook对象 
            var oSheet = oWB.ActiveSheet;
            alert("信息2")
            //激活当前sheet 
            var Lenr = curTbl.rows.length;
            alert("信息1")
            //取得表格行数 
            for (i = 0; i < Lenr; i++) {
                var Lenc = curTbl.rows(i).cells.length;
                //取得每行的列数 
                for (j = 0; j < Lenc; j++) {
                    oSheet.Cells(i + 1, j + 1).value = curTbl.rows(i).cells(j).innerText;
                    //赋值 
                }
            }
            oXL.Visible = true;
            //设置excel可见属性 
        }

        //查看图片
        function ShowFileShowPopu(DesignClassID, Type) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignImageShowList.aspx?DesignClassID=" + DesignClassID + "&OrderID=<%=Request["OrderID"]%>&Type=" + Type;
            showPopuWindows(Url, 700, 800, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <!--startprint-->
    <div runat="server" class="divPrint">
        <table class="tablestyle" id="tblDesignList" border="1" style="width: 98%; border: 1px solid #464040;">
            <tr>
                <th>产品名称</th>
                <th>下单时间</th>
                <th>说明</th>
                <th>采购数量</th>
                <th>材质</th>
                <th>采购单价</th>
                <th>单位</th>
                <%--                <th>参考图</th>--%>
                <th>效果图</th>
                <th>供应商</th>
                <th>结算总价</th>
                <th>实际到货数量</th>
                <th>操作</th>
            </tr>
            <asp:Repeater runat="server" ID="rptDesignList" OnItemCommand="rptDesignList_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("Title") %>
                            <asp:HiddenField runat="server" ID="HideDesignID" Value='<%#Eval("DesignclassID") %>' />
                            <asp:HiddenField runat="server" ID="HideState" Value='<%#Eval("DesignState") %>' />
                        </td>
                        <td><%#Eval("CreateDate") %></td>
                        <td><%#Eval("Node") %></td>
                        <td><%#Eval("PurchaseQuantity") %></td>
                        <td><%#Eval("MaterialName") %></td>
                        <td><%#Eval("PurchasePrice") %></td>
                        <td><%#Eval("Unit") %></td>
                        <%--<td><a href="#" onclick="ShowFileShowPopu('<%#Eval("DesignclassID") %>',2)" class="btn btn-mini   btn-primary needshow">查看</a></td>--%>
                        <td><a href="#" onclick="ShowFileShowPopu('<%#Eval("DesignclassID") %>',1)" class="btn btn-mini   btn-primary needshow">查看</a></td>
                        <td><%#Eval("SupplierName") %></td>
                        <td><%#Eval("TotalPrice") %></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtRealQuantity" Text='<%#Eval("RealQuantity")%>' ClientIDMode="Static" Style="width: 30px;" /></td>
                        <td>
                            <asp:Button runat="server" ID="btn_Confirm" CommandName="Confirm" CommandArgument='<%#Eval("DesignclassID") %>' Text="确定" CssClass="btn btn-primary" /></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Label runat="server" ID="lblTotalPriceSum" /></td>
                <td></td>
                 <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <!--endprint-->

    <div class="div_handle">
        <asp:Button runat="server" ID="btnPrint2" CssClass="btn btn-primary" Text="打印" OnClientClick="return preview()" />
        <asp:Button runat="server" ID="btn_Save" Text="保存" CssClass="btn btn-primary" OnClick="btn_Save_Click" Visible="false" />
        <%--<asp:Button runat="server" ID="btn_Export" Text="导出Excel" CssClass="btn btn-primary" OnClientClick="javascript:method2('tblDesignList');" />
        <input type="button" onclick="javascript: method2('tblDesignList');" value="第二种方法导入到EXCEL" style="cursor:pointer;" />--%>
        <%--<asp:Button runat="server" ID="btnExcel" CssClass="btnprints" Text="导出" Style="background-color: #5A7DE7; color: white; cursor: pointer; border: 1px solid gray; width: auto;" OnClientClick="return ExportExcelWithFormat()" />--%>
    </div>
</asp:Content>
