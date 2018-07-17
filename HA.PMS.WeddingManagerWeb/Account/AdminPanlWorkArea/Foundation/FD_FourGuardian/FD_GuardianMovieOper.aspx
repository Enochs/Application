<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_GuardianMovieOper.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian.FD_GuardianMovieOper" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {

            showPopuWindows($("#createMovie").attr("href"), 600, 300, "a#createMovie");

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">

        <div class="widget-content">
              <span style="color:green;">提示：<br />1.选取图片的来源，需要把对应的电影存放在 &nbsp;软件安装\Files\GuardianMovie\ &nbsp;<br />2.该存放电影的地址里面可以存放多个文件夹，程序将自动读取视频文件<br />3.视频格式为 mov 格式</span><br />
            <a id="createMovie" href="FD_GuardianFilesChoose.aspx?GuardianId=<%=ViewState["GuardianId"] %>&GuradianFileType=1" class="btn btn-primary  btn-mini">添加视频
            </a>
            <table class="table table-bordered table-striped" style="width:600px;">
                <thead>
                    <tr>

                        <th>视频名称</th>
                        <th>视频(封面图)</th>

                        <%--  <th>风格说明</th>--%>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptMovie" OnItemCommand="rptMovie_ItemCommand" runat="server">

                        <ItemTemplate>

                            <tr>

                                <td><%#Eval("MovieName") %></td>
                                <td>
                                     <asp:Image ID="imgStore" ImageUrl='<%#Eval("MovieTopImagePath") %>' Width="100" Height="70" runat="server" /> 
                                   
                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("GuradinMovieID") %>' CssClass="btn btn-danger btn-mini" runat="server">删除</asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>

                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="MoviePager" AlwaysShow="true" OnPageChanged="MoviePager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>

</asp:Content>
