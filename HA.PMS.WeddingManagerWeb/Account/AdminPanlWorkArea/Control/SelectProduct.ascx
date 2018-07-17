<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectProduct.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SelectProduct" %>
<script>


    var ActiveControlClass = "";
    var OldActiveControl = $("#btnMy");

    function Show(DivID) {

        $(".Productdiv").hide();
        $(".btn").removeClass("active");
        $(DivID).addClass("active");
        $(DivID).show();
    }



    function SetActive(Control, ActiveDiv, Typer) {

        if (ActiveDiv == '#Create') {
            $("#btnSaveSelect0").hide();
            $("#btnSaveSelect").hide();
        } else {
            $("#btnSaveSelect0").show();
            $("#btnSaveSelect").show();
        }

        if (ActiveControlClass != "") {
            $(OldActiveControl).removeClass();

        }
        ActiveControlClass = $(Control).attr("class");
        OldActiveControl = Control;
        $(OldActiveControl).removeClass();


        $(".Productdiv").hide();
        $(ActiveDiv).show();
        $("#hideTyper").attr("value", Typer);
        return false;
    }

    $(document).ready(function () {
        $("#treeHouseCatgoryList").hide();

        var Suppname = "<%=Request["SupplierName"] %>";

        $("#tblContent").css({ "border": "3px groove #a9a6a6" });
        $("#tblContent td").css({ "border": "1px solid #a9a6a6" });
        if (Suppname.length > 0) {
            $("#btnSupp").click();
        }


        $(":checkbox").click(function () {
            if ($(this).is(":checked")) {
                var Productname = $(this).parent().parent("tr").children("td").eq(1).text();
                $("#txtProductList").text($("#txtProductList").text() + Productname + "\r\n");
                $("#hideSelectProduct").attr("value", $("#hideSelectProduct").val() + "," + $(this).val());


            } else {
                var Productname = $(this).parent().parent("tr").children("td").eq(1).text();
                $("#txtProductList").text($("#txtProductList").text().replace(Productname + "\r\n", ""));
                $("#hideSelectProduct").attr("value", $("#hideSelectProduct").val().replace("," + $(this).val(), ""));

            }
        })

    });

    $(window).load(function () {
        BindString(20, '<%=txtProductName.ClientID%>:<%=txtUnit.ClientID%>');
            BindMoney('<%=txtPurchasePrice.ClientID%>:<%=txtSalePrice.ClientID%>');
            BindUInt('<%=txtCount.ClientID%>');
            BindText(50, '<%=txtRemark.ClientID%>:<%=txtExplain.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
        });
        function CheckProduct() {
            return ValidateForm('input[check],textarea[check]');
        }

        function Settabs(ControlID, Index, Uri) {

            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#" + Index).show();
            $("#" + Uri).hide();
            $("#treeCatogryList").show();
            $(".MineProject").hide();
        }
 
</script>

<div class="widget-title">
    <ul class="nav nav-tabs">
        <li id="DefaultTab" class="HAtab" onclick="Settabs(this,'SelectProduct','Create');" style="background-color: #2E363F;"><a data-toggle="tab" href="#111">选择产品</a></li>
        <li id="tab2" class="HAtab" onclick="Settabs(this,'Create','SelectProduct');"><a data-toggle="tab" href="#111">新购入</a></li>
        <li id="Li1" class="HAtab"><a data-toggle="tab" href="SelectStoreProduct.aspx?CategoryID=<%=Request["CategoryID"] %>&ControlKey=hideThirdValue&Callback=btnCreateThired&PartyDate=<%=Request["PartyDate"] %>">库房</a></li>

    </ul>
</div>

<table style="width: 100%;" id="SelectProduct">
    <tr>
        <td colspan="2"></td>
    </tr>

    <tr>
        <td>已选产品</td>
        <td>
            <asp:TextBox ID="txtProductList" ClientIDMode="Static" runat="server" TextMode="MultiLine" Width="310" Height="80"></asp:TextBox>
            <asp:HiddenField ID="hideSelectProduct" ClientIDMode="Static" runat="server" />
        </td>
    </tr>

    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Button ID="btnSaveSelect0" ClientIDMode="Static" runat="server" CssClass="btn btn-success" Text="保存已选产品" OnClick="btnSaveSelect_Click" />
        </td>
    </tr>

    <tr>

        <td style="text-align: right; display: none;">

            <asp:HiddenField ID="hideTyper" ClientIDMode="Static" runat="server" Value="3" />

        </td>
        <td style="text-align: left; display: none;">
            <label <%=SetStyleforSystemControlKey("ShowQuotedGYS",1) %>>
                <asp:Button ID="btnSupp" ClientIDMode="Static" runat="server" Text="供应商" OnClientClick="return SetActive(this, '#TablerepProductByCatogryList',2);" CssClass="btn btn-mini" OnClick="btnSupp_Click" />
                <%if (Request["SupplierName"] != "库房" && Request["SupplierName"] != "" && Request["SupplierName"] != null)
                  { %>
                <input id="btnShoallSupply" class="btn btn-mini" type="button" value="所有供应商" onclick="return SetActive(this, '#TablerepSuppProductList', 4);" />
                <%
                  } %>
            </label>
        </td>
    </tr>
    <tr>
        <td><span style="color: red; height: 25px;">类别/项目</span></td>
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="vertical-align: top; width: 22%; border-right-style: groove;">
            <div style="overflow-y: auto; height: 550px;">
                <asp:TreeView ID="treeCatogryList" ClientIDMode="Static" runat="server" OnSelectedNodeChanged="treeCatogryList_SelectedNodeChanged" ShowLines="True" ExpandDepth="0"></asp:TreeView>
            </div>
        </td>
        <td style="vertical-align: top; width: 78%;">

            <div style="overflow-y: auto; height: 550px;">





                <table border="1" cellpadding="5" cellspacing="1" id="TablerepProductByCatogryforWarehouseList" class="Productdiv">

                    <tr>
                        <td>选择</td>
                        <td>产品</td>

                        <td>价格</td>
                        <td>库房</td>
                        <td>剩余数量</td>
                    </tr>
                    <asp:Repeater ID="repProductByCatogryforWarehouseList" runat="server">
                        <ItemTemplate>
                            <tr <%#IsDisposible(Eval("KindID"))&&(GetAvailableCount(Eval("KindID"),Request["CustomerID"],null)<=0)?"style='display:none;'":string.Empty  %>>
                                <td style="width: 30px;" bgcolor="#FFFFEE">
                                    <input runat="server" id="chkProduct" type="checkbox" value='<%#Eval("Keys") %>' /></td>
                                <td style="width: 100px;" class="ProductName" bgcolor="#FFFFEE"><%#Eval("ProductName") %></td>

                                <td><%#Eval("SalePrice") %></td>
                                <td><%#GetHouseNameByID(Eval("StorehouseID")) %></td>
                                <td <%#IsDisposible(Eval("KindID")).Equals(true)?"style='display:none;'":"style='color: red;'" %>>中午：<%#GetAvailableCount(Eval("KindID"),Request["CustomerID"],"中午") %>晚上：<%#GetAvailableCount(Eval("KindID"),Request["CustomerID"],"晚上") %></td>
                                <td <%#IsDisposible(Eval("KindID")).Equals(false)?"style='display:none'":"style='color: red;'"%>>当前：<%#GetAvailableCount(Eval("KindID"),Request["CustomerID"],null)  %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <asp:Button ID="btnSaveSelect" ClientIDMode="Static" runat="server" CssClass="btn btn-success" Text="保存已选产品" OnClick="btnSaveSelect_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
</table>
 
<div id="Create" class="Productdiv">
    <table id="tblContent">
        <tr>
            <td style="width: 60px;">产品名称称</td>
            <td>
                <asp:TextBox ID="txtProductName" check="1" tip="（必填）限20个字符！" MaxLength="20" Width="110" runat="server"></asp:TextBox>
                <span style="color: red">*</span>
            </td>

        </tr>
        <tr>
            <td>产品单位</td>
            <td>
                <asp:TextBox ID="txtUnit" check="1" tip="（必填）产品数量的计量" MaxLength="20" Width="70" runat="server"></asp:TextBox>
                <span style="color: red">*</span>
            </td>

        </tr>

        <tr>
            <td>成本价</td>
            <td>
                <asp:TextBox ID="txtPurchasePrice" check="1" tip="（必填）采购单价" MaxLength="10" runat="server"></asp:TextBox>
                <span style="color: red">*</span>
            </td>

        </tr>

        <tr>
            <td>销售价</td>
            <td>
                <asp:TextBox ID="txtSalePrice" check="1" tip="（必填）销售单价" MaxLength="10" Width="70" runat="server"></asp:TextBox>
                <span style="color: red">*</span>
            </td>

        </tr>
        <tr>
            <td>数量</td>
            <td>
                <asp:TextBox ID="txtCount" check="0" tip="数量只能为整数，不填默认为 0" MaxLength="16" Width="70" runat="server"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td>具体要求</td>
            <td>
                <asp:TextBox ID="txtExplain" TextMode="MultiLine" Rows="3" tip="限50个字符！" MaxLength="50" Width="194" Style="margin: 0;" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>备注</td>
            <td>
                <asp:TextBox ID="txtRemark" TextMode="MultiLine" check="0" tip="限50个字符！" MaxLength="50" Rows="3" Width="194" Style="margin: 0;" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>供应商</td>
            <td>
                <asp:DropDownList ID="ddlSupply" Width="130" runat="server"></asp:DropDownList>
            </td>

        </tr>
        <tr>
            <td>属性:</td>
            <td>
                <asp:RadioButtonList ID="rdotyper" CellPadding="15" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">专业团队</asp:ListItem>
                    <asp:ListItem Value="1" Selected="True">物料</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="btnCreateDate" CssClass="btn btn-success" OnClientClick="return CheckProduct()" runat="server" Text="提交新购入" OnClick="btnCreateDate_Click" />
            </td>

        </tr>
    </table>


</div>


