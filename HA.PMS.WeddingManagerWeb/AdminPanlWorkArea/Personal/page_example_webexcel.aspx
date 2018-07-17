<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="page_example_webexcel.aspx.cs"
    Inherits="Navi.Kernel.Example.Web.page_example_webexcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Office Excel文件展示</title>
    <script language="javascript" type="text/javascript">

        function btn_load_onclick() {
            //调用后台方法
            PageMethods.GetStringExcelContentByFileName("2301005.xml", GetStringExcelContentByFileName_Result);
        }

        function GetStringExcelContentByFileName_Result(result) {
            var con_excel = document.getElementById("ole_excel");
            if (con_excel == null) {
                return;
            }

            if (result.toString().length <= 0) {
                return;
            }

            con_excel.XMLData = result;
            
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <table style="width:100%; height:100;">
        <tr>
            <td rowspan="2" align="left" valign="top" style="width: 5%; height:100%;">
                <fieldset id="field_data" runat="server">
                    <legend>数据区</legend>
                </fieldset>
            </td>
            <td align="left" valign="top">
                <fieldset id="field_function" runat="server">
                    <legend>操作区</legend>
                    <input id="btn_load" type="button" value="加载数据" onclick="return btn_load_onclick()" />
                </fieldset>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top">
                <fieldset id="field_display" runat="server">
                    <legend>展示区</legend>
                    <object id="ole_excel" classid="clsid:0002E559-0000-0000-C000-000000000046" width="100%"
                        height="600px" standby="Loading">
                    </object>
                </fieldset>
            </td>
        </tr>        
    </table>
    </form>
</body>
</html>
