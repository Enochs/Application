<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskWorkPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CarrytaskWorkPanel" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ MasterType VirtualPath="~/AdminPanlWorkArea/Master/MainMaster.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        function Settabs(ControlID, Index, Uri) {
            var URI = "/AdminPanlWorkArea/Carrytask/" + Uri + ".aspx?NeedPopu=1";
            //if (Uri == 'DispatchingforEmployee') {
            //    window.location = URI;
            //    return true;
            //}

            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }

        function GoUrl(UrI, Control) {
            $(".HAtab").removeClass("active");
            $(Control).addClass("active");
            $("#Iframe1").attr("src", UrI + "?NeedPopu=1");
        }
    </script>
    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li class="HAtab active" onclick="Settabs(this,2,'CarrytaskOfNew');"><a data-toggle="tab" href="#111">新订单</a></li>
                    <li class="HAtab" id="DefaultTab" onclick="Settabs(this,1,'DispatchingforEmployee');"><a data-toggle="tab" href="#111">执行中的订单</a></li>
                    <li class="HAtab" onclick="Settabs(this,3,'CarrytaskOfWeek');"><a data-toggle="tab" href="#111">一周内的订单</a></li>
                    <li class="HAtab" onclick="Settabs(this,4,'CarrytaskList');SetIframHeight();"><a data-toggle="tab" href="#111">订单执行明细</a></li>
                    <li class="HAtab" onclick="GoUrl('/AdminPanlWorkArea/CS/CS_DegreeOfSatisfactionList.aspx?Typer=4&NeedPopu=1',this);"><a data-toggle="tab" href="#111" >满意度结果</a></li>
                    <li class="HAtab" onclick="GoUrl('/AdminPanlWorkArea/Flows/Customer/ReturnVisit/FL_CustomerReturnVisitList.aspx?Typer=4&NeedPopu=1',this);"><a data-toggle="tab" href="#111" >回访结果</a></li>
               <%--     <li class="HAtab" onclick="Settabs(this,5,'CarrytaskAppraise');"><a data-toggle="tab" href="#111">订单执行评价</a></li>--%>
                </ul>
            </div>
            <div class="widget-content tab-content">
                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " id="Iframe1"  name="main"   width="100%" height="1150px"  scrolling="no" name="table" src="/AdminPanlWorkArea/Carrytask/CarrytaskOfNew.aspx?NeedPopu=1"></iframe>
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
    <%--  <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_MissionMananger.aspx";

        function Settabs(ControlID,  Uri) {
            Uri = "/AdminPanlWorkArea/Carrytask/" + Uri + ".aspx?NeedPopu=1";
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
                    <li class="HAtab"  id="DefaultTab" onclick="Settabs(this,'DispatchingforEmployee');"><a data-toggle="tab" href="#111" >正在执行的订单</a></li>
                    <li class="HAtab"  onclick="Settabs(this,'CarrytaskOfNew');"><a data-toggle="tab" href="#111">新订单</a></li>
                    <li class="HAtab" onclick="Settabs(this,'CarrytaskOfWeek');"><a data-toggle="tab" href="#111">一周内的订单</a></li>
                    <li class="HAtab" onclick="Settabs(this,'CarrytaskShow');"><a data-toggle="tab" href="#111">订单执行情况</a></li>
                    <li class="HAtab" onclick="Settabs(this,'OrderAppraise');"><a data-toggle="tab" href="#111">订单执行评价</a></li>
                    <li class="HAtab" onclick="Settabs(this,'DoingTask');"><a data-toggle="tab" href="#111">我的执行任务</a></li>
                    <li class="HAtab" onclick="Settabs(this,'DispatchingforEmployee');"><a data-toggle="tab" href="#111>我的派工任务</a></li>
                    <li class="HAtab" onclick="Settabs(this,'OrderDirectCost');"><a data-toggle="tab" href="#111">订单成本明细</a></li>
                </ul>
            </div>
            <div class="widget-content tab-content">

                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " id="Iframe1" width="100%" height="900px" frameborder="0" name="table" src="/AdminPanlWorkArea/Carrytask/DispatchingforEmployee.aspx?NeedPopu=1"></iframe>
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
    </script>--%>
</asp:Content>
