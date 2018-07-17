<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="TeamReport.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.WorkReport.TeamReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<div class="panel-heading">
        <h4 class="panel-title">
            <a id="acollapseTwo0" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" style="color: blue;">员工负责事项</a>
        </h4>
    </div>
    <table class="table table-bordered table-striped">
        <tr>
            <th style="width: 100px; background-color: lightpink;">类别</th>
            <th style="width: 100px; background-color: lightpink;">项目</th>
            <th style="width: 100px; background-color: lightpink;">产品服务内容</th>
            <th style="width: 100px; background-color: lightpink;">具体要求</th>

            <th style="width: 100px; background-color: lightpink;">成本价</th>
            <th style="width: 100px; background-color: lightpink;">数量</th>
            <th style="width: 100px; background-color: lightpink;">小计</th>
            <th style="width: 100px; background-color: lightpink;">责任人</th>
            <th style="width: 100px; background-color: lightpink;">备注</th>
        </tr>
        <asp:Repeater ID="repProductList" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="width: 100px;"><%#Eval("ParentCategoryName") %>
                        <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("ProeuctKey") %>' />
                    </td>
                    <td style="width: 100px;"><%#Eval("CategoryName") %></td>
                    <td style="width: 100px;"><%#GetProductByID(Eval("ProductID")) %></td>
                    <td style="width: 100px;"><%#Eval("Requirement") %></td>

                    <td>
                        <asp:TextBox ID="txtUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtSubtotal" runat="server" Text='<%#Eval("Subtotal") %>'></asp:TextBox></td>

                    <td style="width: 100px;"><%#Eval("SupplierName") %></td>
                    <td>
                        <asp:TextBox ID="txtRemark" runat="server" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="9">合计(计划支出):<asp:Label ID="lblMoneySum" runat="server" Text=""></asp:Label>
                &nbsp;实际支出:<asp:TextBox ID="txtFinishCost" runat="server"></asp:TextBox>
                <asp:Button ID="btnSaveEdit" runat="server" Text="保存成本明细" OnClick="btnSaveEdit_Click" />
            </td>
        </tr>
    </table>
    <div class="panel-heading">
        <h4 class="panel-title">
            <a id="a1" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" style="color: blue;">四大金刚</a>
        </h4>
    </div>
    <table class="table table-bordered table-striped">
        <tr>
            <th style="width: 100px; background-color: cornsilk">类别</th>
            <th style="width: 100px; background-color: cornsilk">项目</th>
            <th style="width: 100px; background-color: cornsilk">产品</th>
            <th style="width: 100px; background-color: cornsilk">产品服务内容</th>
            <th style="width: 100px; background-color: cornsilk">具体要求</th>
            <th style="width: 100px; background-color: cornsilk">图片</th>
            <th style="width: 100px; background-color: cornsilk">单价</th>
            <th style="width: 100px; background-color: cornsilk">数量</th>
            <th style="width: 100px; background-color: cornsilk">小计</th>

            <th style="width: 100px; background-color: cornsilk">备注</th>
        </tr>
        <asp:Repeater ID="repjingang" runat="server">
            <ItemTemplate>
                <tr>
                    <td style="width: 100px; background-color: cornsilk"><%#Eval("ParentCategoryName") %>
                        <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("ProeuctKey") %>' />
                    </td>
                    <td style="width: 100px; background-color: cornsilk"><%#Eval("CategoryName") %></td>
                    <td style="width: 100px; background-color: cornsilk"><%#Eval("ProductID") %></td>
                    <td style="width: 100px; background-color: cornsilk"><%#Eval("ServiceContent") %></td>
                    <td style="width: 100px; background-color: cornsilk"><%#Eval("Requirement") %></td>
                    <td style="width: 100px; background-color: cornsilk"><a href="#">查看资料</a></td>
                    <td>
                        <asp:TextBox ID="txtUnitPrice" runat="server" Text='<%#Eval("UnitPrice") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtSubtotal" runat="server" Text='<%#Eval("Subtotal") %>'></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtRemark" runat="server" Text='<%#Eval("Remark") %>'></asp:TextBox></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="10">合计（计划支出）:<asp:Label ID="lbljingang" runat="server"></asp:Label>
                &nbsp;实际支出:<asp:TextBox ID="txtFinishMoneyforjingang" runat="server"></asp:TextBox>
                <asp:Button ID="btnSaveforjingang" runat="server" Text="保存成本明细" OnClick="btnSaveforjingang_Click" />
            </td>
        </tr>
    </table>--%>

    <script type="text/javascript">
        function preview() {
            bdhtml = window.document.body.innerHTML;
            sprnstr = "<!--startprint-->";
            eprnstr = "<!--endprint-->";
            prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
            prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML = prnhtml;
            window.print();
        }
    </script>
    <div class="panel-heading">
        <h4 class="panel-title">
            <a id="a2" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" style="color: blue;">兼职人员事项</a>
        </h4>
    </div>
    <table style="text-align: center; width: 102%;" border="1" class="table table-bordered table-striped">
        <tr>
            <th style="background-color: lavender">人员类型</th>
            <th style="background-color: lavender">电话</th>
            <th style="background-color: lavender">姓名</th>
            <th style="background-color: lavender">工作内容</th>
            <th style="background-color: lavender">项目工资</th>
            <th style="background-color: lavender">操作</th>

        </tr>
        <tr>
            <td style="background-color: lavender">
                <asp:TextBox ID="txtEmployeeType" check="1" tip="限20个字符！" runat="server" Width="155px" CssClass="NoneEmpty" MaxLength="20"></asp:TextBox><span style="color: red">*</span></td>
            <td style="background-color: lavender">
                <asp:TextBox ID="txtTelPhone" check="1" tip="手机号码为11位数字！" runat="server" Width="155px" CssClass="NoneEmpty" MaxLength="20"></asp:TextBox><span style="color: red">*</span>
            </td>
            <td style="background-color: lavender">
                <asp:TextBox ID="txtEmployeeName" tip="姓名" check="1" runat="server" Width="155px" CssClass="NoneEmpty" MaxLength="20"></asp:TextBox><span style="color: red">*</span></td>
            <td style="background-color: lavender">
                <asp:TextBox ID="txtBulding" check="1" tip="限50个字符！" runat="server" Width="155px" CssClass="NoneEmpty" MaxLength="50"></asp:TextBox><span style="color: red">*</span>
            </td>
            <td style="background-color: lavender">
                <asp:TextBox ID="txtAmount" check="1" runat="server" Width="155px" CssClass="NoneEmpty" MaxLength="20"></asp:TextBox><span style="color: red">*</span></td>

            <td style="background-color: lavender">
                <asp:Button ID="Button1" runat="server" Text="保存" Height="29" OnClick="btnSaveChange_Click" CssClass="btn btn-info" OnClientClick="return SaveChange()" /></td>

        </tr>
        <asp:Repeater ID="repWeddingPlanning" runat="server" OnItemCommand="repWeddingPlanning_ItemCommand">

            <ItemTemplate>
                <tr>
                    <td style="background-color: lavender">
                        <asp:TextBox ID="txtEmployeeType0" runat="server" Width="155px" Text='<%#Eval("EmployeeType") %>'></asp:TextBox></td>

                    <td style="background-color: lavender">
                        <asp:TextBox ID="txtTelPhone0" runat="server" Width="155px" Text='<%#Eval("TelPhone") %>'></asp:TextBox></td>
                    <td style="background-color: lavender">
                        <asp:TextBox ID="txtEmployeeName0" runat="server" Width="155px" Text='<%#Eval("EmployeeName") %>'></asp:TextBox></td>
                    <td style="background-color: lavender">
                        <asp:TextBox ID="txtBulding0" runat="server" Text='<%#Eval("Bulding") %>' Width="155px"></asp:TextBox></td>
                    <td style="background-color: lavender">
                        <asp:TextBox ID="txtAmount0" runat="server" Text='<%#Eval("Amount") %>' Width="155px"></asp:TextBox></td>

                    <td style="white-space: nowrap; background-color: lavender">

                        <asp:LinkButton ID="lnkbtnDelete" CssClass="btndelete btn btn-primary" CommandName="Delete" CommandArgument='<%#Eval("DeJey") %>' runat="server" OnClientClick="return DeleteData();">删除</asp:LinkButton>

                    </td>

                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="6"></td>
        </tr>
    </table>

    <!--startprint-->
    <div class="div_print">
        <div class="panel-heading">
            <h4 class="panel-title">
                <a id="a3" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" style="color: blue;">婚礼人员统筹表</a></h4>
        </div>
        <table style="text-align: center; width: 102%;" border="1" class="table table-bordered table-striped">
            <tr>
                <td style="background-color: lavender">职责</td>
                <td style="background-color: lavender">责任单位</td>
                <td style="background-color: lavender">联系电话</td>

            </tr>
            <asp:Repeater runat="server" ID="repOrderTeamList" OnDataBinding="repOrderTeamList_DataBinding" OnItemDataBound="repOrderTeamList_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td style="background-color: lavender"><%#Eval("Title") %></td>
                        <td style="background-color: lavender">
                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
                        <td style="background-color: lavender">
                            <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label>
                        </td>

                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="3"></td>
            </tr>
        </table>

        <div class="panel-heading">
            <h4 class="panel-title">
                <a id="a4" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" style="color: blue;">供应商</a></h4>
        </div>
        <table style="text-align: center; width: 102%;" border="1" class="table table-bordered table-striped">
            <tr>
                <td style="background-color: lavender">供应商名称</td>
                <td style="background-color: lavender">负责人</td>
                <td style="background-color: lavender">联系电话</td>
            </tr>
            <asp:Repeater runat="server" ID="Repeater1">
                <ItemTemplate>
                    <tr>
                        <td style="background-color: lavender"></td>
                        <td style="background-color: lavender">
                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
                        <td style="background-color: lavender">
                            <asp:Label ID="lblPhone" runat="server" Text=""></asp:Label></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="3"></td>
            </tr>
        </table>
    </div>
    <!--endprint-->
    <div>
        <input type="button" value="打印" class="btn btn-primary" onclick="preview()" />
        <a href="javascript:preview()" onclick="preview()" target="_self">打印</a>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            height: 25px;
        }
    </style>
</asp:Content>

