<%@ Page Title="我发下去的任务" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master"  AutoEventWireup="true" CodeBehind="FL_MyCreateMission.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MyCreateMission" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
 <%@ Register Src="~/AdminPanlWorkArea/Control/MyManager.ascx" TagPrefix="HA" TagName="MyManager" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <a href="#" id="SelectEmpLoyeeBythis" style="display:none;">选择</a>
    <div class="widget-box">
        <div id="tab1" class="tab-pane active" style=" border-bottom:1px solid #e1dede; height:44px;">
            <div class="alert">
         
                <strong>您今日共有
                    <asp:Label ID="lblMissionCount" runat="server" Text="Label"></asp:Label>
                    项任务!</strong> 请安排时间处理。
            </div>
             
        </div>
                           <table class="queryTable">
                        <tr>
                            <td>
                                <table id="queryTableContent">
                                    <tr>

                                        <td>&nbsp;</td>
                                        <td>
                                            <HA:MyManager runat="server" ID="MyManager" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>

                                        <td>
                                            <asp:Button ID="btnSerch" runat="server" Text="查询" OnClick="btnSerch_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
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
                                <th style="white-space:nowrap;">完成时间</th>
                                <th style="white-space:nowrap;">倒计时</th>
                                <th style="white-space:nowrap;">紧急程度</th>
                                <th style="white-space:nowrap;">责任人</th>
                                <th style="white-space:nowrap;">操作</th>
                            </tr>
                        </thead>
            
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td  style="word-break:break-all;"><%#Eval("MissionName") %></td>
                        <td  style="word-break:break-all;"><%#Eval("WorkNode") %></td>
                        <td  style="word-break:break-all;"><%#Eval("FinishStandard") %></td>
                        <td style="white-space:nowrap;"><a href="#" onclick="ShowDownLoad(<%#Eval("DetailedID") %>,this)"  class="btn btn-primary btn-mini" >附件</a></td>
                        <td><%#GetDateStr(Eval("FinishDate")) %></td>
                        <td><%#Eval("Countdown") %>天</td>
                        <td><%#Eval("Emergency") %></td>
                        <td><%#GetEmployeeName(Eval("EmpLoyeeID")) %></td>
                        <td style="white-space:nowrap;">

                           
                            <a  class="btn btn-primary btn-mini"  href="FL_MissionAppraise.aspx?DetailedID=<%#Eval("DetailedID") %>" <%#GetStateStyle(Eval("IsOver")) %>   >评价任务</a>
                        </td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>

                    </table>
                </FooterTemplate>
            </asp:Repeater>
 
            <cc1:AspNetPagerTool ID="AspNetPagerTool1" runat="server">
            </cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
