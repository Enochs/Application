<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" StylesheetTheme="Default" CodeBehind="OrderCostEvaluation.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarryCost.OrderCostEvaluation" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

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

        .auto-style1 {
            height: 48px;
        }
    </style>

    <table style="width: 100%;" border="0">
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">

                    <thead>

                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">执行团队：
                            </td>
                            <td colspan="2">
                                <asp:Button runat="server" ID="btnSavePri" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" /></td>
                        </tr>
                        <tr>
                            <th>姓名</th>
                            <th width="250">说明</th>
                            <th width="120">评价</th>
                            <th width="auto">优点</th>
                            <th style="width: auto;">缺点</th>
                        </tr>
                        <asp:Repeater ID="repEmployeeCost" runat="server">
                            <ItemTemplate>
                                <tr>

                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("Content") %></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlEvaluation" SelectedValue='<%#Eval("Evaluation") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" />
                                        <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAdvance" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("Advance") %>' /></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtShortCome" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("ShortCome") %>' /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>


                    </thead>

                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>

                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">供应商成本明细</td>
                            <td colspan="2">
                                <asp:Button runat="server" ID="btnSaveGong" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" /></td>
                        </tr>
                        <tr>
                            <th>供应商</th>
                            <th width="250" height="auto">说明</th>
                            <th width="120">评价</th>
                            <th>优点</th>
                            <th>缺点</th>
                        </tr>

                        <asp:Repeater ID="repSupplierCost" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("Content") %></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlEvaluation" SelectedValue='<%#Eval("Evaluation") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" /></td>
                                    <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAdvance" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("Advance") %>' /></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtShortCome" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("ShortCome") %>' /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                    </thead>
                </table>


            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">库房： </td>
                            <td colspan="2">
                                <asp:Button runat="server" ID="btnSaveKu" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" /></td>
                        </tr>
                        <tr>
                            <th>名称</th>
                            <th width="250" height="auto">说明</th>
                            <th width="120">评价</th>
                            <th>优点</th>
                            <th>缺点</th>
                        </tr>
                        <asp:Repeater ID="rptStore" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("Content") %></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlEvaluation" SelectedValue='<%#Eval("Evaluation") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" /></td>
                                    <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAdvance" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("Advance") %>' /></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtShortCome" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("ShortCome") %>' /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">采购物料： </td>
                            <td colspan="2">
                                <asp:Button runat="server" ID="btnSaveWu" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" /></td>
                        </tr>
                        <tr>
                            <th>物料</th>
                            <th width="250" height="auto">说明</th>
                            <th width="120">评价</th>
                            <th>优点</th>
                            <th>缺点</th>
                        </tr>
                        <asp:Repeater ID="repBuyCost" runat="server">
                            <ItemTemplate>
                                <tr>

                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("Content") %></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlEvaluation" SelectedValue='<%#Eval("Evaluation") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" /></td>
                                    <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAdvance" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("Advance") %>' /></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtShortCome" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("ShortCome") %>' /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">花艺成本： </td>
                            <td colspan="2">
                                <asp:Button runat="server" ID="btnSaveFlower" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" /></td>
                        </tr>
                        <tr>
                            <th>鲜花名称</th>
                            <th width="250" height="auto">说明</th>
                            <th width="120">评价</th>
                            <th>优点</th>
                            <th>缺点</th>
                        </tr>
                        <asp:Repeater ID="repFlowerCost" runat="server">
                            <ItemTemplate>
                                <tr>

                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("Content") %></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlEvaluation" SelectedValue='<%#Eval("Evaluation") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" /></td>
                                    <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAdvance" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("Advance") %>' /></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtShortCome" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("ShortCome") %>' /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">设计类清单： </td>
                            <td colspan="2">
                                <asp:Button runat="server" ID="btn_SavaClass" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" /></td>
                        </tr>
                        <tr>
                            <th>名称</th>
                            <th width="210" height="auto">说明</th>
                            <th width="80">评价</th>
                            <th width="150">优点</th>
                            <th width="150">缺点</th>
                        </tr>
                        <asp:Repeater ID="rptDesignClass" runat="server">
                            <ItemTemplate>
                                <tr>

                                    <td><%#Eval("Title") %></td>
                                    <td><%#Eval("Node") %></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlEvaluation" SelectedValue='<%#Eval("Evaluation") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" /></td>
                                        <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("DesignClassId") %>' />
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAdvance" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("Advance") %>' /></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtShortCome" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("ShortCome") %>' /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">其他费用： </td>
                            <td colspan="2">
                                <asp:Button runat="server" ID="btnSaveOrher" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" /></td>
                        </tr>
                        <tr>
                            <th>名称</th>
                            <th width="250" height="auto">说明</th>
                            <th width="120">评价</th>
                            <th>优点</th>
                            <th>缺点</th>
                        </tr>
                        <asp:Repeater ID="rptOtherCost" runat="server">
                            <ItemTemplate>
                                <tr>

                                    <td><%#Eval("Name") %></td>
                                    <td><%#Eval("Content") %></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlEvaluation" SelectedValue='<%#Eval("Evaluation") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" /></td>
                                    <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("CostSumId") %>' />
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAdvance" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("Advance") %>' /></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtShortCome" TextMode="MultiLine" Style="width: 200px;" Text='<%#Eval("ShortCome") %>' /></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">
                    <thead>
                        <tr>
                            <td style="font-weight: bold; background-color: #808080" width="150">总评： </td>
                            <td colspan="3">
                                <asp:Button runat="server" ID="btnSaveEvaulation" CssClass="btn btn-info" Text="保存" OnClick="btnSavePri_Click" /></td>
                        </tr>
                        <tr>
                            <th>总评项目</th>
                            <th width="250" height="auto">建议</th>
                            <th width="120">评价</th>
                            <th>备注</th>
                        </tr>
                        <asp:Repeater ID="rptSatisfaction" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%#Eval("EvaluationName") %></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSumEvauluation" Text='<%#Eval("EvaluationContent") %>' TextMode="MultiLine" Style="width: 220px;" /></td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlEvaluation" ClientIDMode="Static" SelectedValue='<%#Eval("EvaluationId") %>' DataSourceID="ObjEvaulationSource" DataValueField="EvaluationId" DataTextField="EvaluationName" /></td>
                                    <asp:HiddenField runat="server" ID="HiddenValue" Value='<%#Eval("SatisfactionId") %>' />
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
        <tr>
            <td align="center">
                <asp:Button runat="server" ID="btnConfirm" ClientIDMode="Static" Text="确认" CssClass="btn btn-success" OnClick="btnConfirm_Click" /></td>
        </tr>
    </table>
    <div>
        <asp:ObjectDataSource ID="ObjEvaulationSource" runat="server" SelectMethod="GetByAll" TypeName="HA.PMS.BLLAssmblly.FD.WeddingSceneEvaluationResult"></asp:ObjectDataSource>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnConfirm").click(function () {
                if ($("#ddlEvaluation").val() == 6) {
                    alert("请选择总体评价");
                    return false;
                }
                //if ($("#ddlSaticfaction").val() == 6) {
                //    alert("请选择总体满意度");
                //    return false;
                //}
            });
        });
    </script>
</asp:Content>
