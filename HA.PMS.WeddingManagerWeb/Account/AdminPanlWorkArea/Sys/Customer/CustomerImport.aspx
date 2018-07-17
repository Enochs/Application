<%@ Page Title="会员导入" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CustomerImport.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Sys.Customer.CustomerImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#<%=btnImportCustomers.ClientID%>").attr({ "disabled": "disabled", "title": "请先选择文件" });
        });
        function selectexcel() {
            $("#<%=uplExcel.ClientID%>").click();
        }
        function fileonchange(ctrl) {
            var _val = $(ctrl).val();
            $('#spanexcelpath').html(_val);
            if (_val == '') {
                $("#btnselectexcel").html("选择文件").attr("title", "点击选择文件");
                $("#<%=btnImportCustomers.ClientID%>").addClass("disabled").attr({ "disabled": "disabled", "title": "请先选择文件" });
            }
            else {
                var ext = _val.substring(_val.lastIndexOf("."));
                if (ext == ".xls" || ext == ".xlsx") {
                    $("#btnselectexcel").html("已选文件").attr("title", _val);
                    $("#<%=btnImportCustomers.ClientID%>").removeClass("disabled").removeAttr("disabled").attr("title", "导入会员");
                }
                else {
                    alert('该文件不是有效的 Excel 文件！Excel 一般扩展名为：xls，xlsx');
                    $(ctrl).val('');
                    fileonchange(ctrl);
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <thead>
                <tr>
                    <td colspan="6">
                        <a href="#" id="btnselectexcel" class="btn btn-primary btn-mini" title="点击选择文件" onclick="selectexcel();return false;">选择文件</a>
                        <span style="font-family:Verdana" id="spanexcelpath"></span>
                        <asp:FileUpload ID="uplExcel" onchange="fileonchange(this)" style="display:none" runat="server" />
                    </td>
                </tr>
                    <tr>
                        <td colspan="6"><asp:Button Text="导入会员" CssClass="btn btn-success btn-mini" ID="btnImportCustomers" runat="server" OnClick="btnImportCustomers_Click" />
                            <asp:Button Text="下载模版" CssClass="btn btn-info btn-mini" ID="btnDownloadTemplate" runat="server" OnClick="btnDownloadTemplate_Click" />
                        </td>
                    </tr>
                <tr>
                    <th style="width:60px;">行号</th>
                    <th style="width:60px;">新郎姓名</th>
                    <th style="width:60px;">新娘姓名</th>
                    <th style="width:100px;">新娘手机</th>
                    <th style="width:150px;">酒店</th>
                    <th>错误信息</th>
                </tr>
                    </thead>
                <tbody>
                <asp:Repeater ID="rptErrorMsg" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%#(Convert.ToInt32(Eval("Index"))+2) %></td>
                            <td><%#Eval("Groom") %></td>
                            <td><%#Eval("Bride") %></td>
                            <td><%#Eval("BrideCellPhone") %></td>
                            <td><%#Eval("Wineshop") %></td>
                            <td><%#Eval("ErrorMsg") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
