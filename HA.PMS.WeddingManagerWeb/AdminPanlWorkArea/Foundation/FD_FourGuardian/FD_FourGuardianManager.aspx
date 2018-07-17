<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_FourGuardianManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_FourGuardian.FD_FourGuardianManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>

    <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 25px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }

        .table thead tr th {
            text-align: left;
        }
    </style>

    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control) {
            alert($("#HidePage").val());
            var Url = "FD_FourGuardianUpdate.aspx?GuardianId=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 500, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {
            $("#trContent th").css({ "white-space": "nowrap" });
            $(".queryTable").css("margin-left", "15px");//98    24
            $(".queryTable td").each(function (indexs, values) {

                $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");

                if (indexs == $(".queryTable td").length - 1) {
                    $(this).css("vertical-align", "top");
                }

            });
            $("select").addClass("centerSelect");

            //  showPopuWindows($("#createType").attr("href"), 700, 1000, "a#createType");
            //showPopuWindows($("#createGuardian").attr("href"), 500, 1000, "a#createGuardian");
            //   showPopuWindows($("#createLeven").attr("href"), 700, 1000, "a#createLeven");





        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a class="btn btn-primary  btn-mini" target="_blank" href="FD_FourGuardianCreate.aspx" id="createGuardian">添加人员</a>
    <asp:LinkButton class="btn btn-primary  btn-mini" runat="server" ID="lbtnRefresh" Text="刷新" OnClick="lbtnRefresh_Click"  />
    <div class="widget-box">
        <div class="widget-box" style="height: 30px; border: 0px;">
            <table class="queryTable">
                <tr>
                    <td>
                        <table>
                            <tr>

                                <td>人员类型:
                                    <asp:DropDownList ID="ddlGuardianType" runat="server"></asp:DropDownList></td>

                                <td>等级:<asp:DropDownList ID="ddlGuardianLeven" runat="server"></asp:DropDownList></td>
                                <td>姓名:<asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:Button ID="btnQuery" Height="27" runat="server" Text="查找" CssClass="btn btn-primary" OnClick="btnQuery_Click" /></td>

                            </tr>

                        </table>
                    </td>
                </tr>
            </table>

        </div>


        <table class="table table-bordered table-striped table-select">
            <thead>
                <tr id="trContent">

                    <th>姓名</th>
                    <th>类型</th>
                    <th>等级</th>
                    <th>合作时间</th>
                    <th style="display: none;">联系电话</th>
                    <th>合作价格</th>
                    <th>销售价格</th>
                    <th style="display: none;">邮箱</th>
                    <%--  <th>风格说明</th>--%>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptGuardian" OnItemCommand="rptGuardian_ItemCommand" runat="server">

                    <ItemTemplate>
                        <tr skey='<%#Eval("GuardianId") %>'>
                            <td><%#Eval("GuardianName") %></td>
                            <td><%#Eval("TypeName") %></td>
                            <td><%#Eval("LevenName") %></td>
                            <td><%#GetDateStr(Eval("StarTime")) %></td>
                            <td style="display: none;"><%#Eval("CellPhone") %></td>
                            <td><%#Eval("CooperationPrice") %></td>
                            <td><%#Eval("SalePrice") %></td>

                            <%--    <td><%#Eval("Skin") %></td>--%>
                            <td style="display: none;"><%#Eval("Email") %></td>
                            <td>
                                <a class="btn btn-primary  btn-mini" target="_blank" href='FD_FourGuardianUpdate.aspx?GuardianId=<%#Eval("GuardianId") %>'>修改</a>
                                <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lkbtnDelete" CommandArgument='<%#Eval("GuardianId") %>' CommandName="Delete" runat="server" OnClientClick="return confirm('你确定要删除该用户吗？');">删除</asp:LinkButton>

                                <a href='FD_GuardianImageOper.aspx?GuardianId=<%#Eval("GuardianId") %>&GuradianFileType=2' class="btn btn-info btn-mini">操作图片</a>
                                <a href='FD_GuardianMovieOper.aspx?GuardianId=<%#Eval("GuardianId") %>&GuradianFileType=1' class="btn btn-info btn-mini">操作视频</a>
     
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <cc1:AspNetPagerTool ID="GuardianPager" PageSize="10" AlwaysShow="true" OnPageChanged="GuardianPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        <asp:HiddenField runat="server" ID="HidePage" ClientIDMode="Static" Value="1" />
        <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
    </div>
</asp:Content>
