<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_SaleSourcesDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_SaleSources.FD_SaleSourcesDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5></h5>
            <span class="label label-info"></span>
        </div>
        <div class="widget-content">
            <table class="table table-bordered table-striped">

                <tr>
                    <td>渠道名称:</td>
                    <td>
                        <asp:Literal ID="ltlSourcename" runat="server"></asp:Literal>
                    </td>
                    <td>拓展时间</td>
                    <td>

                        <asp:Literal ID="ltlProlongationDate" runat="server"></asp:Literal>
                    </td>
                    <td>拓展人</td>
                    <td>
                        <asp:Literal ID="ltlProlongationEmployee" runat="server"></asp:Literal>

                    </td>
                    <td>维护人</td>
                    <td>


                        <asp:Literal ID="ltlMaintenanceEmployee" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>渠道类型</td>
                    <td>
                        <asp:Literal ID="ltlChannelType" runat="server"></asp:Literal>


                    </td>
                    <td>是否返利</td>
                    <td>

                        <asp:Literal ID="ltlNeedRebate" runat="server"></asp:Literal>
                    </td>
                    <td>地址</td>
                    <td>
                        <asp:Literal ID="ltlAddress" runat="server"></asp:Literal></td>
                    <td>传真</td>
                    <td>
                        <asp:Literal ID="ltlFax" runat="server"></asp:Literal></td>

                </tr>
            </table>

            <br />
            <div class="widget-title">
                <span class="icon"><i class="icon-th"></i></span>
                <h5>渠道联系人</h5>
                <span class="label label-info">联系人</span>
            </div>
            <table class="table table-bordered table-striped">
                <tr>
                    <td>渠道联系人1</td>
                    <td>
                        <asp:Literal ID="ltlTactcontacts1" runat="server"></asp:Literal></td>
                    <td>渠道联系人2</td>
                    <td>
                        <asp:Literal ID="ltlTactcontacts2" runat="server"></asp:Literal></td>
                    <td>渠道联系人3</td>
                    <td>
                        <asp:Literal ID="ltlTactcontacts3" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>联系人类型1</td>
                    <td>
                        <asp:Literal ID="ltlTactcontactsType1" runat="server"></asp:Literal></td>
                    <td>联系人类型2</td>
                    <td>
                        <asp:Literal ID="ltlTactcontactsType2" runat="server"></asp:Literal></td>
                    <td>联系人类型3</td>
                    <td>
                        <asp:Literal ID="ltlTactcontactsType3" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>联系人职务1</td>
                    <td>
                        <asp:Literal ID="ltlTactcontactsJob1" runat="server"></asp:Literal></td>
                    <td>联系人职务2</td>
                    <td>
                        <asp:Literal ID="ltlTactcontactsJob2" runat="server"></asp:Literal></td>
                    <td>联系人职务3</td>
                    <td>
                        <asp:Literal ID="ltlTactcontactsJob3" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>联系人手机1</td>
                    <td>
                        <asp:Literal ID="ltlTactcontactsPhone1" runat="server"></asp:Literal></td>
                    <td>联系人手机2</td>
                    <td>
                        <asp:Literal ID="ltlTactcontactsPhone2" runat="server"></asp:Literal></td>
                    <td>联系人手机3</td>
                    <td>
                        <asp:Literal ID="ltlTactcontactsPhone3" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>Email1</td>
                    <td>
                        <asp:Literal ID="ltlEmail1" runat="server"></asp:Literal></td>
                    <td>Email2</td>
                    <td>
                        <asp:Literal ID="ltlEmail2" runat="server"></asp:Literal></td>
                    <td>Email3</td>
                    <td>
                        <asp:Literal ID="ltlEmail3" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>QQ1</td>
                    <td>
                        <asp:Literal ID="ltlQQ1" runat="server"></asp:Literal></td>
                    <td>QQ2</td>
                    <td>
                        <asp:Literal ID="ltlQQ2" runat="server"></asp:Literal></td>
                    <td>QQ3</td>
                    <td>
                        <asp:Literal ID="ltlQQ3" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>微博1</td>
                    <td>
                        <asp:Literal ID="ltlWeibo1" runat="server"></asp:Literal></td>
                    <td>微博2</td>
                    <td>
                        <asp:Literal ID="ltlWeibo2" runat="server"></asp:Literal></td>
                    <td>微博3</td>
                    <td>
                        <asp:Literal ID="ltlWeibo3" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>微信1</td>
                    <td>
                        <asp:Literal ID="ltlWenXin1" runat="server"></asp:Literal></td>
                    <td>微信2</td>
                    <td>
                        <asp:Literal ID="ltlWenXin2" runat="server"></asp:Literal></td>
                    <td>微信3</td>
                    <td>
                        <asp:Literal ID="ltlWenXin3" runat="server"></asp:Literal></td>
                </tr>
            </table>

            <br />
            <hr />

            <div class="widget-title">
                <span class="icon"><i class="icon-th"></i></span>
                <h5>其他信息</h5>
                <span class="label label-info">信息</span>
            </div>
            <table style="table-layout:fixed;word-break:break-all;word-wrap:break-word" class="table table-bordered table-striped">
                <tr>
                    <td style="width:85px">渠道简介</td>
                    <td><asp:Label ID="ltlSynopsis" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>渠道优惠政策</td>
                    <td><asp:Label ID="ltlPreferentialpolicy" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>返利政策</td>
                    <td><asp:Label BorderColor="Black" ID="ltlRebatepolicy" runat="server"></asp:Label></td>
                </tr>
                <tr><td colspan="2"></td></tr>
            </table>
        </div>
    </div>
</asp:Content>
