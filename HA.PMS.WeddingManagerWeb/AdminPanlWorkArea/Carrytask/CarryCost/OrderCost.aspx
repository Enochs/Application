<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderCost.aspx.cs" StylesheetTheme="Default" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost.OrderCost" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <style type="text/css">
        .tablestyle {
            border-width: 1px;
            border-style: solid;
            border-color: Black;
            border-collapse: collapse;
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

        //弹出打印页面
        function ShowPrint(KeyID, Control) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarryCost/OrderCostPrint.aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&NeedPopu=1";
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 1000, 1500, "a#" + $(Control).attr("id"));
        }


        $(document).ready(function () {
            $("#txtSuppName").css("display", "none");
            $("#txtSupplier").css("display", "none");
            $("#txtSuppliers").css("display", "none");

            if ($("#ddlPersonType").val() == 1) {
                $("#txtSuppName").css("display", "none");
                $("#Text1").css("display", "block");
            } else {
                $("#txtSuppName").css("display", "block");
                $("#Text1").css("display", "none");
            }

            if ($("#ddlPersonTypes").val() == 1) {
                $("#txtSupplier").css("display", "none");
                $("#txtInPerson").css("display", "block");
            } else {
                $("#txtSupplier").css("display", "block");
                $("#txtInPerson").css("display", "none");
            }

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
                } else {
                    $("#txtSuppName").css("display", "block");
                    $("#Text1").css("display", "none");
                }
            });

            //物料
            $("#ddlPersonTypes").click(function () {
                if ($("#ddlPersonTypes").val() == 1) {
                    $("#txtSupplier").css("display", "none");
                    $("#txtInPerson").css("display", "block");
                } else {
                    $("#txtSupplier").css("display", "block");
                    $("#txtInPerson").css("display", "none");
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

            //项目完结
            $("#btn_OrderFinish").click(function () {

                if ($("#ddlEvaluation").val() == 6) {
                    alert("请选择总体评价");
                    return false;
                } else if ($("#ddlActionEvaluation").val() == 6) {
                    alert("请选择流程执行");
                    return false;
                } else if ($("#ddlDeployEvaulation").val() == 6) {
                    alert("请选择场景布置");
                    return false;
                }
            });



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

        $(window).load(function () {
            BindUInt('<%=txtSumtotal.ClientID%>:<%=txtMaterialSumTotal.ClientID%>:<%=txtOtherSumTotal.ClientID%>');
        });

            function ShowUpdateWindows(KeyID, Control, Type) {
                //var Url = "FD_MaterialUpdate.aspx?MaterialId=" + KeyID + "&type=" + Type;
                //$(Control).attr("id", "updateShow" + KeyID);
                //showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));

                var Url = "/AdminPanlWorkArea/Carrytask/CarryCost/OrderCostPrint.aspx?DispatchingID=<%=Request["DispatchingID"]%>&CustomerID=<%=Request["CustomerID"]%>&NeedPopu=1";
                $(Control).attr("id", "updateShow" + KeyID);
                showPopuWindows(Url, 1000, 1000, "a#" + $(Control).attr("id"));
            }

    </script>

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <a href="#" onclick='ShowUpdateWindows(0,this);' class="SetState btn btn-primary">打印</a>
    <table style="width: 100%;" border="0">
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 98%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" colspan="2">执行团队：
                            </td>
                            <td colspan="5">
                                <asp:Button runat="server" ID="btnSavePerson" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" />
                                人员销售价:<asp:Label runat="server" ID="lblPersonSale" />&nbsp;&nbsp;
                                        人员成本价:<asp:Label runat="server" ID="lblPersonCost" />&nbsp;&nbsp;
                                        人员毛利率:<asp:Label runat="server" ID="lblPersonRate" />&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <th width="22%" colspan="2">姓名</th>
                            <th width="210">说明</th>
                            <th width="80">实际成本</th>
                            <th width="80">计划成本</th>
                            <th width="50">已支付</th>
                            <th width="80">评价</th>
                            <th width="150">备注</th>
                            <th>操作</th>
                        </tr>
                        <asp:Repeater ID="repEmployeeCost" runat="server" OnItemCommand="repOtherCost_ItemCommand">
                            <ItemTemplate>
                                <tr>

                                    <td colspan="2"><%#Eval("Name") %></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtContent" Text='<%#Eval("Content") %>' TextMode="MultiLine" Width="200px" /></td>
                                    <td><%#Eval("ActualSumTotal") %></td>
                                    <td <%=StatuHideViewInviteInfo() %>>
                                        <asp:TextBox runat="server" ID="txtSumTotal" Text='<%#Eval("Sumtotal") %>' />
                                        <asp:Label runat="server" ID="lblSumTotal" Text='<%#Eval("Sumtotal") %>' Visible="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPayMent" Text='<%#Eval("PayMent") %>' />
                                        <asp:Label runat="server" ID="lblPayMent" Text='<%#Eval("PayMent") %>' Visible="false" />
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlEvaluation" SelectedValue='<%#(Eval("Evaluation").ToString() == "6" && GetState(Eval("CustomerID")) == 208) ? 2 : Eval("Evaluation") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" />
                                        <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("Remark") == null ? "" : Eval("Remark") %>' /></td>
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
                                <asp:Button runat="server" ClientIDMode="Static" ID="btnSave" Text="保存" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </tbody>

                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 98%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150" colspan="2">物料成本明细</td>
                            <td colspan="5">
                                <asp:Button runat="server" ID="btnSaveMaterials" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" />
                                物料销售价:<asp:Label runat="server" ID="lblMaterialSale" />&nbsp;&nbsp;
                                        物料成本价:<asp:Label runat="server" ID="lblMaterialCost" />&nbsp;&nbsp;
                                        物料毛利率:<asp:Label runat="server" ID="lblMaterialRate" />&nbsp;&nbsp;

                            </td>

                        </tr>
                        <tr>

                            <th width="22%" colspan="2">供应商</th>
                            <th width="210">说明</th>
                            <th width="80">实际成本</th>
                            <th width="80">计划成本</th>
                            <th width="50">已支付</th>
                            <th width="80">评价</th>
                            <th width="150">备注</th>
                            <th>操作</th>
                        </tr>

                        <asp:Repeater ID="repSupplierCost" runat="server" OnItemCommand="repOtherCost_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td colspan="2"><%#Eval("Name") %></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtContent" Text='<%#Eval("Content") %>' TextMode="MultiLine" Width="200px" /></td>
                                    <td><%#Eval("ActualSumTotal") %></td>
                                    <td <%=StatuHideViewInviteInfo() %>>
                                        <asp:TextBox runat="server" ID="txtSumTotal" Text='<%#Eval("Sumtotal") %>' />
                                        <asp:Label runat="server" ID="lblSumTotal" Text='<%#Eval("Sumtotal") %>' Visible="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPayMent" Text='<%#Eval("PayMent") %>' />
                                        <asp:Label runat="server" ID="lblPayMent" Text='<%#Eval("PayMent") %>' Visible="false" />
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlEvaluation" SelectedValue='<%#(Eval("Evaluation").ToString() == "6" && GetState(Eval("CustomerID")) == 208) ? 2 : Eval("Evaluation") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" />
                                        <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("Remark") == null ? "" : Eval("Remark") %>' /></td>
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
                                <asp:Button runat="server" ClientIDMode="Static" ID="btnSaveMaterial" Text="保存" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </tbody>

                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 98%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">其他：
                            </td>
                            <td colspan="5">
                                <asp:Button runat="server" ID="btnSaveOrher" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" />
                                其他销售价:<asp:Label runat="server" ID="lblQuotedOtherSale" />&nbsp;&nbsp;
                                        其他成本价:<asp:Label runat="server" ID="lblQuotedOtherCost" />&nbsp;&nbsp;
                                        其他毛利率:<asp:Label runat="server" ID="lblQuotedOtherRate" />&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <th width="22%" colspan="2">名称</th>
                            <th width="210">说明</th>
                            <th width="80">实际成本</th>
                            <th width="80">计划成本</th>
                            <th width="50">已支付</th>
                            <th width="80">评价</th>
                            <th width="150">备注</th>
                            <th>操作</th>
                        </tr>
                        <asp:Repeater ID="repOtherCost" runat="server" OnItemCommand="repOtherCost_ItemCommand">
                            <ItemTemplate>
                                <tr>

                                    <td colspan="2"><%#Eval("Name") %></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtContent" Text='<%#Eval("Content") %>' TextMode="MultiLine" Width="200px" /></td>
                                    <td><%#Eval("ActualSumTotal") %></td>
                                    <td <%=StatuHideViewInviteInfo() %>>
                                        <asp:TextBox runat="server" ID="txtSumTotal" Text='<%#Eval("Sumtotal") %>' Width="60px" />
                                        <asp:Label runat="server" ID="lblSumTotal" Text='<%#Eval("Sumtotal") %>' Visible="false" />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtPayMent" Text='<%#Eval("PayMent") %>' />
                                        <asp:Label runat="server" ID="lblPayMent" Text='<%#Eval("PayMent") %>' Visible="false" />
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlEvaluation" SelectedValue='<%#(Eval("Evaluation").ToString() == "6" && GetState(Eval("CustomerID")) == 208) ? 2 : Eval("Evaluation") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" />
                                        <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("Remark") == null ? "" : Eval("Remark") %>' /></td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="lbtnDelete" CommandName="DeleteOther" CommandArgument='<%#Eval("CostSumId") %>' Text="删除" CssClass="btn btn-primary btn-mini" OnClientClick="return confirm('您确定要删除吗?')" />
                                        <asp:HiddenField runat="server" ID="HideRowType" Value='<%#Eval("RowType") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>

                    <tbody runat="server" id="t_AddOther">
                        <tr>
                            <td style="border-right: 0px solid gray;">
                                <asp:DropDownList runat="server" ID="ddlOtherType" ClientIDMode="Static">
                                    <asp:ListItem Text="内部人员" Value="1" />
                                    <asp:ListItem Text="供应商" Value="2" />
                                </asp:DropDownList>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td id="<%=Guid.NewGuid().ToString()+"2" %>" style="border-left: 0px solid gray;">
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
                                <asp:Button runat="server" ClientIDMode="Static" ID="btnSaveOther" Text="保存" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="table table-bordered table-striped" style="width: 98%; margin-top: 0px;">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="12%">销售费用： </td>
                            <td colspan="4"><asp:Button runat="server" ID="btnSaveSale" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" /></td>

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
                                        <asp:TextBox runat="server" ID="txtContent" Text='<%#Eval("Content") %>' TextMode="MultiLine" Width="200px" /></td>
                                    <td>
                                        <asp:TextBox ID="txtSumTotal" Width="85px" runat="server" Text='<%#Eval("Sumtotal") %>'></asp:TextBox></td>
                                    <td>
                                        <asp:Label ID="lblActualSumTotal" Width="80px" runat="server" Text='<%#Eval("ActualSumTotal") %>'></asp:Label></td>
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
                        <tr runat="server" id="tr_AddSale">
                            <td>
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtSaleTitle" />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtSaleContent" TextMode="MultiLine" Width="200px" /></td>
                            <td>
                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtSaleSumTotal" /></td>
                            <td colspan="3">
                                <asp:Button runat="server" ID="btnSaveSaleTitle" Text="保存" ClientIDMode="Static" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="6"><b style="color: brown; font-size: 15px;">合计:<asp:Label ID="lblSumMoney" runat="server" Text=""></asp:Label>元</b></td>
                        </tr>
                    </tfoot>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 98%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">总评 </td>
                            <td colspan="3">
                                <asp:Button runat="server" ID="btnSaveEvaulation" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" /></td>

                        </tr>
                        <tr>
                            <th>总评项目</th>
                            <th width="210">说明</th>
                            <th width="120">评价</th>
                            <th>备注</th>
                        </tr>
                        <asp:Repeater ID="rptSatisfaction" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td width="22%"><%#Eval("EvaluationName") %></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSumEvauluation" Text='<%#Eval("EvaluationContent") %>' TextMode="MultiLine" Style="width: 220px;" /></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlEvaluation" ClientIDMode="Static" SelectedValue='<%#Eval("EvaluationId") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" />
                                        <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("SatisfactionId") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSumEvauluationRemark" Text='<%#Eval("EvaluationRemark") %>' TextMode="MultiLine" Style="width: 400px;" /></td>
                                </tr>
                                <tr style="display: none;">
                                    <td><%#Eval("SatisfactionName") %></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSumSaticfaction" Text='<%#Eval("SatisfactionContent") %>' TextMode="MultiLine" Style="width: 220px;" /></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlSaticfaction" ClientIDMode="Static" SelectedValue='<%#Eval("SaEvaluationId") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" /></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSumSaticfactionRemark" Text='<%#Eval("SatisfactionRemark") %>' TextMode="MultiLine" Style="width: 400px;" /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>


                </table>
            </td>
        </tr>
    </table>

    <div class="divSub">
        <asp:Button runat="server" ID="btn_OrderFinish" ClientIDMode="Static" Text="项目完结" CssClass="btn btn-primary btn-mini" OnClick="btn_OrderFinish_Click" />
        <a href="#" onclick='ShowUpdateWindows(0,this);' class="SetState btn btn-primary">打印</a>
        <%--<asp:Button runat="server" ID="btnConfirm" ClientIDMode="Static" Text="确认评价" CssClass="btn btn-success" OnClick="btnConfirm_Click" />--%>
        <asp:ObjectDataSource ID="ObjEvaulationSource" runat="server" SelectMethod="GetByAll" TypeName="HA.PMS.BLLAssmblly.FD.WeddingSceneEvaluationResult"></asp:ObjectDataSource>

    </div>

</asp:Content>
