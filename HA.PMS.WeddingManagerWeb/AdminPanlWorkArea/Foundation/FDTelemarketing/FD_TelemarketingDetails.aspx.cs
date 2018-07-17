/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.14
 Description:客户基本信息管理页面
 History:修改日志
 （客户电话营销）
 Author:杨洋
 date:2013.3.14
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
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FDTelemarketing
{
    public partial class FD_TelemarketingDetails : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                //72 9 
                //获取从客户管理页面中传递过来的参数
                int CustomerID = Request.QueryString["CustomerID"].ToInt32();
                FL_Customers fl_Customers = objCustomersBLL.GetByID(CustomerID);
                // 由于该表信息较多，不对下列字段一一注释，如有不解请参考数据库说明
                #region 针对各个属性赋值
                ltlGroom.Text = fl_Customers.Groom;
                ltlGroomBirthday.Text = fl_Customers.GroomBirthday.ToString();
                ltlBride.Text = fl_Customers.Bride;

                ltlBrideirthday.Text = fl_Customers.BrideBirthday.ToString();
                ltlGroomCellPhone.Text = fl_Customers.GroomCellPhone;
                ltlBrideCellPhone.Text = fl_Customers.BrideCellPhone;
                

                ltlOperator.Text = fl_Customers.Operator;
                ltlOperatorRelationship.Text = fl_Customers.OperatorRelationship;
                ltlOperatorPhone.Text = fl_Customers.OperatorPhone;
                ltlPartyBudget.Text = fl_Customers.PartyBudget.ToString();
                ltlFormMarriage.Text = fl_Customers.FormMarriage;
                ltlLikeColor.Text = fl_Customers.LikeColor;
                ltlExpectedAtmosphere.Text = fl_Customers.ExpectedAtmosphere;
                ltlHobbies.Text = fl_Customers.Hobbies;
                ltlNoTaboos.Text = fl_Customers.NoTaboos;
                ltlWeddingServices.Text = fl_Customers.WeddingServices;
                ltlImportantProcess.Text = fl_Customers.ImportantProcess;
                ltlExperience.Text = fl_Customers.Experience;
                ltlDesiredAppearance.Text = fl_Customers.DesiredAppearance;

                ltlCustomersType.Text = fl_Customers.CustomersType;
                ltlGroomEmail.Text = fl_Customers.GroomEmail;
                ltlBrideEmail.Text = fl_Customers.BrideEmail;
                ltlGroomQQ.Text = fl_Customers.GroomQQ.ToString();
                ltlBrideQQ.Text = fl_Customers.BrideQQ.ToString();
                ltlGroomteWeixin.Text = fl_Customers.GroomteWeixin;
                ltlBrideWeiXin.Text = fl_Customers.BrideWeiXin;
                ltlGroomWeiBo.Text = fl_Customers.GroomWeiBo;
                ltlBrideWeiBo.Text = fl_Customers.BrideWeiBo;
                ltlGroomJob.Text = fl_Customers.GroomJob;


                ltlBrideJob.Text = fl_Customers.BrideJob;
                ltlGroomJobCompany.Text = fl_Customers.GroomJobCompany;
                ltlBrideJobCompany.Text = fl_Customers.BrideJobCompany;
                ltlBanquetTypes.Text = fl_Customers.BanquetTypes;
                ltlWeddingSponsors.Text = fl_Customers.WeddingSponsors;
                ltlPartyDate.Text = fl_Customers.PartyDate.ToString();
                ltlAddress.Text = fl_Customers.Address;
                ltlBanquetHall.Text = fl_Customers.BanquetHall;
                ltlTableNumber.Text = fl_Customers.TableNumber.ToString();
                ltlBanquetAt.Text = fl_Customers.BanquetAt.ToString();
                ltlDinnerTime.Text = fl_Customers.DinnerTime;
                ltlBanquetSales.Text = fl_Customers.BanquetSales;
                ltlRecorder.Text = fl_Customers.Recorder.Value+string.Empty;
                ltlStoreAddress.Text = fl_Customers.StoreAddress;
                ltlChannelType.Text = fl_Customers.ChannelType.ToString();
                ltlChannel.Text = fl_Customers.Channel;
                ltlReferee.Text = fl_Customers.Referee;
                ltlGuestsStructure.Text = fl_Customers.GuestsStructure;
                ltlPreparationsWedding.Text = fl_Customers.PreparationsWedding;
                ltlLikeNumber.Text = fl_Customers.LikeNumber;
                ltlLikeFilm.Text = fl_Customers.LikeFilm;
                ltlLikeMusic.Text = fl_Customers.LikeMusic;
                ltlLikePerson.Text = fl_Customers.LikePerson;
                ltlVoidLink.Text = fl_Customers.VoidLink;
                ltlMemorable.Text = fl_Customers.Memorable;
                ltlTheProposal.Text = fl_Customers.TheProposal;
                ltlMemorableGift.Text = fl_Customers.MemorableGift;
                ltlAspirations.Text = fl_Customers.Aspirations;
                ltlWatchingExperience.Text = fl_Customers.WatchingExperience;
                ltlParentsWish.Text = fl_Customers.ParentsWish;
                ltlOther.Text = fl_Customers.Other;
                ltlConvenientIinvitationTime.Text = fl_Customers.ConvenientIinvitationTime;
                ltlCustomerStatus.Text = fl_Customers.CustomerStatus;
                ltlReasons.Text = fl_Customers.Reasons;
                ltlEmployeeSelfAnalysis.Text = fl_Customers.EmployeeSelfAnalysis;
                ltlAnalysisManager.Text = fl_Customers.AnalysisManager;
                ltlDocumentaryRecord.Text = fl_Customers.DocumentaryRecord;
                ltlInterviewTime.Text = fl_Customers.InterviewTime;
                ltlTodate.Text = fl_Customers.Todate.ToString();
                ltlWineshop.Text = fl_Customers.Wineshop;
                #endregion

            }
        }
    }
}