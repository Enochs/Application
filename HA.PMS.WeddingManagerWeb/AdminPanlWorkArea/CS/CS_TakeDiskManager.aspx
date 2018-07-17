<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CS_TakeDiskManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_TakeDiskManager" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 20px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }
    </style>


    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "CS_TakeDiskUpdate.aspx?TakeID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 800, 1800, "a#" + $(Control).attr("id"));
        }

        function validateText() {
            alert("请你填写完成该行的信息");
        }
        var currentHref = "";
        $(document).ready(function () {

            showPopuWindows($("#createTakeDisk").attr("href"), 500, 800, "a#createTakeDisk");
            //$("#<//%=txtStart.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            //$("#<//%=txtEnd.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });


            $("#trContent th").css({ "white-space": "nowrap" });

            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {


                if (indexs != 3) {
                    $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");
                }
                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });



            $(":text").each(function (indexs, values) {
                $(this).addClass("centerTxt");
            });
            $("select").addClass("centerSelect");

            $("html").css({ "overflow-x": "hidden" });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" style="height: auto;">

    <%--<a href="CS_TakeDiskCreate.aspx" class="btn btn-primary  btn-mini" id="createTakeDisk">创建取件</a>--%>
    <div class="widget-box">


        <div class="widget-box" style="height: 30px; border: 0px;">

            <table class="queryTable">
                <tr>
                    <td>
                        <table>
                            <tr>

                                <td>新人姓名:<asp:TextBox ID="txtGroom" runat="server" MaxLength="10"></asp:TextBox>
                                </td>
                                <td>电话:<asp:TextBox ID="txtGroomCellPhone" runat="server" MaxLength="20"></asp:TextBox>
                                </td>
                                <td>婚期:<asp:TextBox ID="txtStart" onclick="WdatePicker();" runat="server"></asp:TextBox>
                                </td>
                                <td>至<asp:TextBox ID="txtEnd" onclick="WdatePicker();" runat="server"></asp:TextBox>
                                </td>
                                <td><HA:MyManager runat="server" ID="MyManager" Title="策划师" /></td>
                               
                                <td>
                                    <asp:Button ID="btnQuery" CssClass="btn btn-primary" Height="27" runat="server" Text="查询" OnClick="btnQuery_Click" />
                                    <cc2:btnReload ID="btnReload2" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </div>

        <table class="table table-bordered table-striped">
            <thead>
                <tr>
                    <th>新人姓名</th>
                    <th>电话</th>
                    <th>婚期</th>
                    <th>酒店</th>
                    <th>婚礼顾问</th>
                    <th>策划师</th>
                    <th>收件时间</th>
                    <th>收件说明</th>
                    <th>责任单位</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody id="tbContent">
                <asp:Repeater ID="rptTalkeDisk" runat="server" OnItemDataBound="rptTalkeDisk_ItemDataBound" OnItemCommand="rptTalkeDisk_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><a href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>&OnlyView=1" target="_blank"><%#ShowCstmName(Eval("Bride"),Eval("Groom")) %></a></td>
                            <td><%#Eval("BrideCellPhone") %></td>
                            <td><%#ShowPartyDate( Eval("PartyDate")) %></td>
                            <td><%#Eval("Wineshop") %></td>
                            <td><%#GetOrderEmpLoyeeName(GetOrderIdByCustomerID(Eval("CustomerID"))) %></td>
                            <td><%#GetQuotedEmpLoyeeName(GetOrderIdByCustomerID(Eval("CustomerID"))) %></td>
                            <td>
                                <table>
                                    <tr>
                                        <td>照片：<cc2:DateEditTextBox ID="txtConsigneeTime" Width="85" onclick="WdatePicker();" runat="server" Text='<%#Eval("ConsigneeTime","{0:yyyy-MM-dd}") %>'></cc2:DateEditTextBox></td>
                                    </tr>
                                    <tr>
                                        <td>视频：<asp:TextBox ID="txtGetFileTime" Width="85" onclick="WdatePicker();" runat="server" Text='<%#Eval("GetFileTime","{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtConsigneeContent" Text='<%#Eval("ConsigneeContent") %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtGetFileContent" Text='<%#Eval("ConsigneeContent") %>'></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtConsigneeWork" Text='<%#GetGuardName(Eval("CustomerID"),3) == "" ? Eval("ConsigneeWork") : GetGuardName(Eval("CustomerID"),3) %>'></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtGetFileWork" Text='<%#GetGuardName(Eval("CustomerID"),2) == "" ? Eval("GetFileWork") : GetGuardName(Eval("CustomerID"),2)  %>'></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                            <%--<td><asp:TextBox ID="txtGetFileTime" Width="85" onclick="WdatePicker();" runat="server"></asp:TextBox></td>--%>

                            <td>
                                <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Edit" CommandArgument='<%#Eval("TakeID") %>' CssClass="btn btn-primary  btn-mini">保存</asp:LinkButton>
                                <%--<asp:LinkButton ID="lbtnDetails" runat="server" CommandName="LookDetails" CommandArgument='<%#Eval("CustomerID") %>' CssClass="btn btn-primary  btn-mini">执行明细</asp:LinkButton>--%>

                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <cc1:AspNetPagerTool ID="TalkeDiskPager" OnPageChanged="TalkeDiskPager_PageChanged" PageSize="7" runat="server"></cc1:AspNetPagerTool>
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />

    </div>

</asp:Content>
