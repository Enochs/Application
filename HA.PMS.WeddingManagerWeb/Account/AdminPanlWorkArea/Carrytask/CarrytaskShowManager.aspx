<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskShowManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskShowManager" Title="执行明细表"  MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <script type="text/javascript">
        //$(function () {
        //    //../AdminPanlWorkArea/Foundation/FD_PPTWarehouse/FD_PPTWareHouseCreate.aspx
        //    $('#tabs').tabs();
        //});

        var ActiveControlClass = "";
        var OldActiveControl;

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
    <br />
    <br />
    <table style="width:90%;">
        <tr>
            <td style="white-space:nowrap;text-align:right;"><b>订单编号:</b></td>
            <td style="white-space:nowrap;text-align:left;">
                 &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCoder" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space:nowrap;text-align:right;"><b>品牌:</b></td>
            <td style="white-space:nowrap;text-align:left;">
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpinpai" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space:nowrap;text-align:right;"><b>订单类型:</b></td>

            <td style="white-space:nowrap;text-align:left;">
              &nbsp;&nbsp;&nbsp;&nbsp;套系
            </td>
            <td style="white-space:nowrap;text-align:right;"><b>风格:</b></td>
            <td style="white-space:nowrap;text-align:left;">
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTyper" runat="server" Text=""></asp:Label></td>
            <td style="white-space:nowrap;text-align:right;"><b>套系名称:</b></td>
            <td style="white-space:nowrap;text-align:left;">&nbsp;&nbsp;&nbsp;&nbsp;套系名称</td>

            <td style="white-space:nowrap;"> </td>

            <td style="white-space:nowrap;">
            </td>
        </tr>
        <tr>
            <td style="white-space:nowrap;text-align:right;"><b>新人姓名:</b></td>
            <td style="white-space:nowrap;text-align:left;">
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCustomerName" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space:nowrap;text-align:right;"><b>联系方式:</b></td>
            <td style="white-space:nowrap;text-align:left;">
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space:nowrap;text-align:right;"><b>酒店:</b></td>
            <td style="text-align:left;">
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblHotel" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space:nowrap;text-align:right;"><b>婚期:</b></td>
            <td style="text-align:left;">
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPartyDate" runat="server" Text=""></asp:Label>
            </td>
            <td style="white-space:nowrap;text-align:right;">
              <b>  时段:</b>
            </td>
            <td style="text-align:left;">
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTimerSpan" runat="server" Text="Label"></asp:Label>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>

    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title" style="height: 75px;">
                <a id="TabitleCarrytaskWeddingPlanningCreate" class="btn btn-primary tabbtn" href="#" onclick="SetActive(this,'D');">婚礼统筹</a>
                <asp:Repeater ID="reptabstitle" runat="server">
                    <ItemTemplate>
                        <a id="Tabitle<%#Container.ItemIndex %>" onclick="SetActive(this,'<%#Container.ItemIndex %>');" class="btn btn-success tabbtn"><%#Eval("CategoryName") %></a>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <a id="Tabitle<%#Container.ItemIndex %>" onclick="SetActive(this,'<%#Container.ItemIndex %>');" class="btn btn-primary tabbtn"><%#Eval("CategoryName") %></a>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        <div class="widget-content tab-content">
            <div class="tab-pane active" id="tab1">
                <iframe id="ContentFram" name="table" class="framchild" width="1500px" height="900px" src="CarrytaskWeddingPlanningCreate.aspx?<%#Request["OrderID"] %>"></iframe>
            </div>
        </div>
        <ul style="display: none;">
            <li id='FrmUrLD' URI='CarrytaskWeddingPlanningCreate.aspx?<%#Request["OrderID"] %>'></li>
            <asp:Repeater ID="reptabContent" runat="server">
                <ItemTemplate>
                    <li id='FrmUrL<%#Container.ItemIndex %>' URI='CarrytaskShow.aspx?NeedPopu=1&DispatchingID=<%#Eval("DispatchingID") %>&CategoryID=<%#Eval("CategoryID") %>'></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</asp:Content>

