<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_SaleSourcesCheckComputation.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SaleSourcesCheckComputation" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        function ShowWindowsUpdate(KeyID, Control) {
            var Url = "FD_PayNeedRabateUpdate.aspx?id=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            //$("#txtPayDate").datepicker({ dateFormat: 'yy-mm-dd ' });

            $(".NeedEdit").hide();
            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {

                $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");

                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });
            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");

            //
            $("html").css({ "overflow-x": "hidden" });

        });

        function DoingEdit() {
            $(".NoEdit").hide();
            $(".NeedEdit").show();
            return false;
        }

        function RowEdit(Control) {
            //alert($(Control).parent().parent().find(".NoEdit").length);
            ////alert($(Control).parent().parent().html());
            $(Control).parent().parent().find(".NoEdit").hide();
            $(Control).parent().parent().find(".NeedEdit").show();
            $(Control).hide();
            $(Control).parent().find(".SaveItem").show();
            return false;
        }

        function Checkfinish() {
            return confirm("确认后将无法再次进行编辑，是否继续？");
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <table>
                <tr>

                    <td>渠道类型: 
                                        <cc2:ddlChannelType ID="ddlChannelType" AutoPostBack="true" OnSelectedIndexChanged="ddlChannelType_SelectedIndexChanged" runat="server"></cc2:ddlChannelType>
                    </td>
                    <td>渠道名称：
                                        <cc2:ddlChannelName ID="ddlChanne" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlChanne_SelectedIndexChanged"></cc2:ddlChannelName>
                    </td>
                    <td>推荐人:
                                         <cc2:ddlReferee ID="ddlSerchReferee" Width="70" runat="server"></cc2:ddlReferee>
                    </td>
                    <td>新人状态:<cc2:ddlCustomersState ID="ddlCustomersState" runat="server"></cc2:ddlCustomersState></td>
                    <td>&nbsp;</td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" Title="录入人" />
                    </td>
                    <%--<td>
                        新人姓名
                        <asp:TextBox runat="server" ID="txtCustomerName" />
                    </td>--%>
                     <td><asp:Button ID="btnSerch" runat="server" Height="27" Text="查询" CssClass="btn btn-primary" OnClick="btnSerch_Click" /></td>
                </tr>
         <%--       <tr>
                    <td>联系电话<asp:TextBox runat="server" ID="txtContactPhone" />
                    </td>
                    <td  colspan="2"><HA:DateRanger runat="server" ID="DateRanger" Title="婚期" />
                    </td>
                   

                </tr>--%>

            </table>
            <table class="table table-bordered table-striped table-select">
                <thead>
                    <tr>
                        <th style="white-space: nowrap;">渠道名称</th>
                        <th style="white-space: nowrap;">新人姓名</th>
                        <th style="white-space: nowrap;">婚期</th>
                        <th style="white-space: nowrap;">支付规则</th>
                        <th style="white-space: nowrap;">订单金额</th>
                        <th style="white-space: nowrap;">收款人</th>
                        <th style="white-space: nowrap;">支付金额</th>
                        <th style="white-space: nowrap;">支付日期</th>
                        <th style="white-space: nowrap;">经办人</th>
                        <th style="white-space: nowrap;">操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptTelemarketingManager" runat="server" OnItemDataBound="rptTelemarketingManager_ItemDataBound" OnItemCommand="rptTelemarketingManager_ItemCommand">
                        <ItemTemplate>
                            <tr skey='FD_SaleSourcesID<%#Eval("ID") %>' style="height: 14px;">
                                <td>
                                    <asp:HiddenField ID="hiddKeyValue" runat="server" Value='<%#Eval("ID") %>' />
                                    <asp:HiddenField ID="hiddSourceKey" runat="server" Value='<%#Eval("SourceID") %>' />
                                    <%#GetChannelHref(GetSourceName(Eval("SourceID"))) %>
                                </td>
                                <td><%#GetGoomByID(Eval("CustomerID")) %></td>
                                <td><%#ShowPartyDate(Eval("PartyDay")) %></td>
                                <td><span style="cursor: default" title='<%#Eval("Paypolicy") %>'><%#ToInLine(Eval("Paypolicy")) %></span></td>
                                <td><%#GetMoney(Eval("CustomerID")) %></td>
                                <td>
                                    <cc2:ddlReferee ID="DdlReferee1" Style="margin: 0" Width="80" runat="server"></cc2:ddlReferee></td>
                                <td>
                                    <label class="NoEdit"><%#Eval("PayMoney") %></label>
                                    <asp:TextBox ID="txtPayMoney" MaxLength="8" Style="display: none; margin: 0;" ToolTip="金额只能是整数" runat="server" CssClass="NeedEdit" Width="55" Text='<%#Eval("PayMoney") %>'></asp:TextBox></td>
                                <td>
                                    <label class="NoEdit"><%#GetDateStr(Eval("PayDate")) %></label>
                                    <cc2:DateEditTextBox Style="margin: 0; display: none;" onclick="WdatePicker();" ID="txtPayDate" MaxLength="20" runat="server" CssClass="NeedEdit DataTextEditoer" Width="55" Text='<%#Eval("PayDate") %>'></cc2:DateEditTextBox></td>
                                <td>
                                    <label class="NoEdit"><%#GetoperEmpLoyee(Eval("OperEmployee")) %></label>
                                    <asp:TextBox ID="txtOperEmployee" Style="margin: 0; display: none;" MaxLength="15" runat="server" CssClass="NeedEdit" Width="55" Text='<%#GetoperEmpLoyee(Eval("OperEmployee")) %>'></asp:TextBox></td>
                                <td style="width: 90px; text-align: center">
                                    <asp:Button Style="display: none;" ID="btnSave" CommandArgument='<%#Eval("ID") %>' CommandName="Save" runat="server" Text="保存" CssClass="btn btn-primary btn-mini SaveItem" />
                                    <input id="Button1" type="button" value="编辑" <%#Eval("IsFinish").ToString()=="True"?"style='display:none;'":"" %> onclick="return RowEdit(this);" class="btn btn-success btn-mini" />
                                    <asp:Button Style="display: none;" ID="btnFinish" CommandArgument='<%#Eval("ID") %>' CommandName="Finish" runat="server" Text="确认" CssClass="btn btn-danger btn-mini SaveItem" OnClientClick="return Checkfinish();" />
                                </td>
                                <%--                     <td>
                                <asp:LinkButton ID="lnkbtnDelete" runat="server" OnClientClick="return DoingEdit(this);" CommandArgument='<%#Eval("Id") %>' CommandName="Delete">删除</asp:LinkButton>
                            </td>--%>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="10">
                            <cc1:AspNetPagerTool ID="CheckComputationPager" AlwaysShow="true" OnPageChanged="CheckComputationPager_PageChanged" runat="server" PageSize="10"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <table style="display: none;">
                <tr>
                    <td>
                        <asp:Button ID="btnSaveAll" runat="server" Text="保存" OnClick="btnSaveAll_Click" CssClass="btn btn-primary" />
                        <asp:Button ID="btnEditall" runat="server" OnClientClick="return DoingEdit();" Text="编辑" CssClass="btn btn-success" />
                        <asp:Button ID="btnFinishall" runat="server" Text="确认" OnClick="btnFinishall_Click" CssClass="btn btn-danger" OnClientClick="return Checkfinish();" /></td>
                </tr>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
