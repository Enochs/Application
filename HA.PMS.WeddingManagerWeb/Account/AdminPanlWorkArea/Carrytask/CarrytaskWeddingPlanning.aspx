<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskWeddingPlanning.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWeddingPlanning" Title="婚礼统筹" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc3" %>
<%@ Register Src="../Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc1" %>
<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        $(document).ready(function () {
            $("a#inline").fancybox();
            if ($("#hideIsDis").val() == "1") {
                $("[type='text']").attr("disabled", "disabled");
                $("[type='button']").attr("disabled", "disabled");
                $(".Requirement").attr("disabled", "disabled");
                $(".btndelete").hide();
                $(".SelectSG").hide();
                $(".SelectPG").hide();
            }

            $(".SaveSubmit").click(function () {
                var ReturnValue = true;

                $(".NoEmpty").each(function () {

                    if ($(this).val() == "") {
                        alert("请完善填写婚礼统筹!");
                        $(this).focus();
                        return false;
                    }
                });


                if ($("#txtPlanFinishDate").val() == "") {
                    alert("请完善填写婚礼统筹!");
                    $("#txtPlanFinishDate").focus();
                    return false;
                }

                return ReturnValue;
            });


            if (window.screen.width >= 1280 && window.screen.width <= 1366) {
                $("#tblContent").css({ "width": "80%" });
            }
        });

        //查看附件
        function ShowFileShowPopu(PlanningID) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWeddingFilesList.aspx?PlanningID=" + PlanningID+"&OnlyView=1";
            showPopuWindows(Url, 700, 800, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //上传附件
        function ShowFileUploadPopu(PlanningID, Kind) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWeddingFileUpload.aspx?PlanningID=" + PlanningID + "&Kind=" + Kind + "&Typer=6";
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 700, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function DeleteData() {
            if (confirm("确认删除！")) {
                return true;
            } else {
                return false;
            }

        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <a href="#" id="SelectEmpLoyeeBythis"></a>
    <uc1:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />
    &nbsp;<asp:HiddenField ID="hideIsDis" ClientIDMode="Static" runat="server" />
    <asp:Button ID="BtnExport" runat="server" Text="导出到Excel" OnClick="BtnExport_Click" CssClass="btn btn-primary" />
    <table id="tblContent" style="text-align: center; width: 100%;" border="1" class="table table-bordered table-striped">
        <thead>
            <tr>
                <th>类别</th>
                <th>项目</th>
                <th>任务内容</th>
                <th>任务要求</th>
                <th>附件</th>
                <td>责任人</td>
                <th>备注</th>
                <th>计划完成时间</th>
                <th>任务状态</th>
                <th>查看任务详情</th>

            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="repWeddingPlanning" runat="server" OnItemCommand="repWeddingPlanning_ItemCommand">

                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("Category") %></td>
                        <td>
                            <%#Eval("CategoryItem") %></td>
                        <td>
                            <%#Eval("ServiceContent") %></td>
                        <td>
                            <%#Eval("Requirement") %></td>
                        <td>
                            <%--<a href="#" onclick="ShowFileUploadPopu('<%#Eval("PlanningID") %>',0)" class="btn btn-mini">上传</a>--%>
                                <a href='#' onclick="ShowFileShowPopu('<%#Eval("PlanningID") %>')" class="btn btn-mini">查看</a>
                           <%-- <a id="inline" href="#data<%#Eval("PlanningID") %>" kesrc="#data<%#Eval("PlanningID")%>">
                                <img src="<%#Eval("ImageAddress") %>" style="height: 35px; width: 35px;" /></a>
                            <div style="display: none;">
                                <div id='data<%#Eval("PlanningID") %>'>
                                    <img src="<%#Eval("ImageAddress") %>" alt="" />
                                </div>
                            </div>--%>

                        </td>
                        <td id="Partd<%#Container.ItemIndex %>">
                            <%#GetEmployeeName(Eval("EmpLoyeeID")) %>
                             

                        </td>
                        <td>
                            <%#Eval("Remark") %></td>
                        <td>
                            <%#GetShortDateString(Eval("PlanFinishDate")) %>
                        </td>
                        <td>
                            <%#Eval("State") %>
                        </td>
                        <td>
                            <asp:LinkButton ID="lnkbtnMission" runat="server" CommandArgument='<%#Eval("PlanningID") %>'>查看任务完成情况</asp:LinkButton></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="8">

                    <cc1:AspNetPagerTool ID="CtrPageIndex" OnPageChanged="WeddingPlanningPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
                </td>
            </tr>
        </tfoot>
    </table>
</asp:Content>


