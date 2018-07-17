
/**
 Version :HaoAi22
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.25
 Description:添加客户页面
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
    public partial class FL_CustomersCreate : SystemPage
    {
        Customers objCustomersBLL = new Customers();
        HA.PMS.BLLAssmblly.Flow.Telemarketing ObjTelemarketingsBLL = new BLLAssmblly.Flow.Telemarketing();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                txtInterviewTime.Text = DateTime.Now + string.Empty;
                txtPartyDate.Text = DateTime.Now + string.Empty;
                txtRecorderDate.Text = DateTime.Now + string.Empty;
                txtTodate.Text = DateTime.Now + string.Empty;
                txtGroomBirthday.Text = DateTime.Now + string.Empty;
                txtConvenientIinvitationTime.Text = DateTime.Now + string.Empty;
                txtBrideBirthday.Text = DateTime.Now + string.Empty;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            FL_Customers fL_Customers = new FL_Customers();
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
            fL_Customers.BrideQQ = txtBrideQQ.Text;

            //  fL_Customers.IsLose = rdoLoseNo.Checked ? false : true;
            //设置为未流失
            fL_Customers.IsLose = false;
            //fL_Customers.State = txtState.Text.ToInt32();
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
            //fL_Customers.Recorder = txtRecorder.Text;
            //记录人
            fL_Customers.Recorder = User.Identity.Name.ToInt32();
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
            //此时为直接到店
            fL_Customers.CustomerStatus = CustomerState.GetEnumDescription(CustomerStates.DidNotFollowOrder);

            fL_Customers.Reasons = txtReasons.Text;

            fL_Customers.EmployeeSelfAnalysis = txtEmployeeSelfAnalysis.Text;
            fL_Customers.AnalysisManager = txtAnalysisManager.Text;
            fL_Customers.DocumentaryRecord = txtDocumentaryRecord.Text;
            fL_Customers.InterviewTime = txtInterviewTime.Text;
            fL_Customers.Todate = txtTodate.Text.ToDateTime();
            fL_Customers.Wineshop = txtWineshop.Text;
            fL_Customers.RecorderDate = txtRecorderDate.Text.ToDateTime();
            //新录入的ID
            int newCustomersId = objCustomersBLL.Insert(fL_Customers);
            //把之前添加的客户添加到电话营销表中
            HA.PMS.DataAssmblly.FL_Telemarketing telemarketing = new FL_Telemarketing();
            //责任人和创建人，在这里都默认是当前录入信息的员工ID

            int empId=User.Identity.Name.ToInt32();
            telemarketing.EmployeeID = empId;
            telemarketing.CreateEmpLoyee = empId;

            telemarketing.CustomerID = newCustomersId;
            //获取最大批次量返回时加 一 更新批次量
            telemarketing.SortOrder = ObjTelemarketingsBLL.GetMaxSortOrder() + 1;
            telemarketing.CreateDate = DateTime.Now;

          
            if (fL_Customers.PartyDate > DateTime.Now)
            {


                //保存到电话营销中
                ObjTelemarketingsBLL.Insert(telemarketing);

                Order ObjOrderBLL = new Order();
                FL_Order ObjOrders = new FL_Order();
                ObjOrders.OrderCoder = DateTime.Now.ToString("yyyyMMdd") + ObjOrderBLL.GetOrderCoder(fL_Customers.PartyDate.Value);  
                ObjOrders.FollowSum = 0;
                ObjOrders.CustomerID = newCustomersId;
                ObjOrders.EmployeeID = empId;
                ObjOrders.CreateDate = DateTime.Now;
                ObjOrders.FlowCount = 0;
                ObjOrderBLL.Insert(ObjOrders);

                //根据返回判断添加的状态
                if (newCustomersId > 0)
                {
                    JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
                    Response.Redirect("~/AdminPanlWorkArea/InviteAdminPanel.aspx");
                }
                else
                {
                    JavaScriptTools.AlertAndClosefancybox("添加失败,请重新尝试", this.Page);

                }
            }
            else
            {
                JavaScriptTools.AlertWindow("婚期不能小于或者是当前时间", this.Page);
            }
        }




    }
}