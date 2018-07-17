<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_TheCaseFileManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases.FD_TheCaseFileManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowOperImg( Control) { 
            var KeyID=<%=ViewState["CaseID"] %>;
            var Url = "/AdminPanlWorkArea/Foundation/FD_Hotel/FD_HotelImageLoadFile.aspx?CaseID=" + KeyID + "&toOperPage=" + <%=ViewState["load"]%>;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 800, 300, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            $("a.grouped_elements").fancybox();
            $("html,body").css({"background-color": "transparent" });
        });
        function ShowChooseWindows( Control) {
            var KeyID=<%=ViewState["CaseID"] %>;
            var Url = "FD_TheCaseMovieChoose.aspx?CaseID=" + KeyID;
            $(Control).attr("id", "chooseShow" + KeyID);
            showPopuWindows(Url, 600, 300, "a#" + $(Control).attr("id"));
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="FD_TheCaseManager.aspx" class="btn btn-primary  btn-mini"><< 返回案例管理</a>
    <div class="widget-box">
        <asp:PlaceHolder ID="phImg" runat="server">
            <a href="#" onclick="ShowOperImg(this);" class="btn btn-primary  btn-mini">上传图片</a>
            <asp:Button ID="btnReflush" Text="刷新" OnClick="btnReflush_Click" CssClass="btn btn-primary btn-mini" runat="server" />
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phMovie" runat="server">
            <span style="color: green;">友情提示：
                <br />1.选取视频的来源，需要把对应的文件存放在：软件安装\Files\TheCase\TheCaseMovie\
                <br />2.该存放电影的地址里面可以存放多个文件夹，程序将自动读取视频文件<br />
            </span>
            <a href="#" onclick="ShowChooseWindows(this);" class="btn btn-primary  btn-mini">选择视频</a>
        </asp:PlaceHolder>
        <div class="widget-content">
            <table class="table table-bordered table-striped" style="width: 500px;">
                <thead><tr><td>文件名</td><td>操作</td></tr></thead>
                <tbody>
                    <asp:Repeater ID="rptTheCaseFile" OnItemCommand="rptTheCaseFile_ItemCommand" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("CaseFileName") %>
                                    <div style='<%#Eval("FileType").ToString() == "2" ? "": "display:none;"%>'>
                                        <a class="grouped_elements" href='<%#(Eval("CaseFilePath")+string.Empty).ToString().Replace("~",string.Empty) %>' rel="group1">
                                            <img width="120" height="70" src='<%#(Eval("CaseFilePath")+string.Empty).ToString().Replace("~",string.Empty)%>' alt="" />
                                    </div>
                                    </a>
                                </td>
                                <td>
                                    <asp:LinkButton CssClass="btn btn-primary btn-mini" ID="lkbtnSetFace" style='<%#Eval("FileType").ToString() == "2" ? "": "display:none;"%>' CommandName="Set" CommandArgument='<%#Eval("CaseFileId") %>' runat="server">设为封面</asp:LinkButton>&nbsp;
                                    <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lkbtnDelete" CommandName="Delete" OnClientClick="return confirm('是否真的要删除该项？');" CommandArgument='<%#Eval("CaseFileId") %>' runat="server">删除</asp:LinkButton>&nbsp;
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td colspan="2">
                            <cc1:AspNetPagerTool ID="TheCaseFilePager" PageSize="5" AlwaysShow="true" OnPageChanged="TheCaseFilePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
                        </td>
                    </tr>
                </tfoot>
            </table>
        </div>
    </div>
</asp:Content>
