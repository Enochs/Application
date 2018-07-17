<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FL_MessageforEmployee.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.FL_MessageforEmployee" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

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
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">

    <a href="/AdminPanlWorkArea/ControlPage/SelectEmployee.aspx?ControlKey=hideEmpLoyeeID&ALL=True" id="ChangeEmployee" onclick="ShowSelectEmployee(this);" style="display: none;">改派</a>
    <asp:HiddenField ID="hideEmpLoyeeID" Value="0" ClientIDMode="Static" runat="server" />
    <div class="widget-box">

        <hr />
        <div class="widget-content ">
              <table class="table table-bordered table-striped with-check" style="width: 80%;" >
                <thead>
                    <tr>
                        <td style="white-space:nowrap;">时间：</td>

                        <td>
          
                            <asp:Label ID="lblCreateDate" runat="server" Text=""></asp:Label>
                        </td>

                        <td style="white-space:nowrap;">发布人</td>

                        <td>
                            <asp:Label ID="lblCreateEmployee" runat="server" Text=""></asp:Label>
                        </td>

                    </tr>


                    <tr>
                        <td style="white-space:nowrap;">消息</td>

                        <td>
                            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                        </td>

                        <td style="white-space:nowrap;">&nbsp;</td>

                        <td>
                            &nbsp;</td>

                    </tr>


                </thead>

            </table>

        </div>
    </div>
</asp:Content>