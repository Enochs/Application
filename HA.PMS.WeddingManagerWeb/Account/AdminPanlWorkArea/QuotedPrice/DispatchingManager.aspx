<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DispatchingManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.DispatchingManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagPrefix="HA" TagName="CarrytaskCustomerTitle" %>


<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">


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

        $(function () {
            //../AdminPanlWorkArea/Foundation/FD_PPTWarehouse/FD_PPTWareHouseCreate.aspx
            $('#tabs').tabs();
        });

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
                        return false;
                    }
                }
            });

            $("#btnSaveUp").click(function () {
                $("#dialog").dialog("open");
                return false;
            });

            $("#btnSaveChange").click(function () {
                $("#dialog").dialog("open");
                return false;
            });

            $("#dialogLoading").dialog({
                autoOpen: false,
                modal: true,
                show: {
                    effect: "blind",
                    duration: 100
                }
            });

        });

        function GoURI(ItemIndex) {
            var URI = $("#FrmUrL" + ItemIndex).attr("URI");
            if ($("#hideIsDis").val() == "1") {
                URI = URI + "&Dis=True";
            }
            $("#ContentFram").attr("src", URI);

        }

        function ClickIframControl() {
            $("#btnSavessss").click();


            return true;

        }
        ///保存责任人
        function SaveChaneges() {
            $("#dialogLoading").dialog("close");


        }

        //弹出选择人员
        $(document).ready(function () {
            $("#Tabitle0").click();
            $(".txtEmpLoyeeName").click(function () {
           
                    ShowPopu(this);
             
            }
            )
        })


        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hiddeEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 400, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //function ShowPopu() {
        //    var Url = "/AdminPanlWorkArea/ControlPage/SelectEmployee.aspx?ControlKey=hiddeEmpLoyeeID&ALL=1";
        //    showPopuWindows(Url, 700, 700, "#SelectEmpLoyeeBythis");
        //    $("#SelectEmpLoyeeBythis").click();
        //}
    </script>

</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">选择</a>
    <HA:CarrytaskCustomerTitle runat="server" ID="CarrytaskCustomerTitle" />
    <div class="row-fluid">
        <div class="widget-box">
            <div class="widget-title" style="height: 75px;">
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
       <div style="width:100%;color:red;">
           说明：完善类别后,请一定去左下角选择总派工人！
       </div>
        <div class="widget-content tab-content">
            <div class="tab-pane active" id="tab1">
                <%--<iframe id="ContentFram" name="table" class="framchild" width="1500px" height="900px" src="/AdminPanlWorkArea/Carrytask/CarrytaskWeddingPlanningCreate.aspx?OrderID=<%=Request["OrderID"] %>"></iframe>--%>
                <iframe id="ContentFram" name="table" class="framchild" width="100%" height="600" src="/AdminPanlWorkArea/Carrytask/CarrytaskWeddingPlanningCreate.aspx?OrderID=<%=Request["OrderID"] %>&CustomerID=<%=Request["CustomerID"] %>"></iframe>
            </div>
        </div>
        <ul style="display: none;">
            <li id='FrmUrLD' uri='/AdminPanlWorkArea/Carrytask/CarrytaskWeddingPlanningCreate.aspx?OrderID=<%=Request["OrderID"] %>&CustomerID=<%=Request["CustomerID"] %>'></li>
            <asp:Repeater ID="reptabContent" runat="server">
                <ItemTemplate>
                    <li id='FrmUrL<%#Container.ItemIndex %>' uri='CreateDispatching.aspx?CustomerID=<%=Request["CustomerID"] %>&QuotedID=<%#Eval("QuotedID") %>&CategoryID=<%#Eval("CategoryID") %>&CelebrationID=<%#Eval("CelebrationID") %>'></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
    <div id="DivState">
        选择派工人
        <asp:HiddenField runat="server" ClientIDMode="Static" ID="hiddeEmpLoyeeID" />
        <input runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" type="text"  />
        <asp:HiddenField ID="hideIsDis" ClientIDMode="Static" runat="server" />
    </div>
    <div id="dialog" title="警示信息">
        你填写的工作内容将作为总派工人的派工依据，总派工人接到该项内容后，一旦派工，相关人员将按此执行，如该项内容不准确，将巨大的麻烦和损失
    </div>
    <div style="display: none;">
        <asp:Button ID="btnSavessss" runat="server" Text="Button" ClientIDMode="Static" OnClick="btnSaveChange_Click" />
    </div>
    <asp:Button ID="btnSaveChange" ClientIDMode="Static" runat="server" Text="派工保存" OnClick="btnSaveChange_Click" CssClass="btn" Visible="false" BackColor="Red" Width="100" Height="75"/>

</asp:Content>
