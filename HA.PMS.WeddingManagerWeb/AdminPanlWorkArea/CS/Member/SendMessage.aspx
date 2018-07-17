<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SendMessage.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member.SendMessage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="/Scripts/jquery-1.7.1.js"></script>
    <script src="/Scripts/Validator.js"></script>
    <link href="/Scripts/Tooltip.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {

            $('#Text1').attr("reg", ".");
            BindText(400, 'TextArea1');
            BindCtrlEvent('input[check],textarea[check]');

            $("#Submit1").click(function () {
                if (ValidateForm('input[check],textarea[check]')) {
                    var URL = "http://sdkhttp.eucp.b2m.cn/sdkproxy/sendsms.action";
                    try {
                        alert("发送完毕,可以关闭当前页面 也可以继续发送！");
                        $.getJSON(URL + "?" + $('#form1').serialize() + "&jsoncallback=?", function (data)
                        { });
                    } catch (e) {
                        alert("发送完毕");
                        parent.parent.$.fancybox.close(1);
                    }
                } else { return false;}
            });
        });
    </script>
</head>
<body style="background-color: white;">
    <form id="form1" method="post" action="http://sdkhttp.eucp.b2m.cn/sdkproxy/sendsms.action">
        <br />
        <div>
            <br />
            <br />
            <table style="width: 97%;">
                <tr>
                    <td>客户姓名</td>
                    <td><%=GetCustomerName() %></td>
                </tr>
                <tr>
                    <td><span style="color:red">*</span>电话</td>
                    <td><input id="Text1" check="1" type="text" tip="多人发送时请以逗号（ , ）分隔，最多支持<%= LimitCount%>个会员同时发送！" style="width: 100%;" name="phone" value="<%=GetPhone() %>" /></td>
                </tr>
                <tr>
                    <td><span style="color:red">*</span>短信内容</td>
                    <td><textarea id="TextArea1" check="1" tip="限400个字符！" cols="20" rows="15" style="width: 100%;" name="message"><%=GetMessAge() %></textarea></td>
                </tr>
                <tr>
                    <td></td>
                    <td><input id="Hidden1" type="hidden" name="cdkey" value="<%=GetCdKey() %>" />
                        <input id="Hidden2" type="hidden" name="password" value="<%=GetCdPwd() %>" />
                        <input id="Hidden3" type="hidden" name="addserial" />
                        <input id="Submit1" type="button" value="提交" class="btn" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
