<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DispatchingManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.DispatchingManager" Title="派工总表" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>


<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        //$(function () {
        //    //../AdminPanlWorkArea/Foundation/FD_PPTWarehouse/FD_PPTWareHouseCreate.aspx
        //    $('#tabs').tabs();
        //});

        var ActiveControlClass = "";
        var OldActiveControl;

        $(document).ready(function () {
            $("#Tabitle0").click();
        });

        function SetActive(Control, ItemIndex) {
            if (ActiveControlClass != "") {
                $(OldActiveControl).removeClass();
                $(OldActiveControl).addClass(ActiveControlClass);
            }
            ActiveControlClass = $(Control).attr("class");
            OldActiveControl = Control;
            $(Control).removeClass("btn-success");
            $(Control).removeClass("btn-primary");
            $(Control).addClass("btn-warning");
            GoURI(ItemIndex);
        }

        //
        function GoURI(ItemIndex) {
            var URI = $("#FrmUrL" + ItemIndex).attr("URI");
            $("#ContentFram").attr("src", URI);

        }

        $(function () {
            $("#dialog").dialog({
                autoOpen: false,
                modal: true,
                show: {
                    effect: "blind",
                    duration: 100
                },
                buttons: {
                    "确定": function () {
                        ClickIframControl();
                        $(this).dialog("close");
                    },
                    "取消": function () {
                        $(this).dialog("close");
                    }
                }
            });

            $("#opener").click(function () {
                $("#dialog").dialog("open");
            });
        });


        function ClickIframControl() {
            var ControlList = $(".framchild").contents().find("#btnSaveChange");
            for (var i = 0; i < ControlList.length; i++) {

                $(ControlList[i]).click().delay(i * 5000);

            }
            return false;

        }
    </script>
    <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />

    <asp:Button ID="btnSaveByMine" runat="server" Text="全部派给自己" CssClass="btn btn-danger" OnClick="btnSaveByMine_Click" />

    <div class="row-fluid">
        <div class="widget-box">

            <div class="widget-title">

                <asp:Repeater ID="reptabstitle" runat="server">
                    <ItemTemplate>
                        <a id="Tabitle<%#Container.ItemIndex %>" onclick="SetActive(this,'<%#Container.ItemIndex %>');" class="btn btn-success tabbtn"><%#Eval("CategoryName") %></a>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <a id="Tabitle<%#Container.ItemIndex %>" onclick="SetActive(this,'<%#Container.ItemIndex %>');" class="btn btn-success tabbtn"><%#Eval("CategoryName") %></a>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="widget-content tab-content">
            <div class="tab-pane active" id="tab1">
                <iframe id="ContentFram" class="framchild " width="100%" style="min-height: 900px" frameborder="0" name="table"></iframe>
            </div>
        </div>
        <ul style="display: none;">
            <li id='FrmUrLD' uri='CarrytaskWeddingPlanningCreate.aspx?OrderID=<%=Request["OrderID"] %>'></li>
            <asp:Repeater ID="reptabContent" runat="server">
                <ItemTemplate>
                    <li id='FrmUrL<%#Container.ItemIndex %>' uri='CarrytaskCreate.aspx?StateKey=<%#Request["StateKey"] %>&NeedPopu=1&DispatchingID=<%#Eval("DispatchingID") %>&CategoryID=<%#Eval("CategoryID") %>&OrderID=<%=Request["OrderID"] %>&CustomerID=<%=Request["CustomerID"] %>'></li>
                </ItemTemplate>
            </asp:Repeater>

        </ul>
    </div>
</asp:Content>

