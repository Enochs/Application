<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectCategoryProduct.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.SelectCategoryProduct" %>
<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<div id="SelectEmployee" class="SelectEmpLoyee" style="width: 900px;"> 
<table style="width:450px;" border="0" cellpadding="0" cellspacing="1" bgcolor="#666666">
        <tr>
            <td>
                <table  style="width:450px;" border="1" cellpadding="5" cellspacing="1" bgcolor="#FFFFFF">
                      <tr>
                        <td colspan="3">
                           
                            <cc1:ClickOnceButton ID="Button1" runat="server" Text="确认"  CssClass="btn  btn-primary" Style="width: 78px" OnClick="btnSaveSelect_Click"  />    

                        </td>
                    </tr>
                    <tr>
                        <td style="width: 38%;">
                            <asp:TreeView ID="treeCategory" runat="server" Width="100%" OnSelectedNodeChanged="treeCategory_SelectedNodeChanged" >
  
                            </asp:TreeView>
                        </td>
                        <td style="width:50px;">
                         >>></td>
                        <td style="vertical-align:top;">
                            <table border="0" cellpadding="1" cellspacing="1" bgcolor="#CCCCCC">
                                <tr>
                                    <td>
                                        <table width="0px;" border="1" cellpadding="5" cellspacing="1">
                                            <asp:Repeater ID="rptProjectList" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="width:30px;"   bgcolor="#FFFFEE">
                                                     
                                                       
                                                            <input type="radio" name="rdo" id="rdoProject" value='<%#Eval("CategoryID") %>'     ref='<%#Eval("CategoryName") %>'/>
                                                        </td>
                                                        <td style="width:100px;"  bgcolor="#FFFFEE"><%#Eval("CategoryName") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                             
                            <cc1:ClickOnceButton ID="btnSaveSelect" runat="server" Text="确认"  CssClass="btn btn-primary"  Style="width: 78px" OnClick="btnSaveSelect_Click" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    
 
  
</div>
