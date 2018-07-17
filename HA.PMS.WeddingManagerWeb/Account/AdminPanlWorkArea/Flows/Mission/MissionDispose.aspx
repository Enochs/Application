<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MissionDispose.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.MissionDispose" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content3">
    <script>
        $(document).ready(function () {
            if ($("#hideNeedOver").val() == "1") {
                $(".DeleteFile").hide();
            }

            var OpenURI = $("#hideOpen").val();
            if (OpenURI != "1") {
                //("#OperURI").attr("href", OpenURI);
                window.open(OpenURI);
                // $("#ContentTable").hide();
                // $("#OperURI").click();
                window.location.href = $("#hideLocation").val();
            }


        });

        function ShowFileUploadPopu(DetailedID) {
            var Url = "/AdminPanlWorkArea/QuotedPrice/QuotedPricefileUpload.aspx?DetailedID=" + DetailedID + "&Kind=0&IsFinish=1";
            showPopuWindows(Url, 800, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 40px;
        }

        .bolders {
            font-weight: bolder;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <asp:HiddenField ID="hideOpen" runat="server" ClientIDMode="Static" Value="1" />
    <asp:HiddenField ID="hideLocation" runat="server" ClientIDMode="Static" Value="1" />
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <HA:CarrytaskCustomerTitle Visible="false" runat="server" ID="CarrytaskCustomerTitle1" />
    <table style="width: 100%;" class="table table-bordered table-striped" id="ContentTable">
        <tr>
            <td style="width: 100px;" class="bolders">任务名称</td>
            <td style="width: 120px;">
                <asp:Label ID="lblMissionName" runat="server" Text=""></asp:Label>
            </td>
            <td class="bolders" style="width: 100px;">任务发出人</td>
            <td style="width: 120px;">
                <asp:Label ID="lblEmpLoyeeName" runat="server" Text=""></asp:Label>
            </td>
            <td class="auto-style1" style="font-weight: bolder; width: 100px;">任务类型</td>
            <td class="auto-style1">
                <asp:Label ID="lblMissionType" runat="server" Text=""></asp:Label>
            </td>
        </tr>

        <tr style="height: 80px;">

            <td class="bolders">任务要求</td>
            <td colspan="5">
                <asp:Label ID="lblWorkNode" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr style="height: 80px;">

            <td class="bolders">完成标准</td>
            <td colspan="5">
                <asp:Label ID="lblFinishStandard" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="bolders">计划开始时间</td>
            <td colspan="5">
                <cc1:DateEditTextBox Width="90" onclick='WdatePicker({dateFmt:"yyyy/M/d HH:m"})' Enabled="false" ID="txtBeginDate" runat="server"></cc1:DateEditTextBox>
            </td>
        </tr>
        <tr>
            <td class="bolders">计划完成时间</td>
            <td colspan="5">
                <asp:Label ID="lblPlanDate" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr style="height: 50px;">
            <td class="bolders">处理结果</td>
            <td colspan="5">
                <asp:TextBox ID="txtFinishNode" runat="server" TextMode="MultiLine" Rows="4" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="bolders">实际完成时间</td>
            <td colspan="5">
                <cc1:DateEditTextBox Width="90" onclick='WdatePicker({dateFmt:"yyyy/M/d HH:m"})' ID="txtFinishDate" runat="server"></cc1:DateEditTextBox>
            </td>
        </tr>
        <tr>
            <td class="bolders">上传附件</td>
            <td colspan="5">
                <a href="#" class="btn btn-info DeleteFile" onclick="ShowFileUploadPopu(<%=Request["DetailedID"] %>);">上传</a>
                <asp:HiddenField ID="hideNeedOver" runat="server" Value="0" ClientIDMode="Static" />
                <div style="overflow-x: auto; height: 150px;">
                    <ul>
                        <asp:Repeater ID="repfileList" runat="server" OnItemCommand="repfileList_ItemCommand">
                            <ItemTemplate>
                                <li style="margin-bottom: 10px;">
                                    <span style="margin-right: 20px;">
                                        <a href="<%#Eval("FileAddress") %>" target="_blank"><%#Eval("FileName") %></a>
                                    </span>
                                    <asp:LinkButton ID="lnkbtnDelete" CommandArgument='<%#Eval("FileID") %>' CommandName="Delete" CssClass="btn btn-danger DeleteFile" runat="server">删除</asp:LinkButton>

                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClick="btnFinish_Click" CssClass="btn btn-success" Visible="false" />
                <a style="width: 25px; height: 20px;" class="btn btn-primary" href="javascript:history.go(-1);">返回</a>
            </td>
            <td>
                <asp:Button ID="btnFinish" runat="server" ref="finish" OnClientClick="return confirm('是否确认处理？')" Text="确认处理" OnClick="btnFinish_Click" CssClass="btn btn-success" />
            </td>
            <td colspan="5">&nbsp;</td>

        </tr>
    </table>
</asp:Content>



