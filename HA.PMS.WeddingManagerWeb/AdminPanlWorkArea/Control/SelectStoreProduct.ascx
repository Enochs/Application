<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectStoreProduct.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SelectStoreProduct" %>


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
        });

    });


</script>

<div class="widget-title">
    <ul class="nav nav-tabs">
        <li id="Li2" class="HAtab"><%--<a data-toggle="tab" href="SelectProduct.aspx?CategoryID=<%=Request["CategoryID"] %>&ControlKey=hideThirdValue&Callback=btnCreateThired&PartyDate=<%=Request["PartyDate"] %>">选择报价产品</a>--%></li>
        <li id="Li1" class="HAtab"><a data-toggle="tab" href="#111">库房</a></li>
    </ul>
</div>
<table id="SelectProduct" style="width: 100%;">
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td>已选产品</td>
        <td>
            <asp:TextBox ID="txtProductList" runat="server" ClientIDMode="Static" Height="80" TextMode="MultiLine" Width="310"></asp:TextBox>
            <asp:HiddenField ID="hideSelectProduct" runat="server" ClientIDMode="Static" />
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
        <td>
            <asp:Button ID="btnSaveSelect0" runat="server" ClientIDMode="Static" CssClass="btn btn-success" OnClick="btnSaveSelect_Click" Text="保存已选产品" />
        </td>
    </tr>
    <tr>
        <td style="text-align: right; display: none;">
            <asp:HiddenField ID="hideTyper" runat="server" ClientIDMode="Static" Value="3" />
        </td>
        <td style="text-align: left; display: none;">
            <label <%=SetStyleforSystemControlKey("showquotedgys",1) %>="">
                <asp:Button ID="btnSupp" runat="server" ClientIDMode="Static" CssClass="btn btn-mini" OnClick="btnSupp_Click" OnClientClick="return SetActive(this, '#TablerepProductByCatogryList',2);" Text="供应商" />
                <%if (Request["SupplierName"] != "库房" && Request["SupplierName"] != "" && Request["SupplierName"] != null)
                  { %>
                <input id="btnShoallSupply" class="btn btn-mini" onclick="return SetActive(this, '#TablerepSuppProductList', 4);" type="button" value="所有供应商" />
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
                <asp:TreeView ID="treeCatogryList" runat="server" ClientIDMode="Static" ExpandDepth="0" OnSelectedNodeChanged="treeCatogryList_SelectedNodeChanged" ShowLines="True">
                </asp:TreeView>
            </div>
        </td>
        <td style="vertical-align: top; width: 78%;">
            <div style="overflow-y: auto; height: 550px;">
                <table id="TablerepProductByCatogryforWarehouseList" border="1" cellpadding="5" cellspacing="1" class="Productdiv">
                    <tr>
                        <td>选择</td>
                        <td>产品</td>
                        <td>价格</td>
                        <td>库房</td>
                        <td>剩余数量</td>
                        <%--<td>时间段</td>--%>
                    </tr>
                    <asp:Repeater ID="repProductByCatogryforWarehouseList" runat="server">
                        <ItemTemplate>
                            <%--<tr <%#IsDisposible(Eval("kindid"))&&(GetAvailableCount(Eval("kindid"),Request["customerid"],null)<=0)?"style='display:none;'":string.Empty  %>="">--%>
                            <tr>
                                <td bgcolor="#FFFFEE" style="width: 30px;">
                                    <input runat="server" id="chkProduct" type="checkbox" value='<%#Eval("Keys") %>' /></td>
                                <td bgcolor="#FFFFEE" class="ProductName" style="width: 100px;"><%#Eval("ProductName") %></td>
                                <td><%#Eval("SalePrice") %></td>
                                <td><%#GetHouseNameByID(Eval("StorehouseID")) %></td>
                                <td><%#Eval("Count") %></td>
                                <%--<td <%#IsDisposible(Eval("kindid")).Equals(true)?"style='display:none;'":"style='color: red;'" %>="">中午：<%#GetAvailableCount(Eval("KindID"),Request["CustomerID"],"中午") %>晚上：<%#GetAvailableCount(Eval("KindID"),Request["CustomerID"],"晚上") %></td>
                                <td <%#IsDisposible(Eval("kindid")).Equals(false)?"style='display:none'":"style='color: red;'"%>="">当前：<%#GetAvailableCount(Eval("KindID"),Request["CustomerID"],null)  %></td>--%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <asp:Button ID="btnSaveSelect" runat="server" ClientIDMode="Static" CssClass="btn btn-success" OnClick="btnSaveSelect_Click" Text="保存已选产品" />
        </td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
</table>


