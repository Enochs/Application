<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="SetOfBasicInformationAdminPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SetOfBasicInformationAdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--渠道管理 -->
    <script type="text/javascript">
        $(document).ready(function () {
            window.location.href = "/AdminPanlWorkArea/Foundation/FD_Content/Sys_ComplayLogoConfig.aspx";
        });
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


                    <li class="HAtab" onclick="Settabs(this,1,'/AdminPanlWorkArea/Foundation/FD_Content/CS_DegreeAssessResultConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">满意评价</a></li>
                    <li class="HAtab" onclick="Settabs(this,2,'/AdminPanlWorkArea/Foundation/FD_Content/CA_WeddingSceneEvaluationResultConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">扣款说明</a></li>
                    <li class="HAtab" onclick="Settabs(this,3,'/AdminPanlWorkArea/Foundation/FD_Content/CS_MemberServiceTypeResultConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">服务类型</a></li>
                    <li class="HAtab" onclick="Settabs(this,4,'/AdminPanlWorkArea/Foundation/FD_Content/CS_MemberServiceMethodResultConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">服务方式</a></li>
                    <li class="HAtab" onclick="Settabs(this,5,'/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_CategoryManager.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">公司产品类别/项目设置</a></li>

                    <li class="HAtab" onclick="Settabs(this,6,'/AdminPanlWorkArea/Foundation/FD_Content/FL_OrderMoneySpanConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">订单金额段设置</a></li>

                    <li class="HAtab" onclick="Settabs(this,7,'/AdminPanlWorkArea/Foundation/FD_Content/FL_MoneyRateSpanConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">订单金额利润率设置</a></li>
                    <li class="HAtab" onclick="Settabs(this,8,'/AdminPanlWorkArea/Foundation/FD_Content/Sys_ComplayLogoConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">公司Logo上传</a></li>
                    <li class="HAtab" onclick="Settabs(this,9,'/AdminPanlWorkArea/Foundation/FD_Content/Sys_WeddingPlanningConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">婚礼统筹</a></li>
                    <li class="HAtab" onclick="Settabs(this,10,'/AdminPanlWorkArea/Foundation/FD_Content/CA_CompanySaleRateConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">公司质量指标</a></li>

                </ul>
            </div>
            <div class="widget-content tab-content">

                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " name="main"  id="Iframe1" width="100%" height="900px" frameborder="0" name="table" src="/AdminPanlWorkArea/Foundation/FD_Content/CS_DegreeAssessResultConfig.aspx?NeedPopu=1"></iframe>
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
