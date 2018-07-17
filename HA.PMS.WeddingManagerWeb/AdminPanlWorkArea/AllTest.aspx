<%@ Page Language="C#" StylesheetTheme="Default" AutoEventWireup="true" CodeBehind="AllTest.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.AllTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../App_Themes/Default/js/jquery.min.js"></script>
    <script src="../App_Themes/Default/js/weboffice.js"></script>
    <!--禁止复制-->
    <script type="text/javascript">
        function click() {
            //alert('禁止你的左键复制！')
        } function click1() {
            if (event.button == 2) {
                //alert('禁止右键点击~！')
            }
        } function CtrlKeyDown() {
            if (event.ctrlKey) {
                //alert('不当的拷贝将损害您的系统！')
            }
        } document.onkeydown = CtrlKeyDown; document.onselectstart = click; document.onmousedown = click1;

        function clicks() {
            //alert("成功测试");

        }

    </script>

    <!--禁止复制-->
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:Button runat="server" ID="btnOpen" CssClass="btn btn-primary" Text="打开" OnClick="btnOpen_Click" />

                <table class="table table-bordered" style="display: none;">
                    <tr>
                        <th>名称</th>
                        <th>操作</th>
                    </tr>
                    <asp:Repeater runat="server" ID="rptCompanyFile" OnItemCommand="rptCompanyFile_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("FileName") %></td>
                                <td>
                                    <asp:LinkButton runat="server" ID="lbtnLook" Text="查看" CssClass="btn btn-primary btn-mini" CommandArgument='<%#Eval("FileID") %>' CommandName="Look" />
                                    <a href='<%#Eval("FileURL") %>'>查看</a>
                                </td>

                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>

                <embed runat="server" src="~/AdminPanlWorkArea/音乐.m3u" id="emPlayer" autoplay="true" width="auto" />
                <br />

                <br />
                <audio />
            </div>


            <table>
                <tr>
                    <td>
                        <asp:TextBox runat="server" ClientIDMode="Static" ID="txtCopyText" /></td>
                    <td>
                        <asp:Button runat="server" ID="btnCopy" Text="复制" OnClick="btnCopy_Click" /></td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlHotel" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button runat="server" ID="btnFavorites" Text="添加收藏夹" OnClick="btnCopy_Click" /></td>
                    <td><a href="#" onclick="this.style.behavior='url(#default#homepage)';this.setHomePage('');">图片</a></td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox runat="server" ID="txtPasteText" OnTextChanged="txtPasteText_TextChanged" /></td>
                    <td>
                        <asp:Button runat="server" ID="btnPaste" Text="粘贴" OnClick="btnPaste_Click" /></td>
                    <td>
                        <input type="button" value="导入收藏夹" onclick="window.external.ImportExportFavorites(true, '//localhost/')" /></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <a href="#" style="cursor: pointer;" onclick="this.style.behavior='url(#default#homepage)';this.setHomePage('http://www.baidu.com)">设为首页</a>
                    </td>
                    <td>
                        <a href="JavaScript:window.external.AddFavorite('http://www.sina.com','Page')">添加到收藏夹</a>
                    </td>
                </tr>
            </table>

        </div>
        <div>
            <marquee width="20%" height="" scrollamount="10" onmouseover="this.stop()" onmouseout="this.start()" direction="left" bgcolor="gray" style="color: white;"> 其中direction控制方向，scrollamount控制滚动速度，width和height根据周边实际情况设定。程序后加 </marquee>
        </div>
        <div>
            <script type="text/javascript">
                function copyUrl2() {
                    function setTxt() {
                        Clipboard.SetText(this.txtCopyText.Text)
                    }
                }
                document.body.onselectstart = document.body.oncontextmenu = function () { return false; }


                function click(e) {
                    if (document.all) {
                        if (event.button == 1 || event.button == 2 || event.button == 3) {
                            oncontextmenu = 'return false';
                        }
                    }
                    if (document.layers) {
                        if (e.which == 3) {
                            oncontextmenu = 'return false';
                        }
                    }
                }
                if (document.layers) {
                    document.captureEvents(Event.MOUSEDOWN);
                }
                document.onmousedown = click;
                document.oncontextmenu = new Function("return false;")
                //*******************************************
                document.onkeydown = function (evt) {
                    if (document.selection.createRange().parentElement().type == "file") {
                        return false;
                    }
                    if ((event.keyCode == 116) || //屏蔽 F5 刷新键 
                    (event.ctrlKey && event.keyCode == 82)) { //Ctrl + R 
                        event.keyCode = 0;
                        event.returnValue = false;
                    }
                    if ((window.event.altKey) && (window.event.keyCode == 115)) { //屏蔽Alt+F4
                        return false;
                    }
                }
            </script>
            <input type="button" onclick="copyUrl2()" value="复制" />
            <div style="text-align: center;" align="center">
                <table style="text-align: center;" align="center">
                    <tr>
                        <td>我们没有在一起</td>
                    </tr>
                </table>
                我们都是在一起的
            </div>
        </div>


    </form>
</body>
</html>
