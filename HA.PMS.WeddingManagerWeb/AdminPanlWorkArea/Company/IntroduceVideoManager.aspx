<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="IntroduceVideoManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.IntroduceVideoManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#openDir").click(function () {
                //openFileIIs();
            });
        });
        /*
        start－>run－>regsvr32 c:\WINDOWS\system32\shell32.dll
        start－>run－>regsvr32 c:\WINDOWS\system32\wshom.ocx
        start－>run－>regsvr32 c:\WINDOWS\system32\scrrun.dll
        */
        function openFileIIs(filename) {
            try {
                var obj = new ActiveXObject("wscript.shell");
                if (obj) {
                    //obj.Run("\"" + filename + "\"", 1, false);  
                    obj = null; 
                }
            } catch (e) {
                //alert("file not found");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="widget-box" style="padding:2em 2em">
            <span style="color: green;">友情提示：
                <br />1.将视频格式转换为 mov ，并将视频名称重命名为：IntroduceVideo ,程序将自动读取该视频文件
                <br />2.把视频的文件拷贝到 软件安装<b><a id="openDir" href="#">\Files\IntroduceVideo\</a></b>目录下
                <br />3.若要使用网络视频,则在下面文本框中输入网络视频地址
                <br />4.若要修改回使用本地视频，请将下面的文本框置为空，并保存即可<br />
            </span>
            网络地址：<asp:TextBox ID="txtVideoPath" Width="30em" MaxLength="256" ToolTip="网络地址是以 http:// 开头的网址，如：http://www.baidu.com" runat="server"></asp:TextBox>
            <asp:Button ID="ButSave" runat="server" CssClass="btn btn-success" Text="保存" OnClick="ButSave_Click" />
    </div>
</asp:Content>
