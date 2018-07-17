<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_CelebrationPackageUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationPackage.FD_CelebrationPackageUpdate" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/AdminPanlWorkArea/Control/MessageBoardforall.ascx" TagPrefix="HA" TagName="MessageBoardforall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .itembox li {
            list-style: none;
            padding: 10px;
            float: left;
        }

        .itemtable {
            text-align: center;
            border-style: none;
            padding: 1px;
        }

            .itemtable tr td {
                border-style: none;
                border-collapse: collapse;
                border: none;
            }
    </style>
    <script type="text/javascript">
        function ShowUploadPackageImageWindow(Control) { 
            var _packageid=<%=Request["PackageID"]%>;
            var _url = "/AdminPanlWorkArea/Foundation/FD_Hotel/FD_HotelImageLoadFile.aspx?PackageID=" + _packageid + "&toOperPage=" + <%=ViewState["load"]%>;
            $(Control).attr("id", "updateShow" + _packageid);
            showPopuWindows(_url, 700, 300, "a#" + $(Control).attr("id"));
        }
        $(document).ready(function () {
            showPopuWindows($("#createCelebrationPackageProduct").attr("href"), 1400, 700, "a#createCelebrationPackageProduct");
        });
        $(window).load(function () {
            BindString(25, '<%=txtPackageTitle.ClientID%>');
            BindMoney('<%=txtPackagePrice.ClientID%>:<%=txtPackagePreferentiaPrice.ClientID%>');
            BindText(400, '<%=txtPackageDirections.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
        });
        function validate() {
            return ValidateForm('input[check],textarea[check]');
        }
        function spiconchange(ctrl) {
            var _val = $(ctrl).val();
            $('#imgpath').html(_val);
            if(_val==''){
                $("#btnuploadpic").html("更换图片").attr("title","点击更换套系封面图片");
            }
            else{
                $("#btnuploadpic").html("已选图片").attr("title",_val);
            }
        }
        function changepic(ctrl){
            $(ctrl).parent("td").children("input[type=file]").click();
        }
        function piconchange(ctrl){
            var _path=$(ctrl).val();
            if(_path!=''){
                $(ctrl).prev().html("已选图片").attr("title",_path);
            }
            else{
                $(ctrl).prev().html("更换图片").attr("title",'点击更换图片');
            }
        }
        function chooseitem(ctrl)
        {
            $(".divitem").css("border","1px solid #bcbcbc");
            $(ctrl).css("border","1px solid #0072c6");
        }

        //点击文本框 弹出部门人员列表
        function ShowPopu(Parent) {
            var Url = "/AdminPanlWorkArea/ControlPage/SelectEmpLoyeeBythis.aspx?ALL=1&ControlKey=hideEmpLoyeeID&ParentControl=" + $(Parent).parent().attr("id");
            showPopuWindows(Url, 480, 380, "#SelectEmpLoyeeBythis");
            $("#SelectEmpLoyeeBythis").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <a href="#" id="SelectEmpLoyeeBythis" style="display: none;">上传图片</a>
    <div class="widget-box" style="width: 99%; overflow-y: scroll; height: 1000px">
        <div class="widget-content">
            <table style="width: 100%" class="table table-bordered table-striped">
                <tr>
                    <td style="width: 64px"><span style="color: red">*</span>套系名称</td>
                    <td style="width: 272px">
                        <asp:TextBox ID="txtPackageTitle" Style="margin: 0; width: 256px" tip="限25个字符！" check="1" runat="server" MaxLength="25"></asp:TextBox></td>
                    <td style="width: 64px">&nbsp;套系风格</td>
                    <td>
                        <asp:DropDownList Style="margin: 0; width: 134px" ID="ddlPackageStyle" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td><span style="color: red">*</span>套系价格</td>
                    <td>
                        <asp:TextBox ID="txtPackagePrice" Style="margin: 0" check="1" tip="套系价格" runat="server" MaxLength="10"></asp:TextBox></td>
                    <td style="min-width: 80px"><span style="color: red">*</span>优惠价格</td>
                    <td>
                        <asp:TextBox ID="txtPackagePreferentiaPrice" Style="margin: 0" check="1" tip="套系优惠价" runat="server" MaxLength="10"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>可预订数量</td>
                    <td>
                        <asp:TextBox ID="txtSum" runat="server"></asp:TextBox>
                    </td>
                    <td>设计师</td>
                    <td id="<%=Guid.NewGuid().ToString() %>">
                        <input runat="server" id="txtEmpLoyee" class="txtEmpLoyeeName" onclick="ShowPopu(this);" type="text" />
                        <asp:TextBox runat="server" ID="txtEmployees" CssClass="txtEmployeeName" onclick="ShowPopu(this);" ClientIDMode="Static" Text='<%#GetEmployeeName(Eval("DesignerEmployee")) %>' Style="margin: 0; width: 90px;" Visible="false" Enabled="false" />

                        <asp:HiddenField ID="hideEmpLoyeeID" ClientIDMode="Static" Value='' runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;优惠说明</td>
                    <td colspan="3">
                        <asp:TextBox ID="txtPackageDirections" Style="margin: 0; width: 99%" tip="限400个字符！" check="0" TextMode="MultiLine" runat="server" MaxLength="400"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;封面图片</td>
                    <td colspan="3"><a href="#" id="btnuploadpic" class="btn btn-primary btn-mini" title="点击更换套系封面图片" onclick="changepic(this);return false;">更换图片</a>
                        <span style="font-family: Verdana" id="imgpath"></span>
                        <asp:FileUpload onchange="spiconchange(this)" Style="display: none" ID="flCelePackage" runat="server" /></td>
                </tr>
                <tr>
                    <td rowspan="2">&nbsp;套系图片</td>
                    <td colspan="3"><a href="#" class="btn btn-primary  btn-mini" onclick='ShowUploadPackageImageWindow(this);'>添加图片</a>&nbsp;&nbsp;
                            <asp:LinkButton ID="btnReloadPackageImage" OnClick="BinderPackageImage" CssClass="btn btn-info btn-mini" runat="server" Text="刷新" />&nbsp;&nbsp;
                            <asp:LinkButton ID="btnSavePackageImage" OnClick="btnSavePackageImage_Click" Text="全部保存" CssClass="btn btn-success btn-mini" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="margin: 0; padding: 0">
                        <div class="itembox">
                            <ul>
                                <asp:Repeater ID="rptPackageImage" OnItemCommand="rptPackageImage_ItemCommand" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <div style="width: auto; height: auto; border: 1px solid #bcbcbc" class="divitem" onclick="chooseitem(this)">
                                                <table class="itemtable">
                                                    <tr>
                                                        <td style="text-align: center; vertical-align: middle; width: 74px; height: 74px">
                                                            <asp:Image ID="img" Style="margin: 0; padding: 0" ImageUrl='<%#Eval("ImageUrl") %>' Width="70" Height="70" runat="server" /></td>
                                                        <td style="text-align: center; vertical-align: middle; width: 80px; height: 80px">
                                                            <asp:TextBox Style="margin: 0" ToolTip="图片简介，限50个字符，超出部分将被忽略" ID="txtMessage" Text='<%#Eval("Message") %>' Height="60" TextMode="MultiLine" runat="server" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <a href="#" class="btn btn-primary btn-mini" onclick="changepic(this);return false;" title="点击更换图片">更换图片</a>
                                                            <asp:FileUpload ID="fpImg" onchange="piconchange(this)" Style="display: none" Width="64" runat="server" /></td>
                                                        <td style="text-align: center;">
                                                            <asp:LinkButton ID="btnLoad" CommandName="Save" CommandArgument='<%#Eval("ImageId") %>' runat="server" Text="保存" CssClass="btn btn-success btn-mini" />&nbsp;
                                                            <asp:LinkButton ID="btnDelete" CommandName="Delete" CommandArgument='<%#Eval("ImageId") %>' runat="server" OnClientClick="return confirm('确认删除该图片吗')" Text="删除" CssClass="btn btn-danger btn-mini" /></td>
                                                        <asp:HiddenField ID="hideImageId" Value='<%#Eval("ImageId") %>' runat="server" />
                                                    </tr>
                                                </table>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td rowspan="2">&nbsp;最佳拍档</td>
                    <td colspan="3"><a href="../FD_SaleSources/FD_StorehouseSupplierProductAlls.aspx?PackageID=<%=Request["PackageID"] %>" class="btn btn-primary  btn-mini" id="createCelebrationPackageProduct">添加搭档</a>&nbsp;&nbsp
                            <asp:LinkButton ID="btnReloalPackageProduct" runat="server" Text="刷新" OnClick="BinderPackageProduct" CssClass="btn btn-info btn-mini" />&nbsp;&nbsp;
                            <asp:LinkButton ID="btnSavePackageProduct" OnClick="btnSavePackageProduct_Click" Text="全部保存" CssClass="btn btn-success btn-mini" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div class="itembox">
                            <ul>
                                <asp:Repeater ID="rptPackageProduct" OnItemCommand="rptPackageProduct_ItemCommand" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <div style="width: auto; height: auto; border: 1px solid #bcbcbc" class="divitem" onclick="chooseitem(this)">
                                                <table class="itemtable">
                                                    <tr>
                                                        <td style="text-align: center; vertical-align: middle; width: 74px; height: 74px">
                                                            <asp:Image ID="img" ImageUrl='<%#Eval("Data") %>' Width="70" Height="70" runat="server" /></td>
                                                        <td style="width: 134px">成本价：<%#Eval("PurchasePrice") %><br />
                                                            销售价：<%#Eval("SalePrice") %><br />
                                                            优惠价：<asp:TextBox ID="txtPackagePrice" Style="padding: 0; margin: 0" Width="64" MaxLength="10" Text='<%#Eval("PackagePrice") %>' onchange="if(/\D/.test(this.value)){alert('只能输入数字');this.value='0';}" runat="server"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div style="cursor: default; width: 74px; overflow-y: hidden; overflow-x: hidden; white-space: nowrap; text-overflow: ellipsis" title='<%#Eval("ProductName") %>'><%#Eval("ProductName") %></div>
                                                        </td>
                                                        <td style="text-align: center">
                                                            <asp:LinkButton ID="lkbtnSave" CommandName="Save" CommandArgument='<%#Eval("PackageProductID") %>' CssClass="btn btn-success btn-mini" runat="server">保存</asp:LinkButton>&nbsp;
                                                            <asp:LinkButton ID="lkbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("PackageProductID") %>' CssClass="btn btn-danger btn-mini" runat="server">删除</asp:LinkButton>
                                                            <asp:HiddenField ID="hidePackageProductID" Value='<%#Eval("PackageProductID") %>' runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;套系详情</td>
                    <td colspan="3">
                        <cc1:CKEditorTool ID="txtPackageDetails" Height="200" Width="99%" runat="server" BasePath="~/Scripts/ckeditor/"></cc1:CKEditorTool></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="3">
                        <asp:Button ID="btnSave" CssClass="btn btn-success" OnClientClick="return validate()" runat="server" Text="保存" OnClick="btnSave_Click" /></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <HA:MessageBoardforall runat="server" ID="MessageBoardforall1" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
