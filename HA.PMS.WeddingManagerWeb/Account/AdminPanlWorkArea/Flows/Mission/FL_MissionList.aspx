<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionList.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionList" Title="所有任务" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>


<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/DateRanger.ascx" TagPrefix="HA" TagName="DateRanger" %>





<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">


        //查看/下载附件
        function ShowDownLoad(KeyID, Control) {
            var Url = "/AdminPanlWorkArea/Flows/Mission/FL_MissionFileDownload.aspx?DetailedID=" + KeyID + "&Kind=0";
            showPopuWindows(Url, 500, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //处理完成
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
        <div id="tab1" class="tab-pane active">
            <div class="alert">
                <strong>您今日共有<cc2:lblMissionfortodaysum ID="lblMissionfortodaysum1" runat="server"></cc2:lblMissionfortodaysum>项任务!</strong> 请安排时间处理。
            </div>
            <table class="table table-borded">
                <tr>
                    <td>任务来源:</td>
                    <td>
                        <asp:DropDownList ID="ddlMissionType" Width="120" runat="server">
                            <asp:ListItem Value="1">电话营销</asp:ListItem>
                            <asp:ListItem Value="2">邀约</asp:ListItem>
                            <asp:ListItem Value="3">跟单</asp:ListItem>
                            <asp:ListItem Value="4">报价</asp:ListItem>
                            <asp:ListItem Value="5">制作执行明细</asp:ListItem>
                            <asp:ListItem>分工执行</asp:ListItem>
                            <asp:ListItem Value="7">婚礼统筹</asp:ListItem>
                            <asp:ListItem Value="60">临时任务</asp:ListItem>
                            <asp:ListItem Value="61">周计划</asp:ListItem>
                            <asp:ListItem Value="62">月计划</asp:ListItem>
                            <asp:ListItem Value="63">季度计划</asp:ListItem>
                            <asp:ListItem Value="65">半年计划</asp:ListItem>
                            <asp:ListItem Value="64">年度计划</asp:ListItem>
                            <asp:ListItem Selected="True" Value="0">请选择</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;任务状态：</td>
                    <td>
                        <asp:DropDownList ID="ddlMisssionState" runat="server"></asp:DropDownList></td>
                    <td>
                        <HA:MyManager runat="server" ID="MyManager" Title="责任人：" />
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDateType">
                            <asp:ListItem Text="请选择" Value="0" />
                            <asp:ListItem Text="计划完成时间" Value="PlanDate" />
                            <asp:ListItem Text="实际完成时间" Value="FinishDate" />
                        </asp:DropDownList>
                    </td>
                    <td>
                        <HA:DateRanger runat="server" ID="DateRanger" />
                    </td>
                    <td>
                        <asp:Button ID="btnSerch" CssClass="btn  btn-primary" runat="server" Text="查询" OnClick="btnSerch_Click" /></td>
                </tr>
            </table>
        </div>
        <div class="widget-content">

            <asp:Repeater ID="rptMission" OnItemCommand="rptMission_ItemCommand" OnItemDataBound="rptMission_ItemDataBound" runat="server">
                <HeaderTemplate>
                    <table class="table table-bordered table-striped" id="tblContent" style="width: 100%;">
                        <thead>
                            <tr>
                                <th style="width: 130px; white-space: nowrap;">任务名称</th>
                                <th style="white-space: nowrap;">任务来源</th>
                                <th style="white-space: nowrap;">工作内容</th>
                                <th style="white-space: nowrap;">完成标准</th>
                                <th style="white-space: nowrap;">附件</th>
                                <th style="white-space: nowrap;">计划完成时间</th>
                                <th style="white-space: nowrap;">实际完成时间</th>
                                <th style="white-space: nowrap;">倒计时</th>
                                <th style="white-space: nowrap;">紧急程度</th>
                                <th style="white-space: nowrap;">责任人</th>
                                <th style="white-space: nowrap;">任务状态</th>
                                <th style="white-space: nowrap;">操作</th>
                            </tr>
                        </thead>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr <%#ChangeColor(Eval("DetailedID")) %>>
                        <td style="word-break: break-all;"><%#Eval("MissionName") %></td>
                        <td><%#GetMissiontypeName(Eval("Missiontype")) %></td>
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
                            <asp:Label runat="server" ID="lblPlanDate" Text='<%#Eval("PlanDate","{0:yyyy-MM-dd HH:mm:ss}") %>' />
                            <asp:TextBox runat="server" ID="txtPlanDate" Text='<%#Eval("PlanDate","{0:yyyy-MM-dd HH:mm:ss}") %>' Width="75px" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" Visible="false" />
                        </td>
                        <td>
                            <asp:Label runat="server" ID="lblFinishDate" Text='<%#Eval("FinishDate","{0:yyyy-MM-dd HH:mm:ss}") %>' />
                            <asp:TextBox runat="server" ID="txtFinishDate" Text='<%#Eval("FinishDate","{0:yyyy-MM-dd HH:mm:ss}") %>' Width="75px" onclick="WdatePicker({skin:'whyGreen',dateFmt:'yyyy-MM-dd HH:mm:ss'})" Visible="false" />
                        </td>
                        <td><%#Eval("Countdown") %>天</td>
                        <td><%#Eval("Emergency") %></td>
                        <td id="<%#Guid.NewGuid().ToString() %>">
                            <input runat="server" id="txtEmpLoyee" style="width: 60px;" class="txtEmpLoyeeName" value='<%#GetEmployeeName(Eval("EmpLoyeeID")) %>' type="text" onclick="ShowPopu(this);" />
                            <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" runat="server" Value='<%#GetEmployeeName(Eval("EmpLoyeeID")) %>' />

                        </td>
                        <td><%#GetMissionState(Eval("DetailedID")) %></td>
                        <td style="white-space: nowrap;">
                            <asp:LinkButton runat="server" ID="btnChangeUpdate" Text="变更"  CssClass="btn btn-primary btn-mini " CommandName="Change" CommandArgument='<%#Eval("DetailedID") %>' />
                            <a class="btn btn-primary btn-mini" <%#(Eval("MissionState").ToString() == "1" || Eval("MissionState").ToString() =="3") || User.Identity.Name.ToInt32() != Eval("EmployeeID").ToString().ToInt32() ? "style='display:none'" : "" %> <%#IsVisible(Eval("DetailedID")) %> onclick='ShowUpdateWindows("<%#Eval("DetailedID") %>",this)'>处理任务</a>
                            <a class="btn btn-primary btn-mini" <%#Eval("MissionState").ToString() == "1" || Eval("MissionState").ToString() =="3" ? "" : "style='display:none'" %> href="MissionDispose.aspx?DetailedID=<%#Eval("DetailedID") %>">查看任务</a>
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
