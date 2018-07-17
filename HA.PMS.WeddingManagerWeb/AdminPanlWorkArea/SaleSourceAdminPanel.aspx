<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="SaleSourceAdminPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SaleSourceAdminPanel" %>

<%@ MasterType VirtualPath="~/AdminPanlWorkArea/Master/MainMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--渠道管理 -->
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/Mission/FL_MissionMananger.aspx";

        function Settabs(ControlID, Uri, FramHeight) {
           var IframID="<%=Iframe1.ClientID%>";
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            if ($(ControlID).attr("id") == "CheckComputation") {
                $("#" + IframID).attr("height", "1200px");

            } else {
                $("#" + IframID).attr("height", "1000px");

            }
            $("#" + IframID).attr("src", Uri);

            //$(ControlID).addClass("active");

            $("#" + IframID).attr("src", Uri);
        }

        //关闭窗口 刷新父级
        function Golocation(Uri) {
            window.open(Uri + "?NeedPopu=1");
        }


        function ClickTab(Index) {
            if (Index == "6") {
                $("#tab6").click();
            }

            if (Index == "1") {
                $("#DefaultTab").click();
            }
        }
    </script>
    <!--Chart-box-->

    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs" id="Titletabs">
 
                    <asp:Repeater runat="server" ID="Navgator">
                        <ItemTemplate>
                            <li class="HAtab" style="font-size:5px;" id="DefaultTab" onclick="Settabs(this,'<%#Eval("ChannelAddress") %>?NeedPopu=1',1200);">
                                <a data-toggle="tab" href="#111"><%#Eval("ChannelName") %></a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div>
                <br />&nbsp;
            </div>
            <div class="widget-content tab-content">
                <div class="tab-pane active" id="tab1">
                    <iframe runat="server" class="framchild " name="main" scrolling="no" noresize id="Iframe1" width="100%" height="1100px" frameborder="no"  src="/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SaleSourcesManager.aspx?NeedPopu=1"></iframe>
                </div>
            </div>
        </div>
    </div>
    <!--End-Chart-box-->

    <!--end-Footer-->
    <script src="js/excanvas.min.js"></script>
    <script src="js/jquery.min.js"></script>
    <script src="js/jquery.ui.custom.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/jquery.flot.min.js"></script>
    <script src="js/jquery.flot.resize.min.js"></script>
    <script src="js/jquery.peity.min.js"></script>
    <script src="js/fullcalendar.min.js"></script>
    <script src="js/lijianwei.js"></script>
    <script src="js/lijianwei.dashboard.js"></script>
    <script src="js/jquery.gritter.min.js"></script>
    <script src="js/lijianwei.interface.js"></script>
    <script src="js/lijianwei.chat.js"></script>
    <script src="js/jquery.validate.js"></script>
    <script src="js/lijianwei.form_validation.js"></script>
    <script src="js/jquery.wizard.js"></script>
    <script src="js/jquery.uniform.js"></script>
    <script src="js/select2.min.js"></script>
    <script src="js/lijianwei.popover.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <script src="js/lijianwei.tables.js"></script>
    <link href="/Scripts/Function/jquery.fancybox.css" rel="stylesheet" />
    <script src="/Scripts/Function/jquery.fancybox.pack.js"></script>
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
</asp:Content>
