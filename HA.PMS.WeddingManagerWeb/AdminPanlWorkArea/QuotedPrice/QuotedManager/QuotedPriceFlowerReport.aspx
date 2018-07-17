<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceFlowerReport.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager.QuotedPriceFlowerReport" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>


<%@ Register Src="~/AdminPanlWorkArea/Control/CustomerTitle.ascx" TagPrefix="HA" TagName="CustomerTitle" %>
<%@ Register Src="/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc1" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
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
            $("a#inline").fancybox();
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

        //显示文件列表
        function ShowFileShowPopu(QuotedID, ChangeID) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceItemFileList.aspx?QuotedID=" + QuotedID + "&ChangeID=" + ChangeID + "&OnlyView=1";
            showPopuWindows(Url, 700, 800, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //上传图片
        function ShowFileUploadPopu(QuotedID, Kind) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceImageUpload.aspx?QuotedID=" + QuotedID + "&Kind=" + Kind;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function CloseWindown() {
            window.location.href = "/AdminPanlWorkArea/QuotedPrice/QuotedManager/QuotedPriceflowerlist.aspx";
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <input id="Button1" style="margin: 0; margin-bottom: 5px" type="button" value="返回" class="btn btn-info" onclick="CloseWindown();" />
    <input id="btnPrint" type="button" value="打印报价单" class="btn" style="display: none;" />
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <hr style="padding: 0; margin: 0" />
    <div id="PrintNode">
        <div>
            <asp:Literal ID="lblTop" runat="server" Text=""></asp:Literal>
        </div>
        <uc1:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />
        <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound">
                <ItemTemplate>
                    <table class="table table-bordered table-striped First<%#Eval("CategoryID") %>">
                        <tr>
                            <th style="width: 180px;white-space: nowrap">类别</td>
                            <th style="width: 180px;white-space: nowrap">项目</td>
                            <th style="width: 180px;white-space: nowrap">产品/服务内容</td>
                            <th style="width: auto;white-space: nowrap">具体要求</td>
                            <th style="width: 40px;white-space: nowrap">附件</td>
                            <th style="width: 75px;white-space: nowrap">单价</td>
                            <th style="width: 75px;white-space: nowrap">单位</td>
                            <th style="width: 75px;white-space: nowrap">数量</td>
                            <th style="width: 75px;white-space: nowrap">小计</td>
                            <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                        </tr>
                        <asp:Repeater ID="repdatalist" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td <%#Container.ItemIndex>0?"class='NeedHide'  style='white-space:nowrap;text-align:center;'" : "style='white-space:nowrap;text-align:center;'"%> <%#Eval("ParentCategoryID")%>>
                                        <b <%#Container.ItemIndex>0?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b><br />
                                    </td>
                                    <td <%#Eval("ItemLevel").ToString()=="3"?"style='white-space:nowrap;text-align:center;'": "style='white-space:nowrap;text-align:center;'" %>>
                                        <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b><br />
                                    </td>
                                    <td><%#Eval("ServiceContent")%></td>
                                    <td><%#Eval("Requirement") %></td>
                                    <td>
                                        <%--<a id="inline" href="#data<%#Eval("ChangeID") %>" kesrc="#data<%#Eval("ChangeID")%>" <%#HideforNoneImage(Eval("ChangeID")) %>>查看</a>--%>
                                        <a href='#' onclick="ShowFileShowPopu('<%#Eval("QuotedID") %>','<%#Eval("ChangeID") %>')" class="btn btn-mini btn-info">查看</a>
                                        <div style="display: none;">
                                            <div id='data<%#Eval("ChangeID") %>'><%#GetKindImage(Eval("ChangeID")) %></div>
                                        </div>
                                    </td>
                                    <td class="SetSubtotal<%#Eval("ProductID") %>"><%#Eval("UnitPrice") %></td>
                                    <td style="text-align: center;"><%#Eval("Unit") %></td>
                                    <td style="text-align: center;" class="SetSubtotal<%#Eval("ProductID") %>"><%#Eval("Quantity") %></td>
                                    <td class="SetSubtotal<%#Eval("ProductID") %> Total<%#Eval("ParentCategoryID") %>"><%#Eval("Subtotal") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr><td colspan="9" style="text-align:right;padding-right:2em">分项合计：<%#Eval("ItemAmount")%></td></tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        <table class="table table-bordered table-striped">
                <tr>
                    <td>报价单总价</td>
                    <td><asp:Label ID="txtAggregateAmount" runat="server" ClientIDMode="Static" Enabled="false"></asp:Label>
                        <asp:HiddenField ID="hideAggregateAmount" ClientIDMode="Static" runat="server" />
                    </td>
                    <td>销售总价</td>
                    <td><asp:Label ID="txtRealAmount" runat="server" ClientIDMode="Static"></asp:Label></td>
                    <td>定金</td>
                    <td><asp:Label ID="lblOrderMoney" runat="server" Text=""></asp:Label></td>
                    <td>首期预付款</td>
                    <td><asp:Label ID="txtEarnestMoney" runat="server" ClientIDMode="Static"></asp:Label></td>
                    <td>余款</td>
                    <td><asp:Label ID="lblyukuan" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        <div>
                <asp:Literal ID="lblBottom" runat="server" Text=""></asp:Literal>
            </div>
    </div>
</asp:Content>
