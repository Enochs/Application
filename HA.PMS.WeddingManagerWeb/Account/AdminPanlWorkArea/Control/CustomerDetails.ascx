<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerDetails.ascx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control.CustomerDetails" %>
<div class="widget-box">
    <div class="widget-title">
        <span class="icon"><i class="icon-th"></i></span>
        <h5>新人基本信息</h5>
    </div>
    <div class="widget-content ">

        <table class="table table-bordered table-striped">
            <tr>
                <td width="148">新郎</td>
                <td width="336">
                    <asp:Literal ID="ltlGroom" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>新郎生日</td>
                <td>
                    <asp:Literal ID="ltlGroomBirthday" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">新娘</td>
                <td class="auto-style1">
                    <asp:Literal ID="ltlBride" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>新娘生日</td>
                <td>
                    <asp:Literal ID="ltlBrideBirthday" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>新郎联系电话</td>
                <td>
                    <asp:Literal ID="ltlGroomCellPhone" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>新娘联系电话</td>
                <td>
                    <asp:Literal ID="ltlBrideCellPhone" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>经办人</td>
                <td>
                    <asp:Literal ID="ltlOperator" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>经办人关系</td>
                <td>
                    <asp:Literal ID="ltlOperatorRelationship" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>经办人联系电话</td>
                <td>
                    <asp:Literal ID="ltlOperatorPhone" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>婚礼预算</td>
                <td>
                    <asp:Literal ID="ltlPartyBudget" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>婚礼形式</td>
                <td>
                    <asp:Literal ID="ltlFormMarriage" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>喜欢的颜色</td>
                <td>
                    <asp:Literal ID="ltlLikeColor" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>期望气氛</td>
                <td>
                    <asp:Literal ID="ltlExpectedAtmosphere" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>爱好特长</td>
                <td>
                    <asp:Literal ID="ltlHobbies" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>有无禁忌</td>
                <td>
                    <asp:Literal ID="ltlNoTaboos" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>最看重婚礼服务项目</td>
                <td>
                    <asp:Literal ID="ltlWeddingServices" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>最看重的婚礼仪式流程</td>
                <td>
                    <asp:Literal ID="ltlImportantProcess" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>相识相恋的经历</td>
                <td>
                    <asp:Literal ID="ltlExperience" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>期望的出场方式</td>
                <td>
                    <asp:Literal ID="ltlDesiredAppearance" runat="server"></asp:Literal>
                </td>
            </tr>

        </table>
    </div>
</div>




