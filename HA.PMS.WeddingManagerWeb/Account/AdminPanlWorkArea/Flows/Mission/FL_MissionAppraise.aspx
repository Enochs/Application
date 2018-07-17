<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionAppraise.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionAppraise" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content3">
    <script>
        function ShowImageList() {
            $(".dropdown-menuX").show();
            return false;
        }

        function SetImage(Control) {
            $("#MineImage").show();
            $("#MineImage").attr("src", $(Control).attr("src"));
            $("#hideImage").attr("value", $(Control).attr("src"));
            $(".dropdown-menuX").hide();
        }
    </script>
    <style type="text/css">
        .bolders {
            font-weight: bolder;
        }

        #listUl li {
            float: left;
            margin-right: 20px;
            list-style: none;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <asp:HiddenField ID="hideOpen" runat="server" ClientIDMode="Static" Value="1" />
    <asp:HiddenField ID="hideLocation" runat="server" ClientIDMode="Static" Value="1" />

    <table style="width: 100%;" class="table table-bordered table-striped" id="ContentTable">
        <tr>
            <td class="bolders" style="width: 110px;">任务名称</td>
            <td>
                <asp:Label ID="lblMissionName" runat="server" Text=""></asp:Label>
            </td>
            <td class="bolders" style="width: 100px;">责任人</td>
            <td style="width: 120px;">
                <asp:Label ID="lblEmpLoyeeName" runat="server" Text=""></asp:Label>
            </td>
            <td class="bolders" style="width: 120px;">任务类型</td>
            <td>
                <asp:Label ID="lblMissionType" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr>
            <td class="bolders">计划开始时间</td>
            <td>
                <cc1:DateEditTextBox ID="txtBeginDate" Width="120" runat="server"></cc1:DateEditTextBox>
            </td>
            <td class="bolders">计划完成时间</td>
            <td>
                <cc1:DateEditTextBox Width="120" ID="txtPlanDate" runat="server"></cc1:DateEditTextBox>
            </td>
            <td class="bolders">倒计时提醒天数</td>
            <td>
                <asp:TextBox ID="txtCountdown" Width="50" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="bolders">处理结果</td>
            <td colspan="5">
                <asp:TextBox ID="txtFinishNode" runat="server" TextMode="MultiLine" Rows="4" Width="100%"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td class="bolders">实际完成时间</td>
            <td>
                <cc1:DateEditTextBox ID="txtFinishDate" Width="120" runat="server"></cc1:DateEditTextBox>
            </td>
            <td colspan="4">&nbsp;</td>

        </tr>
        <tr>
            <td style="white-space: nowrap;" class="bolders">附件：</td>
            <td colspan="5">
                <div style="overflow-x: auto; height: 100px;">
                    <ul>
                        <asp:Repeater ID="repfileList" runat="server">
                            <ItemTemplate>
                                <li><%#Eval("FileName") %>--<a href="<%#Eval("FileAddress") %>">下载</a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td class="bolders">评价</td>
            <td style="width: 80px;">
                <asp:DropDownList Width="75" ID="ddlAppraise" runat="server">
                    <asp:ListItem Value="1">优</asp:ListItem>
                    <asp:ListItem Value="2">良</asp:ListItem>
                    <asp:ListItem Value="3">中</asp:ListItem>
                    <asp:ListItem Value="4">差</asp:ListItem>
                </asp:DropDownList>
                <a href="#" onclick="ShowImageList();" class="btn btn-info">选择表情</a>
            </td>
            <td colspan="4">


                <ul id="listUl" class="dropdown-menuX ample" style="display: none; height: 300px; width:1000px; border: 1px solid;">

                    <li>
                        <img src="/Images/Appraise/1.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                    <li>
                        <img src="/Images/Appraise/2.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                    <li>
                        <img src="/Images/Appraise/3.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                    <li>
                        <img src="/Images/Appraise/4.gif" style="cursor: pointer; height: 100px; width: 100px;" onclick="SetImage(this);" /></li>
                    <li>
                        <img src="/Images/Appraise/5.gif" style="cursor: pointer;height: 100px; width: 100px;" onclick="SetImage(this);" /></li>
                    <li>
                        <img src="/Images/Appraise/6.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                    <li>
                        <img src="/Images/Appraise/7.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                    <li>
                        <img src="/Images/Appraise/8.gif" style="cursor: pointer;height: 100px; width: 100px;" onclick="SetImage(this);" /></li>
                    
                  
                    
                    
                    <li>
                        <img src="/Images/Appraise/16.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                   
                    <li>
                        <img src="/Images/Appraise/18.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                    <li>
                        <img src="/Images/Appraise/19.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                    <li>
                        <img src="/Images/Appraise/20.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                     <li>
                        <img src="/Images/Appraise/10.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                    <li>
                        <img src="/Images/Appraise/9.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                     <li>
                        <img src="/Images/Appraise/11.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                    <li>
                        <img src="/Images/Appraise/12.gif" style="cursor: pointer;height: 100px; width: 100px;" onclick="SetImage(this);" /></li><li>
                        <img src="/Images/Appraise/15.gif" style="cursor: pointer;" onclick="SetImage(this);" /></li>
                </ul>
               <asp:Image ID="MineImage" ClientIDMode="Static" runat="server" />
                <asp:HiddenField ID="hideImage" runat="server" ClientIDMode="Static" />
            </td>
        </tr>
        <tr>
            <td class="bolders">评价说明</td>
            <td colspan="5">
                <asp:TextBox ID="txtAppraiseContent" runat="server" TextMode="MultiLine" Rows="4" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnFinish" runat="server" Text="保存评价" OnClick="btnFinish_Click" CssClass="btn btn-success" />
            </td>
            <td colspan="5">&nbsp;</td>

        </tr>
    </table>
</asp:Content>



