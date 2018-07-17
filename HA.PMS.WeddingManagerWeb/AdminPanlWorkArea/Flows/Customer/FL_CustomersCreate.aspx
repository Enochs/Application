<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanlWorkArea/Master/PopuMaster.Master" AutoEventWireup="true" CodeBehind="FL_CustomersCreate.aspx.cs" Inherits="HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer.FL_CustomersCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            //$("#<//%=txtRecorderDate.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            //$("#<//%=txtTodate.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            //$("#<//%=txtConvenientIinvitationTime.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            //$("#<//%=txtPartyDate.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
 
            //$("#<//%=txtGroomBirthday.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });
            //$("#<//%=txtBrideBirthday.ClientID%>").datepicker({ dateFormat: 'yy-mm-dd ' });

            $("#findAll").click(function () {
                $("#tblDetails").toggle();
                $("html,body").animate({ scrollTop: $("#tblDetails").offset().top - 80 }, 1000);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="widget-box">
        <div class="widget-title">
            <span class="icon"><i class="icon-th"></i></span>
            <h5>新人信息录入</h5>
        </div>
        <div class="widget-content ">

            <table  class="table table-bordered table-striped">
                <tr>
                    <td>新郎</td>
                    <td>
                        <asp:TextBox ID="txtGroom"    CssClass="{required:true}"  runat="server"></asp:TextBox>
                    </td>
                    <td>新娘</td>
                    <td>
                        <asp:TextBox ID="txtBride"    CssClass="{required:true}"  runat="server"></asp:TextBox>
                      
                    </td>
                    <td>经办人</td>
                    <td>
                        <asp:TextBox CssClass="{required:true}" ID="txtOperator" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新郎联系电话</td>
                    <td>
                        <asp:TextBox ID="txtGroomCellPhone"   
                            CssClass="{required:true,number:true,rangelength:[11,11],messages:{number:'你的电话未必不是数字?',required:'请输入电话号码',rangelength:'为了更准确的找到你的客户，你输入的手机号码必须是11位'}}" runat="server"></asp:TextBox>
                    </td>
                    <td>新娘联系电话</td>
                    <td>
                        <asp:TextBox ID="txtBrideCellPhone"
                             CssClass="{required:true,number:true,rangelength:[11,11],messages:{number:'你的电话未必不是数字?',required:'请输入电话号码',rangelength:'为了更准确的找到你的客户，你输入的手机号码必须是11位'}}" runat="server"></asp:TextBox>
                    </td>
                    <td>经办人联系电话</td>
                    <td>
                        <asp:TextBox ID="txtOperatorPhone"
                            CssClass="{required:true,number:true,rangelength:[11,11],messages:{number:'你的电话未必不是数字',required:'请输入电话号码',rangelength:'为了更准确的找到你的客户，你输入的手机号码必须是11位'}}"  runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>新郎生日</td>
                    <td>
                        <asp:TextBox ID="txtBrideBirthday"  onclick="WdatePicker();"  runat="server"></asp:TextBox>   
                    </td>
                    <td>新娘生日</td>
                    <td><asp:TextBox ID="txtGroomBirthday"  onclick="WdatePicker();"   runat="server"></asp:TextBox></td>
                    <td>&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;">EMail:&nbsp;</td>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: right;">EMail:&nbsp;</td>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: right;">EMail:&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;">微信:&nbsp;</td>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: right;">微信:&nbsp;</td>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: right;">微信:&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;">微博:&nbsp;</td>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: right;">微博:&nbsp;</td>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: right;">微博:&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right;">QQ:&nbsp;</td>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: right;">QQ:&nbsp;</td>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: right;">QQ:&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>经办人关系</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox CssClass="{required:true}" ID="txtOperatorRelationship" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>婚礼预算</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtPartyBudget"  CssClass="{required:true,number:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>婚礼形式</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtFormMarriage"  CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>喜欢的颜色</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtLikeColor"  CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>期望气氛</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtExpectedAtmosphere"  CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>爱好特长</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtHobbies"  CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>有无禁忌</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtNoTaboos"  CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>最看重婚礼服务项目</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtWeddingServices"  CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>最看重的婚礼仪式流程</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtImportantProcess"  CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>相识相恋的经历</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtExperience"  CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>期望的出场方式</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:TextBox ID="txtDesiredAppearance"  CssClass="{required:true}" runat="server"></asp:TextBox>
                    </td>
                </tr>

            

            </table>
            <br />
            <a class="btn btn-primary  btn-mini" href="#" id="findAll">更多信息</a>
            <hr />
            <table class="table table-bordered table-striped" id="tblDetails" >
                <tr>
                    <td >新郎邮箱</td>
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
                        <asp:TextBox ID="txtGroomtelPhone"  runat="server"></asp:TextBox>
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
                        <asp:TextBox ID="txtBrideQQ"  CssClass="{number:true,rangelength:[6,15],messages:{number:'请你输入数字',rangelength:'请你输入6到15位的qq号码'}}" runat="server"></asp:TextBox>
                    </td>
                </tr>
               <%-- <tr>
                    <td>是否流失</td>
                    <td>
                        <asp:RadioButton ID="rdoLoseYes" CssClass="ui-icon-radio-off" Checked="true" Text="流失" GroupName="Lose" runat="server" />
                        <asp:RadioButton ID="rdoLoseNo" CssClass="ui-icon-radio-off" Text="未流失" GroupName="Lose" runat="server" />
                    </td>
                </tr>--%>
                <%--<tr>
                    <td>所属状态</td>
                    <td>
                        <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                    </td>
                </tr>--%>
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
                        <asp:TextBox ID="txtPartyDate"  onclick="WdatePicker();" runat="server"></asp:TextBox></td>
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
                        <asp:TextBox ID="txtTableNumber" CssClass="{number:true}" runat="server"></asp:TextBox>
                
                        </td>
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
              <%--  <tr>
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
                        <asp:TextBox ID="txtOther" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>方便邀约的时间</td>
                    <td>
                        <asp:TextBox ID="txtConvenientIinvitationTime" onclick="WdatePicker();" runat="server"></asp:TextBox></td>
                </tr>
               <%-- <tr>
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
                        <asp:TextBox ID="txtTodate"  onclick="WdatePicker();" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>酒店</td>
                    <td>
                        <asp:TextBox ID="txtWineshop"  CssClass="{date:true}" runat="server"></asp:TextBox></td>
                </tr>


                <tr>
                    <td>录入时间</td>
                    <td>
                        <asp:TextBox ID="txtRecorderDate" onclick="WdatePicker();" runat="server"></asp:TextBox></td>
                </tr>


            </table>
            <asp:Button ID="btnSave" CssClass="btn btn-success" runat="server" Text="确定" OnClick="btnSave_Click" />
        </div>
    </div>

</asp:Content>
