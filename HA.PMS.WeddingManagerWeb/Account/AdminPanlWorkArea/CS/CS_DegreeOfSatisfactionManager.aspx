<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CS_DegreeOfSatisfactionManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_DegreeOfSatisfactionManager" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function ShowUpdateWindows(KeyID, Control) {
            var Url = "CS_DegreeOfSatisfactionUpdate.aspx?DofKey=" + KeyID;
            $(Control).attr("id", "updateShow" + KeyID);
            showPopuWindows(Url, 700, 1000, "a#" + $(Control).attr("id"));
        }

        $(document).ready(function () {

            showPopuWindows($("#createDegrees").attr("href"), 700, 1000, "a#createDegrees");
            //$("#<//%=txtPartyDateStar.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            //$("#<//%=txtPartyDateEnd.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <a href="CS_DegreeOfSatisfactionCreate.aspx" class="btn btn-primary  btn-mini" id="createDegrees">添加</a>

    <div class="widget-box">

        <div class="widget-content">
            <div class="widget-box">
    <div class="widget-content">

        <table class="table table-bordered table-striped with-check">
            
            <tr>
                <td style="white-space:nowrap;">婚期</td>
                <td>
                  <asp:TextBox ID="txtPartyDateStar" MaxLength="20" onclick="WdatePicker();" runat="server"></asp:TextBox>
                </td>
                <td style="text-align:center;">到</td>
                <td>
                  <asp:TextBox ID="txtPartyDateEnd" MaxLength="20" onclick="WdatePicker();" runat="server"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td style="white-space:nowrap;width:25%;">客户满意度:</td>
                <td style="white-space:nowrap;width:25%;">
                     <asp:DropDownList ID="ddlSumDof" runat="server" ></asp:DropDownList>星
                </td>
                <td style="white-space:nowrap;width:25%;text-align:right;">
                    <asp:Button ID="btnQuery" CssClass="btn" runat="server" Text="查询" OnClick="btnQuery_Click" />
                </td>
                <td style="white-space:nowrap;width:25%;">            
                </td>
            </tr>  
        </table>
    </div>
</div>  
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th>电话</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>到店时间</th>
                        <th>预定时间</th>
                        <th>婚礼顾问</th>
                        <th>策划师</th>
                        <th>取件时间</th>
                        <th>调查状态</th>
                        <th>调查时间</th>
                        <th>调查结果</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDegree" OnItemCommand="rptDegree_ItemCommand" 
                        OnItemDataBound="rptDegree_ItemDataBound" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><a href="/AdminPanlWorkArea/CS/Member/CustomerDetails.aspx?CustomerID=<%#Eval("CustomerID") %>" target="_blank"><%#Eval("Groom") %>/<%#Eval("Groom") %></a></td>
                                <td><%#Eval("GroomCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#Eval("ConvenientIinvitationTime") %></td>
                                <td><%#Eval("LastFollowDate") %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#GetPlannerName(Eval("Planner")) %></td>
                                <td><%#Eval("realityTime") %></td>
                                <td><%#Eval("StateContent") %></td>
                                <td><%#Eval("DofDate") %></td>
                                <td><%#Eval("DegreeResult") %></td>
                                <td><asp:Literal ID="ltlEdit" runat="server"></asp:Literal>
                                    <asp:LinkButton  CssClass="btn btn-danger btn-mini" ID="lkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("DofKey") %>' runat="server">删除</asp:LinkButton>                              
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="DegreePager" AlwaysShow="true" OnPageChanged="DegreePager_PageChanged"  runat="server"></cc1:AspNetPagerTool>
            <HA:MessageBoard runat="server" ClassType="CS_DegreeOfSatisfactionManager" ID="MessageBoard" />
        </div>
    </div>
</asp:Content>
