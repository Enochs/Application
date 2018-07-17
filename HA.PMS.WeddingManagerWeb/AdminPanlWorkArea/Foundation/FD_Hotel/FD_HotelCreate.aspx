<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_HotelCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Hotel.FD_HotelCreate" %>

<%@ Register Assembly="HA.PMS.ToolsLibrary" Namespace="HA.PMS.ToolsLibrary" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="css/lrtk.css" rel="stylesheet" />


    <script type="text/javascript">
        $(document).ready(function () {
            $("body").css({ "background-color": "transparent", "overflow-y": "scroll", "overflow-x": "hidden", "height": "600px" });
            var availabletags=<%=ViewState["areatags"]%>
            $("#<%=txtArea.ClientID%>").autocomplete({source:availabletags});
        });

        $(window).load(function () {
            BindString(20, '<%=txtHotelName.ClientID%>:<%=txtArea.ClientID%>');
            BindString(30, '<%=txtAddress.ClientID%>:<%=txtTel.ClientID%>');
            BindMoney('<%=txtPriceStar.ClientID%>:<%=txtPriceEnd.ClientID%>');
            BindUInt('<%=txtDeskCount.ClientID%>');
            BindCtrlEvent('input[check],textarea[check]');
        });
        function checksave() {
            return ValidateForm('input[check],textarea[check]');
        }

        $(document).ready(function(){
            $("#txtScore").focusout(function(){
                var value=document.getElementById("txtScore").value;
                if(value > 5){
                    document.getElementById("txtScore").value="5.0";
                }else if(value <= 0){
                    document.getElementById("txtScore").value="0.0";
                }
            });

            $(function(){      
                /*JQuery 限制文本框只能输入数字和小数点*/  
                $("#txtScore").keyup(function(){    
                    $(this).val($(this).val().replace(/[^0-9.]/g,''));    
                }).bind("paste",function(){  //CTR+V事件处理    
                    $(this).val($(this).val().replace(/[^0-9.]/g,''));     
                }).css("ime-mode", "disabled"); //CSS设置输入法不可用    
            });  
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box" style="width: 97%">
        <div class="widget-content">
            <table class="table table-bordered table-striped">
                <tr>
                    <td style="text-align: right; width: 70px"><span style="color: red">*</span>酒店名称</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 260px" ID="txtHotelName" tip="限20个字符内" check="1" runat="server" MaxLength="20"></asp:TextBox></td>
                    <td style="text-align: right; width: 70px"><span style="color: red">*</span>酒店区域</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 260px" ID="txtArea" tip="限20个字符如：重庆市-高薪区" check="1" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right"><span style="color: red">*</span>酒店地址</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 260px" ID="txtAddress" tip="酒店的地理位置，限30个字符！" check="1" runat="server" MaxLength="30"></asp:TextBox></td>
                    <td style="text-align: right"><span style="color: red">*</span>联系方式</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 260px" ID="txtTel" tip="限30个字符！" check="1" MaxLength="30" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right"><span style="color: red">*</span>酒店价格</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 100px" ID="txtPriceStar" tip="价格下限" check="1" runat="server" MaxLength="10"></asp:TextBox>
                        -
                        <asp:TextBox Style="margin: 0; width: 100px" ID="txtPriceEnd" tip="价格上限" check="1" runat="server" MaxLength="10"></asp:TextBox></td>
                    <td style="text-align: right"><span style="color: red">*</span>容纳桌数</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 100px" ID="txtDeskCount" check="1" tip="容纳桌数" CssClass="{required:true,number:true}" runat="server" MaxLength="10"></asp:TextBox></td>
                </tr>
                <tr id="Tr1" runat="server" visible="false">
                    <td style="text-align: right">具体位置</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 100px" ID="txtAddress1" check="1" tip="具体位置" CssClass="{required:true,number:true}" runat="server" MaxLength="10"></asp:TextBox></td>
                    <td style="text-align: right">其他</td>
                    <td>
                        <asp:TextBox Style="margin: 0;" ID="txtOther" check="1" tip="其他" CssClass="{required:true,number:true}" runat="server" MaxLength="10" TextMode="MultiLine" Width="205px"></asp:TextBox></td>
                </tr>
                <tr id="Tr2" runat="server">
                    <td style="text-align: right">评分</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 100px" ClientIDMode="Static" ID="txtScore" check="1" tip="评价得分" CssClass="{required:true,number:true}" runat="server" MaxLength="3"></asp:TextBox></td>
                    <td style="text-align: right">排序</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 100px" ClientIDMode="Static" ID="txtSort" check="1" tip="排序(数字越大越靠前)" CssClass="{required:true,number:true}" runat="server"></asp:TextBox></td>
                </tr>
                <tr id="Tr4" runat="server">
                    <td style="text-align: right">推荐</td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rdoRecommand" RepeatDirection="Horizontal">
                            <asp:ListItem Text="推荐" Value="1" Selected="True" />
                            <asp:ListItem Text="不推荐" Value="2" />
                        </asp:RadioButtonList>
                    </td>
                    <td style="text-align: right">优惠</td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="rdoOnSale" RepeatDirection="Horizontal">
                            <asp:ListItem Text="有优惠" Value="1" Selected="True" />
                            <asp:ListItem Text="无优惠" Value="2" />
                        </asp:RadioButtonList>

                    </td>
                </tr>
                <tr id="Tr3" runat="server" visible="false">
                    <td style="text-align: right">旺季餐标</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 100px" ID="txtMoney2" check="1" tip="旺季餐标" CssClass="{required:true,number:true}" runat="server" MaxLength="10"></asp:TextBox></td>
                    <td style="text-align: right">淡季餐标</td>
                    <td>
                        <asp:TextBox Style="margin: 0; width: 100px" ID="txtMoney1" check="1" tip="淡季餐标" CssClass="{required:true,number:true}" runat="server" MaxLength="10"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="text-align: right">场地标签</td>
                    <td colspan="3">
                        <ul>
                            <asp:Repeater ID="rptLabelList" runat="server">
                                <ItemTemplate>
                                    <li style="list-style: none;">
                                        <asp:CheckBox ID="chSinger" runat="server" />
                                        <%#Eval("HotelLabelName") %>
                                        <asp:HiddenField ID="hfLabelValue" Value='<%#Eval("HotelLabelID") %>' runat="server" />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">酒店星级</td>
                    <td colspan="3">
                        <asp:DropDownList Style="margin: 0" ID="ddlStarLevel" runat="server">
                            <asp:ListItem Text="五星级酒店" Value="5" />
                            <asp:ListItem Text="四星级酒店" Value="4" />
                            <asp:ListItem Text="特色餐厅" Value="3" />
                            <asp:ListItem Text="其他" Value="2" />
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">详细参数</td>
                    <td colspan="3">
                        <cc1:CKEditorTool ID="txtDetails" Width="98%" Height="150" runat="server"></cc1:CKEditorTool></td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="3">
                        <asp:Button ID="btnSave" OnClientClick="return checksave()" CssClass="btn btn-success" runat="server" Text="保存" OnClick="btnSave_Click" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
