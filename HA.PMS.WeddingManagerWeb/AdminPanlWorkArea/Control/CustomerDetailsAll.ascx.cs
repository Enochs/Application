
/**
 Version :HaoAi22
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.25
 Description:客户详细全部页面
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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control
{
    public partial class CustomerDetailsAll : System.Web.UI.UserControl
    {
        Customers objCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int CustomerID = Request.QueryString["CustomerID"].ToInt32();
                FL_Customers fL_Customers = objCustomersBLL.GetByID(CustomerID);

                #region 该代码片段是属于新人必填信息
                ltlGroom.Text = fL_Customers.Groom;
                ltlGroomBirthday.Text = fL_Customers.GroomBirthday + string.Empty;
                ltlBride.Text = fL_Customers.Bride + string.Empty;
                ltlBrideBirthday.Text = fL_Customers.BrideBirthday + string.Empty;
                ltlGroomCellPhone.Text = fL_Customers.GroomCellPhone;
                ltlBrideCellPhone.Text = fL_Customers.BrideCellPhone;
                ltlOperator.Text = fL_Customers.Operator;
                ltlOperatorRelationship.Text = fL_Customers.OperatorRelationship;
                ltlOperatorPhone.Text = fL_Customers.OperatorPhone;
                ltlPartyBudget.Text = fL_Customers.PartyBudget + string.Empty;
                ltlFormMarriage.Text = fL_Customers.FormMarriage;
                ltlLikeColor.Text = fL_Customers.LikeColor;
                ltlExpectedAtmosphere.Text = fL_Customers.ExpectedAtmosphere;
                ltlHobbies.Text = fL_Customers.Hobbies;
                ltlNoTaboos.Text = fL_Customers.NoTaboos;
                ltlWeddingServices.Text = fL_Customers.WeddingServices;
                ltlImportantProcess.Text = fL_Customers.ImportantProcess;
                ltlExperience.Text = fL_Customers.Experience;
                ltlDesiredAppearance.Text = fL_Customers.DesiredAppearance;

                #endregion

                ltlGroomEmail.Text = fL_Customers.GroomEmail;
                ltlBrideEmail.Text = fL_Customers.BrideEmail;
                ltlGroomtelPhone.Text = fL_Customers.GroomtelPhone;
                ltlBridePhone.Text = fL_Customers.BridePhone;
                ltlGroomteWeixin.Text = fL_Customers.GroomteWeixin;
                ltlBrideWeiXin.Text = fL_Customers.BrideWeiXin;
                ltlGroomWeiBo.Text = fL_Customers.GroomWeiBo;
                ltlBrideWeiBo.Text = fL_Customers.BrideWeiBo;
                ltlGroomQQ.Text = fL_Customers.GroomQQ;
                ltlBrideQQ.Text = fL_Customers.BrideQQ;


               
                ltlState.Text = fL_Customers.State + string.Empty;

                ltlGroomJob.Text = fL_Customers.GroomJob;
                ltlBrideJob.Text = fL_Customers.BrideJob;
                ltlCustomersType.Text = fL_Customers.CustomersType;
                ltlGroomJobCompany.Text = fL_Customers.GroomJobCompany;
                ltlBrideJobCompany.Text = fL_Customers.BrideJobCompany;
                ltlBanquetTypes.Text = fL_Customers.BanquetTypes;
                ltlWeddingSponsors.Text = fL_Customers.WeddingSponsors;
                ltlPartyDate.Text = fL_Customers.PartyDate + string.Empty;
                ltlAddress.Text = fL_Customers.Address;
                ltlBanquetHall.Text = fL_Customers.BanquetHall;
                ltlTableNumber.Text = fL_Customers.TableNumber + string.Empty;
                ltlBanquetAt.Text = fL_Customers.BanquetAt + string.Empty;
                ltlDinnerTime.Text = fL_Customers.DinnerTime + string.Empty;
                ltlBanquetSales.Text = fL_Customers.BanquetSales;
                ltlRecorder.Text = fL_Customers.Recorder.Value+string.Empty;
                ltlStoreAddress.Text = fL_Customers.StoreAddress;
                ltlChannelType.Text = fL_Customers.ChannelType + string.Empty;
                ltlChannel.Text = fL_Customers.Channel;
                ltlReferee.Text = fL_Customers.Referee;
                ltlGuestsStructure.Text = fL_Customers.GuestsStructure;
                ltlPreparationsWedding.Text = fL_Customers.PreparationsWedding;
                ltlLikeNumber.Text = fL_Customers.LikeNumber;
                ltlLikeFilm.Text = fL_Customers.LikeFilm;
                ltlLikeMusic.Text = fL_Customers.LikeMusic;
                ltlLikePerson.Text = fL_Customers.LikePerson;
                ltlVoidLink.Text = fL_Customers.VoidLink;
                ltlMemorable.Text = fL_Customers.Memorable;
                ltlTheProposal.Text = fL_Customers.TheProposal;
                ltlMemorableGift.Text = fL_Customers.MemorableGift;
                ltlWatchingExperience.Text = fL_Customers.WatchingExperience;
                ltlParentsWish.Text = fL_Customers.ParentsWish;
                ltlOther.Text = fL_Customers.Other;
                ltlConvenientIinvitationTime.Text = fL_Customers.ConvenientIinvitationTime;

                lblLose.Text = fL_Customers.IsLose == true ? "已流失" : "未流失";
                ltlCustomerStatus.Text = fL_Customers.CustomerStatus;

                ltlReasons.Text = fL_Customers.Reasons;
                ltlEmployeeSelfAnalysis.Text = fL_Customers.EmployeeSelfAnalysis;
                ltlAnalysisManager.Text = fL_Customers.AnalysisManager;
                ltlDocumentaryRecord.Text = fL_Customers.DocumentaryRecord;
                ltlInterviewTime.Text = fL_Customers.InterviewTime;
                ltlTodate.Text = fL_Customers.Todate + string.Empty;
                ltlWineshop.Text = fL_Customers.Wineshop;
                ltlRecorderDate.Text = fL_Customers.RecorderDate + string.Empty;

            }
        }
    }
}