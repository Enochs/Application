<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="SaleTargetCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company.SaleTargetCreate" %>

<%@ Register Assembly="HA.PMS.EditoerLibrary" Namespace="HA.PMS.EditoerLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <style type="text/css">
        .centerTxt {
            width: 85px;
            height: 20px;
        }

        .centerSelect {
            width: 98px;
            height: 30px;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#trContent th").css({ "white-space": "nowrap" });
            $("#trTarget th").css({ "white-space": "nowrap" });
            
            $("#trContentDepart th").css({ "white-space": "nowrap" });
            $(".queryTable td").css({ "border": "0", "vertical-align": "top" });
            $("html").css({ "overflow-x": "hidden" });
            $(":text").change(function() {
               if (!/^[0-9]+(.[0-9]{2})?$/.test($(this).val())) { alert('只能输入数字或者两位小数数字'); $(this).val(''); }
            });
 


          //var  trInput=$("input[name=ctl00$ContentPlaceHolder1$rptDepartTarget$ctl00$hfValue]").parent("tr");
         
          
          //  trInput.children("td").each(function (indexs, values) {
                
          //      $(this).text( parseInt($.trim ((this).text())));
          //  });
            
       

            
          
            
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

           


        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">

        <asp:PlaceHolder ID="phManager" Visible="false" runat="server">

            <div class="widget-title">
                <span class="icon"><i class="icon-th"></i></span>
                <h5>部门目标管理</h5>

            </div>
            <div class="widget-content">
                <table class="table table-bordered table-striped">
                    <thead>
                        <tr id="trContentDepart">
                            <th>指标名称</th>
                            <th>1</th>
                            <th>2</th>
                            <th>3</th>
                            <th>4</th>
                            <th>5</th>
                            <th>6</th>
                            <th>7</th>
                            <th>8</th>
                            <th>9</th>
                            <th>10</th>
                            <th>11</th>
                            <th>12</th>
                            <th>年度</th>
                        </tr>
                    </thead>
                    <tbody>
                           <asp:Repeater ID="rptDepartTarget" OnItemDataBound="rptDepartTarget_ItemDataBound" runat="server">
                        <ItemTemplate>

                            <asp:PlaceHolder ID="phTarget" runat="server">


                                <tr>

                                    <asp:HiddenField ID="hfValue" runat="server" />
                                    <td>
                                        <asp:Label ID="lblMonth" ToolTip="" runat="server" Text="Label"></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="lblMonth1"  ToolTip="1"  runat="server"></asp:Label>

                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth2" ToolTip="2"  runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth3" ToolTip="3" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth4" ToolTip="4"  runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth5" ToolTip="5"   runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth6" ToolTip="6"   runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth7" ToolTip="7"   runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth8" ToolTip="8"  runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth9" ToolTip="9"   runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth10" ToolTip="10"  runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth11" ToolTip="11"  runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblMonth12" ToolTip="12"  runat="server"></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Literal ID="ltlYearCount" Text="0" runat="server"></asp:Literal>
                                    </td>

                                </tr>
                            </asp:PlaceHolder>
                           <!---这块是针对于质量指标的更改 -->
                          <asp:PlaceHolder ID="phTargetRate"  runat="server">


                                <tr>

                                    <asp:HiddenField ID="hfValueRate" runat="server" />
                                    <td>
                                        <asp:Label ID="lblMonthRate" ToolTip="" runat="server" Text="Label"></asp:Label>
                                    </td>

                                    <td>
                                            <asp:TextBox ID="txtMonth1"  ToolTip="1" Width="35" MaxLength="10" runat="server"></asp:TextBox>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth2"  ToolTip="2" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth3"  ToolTip="3" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth4"  ToolTip="4" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth5"  ToolTip="5" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth6" ToolTip="6"  Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth7" ToolTip="7" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth8" ToolTip="8"  Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth9" ToolTip="9" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth10" ToolTip="10"  Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth11" ToolTip="11"  Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth12" ToolTip="12" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>


                                    <td>
                                        <asp:Literal ID="ltlYearCountRate" Text="0" runat="server"></asp:Literal>
                                    </td>

                                </tr>
                            </asp:PlaceHolder>
                        </ItemTemplate>
                    </asp:Repeater>
                    </tbody>

                    <tfoot>
                        <tr>
                            <td colspan="14">
                                <asp:Button ID="btnSaveRate" Visible="false" runat="server" OnClick="btnSaveRate_Click" CssClass="btn" Text="保存质量指标" />
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </div>
            <div class="widget-title">
                <span class="icon"><i class="icon-th"></i></span>
                <h5>员工目标管理</h5>

            </div>
            <div class="widget-content">
                <table class="queryTable">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>部门： 
                                         <asp:DropDownList ID="ddlDepartment" AutoPostBack="true" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                        </td>
                                        <td>
                                            人员:<asp:DropDownList ID="ddlEmployee" runat="server"></asp:DropDownList>
                                        </td>
                                        <td></td>
                                        <td> 
                                            <asp:Button ID="btnQuery" CssClass="btn btn-primary" Height="27" OnClick="btnQuery_Click" runat="server" Text="查询" />

                                        </td>
                                    </tr>

                                </table>

                            </td>
                        </tr>
                    </table>
                  <table class="table table-bordered table-striped">
                    <thead>
                        <tr id="trTarget">
                            <th>员工姓名</th>
                            <th>部门</th>
                            <th>职务</th>
                            <th>指标名称</th>
                            <th>1</th>
                            <th>2</th>
                            <th>3</th>
                            <th>4</th>
                            <th>5</th>
                            <th>6</th>
                            <th>7</th>
                            <th>8</th>
                            <th>9</th>
                            <th>10</th>
                            <th>11</th>
                            <th>12</th>
                            <th>年度</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptManagerMent" OnItemCommand="rptManagerMent_ItemCommand" OnItemDataBound="rptManagerMent_ItemDataBound" runat="server">
                            <ItemTemplate>
                                <asp:PlaceHolder ID="phTarget" runat="server">

                                    <tr>

                                        <asp:HiddenField ID="hfValue" runat="server" />
                                        <td><%#Eval("EmployeeName") %></td>
                                        <td>
                                            <%#GetDepartmentNameByID( Eval("DepartmentID")) %>
                                        </td>

                                        <td>
                                            <%#GetJobNameByJobID(Eval("JobID")) %>
                                        </td>
                                        <td>
                                            <%#GetGoalByDepartId(Eval("DepartmentID")) %>
                                        </td>


                                        <td>
                                            <asp:TextBox ID="txtMonth1"  ToolTip="1" Width="35" MaxLength="10" runat="server"></asp:TextBox>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth2"  ToolTip="2" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth3"  ToolTip="3" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth4"  ToolTip="4" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth5"  ToolTip="5" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth6" ToolTip="6"  Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth7" ToolTip="7" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth8" ToolTip="8"  Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth9" ToolTip="9" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth10" ToolTip="10"  Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth11" ToolTip="11"  Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth12" ToolTip="12" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                        </td>

                                        <td>
                                            <asp:Literal ID="ltlYearCount" Text="0" runat="server"></asp:Literal></td>
                                        <td>
                                            <asp:LinkButton ID="lkbtnUpdate" CommandName="Save" CommandArgument='<%#Eval("EmployeeID") %>' runat="server" CssClass="btn btn-primary btn-mini">修改</asp:LinkButton>

                                        </td>
                                    </tr>
                                </asp:PlaceHolder>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
              
    
         
        </asp:PlaceHolder>
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>我的目标管理</h5>

        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <thead>
                    <tr id="trContent">
                        <th>指标名称</th>
                        <th>1</th>
                        <th>2</th>
                        <th>3</th>
                        <th>4</th>
                        <th>5</th>
                        <th>6</th>
                        <th>7</th>
                        <th>8</th>
                        <th>9</th>
                        <th>10</th>
                        <th>11</th>
                        <th>12</th>
                        <th>年度</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptTarget" OnItemDataBound="rptTarget_ItemDataBound" runat="server">
                        <ItemTemplate>

                            <asp:PlaceHolder ID="phTarget" runat="server">


                                <tr>

                                    <asp:HiddenField ID="hfValue" runat="server" />
                                    <td>
                                        <asp:Label ID="lblMonth" ToolTip="" runat="server" Text="Label"></asp:Label>
                                    </td>

                                    <td>
                                        <asp:TextBox ID="txtMonth1"  ToolTip="1" Width="35" MaxLength="10" runat="server"></asp:TextBox>

                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonth2" ToolTip="2" Width="35"  MaxLength="10" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonth3" ToolTip="3" Width="35"  MaxLength="10" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonth4" ToolTip="4" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonth5" ToolTip="5" Width="35"  MaxLength="10" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonth6" ToolTip="6" Width="35" MaxLength="10" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonth7" ToolTip="7" Width="35"  MaxLength="10" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonth8" ToolTip="8" Width="35"  MaxLength="10" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonth9" ToolTip="9" Width="35"  MaxLength="10" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonth10" ToolTip="10" Width="35"  MaxLength="10" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonth11" ToolTip="11" Width="35"  MaxLength="10" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMonth12" ToolTip="12" Width="35"  MaxLength="10" runat="server"></asp:TextBox>
                                    </td>

                                    <td>
                                        <asp:Literal ID="ltlYearCount" Text="0" runat="server"></asp:Literal>
                                    </td>

                                </tr>
                            </asp:PlaceHolder>
                        </ItemTemplate>
                    </asp:Repeater>


                </tbody>
                <tfoot>
                    <tr>
                        <td>

                            <cc1:ClickOnceButton ID="btnSave" Text="保存" OnClick="btnSave_Click" CssClass="btn btn-success" runat="server" />
                        </td>

                        <td colspan="13"></td>
                    </tr>



                </tfoot>
            </table>
            <HA:MessageBoardforall runat="server" ID="MessageBoardforall" />
        </div>
    </div>
</asp:Content>
