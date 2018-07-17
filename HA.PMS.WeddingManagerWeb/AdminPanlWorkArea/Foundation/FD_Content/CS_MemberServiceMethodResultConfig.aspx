<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="CS_MemberServiceMethodResultConfig.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.CS_MemberServiceMethodResultConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(window).load(function () {
      
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');


            $("#chkIsSp").click(function () {
            
                var IsCheck = $("#chkIsSp").is(':checked');
                if (IsCheck)
                {
                    $(".Sp").show();
                } else
                {
                
                    $(".Sp").hide();
                }

            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtNewName.ClientID%>');
            BindText(400, '<%=txtTemplete.ClientID%>');
            $("input").each(function () {
                var ctrl = $(this);
                ctrl.attr("orival", ctrl.val());
            }).attr("reg", "^.{1,20}$");
        }
        function CheckSuccess(ctrl) {
            return ValidateForm('input[check],textarea[check]');
        }
        function ValidateThis(ctrl) {
            var valc = $(ctrl).parent("td").prev("td").children("input");
            if (valc.val() == '') {
                valc.val(valc.attr("orival"));
                return false;
            }
            else {
                return true;
            }
        }


        function EditTemplate(Key)
        {
 
            var Url = "/AdminPanlWorkArea/Foundation/FD_Content/CS_MemberServiceMethodUpdate.aspx?Key=" + Key;
                showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
                $("#SelectEmpLoyeeBythis").click();
      
        }

        function SetTemplate()
        {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>会员服务方式</h5>

        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped" style="width: 500px;">
                <thead>
                    <tr>
                        <th>名称</th>
                        <th>操作</th>

                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptDegree" OnItemCommand="rptDegree_ItemCommand" runat="server">
                        <ItemTemplate>

                            <tr>
                                <td>
                                    <asp:TextBox ID="txtName" MaxLength="20" Text='<%#Eval("ServiceName") %>' Width="130" runat="server"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lkbtnSave" OnClientClick="return ValidateThis(this);" CommandName="Edit" CssClass="btn btn-primary btn-mini" CommandArgument='<%#Eval("ServiceId") %>' runat="server">保存</asp:LinkButton>
                                    <a href="#" onclick="EditTemplate(<%#Eval("ServiceId") %>)" <%#Eval("IsSP").ToString()=="True"?"":"style='display:none;'" %> class="btn">修改短信模版</a>
                                </td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>

                </tbody>
                <tr>
                    <td style="white-space:nowrap;">服务方式：<br />
                        <asp:TextBox ID="txtNewName" check="1" tip="限20个字符！" Width="130" runat="server"></asp:TextBox>
                        <span style="color:red">*</span>
                       </td>
                    <td>

                        <asp:CheckBox ID="chkIsSp" runat="server"  ClientIDMode="Static" />是否为Sp短信类型

                    </td>

                </tr>
                <tr class="Sp" style="display:none;">
                    <td   style="white-space:nowrap;">标签&amp;Name&amp;为新人姓名 </td>
                    <td>
                         &nbsp;</td>

                </tr>
                <tr class="Sp" style="display:none;">
                    <td colspan="2">模板：<br />
                        <asp:TextBox ID="txtTemplete" MaxLength="400" check="0" tip="限400个字符！" Width="100%" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>

                    </td>

                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" OnClientClick="return CheckSuccess(this);" OnClick="btnSave_Click" runat="server" Text="保存" CssClass="btn btn-success" />

                    </td>

                </tr>
            </table>

        </div>
    </div>
</asp:Content>
