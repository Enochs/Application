<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysWorkPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SysWorkPanel" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--渠道管理 -->
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
                 
                    <li class="HAtab" onclick="Settabs(this,3,'/AdminPanlWorkArea/Sys/Jurisdiction/Sys_ChannelManager.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">频道管理</a></li>
           <%--         <li class="HAtab" onclick="Settabs(this,4,'/AdminPanlWorkArea/Sys/Personnel/Sys_EmployeeManager.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">人员管理</a></li>--%>
                    <li class="HAtab" onclick="Settabs(this,5,'/AdminPanlWorkArea/Sys/Personnel/Sys_DepartmentManager.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">部门及部门人员管理</a></li>
                    <%--<li class="HAtab" onclick="Settabs(this,6,'/AdminPanlWorkArea/Sys/Personnel/Sys_EmpLoyeeHigherupsManager.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">人员上级管理</a></li>--%>
                    <li class="HAtab" onclick="Settabs(this,7,'/AdminPanlWorkArea/Sys/Personnel/Sys_EmployeeTypeManager.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">人员类型</a></li>
                    <li class="HAtab" onclick="Settabs(this,8,'/AdminPanlWorkArea/Sys/Personnel/SysEmployeeJobManager.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">人员职务</a></li>
<%--                    <li class="HAtab" onclick="Settabs(this,9,'/AdminPanlWorkArea/Foundation/FD_Content/FD_LoseContentManager.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">邀约流失信息维护</a></li>
                    <li class="HAtab" onclick="Settabs(this,10,'/AdminPanlWorkArea/Foundation/FD_Content/CS_DegreeAssessResultConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">满意评价</a></li>    
                    <li class="HAtab" onclick="Settabs(this,11,'/AdminPanlWorkArea/Foundation/FD_Content/CA_WeddingSceneEvaluationResultConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">扣款说明</a></li>
                    <li class="HAtab" onclick="Settabs(this,11,'/AdminPanlWorkArea/Foundation/FD_Content/CS_MemberServiceTypeResultConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">服务类型</a></li>
                    <li class="HAtab" onclick="Settabs(this,11,'/AdminPanlWorkArea/Foundation/FD_Content/CS_MemberServiceMethodResultConfig.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">服务方式</a></li>--%>
                    
                     </ul>
            </div>
            <div class="widget-content tab-content">

                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " id="Iframe1" width="100%" height="900px" frameborder="0" name="table" src="/AdminPanlWorkArea/Sys/Jurisdiction/Sys_ChannelManager.aspx?NeedPopu=1"></iframe>
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
