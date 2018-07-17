<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskCreate" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">

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


        function CheckSuppName() {

            //var IsOver = true;
            //$(".txtSuppName").each(
            //    function () {

            //        if ($(this).val() == "")
            //        {
            //            alert("请先指定供应商！");
            //            $(this).focus();
            //            IsOver = false;
            //            return false;
            //        }
            //    }
            //    );
            //return IsOver;

        }



        $(document).ready(function () {
            SetRowFocuse();
            $("a#inline").fancybox();
            showPopuWindows($("#SelectPG").attr("href"), 400, 300, "a#SelectPG");
            $(".NeedHideLable").hide();
        });

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


        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=true&ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=true";
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        function ShowNewPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=true&ControlKey=hideNewEmpLoyeeID&SetEmployeeName=txtEmpLoyeeItem&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=true";
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
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=true&ControlKey=hideFirstEmpLoyeeID&SetEmployeeName=txtFirstEmpLoyeeItem&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=true";
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <asp:HiddenField ID="hideRowKey" runat="server" ClientIDMode="Static" />
    <table style="width: 100%;" border="0">
        <tr>
            <td align="left" id="FirstTypetd">我自己完善物料请直接点击<span style="color: red;">"保存"</span>按钮，<br />
                需要其他人完成物料请点击"选择其他人"再点击<span style="color: red;">"保存"</span>
                <br />
                无论是你自己完善物料还是他人完善物料<br />
                你都可以为类别添加项目，还可以为项目继续添加产品<br />
                <input runat="server" id="txtFirstEmpLoyeeItem" style="width: 65px" class="txtFirstEmpLoyeeItem" type="text" />
                <asp:Button ID="btnSaveItem" CommandName="SaveItem" runat="server" Text="派工保存" CssClass="btn btn-success" OnClick="btnSaveItem_Click" />

                <a href="#" onclick="ShowFirstPopu(this);" class="SetState btn btn-primary">
                    <asp:Label ID="lblSelectTitle" runat="server" Text="选择其他人"></asp:Label></a>&nbsp;
                <asp:HiddenField ID="hideFirstEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
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

                    <asp:Button ID="btnChangeProduct" ClientIDMode="Static" runat="server" Text="更换产品" OnClick="btnChangeProduct_Click" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdValue" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideThirdCategoryID" />
                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hideFoureGingang" />

                    <asp:HiddenField ID="hideChange" ClientIDMode="Static" runat="server" />
                </div>
            </td>
            <td style="text-align: left; width: 25%;">你可以为类别/项目指定供应商，请点击类别/项目旁的<span style="color: red;">"指定供应商"</span>按钮<br />
                你可以通过更换产品来更换供应商，请点击对应的<span style="color: red;">"产品/服务内容"</span>
            </td>
        </tr>
    </table>
    <br />
    <div style="overflow-x: auto;">
        <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound" OnItemCommand="repfirst_ItemCommand">
            <ItemTemplate>
                <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                <asp:HiddenField ID="hideKey" Value='<%#Eval("ProeuctKey") %>' runat="server" />
                <table class="table table-bordered table-striped First<%#Eval("CategoryID") %>  table-select" border="1">

                    <tr>
                        <th>类别</td>
                        <th>项目</td>
                        <th>产品/服务内容</td>
                        <th>具体要求</td>
                        <th>图片</td>
                        <th width="75">成本价</td>
                        <th width="75">销售价</td>
                        <th>单位</td>
                        <th>数量</td>
                        <th>小计</td>
                        <th style="display: none;">备注</td>
                        <th style="display: none;">责任人</td>
                        <th>供应商</td>
                        <th>操作</td>
                    </tr>
                    <asp:Repeater ID="repdatalist" runat="server" OnItemCommand="repdatalist_ItemCommand">
                        <ItemTemplate>
                            <tr  id="CreateEdit<%#Eval("ProeuctKey")%>" onclick="javascript:SetRowKey('CreateEdit<%#Eval("ProeuctKey")%>')">
                                <td <%#Eval("ItemLevel").ToString()=="1"?"style='border-top-style:double;border-bottom-style:none;white-space:nowrap;'" : "class='NeedHide'  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'"%> <%#Eval("ParentCategoryID")%>>
                                    <b <%#Eval("ItemLevel").ToString()!="1"?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b><br />
                                    <label>
                                        <br />
                                        <a id="SelectSG" <%#HideSelectItem(Container.ItemIndex) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-primary btn-mini" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=<%#Eval("ItemLevel").ToString()=="1"?Eval("CategoryID"):Eval("ParentCategoryID") %>&ControlKey=hideSecondValue&Callback=btnCreateSecond" onclick="return SetfunCategoryID(this);"><span style="color:white">增加项目</span></a>
                                    </label>
                                    <asp:HiddenField ID="hidePriceKey" Value='<%#Eval("ProeuctKey") %>' runat="server" />
                                    <asp:HiddenField ID="hideCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                                </td>
                                <td <%#Eval("ItemLevel").ToString()=="2"?"style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:nowrap;'":"style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" %>>
                                    <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<a id="A2" <%#HideSelectProduct(Eval("ItemLevel")) %> categoryid="<%#Eval("CategoryID") %>" class="SelectSG btn btn-mini btn-primary" href="/AdminPanlWorkArea/ControlPage/SelectProduct.aspx?CategoryID=<%#Eval("CategoryID") %>&SU=1&ControlKey=hideThirdValue&Callback=btnCreateThired&SupplierName=<%#Eval("SupplierName") %>&CustomerID=<%=Request["CustomerID"] %>" onclick="SetProduct(this);">增加产品</a>
                                    <input id="txtEmpLoyeeItem" style="width: 65px; display: none;" class="txtEmpLoyeeItem" type="text" value='<%#GetCatgoryEmpLoyeeName(Eval("CategoryID"),Eval("ItemLevel")) %>' />
                                    <a href="#" onclick="ShowNewPopu(this);" class="SetState" style="display: none;">派工</a>
                                    <asp:HiddenField ID="hideNewEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />

                                </td>
                                <td>
                                    <a href="#" onclick="SelectSupp(<%#Eval("ProeuctKey") %>,<%#Eval("CategoryID") %>,<%=Request["CustomerID"] %>);" title="点击此处可以更换产品！"><%#Eval("ItemLevel").ToString()=="3"?Eval("ServiceContent"):""%></a>
                                    <div style="display: none;">
                                        <asp:TextBox ID="txtProductName" MaxLength="20" runat="server" Enabled="false" Text='<%#Eval("ServiceContent") %>' Visible="false"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <div <%#Eval("Requirement").ToString()==""&&(Eval("ItemLevel").ToString()=="1"||Eval("ItemLevel").ToString()=="2")?"style='display: none;'":"" %>>
                                        <asp:TextBox ID="txtRequirement" style="margin:0;padding:0;width:100%" runat="server" Text='<%#Eval("Requirement") %>' TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </td>
                                <td>
                                    <div <%#Eval("Requirement").ToString()==""&&(Eval("ItemLevel").ToString()=="1"||Eval("ItemLevel").ToString()=="2")?"style='display: none;'":"" %>>
                                        <a href="#" class="btn btn-mini btn-primary" onclick="ShowFileUploadPopu('<%#Eval("DispatchingID") %>','<%#Eval("ProeuctKey") %>')">上传</a>
                                        <a id="inline" class="btn btn-mini btn-primary" href="#data<%#Eval("ProeuctKey") %>" kesrc="#data<%#Eval("ProeuctKey")%>">查看</a>

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
                                    <asp:Label Text='<%#Eval("UnitPrice") %>' ID="lblUnitPrice" runat="server" />
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
                                <td style="display: none;">
                                    <asp:TextBox ID="txtRemark" runat="server" Width="140" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                                <td id="Partd<%#Container.ItemIndex %>" style="display: none;">
                                    <input style="display: none;margin:0" maxlength="8" runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" type="text" value='<%#GetEmployeeName(Eval("EmployeeID")) %>' />
                                    <a href="#" onclick="ShowPopu(this);" class="SetState" style="display: none;">派工</a>
                                    <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value="-1" runat="server" />
                                </td>
                                <td style="width: 50px; white-space: nowrap; text-align: left;" id="Row<%#Guid.NewGuid().ToString() %>">
                                    <b>
                                        <input runat="server" style="margin:0" maxlength="10" id="txtSuppName" type="text" class="txtSuppName " value='<%#Eval("SupplierName") %>' />
                                    </b><%#Eval("ItemLevel").ToString()=="1"|| Eval("ItemLevel").ToString()=="2"?"<a href='#' class='btn btn-danger btn-mini' onclick='ChangeSuppByCatogry(this,\"btnSavesupperSave\")'>指定</a>":"<a href='#' class='btn btn-danger btn-mini' onclick='ChangeSuppByCatogry(this,\"btnSaveSuppertoOther\")'>指定</a>"%>


                                    <asp:HiddenField ID="hideSuppID" runat="server" ClientIDMode="Static" />
                                </td>
                                <td class="Delete<%#Eval("NewAdd") %>">
                                    <asp:LinkButton ID="lnkbtnDelete" CssClass="LnkDelete btn btn-danger btn-mini" CommandName="Delete" CommandArgument='<%#Eval("ProeuctKey") %>' runat="server" OnClientClick="return CheckDelete();">删除</asp:LinkButton></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div style="text-align: center">
                    <asp:Label ID="lblAmount" runat="server" Text='<%#Eval("ItemAmount") %>'></asp:Label>
                    <asp:Button ID="btnSaveItem" CommandName="SaveItem" runat="server" Text="保存数据" CssClass="btn btn-success" OnClientClick="return CheckSuppName();" />
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <br />
    </div>
</asp:Content>
