<%@ Page Title="" Language="C#" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CreateSchedule.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.CreateSchedule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
    <%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
    <%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

    <script type="text/javascript">
        //指定四大金刚
        function ChangeFourGuardian(Parent, CallBack) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectFourGuardian.aspx?Callback=" + CallBack + "&ALL=true&ControlKey=hideGuardianID&SetEmployeeName=txtGuardianName&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        //改派四大金刚
        function ChangeFourGuardians(Parent, CallBack) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectFourGuardian.aspx?Callback=" + CallBack + "&ALL=true&ControlKey=HideGuardID&SetEmployeeName=txtGuardianNames&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
    <style type="text/css">
        .table_list tr th {
            text-align: left;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
    <div class="div ui-menu-divider">
        <div runat="server" id="div_insert">
            <table class="table table-bordered" style="width: 98%;">
                <tr>
                    <td>四大金刚:</td>
                    <td style="text-align: left;" id="<%=Guid.NewGuid().ToString()+"1" %>">
                        <lable style="display: none;">
                            <asp:Button ID="btnFourGuardianSave" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="四大金刚保存" CssClass="btn btn-success" OnClick="btnFourGuardianSave_Click" />
                        </lable>
                        <asp:HiddenField ID="hideGuardianID" runat="server" ClientIDMode="Static" />

                        <asp:TextBox runat="server" ID="txtGuardianName" ReadOnly="true" />
                        <input id="btnSetjingang" class="btn btn-primary" type="button" value="指定四大金刚" onclick="ChangeFourGuardian(this, 'btnFourGuardianSave')" />
                    </td>
                    <td>类型:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtGuardianType" /></td>
                    <td>价格:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPrice" /></td>
                    <td>订金:</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtPayMents" /></td>
                    <td>备注</td>
                    <td>
                        <asp:TextBox runat="server" ID="txtReamrk" TextMode="MultiLine" /></td>
                    <td>
                        <asp:Button runat="server" ID="btnConfirm" Text="添加" CssClass="btn btn-primary" OnClick="btnConfirm_Click" /></td>
                </tr>
            </table>
        </div>
        <p></p>

        <lable style="display: none;"><asp:Button ID="btnChangeFourGuardianSave" CommandName="SaveItem" ClientIDMode="Static" runat="server" Text="四大金刚保存" CssClass="btn btn-success" OnClick="btnChangeFourGuardianSave_Click" /></lable>


        <table class="table table-bordered table_list" style="width: 98%;">
            <thead>
                <tr>
                    <th style="width: 15%;">四大金刚</th>
                    <th style="width: 10%;">类型</th>
                    <th style="width: 10%;">价格</th>
                    <th style="width: 10%;">订金</th>
                    <th style="width: 10%;">预定时间</th>
                    <th style="width: 20%;">备注</th>
                    <th style="width: 10%;">操作</th>
                </tr>
            </thead>
            <asp:Repeater runat="server" ID="rptScheduleList" OnItemDataBound="rptScheduleList_ItemDataBound" OnItemCommand="rptScheduleList_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td id="<%=Guid.NewGuid().ToString() %>">
                            <asp:HiddenField runat="server" ID="HideGuardID" ClientIDMode="Static" />
                            <asp:TextBox runat="server" ID="txtGuardianNames" Enabled="false" Style="width: 70px;" />
                            <input id="btnSetjingang" class="btn btn-primary btn-mini" type="button" value="改派" onclick="ChangeFourGuardians(this, 'btnChangeFourGuardianSave')" />

                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblGuardianType" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblGuardianPrice" Text='<%#Eval("ScheGuardianPrice") %>' /></td>
                        <td>
                            <asp:TextBox runat="server" ID="txtPayMent" Text='<%#Eval("SchePayMent") %>' /></td>
                        <td><%#Eval("ScheCreateDate") %></td>
                        <td>
                            <asp:Label runat="server" ID="lblRemark" Text='<%#Eval("ScheReamrk") %>' Style="width: 150px" /></td>
                        <td>
                            <asp:LinkButton runat="server" ID="btnSave" Text="保存" CssClass="btn btn-primary btn-mini" CommandName="Save" CommandArgument='<%#Eval("ScheID") %>' />
                            <asp:LinkButton runat="server" ID="btnDel" Text="删除" CssClass="btn btn-primary btn-mini" CommandName="Del" CommandArgument='<%#Eval("ScheID") %>' OnClientClick="return confirm('您确定要删除吗?');" />
                            <asp:HiddenField runat="server" ID="HideScheID" Value='<%#Eval("ScheID") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>
