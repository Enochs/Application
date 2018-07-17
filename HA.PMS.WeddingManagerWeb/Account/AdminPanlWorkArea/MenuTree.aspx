<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuTree.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.MenuTree" StylesheetTheme="Default" %>

<!DOCTYPE html>
<html lang="zh-CN">
<head runat="server">

    <title>HoldLove Admin</title>
    <meta charset="UTF-8" />

    <script type="text/javascript">

        function SetActiveClass(Control) {
            $(".active").removeClass("active");
            $(Control).addClass("active");
        }

        function GoWorkPanel(ChannelID) {
            var URL = "/AdminPanlWorkArea/SaleSourceAdminPanel.aspx?ChannelID=" + ChannelID;
    
            var OldAddress = $('#mainFrame', window.parent.document).attr("src");
            if (OldAddress == URL) {
                $('#mainFrame', window.parent.document).attr("src", URL);

            } else {
                $('#mainFrame', window.parent.document).attr("src", URL);
            }

        }

        function SetActive(ObjControl) {
            var ObjName = $(ObjControl).attr("AddressName");
            var MainFram = window.parent.frames["mainFrame"].document;
            var TabList = $(".HAtab", MainFram);
            var HaveNodeName = "";

            for (i = 0; i < TabList.length; i++) {
                var NewName = $(TabList[i]).attr("AddressName");
                if ($(TabList[i]).children().text() == ObjName) {
                    $(TabList[i]).click();
                    HaveNodeName = ObjName;
                    break;
                }
            }

            if (HaveNodeName == "") {
                $('#mainFrame', window.parent.document).attr("src", $(ObjControl).attr("ChannelAddress"));
            }
            return false;

        }
    </script>
    <style type="text/css">
        imgs {
            width: 263px;
            height: 20px;
        }

        .logoStyle {
            margin-left: 10px;
            margin-top: 5px;
        }

        #sidebar > ul > li {
            border-top: 1px solid #37414b;
            border-bottom: 1px solid #1f262d;
        }
    </style>
</head>
<body style="background-color: #2E363F;">
    <form runat="server" id="form1">
        <input id="hideNeedKey" type="hidden" />
        <%--<asp:Image ID="imgUrl" ImageUrl="~/Files/logo/Logo.jpg"  Width="230" Height="70"  runat="server" />--%>
        <a id="WorkPanelTaget" style="display: none;" href="adminpanl-1.html" target="mainFrame">打开一级页面</a>
        <!--Header-part-->
        <div id="header" style="height: 50px;">

            <div style="margin-top: 30px; margin-bottom: 0px;">
                <%--                <img width="175" height="60" src="../Files/Logo/logo.jpg" class="imgs" />--%>
                <asp:Image CssClass="logoStyle" ID="imgUrl" ImageUrl="~/Files/logo/Logo.jpg" Width="178" Height="45" runat="server" />
            </div>

        </div>
        <br />
        <!--close-Header-part   -->
        <div id="sidebar">
            <ul>
                <li>
                    <span style="margin-left: 3px;"><font color="#ffffff"><strong>欢迎：
                        <asp:Label ID="lblLoginUser" runat="server"></asp:Label></strong></font>
                    </span>
                    <br>
                    <span style="margin-left: 3px;"><font color="#ffffff"><strong>部门：<asp:Label ID="lblDepartment" runat="server"></asp:Label></strong></font>
                    </span>
                    <br>
                    <span style="margin-left: 3px;"><font color="#ffffff"><strong>职位：<asp:Label ID="lblEmpLoyeeJob" runat="server" Text=""></asp:Label><br></strong></font>
                    </span>
                </li>
                <asp:Repeater ID="RepChannel" runat="server" OnItemDataBound="RepChannel_ItemDataBound">
                    <ItemTemplate>
                        <li class="submenu" onclick="SetActiveClass(this);" style="white-space:nowrap">
                            <a href="<%#Eval("ChannelAddress") %>" target="mainFrame" style="white-space:nowrap"><i class="icon icon-file"></i>
                            <span channeladdress="<%#Eval("ChannelAddress") %>" onclick="GoWorkPanel(<%#Eval("ChannelID") %>)">
                                <%#ReturnNbSP(Eval("ChannelName")) %>
                                <asp:HiddenField runat="server" ID="hidekey" Value='<%#Eval("ChannelID") %>' />
                            </span></a>

                            <asp:Repeater ID="repSecond" runat="server">
                                <HeaderTemplate>
                                    <ul>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <li onclick="SetActive(this)" addressname="<%#Eval("ChannelName") %>" channeladdress="<%#Eval("ChannelAddress") %>">
                                        <a><%#Eval("ChannelName") %></a>
                                    </li>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </ul>
                                </FooterTemplate>
                            </asp:Repeater>

                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
        <!--sidebar-menu-->
        <script src="/Scripts/excanvas.min.js"></script>
        <script src="/Scripts/jquery.min.js"></script>
        <script src="/Scripts/jquery.ui.custom.js"></script>
        <script src="/Scripts/bootstrap.min.js"></script>
        <script src="/Scripts/jquery.flot.min.js"></script>
        <script src="/Scripts/jquery.flot.resize.min.js"></script>
        <script src="/Scripts/jquery.peity.min.js"></script>
        <script src="/Scripts/fullcalendar.min.js"></script>
        <script src="/Scripts/lijianwei2.js"></script>
        <script src="/Scripts/lijianwei.dashboard.js"></script>
        <script src="/Scripts/jquery.gritter.min.js"></script>
        <script src="/Scripts/lijianwei.interface.js"></script>
        <script src="/Scripts/lijianwei.chat.js"></script>
        <script src="/Scripts/jquery.validate.js"></script>
        <script src="/Scripts/lijianwei.form_validation.js"></script>
        <script src="/Scripts/jquery.wizard.js"></script>
        <script src="/Scripts/jquery.uniform.js"></script>
        <script src="/Scripts/select2.min.js"></script>
        <script src="/Scripts/lijianwei.popover.js"></script>
        <script src="/Scripts/jquery.dataTables.min.js"></script>
        <script src="/Scripts/lijianwei.tables.js"></script>

        <script type="text/javascript">
            // This function is called from the pop-up menus to transfer to
            // a different page. Ignore if the value returned is a null string:
            function goPage(newURL) {

                // if url is empty, skip the menu dividers and reset the menu selection to default
                if (newURL != "") {

                    // if url is "-", it is this page -- reset the menu:
                    if (newURL == "-") {
                        resetMenu();
                    }
                        // else, send page to designated URL            
                    else {
                        document.location.href = newURL;
                    }
                }
            }

            // resets the menu selection upon entry to this page:
            function resetMenu() {
                document.gomenu.selector.selectedIndex = 2;
            }
        </script>

    </form>

</body>
</html>
