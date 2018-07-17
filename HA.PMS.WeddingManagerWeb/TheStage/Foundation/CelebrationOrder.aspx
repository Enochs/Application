<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CelebrationOrder.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.TheStage.Foundation.CelebrationOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #ulEmployee {
            width: 800px;
            list-style: none;
        }

            #ulEmployee li {
                width: 100px;
                height: 120px;
                float: left;
                margin-right: 15px;
                line-height: 20px;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".queryTable td").css({ "border": "0", "vertical-align": "top" });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>套系/经典案例介绍详细页面</h5>

        </div>
        <div class="widget-content">
            <table class="queryTable">
                <tr>
                    <td>套系名称</td>
                    <td>风格</td>
                    <td>价格</td>
                    <td>优惠</td>
                </tr>
            </table>

            <hr style="border: 1px solid #7dee8c;" />
            <ul id="ulEmployee">
                <asp:Repeater ID="rptCelerationImg"  runat="server">
                    <ItemTemplate>

                        <li>

                        </li>

                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</asp:Content>
