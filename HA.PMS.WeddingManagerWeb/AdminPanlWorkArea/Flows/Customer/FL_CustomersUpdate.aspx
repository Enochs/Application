<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FL_CustomersUpdate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.FL_CustomersUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtRecorderDate.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            $("#<%=txtTodate.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            $("#<%=txtConvenientIinvitationTime.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            $("#<%=txtPartyDate.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            $("#<%=txtBrideBirthday.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            $("#<%=txtGroomBirthday.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            $(":text").click(function() {
                $(this).select();
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>客户信息修改界面</h5>
           
        </div>
        <div class="widget-content ">

            <table class="table table-bordered table-striped">
                <tr>
                    <td>新郎</td>
                    <td>
                        <asp:TextBox ID="txtGroom" CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新郎生日</td>
                    <td>
                        <asp:TextBox ID="txtGroomBirthday" CssClass="{required:true,date:true}"  runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新娘</td>
                    <td>
                        <asp:TextBox ID="txtBride" CssClass="{required:true}"  runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新娘生日</td>
                    <td>
                        <asp:TextBox ID="txtBrideBirthday"  CssClass="{required:true,date:true}"  runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新郎联系电话</td>
                    <td>
                        <asp:TextBox ID="txtGroomCellPhone"  CssClass="{required:true,number:true,rangelength:[11,11],messages:{number:'你的电话未必不是数字?',required:'请输入电话号码',rangelength:'为了更准确的找到你的客户，你输入的手机号码必须是11位'}}"  runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新娘联系电话</td>
                    <td>
                        <asp:TextBox ID="txtBrideCellPhone"  CssClass="{required:true,number:true,rangelength:[11,11],messages:{number:'你的电话未必不是数字?',required:'请输入电话号码',rangelength:'为了更准确的找到你的客户，你输入的手机号码必须是11位'}}"  runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>经办人</td>
                    <td>
                        <asp:TextBox ID="txtOperator" CssClass="{required:true}"  runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>经办人关系</td>
                    <td>
                        <asp:TextBox ID="txtOperatorRelationship" CssClass="{required:true}"  runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>经办人联系电话</td>
                    <td>
                        <asp:TextBox ID="txtOperatorPhone"  CssClass="{required:true,number:true,rangelength:[11,11],messages:{number:'你的电话未必不是数字?',required:'请输入电话号码',rangelength:'为了更准确的找到你的客户，你输入的手机号码必须是11位'}}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>婚礼预算</td>
                    <td>
                        <asp:TextBox ID="txtPartyBudget" CssClass="{required:true,number:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>婚礼形式</td>
                    <td>
                        <asp:TextBox ID="txtFormMarriage" CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>喜欢的颜色</td>
                    <td>
                        <asp:TextBox ID="txtLikeColor" CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>期望气氛</td>
                    <td>
                        <asp:TextBox ID="txtExpectedAtmosphere" CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>爱好特长</td>
                    <td>
                        <asp:TextBox ID="txtHobbies" CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>有无禁忌</td>
                    <td>
                        <asp:TextBox ID="txtNoTaboos" CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>最看重婚礼服务项目</td>
                    <td>
                        <asp:TextBox ID="txtWeddingServices" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>最看重的婚礼仪式流程</td>
                    <td>
                        <asp:TextBox ID="txtImportantProcess" CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>相识相恋的经历</td>
                    <td>
                        <asp:TextBox ID="txtExperience" CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>期望的出场方式</td>
                    <td>
                        <asp:TextBox ID="txtDesiredAppearance" CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>

            </table>

            <p style="width: 100%; border-bottom: 1px solid #66c83c;">更多信息</p>
            <table>
                <tr>
                    <td>新郎邮箱</td>
                    <td>
                        <asp:TextBox ID="txtGroomEmail" CssClass="{email:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新娘邮箱</td>
                    <td>
                        <asp:TextBox ID="txtBrideEmail" CssClass="{email:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新郎座机</td>
                    <td>
                        <asp:TextBox ID="txtGroomtelPhone" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新娘座机</td>
                    <td>
                        <asp:TextBox ID="txtBridePhone" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新郎微信</td>
                    <td>
                        <asp:TextBox ID="txtGroomteWeixin" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新娘微信</td>
                    <td>
                        <asp:TextBox ID="txtBrideWeiXin" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新郎微博</td>
                    <td>
                        <asp:TextBox ID="txtGroomWeiBo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新娘微博</td>
                    <td>
                        <asp:TextBox ID="txtBrideWeiBo" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新郎QQ</td>
                    <td>
                        <asp:TextBox ID="txtGroomQQ" CssClass="{number:true,rangelength:[6,15],messages:{number:'请你输入数字',rangelength:'请你输入6到15位的qq号码'}}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新娘QQ</td>
                    <td>
                        <asp:TextBox ID="txtBrideQQ" CssClass="{number:true,rangelength:[6,15],messages:{number:'请你输入数字',rangelength:'请你输入6到15位的qq号码'}}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>是否流失</td>
                    <td>
                        <asp:RadioButton ID="rdoLoseYes" Checked="true" Text="流失" GroupName="Lose" runat="server" />
                        <asp:RadioButton ID="rdoLoseNo" Text="未流失" GroupName="Lose" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>所属状态</td>
                    <td>
                        <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新郎职业</td>
                    <td>
                        <asp:TextBox ID="txtGroomJob" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新娘职业</td>
                    <td>
                        <asp:TextBox ID="txtBrideJob" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>客户类型</td>
                    <td>
                        <asp:TextBox ID="txtCustomersType" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>新郎工作单位</td>
                    <td>
                        <asp:TextBox ID="txtGroomJobCompany" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>新娘工作单位</td>
                    <td>
                        <asp:TextBox ID="txtBrideJobCompany" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>宴会类型</td>
                    <td>
                        <asp:TextBox ID="txtBanquetTypes" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>婚礼出资人</td>
                    <td>
                        <asp:TextBox ID="txtWeddingSponsors" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>宴会日期</td>
                    <td>
                        <asp:TextBox ID="txtPartyDate" CssClass="{date:true}" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>地点</td>
                    <td>
                        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>宴会厅</td>
                    <td>
                        <asp:TextBox ID="txtBanquetHall" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>桌数</td>
                    <td>
                        <asp:TextBox ID="txtTableNumber" CssClass="{number:true}" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>宴席单价</td>
                    <td>
                        <asp:TextBox ID="txtBanquetAt" CssClass="{number:true}" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>宴席时段</td>
                    <td>
                        <asp:TextBox ID="txtDinnerTime" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>宴席销售</td>
                    <td>
                        <asp:TextBox ID="txtBanquetSales" runat="server"></asp:TextBox></td>
                </tr>
               <%-- <tr>
                    <td>记录人</td>
                    <td>
                        <asp:TextBox ID="txtRecorder" runat="server"></asp:TextBox></td>
                </tr>--%>
                <tr>
                    <td>门面地址</td>
                    <td>
                        <asp:TextBox ID="txtStoreAddress" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>渠道类型</td>
                    <td>
                        <asp:TextBox ID="txtChannelType" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>渠道</td>
                    <td>
                        <asp:TextBox ID="txtChannel" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>推荐人</td>
                    <td>
                        <asp:TextBox ID="txtReferee" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>来宾结构</td>
                    <td>
                        <asp:TextBox ID="txtGuestsStructure" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>婚礼筹备进度</td>
                    <td>
                        <asp:TextBox ID="txtPreparationsWedding" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>喜欢的数字</td>
                    <td>
                        <asp:TextBox ID="txtLikeNumber" CssClass="{number:true}" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>最喜欢的影视</td>
                    <td>
                        <asp:TextBox ID="txtLikeFilm" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>最喜欢的音乐</td>
                    <td>
                        <asp:TextBox ID="txtLikeMusic" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>最喜欢的明细</td>
                    <td>
                        <asp:TextBox ID="txtLikePerson" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>必须规避的环节</td>
                    <td>
                        <asp:TextBox ID="txtVoidLink" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>难忘的事</td>
                    <td>
                        <asp:TextBox ID="txtMemorable" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>求婚过程</td>
                    <td>
                        <asp:TextBox ID="txtTheProposal" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>难忘礼物</td>
                    <td>
                        <asp:TextBox ID="txtMemorableGift" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>没有实现的愿望</td>
                    <td>
                        <asp:TextBox ID="txtAspirations" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>观摩其他的人婚礼的礼物</td>
                    <td>
                        <asp:TextBox ID="txtWatchingExperience" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>父母愿望</td>
                    <td>
                        <asp:TextBox ID="txtParentsWish" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>其他</td>
                    <td>
                        <asp:TextBox ID="txtOther" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>方便邀约的时间</td>
                    <td>
                        <asp:TextBox ID="txtConvenientIinvitationTime" CssClass="{date:true}" runat="server"></asp:TextBox></td>
                </tr>
              <%--  <tr>
                    <td>客户状态</td>
                    <td>
                        <asp:TextBox ID="txtCustomerStatus" runat="server"></asp:TextBox></td>
                </tr>--%>
                <tr>
                    <td>流失原因</td>
                    <td>
                        <asp:TextBox ID="txtReasons" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>员工自我分析</td>
                    <td>
                        <asp:TextBox ID="txtEmployeeSelfAnalysis" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>店长分析</td>
                    <td>
                        <asp:TextBox ID="txtAnalysisManager" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>跟单记录</td>
                    <td>
                        <asp:TextBox ID="txtDocumentaryRecord" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>跟单次数</td>
                    <td>
                        <asp:TextBox ID="txtInterviewTime" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>来店日期</td>
                    <td>
                        <asp:TextBox ID="txtTodate" CssClass="{date:true}" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>酒店</td>
                    <td>
                        <asp:TextBox ID="txtWineshop" runat="server"></asp:TextBox></td>
                </tr>


                <tr>
                    <td>录入时间</td>
                    <td>
                        <asp:TextBox ID="txtRecorderDate" CssClass="{date:true}" runat="server"></asp:TextBox></td>
                </tr>

                <tr>
                    <td>
                        <asp:Button  CssClass="btn btn-success" ID="btnSave" runat="server" Text="确定" OnClick="btnSave_Click" /></td>
                    <td></td>
                </tr>

            </table>

        </div>
    </div>
</asp:Content>
