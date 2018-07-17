<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="FD_CelebrationPackageLoadImg.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage.FD_CelebrationPackageLoadImg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <script type="text/javascript">
        function addFile() {
            var str = '<INPUT type="file" size="50" NAME="File">'
            document.getElementById('MyFile').insertAdjacentHTML("beforeEnd", str)
        }
        
    </script>
</head>
<body style="  width:500px; height:350px;">
    <form id="form1" method="post" runat="server" enctype="multipart/form-data">
        <div>
            <h3>上传文件</h3>
            <p id="MyFile">
                <input type="file" name="File" />
            </p>
            <p>
                <input type="button" value="增加" onclick="addFile()" /><br />
                <input onclick="this.form.reset()" type="button" value="重置" /><br />
                <asp:Button runat="server" Text="开始上传" ID="UploadButton"></asp:Button>
            </p>
            <p>
               
            </p>
        </div>
    </form>
</body>
</html>
