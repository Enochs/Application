<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyCarrytaskList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.MyCarrytaskList" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" Title="我的执行明细" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>



<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc11" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyCarryTask.ascx" TagPrefix="HA" TagName="MyCarryTask" %>




<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">


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
            $(".Delete").children(".LnkDelete").hide();
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
        function ShowFileUploadPopu(DisID, Kind) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskImageUpload.aspx?DispatchingID=" + DisID + "&Kind=" + Kind;
            showPopuWindows(Url, 765, 300, "#SelectEmpLoyeeBythis");
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
        function SelectSupp(Key, CategoryID,CustomerID) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectProduct.aspx?ControlKey=hideThirdValue&Callback=btnChangeProduct&ChangeProduct=1&CategoryID=" + CategoryID + "&CustomerID=" + CustomerID;

            //$(Control).attr("href", Url);
            showPopuWindows(Url, 600, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
            $("#hideChange").attr("value", Key);
        }

    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>

    <br />
    <table style="width: 100%;" border="0">
        <tr>
            <td align="left" id="FirstTypetd">
                <div style="display: none;">
                    需要其他人完成物料请点击"选择其他人"再点击<span style="color: red;">"保存"</span>
                    <br />
                    无论是你自己完善物料还是他人完善物料<br />
                    你都可以为类别添加项目，还可以为项目继续添加产品<br />
                    <input runat="server" id="txtFirstEmpLoyeeItem" style="width: 65px" class="txtFirstEmpLoyeeItem" type="text" />
                    <asp:Button ID="btnSaveItem" CommandName="SaveItem" runat="server" Text="保存" CssClass="btn btn-success" OnClick="btnSaveItem_Click" />
                    <a href="#" onclick="ShowFirstPopu(this);" class="SetState btn">
                        <asp:Label ID="lblSelectTitle" runat="server" Text="改派"></asp:Label></a>
                </div>
                <asp:HiddenField ID="hideFirstEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
                你可以为类别/项目指定供应商，请点击类别/项目旁的<span style="color: red;">"指定供应商"</span>按钮
                你可以通过更换产品来更换供应商，请点击对应的<span style="color: red;">"产品/服务内容"</span>
                <asp:HiddenField ID="hideOldEmployeeID" ClientIDMode="Static" Value="-1" runat="server" />
                <div style="display: none;">
                     <asp:Button ID="btnSavesupperSave" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="供应商保存" CssClass="btn btn-success" OnClick="btnSavesupperSave_Click" />
                    <asp:Button ID="btnSaveSuppertoOther" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="其他保存" CssClass="btn btn-success" OnClick="btnSaveSuppertoOther_Click" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hidePgValue" />
                    <asp:Button ID="btnStarFirstpg" ClientIDMode="Static" runat="server" Text="生成一级分类" OnClick="btnStarFirstpg_Click" />
                    <asp:Button ID="btnCreateSecond" ClientIDMode="Static" runat="server" Text="生成二级分类" OnClick="btnCreateSecond_Click" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSecondValue" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideSecondCategoryID" />
                    <asp:Button ID="btnCreateThired" ClientIDMode="Static" runat="server" Text="生成产品" OnClick="btnCreateThired_Click" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdValue" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdCategoryID" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideFoureGingang" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideMineState" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideMineNeedState" />

                    <asp:Button ID="btnChangeProduct" ClientIDMode="Static" runat="server" Text="更换产品" OnClick="btnChangeProduct_Click" />
                    <asp:HiddenField ID="hideChange" ClientIDMode="Static" runat="server" />
                </div>
            </td>
            <td style="text-align: left; width: 25%;">&nbsp;</td>
        </tr>
    </table>
    <br />
    <div style="overflow-x: auto;">
        <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound" OnItemCommand="repfirst_ItemCommand">
            <ItemTemplate>
                <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                <asp:HiddenField ID="hideKey" Value='<%#Eval("ProeuctKey") %>' runat="server" />
                <div id="<%#Guid.NewGuid().ToString() %>">
                    <input runat="server" id="txtFirstEmpLoyeeItem" style="width: 65px;margin:0" class="txtFirstEmpLoyeeItem Move" type="text" />
                    <asp:Button ID="btnSaveItem" CommandName="SaveEmployee" runat="server" Text="派工保存" CssClass="btn btn-success Move" />
                    <a href="#" onclick="ShowFirstPopu1(this);" class="SetState btn btn-primary Move">
                        <asp:Label ID="lblSelectTitle" runat="server" Text="选择其他人" CssClass="Move"></asp:Label></a>&nbsp;
                                <asp:HiddenField ID="hideSetFirstEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
                    <asp:HiddenField ID="hideCatgoryID" ClientIDMode="Static" Value='<%#Eval("CategoryID") %>'
                        runat="server" />
                </div>
                <table class="table table-bordered table-striped First<%#Eval("CategoryID") %>" border="1" style="border-color: #f7f5f3; width: 99%;">
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
                        <td><b>备注</b></td>
                        <td style="display: none;"><b>责任人</b></td>
                        <td><b>供应商</b></td>
                        <td><b>操作</b></td>
                    </tr>
                    <asp:Repeater ID="repdatalist" runat="server" OnItemCommand="repdatalist_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td <%#Eval("ItemLevel").ToString()=="1"?"style='border-top-style:double;border-bottom-style:none;white-space:nowrap;'" : "class='NeedHide'  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'"%> <%#Eval("ParentCategoryID")%>>
                                    <b <%#Eval("ItemLevel").ToString()!="1"?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b><br />
                                    <label>
                                        <br />
                                        <a id="SelectSG" <%#HideSelectItem(Container.ItemIndex) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-primary btn-mini" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=<%#Eval("ItemLevel").ToString()=="1"?Eval("CategoryID"):Eval("ParentCategoryID") %>&ControlKey=hideSecondValue&Callback=btnCreateSecond" onclick="return SetfunCategoryID(this);"><span style="color:white">增加项目</spa></a>
                                    </label>
                                    <asp:HiddenField ID="hidePriceKey" Value='<%#Eval("ProeuctKey") %>' runat="server" />
                                    <asp:HiddenField ID="hideCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                                </td>
                                <td <%#Eval("ItemLevel").ToString()=="2"?"style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:nowrap;'":"style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" %>>
                                    <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<a id="A2" <%#HideSelectProduct(Eval("ItemLevel")) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-primary btn-mini" href="/AdminPanlWorkArea/ControlPage/SelectProduct.aspx?CategoryID=<%#Eval("CategoryID") %>&ControlKey=hideThirdValue&Callback=btnCreateThired&SupplierName=<%#Eval("SupplierName") %>&CustomerID=<%=Request["CustomerID"] %>" onclick="SetProduct(this);">增加产品</a>
                                    <input id="txtEmpLoyeeItem" style="width: 65px; display: none;" class="txtEmpLoyeeItem" type="text" value='<%#GetCatgoryEmpLoyeeName(Eval("CategoryID"))%>' />
                                    <a href="#" onclick="ShowNewPopu(this);" class="SetState" style="display: none;">派工</a>
                                    <asp:HiddenField ID="hideNewEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />

                                </td>
                                <td>
                                    <a href="#" onclick="SelectSupp(<%#Eval("ProeuctKey") %>,<%#Eval("CategoryID") %>,<%=Request["CustomerID"] %>);" title="点击此处可以更换产品！"><%#Eval("ItemLevel").ToString()=="3"?Eval("ServiceContent"):""%></a>
                                    <div style="display: none;">
                                        <asp:TextBox ID="txtProductName" style="margin:0" runat="server" Enabled="false" Text='<%#Eval("ServiceContent") %>' Visible="false"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRequirement" style="width:92%;margin:0" runat="server" Text='<%#Eval("Requirement") %>' TextMode="MultiLine"></asp:TextBox>
                                </td>
                                <td>
                                    <div <%#Eval("Requirement").ToString()==""&&(Eval("ItemLevel").ToString()=="1"||Eval("ItemLevel").ToString()=="2")?"style='display: none;'":"" %>>
                                        <a href="#" style="margin:0" class="btn btn-mini btn-primary" onclick="ShowFileUploadPopu('<%#Eval("DispatchingID") %>','<%#Eval("ProeuctKey") %>')">上传</a>
                                        <a id="inline" style="margin:0" class="btn btn-mini btn-primary" href="#data<%#Eval("ProeuctKey") %>" kesrc="#data<%#Eval("ProeuctKey")%>">查看</a>
                                        <div style="display: none;">
                                            <div id='data<%#Eval("ProeuctKey") %>'>
                                                <%#GetKindImage(Eval("ProeuctKey")) %>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td class="SetSubtotal<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>">
                                    <div <%#Eval("Requirement").ToString()==""&&(Eval("ItemLevel").ToString()=="1"||Eval("ItemLevel").ToString()=="2")?"style='display: none;'":"" %>>
                                        <asp:TextBox ID="txtSalePrice" style="margin:0" MaxLength="8" CssClass="SalePrice" runat="server" ProductID='<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>' Text='<%#Eval("PurchasePrice") %>' Width="75"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <div <%#Eval("Requirement").ToString()==""&&(Eval("ItemLevel").ToString()=="1"||Eval("ItemLevel").ToString()=="2")?"style='display: none;'":"" %>>
                                        <%#Eval("Unit") %>
                                    </div>
                                </td>
                                <td class="SetSubtotal<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>">
                                    <div <%#Eval("Requirement").ToString()==""&&(Eval("ItemLevel").ToString()=="1"||Eval("ItemLevel").ToString()=="2")?"style='display: none;'":"" %>>
                                        <asp:TextBox ID="txtQuantity" style="margin:0" MaxLength="8" CssClass="Quantity" runat="server" Width="30" ProductID='<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("Quantity") %>'></asp:TextBox>
                                    </div>
                                </td>
                                <td class="SetSubtotal<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>">
                                    <div <%#Eval("Requirement").ToString()==""&&(Eval("ItemLevel").ToString()=="1"||Eval("ItemLevel").ToString()=="2")?"style='display: none;'":"" %>>
                                        <asp:TextBox ID="txtSubtotal" style="margin:0" MaxLength="8" class='Subtotal' runat="server" Width="75" ProductID='<%#Eval("ProeuctKey")+Container.ItemIndex.ToString()+"ROW" %>' Text='<%#Eval("Subtotal") %>'></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRemark" MaxLength="50" runat="server" Width="80" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                                <td id="Partd<%#Guid.NewGuid().ToString() %>" style="display: none;">
                                    <input style="width: 45px;" maxlength="10" runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" type="text" value='<%#GetEmployeeName(Eval("EmployeeID")) %>' />
                                    <div style="display: none;">
                                        <a href="#" onclick="ShowPopu(this);" class="SetState">派工</a>
                                        <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='<%#Eval("EmployeeID")==null?"-1":"" %>' runat="server" />
                                    </div>

                                </td>
                                <td style="width: 50px; white-space: nowrap; text-align: left;" id="Row<%#Guid.NewGuid().ToString() %>">
                                    <b>
                                        <input runat="server" maxlength="10" id="txtSuppName" type="text" class="txtSuppName " value='<%#Eval("SupplierName") %>' />
                                    </b>

                                    <%# Eval("ItemLevel").ToString()=="2"&&Eval("ItemLevel").ToString()!="1"?"<a href='#'  class='btn btn-danger btn-mini ABCD"+Eval("ItemLevel")+"' onclick='ChangeSuppByCatogry(this,\"btnSavesupperSave\")'>指定</a>":"<a href='#' class='btn btn-danger btn-mini ABCD"+Eval("ItemLevel")+"''  onclick='ChangeSuppByCatogry(this,\"btnSaveSuppertoOther\")'>指定</a>"%>
                          
                                    <asp:HiddenField ID="hideSuppID" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="Delete<%#Eval("NewAdd") %>">
                                    <label <%#Eval("CreateEmployee").ToString()!=User.Identity.Name?"style='display:none;'":"" %>>
                                        <asp:LinkButton ID="lnkbtnDelete" CssClass="LnkDelete btn btn-mini btn-danger" CommandName="Delete" CommandArgument='<%#Eval("ProeuctKey") %>' runat="server" OnClientClick="return CheckDelete();">删除</asp:LinkButton>
                                    </label>
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
        <div style="text-align: center;">
            <asp:Button ID="Button1" CommandName="SaveItem" runat="server" Text="保存" CssClass="btn btn-success" OnClick="btnSaveItem_Click" />
        </div>

    </div>
</asp:Content>


