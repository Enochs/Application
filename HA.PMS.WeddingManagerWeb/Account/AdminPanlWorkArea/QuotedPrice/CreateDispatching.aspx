<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateDispatching.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.CreateDispatching" Title="制作执行明细" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>


<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <style type="text/css">
        .noborderforfirst {
        }

        .NoboderforThird {
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            if ($("#hideIsDis").val() == "1") {
                $("[type='text']").attr("disabled", "disabled");
                $("[type='button']").attr("disabled", "disabled");
                $(".Requirement").attr("disabled", "disabled");
                $(".btndelete").hide();
                $(".SelectSG").hide();
                $(".SelectPG").hide();


            }
            $("a#inline").fancybox();
            showPopuWindows($("#SelectPG").attr("href"), 600, 300, "a#SelectPG");
            $(".NeedHideLable").hide();
        });
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
            });
        });



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
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <asp:HiddenField ID="hideIsDis" ClientIDMode="Static" runat="server" />
    <table style="width: 100%;" border="0">
        <tr>
            <td align="left">
                <a id="SelectPG" class="SelectPG" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=<%=Request["CategoryID"] %>&ControlKey=hidePgValue&Callback=btnStarFirstpg"><b style="color: red;">增加项目</b></a>
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
                </div>
                <asp:Label ID="Label8" runat="server" BackColor="#EEEEEE" Width="35" Height="35"></asp:Label>
                <asp:Label ID="Label1" runat="server" BackColor="#FFCC99" Width="35" Height="35"></asp:Label>
                <asp:Label ID="Label2" runat="server" Text="类别"></asp:Label>
                <asp:Label ID="Label7" runat="server" BackColor="#EEEEEE" Width="35" Height="35"></asp:Label>
                <asp:Label ID="Label3" runat="server" BackColor="#CCFFCC" Width="35" Height="35"></asp:Label>
                <asp:Label ID="Label4" runat="server" Text="项目"></asp:Label>

                <asp:Label ID="Label5" runat="server" BackColor="#EEEEEE" Width="35" Height="35"></asp:Label>
                <asp:Label ID="Label6" runat="server" Text="其余为产品"></asp:Label>
            </td>
        </tr>
    </table>
    <div style="overflow-x: auto;">
        <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound" OnItemCommand="repfirst_ItemCommand">
            <ItemTemplate>
                <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                <asp:HiddenField ID="hideKey" Value='<%#Eval("ItemIndex") %>' runat="server" />
                <table class="First<%#Eval("CategoryID") %>" border="1" style="border-color: gray;width:99.6%">
                    <tr>
                        <td><b>类别</b></td>
                        <td><b>项目</b></td>
                        <td><b>产品/服务内容</b></td>
                        <td><b>具体要求</b></td>
                        <td><b>图片</b></td>
                        <td><b>单价</b></td>
                        <td><b>单位</b></td>
                        <td><b>数量</b></td>
                        <td><b>小计</b></td>
                        <td style="display: none;"><b>说明</b></td>
                        <td><b>操作</b></td>
                    </tr>
                    <asp:Repeater ID="repdatalist" runat="server" OnItemCommand="repdatalist_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td <%#Container.ItemIndex>0?"class='NeedHide'  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" : "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:'nowrap;'"%> <%#Eval("ParentCategoryID")%>>
                                    <b <%#Container.ItemIndex>0?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b>
                                        &nbsp;&nbsp;<a id="SelectSG" <%#HideSelectItem(Container.ItemIndex) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-primary btn-mini" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=<%#Eval("ItemLevel").ToString()=="1"?Eval("CategoryID"):Eval("ParentCategoryID") %>&ControlKey=hideSecondValue&Callback=btnCreateSecond" onclick="return SetfunCategoryID(this);"><span  style="color:white">增加项目</span></a>
                                    <asp:HiddenField ID="hidePriceKey" Value='<%#Eval("ItemIndex") %>' runat="server" />
                                    <asp:HiddenField ID="hideCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                                </td>
                                <td <%#Eval("ItemLevel").ToString()=="3"?"style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'": "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:nowrap;'" %>>
                                    <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b>
                                    &nbsp;&nbsp;<a id="A2" <%#HideSelectProduct(Eval("ItemLevel")) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-primary btn-mini" href="/AdminPanlWorkArea/ControlPage/SelectProduct.aspx?CategoryID=<%#Eval("CategoryID") %>&ControlKey=hideThirdValue&Callback=btnCreateThired&PartyDate=<%#Request["PartyDate"] %>&CustomerID=<%=Request["CustomerID"] %>" onclick="SetProduct(this);">增加产品</a></td>
                                <td><%#Eval("ServiceContent")%><div style="display: none;"><asp:TextBox style="margin:0;padding:0;" ID="txtProductName" Enabled="false" runat="server" Text='<%#Eval("ServiceContent") %>' Width="120"></asp:TextBox></div></td>
                                <td><asp:TextBox style="margin:0;padding:0;width:100%" ID="txtRequirement" runat="server" Text='<%#Eval("Requirement") %>' TextMode="MultiLine"></asp:TextBox></td><td>
                                    <a href="#" class="btn btn-primary  btn-mini" onclick="ShowFileUploadPopu('<%#Eval("QuotedID") %>','<%#Eval("ItemIndex") %>')">上传</a>
                                    <a id="inline" class="btn btn-primary  btn-mini" href="#data<%#Eval("ItemIndex") %>" kesrc="#data<%#Eval("ItemIndex")%>">查看</a>
                                    <div style="display: none;"><div id='data<%#Eval("ItemIndex") %>'><%#GetKindImage(Eval("ItemIndex")) %></div></div>
                                </td>
                                <td class="SetSubtotal<%#Eval("ItemIndex")+Container.ItemIndex.ToString()+"ROW" %>"><asp:TextBox style="margin:0;" ID="txtSalePrice" CssClass="SalePrice" runat="server" ProductID='<%#Eval("ItemIndex")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("UnitPrice") %>' Width="75"></asp:TextBox></td>
                                <td><%#Eval("Unit") %></td>
                                <td class="SetSubtotal<%#Eval("ItemIndex")+Container.ItemIndex.ToString()+"ROW"%>"><asp:TextBox style="margin:0;" ID="txtQuantity" CssClass="Quantity" runat="server" Width="75" ProductID='<%#Eval("ItemIndex")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("Quantity") %>'></asp:TextBox></td>
                                <td class="SetSubtotal<%#Eval("ItemIndex")+Container.ItemIndex.ToString()+"ROW" %> Total<%#Eval("ParentCategoryID") %>"><asp:TextBox style="margin:0;" ID="txtSubtotal" class='Subtotal' runat="server" Width="75" ProductID='<%#Eval("ItemIndex")+Container.ItemIndex.ToString()+"ROW"' Text='<%#Eval("Subtotal") %>'></asp:TextBox></td>
                                <td style="display: none;"><asp:TextBox style="margin:0;" ID="txtRemark" runat="server" Width="140" Text='<%#Eval("Remark") %>' MaxLength="50"></asp:TextBox></td>
                                <td><asp:LinkButton ID="lnkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("ItemIndex") %>' runat="server" OnClientClick="return CheckDelete();" CssClass="btn btn-danger btn-mini">删除</asp:LinkButton></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:Button ID="btnSaveChange" runat="server" Text="保存" CssClass="btn tabbtn btn-warning" OnClick="btnSaveChange_Click1" ClientIDMode="Static" />
    <br />
</asp:Content>
