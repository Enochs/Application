<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceListCreateEdit.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceListCreateEdit" Title="新报价单" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
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

        .btn-success {
            margin-top: 5px;
            margin-left: 10px;
        }

        .btn-danger {
            margin-top: 5px;
            margin-left: 10px;
        }

        .btn-info:hover {
            background-color: #ea5a23;
        }

        #btnAddChange {
            /*background-color: #F89406;*/
        }

            #btnAddChange :hover {
                text-decoration: underline;
            }
    </style>

    <script type="text/javascript">

        function ShowParentWindow(Width, Height) {
            var URL = "/AdminPanlWorkArea/QuotedPrice/QuotedManager/QuotedPriceChange.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>";
            showPopuWindows(URL, Width, Height, "#ShowPopuLable");
            $("#ShowPopuLable").click();

        }


        ///新增变更单
        function InsertIsChange() {
            URI = "/AdminPanlWorkArea/QuotedPrice/QuotedManager/QuotedPriceChange.aspx?QuotedID=<%=Request["QuotedID"] %>&CustomerID=<%=Request["CustomerID"] %>&OrderID=<%=Request["OrderID"] %>";
            $(".HAtab").removeClass("active");
            $("#btnQutedPriceChange").addClass("active");
            $("#Iframe1").attr("src", URI);
            $(".tab-content").css("height", "700");
            $(".active").css("height", "700");
            $(".framchild").css("height", "700");
        }

        $(window).load(function () {
            if ($("#btnQutedPriceChange").val("11")) {
<%--                URI = "/AdminPanlWorkArea/QuotedPrice/QuotedManager/QuotedPriceChange.aspx?QuotedID=<%=Request["QuotedID"] %>&CustomerID=<%=Request["CustomerID"] %>&OrderID=<%=Request["OrderID"] %>";
                $(".HAtab").removeClass("active");
                $("#btnQutedPriceChange").addClass("active");
                $("#Iframe1").attr("src", URI);
                $(".tab-content").css("height", "700");
                $(".active").css("height", "700");
                $(".framchild").css("height", "700");--%>
            }
        });

        $(document).ready(function () {
            $("#btnQutedPriceChange").click(function () {
                //$("#tab9").css("height", "850px");
                $("#Iframe1").css("min-height", "700px");
            });
        });
    </script>
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

        function RealSubmit() {
            if (confirm("导入后将删除现有报价单！且无法恢复！")) {
                return true;
            } else {
                return false;
            }

        }

        function CheckPage() {
            if ($("#txtRealAmount").val() == '' || parseFloat($("#txtRealAmount").val()) <= 0) {
                alert("报价单销售价不能小于0");
                $("#txtRealAmount").focus();
                return false;;
            } else if ($("#txtBrideName").val() == "") {
                alert("请输入新娘姓名");
                $("#txtBrideName").focus();
                return false;
            } else if ($("#<%=ddlHotel.ClientID %> option:selected").text() == "未选择") {
                alert("请选择酒店");
                $("#<%=ddlHotel.ClientID %>").focus();
                return false;
            } else if ($("#txtBridePhone").val() == "") {
                alert("请输入新娘电话");
                $("#txtBridePhone").focus();
                return false;
            } else if ($("#txtGroomName").val() == "") {
                alert("请输入新郎姓名");
                $("#txtGroomName").focus();
                return false;
            } else if ($("#txtGroomPhone").val() == "") {
                alert("请输入新郎电话");
                $("#txtGroomPhone").focus();
                return false;
            } else if ($("#txtpartyday").val() == "" || $("#txtpartyday").val() == "婚期未确定!") {
                alert("请选择婚期");
                $("#txtpartyday").focus();
                return false
            }

            //  if ($("#txtDisCount").val() >= 0.8 && $("#txtDisCount").val() <= 1) {
            //    var num1 = $("#txtMaterialPrice").val();
            //  var num2 = $("#txtDisCount").val();
            //var sum = num1 * num2;
            //document.getElementById("<=txtDiscountPrice.ClientID%>").value = sum;

            //} else {
            //  alert("请输入有效折扣 (0.8~1.0)");
            // $("#txtDisCount").focus();
            // return false;
            // }


}
function checkcountblur(ctrl) {
    var _inputvalue = $(ctrl).val();
    var _avaivalue = $(ctrl).next().val();
    if (_avaivalue != '') {
        if (parseInt(_inputvalue) > parseInt(_avaivalue)) {
            $(ctrl).css("border-color", "red");
        }
        else {
            $(ctrl).css("border-color", "rgb(204, 204, 204)");
        }
    }
}
$(window).load(function () {
    $(".Quantity").each(function () {
        if ($(this).next().val() != '') {
            $(this).attr("title", "当前剩余数量：" + $(this).next().val());
        }
    });
});

