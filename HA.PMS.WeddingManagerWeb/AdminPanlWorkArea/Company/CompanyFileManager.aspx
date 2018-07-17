<%@ Page Title="" Language="C#" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CompanyFileManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.CompanyFileManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowWindows(KeyID, Control, Type, FileID) {
            var Url = "CompanyFileCreate.aspx?Type=" + Type + "&FileID=" + FileID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 250, 110, "a#" + $(Control).attr("id"));
        }
        //上传图片
        function ShowFileUploadPopu(FileID, FileName) {
            var Url = "/AdminPanlWorkArea/Company/CompanyFileUpLoad.aspx?FileID=" + FileID + "&FileName=" + FileName;
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

    </script>

    <style type="text/css">
        #lbntFilename:hover {
            text-decoration: underline;
            color: #cf5f41;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <asp:PlaceHolder ID="phContent" runat="server">
        <table>
            <tr>
                <td id="<%=Guid.NewGuid().ToString() %>">
                    <a href="#" runat="server" id="CreateSystem" onclick='ShowWindows(0,this,"Add",0);' class="SetState btn btn-danger">新建资料</a>&nbsp;&nbsp;&nbsp;
                    <asp:Button Visible="false" runat="server" ID="btnClickMe" Text="ClickMe" CssClass="btn btn-primary btn-mini" OnClick="btnClickMe_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton runat="server" ID="lbtnRefresh" Text="刷新" CssClass="btn btn-primary" OnClick="lbtnRefresh_Click" />
                </td>
            </tr>
        </table>
        <div class="widget-box" style="width: 95%; height: 900px; overflow: auto;">
            <div class="widget-content">
                <table class="table table-bordered" style="width: auto;">
                    <thead>
                        <tr>
                            <th>文件名称</th>
                            <th>上传时间</th>
                            <th>上传用户</th>
                            <th>操作</th>
                        </tr>
                        <asp:Repeater runat="server" ID="repCompanyFile" OnItemDataBound="repCompanyFile_ItemDataBound" OnItemCommand="repCompanyFile_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:LinkButton runat="server" ID="lbntFilename" ClientIDMode="Static" Text='<%#Eval("FileName") %>' Style="font-size: 14px; color: #1b62c6;" />
                                        <asp:HiddenField runat="server" ID="HiddentFileId" Value='<%#Eval("FileID") %>' />
                                    </td>
                                    <td><%#Eval("CreateDate") %></td>
                                    <td><%#GetEmployeeName(Eval("CreateEmployee")) %></td>
                                    <td>
                                        <a <%=IsHideButton() %> href="#" class="btn btn-primary btn-mini" onclick="ShowFileUploadPopu('<%#Eval("FileID") %>','<%#Eval("FileName") %>')" <%#GetJurisdiction() %>>上传</a>
                                        <a <%=IsHideButton() %> href="#" class="btn btn-primary btn-mini" onclick="ShowWindows(0,this,'Modify','<%#Eval("FileID") %>');" <%#GetJurisdiction() %>>修改</a>
                                        <asp:LinkButton runat="server" ID="lbtnDelete" Text="删除" CommandName="Delete" CommandArgument='<%#Eval("FileID") %>' CssClass="btn btn-danger btn-mini" OnClientClick="return confirm('你确定要删除吗?')" />

                                    </td>
                                </tr>
                                <asp:Repeater runat="server" ID="repFileDetails" OnItemDataBound="repFileDetails_ItemDataBound" OnItemCommand="repFileDetails_ItemCommand">
                                    <ItemTemplate>
                                        <tr>
                                            <td><%#GetItemNbsp(Eval("ItemLevel")) %> <%#Eval("FileName") %></td>
                                            <td><%#Eval("CreateDate") %></td>
                                            <td><%#GetEmployeeName(Eval("CreateEmployee")) %></td>
                                            <td>
                                                <asp:LinkButton runat="server" ID="lbtnDownLoad" Text="下载" CommandName="DownLoad" CommandArgument='<%#Eval("FileID") %>' CssClass="btn btn-primary btn-mini" />
                                                <%--<a target="_blank" href="../../GenerationFiles/<%#Eval("FileName").ToString().Replace("doc","html").Replace("xls","html").Replace("htmlx","html") %>" class="btn btn-primary btn-mini">查看</a>--%>
                                                <%--<asp:LinkButton runat="server" ID="lbtnLook" Text="查看" CssClass="btn btn-primary btn-mini" CommandArgument='<%#Eval("FileID") %>' CommandName="Look" />--%>
                                                <a <%=IsHideButton() %> href="#" class="btn btn-primary btn-mini" onclick="ShowWindows(0,this,'ModifyChild','<%#Eval("FileID") %>');" <%#GetJurisdiction() %>>修改</a>
                                                <a target="_blank" href='<%#GetUrl(Eval("FileID")) %>' class="btn btn-primary btn-mini">查看</a>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" Text="删除" CommandName="Delete" CommandArgument='<%#Eval("FileID") %>' CssClass="btn btn-danger btn-mini" OnClientClick="return confirm('你确定要删除吗?')" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                    </thead>
                </table>
            </div>
        </div>
    </asp:PlaceHolder>
</asp:Content>
