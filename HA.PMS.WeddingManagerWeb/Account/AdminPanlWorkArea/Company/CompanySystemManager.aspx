<%@ Page Title="" StylesheetTheme="Default" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CompanySystemManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.CompanySystemManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>

    <script type="text/javascript">

        //弹出窗体  修改
        function ShowWindows(KeyID, Control, Type) {
            var Url = "CompanySystemUpdate.aspx?SystemId=" + KeyID + "&Type=" + Type;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 380, 400, "a#" + $(Control).attr("id"));
        }

        //上传图片
        function ShowFileUploadPopu(SystemID, SystemTitle) {
            var Url = "/AdminPanlWorkArea/Company/CompanySystemUpLoad.aspx?SystemID=" + SystemID + "&SystemTitle=" + SystemTitle + "&FileType=1"; //1.公司制度 2.销售部制度  3.运营部制度
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        $(document).ready(function () {

            showPopuWindows($("#createKnow").attr("href"), 310, 180, "a#createKnow");

            showPopuWindows($("#BrowseKnow").attr("href"), 1000, 700, "a#BrowseKnow");

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>

    <asp:PlaceHolder ID="phContent" runat="server">
        <table runat="server" id="tblWebUrl" class="table table-bordered" style="width: 45%;">
            <tr>
                <td>编号:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtKeyID" Width="180px" Visible="false" /><asp:Label runat="server" ID="lblKeyID" /></td>
                <td>外网域名:</td>
                <td>
                    <asp:TextBox runat="server" ID="txtOutURL" Width="180px" Visible="false" />
                    <asp:Label runat="server" ID="lblOutUrl" /></td>
                <td>
                    <asp:Button runat="server" ID="Button1" Text="修改" OnClick="btnSave_Click" CssClass="btn btn-primary btn-mini" />
                </td>

            </tr>
            <tr>
                <td colspan="5">
                    <label class="lblRemark" style="color: red; cursor: default;">提示：(不需要加http://)</label>
                </td>
            </tr>
        </table>
        <p></p>
        <a class="btn btn-danger" runat="server" clientidmode="Static" href="CompanySystemCreate.aspx?FileType=1" id="createKnow">新建制度</a>&nbsp;&nbsp;
        <asp:LinkButton runat="server" ID="lbtnRefresh" Text="刷新" CssClass="btn btn-primary" OnClick="lbtnRefresh_Click" />

        <div class="widget-box" style="width: 95%">

            <div class="widget-content">
                <table class="table table-bordered" style="width: 60%;">
                    <thead>
                        <tr>
                            <th>制度名称</th>
                            <th>上传时间</th>
                            <th>上传用户</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptKnow" runat="server" OnItemDataBound="rptKnow_ItemDataBound" OnItemCommand="rptKnow_ItemCommand">

                            <ItemTemplate>

                                <tr skey='<%#Eval("SystemId") %>'>
                                    <td>
                                        <asp:Label runat="server" ID="lblSystemTitle" Text='<%#Eval("SystemTitle") %>' Style="font-size: 14px; color: #1b62c6;" />
                                        <asp:HiddenField runat="server" ID="HiddenSystemId" Value='<%#Eval("SystemId") %>' />
                                    </td>
                                    <td><%#Eval("CreateDate") %></td>
                                    <td><%#GetEmployeeName(Eval("CreateEmployee")) %></td>
                                    <td <%#GetJurisdiction() %>>
                                        <a <%=IsHideButton() %> href="#" class="btn btn-primary btn-mini" onclick="ShowFileUploadPopu('<%#Eval("SystemId") %>','<%#Eval("SystemTitle") %>')" <%#GetJurisdiction() %>>上传</a>
                                        <a <%=IsHideButton() %> href="#" class="btn btn-primary btn-mini" onclick="ShowWindows(<%#Eval("SystemId") %>,this,'ParentId')" <%#GetJurisdiction() %>>修改</a>
                                        <asp:LinkButton CssClass="btn btn-danger btn-mini" CommandName="Delete" CommandArgument='<%#Eval("SystemId") %>' ID="lkbtnDelete" runat="server" OnClientClick="return confirm('你确定要删除吗?');">删除</asp:LinkButton>
                                    </td>
                                </tr>
                                <asp:Repeater runat="server" ID="repSystemDetails" OnItemDataBound="repSystemDetails_ItemDataBound" OnItemCommand="repSystemDetails_ItemCommand">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="HideSystemID" Value='<%#Eval("SystemID") %>' />
                                        <tr>
                                            <td><%#GetItemNbsp() %><%#Eval("SystemTitle") %></td>
                                            <td><%#Eval("CreateDate") %></td>
                                            <td><%#GetEmployeeName(Eval("CreateEmployee")) %></td>
                                            <td>
                                                <asp:LinkButton runat="server" ID="lbtnDownLoad" Text="下载" CommandName="DownLoad" CommandArgument='<%#Eval("SystemId") %>' CssClass="btn btn-primary btn-mini" />
                                                <%--<asp:LinkButton runat="server" ID="lbtnLook" Text="查看" CssClass="btn btn-primary btn-mini" CommandArgument='<%#Eval("SystemId") %>' CommandName="Look" />--%>
                                                <a <%=IsHideButton() %> href="#" class="btn btn-primary btn-mini" onclick="ShowWindows(<%#Eval("SystemId") %>,this,'ChildId')" <%#GetJurisdiction() %>>修改</a>
                                                <a target="_blank" href='<%#GetUrl(Eval("SystemID")) %>' class="btn btn-primary btn-mini">查看</a>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" Text="删除" CommandName="Delete" CommandArgument='<%#Eval("SystemId") %>' CssClass="btn btn-danger btn-mini" OnClientClick="return confirm('您确定要删除吗?');" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>

                    </tbody>
                </table>
            </div>
        </div>

    </asp:PlaceHolder>
</asp:Content>
