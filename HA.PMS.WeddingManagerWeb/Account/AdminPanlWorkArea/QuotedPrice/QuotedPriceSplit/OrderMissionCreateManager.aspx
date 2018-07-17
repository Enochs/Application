<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderMissionCreateManager.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.QuotedPrice.QuotedPriceSplit.OrderMissionCreateManager" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc3" %>
<%@ Register Src="/AdminPanlWorkArea/Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc1" %>
<asp:Content runat="server" ContentPlaceHolderID="head" ID="Content2">
    <script type="text/javascript">
        $(window).load(function () {
            $("a#inline").fancybox();
            if ($("#hideIsDis").val() == "1") {
                $("[type='text']").attr("disabled", "disabled");
                $("[type='button']").attr("disabled", "disabled");
                $(".Requirement").attr("disabled", "disabled");
                $(".btndelete").hide();
                $(".SelectSG").hide();
                $(".SelectPG").hide();
            }
           // $(".SaveSubmit").click(function () {
           //     var ReturnValue = true;
           //
           //     $(".NoEmpty").each(function () {
           //
           //         if ($(this).val() == "") {
           //             alert("请完善填写婚礼统筹!");
           //             $(this).focus();
           //             return false;
           //         }
           //     });
           //
           //
           //     if ($("#txtPlanFinishDate").val() == "") {
           //         alert("请完善填写婚礼统筹!");
           //         $("#txtPlanFinishDate").focus();
           //         return false;
           //     }
           //
           //     return ReturnValue;
           // });


            if (window.screen.width >= 1280 && window.screen.width <= 1366) {
                $("#tblContent").css({ "width": "80%" });
            }
        });

        //查看附件
        function ShowFileShowPopu(PlanningID) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWeddingFilesList.aspx?PlanningID=" + PlanningID;
            showPopuWindows(Url, 700, 800, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        //上传附件
        function ShowFileUploadPopu(PlanningID, Kind) {
            var Url = "/AdminPanlWorkArea/Carrytask/CarrytaskWeddingFileUpload.aspx?PlanningID=" + PlanningID + "&Kind=" + Kind + "&Typer=6";
            showPopuWindows(Url, 720, 300, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id") + "&ALL=1";
            showPopuWindows(Url, 700, 700, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }

        function DeleteData() {
            if (confirm("确认删除！")) {
                return true;
            } else {
                return false;
            }
        }

        $(window).load(function () {
            BindText(50, '<%=txtServiceContent.ClientID%>:<%=txtRequirement.ClientID%>:<%=txtRemark.ClientID%>');
            BindString(10, '<%=txtEmpLoyee.ClientID%>');
            BindDate('<%=txtPlanFinishDate.ClientID%>');
            BindCtrlEvent('input[check],textarea[check],.msg');
            $("#<%=BtnSave.ClientID%>").click(function () {
                if (ValidateForm('input[check],textarea[check]'))
                {
                    if (parseInt($("#<%=hideEmpLoyeeID.ClientID%>").val()) > 0)
                    {
                        return true;
                    }
                    $(".empmust").click();
                }
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <a href="#" id="SelectEmpLoyeeBythis"></a>
    <uc1:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />
    <asp:HiddenField ID="hideIsDis" ClientIDMode="Static" runat="server" />
    <div style="overflow-x: auto; width: 1200px;">
        <table id="tblContent" style="text-align: center; width: 100%;" border="1" class="table table-bordered table-striped">
            <thead>
                <tr>
                    <th width="12">类别</th>
                    <th width="12">项目</th>
                    <th>任务内容</th>
                    <th>任务要求</th>
                    <th width="75">附件</th>
                    <th>责任人</th>
                    <th>备注</th>
                    <th>计划完成时间</th>
                    <th>操作</th>

                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:DropDownList ID="ddlCategory" Width="100" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCategoryItem" Width="100" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:TextBox ID="txtServiceContent" check="1" tip="限50个字符！" runat="server" Text='<%#Eval("ServiceContent") %>' MaxLength="50" Width="150" Rows="2" TextMode="MultiLine" CssClass="NoEmpty"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRequirement" check="1" tip="限50个字符！" runat="server" Text='<%#Eval("Requirement") %>' MaxLength="50" Width="150" Rows="2" TextMode="MultiLine" CssClass="NoEmpty"></asp:TextBox></td>
                    <td class="msg" tip="请先添加，再上传附件！" check="0">
                        <asp:FileUpload Visible="false" ID="FileImage" runat="server" Width="75" />
                    </td>
                    <td id="Partd<%=Guid.NewGuid() %>">
                        <input runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName empmust" maxlength="10" onclick="ShowPopu(this);" type="text" style="width: 65px;" value='' />

                        <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='0' runat="server" />

                    </td>
                    <td>
                        <asp:TextBox ID="txtRemark" Width="150" check="0" tip="限50个字符！" TextMode="MultiLine" runat="server" Text='<%#Eval("Remark") %>' CssClass="NoEmpty" MaxLength="50"></asp:TextBox></td>
                    <td>
                        <cc3:DateEditTextBox ID="txtPlanFinishDate" onclick="WdatePicker();" check="1" runat="server" MaxLength="20" Text='<%#Eval("PlanFinishDate") %>' Width="75px" ClientIDMode="Static"></cc3:DateEditTextBox></td>
                    <td>
                        <asp:Button Text="保存" ID="BtnSave" runat="server" Height="29" OnClick="btnSaveChange_Click" CssClass="btn btn-info SaveSubmit"/>
                        <%--<cc3:ClickOnceButton ID="ClickOnceButton1" runat="server" Text="保存" Height="29" OnClick="btnSaveChange_Click" CssClass="btn btn-info SaveSubmit" />--%>
                    </td>

                </tr>
                <asp:Repeater ID="repWeddingPlanning" runat="server" OnItemCommand="repWeddingPlanning_ItemCommand">

                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("Category") %></td>
                            <td>
                                <%#Eval("CategoryItem") %></td>
                            <td>
                                <asp:TextBox ID="txtServiceContent" MaxLength="50" runat="server" Text='<%#Eval("ServiceContent") %>' Width="150"></asp:TextBox></td>
                            <td>
                                <asp:TextBox ID="txtRequirement" MaxLength="50" runat="server" Text='<%#Eval("Requirement") %>' Width="150"></asp:TextBox></td>
                            <td>
                                <a href="#" onclick="ShowFileUploadPopu('<%#Eval("PlanningID") %>',0)" class="btn btn-mini">上传</a>
                                <a href='#' onclick="ShowFileShowPopu('<%#Eval("PlanningID") %>')" class="btn btn-mini">查看</a>
                                    <%--<a id="inline" href="#data<%#Eval("ChangeID") %>" class="btn btn-mini" kesrc="#data<%#Eval("ChangeID")%>" <%#HideforNoneImage(Eval("ChangeID")) %>>查看</a>--%>
                                    
<%--<a id="inline" href='#data<%#Eval("PlanningID") %>' kesrc='#data<%#Eval("PlanningID")%>'><img src='<%#Eval("ImageAddress") %>' style="height:35px;width:35px;" /></a>
                                        <div style="display: none;">
                                            <div id='data<%#Eval("PlanningID") %>'>
                                                <img src="<%#Eval("ImageAddress") %>" alt=""  />
                                            </div>
                                        </div>--%>
     
                        </td>
                            <td id="Partd<%#Container.ItemIndex %>">
                                <input runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowPopu(this);" type="text" style="width: 50px;" value='<%#GetEmployeeName(Eval("EmpLoyeeID")) %>' />
                                <a href="#" onclick="ShowPopu(this);" class="SetState btn btn-mini">派工</a>
                                <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='<%#Eval("EmpLoyeeID")%>' runat="server" />

                            </td>
                            <td>
                                <asp:TextBox ID="txtRemark" runat="server" Text='<%#Eval("Remark") %>' Width="150" MaxLength="50"></asp:TextBox></td>
                            <td>
                                <cc3:DateEditTextBox ID="txtPlanFinishDate" onclick="WdatePicker();" runat="server" Text='<%#GetShortDateString(Eval("PlanFinishDate")) %>' Width="75px"></cc3:DateEditTextBox>
                            </td>
                            <td style="white-space: nowrap;">
                                <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("PlanningID") %>' />
                                <asp:LinkButton ID="lnkbtnDelete" CssClass="btndelete btn btn-primary" CommandName="Delete" CommandArgument='<%#Eval("PlanningID") %>' runat="server" OnClientClick="return DeleteData();">删除</asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnSaveEdit" CssClass="btn btn-success" CommandName="Edit" CommandArgument='<%#Eval("PlanningID") %>' runat="server">保存</asp:LinkButton>
                            </td>

                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="9">

                      
                        <asp:Button ID="btnReturnList" runat="server" Text="返回" Height="29" OnClick="btnReturnList_Click" CssClass="btn btn-info" />
                        <cc1:AspNetPagerTool ID="WeddingPlanningPager" OnPageChanged="WeddingPlanningPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
                    </td>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>