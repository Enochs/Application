<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="Sys_WeddingPlanningConfig.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content.Sys_WeddingPlanningConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/trselection.js"></script>
 
      <script type="text/javascript">

          function ShowUpdateWindows(KeyID, Control) {
              var Url = "Sys_WeddingUpdate.aspx?CategoryId=" + KeyID;
              $(Control).attr("id", "updateShow" + KeyID);
              showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));
          }
          function ShowParentUpdateWindows(KeyID, Control) {
              var Url = "Sys_WeddingParentUpdate.aspx?CategoryId=" + KeyID;
              $(Control).attr("id", "updateShow" + KeyID);
              showPopuWindows(Url, 500, 1000, "a#" + $(Control).attr("id"));
          }

          function ShowCreateChildWindows(KeyID, Control) {
              var Url = "Sys_PlanningCreateChild.aspx?CategoryId=" + KeyID;
              $(Control).attr("id", "CreateChildShow" + KeyID);
              showPopuWindows(Url, 600, 1000, "a#" + $(Control).attr("id"));
          }

          $(document).ready(function () {
              
              showPopuWindows($("#createCategory").attr("href"), 500, 1000, "a#createCategory");

          });
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <a href="Sys_PlanningCreate.aspx" class="btn btn-primary  btn-mini" id="createCategory">创建统筹类别</a>
    <div class="widget-box">

        <div class="widget-content ">
            <table class="table table-bordered table-striped table-select" style="width:600px">
                <thead>
                    <tr>
                        <th>婚礼统筹类别</th>
                        <th>操作</th>
                        
                    </tr>
                </thead>
                <tbody>
                      <asp:Repeater ID="rptCategory" runat="server" OnItemDataBound="rptCategory_ItemDataBound" OnItemCommand="rptCategory_ItemCommand">

                        <ItemTemplate>

                            <tr skey='<%#Eval("PlanningID") %>'>
                                <td style="width:400px; white-space:nowrap;"><%#Eval("PlanningName") %></td>
                                <td style="white-space:nowrap;"> 

                                    <a href="#" class="btn btn-primary  btn-mini" onclick='ShowParentUpdateWindows(<%#Eval("PlanningID") %>,this);'>修改</a>

                                    <asp:LinkButton ID="lkbtnDelete" style="display:none" CssClass="btn btn-danger btn-mini" CommandName="Delete" CommandArgument='<%#Eval("PlanningID") %>' runat="server">删除</asp:LinkButton>

                                    <asp:Literal ID="ltlCreateChild" runat="server"></asp:Literal>
                                </td>
                            </tr>
                            <asp:Repeater ID="rptChildCategory" OnItemDataBound="rptCategory_ItemDataBound" OnItemCommand="rptChildCategory_ItemCommand"  runat="server">
                                <ItemTemplate>

                                    <tr skey='<%#Eval("PlanningID") %>'>
                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Eval("PlanningName") %></td>
                                        <td>

                                            <a href="#" class="btn btn-primary  btn-mini" onclick='ShowUpdateWindows(<%#Eval("PlanningID") %>,this);'>修改</a>

                                            <asp:LinkButton ID="lkbtnDelete" style="display:none" CssClass="btn btn-danger btn-mini" CommandName="Delete" CommandArgument='<%#Eval("PlanningID") %>' runat="server">删除</asp:LinkButton>

                                            
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
