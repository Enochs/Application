<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .btn_Save:hover {
            background-color: #0963c4;
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" CssClass="btn_Save" ID="btn_Save" Text="保存" Style="border: 1px solid gray; cursor: pointer; background-color: #2c8dce; color: white;" OnClick="btn_Save_Click" />
            <asp:Button runat="server" CssClass="btn_Save" ID="Button1" Text="保存满意度" Style="border: 1px solid gray; cursor: pointer; background-color: #2c8dce; color: white;" OnClick="Button1_Click" />
            <asp:Label runat="server" ID="lblSums" Text="" Width="400px" />
            <div>
                <asp:Repeater ID="repContent" runat="server">
                    <ItemTemplate>
                        <input id="chkContent" name="chkContent" runat="server" type="radio" value='<%#Eval("GuardianId") %>' onclientclick="return SetSelectEmpLoyee();" /><%#Eval("GuardianName") %>
                    </ItemTemplate>
                </asp:Repeater>
                 <asp:Button ID="btnSaveSelect" runat="server" Text="确认" OnClientClick="return SetSelectEmpLoyee();" CssClass="btn btn-success" />
                <asp:RadioButtonList runat="server" ID="rbtnContent" CssClass="rbtn_" ClientIDMode="Static" DataTextField="GuardianName" DataValueField="GuardianId" ></asp:RadioButtonList>
            </div>
            <input type="radio" name="ChkTest" value="测试1" />测试1
            <input type="radio" name="ChkTest" value="测试2" />测试2
        </div>

        <script type="text/javascript">
            function SetSelectEmpLoyee() {
                alert("Message");
                var CheckRadioVal = $('input:radio:checked').val();
                alert(CheckRadioVal);
                var EmpLoyeeName = $('input:radio:checked').attr("ref");
                alert(EmpLoyeeName);
                var ParentKey = $("#hideParentKey").val();
                var ControlKey = "#" + $("#hideControlKey").val();
                var SetEmployeeName = $("#hideSetEmployeeName").val();
                alert(CheckRadioVal+"--"+EmpLoyeeName);
                if (ParentKey != "") {
                    parent.$("#" + ParentKey).children(ControlKey).attr("value", CheckRadioVal);
                    parent.$("#" + ParentKey).find("." + SetEmployeeName).attr("value", EmpLoyeeName);
                    
                } else {
                    parent.$("#" + ControlKey).attr("value", CheckRadioVal);
                    parent.$("#" + SetEmployeeName).attr("value", EmpLoyeeName);
                }
                alert(CheckRadioVal + "--" + EmpLoyeeName);
                if (typeof (CheckRadioVal) != "undefined") {
                    //alert(typeof (CheckRadioVal));
                    parent.$("#<%=Request["Callback"]%>").click();

                    parent.$.fancybox.close(1);
                } else {
                    alert("请选择四大金刚！");
                    return false;
                }
                return false;
            }

            function ShowSelectEmployeeWindows() {
                $("a#showSelectEmployee").fancybox();
            }

            //$("#a标签的ID").fancybox({
            //    'titlePosition': 'inside',
            //    'transitionIn': 'none',
            //    'transitionOut': 'none'
            //}).trigger('click');

            //function ShowSelectEmpLoyee() {
            //    alert();
            //    $("#SelectEmployee").fancybox({
            //        'showCloseButton': false,
            //        'titlePosition': 'inside',
            //        'titleFormat': formatTitle
            //    });
            //    return false;
            //}

            function SetSelectEmpLoyees() {
                var CheckRadioVal = $('input:radio:checked').val();
                var EmpLoyeeName = $('input:radio:checked').attr("ref");
                var ParentKey = $("#hideParentKey").val();
                var ControlKey = "#" + $("#hideControlKey").val();
                var SetEmployeeName = $("#hideSetEmployeeName").val();
                if (ParentKey != "") {
                    parent.$("#" + ParentKey).children(ControlKey).attr("value", CheckRadioVal);
                    parent.$("#" + ParentKey).find("." + SetEmployeeName).attr("value", EmpLoyeeName);

                } else {
                    parent.$("#" + ControlKey).attr("value", CheckRadioVal);
                    parent.$("#" + SetEmployeeName).attr("value", EmpLoyeeName);
                }
                alert(CheckRadioVal + "--" + EmpLoyeeName);
                if (typeof (CheckRadioVal) != "undefined") {
                    //alert(typeof (CheckRadioVal));
                    parent.$("#<%=Request["Callback"]%>").click();

                    parent.$.fancybox.close(1);
                } else {
                    alert("请选择四大金刚！");
                    return false;
                }
                return false;
            }

        </script>
    </form>
</body>
</html>
