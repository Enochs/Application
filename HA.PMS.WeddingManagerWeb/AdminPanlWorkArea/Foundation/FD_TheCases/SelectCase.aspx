<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectCase.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases.SelectCase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../../../App_Themes/Default/css/bootstrap.min.css" rel="stylesheet" />
    <style type="text/css">
        .cb td {
            padding: 10px;
        }

        .cb label {
            display: inline-block;
            margin-left: 5px;
            font-size: 12px;
            color: #333333;
        }
    </style>
    <script type="text/javascript">
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 200px; height: 400px;">
            <table style="width: 200px; margin-top: 20px;">
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnSelect" ClientIDMode="Static" Text="确认选择" CssClass="btn btn-success" OnClick="btnSelect_Click" /></td>
                </tr>
                <asp:Repeater runat="server" ID="rptCase">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:CheckBox runat="server" ID="chkCaseName" ClientIDMode="Static" CssClass="cb" Text='<%#Eval("CaseName") %>' ToolTip='<%#Eval("CaseName") %>' AutoPostBack="false" />
                                <asp:HiddenField runat="server" ID="HideCaseID" Value='<%#Eval("CaseID") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnSelects" Text="确认选择" CssClass="btn btn-success" OnClick="btnSelect_Click" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
