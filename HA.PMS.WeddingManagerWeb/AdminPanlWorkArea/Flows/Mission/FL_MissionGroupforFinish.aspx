<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionGroupforFinish.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionGroupforFinish" Title="任务消息审核" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">

        function ShowUpdateWindows(KeyID, Control) {
            var Url = "FL_MissionUpdate.aspx?DetailedID=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 500, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {

            showPopuWindows($("#createMission").attr("href"), 700, 500, "a#createMission");

        });
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1" runat="server">



    <div class="widget-box">

        <hr />
        <div class="widget-content ">
            <table class="table table-bordered table-striped with-check">
                <thead>
                    <tr>
                        <td style="white-space: nowrap;">任务标题</td>
                        <td colspan="7" style="text-align: left;">&nbsp;<asp:Label ID="lbltitle" runat="server" Text="Label"></asp:Label></td>
                    </tr>
                    <tr>
                        <th style="white-space: nowrap;">任务名称</th>
                        <th style="white-space: nowrap;">工作内容</th>
                        <th style="white-space: nowrap;">完成标准</th>
                        <th style="white-space: nowrap;">附件</th>
                        <th style="white-space: nowrap;">完成时间</th>
                        <th style="white-space: nowrap;">倒计时</th>
                        <th style="white-space: nowrap;">紧急程度</th>
                        <th style="white-space: nowrap;">责任人</th>
               
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptMission" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>

                                    <%#Eval("MissionName") %></td>
                                <td>
                                    <%#Eval("WorkNode") %></td>
                                <td>
                                    <%#Eval("FinishStandard") %></td>
                                    <td>
                                        <%#Eval("Attachment") %></td>
                                    <td>
                                        <%#Eval("FinishDate") %></td>
                                    <td>
                                        <%#Eval("Countdown") %>天</td>
                                    <td>
                                        <%#Eval("Emergency") %></td>
                                    <td>
                                        <%#Eval("ChecksNode") %></td>
                      
                              
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tr>
                    <th colspan="8" style="text-align: left;">&nbsp;</th>
                </tr>
                <tr>
                    <td  style="text-align: left;"> 
                        审核意见
                    </td>
                    <td> 
                        <asp:Label ID="lblState" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>审核人</td>
                    <td colspan="5">  
                        <asp:Label ID="lblCheckEmployee" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td  style="text-align: left;"> 
                        审核时间</td>
                    <td> 
                        <asp:Label ID="lblCheckDate" runat="server" Text="Label"></asp:Label>
                    </td>
                    <td>审核说明</td>
                    <td colspan="5">  
                        <asp:Label ID="lblCheckContent" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        &nbsp;</td>
                </tr>
            </table>

        </div>
    </div>
</asp:Content>
