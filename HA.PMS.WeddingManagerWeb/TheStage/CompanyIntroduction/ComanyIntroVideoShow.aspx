<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComanyIntroVideoShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.CompanyIntroduction.ComanyIntroVideoShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="text-align:center">
    <div style="margin:auto 5em">
    <video id="videoPlay" controls="controls" autoplay="autoplay"  width="1000" height="700">
        <source src='<%=ViewState["MoviePath"] %>' />
    </video>
        </div>
</body>
</html>
