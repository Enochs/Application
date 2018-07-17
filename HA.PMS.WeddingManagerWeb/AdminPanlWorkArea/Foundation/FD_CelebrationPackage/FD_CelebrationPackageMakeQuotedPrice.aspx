<%@ Page Title="" Language="C#" EnableViewState="true" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationPackageMakeQuotedPrice.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage.FD_CelebrationPackageMakeQuotedPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {

            var jumpUrl = $("#createCelebrationProduct").attr("href") + "?cidFirst="
            + $("#<%=ddlCategoryProduct.ClientID%>").val() + "&cidSecond=" + $("#<%=ddlCategorySecond.ClientID%>").val();
            showPopuWindows(jumpUrl, 700, 700, "a#createCelebrationProduct");

            //把每个添加产品之后新生成的控件隔离开来

            $("#contentDiv span,#contentDiv input").each(function (index, values) {
                $(this).after("<br />");
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="overflow-x: auto;">
        <div class="widget-box">

            <div id="contentDiv" class="widget-content">
                <table class="table table-bordered table-striped">
                    <thead>
                        <tr>
                            <th>项目</th>
                            <th>类别</th>
                            <th style="width: auto;">产品服务</th>
                            <th>具体要求</th>
                            <th>图片(上传)</th>
                            <th>单价</th>
                            <th>数量</th>
                            <th>小计</th>
                            <th>备注</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>

                                <!--Category项目选项Ul -->

                                <asp:Label ID="lblProductName" runat="server" name="ProductName" Text=""></asp:Label>

                            </td>
                            <td>
                                <!--Category类别选项div -->
                                <asp:Label ID="lblCategoryName" runat="server" Text=""></asp:Label>
                            </td>
                            <td id="tdProduct" runat="server">
                                <!--产品选项div -->
                                <asp:Panel ID="pnlProductName" runat="server"></asp:Panel>

                            </td>
                            <td>
                                <!--具体要求div -->
                                <asp:Panel ID="pnlSpecificRequirements" runat="server"></asp:Panel>
                            </td>
                            <td>
                                <!--图片上传div -->
                                <asp:Panel ID="pnlImage" runat="server"></asp:Panel>
                            </td>
                            <td>
                                <!--单价div -->
                                <asp:Panel ID="pnlProductPrice" runat="server"></asp:Panel>
                            </td>
                            <td>
                                <!--数量div -->
                                <asp:Panel ID="pnlCount" runat="server"></asp:Panel>
                            </td>
                            <td>
                                <!--小计div -->
                                <asp:Panel ID="pnlSmallSum" runat="server"></asp:Panel>
                            </td>
                            <!--备注div -->
                            <td>
                                <asp:Panel ID="pnlExplain" runat="server"></asp:Panel>
                            </td>
                            <td>
                                <!--操作div -->
                                <asp:Panel ID="pnl" runat="server"></asp:Panel>
                            </td>
                        </tr>

                        <tr>
                            <td>

                                <!--Category项目选项Ul -->

                                <asp:DropDownList ID="ddlCategoryProduct" Width="80" runat="server"></asp:DropDownList><br />
                                <asp:Button ID="btnProductSave" CssClass="btn btn-success" runat="server" Text="+" OnClick="btnProductSave_Click" />
                            </td>
                            <td>
                                <!--Category类别选项div -->
                                <asp:DropDownList ID="ddlCategorySecond" Width="80" runat="server"></asp:DropDownList>
                                <br />
                                <asp:Button ID="btnCategorySave" CssClass="btn btn-success" runat="server" Text="+" OnClick="btnCategorySave_Click" />
                            </td>
                            <td>
                                <!--产品选项div -->
                                <a class="btn btn-primary  btn-mini" href="FD_CelebrationProductCreate.aspx" id="createCelebrationProduct">选择产品</a>
                            </td>
                            <td>
                                <!--具体要求div -->

                            </td>
                            <td>
                                <!--图片上传div -->

                            </td>
                            <td>
                                <!--单价div -->

                            </td>
                            <td>
                                <!--数量div -->

                            </td>
                            <td>
                                <!--小计div -->

                            </td>
                            <!--备注div -->
                            <td></td>
                            <td>
                                <!--操作div -->

                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>

        </div>
        <br />
        <asp:Button ID="btnSave" CssClass="btn btn-primary btn-mini" runat="server" Text="保存" OnClick="btnSave_Click" Style="height: 21px" />

    </div>
</asp:Content>
