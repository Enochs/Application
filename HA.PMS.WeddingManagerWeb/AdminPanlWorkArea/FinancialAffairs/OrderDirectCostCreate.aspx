<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Default" CodeBehind="OrderDirectCostCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.OrderDirectCostCreate" Title="填写成本明细" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>


<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">

    <script type="text/javascript">


        //选择内部人员
        function ShowEmployeePopu1(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmployeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //选择内部人员
        function ShowEmployeePopu2(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmployee&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //选择内部人员
        function ShowEmployeePopu3(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideOtherEmployee&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        //指定四大金刚
        function ChangeFourGuardian(Parent, CallBack) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectFourGuardian.aspx?Callback=" + CallBack + "&ALL=true&ControlKey=hideSuppID&SetEmployeeName=txtSuppName&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //选择供应商
        function ChangeSuppByCatogry(Parent, CallBack) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectSupplierBythis.aspx?Callback=" + CallBack + "&ALL=true&ControlKey=HideSupplier&SetEmployeeName=txtSupplier&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //选择供应商
        function ChangeSuppByCatogry2(Parent, CallBack) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectSupplierBythis.aspx?Callback=" + CallBack + "&ALL=true&ControlKey=HideSuppliers&SetEmployeeName=txtSuppliers&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }






        function CheckPage() {
            return ValidateForm('input[check],textarea[check]');
        }

        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
        });

        function HideCtable(ControlID) {
            $(".WorkTable").show();
            $(ControlID).hidden();

        }

        $(document).ready(function () {
            $(".NeedCost").change(function () {
                GetTotal();
            });


            $("#txtSuppName").css("display", "none");
            $("#txtSupplier").css("display", "none");
            $("#txtPersonName").css("display", "none");

            //人员
            if ($("#ddlPersonType").val() == 1) {
                $("#txtSuppName").css("display", "none");
                $("#Text1").css("display", "block");
                $("#txtPersonName").css("display", "none");
            } else if ($("#ddlPersonType").val() == 2) {
                $("#txtSuppName").css("display", "block");
                $("#Text1").css("display", "none");
                $("#txtPersonName").css("display", "none");
            } else if ($("#ddlPersonType").val() == 3) {
                $("#txtSuppName").css("display", "none");
                $("#Text1").css("display", "none");
                $("#txtPersonName").css("display", "block");
            }

            //物料
            if ($("#ddlPersonTypes").val() == 1) {
                $("#txtSupplier").css("display", "none");
                $("#txtInPerson").css("display", "block");
                $("#txtMaterial").css("display", "none");
            } else if ($("#ddlPersonTypes").val() == 2) {
                $("#txtSupplier").css("display", "block");
                $("#txtInPerson").css("display", "none");
                $("#txtMaterial").css("display", "none");
            } else if ($("#ddlPersonTypes").val() == 3) {
                $("#txtSupplier").css("display", "none");
                $("#txtInPerson").css("display", "none");
                $("#txtMaterial").css("display", "block");
            }

            //其他
            if ($("#ddlOtherType").val() == 1) {
                $("#txtSuppliers").css("display", "none");
                $("#txtOtherEmployee").css("display", "block");
            } else {
                $("#txtSuppliers").css("display", "block");
                $("#txtOtherEmployee").css("display", "none");
            }


            //人员
            $("#ddlPersonType").click(function () {
                if ($("#ddlPersonType").val() == 1) {
                    $("#txtSuppName").css("display", "none");
                    $("#Text1").css("display", "block");
                    $("#txtPersonName").css("display", "none");
                } else if ($("#ddlPersonType").val() == 2) {
                    $("#txtSuppName").css("display", "block");
                    $("#Text1").css("display", "none");
                    $("#txtPersonName").css("display", "none");
                } else if ($("#ddlPersonType").val() == 3) {
                    $("#txtSuppName").css("display", "none");
                    $("#Text1").css("display", "none");
                    $("#txtPersonName").css("display", "block");
                }
            });

            //物料
            $("#ddlPersonTypes").click(function () {
                if ($("#ddlPersonTypes").val() == 1) {
                    $("#txtSupplier").css("display", "none");
                    $("#txtInPerson").css("display", "block");
                    $("#txtMaterial").css("display", "none");
                } else if ($("#ddlPersonTypes").val() == 2) {
                    $("#txtSupplier").css("display", "block");
                    $("#txtInPerson").css("display", "none");
                    $("#txtMaterial").css("display", "none");
                } else if ($("#ddlPersonTypes").val() == 3) {
                    $("#txtSupplier").css("display", "none");
                    $("#txtInPerson").css("display", "none");
                    $("#txtMaterial").css("display", "block");
                }
            });


            //其他
            $("#ddlOtherType").click(function () {
                if ($("#ddlOtherType").val() == 1) {
                    $("#txtSuppliers").css("display", "none");
                    $("#txtOtherEmployee").css("display", "block");
                } else {
                    $("#txtSuppliers").css("display", "block");
                    $("#txtOtherEmployee").css("display", "none");
                }
            });



        });


        function GetTotal() {
            var TotalSum = 0;
            $(".NeedCost").each(function () {
                if ($(this).val() != "") {
                    TotalSum += parseFloat($(this).val());
                }
            });
            $("#txtCost").attr("value", TotalSum);
            var GetOnlyData = (TotalSum / parseFloat($("#hideTotal").val())).toFixed(2);
            $("#txtProfitMargin").attr("value", ((1 - GetOnlyData) * 100).toFixed(2) + "%");
        }

        function GoSubmit() {
            if (confirm("请注意！确认成本后将无法进行修改!")) {
                return true;
            } else {
                return false;
            }
        }

        $(document).ready(function () {
            //人员
            $("#btnSave").click(function () {
                if ($("#txtContent").val() == "" || $("#txtSumtotal").val() == "") {
                    alert("请将信息填写完整");
                    return false;
                }
            });

            //物料
            $("#btnSaveMaterial").click(function () {
                if ($("#txtMaterialContent").val() == "" || $("#txtMaterialSumTotal").val() == "") {
                    alert("请将信息填写完整");
                    return false;
                }
            });

            //其他
            $("#btnSaveOther").click(function () {
                if ($("#txtOtherContent").val() == "" || $("#txtOtherSumTotal").val() == "") {
                    alert("请将信息填写完整");
                    return false;
                }
            });

        });


        function ShowUpdateWindows(OrderID, QuotedID, CustomerID, Control, Type) {
            //var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignCreate.aspx?OrderID=" + OrderID + "&QuotedID=" + QuotedID + "&CustomerID=" + CustomerID + "&type=1";
            var url = "http://www.baidu.com";
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 800, 1000, "a#" + $(Control).attr("id"));
        }

    </script>

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <div style="overflow-y: auto; height: 1000px;">
        <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />

        <a target="_blank" class="btn btn-primary" href="/AdminPanlWorkArea/QuotedPrice/QuotedPriceShowOrPrint.aspx?CustomerID=<%=Request["CustomerID"]%>&OrderID=<%=Request["OrderID"]%>&QuotedID=<%=Request["QuotedID"]%>&IsFirstMake=0&NeedPopu=1">查看原始订单</a>
        <br />
        <p></p>
        <table class="WorkTable table table-bordered table-striped" style="width: 98%;">
            <tbody>
                <tr>
                    <td>
                        <!--人员-->
                        <table class="WorkTable table table-bordered table-striped" style="width: 98%;" border="1">
                            <thead>
                                <tr>
                                    <td style="font-weight: bold; background-color: #808080">执行团队：
                                    </td>
                                    <td colspan="6">
                                        <asp:Button runat="server" ID="btnSavePerson" Text="保存" CssClass="btn btn-primary" OnClick="btnSavePerson_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <th width="5%" colspan="2">姓名</th>
                                    <th width="210" height="auto">说明</th>
                                    <th width="40">计划成本</th>
                                    <th width="40">实际成本</th>
                                    <th width="40">已付款</th>
                                    <th width="40">未付款</th>
                                    <th width="80">评价</th>
                                    <%--<th width="150">优点</th>
                                    <th width="150">缺点</th>--%>
                                    <th width="150">备注</th>
                                    <th>操作</th>
                                </tr>
                                <asp:Repeater ID="repEmployeeCost" runat="server" OnItemCommand="repOtherCost_ItemCommand">
                                    <ItemTemplate>
                                        <tr>

                                            <td colspan="2">
                                                <%#Eval("Name") %>
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Width="200px" ID="lblContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Width="85px" ID="lblPlanSumtotal" Text='<%#Eval("Sumtotal") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" Width="80px" ClientIDMode="Static" ID="txtActualSumtotal" Text='<%#Eval("ActualSumTotal") == null ? Eval("Sumtotal") : Eval("ActualSumTotal") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" Width="80px" ClientIDMode="Static" ID="txtPayMent" Text='<%#Eval("PayMent") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblNoPayMent" Text='<%#Eval("NoPayMent") %>' />
                                            </td>
                                            <td>
                                                <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemark" Text='<%#Eval("Remark") %>'></asp:TextBox></</td>
                                            <td>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" CommandName="DeletePerson" CommandArgument='<%#Eval("CostSumId") %>' Text="删除" CssClass="btn btn-primary btn-mini" OnClientClick="return confirm('您确定要删除吗?')" />
                                                <asp:HiddenField runat="server" ID="HideRowType" Value='<%#Eval("RowType") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </thead>
                            <tbody>
                                <tr runat="server" id="tr_AddPerson">
                                    <td style="border-right: 0px solid gray;">
                                        <asp:DropDownList runat="server" ID="ddlPersonType" ClientIDMode="Static">
                                            <asp:ListItem Text="内部人员" Value="1" />
                                            <asp:ListItem Text="四大金刚" Value="2" />
                                            <asp:ListItem Text="手动录入" Value="3" />
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td id='<%=Guid.NewGuid().ToString()+"1" %>' style="border-left: 0px solid gray;">
                                                    <!--内部人员-->
                                                    <asp:TextBox runat="server" ID="Text1" ClientIDMode="Static" CssClass="txtEmpLoyeeName" onclick="ShowEmployeePopu1(this);" />
                                                    <asp:HiddenField ID="hideEmployeeID" ClientIDMode="Static" Value='' runat="server" />

                                                    <!--四大金刚-->
                                                    <asp:TextBox runat="server" ID="txtSuppName" ClientIDMode="Static" CssClass="txtSuppName" onclick="ChangeFourGuardian(this, 'btnFourGuardianSave')" />
                                                    <asp:HiddenField ID="hideSuppID" runat="server" ClientIDMode="Static" />

                                                    <!---手动录入-->
                                                    <asp:TextBox runat="server" ID="txtPersonName" ClientIDMode="Static" />
                                                    <label style="display: none;">
                                                        <asp:Button ID="btnFourGuardianSave" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="四大金刚保存" CssClass="btn btn-success" OnClick="btnFourGuardianSave_Click" />
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtContent" Width="200px" TextMode="MultiLine" /></td>
                                    <td>
                                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtSumtotal" /></td>
                                    <td colspan="3">
                                        <asp:Button runat="server" ID="btnSave" Text="保存" ClientIDMode="Static" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td <%#IsHide(1) %> colspan="8" style="height: 25px;"></td>
                                </tr>
                                <tr>
                                    <td colspan="8">人员销售价:<asp:Label runat="server" ID="lblPersonSale" />&nbsp;&nbsp;
                                        人员成本价:<asp:Label runat="server" ID="lblPersonCost" />&nbsp;&nbsp;
                                        人员毛利率:<asp:Label runat="server" ID="lblPersonRate" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <!--物料-->
                        <table class="WorkTable table table-bordered table-striped" style="width: 98%;" border="1">
                            <thead>
                                <tr>
                                    <td style="font-weight: bold; background-color: #808080" width="150">物料成本成本明细</td>
                                    <td colspan="6">
                                        <asp:Button runat="server" ID="btnSaveMaterials" Text="保存" CssClass="btn btn-primary" OnClick="btnSavePerson_Click" /></td>
                                </tr>
                                <tr>
                                    <th width="5%" colspan="2">供应商</th>
                                    <th width="210" height="auto">说明</th>
                                    <th width="40">计划成本</th>
                                    <th width="40">实际成本</th>
                                    <th width="40">已付款</th>
                                    <th width="40">未付款</th>
                                    <th width="80">评价</th>
                                    <%--<th width="150">优点</th>
                                    <th width="150">缺点</th>--%>
                                    <th width="150">备注</th>
                                    <th>操作</th>
                                </tr>

                                <asp:Repeater ID="repSupplierCost" runat="server" OnItemCommand="repOtherCost_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="2">
                                                <a <%#Eval("RowType").ToString() == "10" ? "style='display:none;'" : "" %>>
                                                    <asp:Label runat="server" ID="lblMName" Text='<%#Eval("Name") %>' /></a>
                                                <a <%#Eval("RowType").ToString() != "10" ? "style='display:none;'" : "" %> href='/AdminPanlWorkArea/Carrytask/CarrytaskWork/Designclass/DesignclassReports.aspx?OrderID=<%#Eval("OrderID") %>&QuotedID=<%#GetQuotedID(Eval("CustomerID")) %>&CustomerID=<%#Eval("CustomerID") %>&type=1' target="_blank"><%#Eval("Name") %></a>
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Width="200px" ID="lblContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Width="85px" ID="lblPlanSumtotal" Text='<%#Eval("Sumtotal") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" Width="80px" ID="txtActualSumtotal" Text='<%#Eval("ActualSumTotal") == null ? Eval("Sumtotal") : Eval("ActualSumTotal") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" Width="80px" ID="txtPayMent" Text='<%#Eval("PayMent") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblNoPayMent" Text='<%#Eval("NoPayMent") %>' />
                                            </td>
                                            <td>
                                                <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                            </td>
                                            <%--<td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtAdvance" Text='<%#Eval("Advance") %>' Enabled="false"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtShortCome" Text='<%#Eval("ShortCome") %>' Enabled="false"></asp:TextBox></</td>--%>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemark" Text='<%#Eval("Remark") %>'></asp:TextBox></</td>
                                            <td>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" CommandName="DeleteSupplier" CommandArgument='<%#Eval("CostSumId") %>' Text="删除" CssClass="btn btn-primary btn-mini" OnClientClick="return confirm('您确定要删除吗?')" />
                                                <asp:HiddenField runat="server" ID="HideRowType" Value='<%#Eval("RowType") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>

                            </thead>
                            <tbody>
                                <tr runat="server" id="tr_AddMaterial">
                                    <td style="border-right: 0px solid gray;">
                                        <asp:DropDownList runat="server" ID="ddlPersonTypes" ClientIDMode="Static">
                                            <asp:ListItem Text="内部人员" Value="1" />
                                            <asp:ListItem Text="供应商" Value="2" />
                                            <%--<asp:ListItem Text="手动录入" Value="3" />--%>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td id="<%=Guid.NewGuid().ToString() %>" style="border-left: 0px solid gray;">
                                                    <!--内部人员-->
                                                    <asp:TextBox runat="server" ID="txtInPerson" ClientIDMode="Static" CssClass="txtEmpLoyeeName" onclick="ShowEmployeePopu2(this);" />
                                                    <asp:HiddenField ID="hideEmployee" ClientIDMode="Static" Value='' runat="server" />

                                                    <!--供应商-->
                                                    <asp:TextBox runat="server" ID="txtSupplier" ClientIDMode="Static" CssClass="txtSuppName" onclick="ChangeSuppByCatogry(this, 'btnSavesupperSave')" />
                                                    <asp:HiddenField ID="HideSupplier" runat="server" ClientIDMode="Static" />

                                                    <!---手动录入-->
                                                    <asp:TextBox runat="server" ID="txtMaterial" ClientIDMode="Static" />
                                                    <label style="display: none;">
                                                        <asp:Button ID="btnSavesupperSave" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="供应商保存" CssClass="btn btn-success" OnClick="btnSavesupperSave_Click" />
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMaterialContent" Width="200px" TextMode="MultiLine" /></td>
                                    <td>
                                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtMaterialSumTotal" /></td>
                                    <td colspan="3">
                                        <asp:Button runat="server" ID="btnSaveMaterial" Text="保存" ClientIDMode="Static" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td <%#IsHide(2) %> colspan="8" style="height: 25px;"></td>
                                </tr>
                                <tr>
                                    <td colspan="8">物料销售价:<asp:Label runat="server" ID="lblMaterialSale" />&nbsp;&nbsp;
                                        物料成本价:<asp:Label runat="server" ID="lblMaterialCost" />&nbsp;&nbsp;
                                        物料毛利率:<asp:Label runat="server" ID="lblMaterialRate" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <!--其他-->
                        <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                            <thead>
                                <tr>
                                    <td style="font-weight: bold; background-color: #808080" width="150">其他： </td>
                                    <td colspan="6">
                                        <asp:Button runat="server" ID="btnSaveOthers" Text="保存" CssClass="btn btn-primary" OnClick="btnSavePerson_Click" /></td>
                                </tr>
                                <tr>
                                    <th width="12%" colspan="2">名称</th>
                                    <th width="210" height="auto">说明</th>
                                    <th width="40">计划成本</th>
                                    <th width="40">实际成本</th>
                                    <th width="40">已付款</th>
                                    <th width="40">未付款</th>
                                    <th width="80">评价</th>
                                    <%--<th width="150">优点</th>
                                    <th width="150">缺点</th>--%>
                                    <th width="150">备注</th>
                                    <th>操作</th>
                                </tr>
                                <asp:Repeater ID="repOtherCost" runat="server" OnItemCommand="repOtherCost_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="2"><%#Eval("Name") %>
                                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" Width="200px" ID="lblContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" Width="85px" ID="lblPlanSumtotal" Text='<%#Eval("Sumtotal") %>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" Width="80px" ID="txtActualSumtotal" Text='<%#Eval("ActualSumTotal") == null ? Eval("Sumtotal") : Eval("ActualSumTotal") %>'></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" Width="80px" ID="txtPayMent" Text='<%#Eval("PayMent") %>' />
                                            </td>
                                            <td>
                                                <asp:Label runat="server" ID="lblNoPayMent" Text='<%#Eval("NoPayMent") %>' />
                                            </td>
                                            <td>
                                                <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtAdvance" Text='<%#Eval("Advance") %>' Enabled="false"></asp:TextBox></td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtShortCome" Text='<%#Eval("ShortCome") %>' Enabled="false"></asp:TextBox></</td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemark" Text='<%#Eval("Remark") %>'></asp:TextBox></</td>
                                            <td>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" CommandName="DeleteOther" CommandArgument='<%#Eval("CostSumId") %>' Text="删除" CssClass="btn btn-primary btn-mini" OnClientClick="return confirm('您确定要删除吗?')" />
                                                <asp:HiddenField runat="server" ID="HideRowType" Value='<%#Eval("RowType") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </thead>
                            <tbody>
                                <tr runat="server" id="tr_AddOther">
                                    <td style="border-right: 0px solid gray;">
                                        <asp:DropDownList runat="server" ID="ddlOtherType" ClientIDMode="Static">
                                            <asp:ListItem Text="内部人员" Value="1" />
                                            <asp:ListItem Text="供应商" Value="2" />
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td id='<%=Guid.NewGuid().ToString()+"2" %>' style="border-left: 0px solid gray;">
                                                    <!--内部人员-->
                                                    <asp:TextBox runat="server" ID="txtOtherEmployee" ClientIDMode="Static" CssClass="txtEmpLoyeeName" onclick="ShowEmployeePopu3(this);" />
                                                    <asp:HiddenField ID="hideOtherEmployee" ClientIDMode="Static" Value='' runat="server" />

                                                    <!--供应商-->
                                                    <asp:TextBox runat="server" ID="txtSuppliers" ClientIDMode="Static" CssClass="txtSuppliers" onclick="ChangeSuppByCatogry2(this, 'btnSaveOtherSupplier')" />
                                                    <asp:HiddenField ID="HideSuppliers" runat="server" ClientIDMode="Static" />

                                                    <label style="display: none;">
                                                        <asp:Button ID="btnSaveOtherSupplier" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="供应商保存" CssClass="btn btn-success" OnClick="btnSavesupperSave_Click" />
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtOtherContent" Width="200px" TextMode="MultiLine" /></td>
                                    <td>
                                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtOtherSumTotal" /></td>
                                    <td colspan="3">
                                        <asp:Button runat="server" ID="btnSaveOther" Text="保存" ClientIDMode="Static" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td <%#IsHide(3) %> colspan="8" style="height: 25px;"></td>
                                </tr>
                                <tr>
                                    <td colspan="8">其他销售价:<asp:Label runat="server" ID="lblQuotedOtherSale" />&nbsp;&nbsp;
                                        其他成本价:<asp:Label runat="server" ID="lblQuotedOtherCost" />&nbsp;&nbsp;
                                        其他毛利率:<asp:Label runat="server" ID="lblQuotedOtherRate" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </td>
                </tr>
            </tbody>
        </table>

        <!--销售费用-->
        <table class="table table-bordered table-striped" style="width: 98%; margin-top: 0px;">
            <thead>
                <tr>
                    <td style="font-weight: bold; background-color: #808080" width="12%">销售费用： </td>
                    <td colspan="4"></td>

                </tr>
                <tr>
                    <th width="5%">项目</th>
                    <th width="210" height="auto">说明</th>
                    <th width="40">计划成本</th>
                    <th width="40">实际成本</th>
                    <th width="40">已付款</th>
                    <th width="40">未付款</th>
                    <th width="80">备注</th>
                </tr>
                <asp:Repeater ID="repSaleMoney" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblCategoryName" Text='<%#Eval("Name") %>' />
                                <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" Width="200px" ID="lblContent" TextMode="MultiLine" Text='<%#Eval("Content") %>'></asp:Label></td>
                            <td>
                                <asp:Label ID="lblPlanSumtotal" Width="85px" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtActualSumTotal" Width="80px" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:TextBox></td>
                            <td>
                                <asp:TextBox runat="server" Width="80px" ID="txtPayMent" Text='<%#Eval("PayMent") == null ? "0" : Eval("PayMent") %>' />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoPayMent" Text='<%#Eval("NoPayMent") == null ? "0" : Eval("NoPayMent") %>' />
                            </td>
                            <td colspan="2">
                                <asp:TextBox runat="server" ID="txtRemark" Text='<%#Eval("Remark") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </thead>
            <tbody>
                <tr runat="server" id="tr1">
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtSaleTitle" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtSaleContent" Width="200px" /></td>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtSaleSumTotal" /></td>
                    <td colspan="3">
                        <asp:Button runat="server" ID="btnSaveSaleTitle" Text="保存" ClientIDMode="Static" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                    </td>
                </tr>
            </tbody>
        </table>

        <!--总评项目-->
        <table class="WorkTable table table-bordered table-striped" style="width: 98%;" border="1">
            <thead>
                <tr>
                    <td style="font-weight: bold; background-color: #808080" width="150">总评： </td>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <th width="12%">总评项目</th>
                    <th width="210" height="auto">说明</th>
                    <th width="120">评价</th>
                    <th>备注</th>
                </tr>
                <asp:Repeater ID="rptSatisfaction" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("EvaluationName") ==null ? "总评项目" : Eval("EvaluationName") %></td>
                            <td>
                                <%#Eval("EvaluationContent") ==null ? "" : Eval("EvaluationContent") %></td>
                            <td>
                                <%#GetNameByEvaulationId(Eval("EvaluationId")) ==null ? "未评价" : GetNameByEvaulationId(Eval("EvaluationId")) %></td>
                            <td>
                                <%#Eval("EvaluationRemark") ==null ? "" : Eval("EvaluationRemark") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </thead>
        </table>

        <!--供金额 成本 利润-->
        <table class="table table-bordered table-striped" style="width: 98%;">
            <tr>
                <td width="100">订单总金额</td>
                <td>
                    <asp:TextBox ID="txtTotal" runat="server" Enabled="false"></asp:TextBox></td>
                <td>&nbsp;</td>
                <td width="100">合计</td>
                <td>
                    <asp:TextBox ID="txtCost" ClientIDMode="Static" runat="server" Enabled="false"></asp:TextBox></td>
                <td>毛利</td>
                <td>
                    <asp:TextBox ID="txtMaoLi" ClientIDMode="Static" runat="server" Enabled="false"></asp:TextBox></td>
                <td>毛利率</td>
                <td>
                    <asp:TextBox ID="txtProfitMargin" ClientIDMode="Static" runat="server" Enabled="false"></asp:TextBox></td>
            </tr>

            <tr>
                <td colspan="9" style="text-align: center;">
                    <asp:Button ID="btnSaveChange" runat="server" Text="保存全部" OnClick="btnSaveChange_Click" ClientIDMode="Static" CssClass="btn btn-success" />
                    <asp:HiddenField ID="hideTotal" runat="server" ClientIDMode="Static" />

                    <asp:Button ID="btnFinish" runat="server" Text="确认成本明细" OnClick="btnFinish_Click" OnClientClick="return GoSubmit();" CssClass="btn btn-danger" />

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
