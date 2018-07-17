<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PurchaseReport.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Carrytask.CarrytaskWork.WorkReport.PurchaseReport" MasterPageFile="~/AdminPanlWorkArea/Master/MainMaster.Master" %>

<asp:Content runat="server" ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1">

    <script type="text/javascript">
        //显示文件列表
        function ShowFileShowPopu(FlowerKey) {
            var Url = "FlowerImageList.aspx?FlowerID=" + FlowerKey;
            showPopuWindows(Url, 700, 800, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>

    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>

    <div class="div_saves">
        <asp:Button runat="server" ID="btnSaveCost" Text="保存成本" CssClass="btn btn-primary" Style="vertical-align: middle;" OnClick="btnSaveCost_Click" /><p></p>
    </div>
    <div class="div_CreateReport">
        <table style="text-align: center; width: 100%;" border="1" class="table table-bordered table-striped">

            <tr>
                <th>物料名称</th>
                <th>采购单价</th>
                <th>单位</th>
                <th style="display: none;">销售单价</th>
                <th>数量</th>
                <th>总成本</th>
                <th style="display: none;">销售总价</th>
                <th>凭据</th>
                <th>说明</th>
                <th>操作</th>

            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtFlosername" check="1" tip="限20个字符！" runat="server" Width="100px" CssClass="NoneEmpty" MaxLength="20"></asp:TextBox><span style="color: red">*</span></td>
                <td>
                    <asp:TextBox ID="txtCostPrice" runat="server" Width="100px" CssClass="NoneEmpty" MaxLength="20"></asp:TextBox><span style="color: red">*</span>
                </td>
                <td style="display: none;">

                    <asp:TextBox ID="txtSalePrice" tip="姓名" runat="server" Width="70px" CssClass="NoneEmpty" MaxLength="20" Text=""></asp:TextBox>

                </td>
                <td>
                    <asp:TextBox ID="txtUnite" runat="server" Width="70px"></asp:TextBox>
                    <span style="color: red">*</span>
                </td>
                <td>
                    <asp:TextBox ID="txtQuantity" runat="server" Width="70px" CssClass="NoneEmpty" MaxLength="50"></asp:TextBox>
                    <span style="color: red">*</span></td>
                <td>自动计算</td>
                <td style="display: none;">
                    <asp:TextBox ID="txtSaleSumPrice" check="1" runat="server" Width="70px" CssClass="NoneEmpty" MaxLength="20" Text="0"></asp:TextBox><span style="color: red">*</span>
                </td>
                <td>
                    <asp:FileUpload runat="server" ID="UploadFile" /></td>
                <td>
                    <asp:TextBox ID="txtNode" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnSaveChange" runat="server" Text="保存" Height="29" OnClick="btnSaveChange_Click" CssClass="btn btn-info" OnClientClick="return SaveChange()" />
                    <asp:Button ID="btnExport" runat="server" Text="导出打印" Height="29" OnClick="btnExport_Click" CssClass="btn btn-info" />

                </td>

            </tr>
            <asp:Repeater ID="repFlowerPlanning" runat="server" OnItemCommand="repFlowerPlanning_ItemCommand">
                <ItemTemplate>

                    <tr>
                        <td>
                            <asp:TextBox ID="txtFLowername" runat="server" Text='<%#Eval("FLowername") %>' Width="100px"></asp:TextBox>
                            <asp:HiddenField ID="hideKey" runat="server" Value='<%#Eval("Flowerkey") %>' />
                        </td>

                        <td>
                            <asp:TextBox ID="txtCostPrice" runat="server" Text='<%#Eval("CostPrice") %>' Width="100px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtUnite" runat="server" Text='<%#Eval("Unite") %>' Width="70px"></asp:TextBox></td>
                        <td style="display: none;">
                            <asp:TextBox ID="txtSalePrice" runat="server" Text='<%#Eval("SalePrice") %>'></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>' Width="70px"></asp:TextBox></td>
                        <td>
                            <%#Eval("CostSumPrice") %></td>
                        <td style="display: none;">
                            <asp:TextBox ID="txtSaleSumPrice" runat="server" Text='<%#Eval("SaleSumPrice") %>'></asp:TextBox></td>
                        <td>
                            <a href='#' onclick="ShowFileShowPopu('<%#Eval("Flowerkey") %>')" class="btn btn-mini  btn-primary needshow">查看</a>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNode" runat="server" Text='<%#Eval("Node") %>'></asp:TextBox></td>
                        <td style="white-space: nowrap; background-color: white">
                            <asp:LinkButton ID="lnkbtnDelete" CssClass="btndelete btn btn-primary" CommandName="Delete" CommandArgument='<%#Eval("Flowerkey") %>' runat="server" OnClientClick="return confirm('你确定要删除吗?')">删除</asp:LinkButton>
                            <asp:LinkButton ID="btnSaveEdit" CssClass="btndelete btn btn-primary" CommandName="Edit" CommandArgument='<%#Eval("Flowerkey") %>' runat="server">修改</asp:LinkButton>
                        </td>

                    </tr>

                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="9">成本合计:<asp:Label ID="lblSumMoney" runat="server" Text="" Style="font-size: 14px; font-weight: bolder;"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

