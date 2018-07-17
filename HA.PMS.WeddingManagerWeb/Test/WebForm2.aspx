<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.Test.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" method="post" action="http://sdkhttp.eucp.b2m.cn/sdkproxy/regist.action">
        <div>
            CD<input id="Text1" type="text" name="cdkey" />PWD<input id="Text2" type="text" name="password" />
            <input id="Submit1" type="submit" value="submit" />
        </div>
    </form>
</body>
</html>
