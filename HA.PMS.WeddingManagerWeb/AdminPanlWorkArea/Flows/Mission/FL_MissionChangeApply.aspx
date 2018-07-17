<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionChangeApply.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionChangeApply" Title="任务变更申请" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoard.ascx" TagPrefix="HA" TagName="MessageBoard" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>



<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">



    <div class="widget-box">

        <hr />
        <div class="widget-content ">
            <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle1" />
            <table class="table table-bordered table-striped with-check" style="width: 80%;" >
                <thead>
                    <tr>
                        <td style="white-space:nowrap;">任务名称</td>

                        <td>
                            <asp:Label ID="lblMissionName" runat="server" Text=""></asp:Label>
                        </td>

                        <td style="white-space:nowrap;">任务类型</td>

                        <td>
                            <asp:Label ID="lbltype" runat="server" Text=""></asp:Label>
                        </td>

                    </tr>


                    <tr>
                        <td style="white-space:nowrap;">任务发出者</td>

                        <td>
                            <asp:Label ID="lblEmpLoyee" runat="server" Text=""></asp:Label>
                        </td>

                        <td style="white-space:nowrap;">开始时间</td>

                        <td>
                            <asp:Label ID="lblStarDate" runat="server" Text=""></asp:Label>
                        </td>

                    </tr>


                    <tr>
                        <td style="white-space:nowrap;">计划完成时间</td>

                        <td>
                            <asp:Label ID="lblPlanDate" runat="server" Text=""></asp:Label>
                        </td>

                        <td style="white-space:nowrap;">任务内容</td>

                        <td>
                            <asp:Label ID="lblWorkNode" runat="server" Text=""></asp:Label>
                        </td>

                    </tr>


                    <tr>
                        <td style="white-space:nowrap;">任务要求</td>

                        <td>
                            <asp:Label ID="lblFinishStandard" runat="server" Text=""></asp:Label>
                        </td>

                        <td>&nbsp;</td>

                        <td>&nbsp;</td>

                    </tr>


                    <tr>
                        <td colspan="4" style="white-space:nowrap;">变更理由</td>

                    </tr>


                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="txtChangeNode" runat="server" Rows="15" TextMode="MultiLine" Width="100%"></asp:TextBox>
                        </td>

                    </tr>


                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnSaveChange" runat="server" Text="保存" OnClick="btnSaveChange_Click"  CssClass="btn"/>
                            请小心 输入理由后不能修改</td>

                    </tr>


                </thead>

            </table>
            <HA:MessageBoard runat="server" ClassType="FL_MissionChangeApply" ID="MessageBoard" />
        </div>
    </div>
</asp:Content>
