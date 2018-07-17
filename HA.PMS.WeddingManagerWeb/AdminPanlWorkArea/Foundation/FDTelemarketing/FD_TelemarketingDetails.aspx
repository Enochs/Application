<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FD_TelemarketingDetails.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing.FD_TelemarketingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>基本信息展示</h5>
            <span class="label label-info">信息展示</span>
        </div>
        <div class="widget-content">

            <table class="table table-bordered table-striped">
                <tr>
                    <td>新郎姓名:</td>
                    <td>
                        <asp:Literal ID="ltlGroom" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>新郎生日:</td>
                    <td>
                        <asp:Literal ID="ltlGroomBirthday" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>新娘:</td>
                    <td>
                        <asp:Literal ID="ltlBride" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>新娘生日：</td>
                    <td>
                        <asp:Literal ID="ltlBrideirthday" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>新郎联系电话:</td>
                    <td>
                        <asp:Literal ID="ltlGroomCellPhone" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>新娘联系电话:</td>
                    <td>
                        <asp:Literal ID="ltlBrideCellPhone" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>经办人:</td>
                    <td>
                        <asp:Literal ID="ltlOperator" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>经办人关系:</td>
                    <td>

                        <asp:Literal ID="ltlOperatorRelationship" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>经办人联系电话:</td>
                    <td>

                        <asp:Literal ID="ltlOperatorPhone" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>婚礼预算:</td>
                    <td>

                        <asp:Literal ID="ltlPartyBudget" runat="server"></asp:Literal>（元）
                    </td>
                </tr>
                <tr>
                    <td>婚礼形式:</td>
                    <td>
                        <asp:Literal ID="ltlFormMarriage" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>喜欢的颜色:</td>
                    <td>
                        <asp:Literal ID="ltlLikeColor" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>期望气氛:
                    </td>
                    <td>
                        <asp:Literal ID="ltlExpectedAtmosphere" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>爱好特长:</td>
                    <td>
                        <asp:Literal ID="ltlHobbies" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>有无禁忌:</td>
                    <td>
                        <asp:Literal ID="ltlNoTaboos" runat="server"></asp:Literal></td>
                </tr>
                <tr>
                    <td>最看重婚礼服务项目:</td>
                    <td>
                        <asp:Literal ID="ltlWeddingServices" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>最看重的婚礼仪式流程:
                    </td>
                    <td>
                        <asp:Literal ID="ltlImportantProcess" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>相识相恋的经历:</td>
                    <td>
                        <asp:Literal ID="ltlExperience" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>期望的出场方式:</td>
                    <td>
                        <asp:Literal ID="ltlDesiredAppearance" runat="server"></asp:Literal>

                    </td>
                </tr>
            </table>
            <strong>详细信息<a href="#" title="点击我展开" id="findAll" onclick="showBox();">点击我展开</a></strong>
       
             <script type="text/javascript">
                 $(document).ready(function () {
                     $("#findAll").click(function () {
                         $("#contentDetails").toggle();
                         $("html,body").animate({ scrollTop: $("#contentDetails").offset().top - 50 }, 1000);
                     });

                 });

        </script>
    <div id="contentDetails" style=" display: none;">
        <table>
            <tr>
                <td>客户类型:</td>
                <td>
                    <asp:Literal ID="ltlCustomersType" runat="server"></asp:Literal></td>
            </tr>

            <tr>
                <td>新郎邮箱:</td>
                <td>
                    <asp:Literal ID="ltlGroomEmail" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>新娘邮箱:</td>
                <td>
                    <asp:Literal ID="ltlBrideEmail" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>新郎QQ
                </td>
                <td>
                    <asp:Literal ID="ltlGroomQQ" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>新娘QQ</td>
                <td>
                    <asp:Literal ID="ltlBrideQQ" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>新郎微信:
                </td>
                <td>
                    <asp:Literal ID="ltlGroomteWeixin" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>新娘微信:</td>
                <td>
                    <asp:Literal ID="ltlBrideWeiXin" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>新郎微博:</td>
                <td>
                    <asp:Literal ID="ltlGroomWeiBo" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>新娘微博:</td>
                <td>
                    <asp:Literal ID="ltlBrideWeiBo" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>新郎职业:
                </td>
                <td>
                    <asp:Literal ID="ltlGroomJob" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>新娘职业:</td>
                <td>
                    <asp:Literal ID="ltlBrideJob" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>新郎工作单位:</td>
                <td>
                    <asp:Literal ID="ltlGroomJobCompany" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>新娘工作单位:</td>
                <td>
                    <asp:Literal ID="ltlBrideJobCompany" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>宴会类型:</td>
                <td>
                    <asp:Literal ID="ltlBanquetTypes" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>婚礼出资人:</td>
                <td>
                    <asp:Literal ID="ltlWeddingSponsors" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>宴会日期:
                </td>
                <td>
                    <asp:Literal ID="ltlPartyDate" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>地点:</td>
                <td>
                    <asp:Literal ID="ltlAddress" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>宴会厅:
                </td>
                <td>
                    <asp:Literal ID="ltlBanquetHall" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>桌数:</td>
                <td>
                    <asp:Literal ID="ltlTableNumber" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>宴席单价:</td>
                <td>
                    <asp:Literal ID="ltlBanquetAt" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>宴席时段:</td>
                <td>
                    <asp:Literal ID="ltlDinnerTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>宴会销售:</td>
                <td>
                    <asp:Literal ID="ltlBanquetSales" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>记录人:
                </td>
                <td>
                    <asp:Literal ID="ltlRecorder" runat="server"></asp:Literal>
                </td>
            </tr>

            <tr>
                <td>门店地址:</td>
                <td>
                    <asp:Literal ID="ltlStoreAddress" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>渠道类型:</td>
                <td>
                    <asp:Literal ID="ltlChannelType" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>渠道:</td>
                <td>
                    <asp:Literal ID="ltlChannel" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>推荐人:</td>
                <td>
                    <asp:Literal ID="ltlReferee" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>来宾结构:
                </td>
                <td>
                    <asp:Literal ID="ltlGuestsStructure" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>婚礼筹备进度:
                </td>
                <td>
                    <asp:Literal ID="ltlPreparationsWedding" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>喜欢的数字:
                </td>
                <td>
                    <asp:Literal ID="ltlLikeNumber" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>最喜欢的影视:</td>
                <td>
                    <asp:Literal ID="ltlLikeFilm" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>最喜欢音乐风格:
                </td>
                <td>
                    <asp:Literal ID="ltlLikeMusic" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>最喜欢明星:
                </td>
                <td>
                    <asp:Literal ID="ltlLikePerson" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>必须规避的环节:
                </td>
                <td>
                    <asp:Literal ID="ltlVoidLink" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>最难忘的一件事:</td>
                <td>
                    <asp:Literal ID="ltlMemorable" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>求婚过程:</td>
                <td>
                    <asp:Literal ID="ltlTheProposal" runat="server"></asp:Literal>
                </td>
            </tr>

            <tr>
                <td>最难忘的礼物:
                </td>
                <td>
                    <asp:Literal ID="ltlMemorableGift" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>未被实现的愿望:</td>
                <td>

                    <asp:Literal ID="ltlAspirations" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>观摩他人婚礼的感受:
                </td>
                <td>
                    <asp:Literal ID="ltlWatchingExperience" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>父母的愿望:</td>
                <td>
                    <asp:Literal ID="ltlParentsWish" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>其他:</td>
                <td>
                    <asp:Literal ID="ltlOther" runat="server"></asp:Literal></td>
            </tr>
            <tr>
                <td>方便邀约的时间:
                </td>
                <td>
                    <asp:Literal ID="ltlConvenientIinvitationTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>客户状态:</td>
                <td>
                    <asp:Literal ID="ltlCustomerStatus" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>流失原因:
                </td>
                <td>
                    <asp:Literal ID="ltlReasons" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>员工自我分析:</td>
                <td>
                    <asp:Literal ID="ltlEmployeeSelfAnalysis" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>店长分析:
                </td>
                <td>
                    <asp:Literal ID="ltlAnalysisManager" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>跟单记录:
                </td>
                <td>
                    <asp:Literal ID="ltlDocumentaryRecord" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>跟单次数:
                </td>
                <td>
                    <asp:Literal ID="ltlInterviewTime" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>来点日期:</td>
                <td>
                    <asp:Literal ID="ltlTodate" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>酒店:
                </td>
                <td>
                    <asp:Literal ID="ltlWineshop" runat="server"></asp:Literal>
                </td>
            </tr>


        </table>

    </div>
        </div>

    </div>
</asp:Content>
