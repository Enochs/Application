
/**
 Version :HaoAi22
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.25
 Description:修改客户页面
 History:修改日志

 Author:杨洋
 date:2013.3.25
 version:好爱1.0
 description:修改描述
 
 
 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Customer
{
    public partial class FL_CustomersUpdate : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region
                //Type state = typeof(CustomerStates);
                //Array Arrays = Enum.GetValues(state);
                //for (int i = 0; i < Arrays.LongLength; i++)
                //{
                //    int val = (Arrays.GetValue(i) + string.Empty).ToInt32();
                //    if (val<=6)
                //    {
                //         CustomerState.GetEnumDescription(val);
                //    }

                //}
                #endregion
                int CustomerID = Request.QueryString["CustomerID"].ToInt32();
                FL_Customers fL_Customers = objCustomersBLL.GetByID(CustomerID);

                #region 该代码片段是属于新人必填信息
                txtGroom.Text = fL_Customers.Groom;
                txtGroomBirthday.Text = fL_Customers.GroomBirthday + string.Empty;
                txtBride.Text = fL_Customers.Bride + string.Empty;
                txtBrideBirthday.Text = fL_Customers.BrideBirthday + string.Empty;
                txtGroomCellPhone.Text = fL_Customers.GroomCellPhone;
                txtBrideCellPhone.Text = fL_Customers.BrideCellPhone;
                txtOperator.Text = fL_Customers.Operator;
                txtOperatorRelationship.Text = fL_Customers.OperatorRelationship;
                txtOperatorPhone.Text = fL_Customers.OperatorPhone;
                txtPartyBudget.Text = fL_Customers.PartyBudget + string.Empty;
                txtFormMarriage.Text = fL_Customers.FormMarriage;
                txtLikeColor.Text = fL_Customers.LikeColor;
                txtExpectedAtmosphere.Text = fL_Customers.ExpectedAtmosphere;
                txtHobbies.Text = fL_Customers.Hobbies;
                txtNoTaboos.Text = fL_Customers.NoTaboos;
                txtWeddingServices.Text = fL_Customers.WeddingServices;
                txtImportantProcess.Text = fL_Customers.ImportantProcess;
                txtExperience.Text = fL_Customers.Experience;
                txtDesiredAppearance.Text = fL_Customers.DesiredAppearance;

                #endregion


                txtGroomEmail.Text = fL_Customers.GroomEmail;
                txtBrideEmail.Text = fL_Customers.BrideEmail;
                txtGroomtelPhone.Text = fL_Customers.GroomtelPhone;
                txtBridePhone.Text = fL_Customers.BridePhone;
                txtGroomteWeixin.Text = fL_Customers.GroomteWeixin;
                txtBrideWeiXin.Text = fL_Customers.BrideWeiXin;
                txtGroomWeiBo.Text = fL_Customers.GroomWeiBo;
                txtBrideWeiBo.Text = fL_Customers.BrideWeiBo;
                txtGroomQQ.Text = fL_Customers.GroomQQ + string.Empty;
                txtBrideQQ.Text = fL_Customers.BrideQQ + string.Empty;


                rdoLoseNo.Checked = fL_Customers.IsLose == true ? false : true;
                txtState.Text = fL_Customers.State + string.Empty;

                txtGroomJob.Text = fL_Customers.GroomJob;
                txtBrideJob.Text = fL_Customers.BrideJob;
                txtCustomersType.Text = fL_Customers.CustomersType;
                txtGroomJobCompany.Text = fL_Customers.GroomJobCompany;
                txtBrideJobCompany.Text = fL_Customers.BrideJobCompany;
                txtBanquetTypes.Text = fL_Customers.BanquetTypes;
                txtWeddingSponsors.Text = fL_Customers.WeddingSponsors;
                txtPartyDate.Text = fL_Customers.PartyDate + string.Empty;
                txtAddress.Text = fL_Customers.Address;
                txtBanquetHall.Text = fL_Customers.BanquetHall;
                txtTableNumber.Text = fL_Customers.TableNumber + string.Empty;
                txtBanquetAt.Text = fL_Customers.BanquetAt + string.Empty;
                txtDinnerTime.Text = fL_Customers.DinnerTime + string.Empty;
                txtBanquetSales.Text = fL_Customers.BanquetSales;
                //txtRecorder.Text = fL_Customers.Recorder;
                txtStoreAddress.Text = fL_Customers.StoreAddress;
                txtChannelType.Text = fL_Customers.ChannelType + string.Empty;
                txtChannel.Text = fL_Customers.Channel;
                txtReferee.Text = fL_Customers.Referee;
                txtGuestsStructure.Text = fL_Customers.GuestsStructure;
                txtPreparationsWedding.Text = fL_Customers.PreparationsWedding;
                txtLikeNumber.Text = fL_Customers.LikeNumber;
                txtLikeFilm.Text = fL_Customers.LikeFilm;
                txtLikeMusic.Text = fL_Customers.LikeMusic;
                txtLikePerson.Text = fL_Customers.LikePerson;
                txtVoidLink.Text = fL_Customers.VoidLink;
                txtMemorable.Text = fL_Customers.Memorable;
                txtTheProposal.Text = fL_Customers.TheProposal;
                txtMemorableGift.Text = fL_Customers.MemorableGift;
                txtWatchingExperience.Text = fL_Customers.WatchingExperience;
                txtParentsWish.Text = fL_Customers.ParentsWish;
                txtOther.Text = fL_Customers.Other;
                txtConvenientIinvitationTime.Text = fL_Customers.ConvenientIinvitationTime;


              //  txtCustomerStatus.Text = fL_Customers.CustomerStatus;

                txtReasons.Text = fL_Customers.Reasons;
                txtEmployeeSelfAnalysis.Text = fL_Customers.EmployeeSelfAnalysis;
                txtAnalysisManager.Text = fL_Customers.AnalysisManager;
                txtDocumentaryRecord.Text = fL_Customers.DocumentaryRecord;
                txtInterviewTime.Text = fL_Customers.InterviewTime;
                txtTodate.Text = fL_Customers.Todate + string.Empty;
                txtWineshop.Text = fL_Customers.Wineshop;
                txtRecorderDate.Text = fL_Customers.RecorderDate+string.Empty;


            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            int CustomerID = Request.QueryString["CustomerID"].ToInt32();
            FL_Customers fL_Customers = objCustomersBLL.GetByID(CustomerID);


            #region 该代码片段是属于新人必填信息
            fL_Customers.Groom = txtGroom.Text;
            fL_Customers.GroomBirthday = txtGroomBirthday.Text.ToDateTime();
            fL_Customers.Bride = txtBride.Text;
            fL_Customers.BrideBirthday = txtBrideBirthday.Text.ToDateTime();
            fL_Customers.GroomCellPhone = txtGroomCellPhone.Text;
            fL_Customers.BrideCellPhone = txtBrideCellPhone.Text;
            fL_Customers.Operator = txtOperator.Text;
            fL_Customers.OperatorRelationship = txtOperatorRelationship.Text;
            fL_Customers.OperatorPhone = txtOperatorPhone.Text;
            fL_Customers.PartyBudget = txtPartyBudget.Text.ToDecimal();
            fL_Customers.FormMarriage = txtFormMarriage.Text;
            fL_Customers.LikeColor = txtLikeColor.Text;
            fL_Customers.ExpectedAtmosphere = txtExpectedAtmosphere.Text;
            fL_Customers.Hobbies = txtHobbies.Text;
            fL_Customers.NoTaboos = txtNoTaboos.Text;
            fL_Customers.WeddingServices = txtWeddingServices.Text;
            fL_Customers.ImportantProcess = txtImportantProcess.Text;
            fL_Customers.Experience = txtExperience.Text;
            fL_Customers.DesiredAppearance = txtDesiredAppearance.Text;
            #endregion

            fL_Customers.GroomEmail = txtGroomEmail.Text;
            fL_Customers.BrideEmail = txtBrideEmail.Text;
            fL_Customers.GroomtelPhone = txtGroomtelPhone.Text;
            fL_Customers.BridePhone = txtBridePhone.Text;
            fL_Customers.GroomteWeixin = txtGroomteWeixin.Text;
            fL_Customers.BrideWeiXin = txtBrideWeiXin.Text;
            fL_Customers.GroomWeiBo = txtGroomWeiBo.Text;
            fL_Customers.BrideWeiBo = txtBrideWeiBo.Text;
            fL_Customers.GroomQQ = txtGroomQQ.Text;

            fL_Customers.IsLose = rdoLoseNo.Checked ? false : true;

            fL_Customers.State = txtState.Text.ToInt32();
            fL_Customers.GroomJob = txtGroomJob.Text;
            fL_Customers.BrideJob = txtBrideJob.Text;
            fL_Customers.CustomersType = txtCustomersType.Text;
            fL_Customers.GroomJobCompany = txtGroomJobCompany.Text;
            fL_Customers.BrideJobCompany = txtBrideJobCompany.Text;
            fL_Customers.BanquetTypes = txtBanquetTypes.Text;
            fL_Customers.WeddingSponsors = txtWeddingSponsors.Text;
            fL_Customers.PartyDate = txtPartyDate.Text.ToDateTime();
            fL_Customers.Address = txtAddress.Text;
            fL_Customers.BanquetHall = txtBanquetHall.Text;
            fL_Customers.TableNumber = txtTableNumber.Text.ToInt32();
            fL_Customers.BanquetAt = txtBanquetAt.Text.ToDecimal();
            fL_Customers.DinnerTime = txtDinnerTime.Text;
            fL_Customers.BanquetSales = txtBanquetSales.Text;
         //   fL_Customers.Recorder = txtRecorder.Text;
            fL_Customers.StoreAddress = txtStoreAddress.Text;
            fL_Customers.ChannelType = txtChannelType.Text.ToInt32();
            fL_Customers.Channel = txtChannel.Text;
            fL_Customers.Referee = txtReferee.Text;
            fL_Customers.GuestsStructure = txtGuestsStructure.Text;
            fL_Customers.PreparationsWedding = txtPreparationsWedding.Text;
            fL_Customers.LikeNumber = txtLikeNumber.Text;
            fL_Customers.LikeFilm = txtLikeFilm.Text;
            fL_Customers.LikeMusic = txtLikeMusic.Text;
            fL_Customers.LikePerson = txtLikePerson.Text;
            fL_Customers.VoidLink = txtVoidLink.Text;
            fL_Customers.Memorable = txtMemorable.Text;
            fL_Customers.TheProposal = txtTheProposal.Text;
            fL_Customers.MemorableGift = txtMemorableGift.Text;
            fL_Customers.WatchingExperience = txtWatchingExperience.Text;
            fL_Customers.ParentsWish = txtParentsWish.Text;
            fL_Customers.Other = txtOther.Text;
            fL_Customers.ConvenientIinvitationTime = txtConvenientIinvitationTime.Text;



          //  fL_Customers.CustomerStatus = txtCustomerStatus.Text;
            fL_Customers.Reasons = txtReasons.Text;

            fL_Customers.EmployeeSelfAnalysis = txtEmployeeSelfAnalysis.Text;
            fL_Customers.AnalysisManager = txtAnalysisManager.Text;
            fL_Customers.DocumentaryRecord = txtDocumentaryRecord.Text;
            fL_Customers.InterviewTime = txtInterviewTime.Text;
            fL_Customers.Todate = txtTodate.Text.ToDateTime();
            fL_Customers.Wineshop = txtWineshop.Text;
            fL_Customers.RecorderDate = txtRecorderDate.Text.ToDateTime();

            int result = objCustomersBLL.Update(fL_Customers);
            //根据返回判断添加的状态
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("修改成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("修改失败,请重新尝试", this.Page);

            }
        }


    }
}