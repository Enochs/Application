<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceShowOrPrint.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceShowOrPrint" Title="打印或者展示" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>


<%@ Register Src="../Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc1" %>


<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link rel="stylesheet" href="css/jquery.gritter.css" />
    <link href="/Scripts/Function/jquery.fancybox.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.7.1.js"></script>

    <script src="/Scripts/jquery.metadata.js"></script>

    <script src="/Scripts/messages_cn.js"></script>

    <script src="/Scripts/Function/jquery.fancybox.pack.js"></script>
    <script src="/Scripts/Function/masterFunction.js"></script>

    <link href="/Scripts/UI/css/ui-lightness/jquery-ui-1.10.2.custom.css" rel="stylesheet" />
    <script src="/Scripts/UI/js/jquery-ui-1.10.2.custom.js"></script>
    <%--<script src="/Scripts/UI/development-bundle/ui/i18n/jquery.ui.datepicker-zh-CN.js"></script>--%>

    <script src="/Scripts/superValidator.js"></script>
    <script src="/Scripts/Validator.js"></script>
    <link href="/Scripts/Tooltip.css" rel="stylesheet" />

    <script src="/Scripts/DatePicker/WdatePicker.js"></script>
    <script src="/Scripts/DatePicker/calendar.js"></script>
    <script src="/Scripts/DatePicker/config.js"></script>
    <style type="text/css">
        .tab {
            border-top-width: 1px;
            border-left-width: 1px;
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-top-color: #FF0000;
            border-left-color: #FF0000;
            border-right-color: #FF0000;
            border-bottom-color: #FF0000;
            border-top-style: solid;
            border-left-style: solid;
            border-right-style: solid;
            border-bottom-style: solid;
        }

        .tab_right_bottom {
            border-right-width: 1px;
            border-right-color: #FF0000;
            border-right-style: solid;
            border-bottom-width: 1px;
            border-bottom-color: #FF0000;
            border-bottom-style: solid;
        }

        .tab_right {
            border-right-width: 1px;
            border-right-color: #FF0000;
            border-right-style: solid;
        }

        .tab_bottom {
            border-bottom-width: 1px;
            border-bottom-color: #FF0000;
            border-bottom-style: solid;
        }
    </style>
    <script src="/Scripts/jquery.PrintArea.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnPrint").click(function () {

                var mode = "popup";
                var close = mode == "popup";

                var options = { mode: mode, popClose: close };

                $("#PrintNode").printArea(options);
            });

            showPopuWindows($("#SelectPG").attr("href"), 400, 300, "a#SelectPG");
            $(".NeedHideLable").hide();
        });

        $(document).ready(function () {

            $(".SaleItem").change(function () {
                eachAmount();
            });

            $(".SelectSG").each(function () {
                showPopuWindows($(this).attr("href"), 400, 300, $(this));
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



        //上传图片
        function ShowFileUploadPopu(QuotedID, Kind) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceImageUpload.aspx?QuotedID=" + QuotedID + "&Kind=" + Kind;
            showPopuWindows(Url, 400, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function CloseWindown() {
            window.close();
            this.close();
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
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <input id="Button1" type="button" value="退出" class="btn btn-danger" onclick="CloseWindown();" />
    <input id="btnPrint2" type="button" value="打印报价单" class="btn btn-info" onclick="return preview()" />&nbsp;
    <%--<input id="btnPrint" type="button" value="打印报价单" class="btn btn-info" />&nbsp;--%>
    <asp:Button ID="btnExport" runat="server" Text="导出报价单" OnClick="btnExport_Click" />
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <br />
    <!--startprint-->
    <div id="PrintNode" style="border: groove; font-size: 12px;">
        <div>
            <asp:Literal ID="lblTop" runat="server" Text=""></asp:Literal>
        </div>
        <div style="font-size: small;">
            <uc1:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />
        </div>
        <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound">
            <ItemTemplate>
                <table class=" First<%#Eval("CategoryID") %>" border="1" style="border: solid 1px #808080; width: 100%; font-size: 12px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="180" style="white-space: nowrap; text-align: center;"><b>类别</b></td>
                        <td width="180" style="white-space: nowrap; text-align: center;"><b>项目</b></td>
                        <td width="180" style="white-space: nowrap; text-align: center;"><b>产品/服务内容</b></td>
                        <td width="256" style="white-space: nowrap; text-align: center;"><b>具体要求</b></td>
                        <td width="75" style="white-space: nowrap; text-align: center;"><b>单价</b></td>
                        <td width="75" style="white-space: nowrap; text-align: center;"><b>单位</b></td>
                        <td width="75" style="white-space: nowrap; text-align: center;"><b>数量</b></td>
                        <td width="75" style="white-space: nowrap; text-align: center;"><b>小计</b></td>
                        <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                    </tr>
                    <asp:Repeater ID="repdatalist" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td width="180" <%#Container.ItemIndex>0?"class='NeedHide'  style='border-top-style:none;border-bottom-style:none;'" : "style='border-top-color:black;border-top-style:double;border-bottom-style:none;word-break:break-all;'"%> <%#Eval("ParentCategoryID")%>>
                                    <b <%#Container.ItemIndex>0?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b><br />
                                </td>
                                <td width="180" <%#Eval("ItemLevel").ToString()=="3"?"style='border-top-style:none;border-bottom-style:none;word-break:break-all;'": "style='border-top-color:black;border-top-style:double;border-bottom-style:none;word-break:break-all;'" %>>
                                    <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b><br />
                                </td>
                                <td width="180" style="word-break: break-all;"><%#Eval("ServiceContent")%></td>
                                <td width="256" style="word-break: break-all;"><%#Eval("Requirement")%></td>
                                <td width="75" class="SetSubtotal<%#Eval("ProductID") %>" style="white-space: nowrap;">
                                    <%#GetNbsp(Eval("UnitPrice").ToString(),8) %></td>
                                <td width="75" style="white-space: nowrap;">
                                    <%#Eval("Unit") %></td>
                                <td width="75" class="SetSubtotal<%#Eval("ProductID") %>" style="white-space: nowrap;">
                                    <%#GetNbsp(Eval("Quantity").ToString(),4)%></td>
                                <td width="75" class="SetSubtotal<%#Eval("ProductID") %> Total<%#Eval("ParentCategoryID") %>" style="white-space: nowrap;">
                                    <%#GetNbsp(Eval("Subtotal").ToString(),10) %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div style="text-align: right; margin-right: 20px; font-size: 12px;">分项合计：<%#Eval("ItemAmount")%></div>
            </ItemTemplate>
        </asp:Repeater>
        婚礼顾问：<asp:Label ID="lblOrderEmployee" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;策划师：<asp:Label ID="lblQuotedEmployee" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <br />
        <table border="0" style="width: 100%; font-size: 12px;" cellpadding="0" cellspacing="0">
            <tr>
                <td>总金额：<asp:Label ID="txtAggregateAmount" runat="server" ClientIDMode="Static" Enabled="false"></asp:Label></td>
                <td>
                    <asp:HiddenField ID="hideAggregateAmount" ClientIDMode="Static" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="border-style: groove; border-left-style: none; border-right-style: none; border-bottom-style: none;">说明：<br />
                    &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRemark" Text="" />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td colspan="7" style="border-style: groove; border-left-style: none; border-right-style: none; border-bottom-style: none;">
                    <asp:Label ID="lblNode" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <div>
            <asp:Literal ID="lblBottom" runat="server" Text=""></asp:Literal>
        </div>
    </div>
    <!--endprint-->
</asp:Content>
