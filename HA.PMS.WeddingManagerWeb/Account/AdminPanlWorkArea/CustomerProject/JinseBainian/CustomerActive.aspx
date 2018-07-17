<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CustomerActive.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CustomerProject.JinseBainian.CustomerActive" %>

<%@ Register Src="../../Control/DateRanger.ascx" TagName="DateRanger" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">



        $('div').dblclick(function () {

            clearTimeout(TimeFn);

        });
        function ShowReport(Control, SourceItem) {
            var DateTime = $("#" + Control.id).parent().parent().find("td").eq(0).text();
            var PackageID = $("#" + Control.id).parent().find("#ClickDiv1").attr("class");

            var URL = "/AdminPanlWorkArea/CustomerProject/JinseBainian/CustomerActiveCreateUpdate.aspx?PackageID=" + PackageID + "&DateTime=" + DateTime + "&SourceItem=" + SourceItem;

            ShowPopuWindow(URL, 700, 700);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table table-bordered table-striped with-check table-select" style="width: 1300px;">
        <tr>
            <td style="white-space: nowrap;">时间</td>

            <td>
                <uc1:DateRanger ID="DateRanger1" runat="server" />
            </td>


            <td>
                <asp:Button ID="btnSerch" ClientIDMode="Static" runat="server" Text="查询" OnClick="btnSerch_Click" />
            </td>
        </tr>
    </table>
    <div style="width: 1100px; overflow-x: auto;">
        <table class="table table-bordered table-striped with-check table-select" style="width: 100%;">
            <tr>
                <td style="white-space: nowrap;">时间/套餐</td>

                <asp:Repeater ID="repDataList" runat="server">
                    <ItemTemplate>
                        <td style="width: 45px;">
                            <%#Eval("PackageTitle")%>
                        </td>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>

            <asp:Repeater ID="repDataTimeList" runat="server" OnItemDataBound="repDataTimeList_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td><%#Eval("Day") %></td>
                        <asp:Repeater ID="repColn" runat="server" OnItemDataBound="repColn_ItemDataBound">
                            <ItemTemplate>
                                <td style="text-align: center;">
                                    <div id="ClickDiv1" class="<%#Eval("PackageID") %>" style="display: none;"></div>
                                    <label style="color:red;">
                                    <br />
                                    可订:<%#Eval("PackageSum") %><br />已订:<%#Eval("PackageSum") %><br />暂定:<%#Eval("PackageSum") %><br /></label><asp:HiddenField ID="hideDay" runat="server" Value='<%#DataBinder.Eval((Container.NamingContainer.NamingContainer as RepeaterItem).DataItem, "Day") %>' />
                                    <asp:Label ID="lblItem1" Font-Bold="true" runat="server" Width="30" Height="30" BackColor="Snow" Text="午宴" onclick="ShowReport(this,'午宴');"></asp:Label>
                                 
                                    <asp:Label ID="lblItem2" Font-Bold="true" runat="server" Width="30" Height="30" BackColor="Snow" Text="晚宴" onclick="ShowReport(this,'晚宴');"></asp:Label>

                                </td>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tr>

                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>
