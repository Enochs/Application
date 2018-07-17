<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.Test.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1"  method="post" action="http://sdkhttp.eucp.b2m.cn/sdkproxy/sendsms.action">
    <div>
        <input id="Text1" type="text" name="cdkey" value="0SDK-EBB-0130-NETOL" />
        <input id="Text2" type="text" name="password" value="459519" />
        <input id="Text3" type="text" name="phone" value="13883085340,13883232505" />
        <input id="Text4" type="text" name="message" value="猜猜我是谁" />
        <input id="Text5" type="text" name="addserial" />
        <input id="Submit1" type="submit" value="submit" />
    </div>
    </form>
</body>
</html>
