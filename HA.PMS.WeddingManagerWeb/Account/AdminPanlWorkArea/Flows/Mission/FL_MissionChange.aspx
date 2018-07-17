<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MissionChange.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MissionChange" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        function ShowSelectEmployee(Control) {

            showPopuWindows($(Control).attr("href"), 700, 1000, "#" + $(Control).attr("id"));

        }

        $(document).ready(function () {
            $("#ddlState").change(function () {
                var SelectItem = $("#ddlState").find("option:selected").text();

                if (SelectItem == "改派") {

                    $("#ChangeEmployee").click();
                }

            })
        }
        );

        $(function () {

           // $(".DateTimeTxt").datepicker({ dateFormat: 'yy-mm-dd ' });

        });


    </script>
    <style type="text/css">
        .bolder {
            font-weight: bolder;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <a href="/AdminPanlWorkArea/ControlPage/SelectEmployee.aspx?ControlKey=hideEmpLoyeeID&ALL=True" id="ChangeEmployee" onclick="ShowSelectEmployee(this);" style="display: none;">改派</a>
    <asp:HiddenField ID="hideEmpLoyeeID" Value="0" ClientIDMode="Static" runat="server" />
    <div class="widget-box">


        <div class="widget-content ">
            <table class="table table-bordered table-striped" style="width: 80%; border: 1px solid #e4e0e0;">
                <thead>
                    <tr>
                        <td style="white-space: nowrap; width: 80px;" class="bolder">任务名称</td>

                        <td colspan="7">

                            <asp:TextBox ID="lblMissionName" ClientIDMode="Static" Font-Names="lblMissionName" runat="server" Text="" CssClass="{required:true}"></asp:TextBox>
                        </td>
 

                    </tr>


                    <tr>
                        <td style="white-space: nowrap; width: 90px;" class="bolder">任务类型</td>

                        <td>
                            <asp:TextBox ID="lbltype" runat="server" Text=""></asp:TextBox>
                        </td>

                        <td style="white-space: nowrap;" class="bolder">责任人</td>

                        <td>
                            <asp:Label ID="lblEmpLoyee" runat="server" Text=""></asp:Label>
                        </td>

                        <td style="white-space: nowrap;" class="bolder">开始时间</td>

                        <td>
                            <asp:TextBox ID="lblStarDate" onclick="WdatePicker();" CssClass="DateTimeTxt" Width="93" runat="server" Text=""></asp:TextBox>
                        </td>
                        <td style="white-space: nowrap;" class="bolder">计划完成时间</td>

                        <td colspan="3">
                            <asp:TextBox ID="lblPlanDate" onclick="WdatePicker();" runat="server" Width="93" CssClass="DateTimeTxt" Text=""></asp:TextBox>
                        </td>
                    </tr>




                    <tr>
                        <td style="white-space: nowrap;" class="bolder">任务内容</td>

                        <td colspan="7">
                            <asp:TextBox ID="lblWorkNode" runat="server" Text="" Rows="4" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                        
                    </tr>


                    <tr>
                        <td style="white-space: nowrap;" class="bolder">任务要求</td>

                        <td colspan="7">
                            <asp:TextBox ID="lblFinishStandard" runat="server"  Text="" Rows="4" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>

                        
                    </tr>


                    <tr>
                        <td  style="white-space: nowrap;" class="bolder">申请变更理由</td>
                        <td colspan="7">
                            <asp:Label ID="lblChangeNode" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>


                  


                    <tr>
                        <td style="white-space: nowrap;" class="bolder">变更处理</td>

                        <td colspan="7">
                            <asp:DropDownList ID="ddlState" ClientIDMode="Static" Width="132" runat="server">
                                <asp:ListItem Value="3">不同意变更</asp:ListItem>
                                <asp:ListItem Value="-1">停止该项任务</asp:ListItem>
                                <asp:ListItem Value="3">改派</asp:ListItem>
                                <asp:ListItem Value="3">编辑</asp:ListItem>
                            </asp:DropDownList>
                        </td>
 
                    </tr>


                    <tr>
                        <td class="bolder">理由</td>

                        <td colspan="7">
                            <asp:TextBox ID="txtCheckChangeNode" runat="server" Rows="4" TextMode="MultiLine" Width="99%"></asp:TextBox>
                        </td>
                       
                    </tr>


                    <tr>
                        <td colspan="8">
                            <asp:Button ID="btnSaveChange" runat="server" Text="保存" OnClick="btnSaveChange_Click" CssClass="btn btn-success" />
                        </td>
                       
                    </tr>


                </thead>

            </table>

        </div>
    </div>
</asp:Content>


