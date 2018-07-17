<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarrytaskAppraiseShow.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskAppraiseShow" Title="项目评价" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Src="../Control/CustomerDetails.ascx" TagName="CustomerDetails" TagPrefix="uc1" %>
<%@ Register Src="../Control/CustomerTitle.ascx" TagName="CustomerTitle" TagPrefix="uc2" %>
<%@ Register Src="../Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc3" %>
<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
    <script type="text/javascript">
        function NeedLock() {
            if (confirm("确认后将无法再编辑评价！")) {
                return true;

            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div style="overflow-x: auto;">
        <div class="widget-box">
            <table class="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th colspan="5">
                            <uc3:CarrytaskCustomerTitle ID="CarrytaskCustomerTitle1" runat="server" />

                        </th>
                    </tr>
                    <tr>
                        <th colspan="5" style="text-align: left;">专业团队：</th>
                    </tr>
                    <tr>
                        <th>对象</th>
                        <th>评价结果</th>
                        <th>有无差错</th>
                        <th>评价说明</th>
                        <th>建议改进</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repOrderAppraise" runat="server" OnItemDataBound="repOrderAppraise_ItemDataBound">
                        <ItemTemplate>

                            <tr>
                                <td><%#Eval("AppraiseTitle") %>
                                    <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("AppraiseID") %>' />

                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPoint" runat="server" Enabled="false">
                                        <asp:ListItem Text="很好" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="较好" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="一般" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="不好" Value="1"></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="ddErro" runat="server" Enabled="false">
                                        <asp:ListItem Text="有" Value="0" ></asp:ListItem>
                                        <asp:ListItem Text="无" Value="1"></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtRemark" runat="server"   Text='<%#Eval("Remark") %>' Enabled="false"></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtSuggest" runat="server" Text='<%#Eval("Suggest") %>' Enabled="false"></asp:TextBox></td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>
                </tbody>

            </table>

           
              <table class="table table-bordered table-striped">
                  <thead>
                       <tr>
                        <th colspan="5" style="text-align: left;"> 供应商：</th>
                    </tr>
                      <tr>
                          <th>对象</th>
                          <th>评价结果</th>
                          <th>有无差错</th>
                          <th>评价说明</th>
                          <th>建议改进</th>
                      </tr>
                      
                  </thead>
                  <tbody>
                      <asp:Repeater ID="repSupplily" runat="server" OnItemDataBound="repSupplily_ItemDataBound">
                          <ItemTemplate>

                              <tr>
                                  <td><%#Eval("AppraiseTitle") %>
                                      <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("AppraiseID") %>' />

                                  </td>
                                  <td>
                                      <asp:DropDownList ID="ddlPoint" runat="server" Enabled="false">
                                          <asp:ListItem Text="很好" Value="4"></asp:ListItem>
                                          <asp:ListItem Text="较好" Value="3"></asp:ListItem>
                                          <asp:ListItem Text="一般" Value="2"></asp:ListItem>
                                          <asp:ListItem Text="不好" Value="1"></asp:ListItem>
                                      </asp:DropDownList></td>
                                  <td>
                                      <asp:DropDownList ID="ddErro" runat="server" Enabled="false">
                                          <asp:ListItem Text="有" Value="0"></asp:ListItem>
                                          <asp:ListItem Text="无" Value="1"></asp:ListItem>
                                      </asp:DropDownList></td>
                                  <td>
                                      <asp:TextBox ID="txtRemark" runat="server" Text='<%#Eval("Remark") %>' Enabled="false"></asp:TextBox></td>
                                  <td>
                                      <asp:TextBox ID="txtSuggest" runat="server" Text='<%#Eval("Suggest") %>' Enabled="false"></asp:TextBox></td>
                              </tr>
                          </ItemTemplate>

                      </asp:Repeater>
                  </tbody>
 
              </table>
        </div>
    </div>
</asp:Content>
