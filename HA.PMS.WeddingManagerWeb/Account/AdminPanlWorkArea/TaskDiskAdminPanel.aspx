<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="TaskDiskAdminPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.TaskDiskAdminPanel" %>

<%@ MasterType VirtualPath="~/AdminPanlWorkArea/Master/MainMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--取件管理 -->
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/Mission/FL_MissionMananger.aspx";

        function Settabs(ControlID, Index, Uri) {

            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");

            //$(ControlID).addClass("active");
            $("#Iframe1").attr("src", Uri);
        }

    </script>
    <!--Chart-box-->

    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs" id="Titletabs">
                    <li class="HAtab active" id="DefaultTab" onclick="Settabs(this,1,'/AdminPanlWorkArea/CS/CS_TakeDiskManager.aspx?NeedPopu=1');">
                        <a data-toggle="tab" href="#111">未收件</a>

                    </li>
                    <li class="HAtab" id="Li1" onclick="Settabs(this,1,'/AdminPanlWorkArea/CS/CS_TakeforNoneNotice.aspx?NeedPopu=1');">
                        <a data-toggle="tab" href="#111">已收件未通知</a>

                    </li>
                    <li class="HAtab" onclick="Settabs(this,2,'/AdminPanlWorkArea/CS/CS_TakeDiskManagerHaveTake.aspx?NeedPopu=1');">
                        <a data-toggle="tab" href="#111">已收件已通知</a>

                    </li>
                    <li class="HAtab" onclick="Settabs(this,3,'/AdminPanlWorkArea/CS/CS_TakeDiskManagerTswkTake.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">本周取件</a></li>
                    <li class="HAtab" onclick="Settabs(this,4,'/AdminPanlWorkArea/CS/CS_TakeDiskManagerAllTake.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">全部取件</a></li>

                </ul>
            </div>
            <div class="widget-content tab-content">

                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " name="main" scrolling="no" noresize id="Iframe1" width="100%" height="1200px" frameborder="0" name="table" src="/AdminPanlWorkArea/CS/CS_TakeDiskManager.aspx?NeedPopu=1"></iframe>
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
