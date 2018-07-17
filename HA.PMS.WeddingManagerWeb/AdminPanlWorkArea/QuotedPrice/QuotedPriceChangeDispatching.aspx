<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotedPriceChangeDispatching.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceChangeDispatching" Title="变更派工" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">

        function Settabs(Keys,ChangeID) {
            Uri = "/AdminPanlWorkArea/QuotedPrice/" + Uri + ".aspx?NeedPopu=1&Key=" + Keys;
            $(".tab-pane").removeClass("active");
            $(ControlID).addClass("active");
            $("#Iframe" + ChangeID).attr("src", Uri);
        }
    </script>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <asp:Repeater ID="repSortListList" runat="server" OnItemDataBound="repSortListList_ItemDataBound">
        <ItemTemplate>
            <div class="row-fluid">
                <div class="widget-box">
                    <div class="widget-title">
                        <ul class="nav nav-tabs">
                            <asp:HiddenField ID="hideChangeKey" runat="server" Value='<%#Eval("QuotedID") %>' />
                            <asp:Repeater ID="repCatageList" runat="server">
                                <ItemTemplate>
                                    <li id="<%#Guid.NewGuid().ToString().Substring(0,6) %>DefaultTab<%#Container.ItemIndex %>" onclick="Settabs('<%#Eval("CategoryID") %>','<%#Eval("ChangeID") %>');"><a data-toggle="tab" href="#111"><%#Eval("CategoryName") %></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div class="widget-content tab-content">
                        <div class="tab-pane active" id="tab1">
                            <iframe class="framchild " id="Iframe<%#Eval("QuotedID") %>" width="100%" height="900px" frameborder="0" name="table" src="CreateDispatching.aspx?NeedPopu=1"></iframe>
                        </div>
                    </div>
                </div>
            </div>
            <br />
        </ItemTemplate>
    </asp:Repeater>
    <asp:Button ID="btnSaveChange" runat="server" Text="保存派工" OnClick="btnSaveChange_Click1" />
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
    <asp:Button ID="btnSubmit" runat="server" Text="提交到派工人" OnClick="btnSubmit_Click" />
</asp:Content>