$(document).ready(function () {

    if ($("#HideFlowerSplit").val() == "1") {
        $(".FlowerItem").remove();
    }

    if ($("#HideSaleSplit").val() == "1") {
        $(".SaleItem").remove();
    }

    if ($("#hideDisState").val() == "4") {
        //$("[type='text']").attr("disabled", "disabled");
        $(".SelectSG").hide();

        $("textarea").attr("disabled", "disabled");


    }

    $("input[type='radio']").click(function () {
        var RadioValue = $("input[type='radio']:checked").val();
        RadioValue = parseInt(RadioValue);
        if (RadioValue == 0) {
            $(".HaveFile").show();
        } else {
            $(".HaveFile").hide();
        }

    });

    $("a#inline").fancybox();

    if ($("#hidecheck").val() != "0") {
        $(".CheckNode").hide();
    }

    showPopuWindows($(".SelectPG").attr("href"), 500, 300, "a.SelectPG");
    $(".NeedHideLable").hide();


    $(".btnSubmit").click(function () {


    });

    if ('<%=Request["OnlyView"]%>') {
        $("input,textarea,select").attr("disabled", "disabled");
        $("input[type=button],.btn").hide();
    }

});

$(document).ready(function () {
    if ($("#HideHaveEmployee").val() == "1") {
        $(".HaveEmployee").hide();
    }

    SetRowFocuse();
    var SelectItem = $("#ddlType").find("option:selected").text()
    if (SelectItem == "新增") {
        $(".NoAdd").hide();
    }

    $("#ddlType").change(function () {
        var SelectItem = $(this).find("option:selected").text()
        if (SelectItem == "新增") {
            $(".NoAdd").hide();
        } else {
            $(".NoAdd").show();
        }
    }
    )

    document.bgColor = "#ff0000";


    $(".SaleItem").change(function () {
        eachAmount();
    });

    $(".SelectSG").each(function () {
        showPopuWindows($(this).attr("href"), 640, 200, $(this));
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
        //$(".First" + $(this).attr("ParentCategoryID")).find(".Subtotal").each(function () {
        //    TotalNum += parseFloat($(this).val());
        //});

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
        //$(".First" + $(this).attr("ParentCategoryID")).find(".Subtotal").each(function () {
        //    TotalNum += parseFloat($(this).val());
        //});

    });

});


