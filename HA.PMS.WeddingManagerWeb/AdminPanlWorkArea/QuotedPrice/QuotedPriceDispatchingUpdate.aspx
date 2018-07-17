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

        .table_Report tr td {
            text-align: center;
        }
    </style>
    <script type="text/javascript">


        //类别 项目 只显示一次(第一行)
        $(document).ready(function () {

            if ($("#hidecheck").val() != "0") {
                $(".CheckNode").hide();
            }

            showPopuWindows($("#SelectPG").attr("href"), 500, 300, "a#SelectPG");
            $(".NeedHideLable").hide();

            if ('<%=Request["OnlyView"]%>') {
                $("input,textarea,select").attr("disabled", "disabled");
                $("input[type=button],.btn").hide();
            }

        });

        $(document).ready(function () {


            document.bgColor = "#ff0000";


            //成本价改变
            $(".CostPrice").change(function () {
                var PriceIndex = ".SetSubtotal" + $(this).attr("ProductID");
                var ParetnTotal = "#txtCost" + $(this).attr("ParentCategoryID");
                var TotalNum = 0;
                var OldTexValue = $(ParetnTotal).val();
                var OLdValue = 0;
                if (OldTexValue != "") {
                    OLdValue = parseFloat(OldTexValue);
                }

                var NewValue = 0;

                var Quantity = parseFloat($(PriceIndex).find(".Quantity").val());
                var SalePrice = parseFloat($(PriceIndex).find(".CostPrice").val());

                var Subtotal = Quantity * SalePrice;

                if ($(PriceIndex).find(".CostSubTotal").val() == "") {
                    OLdValue = Subtotal + OLdValue;

                } else {
                    OLdValue = OLdValue - parseFloat($(PriceIndex).find(".CostSubTotal").val());
                    OLdValue = OLdValue + Subtotal;
                }

                $(PriceIndex).find(".CostSubTotal").attr("value", Subtotal);
                $(ParetnTotal).attr("value", OLdValue);
                SetTotalAmount();

            });

        });



        function eachAmount() {
            var TotalAmount = 0;
            $(".SaleItem").each(function () {
                if ($(this).val() != "") {
                    TotalAmount = TotalAmount + parseFloat($(this).val());
                }
                $("#txtRealAmount").attr("value", TotalAmount);
            });
        }


        function SetfunCategoryID(Control) {
            $("#hideSecondCategoryID").attr("value", $(Control).attr("categoryid"));

            return false;
        }


        function SetProduct(Control) {
            $("#hideThirdCategoryID").attr("value", $(Control).attr("categoryid"));
            return false;
        }




        //查看图片
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



        $(window).load(function () {
            if ('<%=Request["OnlyView"]%>') {
                $("input,textarea,select").attr("disabled", "disabled");
                $("input[type=button],.btn").hide();
                $(".needshow").show();
            }
        });




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
                            <td width="75" style="white-space: nowrap;"><b>成本价</b></td>
                            <td width="80" style="white-space: nowrap;"><b>单位</b></td>
                            <td width="80" style="white-space: nowrap;"><b>数量</b></td>
                            <td width="80" style="white-space: nowrap;"><b>小计</b></td>
                            <td width="180" style="white-space: nowrap;"><b>小计(成本)</b></td>
                            <td width="80" style="white-space: nowrap; display: none"><b>操作</b></td>
                        </tr>
                        <asp:Repeater ID="repdatalist" runat="server">
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
                                        <asp:Label ID="lblSalePrice" CssClass="SalePrice" MaxLength="8" runat="server" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("UnitPrice") %>' Width="75"></asp:Label></td>
                                    <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>">
                                        <asp:TextBox ID="txtCostPrice" CssClass="CostPrice" MaxLength="9" runat="server" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("Cost") == null ? "0" :Eval("Cost").ToString() %>' Width="75"></asp:TextBox></td>
                                    <td><%#Eval("Unit") %></td>
                                    <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW"%>">
                                        <asp:TextBox ID="txtQuantity" Enabled="false" CssClass="Quantity" MaxLength="8" runat="server" Width="75" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("Quantity") %>' Style="border: 0px solid gray; background: #EEEEEE;"></asp:TextBox>
                                        <asp:HiddenField ID="hiddenAvailableCount" Value='<%#GetAvailableCount(Eval("ProductID"),Eval("RowType")) %>' runat="server" />
                                    </td>
                                    <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %> Total<%#Eval("ParentCategoryID") %>">
                                        <asp:Label ID="lblSubtotal" class='Subtotal' MaxLength="8" runat="server" Width="75" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW"' Text='<%#Eval("Subtotal") %>'></asp:Label>
                                    </td>
                                    <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %> Total<%#Eval("ParentCategoryID") %>">
                                        <asp:TextBox ID="txtCostSubTotal" runat="server" class='CostSubTotal' Width="75" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW"' Text='<%#Eval("CostSubtotal") %>' Style="border: 0px solid gray; background: #EEEEEE;"></asp:TextBox>
                                    </td>
                                </tr>

                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                    <div style="margin-right: 268px; padding: 2px; margin-top: auto; text-align: right;">
                        <span>分项合计：
                    <input id='txtTotal<%#Eval("CategoryID") %>' style="width: 75px;" disabled="disabled" type="text" class="ItemAmount" value="<%#Eval("ItemAmount")==null?"0":Eval("ItemAmount")%>" />
                            <div style="display: none;">销售价：<asp:TextBox ID="txtSaleItem" CssClass="SaleItem" Width="75" runat="server" Text='<%#Eval("ItemSaleAmount") %>'></asp:TextBox></div>
                        </span>
                        <span>成本合计：
                    <input id='txtCost<%#Eval("CategoryID") %>' style="width: 75px;" disabled="disabled" type="text" class="ItemAmount" value="<%#Eval("ItemCost")==null?"0":Eval("ItemCost")%>" />
                            <div style="display: none;">成本价：<asp:TextBox ID="txtCostItem" CssClass="CostItem" Width="75" runat="server" Text='<%#Eval("CostItemAmount") %>'></asp:TextBox></div>
                            <asp:Button ID="btnSaveItem" CommandName="SaveItem" runat="server" Text="保存分项" CssClass="btn btn-success" />
                        </span>

                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div style="margin-right: 268px; padding: 2px; margin-top: auto; text-align: left;">
                <span>销售总价:<asp:Label runat="server" ID="lblFinishAmount" /></span>
                <span>成本总价:<asp:Label runat="server" ID="lblCostAmount" /></span>
            </div>


            <div style="text-align: center;">
                <table class="table table-bordered table_Report" style="width: 70%; margin-top: 15px;">
                    <tr>
                        <th>项目</th>
                        <th>价格</th>
                        <th>成本</th>
                        <th>毛利</th>
                        <th>毛利率</th>
                        <th rowspan="5" style="vertical-align: top;"><span style="margin-top: 15px;">审核说明:</span></th>
                        <th rowspan="5">
                            <asp:TextBox runat="server" ID="txtCheckContent" TextMode="MultiLine" Width="400px" Height="150px" /></th>
                    </tr>

                    <tr>
                        <td>人员</td>
                        <td><%=GetMoney(1,"person") %></td>
                        <td><%=GetMoney(2,"person") %></td>
                        <td><%=GetMoney(3,"person") %></td>
                        <td><%=GetRates("person") %></td>
                    </tr>
                    <tr>
                        <td>物料</td>
                        <td><%=GetMoney(1,"material") %></td>
                        <td><%=GetMoney(2,"material") %></td>
                        <td><%=GetMoney(3,"material") %></td>
                        <td><%=GetRates("material") %></td>
                    </tr>
                    <tr>
                        <td>其他</td>
                        <td><%=GetMoney(1,"other") %></td>
                        <td><%=GetMoney(2,"other") %></td>
                        <td><%=GetMoney(3,"other") %></td>
                        <td><%=GetRates("other") %></td>
                    </tr>
                    <tr>
                        <td>合计</td>
                        <td><%=GetMoney(1,"all") %></td>
                        <td><%=GetMoney(2,"all") %></td>
                        <td><%=GetMoney(3,"all") %></td>
                        <td><%=GetRates("all") %></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="div_Bottom" style="text-align: center;">
            <%--<asp:Button runat="server" ID="btn_BackOrder" Text="打回订单" OnClientClick="return confirm('你确认要打回该订单吗？');" CssClass="btn btn-primary" OnClick="btn_BackOrder_Click" />--%>
            <asp:Button runat="server" ID="btn_Save" Text="保存" CssClass="btn btn-primary" OnClick="btn_Save_Click" />
            <%--<asp:Button runat="server" ID="btn_Pass" Text="通过审核" CssClass="btn btn-primary" OnClick="btn_Pass_Click" />--%>
            <%--<asp:Button runat="server" ID="btn_Confirm" Text="确认派工" CssClass="btn btn-success" OnClick="btn_Confirm_Click" />--%>
            <br />
            <asp:Label runat="server" ID="lblCheckNode" Text="审核已经通过" Visible="false" Style="color: green; font-size: 14px; text-align: center; font-weight: bold;"></asp:Label>
        </div>
        <div style="overflow-y: auto; height: 1650px;">
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
