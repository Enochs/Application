using System;
using System.Linq;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Report;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Invite.Customer
{
    public partial class CustomerCreate : HA.PMS.Pages.SystemPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["IsAdd"] != null)
            {
                JavaScriptTools.AlertWindow("添加成功！", Page);
            }
        }

        /// <summary>
        /// 创建客户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            HA.PMS.BLLAssmblly.Flow.Customers ObjCustomersBLL = new HA.PMS.BLLAssmblly.Flow.Customers();
            HA.PMS.BLLAssmblly.Flow.Invite ObjInviteBLL = new HA.PMS.BLLAssmblly.Flow.Invite();
            HA.PMS.BLLAssmblly.Flow.Telemarketing ObjTelemarketingsBLL = new BLLAssmblly.Flow.Telemarketing();
            int employeeid = User.Identity.Name.ToInt32();

            FL_Customers fL_Customers = ObjCustomersBLL.Where(C => C.BrideCellPhone == txtBrideCellPhone.Text).FirstOrDefault();
            if (fL_Customers != null)
            {
                FL_Invite fL_Invite = ObjInviteBLL.GetByCustomerID(fL_Customers.CustomerID);
                if (fL_Invite != null)
                {
                    if (fL_Invite.EmpLoyeeID == employeeid)
                    {
                        Response.Redirect("/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?OnlyView=1&CustomerID=" + fL_Invite.CustomerID);
                    }
                    else
                    {
                        JavaScriptTools.AlertWindow(string.Format("此客户已经存在，邀约责任人：{0}，所在渠道：{1}", GetEmployeeName(fL_Invite.EmpLoyeeID), fL_Customers.Channel), Page);
                        return;
                    }
                }
                else
                {
                    FL_Invite ObjInvte = new FL_Invite();
                    ObjInvte.CustomerID = fL_Customers.CustomerID;
                    ObjInvte.EmpLoyeeID = employeeid;
                    ObjInvte.CreateEmployee = employeeid;
                    ObjInvte.CreateEmployeeName = GetEmployeeName(employeeid);
                    ObjInvte.CreateDate = DateTime.Now;
                    ObjInvte.LastFollowDate = DateTime.Now;
                    ObjInvte.FllowCount = 0;
                    ObjInviteBLL.Insert(ObjInvte);

                    fL_Customers.State = (int)CustomerStates.DoInvite;
                    ObjCustomersBLL.Update(fL_Customers);

                    Response.Redirect("/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=" + fL_Customers.CustomerID);
                }
            }
            else
            {
                #region 插入新人
                FL_Customers fl_Coustomer = new FL_Customers();
                fl_Coustomer.Groom = txtGroom.Text;
                fl_Coustomer.GroomBirthday = DateTime.Now;
                fl_Coustomer.BrideCellPhone = txtBrideCellPhone.Text;
                fl_Coustomer.GroomCellPhone = txtGroomCellPhone.Text;
                fl_Coustomer.Bride = txtBride.Text;

                fl_Coustomer.BrideWeiBo = txtBrideWeibo.Text;
                fl_Coustomer.BrideBirthday = DateTime.Now;
                fl_Coustomer.Referee = "自己收集";
                fl_Coustomer.GroomCellPhone = txtGroomCellPhone.Text;
                fl_Coustomer.BrideWeiXin = txtBrideWeiXin.Text;
                fl_Coustomer.BrideEmail = txtBrideEmail.Text;
                fl_Coustomer.BrideQQ = txtBrideQQ.Text;
                fl_Coustomer.GroomQQ = txtGroomQQ.Text;

                fl_Coustomer.GroomEmail = txtGroomEmail.Text;
                fl_Coustomer.GroomteWeixin = txtGroomteWeixin.Text;
                fl_Coustomer.GroomWeiBo = txtGroomWeibo.Text;

                fl_Coustomer.Operator = txtOperator.Text;
                fl_Coustomer.OperatorRelationship = string.Empty;
                fl_Coustomer.OperatorPhone = txtOperatorPhone.Text;
                fl_Coustomer.OperatorEmail = txtOperatorEmail.Text;
                fl_Coustomer.OperatorWeiBo = txtOperatorWeibo.Text;
                fl_Coustomer.OperatorWeiXin = txtOperatorWeiXin.Text;
                fl_Coustomer.OperatorQQ = txtOperatorQQ.Text;
                fl_Coustomer.PartyBudget = 0;//婚礼预算
                fl_Coustomer.IsLose = false;//是否流失
                fl_Coustomer.PartyDate = txtPartyDay.Text.ToDateTime(DateTime.Parse("1949-10-1"));

                fl_Coustomer.BrideBirthday = DateTime.Parse("1949-10-1");
                fl_Coustomer.GroomBirthday = DateTime.Parse("1949-10-1");
                fl_Coustomer.ChannelType = 0;
                fl_Coustomer.Channel = "自己收集";
                fl_Coustomer.LikeColor = "";
                fl_Coustomer.ExpectedAtmosphere = "";
                fl_Coustomer.Hobbies = "";
                fl_Coustomer.NoTaboos = "";
                fl_Coustomer.WeddingServices = "";
                fl_Coustomer.ImportantProcess = "";
                fl_Coustomer.Experience = "";
                fl_Coustomer.DesiredAppearance = "";
                fl_Coustomer.Wineshop = ddlHotel.SelectedItem.Text;
                fl_Coustomer.CustomerStatus = CustomerState.GetEnumDescription(CustomerStates.New);
                fl_Coustomer.FormMarriage = "";
                fl_Coustomer.IsDelete = false;
                fl_Coustomer.TimeSpans = ddlTimerSpan.Text;
                fl_Coustomer.Other = txtOther.Text;
                fl_Coustomer.State = (int)CustomerStates.DoInvite;
                fl_Coustomer.Recorder = employeeid;
                fl_Coustomer.RecorderDate = DateTime.Now;

                int CustomerID = ObjCustomersBLL.Insert(fl_Coustomer);

                //把之前添加的客户添加到电话营销表中
                HA.PMS.DataAssmblly.FL_Telemarketing fL_Telemarketing = new FL_Telemarketing();
                fL_Telemarketing.EmployeeID = employeeid;
                fL_Telemarketing.CreateEmpLoyee = employeeid;
                fL_Telemarketing.CustomerID = CustomerID;
                fL_Telemarketing.SortOrder = ObjTelemarketingsBLL.GetMaxSortOrder() + 1;
                fL_Telemarketing.CreateDate = DateTime.Now;

                //保存到电话营销中
                ObjTelemarketingsBLL.Insert(fL_Telemarketing);

 

                HA.PMS.DataAssmblly.FL_Invite ObjInvte = new HA.PMS.DataAssmblly.FL_Invite();
                ObjInvte.CustomerID = fl_Coustomer.CustomerID;
                ObjInvte.EmpLoyeeID = employeeid;
                ObjInvte.CreateEmployee = employeeid;
                ObjInvte.CreateEmployeeName = GetEmployeeName(employeeid);
                ObjInvte.CreateDate = DateTime.Now;
                ObjInvte.LastFollowDate = DateTime.Now;
                ObjInvte.FllowCount = 0;
                ObjInviteBLL.Insert(ObjInvte);


                //创建统计目录
                Report ObjReportBLL = new Report();
                SS_Report ObjReportModel = new SS_Report();
                if (txtPartyDay.Text != string.Empty)
                {
                    ObjReportModel.Partydate = txtPartyDay.Text.ToDateTime();
                }
                ObjReportModel.CreateEmployee = User.Identity.Name.ToInt32();
                ObjReportModel.CustomerID = ObjInvte.CustomerID;
                ObjReportModel.CreateDate = DateTime.Now;
                ObjReportModel.InviteCreateDate = DateTime.Now;
                ObjReportModel.State = (int)CustomerStates.DoInvite;
                ObjReportModel.InviteEmployee = User.Identity.Name.ToInt32();
                ObjReportBLL.Insert(ObjReportModel);
                Response.Redirect("/AdminPanlWorkArea/Invite/Customer/InviteCommunicationContent.aspx?CustomerID=" + CustomerID);
                #endregion
            }
        }
    }
}