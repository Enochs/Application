<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissioninDoing.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissioninDoing" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
    <script type="text/javascript">
        //查看/下载附件
        function ShowDownLoad(KeyID, Control) {
            var Url = "/AdminPanlWorkArea/Flows/Mission/FL_MissionFileDownload.aspx?DetailedID=" + KeyID + "&Kind=0";
            showPopuWindows(Url, 500, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //确认完成
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "/AdminPanlWorkArea/Flows/Mission/DealWithMission.aspx?DetailsID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));
        }

        //改派
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ClassType=QuotedPriceWorkPanel&ALL=1";
            showPopuWindows(Url, 450, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div class="widget-box">
        <div id="tab1" class="tab-pane active" style="border-bottom: 1px solid #e1dede; height: 44px;">
            <div class="alert">

                <strong>您今日共有
              <cc2:lblMissionfortodaysum ID="LblMissionfortodaysum1" runat="server"></cc2:lblMissionfortodaysum>
                    项任务!</strong> 请安排时间处理。
            </div>

        </div>

        <div class="widget-content">

            <asp:Repeater ID="rptMission" OnItemCommand="rptMission_ItemCommand" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th style="white-space: nowrap;">任务名称</th>
                                <th style="white-space: nowrap;">工作内容</th>
                                <th style="white-space: nowrap;">完成标准</th>
                                <th style="white-space: nowrap;">附件</th>
                                <th style="white-space: nowrap;">计划开始时间</th>
                                <th style="white-space: nowrap;">计划完成时间</th>
                                <th style="white-space: nowrap;">倒计时</th>
                                <th style="white-space: nowrap;">紧急程度</th>
                                <th style="white-space: nowrap;">责任人</th>
                                <th style="white-space: nowrap;">操作</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr <%#ChangeColor(Eval("DetailedID")) %>>
                        <td style="word-break: break-all;"><%#Eval("MissionName") %></td>
                        <td style="word-break: break-all;">
                            <asp:Label runat="server" ID="lblWorkNode" Text='<%#Eval("WorkNode") %>' />
                            <asp:TextBox runat="server" Text='<%#Eval("WorkNode") %>' ID="txtWorkNode" Visible="false" />
                        </td>
                        <td style="word-break: break-all;">
                            <asp:Label runat="server" ID="lblFinishStandard" Text='<%#Eval("FinishStandard") %>' />
                            <asp:TextBox runat="server" Text='<%#Eval("FinishStandard") %>' ID="txtFinishStandard" Visible="false" />
                        </td>
                        <td style="white-space: nowrap;"><a href="#" onclick="ShowDownLoad(<%#Eval("DetailedID") %>,this)" class="btn btn-primary btn-mini">查看/下载附件</a></td>
                        <td>
                            <asp:Label runat="server" ID="lblStartDate" Text='<%#Eval("StarDate","{0:yyyy-MM-dd HH:mm:ss}") %>' />
                            <asp:TextBox runat="server" ID="txtStartDatee" Text='<%#Eval("StarDate","{0:yyyy-MM-dd HH:mm:ss}") %>' onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" Visible="false" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblPlanDate" Text='<%#Eval("PlanDate","{0:yyyy-MM-dd HH:mm:ss}") %>' />
                            <asp:TextBox runat="server" ID="txtPlanDate" Text='<%#Eval("PlanDate","{0:yyyy-MM-dd HH:mm:ss}") %>' onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" Visible="false" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblCountdown" Text='<%#Eval("Countdown") %>' />天</td>
                        <td>
                            <asp:Label runat="server" ID="lblEmergency" Text='<%#Eval("Emergency") %>' />
                        </td>
                        <td id="<%#Guid.NewGuid().ToString() %>">
                            <input runat="server" id="txtEmpLoyee" style="width: 60px;" class="txtEmpLoyeeName" value='<%#GetEmployeeName(Eval("EmpLoyeeID")) %>' type="text" onclick="ShowPopu(this);" /><br />
                            <%--<a href="#" onclick="ShowPopu(this);" class="SGEmpLoyee btn btn-primary btn-mini">选择</a>--%>
                            <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" runat="server" Value='<%#GetEmployeeName(Eval("EmpLoyeeID")) %>' />
                        </td>
                        <td style="white-space: nowrap;">
                            <%--<a class="btn btn-primary btn-mini" href="FL_MissionChangeApply.aspx?DetailedID=<%#Eval("DetailedID") %>" <%#IsManager(Eval("EmployeeID")) %>>任务变更</a>&nbsp;--%>
                            <%--<asp:LinkButton runat="server" ID="btnSaveEmployeeID" Text="保存" CommandName="Save" CommandArgument='<%#Eval("DetailedID") %>' CssClass="btn btn-primary btn-mini" />--%>
                            <asp:LinkButton runat="server" ID="btnChangeUpdate" Text="变更" CssClass="btn btn-primary btn-mini " CommandName="Change" CommandArgument='<%#Eval("DetailedID") %>' />
                            <a class="btn btn-success btn-mini btnSaveSubmit<%#Eval("EmployeeID") %> btnSumbmit" href="#" onclick='ShowUpdateWindows("<%#Eval("DetailedID") %>",this)' <%#IsVisible(Eval("DetailedID")) %>>确认完成</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <cc1:AspNetPagerTool ID="CtrPageIndex" runat="server" OnPageChanged="CtrPageIndex_PageChanged"></cc1:AspNetPagerTool>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
