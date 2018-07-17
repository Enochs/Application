<%@ Page Language="C#"   AutoEventWireup="true" CodeBehind="FD_CelebrationPackageDetails2.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.Foundation.FD_CelebrationPackageDetails2" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="/App_Themes/FourGuardian/demo/css/demo.css" type="text/css" rel="stylesheet" />
     <script src="/App_Themes/FourGuardian/demo/js/jquery-1.5.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.fadeThis').append('<span class="hover"></span>').each(function () {
                var $span = $('> span.hover', this);
                if ($.browser.msie && $.browser.version < 9)
                    $span.hide();
                else
                    $span.css('opacity', 0);
                $(this).hover(function () {
                    if ($.browser.msie && $.browser.version < 9)
                        $span.show();
                    else
                        $span.stop().fadeTo(500, 1);
                }, function () {
                    if ($.browser.msie && $.browser.version < 9)
                        $span.hide();
                    else
                        $span.stop().fadeTo(500, 0);
                });
            });
        });
    </script>
</head>
<body>
    <div id="holder">
        <div class="bg">
            <div class="content">
                <asp:Repeater ID="rptCelePackageTop" runat="server">
                    <ItemTemplate>
                        <div class="gallery">
                            <a style="color:#cfcbcb" class="fadeThis" href='CelebrationPackageSinger.aspx?PackageID=<%#Eval("PackageID") %>' target="_blank">
                                <asp:Image ID="imgtop1" Width="460"  Height="240" ToolTip="" runat="server" ImageUrl='<%#Eval("PackageImgTop") %>' />
                                <i></i><span class="hover"></span>
                                <h4>特别呈现：<a style="color:#cfcbcb" href='CelebrationPackageSinger.aspx?PackageID=<%#Eval("PackageID") %>' target="_blank"><%#Eval("PackageTitle") %></a></h4>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Repeater ID="rptCelePackageList" runat="server">
                    <ItemTemplate>
                        <div class="gallery small">
                            <a class="fadeThis" href='CelebrationPackageSinger.aspx?PackageID=<%#Eval("PackageID") %>' target="_blank">
                                 <asp:Image ID="imgtop2" Width="160"  Height="90" ToolTip="" runat="server" ImageUrl='<%#Eval("PackageImgTop") %>' />
                                <i></i><span class="hover"></span>
                                <a style="color:#cfcbcb" href='CelebrationPackageSinger.aspx?PackageID=<%#Eval("PackageID") %>' target="_blank"><%#Eval("PackageTitle") %></a>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
    <form id="form1" runat="server">
    </form>
</body>
</html>
