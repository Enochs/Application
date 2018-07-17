<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderReferenceManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales.OrderReferenceManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        var DefaultURI = "/AdminPanlWorkArea/Flows/FL_MissionMananger.aspx";
        function Settabs(ControlID, Index, Uri, Ctrl) {
            $(".tab-pane").removeClass("active");
            $(ControlID).addClass("active");

            $(".HAtab").css("background-color", "rgb(128,128,128)").css("color", "white");
            $(Ctrl).css("background-color", "rgb(255,255,255)").css("color", "black");
        }
    </script>
    <!--Chart-box-->

    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title">
                <ul class="nav nav-tabs">
                    <li id="DefaultTab"><a data-toggle="tab" class="HAtab" href="#111" onclick="Settabs('#tab1',1,'DispatchingforEmployee',this);">建立信任</a></li>
                    <li><a data-toggle="tab" class="HAtab" href="#111" onclick="Settabs('#tab2',2,'CarrytaskOfNew',this);">找到燃烧点</a></li>
                    <li><a data-toggle="tab" class="HAtab" href="#111" onclick="Settabs('#tab3',3,'CarrytaskOfWeek',this);">优选</a></li>
                    <li><a data-toggle="tab" class="HAtab" href="#111" onclick="Settabs('#tab4',4,'CarrytaskShow',this);">确定</a></li>
                </ul>
            </div>
            <div class="widget-content tab-content">

                <div class="tab-pane active" id="tab1">
                    <cc1:CKEditorTool ID="txtContent1" runat="server"></cc1:CKEditorTool>
                </div>
                <div class="tab-pane" id="tab2">
                    <cc1:CKEditorTool ID="txtContent2" runat="server"></cc1:CKEditorTool>
                </div>
                <div class="tab-pane" id="tab3">
                    <cc1:CKEditorTool ID="txtContent3" runat="server"></cc1:CKEditorTool>
                </div>
                <div class="tab-pane" id="tab4">
                    <cc1:CKEditorTool ID="txtContent4" runat="server"></cc1:CKEditorTool>
                </div>



            </div>
        </div>
    </div>
    <asp:Button ID="btnCreate" runat="server" Text="保存修改" CssClass="btn btn-success" OnClick="btnCreate_Click" />
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
        <HA:MessageBoardforall runat="server" id="MessageBoardforall" />
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
