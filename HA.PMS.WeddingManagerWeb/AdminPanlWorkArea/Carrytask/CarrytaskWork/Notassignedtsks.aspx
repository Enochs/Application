<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" StylesheetTheme="None" AutoEventWireup="true" CodeBehind="Notassignedtsks.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.Notassignedtsks" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>



<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyCarryTask.ascx" TagPrefix="HA" TagName="MyCarryTask" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Scripts/jquery.freezeheader.js">
 
    </script>
    <script>
        $(document).ready(function () {
            $("#FirstTable").freezeHeader({ 'height': '800px' });
        })
    </script>
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
            border-width: 1px;
            border-style: solid;
            border-color: White;
            border-collapse: collapse;
            border-bottom-color: Black;
            border-right-color: Black;
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
        //function ShowFileUploadPopu(DisID, Kind) {
        //    var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskImageUpload.aspx?DispatchingID=" + DisID + "&Kind=" + Kind;
        //    showPopuWindows(Url, 765, 300, "#SelectEmpLoyeeBythis");
        //    $("#SelectEmpLoyeeBythis").click();
        //}

        //上传图片
        function ShowFileUploadPopu(QuotedID, Kind) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPriceImageUpload.aspx?QuotedID=" + QuotedID + "&Kind=" + Kind;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
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
        function ChangeSuppByCatogry(Parent, CallBack) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectSupplierBythis.aspx?Callback=" + CallBack + "&ALL=true&ControlKey=hideSuppID&SetEmployeeName=txtSuppName&ParentControl=" + $(Parent).parent().attr("id");
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
            <td align="left" id="FirstTypetd">
                <div style="display: none;">


                    <asp:Button ID="btnSaveItem" CommandName="SaveItem" runat="server" Text="保存" CssClass="btn btn-success" OnClick="btnSaveItem_Click" />
                    <a href="#" onclick="ShowFirstPopu(this);" class="SetState btn">
                        <asp:Label ID="lblSelectTitle" runat="server" Text="改派"></asp:Label></a>
                </div>
                <asp:HiddenField ID="hideFirstEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
                &nbsp;<asp:HiddenField ID="hideOldEmployeeID" ClientIDMode="Static" Value="-1" runat="server" />
                <div style="display: none;">
                    <asp:Button ID="btnSavesupperSave" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="供应商保存" CssClass="btn btn-success" OnClick="btnSavesupperSave_Click" />
                    <asp:Button ID="btnSaveSuppertoOther" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="其他保存" CssClass="btn btn-success" OnClick="btnSaveSuppertoOther_Click" />


                    <asp:Button ID="btnCreateSecond" ClientIDMode="Static" runat="server" Text="生成二级分类" OnClick="btnCreateSecond_Click" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSecondValue" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSecondCategoryID" />
                    <asp:Button ID="btnCreateThired" ClientIDMode="Static" runat="server" Text="生成产品" OnClick="btnCreateThired_Click" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdValue" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdCategoryID" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideFoureGingang" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideMineState" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideMineNeedState" />
                    <asp:Button ID="btnStarFirstpg" ClientIDMode="Static" runat="server" Text="生成一级分类" OnClick="btnStarFirstpg_Click" />
                    <asp:Button ID="btnChangeProduct" ClientIDMode="Static" runat="server" Text="更换产品" OnClick="btnChangeProduct_Click" />
                    <asp:HiddenField ID="hideChange" ClientIDMode="Static" runat="server" />
                </div>
            </td>
            <td style="text-align: left; width: 25%;">&nbsp;</td>
        </tr>
    </table>
    <br />
    <table id="FirstTable" border="0" style="border-color: #000000; width: 99%;">
        <thead>
            <tr>
                <th style="text-align: left;" id="<%=Guid.NewGuid().ToString() %>">
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hidePgValue" />
                    <a id="SelectPG" class="SelectPG btn btn-warning" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=0&ControlKey=hidePgValue&Callback=btnStarFirstpg"><b style="color: black;">添加类别</b></a>
                    <asp:Button ID="btnFlower" runat="server" Text="花艺" CssClass="btn" OnClick="btnFlower_Click" />
                    <asp:Button ID="btnProduct" runat="server" Text="道具" CssClass="btn" OnClick="btnFlower_Click" />
                    <asp:Button ID="btnLine" runat="server" Text="灯光" CssClass="btn" OnClick="btnFlower_Click" />
                    <asp:Button ID="btnEmployee" runat="server" Text="人员" CssClass="btn" OnClick="btnFlower_Click" />
                    <asp:Button ID="btnOther" runat="server" Text="其它" CssClass="btn" OnClick="btnFlower_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="btn" />
                    <asp:Button ID="btnSplite" runat="server" Text="自动分解" OnClick="btnSplite_Click" />
                </th>
            </tr>
        </thead>
        <tr>
            <td>
                <div style="overflow-x: auto;">

                    <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound" OnItemCommand="repfirst_ItemCommand">
                        <ItemTemplate>
                            <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                            <asp:HiddenField ID="hideKey" Value='<%#Eval("ProeuctKey") %>' runat="server" />
                            <div id="<%#Guid.NewGuid().ToString() %>">


                                <asp:HiddenField ID="hideSetFirstEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
                                <asp:HiddenField ID="hideCatgoryID" ClientIDMode="Static" Value='<%#Eval("CategoryID") %>' runat="server" />

                            </div>
                            <table class="tablestyle First<%#Eval("CategoryID") %>" border="1" style="border-color: #000000; width: 99%;">

                                <tr>
                                    <td style="width: 100px;"><b>类别</b></td>
                                    <td style="width: 100px;"><b>项目</b></td>
                                    <td style="width: 100px;"><b>产品/服务内容</b></td>
                                    <td style="width: 300px;"><b>具体要求</b></td>
                                    <td style="width: 50px;"><b>图片</b></td>
                                    <td style="width: 50px;"><b>单价</b></td>
                                    <td style="width: 50px; word-break: break-all; overflow: hidden; word-wrap: break-word;"><b>单位</b></td>
                                    <td style="width: 50px;"><b>数量</b></td>
                                    <td style="width: 50px;"><b>小计</b></td>
                                    <td style="width: 100px;"><b>备注</b></td>
                                    <td style="display: none;"><b>责任人</b></td>
                                    <td><b>供应商</b></td>
                                    <td><b>操作</b></td>
                                </tr>
                                <asp:Repeater ID="repdatalist" runat="server" OnItemCommand="repdatalist_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td <%#Eval("ItemLevel").ToString()=="1"?"style='border-top-style:double;border-bottom-style:none;white-space:nowrap;'" : "class='NeedHide'  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'"%> <%#Eval("ParentCategoryID")%>>
                                                <b <%#Eval("ItemLevel").ToString()!="1"?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b><br />
                                                <a id="SelectSG" <%#HideSelectItem(Container.ItemIndex) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-primary " href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=<%#Eval("ItemLevel").ToString()=="1"?Eval("CategoryID"):Eval("ParentCategoryID") %>&ControlKey=hideSecondValue&Callback=btnCreateSecond" onclick="return SetfunCategoryID(this);">
                                                    <span>增加项目</spa></a>

                                                <asp:HiddenField ID="hidePriceKey" Value='<%#Eval("ProeuctKey") %>' runat="server" />
                                                <asp:HiddenField ID="hideCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                                            </td>
                                            <td <%#Eval("ItemLevel").ToString()=="2"?"style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:nowrap;'":"style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" %>>
                                                <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b><br />
                                                &nbsp;&nbsp;&nbsp;&nbsp;<a id="A2" <%#HideSelectProduct(Eval("ItemLevel")) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-primary " href="/AdminPanlWorkArea/ControlPage/SelectProduct.aspx?CategoryID=<%#Eval("CategoryID") %>&ControlKey=hideThirdValue&Callback=btnCreateThired&SupplierName=<%#Eval("SupplierName") %>&CustomerID=<%=Request["CustomerID"] %>" onclick="SetProduct(this);">增加产品</a>
                                                <input id="txtEmpLoyeeItem" style="width: 65px; display: none;" class="txtEmpLoyeeItem" type="text" value='<%#GetCatgoryEmpLoyeeName(Eval("CategoryID"))%>' />

                                                <asp:HiddenField ID="hideNewEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />

                                            </td>
                                            <td>
                                                <b <%#Eval("ItemLevel").ToString()!="3"?"class='NeedHideLable'": ""%>>
                                                    <asp:CheckBox ID="CheckItem" Text="选择" runat="server" /></b>

                                                <a href="#" onclick="SelectSupp(<%#Eval("ProeuctKey") %>,<%#Eval("CategoryID") %>,<%=Request["CustomerID"] %>);" title="点击此处可以更换产品！"><%#Eval("ItemLevel").ToString()=="3"?Eval("ServiceContent"):""%></a>
                                                <div style="display: none;">
                                                    <asp:TextBox ID="txtProductName" Style="margin: 0" runat="server" Enabled="false" Text='<%#Eval("ServiceContent") %>' Visible="false"></asp:TextBox>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRequirement" Style="width: 92%; margin: 0" runat="server" Text='<%#Eval("Requirement") %>' TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                            <td style="width: 72px;">&nbsp;
                                                <a href="#" onclick="ShowFileUploadPopu('<%#GetQuotedID(Eval("DispatchingID")) %>','<%#GetChangeId(Eval("ProductID")) %>')" class="btn btn-mini   btn-primary">上传</a>
                                                <%--<a id="inline" style="margin: 0" class="btn  btn-primary" href="#data<%#Eval("ProeuctKey") %>" kesrc="#data<%#Eval("ProeuctKey")%>">查看</a>--%>
                                                <a href='#' onclick="ShowFileShowPopu('<%#GetQuotedID(Eval("DispatchingID")) %>','<%#GetChangeId(Eval("ProductID")) %>')" class="btn btn-mini  btn-primary needshow">查看</a>

                                                <div id='data<%#Eval("ProeuctKey") %>'>
                                                    <%#GetKindImage(Eval("ProeuctKey")) %>
                                                </div>
                                                </div>

                                            </td>
                                            <td class="SetSubtotal<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>">

                                                <asp:TextBox ID="txtSalePrice" Style="margin: 0" MaxLength="8" CssClass="SalePrice" runat="server" ProductID='<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>' Text='<%#Eval("PurchasePrice") %>' Width="75"></asp:TextBox>

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
                                                <asp:TextBox ID="txtRemark" MaxLength="50" runat="server" Width="80" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                                            <td id="Partd<%#Guid.NewGuid().ToString() %>" style="display: none;">
                                                <input style="width: 45px;" maxlength="10" runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" type="text" value='<%#GetEmployeeName(Eval("EmployeeID")) %>' />


                                            </td>
                                            <td style="width: 50px; white-space: nowrap; text-align: left;" id="Row<%#Guid.NewGuid().ToString() %>">

                                                <input runat="server" maxlength="10" id="txtSuppName" type="text" class="txtSuppName " value='<%#Eval("SupplierName") %>' />



                                                <asp:HiddenField ID="hideSuppID" runat="server" ClientIDMode="Static" />
                                            </td>
                                            <td class="Delete<%#Eval("NewAdd") %>" style="width: 40px;">&nbsp;
                                                <asp:LinkButton ID="lnkbtnDelete" CssClass="LnkDelete btn  btn-danger" CommandName="Delete" CommandArgument='<%#Eval("ProeuctKey") %>' runat="server" OnClientClick="return CheckDelete();">删除</asp:LinkButton>

                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
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
        <asp:Button ID="Button1" CommandName="SaveItem" runat="server" Text="保存" CssClass="btn btn-success" OnClick="btnSaveItem_Click" />
    </div>


</asp:Content>
