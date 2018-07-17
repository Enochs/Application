<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommandByManagement.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.CommandByManagement" Title="本人指挥台"  StylesheetTheme="Default" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_MissionMananger.aspx";

        function Settabs(ControlID, Index, Uri) {
            Uri = "/AdminPanlWorkArea/Flows/Mission/" + Uri + ".aspx?NeedPopu=1";
            $(".tab-pane").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe" + Index).attr("src", Uri);
        }



    </script>
    <!--Chart-box-->

    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li id="DefaultTab"><a data-toggle="tab" href="#111" onclick="Settabs('#tab1',1,'FL_MissionMananger');">工作指导检查</a></li>
                    <li><a data-toggle="tab" href="#111" onclick="Settabs('#tab2',2,'FL_MissionMananger');">下属工作状态</a></li>
                    <li><a data-toggle="tab" href="#111" onclick="Settabs('#tab3',3,'FL_MissionNextDay');">经营指标动态</a></li>
                    <li><a data-toggle="tab" href="#111" onclick="Settabs('#tab4',4,'FL_Missioninweek');">销售动态监控</a></li>
                    <li><a data-toggle="tab" href="#111" onclick="Settabs('#tab5',5,'FL_MissioninDoing');">部门指标情况</a></li>
                    <li><a data-toggle="tab" href="#111" onclick="Settabs('#tab6',6,'FL_MissioninWait');">供应商管理</a></li>
                    <li><a data-toggle="tab" href="#111" onclick="Settabs('#tab7',7,'FL_MissionChangeApply');">库房盘存</a></li>
                </ul>
            </div>
            <div class="widget-content tab-content">
                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " id="Iframe1" width="100%" height="900px" frameborder="0" name="table" src="/AdminPanlWorkArea/StoreSales/StarOrder.aspx?NeedPopu=1"></iframe>
                </div>
                <div class="tab-pane" id="tab2">
                    <iframe class="framchild" id="Iframe2" width="100%" height="900px" frameborder="0" name="table"></iframe>
                </div>
                <div class="tab-pane" id="tab3">
                    <iframe class="framchild" id="Iframe3" width="100%" height="900px" frameborder="0" name="table"></iframe>
                </div>
                <div class="tab-pane" id="tab4">
                    <iframe class="framchild" id="Iframe4" width="100%" height="900px" frameborder="0" name="table"></iframe>
                </div>
                <div class="tab-pane" id="tab5">
                    <iframe class="framchild" id="Iframe5" width="100%" height="900px" frameborder="0" name="table"></iframe>
                </div>
                <div class="tab-pane" id="tab6">
                    <iframe class="framchild" id="Iframe6" width="100%" height="900px" frameborder="0" name="table"></iframe>
                </div>
                <div class="tab-pane" id="tab7">
                    <iframe class="framchild" id="Iframe7" width="100%" height="900px" frameborder="0" name="table"></iframe>
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
