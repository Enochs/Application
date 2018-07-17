<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceChange.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceChange" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>



<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">


    <script type="text/javascript">
        $(document).ready(function () {
            $("a#inline").fancybox();

            $(".NeedHideLable").hide();

            showPopuWindows($(".SelectPG").attr("href"), 600, 500, "a#SelectPG");
 

        });

        function EachSelectState() {
            var LessTotal = 0;
            var AddTotal = 0;
            var FinishTotal = 0;
            $(".Subtotal").each(function () {
                if ($(this).val() != "") {
                    var ItemTotal = parseFloat($(this).val());

                    if (ItemTotal < 0) {
                        LessTotal += ItemTotal;
                    } else {
                        AddTotal += ItemTotal;
                    }
                }
            });
            var AllMount = parseFloat($("#hideFinishAmount").val());
     
            if ($("#txtFinishAmount").val() != "" && $("#txtFinishAmount").val() != "NaN") {

                FinishTotal = AllMount + parseFloat($("#txtAggregateAmount").val());
            } else {

               
                FinishTotal = AllMount + parseFloat($("#txtAggregateAmount").val());
            }
    
            $("#txtAddedAmount").attr("value", AddTotal);
            $("#txtLessenedAmount").attr("value", LessTotal);
            $("#txtAggregateAmount").attr("value", AddTotal + LessTotal);
            $("#txtFinishAmount").attr("value", AllMount + LessTotal + AddTotal);


            $("#txtLessenedAmount").attr("value", LessenedAmount);
            $("#txtAddedAmount").attr("value", AddAmount);
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
                var SelectState = "#ParentState" + $(this).attr("ParentCategoryID");
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
                var LessenedAmount = 0;


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
                EachSelectState();
            });


            ////计算增项 减项
            //$(".AddState").change(function () {
            //    EachSelectState();
            //});

            //单价改变时
            $(".SalePrice").change(function () {

                var PriceIndex = ".SetSubtotal" + $(this).attr("ProductID");
                var ParetnTotal = "#txtTotal" + $(this).attr("ParentCategoryID");
                var SelectState = "#ParentState" + $(this).attr("ParentCategoryID");
                var TotalNum = 0;
                var OldTexValue = $(ParetnTotal).val();
                var OLdValue = 0;
                if (OldTexValue != "") {
                    OLdValue = parseFloat(OldTexValue);
                }
                //AddState
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
                EachSelectState();
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

            var LessenedAmount = $("#txtLessenedAmount").val();

            if (LessenedAmount != "") {
                AddedAmount = TotalAmount - parseFloat(LessenedAmount);
            } else {
                AddedAmount = 0 - parseFloat(LessenedAmount);
            }

            //$("#txtAggregateAmount").attr("value", TotalAmount);

            //$("#hideAggregateAmount").attr("value", TotalAmount);

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


        function ShowPrint() {
            window.open('QuotedPriceShow.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>');
        }

        $(window).load(function () {
            BindMoney('<%=txtAggregateAmount.ClientID%>:<%=txtLessenedAmount.ClientID%>:<%=txtAddedAmount.ClientID%>');
            BindText(200, '<%=txtRemark.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
        });

        function CheckSuccess()
        {
            if ($("table[class^=First]").length > 0) {
                return ValidateForm('input[check],textarea[check]');
            } else {
                alert("报价单不能为空，至少选择一项！"); return false;
            }
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <br />
    <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
    <table style="width: 100%;" border="0">
        <tr>
            <td align="left">
                <a id="SelectPG" class="SelectPG  btn btn-warning" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=0&ControlKey=hidePgValue&Callback=btnStarFirstpg"><b style="color: red;">添加类别</b></a>
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
                </div>
                &nbsp;&nbsp;&nbsp;&nbsp;
                报价单名称：<asp:TextBox ID="txtQuotedTitle" runat="server" Enabled="false" Width="255px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <div style="overflow-x: auto;">
        <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound" OnItemCommand="repfirst_ItemCommand">
            <ItemTemplate>
                <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                <asp:HiddenField ID="hideKey" Value='<%#Eval("ChangeID") %>' runat="server" />
                <table class="First<%#Eval("CategoryID") %>" border="1" style="border-color: gray;width:99.6%">
                    <tr>
                        <td width="300" style="white-space: nowrap;"><b>类别</b></td>
                        <td width="300" style="white-space: nowrap;"><b>项目</b></td>
                        <td width="140" style="white-space: nowrap;"><b>产品/服务内容</b></td>
                        <td width="140" style="white-space: nowrap;"><b>具体要求</b></td>
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
                            <tr>
                                <td <%#Container.ItemIndex>0?"class='NeedHide'  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" : "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:'nowrap;'"%> <%#Eval("ParentCategoryID")%>>
                                    <b <%#Container.ItemIndex>0?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b>
                                    &nbsp;&nbsp;<a id="SelectSG" <%#HideSelectItem(Container.ItemIndex) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-primary btn-mini" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=<%#Eval("ItemLevel").ToString()=="1"?Eval("CategoryID"):Eval("ParentCategoryID") %>&ControlKey=hideSecondValue&Callback=btnCreateSecond" onclick="return SetfunCategoryID(this);"><span  style="color:white">增加项目</span></a>
                                    <asp:HiddenField ID="hidePriceKey" Value='<%#Eval("ChangeID") %>' runat="server" />
                                    <asp:HiddenField ID="hideCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                                </td>
                                <td <%#Eval("ItemLevel").ToString()=="3"?"style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'": "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:nowrap;'" %>>
                                    <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b>
                                    &nbsp;&nbsp;<a id="A2" <%#HideSelectProduct(Eval("ItemLevel")) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-primary btn-mini" href="/AdminPanlWorkArea/ControlPage/SelectProduct.aspx?CategoryID=<%#Eval("CategoryID") %>&ControlKey=hideThirdValue&Callback=btnCreateThired&CustomerID=<%=Request["CustomerID"] %>" onclick="SetProduct(this);">增加产品</a></td>
                                <td><%#Eval("ServiceContent")%><div style="display: none;"><asp:TextBox ID="txtProductName" Enabled="false" runat="server" Text='<%#Eval("ServiceContent") %>' Width="120"></asp:TextBox></div></td>
                                <td><asp:TextBox style="margin:0;padding:0;" ID="txtRequirement" runat="server" Width="255" Text='<%#Eval("Requirement") %>' MaxLength="50"></asp:TextBox></td>
                                <td><a href="#" onclick="ShowFileUploadPopu('<%#Eval("QuotedID") %>','<%#Eval("ChangeID") %>')" class="btn btn-mini btn-primary">上传</a>
                                    <a href='#' onclick="ShowFileShowPopu('<%#Eval("QuotedID") %>','<%#Eval("ChangeID") %>')" class="btn btn-mini btn-primary">查看</a>
                                </td>
                                <td class="SetSubtotal<%#Eval("ProductID")!=null?Eval("ProductID"):Eval("ChangeID").ToString()+"ROW" %>"><asp:TextBox style="margin:0;padding:0;" ID="txtSalePrice" CssClass="SalePrice" runat="server" Width="75" MaxLength="9" ProductID='<%#Eval("ProductID")!=null?Eval("ProductID"):Eval("ChangeID").ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("UnitPrice") %>'></asp:TextBox></td>
                                <td><%#Eval("Unit") %></td>
                                <td class="SetSubtotal<%#Eval("ProductID")!=null?Eval("ProductID"):Eval("ChangeID").ToString()+"ROW" %>">
                                    <asp:TextBox style="margin:0;padding:0;"  ID="txtQuantity" CssClass="Quantity" runat="server" Width="75" MaxLength="9" ProductID='<%#Eval("ProductID")!=null?Eval("ProductID"):Eval("ChangeID").ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("Quantity") %>'></asp:TextBox></td>
                                <td class="SetSubtotal<%#Eval("ProductID")!=null?Eval("ProductID"):Eval("ChangeID").ToString()+"ROW" %> Total<%#Eval("ParentCategoryID") %>">
                                    <asp:TextBox style="margin:0;padding:0;"  ID="txtSubtotal" class='Subtotal' runat="server" Width="75"  MaxLength="9" ProductID='<%#Eval("ProductID")!=null?Eval("ProductID"):Eval("ChangeID").ToString()+"ROW" %>' Text='<%#Eval("Subtotal") %>'></asp:TextBox></td>
                                <td style="display: none;"><asp:TextBox style="margin:0;padding:0;"  ID="txtRemark" runat="server" Width="140" Text='<%#Eval("Remark") %>' MaxLength="50"></asp:TextBox></td>
                                <td><asp:LinkButton ID="lnkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("ChangeID") %>' runat="server" OnClientClick="return CheckDelete();" CssClass="btn btn-danger btn-mini">删除</asp:LinkButton></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div style="text-align: right;padding:2px;font-weight:800">
                    分项合计：<input id='txtTotal<%#Eval("CategoryID") %>' style="margin:0;padding:0;width:75px;font-weight:800" disabled="disabled" type="text" class="ItemAmount" value="<%#Eval("ItemAmount")==null?"0":Eval("ItemAmount")%>" />
                    <div style="display: none;">
                        销售价:<asp:TextBox ID="txtSaleItem" CssClass="SaleItem" Width="75" runat="server" Text='<%#Eval("ItemSaleAmount") %>'></asp:TextBox>
                    </div>
                    <asp:Button ID="btnSaveItem" CommandName="SaveItem" runat="server" Text="保存" CssClass="btn btn-mini btn-success" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <table style="border:0;column-span:none" cellspacing="0" >
        <tr>
            <td>变更总价：</td>
            <td><asp:TextBox style="padding:0;margin:0" ID="txtAggregateAmount" check="1" runat="server" ClientIDMode="Static" MaxLength="8"></asp:TextBox></td>
            <td>变更减少：</td>
            <td><asp:TextBox style="padding:0;margin:0" ID="txtLessenedAmount" check="1" runat="server" ClientIDMode="Static" Text="0" MaxLength="8"></asp:TextBox></td>
            <td>变更增加：</td>
            <td><asp:TextBox style="padding:0;margin:0" ID="txtAddedAmount" check="1" ClientIDMode="Static" runat="server" Text="0" MaxLength="8"></asp:TextBox></td>
        </tr>
        <tr>
            <td>变更后订单总价：</td>
            <td><asp:Label ID="lblAllMoney" runat="server" Text=""></asp:Label></td>
            <td><asp:HiddenField ID="hideFinishAmount" runat="server" ClientIDMode="Static" /></td>
            <td>&nbsp;</td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="6">说明：<asp:TextBox style="padding:0;margin:0" ID="txtRemark" check="0" tip="限200个字符！" runat="server" Rows="3" TextMode="MultiLine" Width="92%" MaxLength="200"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="6" style="text-align:center">
                <asp:Button ID="btnSaveChange" runat="server" Text="保存" OnClientClick="return CheckSuccess();" OnClick="btnSaveChange_Click" CssClass="btn btn-primary" Visible="false" />
                <input id="btnShow" type="button" value="预览" onclick="ShowPrint();" class="btn btn-success" />
                <asp:Button ID="btnSaveChecks" runat="server" Text="提交审核" OnClientClick="return CheckSuccess();" OnClick="btnSaveChecks_Click" CssClass="btn btn-danger" Visible="false" />
                <asp:Button ID="btnfinish" runat="server" Text="确认此订单" OnClientClick="return CheckSuccess();" OnClick="btnfinish_Click" CssClass="btn btn-success" Visible="true" />
                <asp:Button ID="btnPrint" runat="server" Text="打印" CssClass="btn btn-primary" OnClientClick="return Print" Visible="false" />
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
