<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CelebrationPackagePanel.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.Foundation.CelebrationPackagePanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
<div class="row-fluid" style="margin-top:0">
<div class="widget-box" style="margin-top:0">
<div class="widget-title">
<ul class="nav nav-tabs" id="Titletabs">
<li class="HAtab active" id="DefaultTab" AddressName="套系" onclick="Settabs(this,1,'FD_CelebrationPackageDetails2.aspx?NeedPopu=1&PackageType=0');"><a data-toggle="tab" href="#111" >套系</a></li>
<li class="HAtab" AddressName="专业团队" onclick="Settabs(this,2,' ProfessionalTeam.aspx');"><a data-toggle="tab" href="#111" >专业团队</a></li>
</ul>
</div>
<div class="widget-content tab-content">
<div class="tab-pane active" id="tab1">
<iframe class="framchild "name="main" scrolling="yes" noresize id="Iframe1" width="100%" height="900px" frameborder="0" style="" name="table" src="FD_CelebrationPackageDetails2.aspx?NeedPopu=1"></iframe>
</div>
</div>
</div>
</div>
<!--End-Chart-box-->
<!--end-Footer-->
<script src="/AdminPanlWorkArea/js/excanvas.min.js"></script>
<script src="/AdminPanlWorkArea/js/jquery.min.js"></script>
<script src="/AdminPanlWorkArea/js/jquery.ui.custom.js"></script>
<script src="/AdminPanlWorkArea/js/bootstrap.min.js"></script>
<script src="/AdminPanlWorkArea/js/jquery.flot.min.js"></script>
<script src="/AdminPanlWorkArea/js/jquery.flot.resize.min.js"></script>
<script src="/AdminPanlWorkArea/js/jquery.peity.min.js"></script>
<script src="/AdminPanlWorkArea/js/fullcalendar.min.js"></script>
<script src="/AdminPanlWorkArea/js/lijianwei.js"></script>
<script src="/AdminPanlWorkArea/js/lijianwei.dashboard.js"></script>
<script src="/AdminPanlWorkArea/js/jquery.gritter.min.js"></script>
<script src="/AdminPanlWorkArea/js/lijianwei.interface.js"></script>
<script src="/AdminPanlWorkArea/js/lijianwei.chat.js"></script>
<script src="/AdminPanlWorkArea/js/jquery.validate.js"></script>
<script src="/AdminPanlWorkArea/js/lijianwei.form_validation.js"></script>
<script src="/AdminPanlWorkArea/js/jquery.wizard.js"></script>
<script src="/AdminPanlWorkArea/js/jquery.uniform.js"></script>
<script src="/AdminPanlWorkArea/js/select2.min.js"></script>
<script src="/AdminPanlWorkArea/js/lijianwei.popover.js"></script>
<script src="/AdminPanlWorkArea/js/jquery.dataTables.min.js"></script>
<script src="/AdminPanlWorkArea/js/lijianwei.tables.js"></script>
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
