<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FL_ReturnVisitMessageShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.ReturnVisit.FL_ReturnVisitMessageShow" %>
 <%@ Register Src="../../../Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="widget-box">
        <div class="widget-content">
            <uc1:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />
            <table   class="table table-bordered table-striped">
                <tr>
                    <td style="text-align: left" ><a href="FL_CustomerReturnVisitManagerNot.aspx?NeedPopu=1">返回列表</a></td>


                    <td style="text-align: left" >&nbsp;</td>
                    <td style="text-align: left" >&nbsp;</td>
                    <td style="text-align: left" >回访时间<asp:Label ID="lblReturnDate" runat="server" Text=""></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="text-align: left" >回访问题</td>


                    <td style="text-align: left" >结果</td>
                    <td style="text-align: left" >备注</td>
                    <td style="text-align: left" >反馈</td>

                </tr>

                <asp:Repeater ID="repItemList" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: left">
                                <%#Eval("StateItem") %>
                                </td>


                            <td style="text-align: left">
                             <%#Eval("Source") %>
                            </td>
                            <td style="text-align: left">
                                 <%#Eval("SourceNode") %>
                            </td>
                            <td style="text-align: left">
                                 <%#Eval("ReturnSource") %>
                            </td>

                        </tr>
                    </ItemTemplate>

                </asp:Repeater>


                <tr>
                    <td style="text-align: left" colspan="4">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
