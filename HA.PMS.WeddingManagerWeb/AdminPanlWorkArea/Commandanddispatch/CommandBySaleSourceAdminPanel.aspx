<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CommandBySaleSourceAdminPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.CommandBySaleSourceAdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_MissionMananger.aspx";

        function Settabs(ControlID, Uri) {

            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", Uri);
        }



    </script>
    <!--Chart-box-->

    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/WorkCheckGuide.aspx?NeedPopu=1');" id="DefaultTab"><a data-toggle="tab" href="#111">工作指导检查</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/UnderTheWorkingState.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">下属工作状态</a></li>
                    <li class="HAtab" style="display: none;" onclick="Settabs(this,'/AdminPanlWorkArea/Commandanddispatch/CommandbySaleSourceEmployeeManager.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">下属指标动态</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Foundation/SysTarget/FL_TargetByDepartment.aspx');SetIframHeight();"><a data-toggle="tab" href="#111">部门指标动态</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SaleSourcesRebateStatistics.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">渠道返利明细</a></li>
                    <li style="display: none;"><a data-toggle="tab" href="#111" onclick="Settabs(this,'/AdminPanlWorkArea/Foundation/FDTelemarketing/FD_TelemarketingCustomersDetails.aspx?NeedPopu=1');">渠道客源明细</a></li>
                    <li class="HAtab"  id="Li2" onclick="Settabs(this,'/AdminPanlWorkArea/Carrytask/CarrytaskOfMonth.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">当月订单</a></li>

                </ul>
            </div>
            <div class="widget-content tab-content">
                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " id="Iframe1" name="main" scrolling="no" noresize width="100%" height="900px" frameborder="0" name="table" src="/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/WorkCheckGuide.aspx?NeedPopu=1"></iframe>
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
