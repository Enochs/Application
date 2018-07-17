<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderAppraise.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.OrderAppraise" Title="项目评价" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<%@ Register Src="../Control/CustomerDetails.ascx" TagName="CustomerDetails" TagPrefix="uc1" %>
<%@ Register Src="../Control/CustomerTitle.ascx" TagName="CustomerTitle" TagPrefix="uc2" %>
<%@ Register Src="../Control/CarrytaskCustomerTitle.ascx" TagName="CarrytaskCustomerTitle" TagPrefix="uc3" %>
<asp:Content ContentPlaceHolderID="head" ID="Content2" runat="server">
    <script type="text/javascript">
        function NeedLock()
        {
            if (confirm("确认后将无法再编辑评价！")) {
                return true;

            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1" ID="Content1">
    <div style="overflow-x: auto;overflow-y:auto;height:1200px;">
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
                        <th>扣款说明</th>
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
                                    <asp:DropDownList ID="ddlPoint" runat="server">
                                        <asp:ListItem Text="很好" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="较好" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="一般" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="不好" Value="1"></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="ddErro" runat="server">
                                        <asp:ListItem Text="无" Value="0" ></asp:ListItem>
                                        <asp:ListItem Text="有" Value="1"></asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txtRemark" runat="server" MaxLength="20"  Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                                <td>
                                    <asp:TextBox ID="txtSuggest" runat="server" MaxLength="20" Text='<%#Eval("Suggest") %>'></asp:TextBox></td>
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
                          <th>扣款说明</th>
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
                                      <asp:DropDownList ID="ddlPoint" runat="server">
                                          <asp:ListItem Text="很好" Value="4"></asp:ListItem>
                                          <asp:ListItem Text="较好" Value="3"></asp:ListItem>
                                          <asp:ListItem Text="一般" Value="2"></asp:ListItem>
                                          <asp:ListItem Text="不好" Value="1"></asp:ListItem>
                                      </asp:DropDownList></td>
                                  <td>
                                      <asp:DropDownList ID="ddErro" runat="server">
                                          <asp:ListItem Text="无" Value="0"></asp:ListItem>
                                          <asp:ListItem Text="有" Value="1"></asp:ListItem>
                                      </asp:DropDownList></td>
                                  <td>
                                      <asp:TextBox ID="txtRemark" runat="server" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                                  <td>
                                      <asp:TextBox ID="txtSuggest" runat="server" Text='<%#Eval("Suggest") %>'></asp:TextBox></td>
                              </tr>
                          </ItemTemplate>

                      </asp:Repeater>
                  </tbody>
                  <tfoot>
                      <tr>
                          <td>
                              <asp:Button ID="Button1"  runat="server" Text="保存" OnClick="btnSaveChange_Click"  CssClass="btn btn-success" Visible="false" />
                              <asp:Button ID="btnLock" runat="server" Text="保存" OnClick="btnLock_Click" CssClass="btn btn-info" OnClientClick="return NeedLock();" />
                          </td>
                      </tr>
                  </tfoot>
              </table>
        </div>
    </div>
</asp:Content>
