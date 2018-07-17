<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_Target.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget.FL_Target" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" Title="目标管理" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        function ShowPopu(TargetID) {
            var Url = "/AdminPanlWorkArea/Foundation/SysTarget/FL_TagetShow.aspx?TargetID=" + TargetID;
            showPopuWindows(Url, 450, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        function ShowUpdatePopu(TargetID) {
            var Url = "/AdminPanlWorkArea/Foundation/SysTarget/FL_TargetUpdate.aspx?TargetID=" + TargetID;
            showPopuWindows(Url, 450, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }


        function ShouCreatePopu()
        {
            var Url = "/AdminPanlWorkArea/Foundation/SysTarget/FL_TargetCreate.aspx";
            showPopuWindows(Url, 450, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <div class="widget-box">

        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <td colspan="3" style="border: none;">
                            <div style="margin-left: 79%;">
                              <a href="#" onclick="ShouCreatePopu();" class="btn btn-primary  btn-mini">添加目标类型</a> 
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <th style="white-space: nowrap;">指标名称</th>
                        <th style="white-space: nowrap;">所属模块</th>
                        <th style="white-space: nowrap;">操作</th>

                    </tr>
                </thead>
                <asp:Repeater ID="repTarget" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("TargetTitle") %></td>
                            <td><%#Eval("ChannelName")%></td>
                            <td>
                                <a href="# " onclick="ShowPopu(<%#Eval("TargetID") %>);" class="btn btn-primary  btn-mini">查看说明</a>
                                <a href="#" onclick="ShowUpdatePopu(<%#Eval("TargetID") %>);" class="btn btn-primary  btn-mini">修改说明</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>

            </table>
        </div>
    </div>

</asp:Content>


