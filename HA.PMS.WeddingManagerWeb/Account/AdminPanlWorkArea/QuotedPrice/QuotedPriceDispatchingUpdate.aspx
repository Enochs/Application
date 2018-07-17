<%@ Page Title="" Language="C#" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="QuotedPriceDispatchingUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceDispatchingUpdate" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="uc1" TagName="MessageBoard" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .widthText {
            width: 406px;
        }

        .warning {
            border: 1px solid red;
        }

        .auto-style1 {
            height: 24px;
        }
    </style>
    <script type="text/javascript">

        var RowTrID = "";
        function SetRowFocuse() {


            if ($("#hideRowKey").val() != "") {

                RowTrID = $("#hideRowKey").val();
                $(".table-select").removeClass("table-bordered").removeClass("table-striped");
                $("#" + RowTrID).attr("style", "background-color:rgb(166, 178, 195);color: white;");

            }
        }

        function SetRowKey(Key) {

            $("#hideRowKey").attr("value", Key);

        }

        function RealSubmit() {
            if (confirm("导入后将删除现有报价单！且无法恢复！")) {
                return true;
            } else {
                return false;
            }

        }

        function CheckPage() {
            if ($("#txtRealAmount").val() == '' || parseFloat($("#txtRealAmount").val()) <= 0) {
                alert("报价单销售价不能小于0");
                $("#txtRealAmount").focus();
                return false;;
            }


        }
        function checkcountblur(ctrl) {
            var _inputvalue = $(ctrl).val();
            var _avaivalue = $(ctrl).next().val();
            if (_avaivalue != '') {
                if (parseInt(_inputvalue) > parseInt(_avaivalue)) {
                    $(ctrl).css("border-color", "red");
                }
                else {
                    $(ctrl).css("border-color", "rgb(204, 204, 204)");
                }
            }
        }
        $(window).load(function () {
            $(".Quantity").each(function () {
                if ($(this).next().val() != '') {
                    $(this).attr("title", "当前剩余数量：" + $(this).next().val());
                }
            });
        });

        $(document).ready(function () {

            if ($("#HideFlowerSplit").val() == "1") {
                $(".FlowerItem").remove();
            }

            if ($("#HideSaleSplit").val() == "1") {
                $(".SaleItem").remove();
            }

            if ($("#hideDisState").val() == "4") {
                $("[type='text']").attr("disabled", "disabled");
                $(".SelectSG").hide();

                $("textarea").attr("disabled", "disabled");


            }

            $("input[type='radio']").click(function () {
                var RadioValue = $("input[type='radio']:checked").val();
                RadioValue = parseInt(RadioValue);
                if (RadioValue == 0) {
                    $(".HaveFile").show();
                } else {
                    $(".HaveFile").hide();
                }

            });

            $("a#inline").fancybox();

            if ($("#hidecheck").val() != "0") {
                $(".CheckNode").hide();
            }

            showPopuWindows($("#SelectPG").attr("href"), 500, 300, "a#SelectPG");
            $(".NeedHideLable").hide();


            $(".btnSubmit").click(function () {


            });

            if ('<%=Request["OnlyView"]%>') {
                $("input,textarea,select").attr("disabled", "disabled");
                $("input[type=button],.btn").hide();
            }

        });

        $(document).ready(function () {
            if ($("#HideHaveEmployee").val() == "1") {
                $(".HaveEmployee").hide();
            }

            SetRowFocuse();
            var SelectItem = $("#ddlType").find("option:selected").text()
            if (SelectItem == "新增") {
                $(".NoAdd").hide();
            }

            $("#ddlType").change(function () {
                var SelectItem = $(this).find("option:selected").text()
                if (SelectItem == "新增") {
                    $(".NoAdd").hide();
                } else {
                    $(".NoAdd").show();
                }
            }
            )

            document.bgColor = "#ff0000";


            $(".SaleItem").change(function () {
                eachAmount();
            });

            $(".SelectSG").each(function () {
                showPopuWindows($(this).attr("href"), 640, 200, $(this));
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
                var SalePrice = parseFloat($(PriceIndex).find(".SalePrice").val());

                var Subtotal = Quantity * SalePrice;

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
                //$(".First" + $(this).attr("ParentCategoryID")).find(".Subtotal").each(function () {
                //    TotalNum += parseFloat($(this).val());
                //});

            });


            //单价改变时
            $(".SalePrice").change(function () {

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
                var SalePrice = parseFloat($(PriceIndex).find(".SalePrice").val());

                var Subtotal = Quantity * SalePrice;

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
                //$(".First" + $(this).attr("ParentCategoryID")).find(".Subtotal").each(function () {
                //    TotalNum += parseFloat($(this).val());
                //});

            });

        });


        //计算销售总价
        function SetTotalAmount() {
            var TotalAmount = 0;
            $(".Subtotal").each(function () {
                if ($(this).val() != "") {
                    TotalAmount = TotalAmount + parseFloat($(this).val());
                }
            });
            $("#txtAggregateAmount").attr("value", TotalAmount);
            $("#hideAggregateAmount").attr("value", TotalAmount);
            $("#txtRealAmount").attr("value", TotalAmount);
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


        function ShowFileShowPopu(QuotedID, ChangeID) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceItemFileList.aspx?QuotedID=" + QuotedID + "&ChangeID=" + ChangeID;
            showPopuWindows(Url, 700, 800, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //上传图片
        function ShowFileUploadPopu(QuotedID, Kind) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceImageUpload.aspx?QuotedID=" + QuotedID + "&Kind=" + Kind;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        function ShowPrint() {
            window.open('QuotedPriceShow.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>');
        }

        function BeginPrint() {
            window.open('QuotedPriceShowOrPrint.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>');
            return false;
        }
        $(window).load(function () {
            if ('<%=Request["OnlyView"]%>') {
                $("input,textarea,select").attr("disabled", "disabled");
                $("input[type=button],.btn").hide();
                $(".needshow").show();
            }
        });



            function ShowEmployeePopu(Parent) {
                var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
                showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
                $("#SelectEmpLoyeeBythis").click();
            }



            function ShowEmployeePopu1(Parent) {
                var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideSaleEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
                showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
                $("#SelectEmpLoyeeBythis").click();
            }

            function ShowEmployeePopu2(Parent) {
                var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideMissionManager&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
                showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
                $("#SelectEmpLoyeeBythis").click();
            }

            function ShowEmployeeProduct() {
                var Url = "/AdminPanlWorkArea/Foundation/FD_Storehouse/FD_StroehouseProductList.aspx";
                showPopuWindows(Url, 800, 800, "#SelectEmpLoyeeBythis");
                $("#SelectEmpLoyeeBythis").click();
            }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <asp:HiddenField ID="HideSaleSplit" runat="server" ClientIDMode="Static" />

    <asp:HiddenField ID="hideDisStatehide" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hideDisState" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HideHaveEmployee" runat="server" ClientIDMode="Static" />

    <asp:HiddenField ID="hideRowKey" runat="server" ClientIDMode="Static" />
    <br />

    <div class="divider-vertical">
        <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
        <div style="overflow-x: auto; overflow-y: hidden; margin-left: 60px; text-align: center;">
            <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound" OnItemCommand="repfirst_ItemCommand">
                <ItemTemplate>
                    <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                    <asp:HiddenField ID="hideKey" Value='<%#Eval("ChangeID") %>' runat="server" />
                    <table class="First<%#Eval("CategoryID") %> table-select" border="1">
                        <tr>
                            <td width="100" style="white-space: nowrap;"><b>类别</b></td>
                            <td width="110" style="white-space: nowrap;"><b>项目</b></td>
                            <td width="140" style="white-space: nowrap;"><b>产品/服务内容</b></td>
                            <td width="220" style="white-space: nowrap;"><b>具体要求</b></td>
                            <td width="100" style="white-space: nowrap;"><b>附件</b></td>
                            <td width="75" style="white-space: nowrap;"><b>单价</b></td>
                            <td width="80" style="white-space: nowrap;"><b>单位</b></td>
                            <td width="80" style="white-space: nowrap;"><b>数量</b></td>
                            <td width="80" style="white-space: nowrap;"><b>小计</b></td>
                            <td width="180" style="white-space: nowrap;"><b>说明</b></td>
                            <td width="80" style="white-space: nowrap; display: none"><b>操作</b></td>
                        </tr>
                        <asp:Repeater ID="repdatalist" runat="server" OnItemCommand="repdatalist_ItemCommand">
                            <ItemTemplate>
                                <tr id='CreateEdit<%#Eval("ChangeID")%>' onclick="javascript:SetRowKey('CreateEdit<%#Eval("ChangeID")%>')">
                                    <td <%#Container.ItemIndex>0?"  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" : "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:'nowrap;'"%> <%#Eval("ParentCategoryID")%>>
                                        <b <%#Container.ItemIndex>0?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b>
                                        <br />

                                        <asp:HiddenField ID="hidePriceKey" Value='<%#Eval("ChangeID") %>' runat="server" />
                                        <asp:HiddenField ID="hideCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                                    </td>
                                    <td <%#Eval("ItemLevel").ToString()=="3"?"style='border-top-style:none;border-bottom-style:none;word-break:break-all;'": "style='border-top-color:black;border-top-style:double;border-bottom-style:none;word-break:break-all;'" %>>
                                        <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b><br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        <%#Eval("ServiceContent")%>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox Style="margin: 0; padding: 0;" ID="txtRequirement" runat="server" Width="100%" Height="100%" ReadOnly="true" Text='<%#Eval("Requirement") %>' check="0" TextMode="MultiLine"></asp:TextBox></td>
                                    <td>
                                        <a href="#" onclick="ShowFileUploadPopu('<%#Eval("QuotedID") %>','<%#Eval("ChangeID") %>')" class="btn btn-mini   btn-primary">上传</a>
                                        <a href='#' onclick="ShowFileShowPopu('<%#Eval("QuotedID") %>','<%#Eval("ChangeID") %>')" class="btn btn-mini  btn-primary needshow">查看</a>
                                    </td>
                                    <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>">
                                        <asp:TextBox ID="txtSalePrice" ReadOnly="true" CssClass="SalePrice" MaxLength="8" runat="server" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("UnitPrice") %>' Width="75"></asp:TextBox></td>
                                    <td><%#Eval("Unit") %></td>
                                    <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW"%>">
                                        <asp:TextBox ID="txtQuantity" ReadOnly="true" CssClass="Quantity" MaxLength="8" runat="server" Width="75" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("Quantity") %>'></asp:TextBox>
                                        <asp:HiddenField ID="hiddenAvailableCount" Value='<%#GetAvailableCount(Eval("ProductID"),Eval("RowType")) %>' runat="server" />
                                    </td>
                                    <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %> Total<%#Eval("ParentCategoryID") %>">
                                        <asp:TextBox ID="txtSubtotal" ReadOnly="true" class='Subtotal' MaxLength="8" runat="server" Width="75" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW"' Text='<%#Eval("Subtotal") %>'></asp:TextBox>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtRemark" runat="server" Width="90%" Text='<%#Eval("Remark") %>' TextMode="MultiLine"></asp:TextBox></td>
                                    <td style="display: none;">
                                        <asp:LinkButton ID="lnkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("ChangeID") %>' runat="server" OnClientClick="return CheckDelete();" CssClass="btn btn-danger ">删除</asp:LinkButton></td>
                                </tr>

                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <div style="margin-right: 268px; padding: 2px; margin-top: auto; text-align: right;">
                        <span>分项合计：
                    <input id='txtTotal<%#Eval("CategoryID") %>' style="width: 75px;" disabled="disabled" type="text" class="ItemAmount" value="<%#Eval("ItemAmount")==null?"0":Eval("ItemAmount")%>" />
                            <div style="display: none;">销售价：<asp:TextBox ID="txtSaleItem" CssClass="SaleItem" Width="75" runat="server" Text='<%#Eval("ItemSaleAmount") %>'></asp:TextBox></div>
                            <asp:Button ID="btnSaveItem" CommandName="SaveItem" runat="server" Text="保存分项" CssClass="btn btn-success" />
                        </span>

                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="div_Bottom" style="text-align: center;">
            <asp:Button runat="server" ID="btn_BackOrder" Text="打回订单" OnClientClick="return confirm('你确认要打回该订单吗？');" CssClass="btn btn-primary" OnClick="btn_BackOrder_Click" />
            <asp:Button runat="server" ID="btn_Save" Text="保存" CssClass="btn btn-primary" OnClick="btn_Save_Click" />
            <asp:Button runat="server" ID="btn_Confirm" Text="确认派工" CssClass="btn btn-success" OnClick="btn_Confirm_Click" />
        </div>
        <div style="overflow-y: auto; height: 1650px;">
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
