<%@ Page Title="变更单明细" Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="QuotedPriceChangeList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceChangeList" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MyCarryTask.ascx" TagPrefix="HA" TagName="MyCarryTask" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <script type="text/javascript">
        $(document).ready(function () {
            $(".ABCD1").remove();
            $("a#inline").fancybox();
            showPopuWindows($("#SelectPG").attr("href"), 400, 300, "a#SelectPG");
            $(".NeedHideLable").hide();
        });

        $(document).ready(function () {
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


      

        ///计算小计
        function GetAvgSum(Control) {
            $(".SetSubtotal").find();
            $(".SetSubtotal").find();
            $(".SetSubtotal").find();
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




    </script>

    <style type="text/css">
        .btn-success {
            margin-right: 15px;
        }
       
    </style>

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <div runat="server" id="div_Button">
    </div>

    <table id="FirstTable" border="0" style="border-color: #000000; width: 90%;">
        <tr>
            <td>
                <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound">
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
                                <td style="width: 105px;"><b>图片</b></td>
                                <td style="width: 50px;"><b>销售价</b></td>
                                <td style="width: 50px;"><b>成本价</b></td>
                                <td style="width: 50px; word-break: break-all; overflow: hidden; word-wrap: break-word;"><b>单位</b></td>
                                <td style="width: 50px;"><b>数量</b></td>
                                <td style="width: 50px;"><b>小计</b></td>
                                <td style="width: 150px;"><b>备注</b></td>
                                <td style="display: none;"><b>责单位</b></td>
                                <td><b>责任单位</b></td>
                            </tr>
                            <asp:Repeater ID="repdatalist" runat="server">
                                <ItemTemplate>
                                    <tr <%#GetByQuoted(Eval("DispatchingID"),Eval("ProductID")).ToString().ToInt32() > 0 ? "style='color:red;font-weight:bold;'" : "" %>>
                                        <td <%#Eval("ItemLevel").ToString()=="1"?"style='border-top-style:double;border-bottom-style:none;white-space:nowrap;'" : "class='NeedHide'  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'"%> <%#Eval("ParentCategoryID")%>>

                                            <b <%#Eval("ItemLevel").ToString()!="1"?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b><br />
                                            <asp:HiddenField ID="hidePriceKey" Value='<%#Eval("ProeuctKey") %>' runat="server" />
                                            <asp:HiddenField ID="hideCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                                        </td>
                                        <td <%#Eval("ItemLevel").ToString()=="2"?"style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:nowrap;'":"style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" %>>
                                            <b <%#HideSelectProduct(Container.ItemIndex) %>><%#Eval("CategoryName") %></b>

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
                                                 <a href="#" onclick="ShowFileUploadPopu('<%#GetQuotedID(Eval("DispatchingID")) %>','<%#GetChangeId(Eval("ProductID")) %>')">上传</a>
                                            <a href='#' onclick="ShowFileShowPopu('<%#GetQuotedID(Eval("DispatchingID")) %>','<%#GetChangeId(Eval("ProductID")) %>')">查看</a>


                                        </td>

                                        <td class="SetSubtotal<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>">

                                            <asp:TextBox ID="txtSalePrice" Style="margin: 0" MaxLength="8" CssClass="SalePrice" runat="server" ProductID='<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>' Text='<%#Eval("UnitPrice") %>' Width="75"></asp:TextBox>

                                        </td>
                                        <td class="SetSubtotal<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>">

                                            <asp:TextBox ID="txtPurchasePrice" Style="margin: 0" MaxLength="8" CssClass="OldPrice" runat="server" ProductID='<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>' Text='<%#Eval("PurchasePrice") %>' Width="75"></asp:TextBox>

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
                                            <asp:TextBox ID="txtRemark" TextMode="MultiLine" runat="server" Width="85%" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                                        <td id="Partd<%#Guid.NewGuid().ToString() %>" style="display: none;">
                                            <input style="width: 45px;" maxlength="10" runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" type="text" value='<%#GetEmployeeName(Eval("EmployeeID")) %>' />


                                        </td>
                                        <td style="width: auto; white-space: nowrap; text-align: left;" id="Row<%#Guid.NewGuid().ToString() %>">
                                            <input runat="server" maxlength="10" id="txtSuppName" type="text" class="txtSuppName " value='<%#Eval("SupplierName") %>' />
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
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Label ID="lblSumQuantity" runat="server" Text=""></asp:Label></td>
                                <td>
                                    <asp:Label ID="lblSumMoney" runat="server" Text=""></asp:Label></td>
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

    <HA:MessageBoard runat="server" ClassType="QuotedPriceList" ID="MessageBoard" />
</asp:Content>
