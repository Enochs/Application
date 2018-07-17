<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="SedimentationReservoirPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SedimentationReservoirPanel" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/Mission/FL_MissionMananger.aspx";

        function Settabs(ControlID, Index, Uri) {
           
            $(".HAtab").removeClass("active");

            $(ControlID).addClass("active");

            //$(ControlID).addClass("active");
            $("#Iframe1").attr("src", Uri);
        }


        function GoUrl(UrI, Control) {
            $(".HAtab").removeClass("active");
            $(Control).addClass("active");
            $("#Iframe1").attr("src", UrI + "?NeedPopu=1");
        }
    </script>
    <!--Chart-box-->
    <!--沉淀库 -->
    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs" id="Titletabs">
                    <li class="HAtab active" id="DefaultTab" AddressName="PPT" onclick="Settabs(this,1,'/AdminPanlWorkArea/Foundation/FD_PPTWarehouse/FD_PPTWareHouseManager.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111" >PPT</a></li>
                    <li class="HAtab" AddressName="公司图片库" onclick="Settabs(this,2,'/AdminPanlWorkArea/Foundation/FD_ImageWarehouse/FD_ImageWarehouseManager.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111" >公司图片库</a></li>
                    
                   <li class="HAtab" AddressName="视频"  onclick="GoUrl('',this);"><a data-toggle="tab" href="#111">视频</a></li>
                  <li class="HAtab" AddressName="文本文件"><a data-toggle="tab" href="#111" onclick="Settabs(this,7);">文本文件</a></li>
                     
                </ul>
            </div>
            <div class="widget-content tab-content">

                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " name="main" scrolling="no" noresize id="Iframe1" width="100%" height="900px" frameborder="0" style="" name="table" src="/AdminPanlWorkArea/Foundation/FD_PPTWarehouse/FD_PPTWareHouseManager.aspx?NeedPopu=1"></iframe>
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
