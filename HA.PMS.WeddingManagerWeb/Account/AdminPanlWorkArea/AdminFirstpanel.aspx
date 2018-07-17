<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminFirstpanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.AdminFirstpanel" StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_NewMission.aspx?NeedPopu=1&singer=1";

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

                <ul id="menuUlSmall" class="nav nav-tabs" style="width: 1280px;">
                    <%--<li class="HAtab" onclick="Settabs(this,1,'/AdminPanlWorkArea/Flows/Mission/FL_NewMission.aspx?NeedPopu=1&singer=1');"><a data-toggle="tab" href="#111">新收到任务</a></li>
                    <li class="HAtab" onclick="Settabs(this,1,'/AdminPanlWorkArea/Flows/Mission/FL_MissionMananger.aspx?NeedPopu=1&singer=1');"><a data-toggle="tab" href="#111">今日任务</a></li>
                    <li class="HAtab" onclick="Settabs(this,1,'/AdminPanlWorkArea/Flows/Mission/FL_MissionNextDay.aspx?NeedPopu=1&singer=1');"><a data-toggle="tab" href="#111">明日任务</a></li>
                    <li class="HAtab" onclick="Settabs(this,1,'/AdminPanlWorkArea/Flows/Mission/FL_Missioninweek.aspx?NeedPopu=1&singer=1');"><a data-toggle="tab" href="#111">本周任务</a></li>--%>

                    <li id="DefaultTab" class="HAtab" onclick="Settabs(this,1,'/AdminPanlWorkArea/Flows/Mission/FL_MissionDetailedCreate.aspx?NeedPopu=1&singer=1');"><a data-toggle="tab" href="#111">新建任务/计划</a></li>
                    <%--<li class="HAtab" onclick="Settabs(this,1,'/AdminPanlWorkArea/Flows/Mission/FL_MissioninWait.aspx?NeedPopu=1&singer=1');"><a data-toggle="tab" href="#111">待办任务</a></li>--%>
                    <li class="HAtab" onclick="Settabs(this,1,'/AdminPanlWorkArea/Flows/Mission/FL_MissioninDoing.aspx?NeedPopu=1&singer=1');"><a data-toggle="tab" href="#111">进行中的任务</a></li>
                    <%--<li class="HAtab" onclick="Settabs(this,1,'<%=GetCheckURI() %>');"><a data-toggle="tab" href="#111">待审批任务/计划</a></li>--%>
                    <%--<li class="HAtab" style="display: none;" onclick="Settabs(this,1,'/AdminPanlWorkArea/Flows/Mission/FL_MissionGroupforEdit.aspx?NeedPopu=1&singer=1');"><a data-toggle="tab" href="#111">编辑中的任务/计划</a></li>--%>
                    <li class="HAtab" onclick="Settabs(this,1,'/AdminPanlWorkArea/Flows/Mission/FL_MissionList.aspx?NeedPopu=1&singer=1');"><a data-toggle="tab" href="#111">我的所有任务</a></li>
                    <li class="HAtab" onclick="Settabs(this,1,'/AdminPanlWorkArea/Flows/Mission/FL_MissionSumupManager.aspx?NeedPopu=1&singer=1');"><a data-toggle="tab" href="#111">计划/总结</a></li>
                    <li class="HAtab" onclick="Settabs(this,1,'/AdminPanlWorkArea/Foundation/SysTarget/FL_TargetforEmployee.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">目标管理</a></li>
                    <li class="HAtab" onclick="Settabs(this,1,'/AdminPanlWorkArea/Flows/Mission/WorkReportByDay.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">每日汇总</a></li>

                </ul>

            </div>
            <div class="widget-content tab-content">

                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " id="Iframe1" name="main" scrolling="no" noresize width="100%" height="1000px" frameborder="0" name="table" src="/AdminPanlWorkArea/Flows/Mission/FL_NewMission.aspx?NeedPopu=1&singer=1"></iframe>
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

        $(document).ready(function () {

        });
    </script>
</asp:Content>
