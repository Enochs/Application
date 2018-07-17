<%@ Page Title="" Language="C#" EnableEventValidation="false" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" StylesheetTheme="None" CodeBehind="TaskWork.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.TaskWork" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyCarryTask.ascx" TagPrefix="HA" TagName="MyCarryTask" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tablestyle {
            border-width: 1px;
            border-style: solid;
            border-color: Black;
            border-collapse: collapse;
            font-size: 12px;
            font-weight: 100;
            color: #2F4F4F;
        }


        #table1 td {
            vertical-align: top;
            border-width: 0px;
        }

        #tlefttop {
            background-color: #ccc;
        }

        #trighttop {
            background-color: #ccc;
        }

        #tlefttop td {
            border-width: 1px;
            border-color: Black;
            border-style: solid;
            width: 100px;
            text-align: center;
            height: 20px;
        }

        #tleftbottom td {
            border-width: 1px;
            border-color: Black;
            border-style: solid;
            width: 100px;
            text-align: center;
            height: 20px;
        }

        #trighttop td {
            border-width: 1px;
            border-color: Black;
            border-style: solid;
            width: 100px;
            text-align: center;
            height: 20px;
        }

        #trightbottom td {
            border-width: 1px;
            border-color: Black;
            border-style: solid;
            width: 100px;
            text-align: center;
            height: 20px;
        }

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

        a:visited {
            color: blue;
        }

        input:visited {
            color: blue;
        }

        .divPrint input:hover {
            background-color: #2050DF;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".ABCD1").remove();
            $("a#inline").fancybox();
            showPopuWindows($("#SelectPG").attr("href"), 400, 300, "a#SelectPG");
            $(".NeedHideLable").hide();
            HideCatgoryCreate();

            if ($("#hideMineNeedState").val() == "1") {
                //$(".SelectSecondSG").hide();
                $(".SelectProductSG").hide();
                $(".SetState").hide();
                $(".Move").remove();

            }


            $("#chkchkall").click(function () {
                if ($(this).is(":checked")) {

                    $(":checkbox").attr("checked", true);

                } else {
                    $(":checkbox").attr("checked", false);
                }
            })


            $(":checkbox").change(function () {
                if ($(this).is(":checked")) {
                    $(this).parent().parent().parent("tr").attr("background-color", "aqua");
                }
            })
        });

        //禁止添加项目
        function HideCatgoryCreate() {
            //$(".SelectSG").hide();
            //$(".SelectSecondSG" +$("#hideMineState").val()).show();
            //$(".SelectProductSG" + $("#hideMineState").val()).show();

        }

        function HideProductCreate() {
            if ($("#hideMineState").val() == "3") {
                $(".SelectSecondSG").hide();
                $(".SelectProductSG").hide();

            }
            //if ($("#hideMineNeedState").val() == "1") {
            //    $(".SetState").hide();
            //}
        }


        $(document).ready(function () {

            $(".SaleItem").change(function () {
                eachAmount();
            });



            $(".SelectSG").each(function () {
                showPopuWindows($(this).attr("href"), 600, 300, $(this));
                $("#hideSecondCategoryID").attr("value", $(this).attr("CategoryID"));
            });

            $(".SlectFour").each(function () {
                showPopuWindows($(this).attr("href"), 400, 300, $(this));
            });




            //单价改变时
            $(".Quantity").change(function () {

                var PriceIndex = ".SetSubtotal" + $(this).attr("ProductID");
                var ParetnTotal = "#txtTotal" + $(this).attr("ParentCategoryID");
                var TotalNum = 0;
                var OldTexValue = $(ParetnTotal).val();
                var OLdValue = 0;
                if (OldTexValue != "") {
                    OLdValue = parseFloat(OldTexValue);
                }

                //alert(PriceIndex);
                var NewValue = 0;

                var Quantity = parseFloat($(PriceIndex).find(".Quantity").val());
                var OldPrice = parseFloat($(PriceIndex).find(".OldPrice").val());

                var Subtotal = Quantity * OldPrice;

                //var OldTotal = parseFloat($("#txtAggregateAmount").attr("value"));
                if ($(PriceIndex).find(".Subtotal").val() == "") {
                    OLdValue = Subtotal + OLdValue;
                    //OldTotal = Subtotal + OLdValue;
                } else {
                    OLdValue = OLdValue - parseFloat($(PriceIndex).find(".Subtotal").val());
                    OLdValue = OLdValue + Subtotal;
                }

                $(PriceIndex).find(".Subtotal").attr("value", Subtotal);
                $(ParetnTotal).attr("value", OLdValue);
                SetTotalAmount();

            });


            //单价改变时
            $(".OldPrice").change(function () {

                var PriceIndex = ".SetSubtotal" + $(this).attr("ProductID");
                var ParetnTotal = "#txtTotal" + $(this).attr("ParentCategoryID");
                var TotalNum = 0;
                var OldTexValue = $(ParetnTotal).val();
                var OLdValue = 0;
                if (OldTexValue != "") {
                    OLdValue = parseFloat(OldTexValue);
                }

                var NewValue = 0;

                var Quantity = parseFloat($(PriceIndex).find(".Quantity").val());
                var OldPrice = parseFloat($(PriceIndex).find(".OldPrice").val());

                var Subtotal = Quantity * OldPrice;

                //var OldTotal = parseFloat($("#txtAggregateAmount").attr("value"));
                if ($(PriceIndex).find(".Subtotal").val() == "") {
                    OLdValue = Subtotal + OLdValue;
                    //OldTotal = Subtotal + OLdValue;
                } else {
                    OLdValue = OLdValue - parseFloat($(PriceIndex).find(".Subtotal").val());
                    OLdValue = OLdValue + Subtotal;
                }

                $(PriceIndex).find(".Subtotal").attr("value", Subtotal);
                $(ParetnTotal).attr("value", OLdValue);
                SetTotalAmount();

            });
        });


        //计算销售总价
        function SetTotalAmount() {
            var TotalAmount = 0;
            $(".ItemAmount").each(function () {
                if ($(this).val() != "") {
                    TotalAmount = TotalAmount + parseFloat($(this).val());
                }
            });
            $("#txtAggregateAmount").attr("value", TotalAmount);

            $("#hideAggregateAmount").attr("value", TotalAmount);

        }

        //循环销售价
        function SetRealAmount() {

        }

        //
        function eachAmount() {
            var TotalAmount = 0;
            $(".SaleItem").each(function () {
                if ($(this).val() != "") {
                    TotalAmount = TotalAmount + parseFloat($(this).val());
                }
                $("#txtRealAmount").attr("value", TotalAmount);
            });
        }


        function CheckDelete() {
            if (confirm("确认删除！删除后无法恢复")) {
                return true;
            } else {
                return false;
            }
        }

        function SetfunCategoryID(Control) {
            $("#hideSecondCategoryID").attr("value", $(Control).attr("categoryid"));

            return false;
        }


        function SetProduct(Control) {
            $("#hideThirdCategoryID").attr("value", $(Control).attr("categoryid"));
            return false;
        }



        ///计算小计
        function GetAvgSum(Control) {
            $(".SetSubtotal").find();
            $(".SetSubtotal").find();
            $(".SetSubtotal").find();
        }

        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        function ShowNewPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideNewEmpLoyeeID&SetEmployeeName=txtEmpLoyeeItem&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //上传图片
        function ShowFileUploadPopu(QuotedID, Kind) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceImageUpload.aspx?QuotedID=" + QuotedID + "&Kind=" + Kind;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        //查看图片
        function ShowFileShowPopu(QuotedID, ChangeID) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceItemFileList.aspx?QuotedID=" + QuotedID + "&ChangeID=" + ChangeID;
            showPopuWindows(Url, 700, 800, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function ShowFirstPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideFirstEmpLoyeeID&SetEmployeeName=txtFirstEmpLoyeeItem&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        function ShowFirstPopu1(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideSetFirstEmpLoyeeID&SetEmployeeName=txtFirstEmpLoyeeItem&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
        //选择供应商
        function ChangeSuppByCatogry(Parent, CallBack) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectSupplierBythis.aspx?Callback=" + CallBack + "&ALL=true&ControlKey=hideSuppID&SetEmployeeName=txtSuppName&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
        //指定四大金刚
        function ChangeFourGuardian(Parent, CallBack) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectFourGuardian.aspx?Callback=" + CallBack + "&ALL=true&ControlKey=hideSuppID&SetEmployeeName=txtSuppName&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function SetEmpoyee(Parent, CallBack) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?Callback=" + CallBack + "&ALL=true&ControlKey=hideSuppID&SetEmployeeName=txtSuppName&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
        //更换产品
        function SelectSupp(Key, CategoryID, CustomerID) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectProduct.aspx?ControlKey=hideThirdValue&Callback=btnChangeProduct&ChangeProduct=1&CategoryID=" + CategoryID + "&CustomerID=" + CustomerID;

            //$(Control).attr("href", Url);
            showPopuWindows(Url, 600, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
            $("#hideChange").attr("value", Key);
        }


        function CreateFlower() {
            var URL = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/FlowerReport/FlowerReport.aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%#Request["OrderID"]%>&NeedPopu=1";
            ShowReport(URL, 1200, 1000);

        }


        function ShowWareHouse() {
            var URL = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/WorkReport/StorehouseReport.aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%#Request["OrderID"]%>&NeedPopu=1";
            ShowReport(URL, 1000, 1000);
        }


        function ShowBuyList() {
            var URL = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/WorkReport/PurchaseReport.aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%#Request["OrderID"]%>&NeedPopu=1";
            ShowReport(URL, 1200, 1000);
        }

        function ShowAllreport() {
            var URL = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/WorkReport/WorkReportPanel.aspx?PageNameProductList&DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&NeedPopu=1";
            //ShowReport(URL, 1500, 1000);
            window.parent.parent.ShowPopuWindow(URL, 1500, 1000);
        }

        function ShowPen() {

            URI = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignclassReport.aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%=Request["OrderID"]%>&QuotedID=<%=Request["QuotedID"]%>&NeedPopu=1";
            ShowReport(URI, 1200, 1000);
        }

        function ShowThis() {
            window.parent.parent.ShowPopuWindow("/AdminPanlWorkArea/QuotedPrice/QuotedPriceShowOrPrint.aspx?CustomerID=<%=Request["CustomerID"]%>&NeedPopu=1", 1500, 1000);
            return false;
        }

        function ShowReport(URL, Width, Height) {
            window.parent.parent.ShowPopuWindow(URL, Width, Height);

        }

        function ShowThis(KeyID, Control, Type) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/WorkReport/WorkReportPanel.aspx?PageNameProductList&DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&OrderID=<%=Request["OrderID"]%>&QuotedID=<%=Request["QuotedID"]%>&NeedPopu=1&Type=" + Type;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 1280, 1500, "a#" + $(Control).attr("id"));
        }


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

        //查看图片
        function ShowFileShowPopu(QuotedID, ChangeID) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceItemFileList.aspx?QuotedID=" + QuotedID + "&ChangeID=" + ChangeID;
            showPopuWindows(Url, 700, 800, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content3">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <table style="width: 100%; display: none;" border="0">
        <tr>
            <td align="left" id="FirstTypetd"></td>
            <td style="text-align: left; width: 25%;">&nbsp;</td>
        </tr>
    </table>
    <table id="FirstTable" border="0" style="border-color: #000000; width: 100%;">
        <thead>
            <tr>
                <th style="text-align: left;" id="<%=Guid.NewGuid().ToString() %>">
                    <input id="btnSetSuplier" class="btn btn-primary" type="button" value="指定供应商" onclick="ChangeSuppByCatogry(this, 'btnSavesupperSave')" />
                    <input id="btnSetEmployee" class="btn btn-primary" type="button" value="指定内部员工" onclick="SetEmpoyee(this, 'btnSaveSuppertoOther')" />
                    <input id="btnSetjingang" class="btn btn-primary" type="button" value="指定四大金刚" onclick="ChangeFourGuardian(this, 'btnFourGuardianSave')" />
                    <asp:Button ID="btnSetWarhouse" CssClass="btn btn-primary" runat="server" Text="指定库房" OnClick="btnSetWarhouse_Click" />
                    <asp:Button ID="btnSetBuy" CssClass="btn btn-primary" runat="server" Text="指定新购入" OnClick="btnSetBuy_Click" Visible="false" />
                    <asp:Button ID="btnCppy" CssClass="btn btn-primary" runat="server" Text="复制到花艺" OnClick="btnCppy_Click" />
                    <asp:Button ID="Button3" CssClass="btn btn-primary" runat="server" Text="复制到道具" OnClick="btnCppy_Click" />
                    <asp:Button ID="Button4" CssClass="btn btn-primary" runat="server" Text="复制到灯光" OnClick="btnCppy_Click" />
                    <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="复制到人员" OnClick="btnCppy_Click" />
                    <a href="#" class="btn btn-success" onclick='ShowThis(0,this,"All")'>
                        <input id="Button6" runat="server" type="button" value="查看派工清单" onclick='ShowThis(0, this, "All")' visible="false" /></a>
                    <a href="#" onclick='ShowThis(0,this,"Flower")'>
                        <input id="btn_Flower" runat="server" type="button" value="花艺采购单" onclick='ShowThis(0, this, "Flower")' visible="true" /></a>
                    <a href="#" onclick='ShowThis(0,this,"Prop")'>
                        <input id="btn_Caigou" runat="server" type="button" value="采购清单" onclick='ShowThis(0, this, "Prop")' visible="false" /></a>
                    <asp:HiddenField ID="hideSuppID" runat="server" ClientIDMode="Static" />
                    <lable style="display: none;">
                        <asp:Button ID="btnSaveItem" CommandName="SaveItem" runat="server" Text="保存" CssClass="btn btn-success" OnClick="btnSaveItem_Click" />
                        <a href="#" onclick="ShowFirstPopu(this);" class="SetState btn">
                        <asp:Label ID="lblSelectTitle" runat="server" Text="改派"></asp:Label></a>
                        <asp:HiddenField ID="hideFirstEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
                        <asp:HiddenField ID="hideOldEmployeeID" ClientIDMode="Static" Value="-1" runat="server" />
                        <asp:Button ID="btnSavesupperSave" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="供应商保存" CssClass="btn btn-success" OnClick="btnSavesupperSave_Click" />
                        <asp:Button ID="btnSaveSuppertoOther" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="其他保存" CssClass="btn btn-success" OnClick="btnSaveSuppertoOther_Click" />
                        <asp:Button ID="btnFourGuardianSave" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="四大金刚保存" CssClass="btn btn-success" OnClick="btnFourGuardianSave_Click" />
                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hidePgValue" />
                        <asp:Button ID="btnStarFirstpg" ClientIDMode="Static" runat="server" Text="生成一级分类" OnClick="btnStarFirstpg_Click" />
                        <asp:Button ID="btnCreateSecond" ClientIDMode="Static" runat="server" Text="生成二级分类" OnClick="btnCreateSecond_Click" />
                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSecondValue" />
                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSecondCategoryID" />
                        <asp:Button ID="btnCreateThired" ClientIDMode="Static" runat="server" Text="生成产品" OnClick="btnCreateThired_Click" />
                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdValue" />
                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdCategoryID" />
                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideFoureGingang" />
                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideMineState" />
                        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideMineNeedState" />
                        <asp:Button ID="btnChangeProduct" ClientIDMode="Static" runat="server" Text="更换产品" OnClick="btnChangeProduct_Click" />
                        <asp:HiddenField ID="hideChange" ClientIDMode="Static" runat="server" />
                    </lable>
                </th>
            </tr>
            <tr>
                <td style="background-color: white;">
                    <%--<input id="btnCreateflowerList" runat="server" type="button" value="查看派工清单" onclick="ShowAllreport();" visible="true" />&nbsp;--%>
                    <input id="btnWareHouseProductList" type="button" value="库房领料单" runat="server" onclick="ShowWareHouse();" visible="false" />
                    <input id="btnBuyProductList" type="button" value="采购清单" runat="server" onclick="ShowBuyList();" visible="false" />
                    <input id="Button5" type="button" value="设计类清单" runat="server" onclick="ShowPen();" visible="false" />
                </td>
            </tr>
            <tr>
                <td style="background-color: white;">
                    <input id="btnEmployeList" runat="server" type="button" value="执行人员清单" visible="false" />

                </td>
            </tr>
        </thead>
        <tr>
            <td>
                <div style="overflow: auto;">
                    <input id="chkchkall" type="checkbox" />全选所有产品
                   <asp:Button runat="server" ID="btnClearCost" Text="清空成本" CssClass="btn btn-primary" OnClick="btnClearCost_Click" Style="cursor: pointer; background-color: #5A7DE7; color: white; width: 65px; height: 25px; border: 1px solid gray; margin-bottom: 5px;" />
                    <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound" OnItemCommand="repfirst_ItemCommand">
                        <ItemTemplate>
                            <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                            <asp:HiddenField ID="hideKey" Value='<%#Eval("ProeuctKey") %>' runat="server" />
                            <div id="<%#Guid.NewGuid().ToString() %>">


                                <asp:HiddenField ID="hideSetFirstEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
                                <asp:HiddenField ID="hideCatgoryID" ClientIDMode="Static" Value='<%#Eval("CategoryID") %>' runat="server" />

                            </div>
                            <table class="tablestyle First<%#Eval("CategoryID") %>" id="tbl_DataList" border="1" style="border-color: #000000; width: 100%;">

                                <tr>
                                    <td style="width: 100px;"><b>类别</b></td>
                                    <td style="width: 100px;"><b>项目</b></td>
                                    <td style="width: 100px;"><b>产品/服务内容</b>
                                    </td>
                                    <td style="width: 180px;"><b>具体要求</b></td>
                                    <td style="width: 50px;"><b>图片</b></td>
                                    <td style="width: 50px;"><b>销售价</b></td>
                                    <td style="width: 50px;"><b>成本价</b></td>
                                    <td style="width: 50px; word-break: break-all; overflow: hidden; word-wrap: break-word;"><b>单位</b></td>
                                    <td style="width: 50px;"><b>数量</b></td>
                                    <td style="width: 50px;"><b>小计</b></td>
                                    <td style="width: 150px;"><b>备注</b></td>
                                    <td style="display: none;"><b>责单位</b></td>
                                    <td><b>责任单位</b></td>
                                    <td style="white-space: nowrap;"><b>操作</b></td>
                                </tr>
                                <asp:Repeater ID="repdatalist" runat="server" OnItemCommand="repdatalist_ItemCommand" ClientIDMode="Static">
                                    <ItemTemplate>
                                        <tr <%#GetByQuoted(Eval("DispatchingID"),Eval("ProductID")).ToString().ToInt32() > 0 ? "style='color:red;font-weight:bold;'" : "" %>>
                                            <td <%#Eval("ItemLevel").ToString()=="1"?"style='border-top-style:double;border-bottom-style:none;white-space:nowrap;'" : "class='NeedHide'  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'"%> <%#Eval("ParentCategoryID")%>>

                                                <b <%#Eval("ItemLevel").ToString()!="1"?"class='NeedHideLable'": ""%> class="btn btn-primary"><%#Eval("ParentCategoryName") %></b><br />
                                                <label>
                                                    <br />
                                                    <a id="SelectSG" <%#HideSelectItem(Container.ItemIndex) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-primary btn-mini" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=<%#Eval("ItemLevel").ToString()=="1"?Eval("CategoryID"):Eval("ParentCategoryID") %>&ControlKey=hideSecondValue&Callback=btnCreateSecond" onclick="return SetfunCategoryID(this);"><span>增加项目</spa></a>
                                                </label>
                                                <asp:HiddenField ID="hidePriceKey" Value='<%#Eval("ProeuctKey") %>' runat="server" />
                                                <asp:HiddenField ID="hideCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                                            </td>
                                            <td <%#Eval("ItemLevel").ToString()=="2"?"style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:nowrap;'":"style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" %>>
                                                <b <%#HideSelectProduct(Container.ItemIndex) %>><%#Eval("CategoryName") %></b><br />
                                                &nbsp;&nbsp;&nbsp;&nbsp;<a id="A2" <%#HideSelectProduct(Container.ItemIndex) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-primary btn-mini" href="/AdminPanlWorkArea/ControlPage/SelectProduct.aspx?CategoryID=<%#Eval("CategoryID") %>&ControlKey=hideThirdValue&Callback=btnCreateThired&PartyDate=<%#Request["PartyDate"] %>&CustomerID=<%=Request["CustomerID"] %>" onclick="SetProduct(this);">增加产品</a>
                                                <input id="txtEmpLoyeeItem" style="width: 65px; display: none;" class="txtEmpLoyeeItem" type="text" value='<%#GetCatgoryEmpLoyeeName(Eval("CategoryID"))%>' />

                                                <asp:HiddenField ID="hideNewEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />

                                            </td>

                                            <td>
                                                <b <%#Eval("ItemLevel").ToString()!="3"?"class='NeedHideLable'": ""%>>
                                                    <asp:CheckBox ID="CheckItem" runat="server" /></b>

                                                <a href="#" onclick="SelectSupp(<%#Eval("ProeuctKey") %>,<%#Eval("CategoryID") %>,<%=Request["CustomerID"] %>);" title="点击此处可以更换产品！"><%#Eval("ItemLevel").ToString()=="3"?Eval("ServiceContent"):""%></a>
                                                <div style="display: none;">
                                                    <asp:TextBox ID="txtProductName" Style="margin: 0;" runat="server" Enabled="false" Text='<%#Eval("ServiceContent") %>' Visible="false"></asp:TextBox>
                                                </div>
                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtRequirement" Style="width: 92%; margin: 0" runat="server" Text='<%#Eval("Requirement") %>' TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td style="width: 78px">&nbsp;
                                                 <a href="#" onclick="ShowFileUploadPopu('<%#GetQuotedID(Eval("DispatchingID")) %>','<%#GetChangeId(Eval("ProductID")) %>')" class="btn btn-mini   btn-primary">上传</a>
                                                <%--<a id="inline" style="margin: 0" class="btn  btn-primary" href="#data<%#Eval("ProeuctKey") %>" kesrc="#data<%#Eval("ProeuctKey")%>">查看</a>--%>
                                                <a href='#' onclick="ShowFileShowPopu('<%#GetQuotedID(Eval("DispatchingID")) %>','<%#GetChangeId(Eval("ProductID")) %>')" class="btn btn-mini  btn-primary needshow">查看</a>

                                                <div style="display: none;">
                                                    <div id='data<%#Eval("ProeuctKey") %>'>
                                                        <%#GetKindImage(Eval("ProeuctKey")) %>
                                                    </div>
                                                </div>
                                            </td>

                                            <td class="SetSubtotal<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>">

                                                <asp:TextBox ID="txtSalePrice" Style="margin: 0" MaxLength="8" CssClass="SalePrice" runat="server" ProductID='<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>' Text='<%#Eval("UnitPrice") %>' Width="75"></asp:TextBox>

                                            </td>
                                            <td class="SetSubtotal<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>">

                                                <asp:TextBox ID="txtPurchasePrice" Style="margin: 0" MaxLength="8" CssClass="OldPrice" runat="server" ProductID='<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>' Text='<%#Eval("PurchasePrice") %>' Width="75" ClientIDMode="Static"></asp:TextBox>
                                                <%--<input type="text" id="txtPurchasePrices" value='<%#Eval("PurchasePrice") %>' name="txtprices" style="width: 75px;" />--%>
                                            </td>
                                            <td>
                                                <div <%#Eval("Requirement").ToString()==""&&(Eval("ItemLevel").ToString()=="1"||Eval("ItemLevel").ToString()=="2")?"style='display: none;'":"" %>>
                                                    <%#Eval("Unit") %>
                                                </div>
                                            </td>
                                            <td class="SetSubtotal<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>">
                                                <div <%#Eval("Requirement").ToString()==""&&(Eval("ItemLevel").ToString()=="1"||Eval("ItemLevel").ToString()=="2")?"style='display: none;'":"" %>>
                                                    <asp:TextBox ID="txtQuantity" Style="margin: 0" MaxLength="8" CssClass="Quantity" runat="server" Width="30" ProductID='<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("Quantity") %>'></asp:TextBox>
                                                </div>
                                            </td>
                                            <td class="SetSubtotal<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>">

                                                <asp:TextBox ID="txtSubtotal" Style="margin: 0" MaxLength="8" class='Subtotal' runat="server" Width="75" ProductID='<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>' Text='<%#Eval("Subtotal") %>'></asp:TextBox>

                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Width="95%" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                                            <td id="Partd<%#Guid.NewGuid().ToString() %>" style="display: none;">
                                                <input style="width: 45px;" maxlength="10" runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" type="text" value='<%#GetEmployeeName(Eval("EmployeeID")) %>' />
                                            </td>
                                            <td style="width: 50px; white-space: nowrap; text-align: left;" id="Row<%#Guid.NewGuid().ToString() %>">
                                                <input runat="server" maxlength="10" id="txtSuppName" type="text" class="txtSuppName " value='<%#Eval("SupplierName") %>' />
                                            </td>
                                            <td class="Delete<%#Eval("NewAdd") %>" style="width: 40px;">&nbsp;
                                                <asp:LinkButton ID="lnkbtnDelete" CssClass="LnkDelete btn btn-mini btn-danger" CommandName="Delete" CommandArgument='<%#Eval("ProeuctKey") %>' runat="server" OnClientClick="return CheckDelete();">删除</asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lbtnSaveItems" Text="保存" CssClass="btn btn-danger btn-mini" CommandName="Save" CommandArgument='<%#Eval("ProeuctKey") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr style="border: none;">
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td><%--<asp:Label ID="lblSumCostPrice" runat="server" Text="" />--%></td>
                                    <td></td>
                                    <td>
                                        <asp:Label ID="lblSumQuantity" runat="server" Text=""></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblSumMoney" runat="server" Text=""></asp:Label></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                            <div style="text-align: center">
                                <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("ItemAmount") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
            </td>
        </tr>

    </table>
    <div style="text-align: center;">
        <asp:Button ID="btnSaveItems" CommandName="SaveItem" runat="server" Text="保存" CssClass="btn btn-info" OnClick="btnSaveItem_Click" />
    </div>
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
