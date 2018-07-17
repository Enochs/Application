<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CommandByGeneralManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.CommandByGeneralManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_MissionMananger.aspx";

        function Settabs(ControlID, Uri) {

            $(".HAtab").removeClass("active");

            $(ControlID).addClass("active");
            if ($(ControlID).attr("id") == "orderDetailsLi") {
                $("#Iframe1").attr("height", "1780px");

            } else {
                $("#Iframe1").attr("height", "1780px");

            }
            $("#Iframe1").attr("src", Uri);

        }
    </script>
    <!--Chart-box-->

    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li id="DefaultTab" class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/WorkCheckGuide.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">工作指导检查</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/UnderTheWorkingState.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">下属工作状态</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/DynamicIndex.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">年度报表</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/SysReport/SalesRanking.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">销售排行榜</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/SysReport/ChannelCustomerAnalysis.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">渠道排行榜</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Commandanddispatch/Salestrends.aspx');SetIframHeight();"><a data-toggle="tab" href="#111">销售动态监控</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Foundation/SysTarget/FL_TargetByDepartment.aspx');SetIframHeight();"><a data-toggle="tab" href="#111">部门指标动态</a></li>
                    <li class="HAtab" onclick="Settabs(this,'/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_DeliveryScheduleDetailsList.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">供应商管理</a></li>
                    <li class="HAtab" style="display: none;" onclick="Settabs(this,'/AdminPanlWorkArea/Foundation/FD_Storehouse/StorehouseStatistics.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">库房盘存</a></li>
                    <li class="HAtab" style="display: none;" id="orderDetailsLi" onclick="Settabs(this,'/AdminPanlWorkArea/Carrytask/CarrytaskList.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">订单明细</a></li>
                    <li class="HAtab" id="Li1" onclick="Settabs(this,'/AdminPanlWorkArea/Carrytask/CarrytaskList.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">订单明细</a></li>
                   <%-- <li class="HAtab" id="Li2" onclick="Settabs(this,'/AdminPanlWorkArea/Carrytask/CarrytaskOfMonth.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">本月执行订单明细</a></li>--%>
                    <%--<li class="HAtab" id="Li4" onclick="Settabs(this,'/AdminPanlWorkArea/QuotedPrice/QuotedPriceforMonth.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">新签订单明细</a></li>--%>


                    <li class="HAtab" id="Li3" onclick="Settabs(this,'/AdminPanlWorkArea/Commandanddispatch/CanpanyReport.aspx?NeedPopu=1');" style="display: none;"><a data-toggle="tab" href="#111">经营指标动态</a></li>


                </ul>
            </div>
            <div class="widget-content tab-content">
                <div class="tab-pane active" id="tab1">
                    <iframe id="Iframe1" name="main" scrolling="no" noresize width="100%" height="9700px" frameborder="0" name="table" src="/AdminPanlWorkArea/Commandanddispatch/GeneralManagerPodium/WorkCheckGuide.aspx?NeedPopu=1"></iframe>
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