//计算销售总价
function SetTotalAmount() {
    var TotalAmount = 0;
    $(".Subtotal").each(function () {
        if ($(this).val() != "") {
            TotalAmount = TotalAmount + parseFloat($(this).val());
        }
    });
    $("#txtAggregateAmount").attr("value", TotalAmount);
    $("#hideAggregateAmount").attr("value", TotalAmount);
    $("#txtRealAmount").attr("value", TotalAmount);
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


function ShowPrint() {
    window.open('QuotedPriceShow.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>');
    return false;
}

function BeginPrint() {
    window.open('QuotedPriceShowOrPrint.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>&IsFirstMake=0');
    return false;
}
$(window).load(function () {
    if ('<%=Request["OnlyView"]%>') {
        $("input,textarea,select").attr("disabled", "disabled");
        $("input[type=button],.btn").hide();
        $(".needshow").show();
    }
});



    function ShowEmployeePopu(Parent) {
        var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
        showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
        $("#SelectEmpLoyeeBythis").click();
    }



    function ShowEmployeePopu1(Parent) {
        var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideSaleEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
        showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
        $("#SelectEmpLoyeeBythis").click();
    }

    function ShowEmployeePopu2(Parent) {
        var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideMissionManager&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
        showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
        $("#SelectEmpLoyeeBythis").click();
    }

    function ShowEmployeeProduct() {
        var Url = "/AdminPanlWorkArea/Foundation/FD_Storehouse/FD_StroehouseProductList.aspx";
        showPopuWindows(Url, 800, 800, "#SelectEmpLoyeeBythis");
        $("#SelectEmpLoyeeBythis").click();
    }


    </script>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>+
    <asp:HiddenField ID="HideSaleSplit" runat="server" ClientIDMode="Static" />

    <asp:HiddenField ID="hideDisStatehide" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hideDisState" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="HideHaveEmployee" runat="server" ClientIDMode="Static" />

    <asp:HiddenField ID="hideRowKey" runat="server" ClientIDMode="Static" />
    <br />
    <table class="table" style="width: 1325px; margin-left: 60px; margin-right: 60px;">

        <tr>
            <td style="white-space: nowrap; text-align: right;"><b>订单编号:</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCoder" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b></b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpinpai" runat="server" Text="" Visible="false"></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>

            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;</td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;</td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;</td>

        </tr>
        <tr>
            <td style="white-space: nowrap; text-align: right;"><b>新娘姓名:</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBrideName" runat="server" ClientIDMode="Static" Text=""></asp:TextBox>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>联系方式:</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBridePhone" runat="server" ClientIDMode="Static" Text=""></asp:TextBox>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>酒店:</b></td>
            <td style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<cc2:ddlHotel ID="ddlHotel" runat="server" ClientIDMode="Static" Width="100px"></cc2:ddlHotel>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>婚期:</b></td>
            <td style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtpartyday" runat="server" onclick="WdatePicker()" ClientIDMode="Static"></asp:TextBox>

            </td>
            <td style="white-space: nowrap; text-align: right;">
                <b>时段:</b>
            </td>
            <td style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTimerSpan" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="white-space: nowrap; text-align: right;"><b>新郎姓名:</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtGroomName" runat="server" ClientIDMode="Static" Text=""></asp:TextBox>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>联系方式:</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtGroomPhone" runat="server" ClientIDMode="Static" Text=""></asp:TextBox>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>策划师:</b></td>
            <td style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEmpLoyee" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>签约时间:</b></td>
            <td style="text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="txtSuccessDates" onclick="WdatePicker()" /></td>
            <td>
                <asp:Button runat="server" ID="btnSave" Text="保存" CssClass="btn btn-primary" OnClick="btnSave_Click" /></td>
        </tr>
        <tr>
            <td style="white-space: nowrap; text-align: right;"><b>订单类型:</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddlType" runat="server" Width="85" ClientIDMode="Static">
                    <asp:ListItem>新增</asp:ListItem>
                    <asp:ListItem>套系</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="white-space: nowrap; text-align: right;"><b>风格:</b></td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddlfengge" runat="server" Width="85" AutoPostBack="True" OnSelectedIndexChanged="ddlfengge_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="white-space: nowrap; text-align: right;">
                <b class="NoAdd">套系名称:</b>
            </td>

            <td style="white-space: nowrap; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddlPackgeName" runat="server" Width="85" class="NoAdd">
                </asp:DropDownList>
            </td>
            <td style="white-space: nowrap; text-align: right;">
                <asp:Button ID="btnInsert" runat="server" Text="导入" OnClick="btnInsert_Click" CssClass="btn btn-primary NoAdd" OnClientClick="return RealSubmit();" />
            </td>
            <td style="white-space: nowrap; text-align: left;"></td>
            <td style="white-space: nowrap; text-align: right;">&nbsp;</td>
            <td style="white-space: nowrap; text-align: left;">&nbsp;</td>
        </tr>
    </table>

    <table border="0" style="width: 1325px; margin-left: 60px; margin-right: 60px;" class="table-select">
        <tr>
            <td style="text-align: left; vertical-align: middle;">
                <a id="SelectPG" <%=ShowOrHide() %> class="SelectPG btn btn-warning" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=0&ControlKey=hidePgValue&Callback=btnStarFirstpg"><b style="color: black;">添加类别</b></a>
                &nbsp;&nbsp;报价单名称：<asp:TextBox ID="txtQuotedTitle" runat="server" Width="255" Enabled="false"></asp:TextBox>
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

                    <asp:HiddenField runat="server" ID="HideFlowerSplit" ClientIDMode="Static" />

                </div>
                <a class="btn btn-danger " href="#" onclick="ShowEmployeeProduct();">查看库房产品</a>
                <input id="btnShows" type="button" value="预览" onclick="ShowPrint();" class="btn btn-primary btn-mini btnSubmit" />
                <asp:Button ID="btnPrints" runat="server" Text="打印" CssClass="btn btn-primary btn-mini btnSubmit" OnClientClick="return BeginPrint();" Visible="false" />
            </td>
        </tr>
    </table>
    <br />
    <div style="overflow-x: auto; overflow-y: hidden; margin-left: 60px; text-align: center;">
        <asp:Repeater ID="repfirst" runat="server" OnItemDataBound="repfirst_ItemDataBound" OnItemCommand="repfirst_ItemCommand">
            <ItemTemplate>
                <asp:HiddenField ID="hidefirstCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                <asp:HiddenField ID="hideKey" Value='<%#Eval("ChangeID") %>' runat="server" />
                <table class="First<%#Eval("CategoryID") %> table-select" border="1">
                    <tr>
                        <td width="120" style="white-space: nowrap;"><b>类别</b></td>
                        <td width="150" style="white-space: nowrap;"><b>项目</b></td>
                        <td width="140" style="white-space: nowrap;"><b>产品/服务内容</b></td>
                        <td width="380" style="white-space: nowrap;"><b>具体要求</b></td>
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
                            <tr id='CreateEdit<%#Eval("ChangeID")%>' onclick="javascript:SetRowKey('CreateEdit<%#Eval("ChangeID")%>')">
                                <td <%#Container.ItemIndex>0?"class='NeedHide'  style='border-top-style:none;border-bottom-style:none;white-space:nowrap;'" : "style='border-top-color:black;border-top-style:double;border-bottom-style:none;white-space:'nowrap;'"%> <%#Eval("ParentCategoryID")%>>
                                    <b <%#Container.ItemIndex>0?"class='NeedHideLable'": ""%>><%#Eval("ParentCategoryName") %></b>
                                    <br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<a class="SelectSG btn btn-primary" id="SelectSG" <%#HideSelectItem(Container.ItemIndex) %> categoryid="<%#Eval("CategoryID") %>" href="/AdminPanlWorkArea/ControlPage/SelectCategory.aspx?ParentID=<%#Eval("ItemLevel").ToString()=="1"?Eval("CategoryID"):Eval("ParentCategoryID") %>&ControlKey=hideSecondValue&Callback=btnCreateSecond" onclick="return SetfunCategoryID(this);"><span style="color: white">增加项目</span></a>
                                    <asp:HiddenField ID="hidePriceKey" Value='<%#Eval("ChangeID") %>' runat="server" />
                                    <asp:HiddenField ID="hideCategoryID" Value='<%#Eval("CategoryID") %>' runat="server" />
                                </td>
                                <td <%#Eval("ItemLevel").ToString()=="3"?"style='border-top-style:none;border-bottom-style:none;word-break:break-all;'": "style='border-top-color:black;border-top-style:double;border-bottom-style:none;word-break:break-all;'" %>>
                                    <b <%#HideSelectProduct(Eval("ItemLevel")) %>><%#Eval("CategoryName") %></b><br />
                                    &nbsp;&nbsp;&nbsp;&nbsp;<a class="SelectSG btn  btn-primary " id="A2" <%#HideSelectProduct(Eval("ItemLevel")) %> categoryid="<%#Eval("CategoryID") %>" href="/AdminPanlWorkArea/ControlPage/SelectProduct.aspx?CategoryID=<%#Eval("CategoryID") %>&ControlKey=hideThirdValue&Callback=btnCreateThired&PartyDate=<%#Request["PartyDate"] %>&CustomerID=<%=Request["CustomerID"] %>" onclick="SetProduct(this);">增加产品</a></td>
                                <td style="word-break: break-all;"><%#Eval("ServiceContent")%><div style="display: none;">
                                    <asp:TextBox ID="txtProductName" Enabled="false" runat="server" Text='<%#Eval("ServiceContent") %>' Width="120"></asp:TextBox>
                                </div>
                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox Style="margin: 0; padding: 0;" ID="txtRequirement" runat="server" Width="100%" Height="100%" Text='<%#Eval("Requirement") %>' check="0" TextMode="MultiLine"></asp:TextBox></td>
                                <td>
                                    <a href="#" onclick="ShowFileUploadPopu('<%#Eval("QuotedID") %>','<%#Eval("ChangeID") %>')" class="btn btn-mini   btn-primary">上传</a>
                                    <%--<a id="inline" href="#data<%#Eval("ChangeID") %>" class="btn " kesrc="#data<%#Eval("ChangeID")%>" <%#HideforNoneImage(Eval("ChangeID")) %>>查看</a>--%>
                                    <a href='#' onclick="ShowFileShowPopu('<%#Eval("QuotedID") %>','<%#Eval("ChangeID") %>')" class="btn btn-mini  btn-primary needshow">查看</a>
                                    <div style="display: none;">
                                        <div id='data<%#Eval("ChangeID") %>'><%#GetKindImage(Eval("ChangeID")) %></div>
                                    </div>
                                </td>
                                <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>">
                                    <asp:TextBox ID="txtSalePrice" CssClass="SalePrice" MaxLength="8" runat="server" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("UnitPrice") %>' Width="75"></asp:TextBox></td>
                                <td><%#Eval("Unit") %></td>
                                <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW"%>">
                                    <asp:TextBox ID="txtQuantity" CssClass="Quantity" MaxLength="8" runat="server" Width="75" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %>' ParentCategoryID='<%#Eval("ParentCategoryID") %>' Text='<%#Eval("Quantity") %>'></asp:TextBox>
                                    <asp:HiddenField ID="hiddenAvailableCount" Value='<%#GetAvailableCount(Eval("ProductID"),Eval("RowType")) %>' runat="server" />
                                </td>
                                <td class="SetSubtotal<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW" %> Total<%#Eval("ParentCategoryID") %>">
                                    <asp:TextBox ID="txtSubtotal" class='Subtotal' MaxLength="8" runat="server" Width="75" ProductID='<%#Eval("ChangeID")+Container.ItemIndex.ToString()+"ROW"' Text='<%#Eval("Subtotal") %>'></asp:TextBox>
                                </td>
                                <td style="display: none;">
                                    <asp:TextBox ID="txtRemark" runat="server" Width="140" Text='<%#Eval("Remark") %>' MaxLength="50"></asp:TextBox></td>
                                <td>
                                    <asp:LinkButton ID="lnkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("ChangeID") %>' runat="server" OnClientClick="return CheckDelete();" CssClass="btn btn-danger ">删除</asp:LinkButton></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
                <div style="margin-right: 268px; padding: 2px; margin-top: auto; text-align: right;">
                    <span>分项合计：
                    <input id='txtTotal<%#Eval("CategoryID") %>' style="width: 75px;" disabled="disabled" type="text" class="ItemAmount" value="<%#Eval("ItemAmount")==null?"0":Eval("ItemAmount")%>" />
                        <div style="display: none;">销售价：<asp:TextBox ID="txtSaleItem" CssClass="SaleItem" Width="75" runat="server" Text='<%#Eval("ItemSaleAmount") %>'></asp:TextBox></div>
                        <asp:Button ID="btnSaveItem" CommandName="SaveItem" runat="server" Text="保存分项" CssClass="btn btn-success" />
                    </span>

                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#txtSalePrices").focusout(function () {
                var mPrice = $("#txtSalePrices").val();
                var pPrice = $("#txtPersonPrice").val();
                var oPrice = $("#txtOtherPrice").val();
                var sum = mPrice * 1 + pPrice * 1 + oPrice * 1;
                document.getElementById("<%=txtRealAmount.ClientID%>").value = sum;
            });

        });

    </script>
    <table style="margin-left: 60px;">
        <tr style="margin-bottom: 50px;">
            <td>物料总价格：</td>
            <td>
                <asp:TextBox Style="margin: 0; padding: 0;" runat="server" ID="txtMaterialPrice" ClientIDMode="Static" Width="90" Enabled="false" /></td>
            <td>物料销售价：</td>
            <td>
                <asp:TextBox Style="margin: 0; padding: 0;" Width="90" runat="server" ID="txtSalePrices" ClientIDMode="Static" />

            </td>
            <td>人员价格：
                <asp:TextBox runat="server" ID="txtPersonPrice" Style="margin: 0; padding: 0;" Width="90" ClientIDMode="Static" Enabled="false" />
            </td>
            <td>其他价格：
                <asp:TextBox runat="server" ID="txtOtherPrice" Style="margin: 0; padding: 0;" Width="90" ClientIDMode="Static" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <a target="_blank" href='QuotedPriceShowOrPrint.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>&IsFirstMake=0&Type=2' class="btn btn-primary">打印物料单</a>

                <a target="_blank" href='QuotedPriceShowOrPrint.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>&IsFirstMake=0&Type=1' class="btn btn-primary">打印人员单</a>

                <a target="_blank" href='QuotedPriceShowOrPrint.aspx?QuotedID=<%=Request["QuotedID"]%>&OrderID=<%=Request["OrderID"]%>&CustomerID=<%=Request["CustomerID"]%>&IsFirstMake=0&Type=3' class="btn btn-primary">打印其它</a>
            </td>
        </tr>
        <tr>
            <td style="display: none;">报价单总价：</td>
            <td style="display: none;">
                <asp:TextBox Style="margin: 0; padding: 0;" ID="txtAggregateAmount" Width="90" runat="server" ClientIDMode="Static" Enabled="false" Visible="false"></asp:TextBox>
                <asp:HiddenField ID="hideAggregateAmount" ClientIDMode="Static" runat="server" />
            </td>
            <td>销售总价：</td>
            <td>
                <asp:TextBox Style="margin: 0; padding: 0;" ID="txtRealAmount" Width="90" runat="server" ClientIDMode="Static" MaxLength="10" Enabled="false"></asp:TextBox></td>
            <td></td>
            <td>
                <asp:Label runat="server" ID="lblOrderMoney" Visible="false" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:TextBox Visible="false" Style="margin: 0; padding: 0;" ID="txtEarnestMoney" Width="90" runat="server" ClientIDMode="Static" MaxLength="10"></asp:TextBox></td>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <asp:Label ID="lblyukuan" runat="server" Text="" Visible="false"></asp:Label></td>
        </tr>
        <tr>
            <td>说明：</td>
            <td colspan="5">
                <asp:TextBox Style="margin: 0; padding: 0;" ID="txtRemark" runat="server" Rows="3" TextMode="MultiLine" CssClass="widthText"></asp:TextBox></td>
        </tr>
        <tr>
            <td>下次沟通时间：</td>
            <td colspan="5">
                <cc1:DateEditTextBox Style="margin: 0; padding: 0;" ID="txtNextFlowDate" onclick="WdatePicker();" runat="server"></cc1:DateEditTextBox></td>
        </tr>
        <tr>
            <td>是否有取件：</td>
            <td colspan="5">
                <asp:RadioButtonList ID="rdoIsHaveFile" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">有取件</asp:ListItem>
                    <asp:ListItem Value="1">无取件</asp:ListItem>
                </asp:RadioButtonList>

            </td>
        </tr>

        <tr class="HaveFile" style="display: none;">
            <td class="auto-style1">取件类型：</td>
            <td style="white-space: nowrap;" class="auto-style1">
                <asp:CheckBox ID="chkPhoto" runat="server" Text="照片" /></td>
            <td colspan="4" class="auto-style1">
                <asp:CheckBox ID="chkAvi" runat="server" Text="视频" /></td>
        </tr>

        <tr>
            <td colspan="6">
                <asp:Button ID="btnSaveChange" runat="server" Text="保存" OnClick="btnSaveChange_Click" CssClass="btn btn-success btn-mini" Visible="false" OnClientClick="return CheckPage();" />
                <%--<a target="_blank" href="QuotedPriceShow.aspx?QuotedID=<%=GetIDForType(2) %>&OrderID=<%=GetIDForType(1) %>&CustomerID=<%=GetIDForType(3) %>" >--%>
                <asp:Button runat="server" ID="lbtnShows" Text="预览" class="btn btn-primary btn-mini" OnClientClick="return ShowPrint()" Style="margin-top: 5px;" />
                <%--</a>--%>
                <asp:Button ID="btnfinish" runat="server" Text="确认签约" OnClick="btnfinish_Click" CssClass="btn btn-success btn-mini btnSubmit" OnClientClick="return CheckPage()" />
                <asp:Button ID="btnPrint" runat="server" Text="打印" CssClass="btn btn-primary btn-mini btnSubmit" OnClientClick="return BeginPrint();" Visible="false" Style="margin: 5px 0px 0px 0px;" />
                <asp:Button ID="btnRefresh" runat="server" Text="刷新" CssClass="btn btn-success btn-mini btnSubmit" OnClick="btnRefresh_Click" />
                <input type="button" runat="server" visible="false" value="新增变更单" class="btn btn-primary" id="btnQutedPriceChange" onclick="InsertIsChange()" />
            </td>
        </tr>

        <tr class="FlowerItem">
            <td>选择花艺部门负责人</td>
            <td id="<%=Guid.NewGuid().ToString() %>" colspan="5">
                <input style="margin: 0" runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowEmployeePopu(this);" type="text" />
                <a href="#" class="btn  btn-primary" onclick="ShowEmployeePopu(this);" class="SetState">选择负责人</a>
                <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='' runat="server" />

            </td>
        </tr>
        <tr class="SaleItem">
            <td>选择报价负责人</td>
            <td id="<%=Guid.NewGuid().ToString() %>" colspan="5">
                <input style="margin: 0" runat="server" id="txtSaleEmployee" class="txtEmpLoyeeName" onclick="ShowEmployeePopu1(this);" type="text" />
                <a href="#" class="btn  btn-primary" onclick="ShowEmployeePopu1(this);" class="SetState">选择负责人</a>
                <asp:HiddenField ID="hideSaleEmpLoyeeID" ClientIDMode="Static" Value='' runat="server" />

            </td>
        </tr>

        <tr style="display: none;">
            <td>选择婚礼管家</td>
            <td id="<%=Guid.NewGuid().ToString() %>" colspan="5">
                <input style="margin: 0" runat="server" id="txtMissionManager" class="txtEmpLoyeeName" onclick="ShowEmployeePopu2(this);" type="text" />
                <a href="#" class="btn  btn-primary" onclick="ShowEmployeePopu1(this);" class="SetState">选择负责人</a>
                <asp:HiddenField ID="hideMissionManager" ClientIDMode="Static" Value='' runat="server" />
            </td>
        </tr>

        <tr class="CheckNode">
            <td style="color: red;">
                <asp:Label ID="lblChecksNode" runat="server" Text="审核说明:"></asp:Label></td>
            <td colspan="5">
                <asp:Label ID="lblCheckContent" runat="server" Font-Bold="True" Font-Underline="True" ForeColor="#0066FF"></asp:Label>
                <asp:HiddenField ID="hidecheck" Value="0" runat="server" ClientIDMode="Static" />
            </td>
        </tr>

        <tr class="CheckNode">
            <td colspan="6">
                <table style="width: 100%; background-color: aliceblue;" border="1">
                    <tr>
                        <td runat="server" id="td_InserChange" colspan="5" height="35px">
                            <a id="btnAddChange" onclick='ShowParentWindow(1000,1000);' <%=IsHaveChange() %> class="btn btn-info">新增变更单</a>
                        </td>
                    </tr>
                    <tr>
                        <td>变更单</td>
                        <td>变更金额</td>
                        <td>变更时间</td>
                        <td>变更人</td>
                        <td>变更操作</td>
                    </tr>
                    <tr>
                        <td>
                            <div runat="server" id="div_Change"></div>
                        </td>
                        <td>
                            <div runat="server" id="div_ChangeAmount"></div>
                        </td>
                        <td>
                            <div runat="server" id="div_ChangeDate"></div>
                        </td>
                        <td>
                            <div runat="server" id="div_ChangeEmployee"></div>
                        </td>
                        <td>
                            <div runat="server" id="div_Handle"></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" height="25px"></td>
                    </tr>
                </table>
            </td>
        </tr>

    </table>
    <br />
    <div class="widget-content tab-content" style="height: auto; overflow: auto;" <%=InsertShow() %>>
        <div class="tab-pane active" id="tab9" style="width: 100%;" <%=InsertShow() %>>
            <iframe class="framchild" id="Iframe1" width="100%" frameborder="0" name="table" <%=InsertShow() %>></iframe>
        </div>
    </div>

</asp:Content>
