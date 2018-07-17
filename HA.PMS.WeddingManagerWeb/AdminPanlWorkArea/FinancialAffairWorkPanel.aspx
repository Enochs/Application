<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FinancialAffairWorkPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.FinancialAffairWorkPanel" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>
<%@ MasterType VirtualPath="~/AdminPanlWorkArea/Master/MainMaster.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/FinancialAffairs/FL_MissionMananger.aspx";


        function Settabs(ControlID, Index, Uri) {
            var URI = "/AdminPanlWorkArea/FinancialAffairs/" + Uri + ".aspx?NeedPopu=1";
            //$(".tab-pane").removeClass("active");
            //$(ControlID).addClass("active");
            //$("#Iframe" + Index).attr("src", Uri);
        
            $("#Iframe1").attr("height", $(ControlID).attr("valHight"));
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");

            //$(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }

        function GoURI(Control,URI)
        {
            URI = URI + "?needpopu=1";
            //$(".tab-pane").removeClass("active");
            //$(ControlID).addClass("active");
            //$("#Iframe" + Index).attr("src", Uri);
            $("#Iframe1").attr("height", $(Control).attr("valHight"));
            $(".HAtab").removeClass("active");
            $(Control).addClass("active");

            //$(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }
    </script>
    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li class="HAtab active" id="DefaultTab"  onclick="Settabs(this,1,'EarnestMoneyManager');"><a data-toggle="tab" href="#111">定金处理</a></li>
                    <li  class="HAtab" onclick="GoURI(this,'/AdminPanlWorkArea/FinancialAffairs/OrderDirectCost.aspx');SetIframHeightValue(9100);"><a data-toggle="tab" href="#111">订单成本明细</a></li>
                    <li   class="HAtab" onclick="GoURI(this,'/AdminPanlWorkArea/FinancialAffairs/QuptedCollectionsPlanList.aspx');"><a data-toggle="tab" href="#111">收款管理</a></li>
                    <li   class="HAtab" onclick="GoURI(this,'/AdminPanlWorkArea/FinancialAffairs/SupplierCostList.aspx');"><a data-toggle="tab" href="#111">供应商支付明细表</a></li>
                </ul>
            </div>
            <div class="widget-content tab-content">
                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " name="main" scrolling="no" noresize id="Iframe1" width="100%" height="2100px" frameborder="0" name="table" src="/AdminPanlWorkArea/FinancialAffairs/EarnestMoneyManager.aspx?NeedPopu=1"></iframe>
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
