<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceWorkPanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPriceWorkPanel" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ MasterType VirtualPath="~/AdminPanlWorkArea/Master/MainMaster.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_MissionMananger.aspx";


        function Settabs(ControlID, Index, Uri) {

            URI = "/AdminPanlWorkArea/QuotedPrice/" + Uri + ".aspx?NeedPopu=1&SaleEmployee=<%=Request["SaleEmployee"]%>";

            //$(".tab-pane").removeClass("active");
            //$(ControlID).addClass("active");
            //$("#Iframe" + Index).attr("src", Uri);
            if ($(ControlID).attr("id") == "QuotedPriceallList") {
                $("#Iframe1").attr("height", "1450px");

                // scrolling="no" noresize
            } else {
                $("#Iframe1").attr("height", "1250px");


            }
            $(".HAtab").removeClass("active");
            $(ControlID).addClass("active");

            //$(ControlID).addClass("active");
            $("#Iframe1").attr("src", URI);
        }

        function GoURI(Control, UTI) {
            $(".HAtab").removeClass("active");
            $(Control).addClass("active");

            //$(ControlID).addClass("active");
            $("#Iframe1").attr("src", UTI);
        }
    </script>
    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li class="HAtab active" id="DefaultTab" onclick="Settabs(this,1,'QuotedPriceList');"><a data-toggle="tab" href="#111">制作\变更报价单</a></li>
                    <li class="HAtab" onclick="Settabs(this,2,'QuotedPricefileManager');"><a data-toggle="tab" href="#111" style="display: none;">提案</a></li>
                    <li class="HAtab" onclick="Settabs(this,3,'QuotedPriceChecking');"><a data-toggle="tab" href="#111" style="display: none;">审核中</a></li>

                    <li class="HAtab" onclick="Settabs(this,6,'QuotedPriceFinish');"><a data-toggle="tab" href="#111">待确认/打印报价单</a></li>
                    <li class="HAtab" onclick="Settabs(this,6,'QuotedDispatchingList');"><a data-toggle="tab" href="#111">制作执行明细</a></li>
                    <li class="HAtab" id="Li1" onclick="Settabs(this,6,'QuotedPriceFinishByEmployee');"><a data-toggle="tab" href="#111">已完成订单</a></li>
                    <li class="HAtab" id="QuotedPriceallList" onclick="Settabs(this,6,'QuotedCollectionsPlan');"><a data-toggle="tab" href="#111">订单明细</a></li>
                    <li class="HAtab" onclick="Settabs(this,6,'QuotedCollectionList');"><a data-toggle="tab" href="#111">收款明细</a></li>
                    <li class="HAtab" onclick="GoURI(this,'/AdminPanlWorkArea/Carrytask/CarrytaskAppraise.aspx?NeedPopu=1');"><a data-toggle="tab" href="#111">订单执行评价</a></li>
                    <li class="HAtab" onclick="GoURI(this,'/AdminPanlWorkArea/CS/CS_DegreeOfSatisfactionList.aspx?Typer=3&NeedPopu=1');"><a data-toggle="tab" href="#111">满意度结果</a></li>
                    <li  style="display: none;" class="HAtab" onclick="GoURI(this,'/AdminPanlWorkArea/Flows/Customer/ReturnVisit/FL_CustomerReturnVisitList.aspx?Typer=3&NeedPopu=1');"><a data-toggle="tab" href="#111">回访结果</a></li>
                </ul>
            </div>
            <div class="widget-content tab-content">
                <div class="tab-pane active" id="tab1">
                    <iframe class="framchild " id="Iframe1" name="main" scrolling="no" noresize width="100%" height="1250px" frameborder="0" name="table" src="/AdminPanlWorkArea/QuotedPrice/QuotedPriceList.aspx?NeedPopu=1&SaleEmployee=<%=Request["SaleEmployee"]%>"></iframe>
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
