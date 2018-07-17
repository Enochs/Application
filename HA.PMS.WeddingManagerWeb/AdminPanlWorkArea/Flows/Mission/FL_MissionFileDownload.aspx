<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionFileDownload.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionFileDownload" Title="文件下载" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%--<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
            $("html,body").css({ "width": "500px", "height": "300px", "background-color": "transparent" });
        });
    </script>

    <div class="widget-box">
        <div class="widget-content ">

            <asp:Repeater ID="repMissionResualt" runat="server" OnItemCommand="repMissionResualt_ItemCommand">
                <HeaderTemplate>
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th style="white-space: nowrap; width: 33%;">文件名称</th>
                                <th style="white-space: nowrap; width: 33%;">上传时间</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td style="white-space: nowrap;"><%#Eval("FileName") %></td>
                        <td><%#Eval("CreateDate") %></td>
                        <td>
                            <%--<a href="<%#Eval("FileAddress") %>" class="btn btn-primary btn-mini">查看</a>--%>
                            <asp:LinkButton runat="server" ID="lbtnDownLoad" Text="下载" CssClass="btn btn-primary btn-mini" CommandName="DownLoad" CommandArgument='<%#Eval("FileID") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

        </div>
    </div>
</asp:Content>
