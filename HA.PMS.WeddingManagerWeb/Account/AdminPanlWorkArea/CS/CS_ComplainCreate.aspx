<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CS_ComplainCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_ComplainCreate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>

<%@ Register Src="~/AdminPanlWorkArea/Control/CustomerDetails.ascx" TagPrefix="uc1" TagName="CustomerDetails" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="uc1" TagName="MessageBoardforall" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     
    <script type="text/javascript">
        function ShowSerachCustomerWindows() {
            $("a#showsearchCustomers").fancybox({ width: 323, height: 252, topRatio: 0 });
        }
        
        function ShowFirstPopu() {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ControlKey=hiddeEmpLoyeeID&SetEmployeeName=vas&ParentControl=tdPerson&ALL=true";
            showPopuWindows(Url, 700, 300, "#SelectEmpLoyeeBythis");
            
           
      
        }
     

        $(document).ready(function () {
            //$("#txtComplainDate,#txtPartyDate").datepicker({ dateFormat: 'yy-mm-dd ' });


            $("#tblCustomer span").css({ "font-weight": "bold", "margin-right": "15px" })
            
            //$("#txtChoosePerson").hide();
            if ($("#hfCustomers").val()=="0") {
                $("a#showsearchCustomers").fancybox().trigger("click");

                $(".fancybox-overlay").appendTo($("#form1"));
            }
          

          
        });

        $(window).load(function () {
            BindCtrlRegex();
            BindCtrlEvent('input[check],textarea[check]');
            $("#<%=btnSave.ClientID%>").click(function () {
               return ValidateForm('input[check],textarea[check]');
            });
        });
        function BindCtrlRegex() {
            BindString(20, '<%=txtComplainContent.ClientID%>');
            BindDate('<%=txtComplainDate.ClientID%>');
            BindText(200, '<%=txtComplainRemark.ClientID%>');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:PlaceHolder ID="phContent" runat="server">
        <div class="widget-box">
            <div class="widget-content">
                <table class="table table-bordered table-striped">
                    <tr>
                        <td>投诉客户</td>
                        <td>
                            <br />
                            <div id="ShowDetails">
                                <table id="tblCustomer" class="table table-bordered table-striped">
                                    <tr>
                                        <td><span>姓名:</span>
                                            <asp:Literal ID="ltlName" runat="server"></asp:Literal>
                                        </td>
                                        <td>
                                            <span>电话:</span>
                                            <asp:Literal ID="ltlCelPhone" runat="server"></asp:Literal>
                                        </td>
                                        <td><span>婚期:</span>
                                            <asp:Literal ID="ltlPartyDate" runat="server"></asp:Literal></td>
                                        <td>
                                            <span>酒店:</span>
                                            <asp:Literal ID="ltlWineshop" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>

                                            <span>邀约人:</span>
                                            <asp:Literal ID="ltlInvitePerson" runat="server"></asp:Literal>
                                        </td>
                                        <td><span>婚礼顾问:</span>
                                            <asp:Literal ID="ltlAdviser" runat="server"></asp:Literal></td>
                                        <td><span>策划师:</span>
                                            <asp:Literal ID="ltlProgrammer" runat="server"></asp:Literal></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>

                    <tr id="tdContent">
                        <td><span style="color:red">*</span>投诉内容</td>
                        <td>
                            <asp:TextBox ID="txtComplainContent" check="1" tip="限20个字符！" MaxLength="20" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><span style="color:red">*</span>投诉时间</td>
                        <td>
                            <asp:TextBox ID="txtComplainDate" check="1" MaxLength="20" ClientIDMode="Static" onclick="WdatePicker();" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td><span style="color:red">*</span>处理要求</td>
                        <td>
                            <asp:TextBox ID="txtComplainRemark" check="1" tip="限200个字符！" runat="server" TextMode="MultiLine" Rows="3" Width="99%"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td><span style="color:red">*</span>选择处理人</td>
                        <td id="tdPerson">
                            <cc1:ddlOrderEmployee ID="ddlOrderEmployee" runat="server"></cc1:ddlOrderEmployee>
                        </td>
                    </tr>
                    

                    <asp:PlaceHolder ID="phSaveComplainContent" runat="server">
                    <tr>
                        <td>
                            <asp:Button ID="btnSave"   CssClass="btn btn-success" runat="server" Text="保存" OnClick="btnSave_Click" /></td>
                        <td></td>
                    </tr>

                    </asp:PlaceHolder>
                    <!--处理投诉提交-->
                    <asp:PlaceHolder ID="phExcuteComplainResult" Visible="false" runat="server" >
                        <tr>
                        <td>处理结果</td>
                        <td>
                            <asp:TextBox ID="txtReturnContent" CssClass="{required:true}"    runat="server" TextMode="MultiLine" Rows="3" Width="100%"></asp:TextBox>
                        </td>
                    </tr>

                        
                       <tr>
                        <td>
                            <asp:Button ID="btnExcute"   CssClass="btn btn-success" runat="server" Text="处理完毕" OnClick="btnExcute_Click" />

                        </td>
                        <td></td>
                    </tr>

                    </asp:PlaceHolder>

                </table>
            </div>
        </div>

      

    </asp:PlaceHolder>
      <asp:HiddenField ID="hfCustomers" ClientIDMode="Static" Value="0" runat="server" />
    <style>
        .centerTxt {
            width: 120px;
            height: 25px;
        }
    </style>


    <asp:PlaceHolder ID="phSearch" runat="server">
        <!--查询操作 start -->
        <a id="showsearchCustomers" href="#searchCustomers" style="display: none;" onclick="ShowSerachCustomerWindows();"><i class="icon-fullscreen"></i>搜索新人</a>
        <div id="searchCustomers" style="display: none; text-align: center;">
            <div style="width: 350px; height: 272px;">

                <div class="widget-box">
                    <div class="widget-title">
                        <span class="icon"><i class="icon-ok"></i></span>
                        <h5>查找新人</h5>
                    </div>
                    <div class="widget-content">
                        姓名:<asp:TextBox ID="txtCustomer" runat="server" CssClass="centerTxt"></asp:TextBox><br />
                        婚期:
                        <asp:TextBox ID="txtPartyDate" onclick="WdatePicker();" ClientIDMode="Static" runat="server"></asp:TextBox>
                        <br />
                        酒店:<asp:TextBox ID="txtWineShop" runat="server" CssClass="centerTxt"></asp:TextBox><br />
                        电话:<asp:TextBox ID="txtCelPhone" runat="server"></asp:TextBox>
                        <br />
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click"  CssClass="btn btn-success" Text="查找" runat="server"  />
                    </div>
                </div>


            </div>
        </div>

        <!-- 在搜索客户之后弹出框A标签-->
        <a href="#" style="display: none;" onclick="ShowShowSerachResult('#popuSerach');" id="popuSerach"></a>
    </asp:PlaceHolder>
    <uc1:MessageBoardforall runat="server" ID="MessageBoardforall" />
</asp:Content>
