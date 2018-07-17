<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="SupplierAdminPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.SupplierAdminPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!--库房管理 -->
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/Mission/FL_MissionMananger.aspx";

        function Settabs(ControlID, Index, Uri) {
            $("#Iframe1").attr("height", $(ControlID).attr("valHight"));
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
                    <li class="HAtab active" id="DefaultTab" hight="700px" onclick="Settabs(this,1,'/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SupplierCreate.aspx?NeedPopu=1');">
                        <a data-toggle="tab" href="#111">新建供应商</a>

                    </li>
                    <li class="HAtab" hight="1000px" onclick="Settabs(this,2,'/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_DeliveryScheduleDetails.aspx?NeedPopu=1');">
                        <a data-toggle="tab" href="#111">供应商明细</a>

                    </li>
                    <%--             <li class="HAtab"  onclick="Settabs(this,3,'/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SupplierErrorLog.aspx?NeedPopu=1');">
                        <a data-toggle="tab" href="#111" >差错记录表</a>

                    </li>--%>
                    <%--<li class="HAtab"  onclick="Settabs(this,4,'/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_DeliveryScheduleDetails.aspx?NeedPopu=1');">
                        <a data-toggle="tab" href="#111" >供货明细表</a></li>--%>
                    <%--  <li class="HAtab"  onclick="Settabs(this,5,'/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SupplierProductCreate.aspx#?NeedPopu=1');">
                        <a data-toggle="tab" href="#111" >添加供货商产品</a></li>--%>
                    <li class="HAtab" valhight="1400px" id="SupplierProductDetails" onclick="Settabs(this,5,'/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SupplierProductDetails.aspx#?NeedPopu=1');">
                        <a data-toggle="tab" href="#111">供应商产品明细</a></li>
                    <li class="HAtab" valhight="1300px" onclick="Settabs(this,5,'/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_ProductTobeDistributed.aspx#?NeedPopu=1');">
                        <a data-toggle="tab" href="#111">待分配产品库</a></li>
                    <li class="HAtab" valhight="1300px" onclick="Settabs(this,5,'/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SupplierProductSignIn.aspx#?NeedPopu=1');">
                        <a data-toggle="tab" href="#111">待分配供应商</a></li>
                    <%--      <li class="HAtab"  onclick="Settabs(this,5,'/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_StorehouseSupplierProductAlls.aspx#?NeedPopu=1');">
                        <a data-toggle="tab" href="#111" >库房及供应商虚拟总产品库</a></li>--%>
                    <li class="HAtab" onclick="Settabs(this,5,'/AdminPanlWorkArea/FinancialAffairs/SupplierCostList.aspx?NeedPopu=1');">
                        <a data-toggle="tab" href="#111">供应商已付款
                        </a>
                    </li>

                </ul>
            </div>
            <div class="widget-content tab-content">

                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " name="main" scrolling="no" noresize id="Iframe1" width="100%" height="700px" frameborder="0" name="table" src="/AdminPanlWorkArea/Foundation/FD_SaleSources/FD_SupplierCreate.aspx?NeedPopu=1"></iframe>
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
