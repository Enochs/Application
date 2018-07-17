<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Default" CodeBehind="CS_TakeDiskCheck.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_TakeDiskCheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../../Scripts/jquery.min.js"></script>

    <style type="text/css">
        .Main {
            margin-top:15px;
        }
        .table_Content {
            /*color:black;*/
        }
    </style>
    
    <script type="text/javascript">
        $(document).ready(function () {
            $("#btnConfirm").click(function () {
                var DropDownList1 = document.getElementById("ddlCheckState");            //获取DropDownList控件的引用

                var DropDownList1_Index = DropDownList1.selectedIndex;                 //获取选择项的索引
                var DropDownList1_Value = DropDownList1.options[DropDownList1_Index].value;   //获取选择项的值
                var DropDownList1_Text = DropDownList1.options[DropDownList1_Index].text;     //获取选择项的文本
                if (DropDownList1_Text == "审核失败" && $("#txtReason").val() == "") {
                    alert("请说明失败原因");
                    return false;
                } else if (DropDownList1_Text == "未审核") {
                    alert("请选择一个审核状态");
                    return false;
                }
            });
        });
        function Check() {

            var DropDownList1 = document.getElementById("ddlCheckState");            //获取DropDownList控件的引用

            var DropDownList1_Index = DropDownList1.selectedIndex;                 //获取选择项的索引
            var DropDownList1_Value = DropDownList1.options[DropDownList1_Index].value;   //获取选择项的值
            var DropDownList1_Text = DropDownList1.options[DropDownList1_Index].text;     //获取选择项的文本
            if (DropDownList1_Text == "审核失败" && $("#txtReason").val() == "") {
                alert("请说明失败原因");
                return false;
            } else if (DropDownList1_Text == "未审核") {
                alert("请选择一个审核状态");
                return false;
            }
        }


        function selectIndexChanged() {
            var textbox = document.getElementById("txtReason");
            var trs = document.getElementById("tr_CheckReason");
            var DropDownList1 = document.getElementById("ddlCheckState");            //获取DropDownList控件的引用

            var DropDownList1_Index = DropDownList1.selectedIndex;                 //获取选择项的索引
            var DropDownList1_Value = DropDownList1.options[DropDownList1_Index].value;   //获取选择项的值
            var DropDownList1_Text = DropDownList1.options[DropDownList1_Index].text;     //获取选择项的文本
            if (DropDownList1_Text == "未审核") {
                trs.style.display = "none";
            } else if (DropDownList1_Text == "审核通过") {
                trs.style.display = "none";
            }
            if (DropDownList1_Text == "审核失败") {
                trs.style.display = "block";
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="All">
            <div class="Main">
                <table runat="server" id="tblChecks">
                    <tr>
                        <td colspan="2">审核状态
                            <asp:DropDownList runat="server" ID="ddlCheckState">
                                <asp:ListItem Text="未审核" Value="0" />
                                <asp:ListItem Text="审核通过" Value="1" />
                                <asp:ListItem Text="审核失败" Value="2" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="tr_CheckReason" style="display: none;">
                        <td>失败原因</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtReason" TextMode="MultiLine" Height="150px" Width="325px" ClientIDMode="Static" /></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button runat="server" ID="btnConfirm" ClientIDMode="Static" Text="确定" CssClass="btn btn-primary" OnClick="btnConfirm_Click" /></td>
                    </tr>
                </table>
                <h3 style="color:#247ad7;font-weight:bolder;font-size:14px;" class="alert alert-success">审核记录</h3>
                <table class="table_Content">
                    <asp:Repeater runat="server" ID="rptContent">
                        <ItemTemplate>
                            <tr>
                                <td colspan="2" style="text-align: left">
                                    <%#Eval("CreateDate","{0:yyyy-MM-dd}") %>&nbsp;&nbsp;&nbsp;
                                    第<font style="color: red;"><%#Container.ItemIndex+1%></font>次审核&nbsp;&nbsp;&nbsp;
                                    审核后状态：<%#Eval("Status") %>&nbsp;&nbsp;&nbsp;
                                    本次审核人：<%#GetEmployeeName(Eval("CreateEmployee").ToString()) %>&nbsp;&nbsp;&nbsp;
                                            <p><%#Eval("Content") %></p>
                                </td>
                            </tr>
                            <tr>
                                <td>----------------------------------------------------------------------------------------------</td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
