<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FL_ReturnVisitManagerUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.ReturnVisit.FL_ReturnVisitManagerUpdate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>

<%@ Register Src="../../../Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 18px;
        }

        .auto-style2 {
            width: 60px;
            height: 18px;
        }

        .auto-style3 {
            width: 164px;
            height: 18px;
        }

        ul li {
            list-style-type: none;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnSaveReturn").click(function () {
                if ($("#txtSuggest").val() == "") {
                    alert("请输入客户的建议");
                    return false;
                }
            });
        });
        //点击文本框 弹出部门人员列表
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=1&ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 480, 380, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div class="widget-box">
        <div class="widget-content">
            <uc1:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />
            <ul>
                <li style="float: left;">回访人：</li>
                <li style="float: left;">
                    <asp:Label runat="server" ID="lblReturnPerson" /></li>

                <li style="float: right;">
                    <asp:Label runat="server" ID="lblReturnDate" /></li>
                <li style="float: right;">回访时间：</li>
            </ul>
            <table border="1" style="width: 100%; border-color: black;">
                <%--<tr>
                    <td style="text-align: left"><a href="FL_CustomerReturnVisitManagerNot.aspx?NeedPopu=1">返回列表</a></td>


                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="text-align: left">回访时间<asp:Label ID="lblReturnDate" runat="server" Text=""></asp:Label>
                    </td>

                </tr>--%>
                <tr>
                    <td style="text-align: left">回访问题</td>
                    <td style="text-align: left; width: auto">结果</td>
                    <td style="text-align: left">备注</td>
                    <td style="text-align: left">反馈</td>

                </tr>

                <asp:Repeater ID="repItemList" runat="server" OnItemDataBound="repItemList_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: left">
                                <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("ItemTitle") %>'></asp:Label>
                            </td>
                            <td style="text-align: left">
                                <asp:RadioButtonList ID="rdoState" runat="server" RepeatDirection="Horizontal" DataTextField="Name" DataValueField="InviteStateID" CellPadding="5" CellSpacing="5">
                                </asp:RadioButtonList>
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtSourceNode" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtReturnSource" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblNextComeDate">下次到店时间</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtDate" Text="" onclick="WdatePicker();" runat="server" /></td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtDateSource" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtDateReturnSource" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblSuggest">回访说明</asp:Label></td>
                    <td style="margin-top: 5px">
                        <asp:TextBox ID="txtSuggest" Text="" runat="server" Width="250px" TextMode="MultiLine" ClientIDMode="Static" Rows="5" />
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtSuggestSource" runat="server" TextMode="MultiLine" ClientIDMode="Static" Rows="5"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtSuggestReturnSource" runat="server" TextMode="MultiLine" ClientIDMode="Static" Rows="5"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>状态</td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rdoState" RepeatColumns="3" RepeatDirection="Horizontal" CellPadding="10" CellSpacing="10" ClientIDMode="Static">
                            <%--<asp:ListItem Text="跟单" Value="1" Selected="True" />
                            <asp:ListItem Text="流失" Value="2" />
                            <asp:ListItem Text="改派" Value="3" />--%>
                        </asp:RadioButtonList>
                        <div id="divChanges">
                            <asp:TextBox runat="server" ID="txtEmpLoyee" CssClass="txtEmpLoyeeName" onclick="ShowPopu(this);" Text="<%#GetOrderEmployee() %>" />

                            <asp:LinkButton runat="server" ID="lbtnChange" CssClass="btn btn-primary btn-mini" Text="改派" OnClick="lbtnChange_Click" />
                            <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='' runat="server" />
                        </div>
                        <script type="text/javascript">
                            $(document).ready(function () {
                                $("#divChanges").css("display", "none");
                                $("#rdoState").change(function () {
                                    var zhi = $("#<%=rdoState.ClientID %>").find("input[type='radio']:checked").val();
                                    if (zhi == "3") {             //派单
                                        $("#divChanges").css("display", "block");
                                    } else {
                                        $("#divChanges").css("display", "none");
                                    }
                                });
                                var zhi = $("#<%=rdoState.ClientID %>").find("input[type='radio']:checked").val();
                                if (zhi == "3") {             //派单
                                    $("#divChanges").css("display", "block");
                                } else {
                                    $("#divChanges").css("display", "none");
                                }
                            });
                        </script>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left" colspan="4">
                        <asp:Button ID="btnSaveReturn" runat="server" Text="保存回访结果" ClientIDMode="Static" OnClick="btnSaveReturn_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
