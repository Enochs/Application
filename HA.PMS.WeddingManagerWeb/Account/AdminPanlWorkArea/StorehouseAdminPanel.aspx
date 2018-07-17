<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="StorehouseAdminPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StorehouseAdminPanel" %>
<%@ MasterType VirtualPath="~/AdminPanlWorkArea/Master/MainMaster.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!--库房管理 -->
     <script type="text/javascript">
         var DefaultURI = "/AdminPanlWorkArea/Flows/Mission/FL_MissionMananger.aspx";
         function Settabs(ControlID, Index, Uri) {
             $(".HAtab").removeClass("active");
             $(ControlID).addClass("active");
             if ($(ControlID).attr("id") == "FD_StorehouseMarkProductsList") {
                 $("#Iframe1").attr("height", "2100px");
                 // scrolling="no" noresize
             } else {
                 $("#Iframe1").attr("height", "2300px");
             }
             //$(ControlID).addClass("active");
             $("#Iframe1").attr("src", Uri);
         }
    </script>
    <!--Chart-box-->
    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs" id="Titletabs">
                    <li class="HAtab active" id="DefaultTab"  onclick="Settabs(this,1,'/AdminPanlWorkArea/Foundation/FD_Storehouse/FD_StorehouseProductCreate.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111" >录入库房产品</a></li>
                    <li class="HAtab"  onclick="Settabs(this,2,'/AdminPanlWorkArea/Foundation/FD_Storehouse/FD_StorehouseProductQuery.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111" >产品使用状况管理</a></li>
                    <li class="HAtab"  id="FD_StorehouseMarkProductsList" onclick="Settabs(this,3,'/AdminPanlWorkArea/Foundation/FD_Storehouse/FD_StorehouseMarkProductsList.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111" >库房备料单</a></li>
                    <li class="HAtab" id="Li2"  onclick="Settabs(this,1,'/AdminPanlWorkArea/Foundation/FD_Storehouse/FD_StorehouseProductCreate.aspx?NeedPopu=1&Vtype=1');"><a data-toggle="tab" href="#111" >报价产品维护</a></li>
                    <li class="HAtab"  id="Li1" onclick="Settabs(this,3,'/AdminPanlWorkArea/Foundation/FD_Storehouse/StorehouseStatistics.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111" >查看库房产品统计</a></li>
                </ul>
            </div>
            <div class="widget-content tab-content">
                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " id="Iframe1"  name="main" scrolling="no" noresize width="100%" height="2300px" frameborder="0" name="table" src="/AdminPanlWorkArea/Foundation/FD_Storehouse/FD_StorehouseProductCreate.aspx?NeedPopu=1"></iframe>
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
