<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="QuotedPriceFlowerItemEdit.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedManager.QuotedPriceFlowerItemEdit" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>

<%@ Register Src="../../Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            if ($("#hideisChange").val() == "") {

                $(".Price").attr("disabled", "disabled");
            }

        });

        function ShowEmployeePopu(Parent) {

            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");

            $("#SelectEmpLoyeeBythis").click();
        }



        function ShowEmployeePopu1(Parent) {

            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=HiddenField1&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 450, 200, "#SelectEmpLoyeeBythis");

            $("#SelectEmpLoyeeBythis").click();
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <a href="/AdminPanlWorkArea/QuotedPrice/QuotedManager/QuotedPriceflowerlist.aspx">返回</a>
    <asp:HiddenField ID="hideisChange" runat="server" ClientIDMode="Static" />
    <uc1:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th style="text-align: left;" colspan="5" id="<%=Guid.NewGuid().ToString() %>">

                            <input style="margin: 0" runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowEmployeePopu(this);" type="text" />
                            <a href="#" class="btn btn-mini btn-primary" onclick="ShowEmployeePopu(this);" class="SetState"><asp:Label ID="lblhejia" runat="server" Text="选择核价负责人"></asp:Label></a>
                            <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='' runat="server" />

                        </th>
                        <th style="text-align: left;" id="Th1234567">

                            <input style="margin: 0" runat="server" id="Text1" class="txtEmpLoyeeName"  onclick="ShowEmployeePopu1(this);" type="text" />
                            <a href="#" class="btn btn-mini btn-primary" onclick="ShowEmployeePopu1(this);" class="SetState"><asp:Label ID="Label1" runat="server" Text="选择采购负责人"></asp:Label></a>
                            <asp:HiddenField ID="HiddenField1" ClientIDMode="Static" Value='' runat="server" />
                        </th>

                    </tr>
                    <tr>
                        <th>产品</th>
                        <th>数量</th>
                        <th>详细说明</th>
                        <th>

                            <asp:Label ID="lblChengben" runat="server" Text="预计总成本"></asp:Label></th>
                        <th>对客户销售价单价</th>
                        <th>保存</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <th><%#Eval("ServiceContent") %></th>
                                <th><%#Eval("Quantity") %></th>
                                <th><%#Eval("Requirement") %></th>
                                <th>
                                    <asp:TextBox ID="txtPurchasePrice" Text='<%#Eval("PurchasePrice") %>' runat="server"></asp:TextBox></th>

                                <th>
                                    <asp:TextBox CssClass="Price" ID="txtSaleItem" Text='<%#Eval("UnitPrice") %>' runat="server"></asp:TextBox></th>
                                <th>
                                    <asp:Button ID="btnSaveRow" CommandName="btnSaveRow" CommandArgument='<%#Eval("ChangeID") %>' runat="server" Text="保存" /></th>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>

                </tbody>

                <tr>
                    <td colspan="6">
                        <asp:Button ID="btnSaveRow" runat="server" Text="保存全部" OnClick="btnSaveRow_Click" />
                        <asp:Button ID="btnCheck" runat="server" Text="确认审核" OnClick="btnCheck_Click" />
                    </td>
                </tr>

            </table>

        </div>
    </div>
</asp:Content>

