﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CommandByExecuteAdminPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.CommandByExecuteAdminPanel" %>

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
                    <li id="DefaultTab" onclick="Settabs(this,'/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/WorkCheckGuide.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111" >工作指导检查</a></li>
                    <li onclick="Settabs(this,'/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/UnderTheWorkingState.aspx?NeedPopu=1');" class="HAtab"><a data-toggle="tab" href="#111" >下属工作状态</a></li>
                    <li class="HAtab" style="display: none;"><a data-toggle="tab" href="#111" onclick="Settabs('#tab3',3,'/AdminPanlWorkArea/Commandanddispatch/CommandByExecuteEmployeeManager.aspx?NeedPopu=1');">下属指标动态</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Foundation/SysTarget/FL_TargetByDepartment.aspx');SetIframHeight();"><a data-toggle="tab" href="#111">部门指标动态</a></li>
                    <li class="HAtab"  onclick="Settabs(this,'/AdminPanlWorkArea/Commandanddispatch/CommandByInviteManager.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">正在执行的订单</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Carrytask/CarrytaskList.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111" >订单执行明细</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/CS/CS_ComplainManagerAll.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111" >投诉</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/CS/CS_DegreeOfSatisfactionAll.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111" >满意度</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Carrytask/CarrytaskList.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111" >订单成本明细</a></li>

                </ul>
            </div>
            <div class="widget-content tab-content">
                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " id="Iframe1" name="main" scrolling="no" noresize width="100%" height="900px" frameborder="0" name="table" src="/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/WorkCheckGuide.aspx?NeedPopu=1"></iframe>
                </div>
                <div class="tab-pane" id="tab2">
                    <iframe class="framchild" id="Iframe2" name="main" scrolling="no" noresize width="100%" height="900px" frameborder="0" name="table"></iframe>
                </div>
                <div class="tab-pane" id="tab3">
                    <iframe class="framchild" id="Iframe3" name="main" scrolling="no" noresize width="100%" height="900px" frameborder="0" name="table"></iframe>
                </div>
                <div class="tab-pane" id="tab4">
                    <iframe class="framchild" id="Iframe4" name="main" scrolling="no" noresize width="100%" height="900px" frameborder="0" name="table"></iframe>
                </div>
                <div class="tab-pane" id="tab5">
                    <iframe class="framchild" id="Iframe5" name="main" scrolling="no" noresize width="100%" height="900px" frameborder="0" name="table"></iframe>
                </div>
                <div class="tab-pane" id="tab6">
                    <iframe class="framchild" id="Iframe6" name="main" scrolling="no" noresize width="100%" height="900px" frameborder="0" name="table"></iframe>
                </div>
                <div class="tab-pane" id="tab7">
                    <iframe class="framchild" id="Iframe7" name="main" scrolling="no" noresize width="100%" height="900px" frameborder="0" name="table"></iframe>
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
