<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="DetailsDispatchingEvaulation.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.DetailsDispatchingEvaulation" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_Evaulation">
        <table style="width: 100%;" border="0">
            <tr>
                <td>
                    <table class="WorkTable table table-bordered table-striped" style="width: 100%;" border="1">

                        <thead>

                            <tr>
                                <td style="font-weight: bold; background-color: #808080" width="150">执行团队：
                                </td>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <th>姓名</th>
                                <th width="250">说明</th>
                                <th width="120">评价</th>
                                <th width="auto">优点</th>
                                <th style="width: auto;">缺点</th>
                            </tr>
                            <asp:Repeater ID="repEmployeeCost" runat="server" >
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Name") %></td>
                                        <td><%#Eval("Content") %></td>
                                        <td>
                                            <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                        </td>
                                        <td>
                                            <%#Eval("Advance") %></td>
                                        <td>
                                            <%#Eval("ShortCome") %></td>
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
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <th>供应商</th>
                                <th width="250" height="auto">说明</th>
                                <th width="120">评价</th>
                                <th>优点</th>
                                <th>缺点</th>
                            </tr>

                            <asp:Repeater ID="repSupplierCost" runat="server" >
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Name") %></td>
                                        <td><%#Eval("Content") %></td>
                                        <td>
                                            <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                        </td>
                                        <td>
                                            <%#Eval("Advance") %></td>
                                        <td>
                                            <%#Eval("ShortCome") %></td>
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
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <th>名称</th>
                                <th width="250" height="auto">说明</th>
                                <th width="120">评价</th>
                                <th>优点</th>
                                <th>缺点</th>
                            </tr>
                            <asp:Repeater ID="rptStore" runat="server" >
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Name") %></td>
                                        <td><%#Eval("Content") %></td>
                                        <td>
                                            <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                        </td>
                                        <td>
                                            <%#Eval("Advance") %></td>
                                        <td>
                                            <%#Eval("ShortCome") %></td>
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
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <th>物料</th>
                                <th width="250" height="auto">说明</th>
                                <th width="120">评价</th>
                                <th>优点</th>
                                <th>缺点</th>
                            </tr>
                            <asp:Repeater ID="repBuyCost" runat="server" >
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Name") %></td>
                                        <td><%#Eval("Content") %></td>
                                        <td>
                                            <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                        </td>
                                        <td>
                                            <%#Eval("Advance") %></td>
                                        <td>
                                            <%#Eval("ShortCome") %></td>
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
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <th>鲜花名称</th>
                                <th width="250" height="auto">说明</th>
                                <th width="120">评价</th>
                                <th>优点</th>
                                <th>缺点</th>
                            </tr>
                            <asp:Repeater ID="repFlowerCost" runat="server" >
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("Name") %></td>
                                        <td><%#Eval("Content") %></td>
                                        <td>
                                            <%#GetNameByEvaulationId(Eval("Evaluation")) %>
                                        </td>
                                        <td>
                                            <%#Eval("Advance") %></td>
                                        <td>
                                            <%#Eval("ShortCome") %></td>
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
                                <td colspan="3"></td>
                            </tr>
                            <tr>
                                <th>总评项目</th>
                                <th width="250" height="auto">说明</th>
                                <th width="120">评价</th>
                                <th>备注</th>
                            </tr>
                            <asp:Repeater ID="rptSatisfaction" runat="server" >
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("EvaluationName") %></td>
                                        <td>
                                            <%#Eval("EvaluationContent") %></td>
                                        <td>
                                            <%#GetNameByEvaulationId(Eval("EvaluationId")) %></td>
                                        <td>
                                            <%#Eval("EvaluationRemark") %></td>
                                    </tr>
                                    <tr>
                                        <td><%#Eval("SatisfactionName") %></td>
                                        <td>
                                            <%#Eval("SatisfactionContent") %></td>
                                        <td>
                                            <%#GetNameByEvaulationId(Eval("SaEvaluationId")) %></td>
                                        <td>
                                            <%#Eval("SatisfactionRemark") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </thead>
                    </table>
                </td>
            </tr>
        </table>
        <div>
            <asp:ObjectDataSource ID="ObjEvaulationSource" runat="server" SelectMethod="GetByAll" TypeName="HA.PMS.BLLAssmblly.FD.WeddingSceneEvaluationResult"></asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>
