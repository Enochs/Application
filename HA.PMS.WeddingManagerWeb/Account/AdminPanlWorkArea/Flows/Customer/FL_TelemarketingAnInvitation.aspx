<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FL_TelemarketingAnInvitation.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.FL_TelemarketingAnInvitation" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            //$("#<//%=txtDate.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            $("a[id^=configTime]").fancybox();

            $("#<%=txtDate.ClientID%>").change(function () {
                $("#hid_Flag").val($(this).val());
               
            });
        });
       
        //设置沟通时间 objNode 当前点击元素节点 InvitationId沟通Id
        function ShowConfigTimePopu(objNode, InvitationId) {
            //name="Invita"
         
            $("#<%=hfInvitation.ClientID%>").val(InvitationId);
            var Url = $(objNode).attr("href");
           
            $(Control).attr("id", "configTime" + InvitationId);
            $("a#"+$(Control).attr("id")).fancybox();
           
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>
                客户未邀约界面
            </h5>
        </div>
        <div class="widget-content ">
            <a href="#" class="btn btn-primary  btn-mini" id="newMessage">新消息</a>
            <a href="#" class="btn btn-primary  btn-mini" id="NotTelemarkting">未邀约</a>
            <a href="#" class="btn btn-primary  btn-mini" id="TelemarkingIng">邀约中</a>
            <a href="#" class="btn btn-primary  btn-mini" id="SuccessTelemark">邀约成功</a>
            <a href="#" class="btn btn-primary  btn-mini" id="TelemarkingRunOff">流失</a>
            <a href="#" class="btn btn-primary  btn-mini" id="newPersonDetails">新人明细</a>
            <a class="btn btn-primary  btn-mini" href="FL_CustomersCreate.aspx" id="createCustomers">录入新人信息 </a>
            <a class="btn btn-primary  btn-mini" id="TelemarkSum" href="#">邀约统计分析</a>
            <a class="btn btn-primary  btn-mini" id="createMission" href="#">创建新任务</a>

            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>姓名</th>
                        <th>联系方式</th>
                        <th>婚期</th>
                        <th>酒店</th>
                        <th>来源渠道</th>
                        <th>推荐人</th>
                        <th>分派日期</th>

                        <th>分派人</th>
                        <th>说明</th>
                        <th>沟通时间</th>
                        <th>设定沟通时间</th>


                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptCustomers" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("Groom") %></td>
                                <td><%#Eval("GroomCellPhone") %></td>
                                <td><%#ShowPartyDate(Eval("PartyDate")) %></td>
                                <td><%#Eval("Wineshop") %></td>
                                <td><%#Eval("Channel") %></td>
                                <td><%#Eval("Referee") %></td>
                                <td><%#GetDateStr(Eval("CreateDate")) %></td>
                                <td><%#Eval("EmployeeName") %></td>
                                <td><%#Eval("Other") %></td>
                                <td><%#Eval("CustomerID ") %></td>
                                <td>
                                    <a href="#divChoose"  id='configTime<%#Eval("InviteID")%>' 
                                        onclick="ShowConfigTimePopu(this,<%#Eval("InviteID") %>)"
                                     class="btn btn-primary  btn-mini">设定时间</a>

                                </td>
                            </tr>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="InvitationPager" AlwaysShow="true" OnPageChanged="InvitationPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
            <asp:HiddenField ID="hfInvitation" runat="server" /> 
            <!--设置时间层 start -->
            <div class="widget-box" id='divChoose' style="display: none; width: 500px;">
                <div class="widget-content">
                    <div class="row-fluid">
                        <div class="span7">
                            <div class="widget-box">
                                <div class="widget-title">
                                    <span class="icon"><i class="icon-ok"></i></span>
                                    <h6>选择时间</h6>
                                </div>
                                <div class="widget-content nopadding">
                                
                                   设置: <asp:TextBox ID="txtDate" onclick="WdatePicker();" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:LinkButton ID="lkbtnQuery" OnClick="lkbtnQuery_Click"  CssClass="btn btn-success"  runat="server">确定</asp:LinkButton>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <!--设置时间层 end-->
        </div>
    </div>
</asp:Content>
