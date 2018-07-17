<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskShow" Title="查看派工单" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">

    <script type="text/javascript">
        $(document).ready(function () {
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
                //if (OldTexValue != "") {
                //    OLdValue = parseFloat(OldTexValue);
                //}

                var NewValue = 0;

                var Quantity = parseFloat($(PriceIndex).find(".Quantity").val());
                var SalePrice = parseFloat($(PriceIndex).find(".SalePrice").val());

                var Subtotal = Quantity * SalePrice;
                //var OldTotal = parseFloat($("#txtAggregateAmount").attr("value"));
                //if ($(PriceIndex).find(".Subtotal").val() == "") {
                //    OLdValue = Subtotal + OLdValue;
                //    //OldTotal = Subtotal + OLdValue;
                //} else {
                //    OLdValue = OLdValue - parseFloat($(PriceIndex).find(".Subtotal").val());
                //    OLdValue = OLdValue + Subtotal;
                //}

                $(PriceIndex).find(".Subtotal").attr("value", Subtotal);
                //$(ParetnTotal).attr("value", OLdValue);

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

        //隐藏合并
        function SetHidden() {



        }


        ///打印
        function Print(QuotedID) {

        }


        //预览打印
        function ShowPrint(QuotedID) {

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

        //上传图片
        function ShowFileUploadPopu(QuotedID, Kind) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceImageUpload.aspx?QuotedID=" + QuotedID + "&Kind=" + Kind;
            showPopuWindows(Url, 400, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>

    <br />
    <div style="overflow-x: auto;">
        <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound">
            <ItemTemplate>
                <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                <asp:HiddenField ID="hideKey" Value='<%#Eval("ProeuctKey") %>' runat="server" />
                <table class="table table-bordered table-striped First<%#Eval("CategoryID") %>" border="1" style="border-color: gray;">
                    <tr>
                        <td style="width: 80px; white-space: nowrap;"><b>类别</b></td>
                        <td style="width: 80px; white-space: nowrap;"><b>项目</b></td>
                        <td style="width: 80px; white-space: nowrap;"><b>产品/服务内容</b></td>
                        <td style="width: 80px; white-space: nowrap;"><b>具体要求</b></td>
                        <td style="width: 80px; white-space: nowrap;"><b>图片</b></td>
                        <td style="width: 45px; white-space: nowrap;"><b>单价</b></td>
                        <td style="width: 45px; white-space: nowrap;"><b>单位</b></td>
                        <td style="width: 30px; white-space: nowrap;"><b>数量</b></td>
                        <td style="width: 50px; white-space: nowrap;"><b>小计</b></td>
                        <td style="width: 0px; white-space: nowrap; display: none;"><b>说明</b></td>
                        <td style="width: 50px; white-space: nowrap;"><b>责任人</b></td>
                    </tr>
                    <asp:Repeater ID="repdatalist" runat="server" OnItemCommand="repdatalist_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td <%#Container.ItemIndex>0?"class='NeedHide'  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" : "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:nowrap;'"%> <%#Eval("ParentCategoryID")%>>
                                    <b <%#Container.ItemIndex>0?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b><br />
                                </td>
                                <td <%#Eval("ItemLevel").ToString()=="3"?"style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'": "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:nowrap;'" %>>
                                    <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b>
                                    <td>
                                        <%#GetProductByID(Eval("ProductID"))%>
 
                                    </td>
                                    <td>
                                        <%#Eval("Requirement") %>

                                    </td>
                                    <td><a href="#" onclick="ShowFileUploadPopu('<%#Eval("DispatchingID") %>','<%#Eval("ProeuctKey") %>')">上传图片</a></td>
                                    <td class="SetSubtotal<%#Eval("ProductID") %>">
                                      <%#Eval("UnitPrice") %></td>
                                    <td>
                                        <%#Eval("Unit") %></td>
                                    <td class="SetSubtotal<%#Eval("ProductID") %>">
                                       <%#Eval("Quantity") %></td>
                                    <td class="SetSubtotal<%#Eval("ProductID") %> Total<%#Eval("ParentCategoryID") %>">
                                       <%#Eval("Subtotal") %></td>
                                    <td style="display: none;">
                                      <%#Eval("Remark")%></td>
                                    <td id="Partd<%#Container.ItemIndex %>">
                                       <%#GetEmployeeName(Eval("EmpLoyeeID")) %> 
                                    </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </ItemTemplate>
        </asp:Repeater>

        <br />
    </div>
</asp:Content>
