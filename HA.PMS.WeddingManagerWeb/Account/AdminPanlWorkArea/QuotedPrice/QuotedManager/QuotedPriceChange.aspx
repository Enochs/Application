<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceChange.aspx.cs" StylesheetTheme="Default" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager.QuotedPriceChange" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ID="Conten1" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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

            showPopuWindows($(".SelectPG").attr("href"), 500, 300, "a.SelectPG");
            $(".NeedHideLable").hide();


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

        $(window).load(function () {
            if ('<%=Request["OnlyView"]%>') {
                $("input,textarea,select").attr("disabled", "disabled");
                $("input[type=button],.btn").hide();
                $(".needshow").show();
            }
        });


        function ShowEmployeePopu1(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmployeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
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
        //打开打印界面
        function BeginPrint() {
            var Index = document.getElementById("HideIndx").value;
            window.open('/AdminPanlWorkArea/QuotedPrice/QuotedPriceShowOrPrint.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>&IsFirstMake=' + Index);
            return false;
        }
    </script>

    <style type="text/css">
        .btn-success {
            margin: 10px 0px 10px 10px;
        }
    </style>

    <a id="SelectEmpLoyeeBythis" href="#" style="display: none;"></a>
    <div class="panel panel-default">
        <div runat="server" id="div_Button"></div>
        <div id="Div6" class="panel-collapse collapse in">
            <table border="0" style="width: 1325px; margin-left: 60px; margin-right: 60px;" class="table-select">
                <tr>
                    <td style="text-align: left; vertical-align: middle;">
                        <a runat="server" id="SelectPG" class="SelectPG btn btn-warning" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=0&ControlKey=hidePgValue&Callback=btnStarFirstpg"><b style="color: black;">添加类别</b></a>
                        变更时间：<asp:Label runat="server" ID="lblChangeDate" Text="" />
                        &nbsp;操作人：<asp:Label runat="server" ID="lblChangeEmployee" Text="" />
                        <div style="display: none;">
                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hidePgValue" />
                            <asp:Button ID="btnStarFirstpg" ClientIDMode="Static" runat="server" Text="生成一级分类" OnClick="btnStarFirstpg_Click" />
                            <asp:Button ID="btnCreateSecond" ClientIDMode="Static" runat="server" Text="生成二级分类" OnClick="btnCreateSecond_Click" />
                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSecondValue" />
                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSecondCategoryID" />
                            <asp:Button ID="btnCreateThired" ClientIDMode="Static" runat="server" Text="生成产品" OnClick="btnCreateThired_Click" />
                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdValue" />
                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdCategoryID" />
                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideFoureGingang" />
                            <asp:HiddenField runat="server" ID="hideIschek" />

                            <asp:HiddenField runat="server" ID="HideFlowerSplit" ClientIDMode="Static" />

                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <div style="overflow-x: auto; overflow-y: hidden; margin-left: 60px; text-align: center;">
                <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound" OnItemCommand="repfirst_ItemCommand">
                    <ItemTemplate>
                        <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                        <asp:HiddenField ID="hideKey" Value='<%#Eval("ChangeID") %>' runat="server" />
                        <table class="First<%#Eval("CategoryID") %> table-select" border="1">
                            <tr>
                                <td width="120" style="white-space: nowrap;"><b>类别</b></td>
                                <td width="150" style="white-space: nowrap;"><b>项目</b></td>
                                <td width="140" style="white-space: nowrap;"><b>产品/服务内容</b></td>
                                <td width="380" style="white-space: nowrap;"><b>具体要求</b></td>
                                <td width="100" style="white-space: nowrap;"><b>附件</b></td>
                                <td width="75" style="white-space: nowrap;"><b>单价</b></td>
                                <td width="80" style="white-space: nowrap;"><b>单位</b></td>
                                <td width="80" style="white-space: nowrap;"><b>数量</b></td>
                                <td width="80" style="white-space: nowrap;"><b>小计</b></td>
                                <td width="150" style="white-space: nowrap; display: none;"><b>说明</b></td>
                                <td width="80" style="white-space: nowrap;"><b>操作</b></td>
                            </tr>
                            <asp:Repeater ID="repdatalist" runat="server" OnItemCommand="repdatalist_ItemCommand">
                                <ItemTemplate>
                                    <tr id='CreateEdit<%#Eval("ChangeID")%>' onclick="javascript:SetRowKey('CreateEdit<%#Eval("ChangeID")%>')">
                                        <td <%#Container.ItemIndex>0?"class='NeedHide'  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" : "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:'nowrap;'"%> <%#Eval("ParentCategoryID")%>>
                                            <b <%#Container.ItemIndex>0?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b>
                                            <br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<a class="SelectSG btn btn-primary" id="SelectSG" <%#HideSelectItem(Container.ItemIndex,Eval("IsFinishMake")) %> categoryid="<%#Eval("CategoryID") %>" href='/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=<%#Eval("ItemLevel").ToString()=="1" ? Eval("CategoryID") :Eval("ParentCategoryID") %>&ControlKey=hideSecondValue&Callback=btnCreateSecond' onclick="return SetfunCategoryID(this);"><span style="color: white">增加项目</span></a>
                                            <asp:HiddenField ID="hidePriceKey" Value='<%#Eval("ChangeID") %>' runat="server" />
                                            <asp:HiddenField ID="hideCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                                        </td>
                                        <td <%#Eval("ItemLevel").ToString()=="3"?"style='border-top-style:none;border-bottom-style:none;word-break:break-all;'": "style='border-top-color:black;border-top-style:double;border-bottom-style:none;word-break:break-all;'" %>>
                                            <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b><br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;<a class="SelectSG btn  btn-primary " id="A2" <%#HideSelectProduct(Eval("ItemLevel")) %> categoryid="<%#Eval("CategoryID") %>" <%#Eval("IsFinishMake").ToString() == "0" ? "style='display:block;'" : "style='display:none;'" %> href="/AdminPanlWorkArea/ControlPage/SelectProduct.aspx?CategoryID=<%#Eval("CategoryID") %>&ControlKey=hideThirdValue&Callback=btnCreateThired&PartyDate=<%#Request["PartyDate"] %>&CustomerID=<%=Request["CustomerID"] %>" onclick="SetProduct(this);">增加产品</a>
                                        </td>
                                        <td style="word-break: break-all;"><%#Eval("ServiceContent")%><div style="display: none;">
                                            <asp:TextBox ID="txtProductName" Enabled="false" runat="server" Text='<%#Eval("ServiceContent") %>' Width="120"></asp:TextBox>
                                        </div>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox Style="margin: 0; padding: 0;" ID="txtRequirement" runat="server" Width="100%" Height="100%" Text='<%#Eval("Requirement") %>' check="0" TextMode="MultiLine"></asp:TextBox></td>
                                        <td>
                                            <a href="#" onclick="ShowFileUploadPopu('<%#Eval("QuotedID") %>','<%#Eval("ChangeID") %>')" class="btn btn-mini   btn-primary">上传</a>
                                            <%--<a id="inline" href="#data<%#Eval("ChangeID") %>" class="btn " kesrc="#data<%#Eval("ChangeID")%>" <%#HideforNoneImage(Eval("ChangeID")) %>>查看</a>--%>
                                            <a href='#' onclick="ShowFileShowPopu('<%#Eval("QuotedID") %>','<%#Eval("ChangeID") %>')" class="btn btn-mini  btn-primary needshow">查看</a>
                                            <div style="display: none;">
                                                <div id='data<%#Eval("ChangeID") %>'><%#GetKindImage(Eval("ChangeID")) %></div>
                                            </div>
                                        </td>
                                        <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>">
                                            <asp:TextBox ID="txtSalePrice" CssClass="SalePrice" MaxLength="8" runat="server" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("UnitPrice") %>' Width="75"></asp:TextBox></td>
                                        <td><%#Eval("Unit") %></td>
                                        <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW"%>">
                                            <asp:TextBox ID="txtQuantity" CssClass="Quantity" MaxLength="8" runat="server" Width="75" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("Quantity") %>'></asp:TextBox>
                                            <asp:HiddenField ID="hiddenAvailableCount" Value='<%#GetAvailableCount(Eval("ProductID"),Eval("RowType")) %>' runat="server" />
                                        </td>
                                        <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %> Total<%#Eval("ParentCategoryID") %>">
                                            <asp:TextBox ID="txtSubtotal" class='Subtotal' MaxLength="8" runat="server" Width="75" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW"' Text='<%#Eval("Subtotal") %>'></asp:TextBox>
                                        </td>
                                        <td style="display: none;">
                                            <asp:TextBox ID="txtRemark" runat="server" Width="140" Text='<%#Eval("Remark") %>' MaxLength="50"></asp:TextBox></td>
                                        <td <%#Eval("IsFinishMake").ToString() == "0" ? "style='display:block;'" : "style='display:none;'" %>>
                                            <asp:LinkButton ID="lnkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("ChangeID") %>' runat="server" OnClientClick="return CheckDelete();" CssClass="btn btn-danger">删除</asp:LinkButton></td>
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

                <script type="text/javascript">
                    $(document).ready(function () {
                        $("#txtDiscountPrice").focusout(function () {
                            var mPrice = $("#txtDiscountPrice").val();
                            var pPrice = $("#txtPersonPrice").val();
                            var oPrice = $("#txtOtherPrice").val();
                            var sum = mPrice * 1 + pPrice * 1 + oPrice * 1;
                            document.getElementById("<%=txtRealAmount.ClientID%>").value = sum;
                        });

                    });

                </script>

                <table>
                    <tr style="margin-bottom: 50px;">
                        <td>物料总价格：</td>
                        <td>
                            <asp:TextBox Style="margin: 0; padding: 0;" runat="server" ID="txtMaterialPrice" ClientIDMode="Static" Width="90" Enabled="false" /></td>
                        <td>物料销售价：</td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox Style="margin: 0; padding: 0;" runat="server" ID="txtDiscountPrice" Width="90" ClientIDMode="Static" Enabled="false" />
                                    <asp:Button runat="server" ID="btnIsLock" Text="解锁" OnClick="btnIsLock_Click" ToolTip="目前加锁状态,不能修改销售价格" CssClass="btn btn-primary btn-mini" ClientIDMode="Static" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>人员价格：
                            <asp:TextBox runat="server" ID="txtPersonPrice" Style="margin: 0; padding: 0;" Width="90" Enabled="false" ClientIDMode="Static" />
                        </td>
                        <td>其他价格：
                            <asp:TextBox runat="server" ID="txtOtherPrice" Style="margin: 0; padding: 0;" Width="90" Enabled="false" ClientIDMode="Static" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 10px"></td>
                    </tr>
                    <tr>
                        <td colspan="6" align="left">
                            <a target="_blank" href='../QuotedPriceShowOrPrint.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>&IsFirstMake=<%=GetIsFirstMake() %>&Type=2' class="btn btn-primary">打印物料单</a>

                            <a target="_blank" href='../QuotedPriceShowOrPrint.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>&IsFirstMake=<%=GetIsFirstMake() %>&Type=1' class="btn btn-primary">打印人员单</a>

                            <a target="_blank" href='../QuotedPriceShowOrPrint.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>&IsFirstMake=<%=GetIsFirstMake() %>&Type=3' class="btn btn-primary">打印其它</a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" style="height: 10px"></td>
                    </tr>
                    <tr>

                        <td>销售总价：</td>
                        <td>
                            <asp:TextBox Style="margin: 0; padding: 0;" ID="txtRealAmount" Width="90" runat="server" ClientIDMode="Static" MaxLength="10" Enabled="false"></asp:TextBox></td>
                        <td></td>
                        <td>
                            <asp:Label ID="lblOrderMoney" runat="server" Text="" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <div class="divHandle">

                <table style="margin-left: 50px;">
                    <tr>
                        <td colspan="2">
                            <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-primary" Text="保存" OnClick="btnCancel_Click" />
                            <asp:Button runat="server" ID="btnConfirm" ClientIDMode="Static" CssClass="btn btn-primary" Text="确认派工" OnClick="btnConfirm_Click" />
                            <asp:HiddenField runat="server" ID="HideIndx" ClientIDMode="Static" Value="1" />
                            <a runat="server" id="btnPrint" visible="false" class="btn btn-danger" onclick="BeginPrint()">打印</a>

                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
