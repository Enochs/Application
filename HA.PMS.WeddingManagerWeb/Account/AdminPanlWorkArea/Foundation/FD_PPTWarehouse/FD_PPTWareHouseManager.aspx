<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_PPTWareHouseManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_PPTWarehouse.FD_PPTWareHouseManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

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
    </style>

    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FD_PPTWareHouseUpdate.aspx?PPTID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 600, 500, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {

            showPopuWindows($("#createPPTWareHouse").attr("href"), 600, 500, "a#createPPTWareHouse");
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
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <a href="FD_PPTWareHouseCreate.aspx" class="btn btn-primary  btn-mini" id="createPPTWareHouse">上传PPT上传</a>

    <br />


    <div class="widget-box">

        <div class="widget-box" style="height: 30px; border: 0px;">


            <table class="queryTable">
                <tr>
                    <td>
                        <table>
                            <tr>

                                <td>PPT模板风格: 
                                    <asp:DropDownList ID="ddlImageType" runat="server"></asp:DropDownList></td>


                                <td>
                                    <asp:Button ID="btnQuery" runat="server" CssClass="btn btn-primary" Text="查找" OnClick="btnQuery_Click" />
                                </td>
                            </tr>

                        </table>
                    </td>
                </tr>
            </table>

        </div>

        <table class="table table-bordered table-striped table-select">
            <thead>
                <tr>
                    <th>模版名称</th>
                    <th>模版风格</th>
                    <th>关键字</th>
                    <th>上传时间</th>
                    <th>上传人</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptPPTManager" runat="server" OnItemCommand="rptPPTManager_ItemCommand">

                    <ItemTemplate>
                        <tr skey='<%#Eval("PPTID") %>'>
                            <td><%#Eval("PPTTitle") %></td>
                            <td><%#Eval("TypeName") %></td>
                            <td><%#Eval("LoadLabel") %></td>
                            <td><%#Eval("LoadTime") %></td>
                            <td><%#Eval("EmployeeName") %></td>
                            <td>

                                <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("PPTID") %>,this);'>修改</a>
                                <asp:LinkButton CssClass="btn btn-danger btn-mini" ID="lkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("PPTID") %>' runat="server">删除</asp:LinkButton>
                            </td>
                        </tr>

                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <cc1:AspNetPagerTool ID="PPTPager" runat="server" AlwaysShow="true" OnPageChanged="PPTPager_PageChanged"></cc1:AspNetPagerTool>


    </div>
</asp:Content>
