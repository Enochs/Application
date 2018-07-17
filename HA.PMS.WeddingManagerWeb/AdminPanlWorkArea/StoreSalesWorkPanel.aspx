<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreSalesWorkPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSalesWorkPanel" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_MissionMananger.aspx";

        function Settabs(ControlID, Index, Uri) {
            $("#Iframe1").attr("height", 1000);
            Uri = "/AdminPanlWorkArea/StoreSales/" + Uri + ".aspx?NeedPopu=1";
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");
            //if ($(ControlID).attr("id") == "ExpectOrder") {
            //    $("#Iframe1").attr("height", "1200px");
             

            //    // scrolling="no" noresize
            //} else {
            //    $("#Iframe1").attr("height", "900px");
           

            //}
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

    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li  id="ExpectOrder" class="HAtab active" onclick="Settabs(this,2,'ExpectOrder');"><a data-toggle="tab" href="#111" >邀约成功</a></li>
                    <li  class="HAtab" onclick="Settabs(this,3,'FollowUpOrder')"><a data-toggle="tab" href="#111" );">跟单</a></li>
                    <li  class="HAtab" onclick="Settabs(this,4,'SucessOrder');SetIframHeight();"><a data-toggle="tab" href="#111" >成功预定</a></li>
                    <li  class="HAtab" onclick="GoUrl('/AdminPanlWorkArea/StoreSales/StoreSalesList.aspx',this);SetIframHeight();"><a data-toggle="tab" href="#111" >新人明细表</a></li>
                    <li  class="HAtab" onclick="Settabs(this,6,'LoseOrder');"><a data-toggle="tab" href="#111" >流失</a></li>
                    <li  class="HAtab" onclick="Settabs(this,6,'CreateCustomer');" ><a data-toggle="tab" href="#111" style="display:none;">到店新人信息录入</a></li>
                    <li class="HAtab" onclick="GoUrl('/AdminPanlWorkArea/CS/CS_DegreeOfSatisfactionList.aspx?Typer=2&NeedPopu=1',this);"><a data-toggle="tab" href="#111">满意度结果</a></li>
                    <li class="HAtab" onclick="GoUrl('/AdminPanlWorkArea/Flows/Customer/ReturnVisit/FL_CustomerReturnVisitList.aspx?Typer=2&NeedPopu=1',this);"><a data-toggle="tab" href="#111">回访结果</a></li>
                  <li class="HAtab" AddressName="创建任务"><a data-toggle="tab" href="#111" onclick="GoUrl('/AdminPanlWorkArea/Flows/Mission/FL_MissionDetailedCreate.aspx',this);">创建任务</a></li>
                </ul>
            </div>
            <div class="widget-content tab-content">
                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " id="Iframe1"  name="main"  width="100%" height="2500px" frameborder="0" scrolling="no"  name="table" src="/AdminPanlWorkArea/StoreSales/ExpectOrder.aspx?NeedPopu=1"></iframe>
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
