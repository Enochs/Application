<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="CS_ComplainDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.CS_ComplainDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function() {
            $("html,body").css({ "width": "500px", "height": "400px" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     
   <div class="widget-box">
        
       <div class="widget-content">
           <table class="table table-bordered table-striped">
               <tr>
                   <td>投诉客户</td>
                   <td>
                       <asp:Literal ID="ltlCustomerID" runat="server"></asp:Literal>
                   </td>
               </tr>
               <tr>
                   <td>投诉内容</td>
                   <td>
                       <asp:Literal ID="ltlComplainContent" runat="server"></asp:Literal></td>
               </tr>
               <tr>
                   <td>投诉时间</td>
                   <td>
                       <asp:Literal ID="ltlComplainDate" runat="server"></asp:Literal>
                   </td>
               </tr>
               <tr>
                   <td>处理人</td>
                   <td>
                       <asp:Literal ID="ltlEmployee" runat="server"></asp:Literal>
                   </td>
               </tr>
               <tr>
                   <td>处理意见</td>
                   <td>
                       <asp:Literal ID="ltlComplainRemark" runat="server"></asp:Literal>
                   </td>
               </tr>
               <tr>
                   <td>处理结果</td>
                   <td>
                       <asp:Literal ID="ltlReturnContent" runat="server"></asp:Literal>
                   </td>
               </tr>
               <tr>
                   <td>处理时间</td>
                   <td>
                       <asp:Literal ID="ltlReturnDate" runat="server"></asp:Literal>
                   </td>
               </tr>


           </table>
       </div>
   </div>
</asp:Content>
