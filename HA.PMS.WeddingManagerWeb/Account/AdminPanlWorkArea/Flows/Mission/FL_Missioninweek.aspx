<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_Missioninweek.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_Missioninweek" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc2" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FL_MissionUpdate.aspx?DetailedID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 500, "a#" + $(Control).attr("id"));
        }


        function ShowDownLoad(KeyID, Control) {
            var Url = "/AdminPanlWorkArea/Flows/Mission/FL_MissionFileDownload.aspx?DetailedID=" + KeyID + "&Kind=0";
            showPopuWindows(Url, 500, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();

            //var Url = "FL_MissionFileDownload.aspx?DetailedID=" + KeyID;
            //showPopuWindows(Url, 700, 500, "a#" + $(Control).attr("id"));
        }


        //$(document).ready(function () {

        //    showPopuWindows($("#createMission").attr("href"), 1000, 1000, "a#createMission");

        //});
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <a href="#" id="SelectEmpLoyeeBythis" style="display:none;">选择</a>
    <div class="widget-box">
        <div id="tab1" class="tab-pane active" style=" border-bottom:1px solid #e1dede; height:44px;">
            <div class="alert">
                

                <strong>您今日共有
             <cc2:lblMissionfortodaysum ID="LblMissionfortodaysum1" runat="server"></cc2:lblMissionfortodaysum>
                    项任务!</strong> 请安排时间处理。
            </div>
             
        </div>
       
        <div class="widget-content">

            <asp:Repeater ID="rptMission" OnItemCommand="rptMission_ItemCommand" runat="server">
                <HeaderTemplate>
                    <table  class="table table-bordered table-striped">
                        <thead>
                            <tr>
                                <th style="white-space:nowrap;">任务名称</th>
                                <th style="white-space:nowrap;"> 工作内容</th>
                                <th style="white-space:nowrap;">完成标准</th>
                                <th style="white-space:nowrap;">附件</th>
                                <th style="white-space:nowrap;">计划完成时间</th>
                                <th style="white-space:nowrap;">倒计时</th>
                                <th style="white-space:nowrap;">完成时间</th>
                                <th style="white-space:nowrap;">紧急程度</th>
                                <th style="white-space:nowrap;">责任人</th>
                                <th style="white-space:nowrap;">任务状态</th>
                                <th style="white-space:nowrap;">操作</th>
                            </tr>
                        </thead>
            
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td  style="word-break:break-all;"><%#Eval("MissionName") %></td>
                        <td  style="word-break:break-all;"><%#Eval("WorkNode") %></td>
                        <td  style="word-break:break-all;"><%#Eval("FinishStandard") %></td>
                        <td style="white-space:nowrap;"><a href="#" onclick="ShowDownLoad(<%#Eval("DetailedID") %>,this)"  class="btn btn-primary btn-mini" >查看/下载附件</a></td>
                        <td><%#Eval("PlanDate") %></td>
                        <td><%#Eval("Countdown") %>天</td>
                        <td><%#Eval("FinishDate") %></td>
                        <td><%#Eval("Emergency") %></td>
                        <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                        <td><%#GetMissionState(Eval("IsLook"),Eval("IsOver")) %></td>
                        <td style="white-space:nowrap;">

                           <a  class="btn btn-primary btn-mini"  href="MissionDispose.aspx?DetailedID=<%#Eval("DetailedID") %>"   <%#Eval("EmpLoyeeID").ToString()==User.Identity.Name?"":"style='display:none;'" %> > <%#Eval("IsOver").ToString()=="True"?"查看完成情况":"处理任务" %> </a>
                            <a  class="btn btn-primary btn-mini"  href="FL_MissionAppraise.aspx?DetailedID=<%#Eval("DetailedID") %>" style="display:none;"  >评价任务</a>
                            <a  class="btn btn-primary btn-mini"  href="FL_MissionChangeApply.aspx?DetailedID=<%#Eval("DetailedID") %>" <%#Eval("MissionType").ToString().ToInt32()>7&&Eval("ISover").ToString()=="False"?"":"style='display:none'" %> >申请变更</a>
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
