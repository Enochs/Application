<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="WorkCheckGuide.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.GeneralManagerPodium.WorkCheckGuide" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforEmpLoyee.ascx" TagPrefix="HA" TagName="MessageBoardforEmpLoyee" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .link-m1 ul li {
            float: left;
            margin: 2px 5px;
            list-style: none;
            display: block;
        }
    </style>

    <script type="text/javascript">

        $("img").error(function () {
            $(this).attr("src", "/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/defaultImage.jpg");
        });
        //留言板
        function MessageBorde(EmpLoyeeID) {
            var URI = "/AdminPanlWorkArea/ControlPage/MessageBoardPage.aspx?EmpLoyeeID=" + EmpLoyeeID;
            showPopuWindows(URI, 600, 600, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        $(document).ready(function () {
            $("a#serach").fancybox({
                "width": 350,
                "height": 170,
                'autoScale': true,

                'autoDimensions': true,
                "centerOnScroll": true,
                "changeFade": "fast",
                'transitionIn': 'none',
                'transitionOut': 'none',
                'topRatio': 0,
                'type': 'iframe',
                'onClosed': function () {


                }
            });
            $(".queryTable td").css({ "border": "0", "vertical-align": "top" });
            $("html,body").css({ "background-color": "transparent" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box" style="overflow-y: auto; height: 900px;">
        <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>

            <span class="label label-info"></span>
        </div>
        <div class="widget-content">
            <table class="queryTable">
                <tr>
                    <td>
                        <div style="width: 10px; height: 10px; border: 1px solid #000; background-color: #f00;">
                        </div>
                    </td>
                    <td>为有报警事项的员工</td>
                    <td>
                        <div style="width: 10px; height: 10px; border: 1px solid #000; background-color: #00ff21;">
                        </div>
                    </td>
                    <td>为工作状态正常的员工</td>
                    <td>点击员工头像可以查看他的异常工作列表</td>
                </tr>
            </table>
            <br />

            <span style="color: white; background-color: #32b61d;">直接下属：</span>
            <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="6" BorderStyle="Solid" Width="100%">
                <ItemTemplate>
                    <div style="vertical-align: bottom;">
                        <br />
                        <%#GetURL(Eval("EmployeeID")) %>
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <br />


            <span style="color: white; background-color: #0094ff;">间接下属：</span>
            <cc1:DepartmentDropdownList ID="DepartmentDropdownList1" Width="120" runat="server">
            </cc1:DepartmentDropdownList>
            <asp:Button ID="btnSerch" runat="server" CssClass="btn btn-success" Text="查询" OnClick="btnSerch_Click" />
            &nbsp;<asp:DataList ID="DataList2" runat="server" RepeatDirection="Horizontal" RepeatColumns="6" BorderStyle="Solid" Width="100%">
                <ItemTemplate>
                    <div>
                        <br />
                        <%#GetSecondURL(Eval("EmployeeID")) %>
                    </div>
                </ItemTemplate>
            </asp:DataList>

        </div>
        <HA:MessageBoardforEmpLoyee runat="server" ID="MessageBoardforEmpLoyee" />
    </div>



</asp:Content>
