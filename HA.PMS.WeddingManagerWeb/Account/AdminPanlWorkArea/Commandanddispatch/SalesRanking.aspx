<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesRanking.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.SalesRanking" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .et2 {
            color: #000000;
            font-size: 11.0pt;
            font-family: 宋体;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            text-align: general;
            vertical-align: middle;
        }

        .et3 {
            color: #000000;
            font-size: 11.0pt;
            font-family: 宋体;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            text-align: general;
            vertical-align: middle;
        }

        .auto-style1 {
            color: #000000;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            text-align: general;
            vertical-align: middle;
            height: 14.25pt;
            width: 102.75pt;
            font-family: 宋体;
            border: .5pt solid #000000;
        }

        .auto-style2 {
            color: #000000;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            text-align: general;
            vertical-align: middle;
            height: 14.25pt;
            width: 54.00pt;
            font-family: 宋体;
            border: .5pt solid #000000;
            background: #FFFF00;
        }

        .auto-style3 {
            color: #000000;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            text-align: general;
            vertical-align: middle;
            height: 14.25pt;
            width: 54.00pt;
            font-family: 宋体;
            border: .5pt solid #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 
    <asp:HiddenField ID="hideKey" runat="server" />
    <table height="342" style="border-collapse: collapse; width: 534.75pt;" width="713">
        <colgroup>
            <col width="72" />
            <col width="72" />
            <col width="72" />
            <col width="72" />
            <col width="72" />
            <col width="72" />
            <col width="72" />
            <col width="72" />
        </colgroup>
        <tr height="19">
            <td class="auto-style1" height="19" style="mso-protection: locked visible;" width="137" x:str="">名次</td>
            <td class="auto-style2" height="19" style="mso-pattern: none none; mso-protection: locked visible;" width="72">员工</td>
            <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72" x:str="">客户数量</td>
            <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72" x:str="">销售额</td>
            <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72" x:str="">完工额</td>
            <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72" x:str="">平均消费</td>
            <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72" x:str="">现金流</td>
            <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72" x:str="">毛利率</td>
            <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72" x:str="">满意度</td>
        </tr>
        <asp:Repeater ID="repRanking" runat="server">
            <ItemTemplate>
                <tr height="19">
                    <td class="auto-style1" height="19" style="mso-protection: locked visible;" width="137" x:num="1"><%#Container.ItemIndex+1 %></td>
                    <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72"><%#GetEmployeeName(Eval("EmployeeID")) %></td>
                    <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72"><%#GetCustomerCount(Eval("EmployeeID")) %></td>
                    <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72"><%#Eval("FinishSum") %></td>
                    <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72"><%#Eval("OverYearFinishSum") %></td>
                    <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72"><%#GetCustomerCountAvg( Eval("FinishSum"), Eval("EmployeeID")) %></td>
                    <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72"><%#Eval("FinishSum") %></td>
                    <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72">80%</td>
                    <td class="auto-style3" height="19" style="mso-protection: locked visible;" width="72">100%</td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>



</asp:Content>
