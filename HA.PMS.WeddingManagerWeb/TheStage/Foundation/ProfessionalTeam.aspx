<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProfessionalTeam.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.Foundation.ProfessionalTeam" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
        <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 25px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }
    </style>
     <script type="text/javascript">

         $(document).ready(function () {
             $("#trContent th").css({ "white-space": "nowrap" });

             $(".queryTable").css("margin-left", "15px");//98    24
             $(".queryTable td").each(function (indexs, values) {


                 
                     $(this).css({ "border": "0", "vertical-align": "middle" }).after("&nbsp;&nbsp;&nbsp;&nbsp;");
             
                 if (indexs == $(".queryTable td").length - 1) {
                     $(this).css("vertical-align", "top");
                 }

             });


             $(":text").each(function (indexs, values) {
                 $(this).addClass("centerTxt");
             });
             $("select").addClass("centerSelect");

             $("html").css({ "overflow-x": "hidden" });
         });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>专业团队</h5>

        </div>
        <div class="widget-content">
             <table class="queryTable">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td> 
                                    <asp:PlaceHolder runat="server"  Visible="false" ID="phCV">
                                    品牌:<asp:DropDownList ID="ddlBrand" runat="server">
                                    <asp:ListItem Text="请选择" Value="0" />
                                    <asp:ListItem Text="精品奢华" Value="1" />
                                    <asp:ListItem Text="爱情甜美" Value="2" />
                                    <asp:ListItem Text="野性狂野" Value="3" />
                                </asp:DropDownList>
                             </asp:PlaceHolder> 


                                </td>
                                <td>人员类型:
                                     <asp:DropDownList ID="ddlGuardianType" runat="server"></asp:DropDownList>
                                </td>
                                <td>等级:
                                    <asp:DropDownList ID="ddlGuardianLeven" runat="server"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:Button ID="btnQuery" Height="27"   CssClass="btn btn-primary" runat="server" Text="查找" OnClick="btnQuery_Click" />

                                </td>
                            </tr>
                        </table>

                    </td>
                </tr>
            </table>

              <table style="width:750px;" class="table table-bordered table-striped">
                <thead>
                    <tr id="trContent">
                        <th>人员名称</th>
                        <th>类型</th>
                        <th>等级</th>
                       
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptGuardian" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><a href='FD_GuardianDetails.aspx?GuardianId=<%#Eval("GuardianId") %>' target="_blank"> <%#Eval("GuardianName") %></a></td>
                                <td><%#GetTypeById(Eval("GuardianTypeId")) %></td>
                                <td><%#GetLevenById(Eval("GuardianLevenId")) %></td>
                               
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>


                </tbody>
            </table>
            <cc1:AspNetPagerTool ID="GuardianPager" AlwaysShow="true" OnPageChanged="GuardianPager_PageChanged" runat="server"></cc1:AspNetPagerTool>
        </div>
    </div>
</asp:Content>
