<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Sys_ComplayLogoConfig.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.Sys_ComplayLogoConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function CheckFile(str) {
            var strRegex = "(.jpg|.bmp|.jpeg|.gif|.png)$"; //用于验证PPT扩展名的正则表达式
            var re = new RegExp(strRegex);

            if (re.test(str)) {
                return (true);
            }
            else {
                alert("该文件扩展名不是图片格式");
                return (false);
            }
        }

    </script>
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <span style="color: green;">请上传图片大小为：宽度为175，高度为60 </span>
            <br />
            请选择logo:
            <asp:FileUpload ID="fuLoadFile" onchange="CheckFile(this.value);" runat="server" />
            <br />
            <img runat="server" id="imgLogo" class="imgLogo" style="width: 175px; height: 60px;" />
            <br /><br />

            <asp:Button ID="btnFileLoad" CssClass="btn btn-success" OnClick="btnFileLoad_Click" runat="server" Text="上传" />

        </div>
    </div>

</asp:Content>
