using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.StoreSales
{
    public partial class CreateCustomer : SystemPage
    {
        Customers ObjCustomersBLL = new Customers();

        Order ObjOrderBLL = new Order();

        Employee ObjEmployeeBLL = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["IsAdd"] != null)
            {
                JavaScriptTools.AlertWindow("添加成功！", Page);
            }
        }


        /// <summary>
        /// 保存到店新人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (txtBride.Text != string.Empty && txtBrideCellPhone.Text != string.Empty)
            {
                FL_Customers ObjCustomersModel = new FL_Customers();
                List<ObjectParameter> ObjParList = new List<ObjectParameter>();

                //ObjParList.Add(new ObjectParameter("Bride", txtBride.Text));
                ObjParList.Add(new ObjectParameter("BrideCellPhone", txtBrideCellPhone.Text));
                var SerchList = ObjCustomersBLL.GetOnlybyParameter(ObjParList.ToArray());
                if (SerchList != null)
                {
                    SerchList.State = (int)CustomerStates.BeginFollowOrder;
                    ObjCustomersBLL.Update(SerchList);
                    var ObjOrderModel = ObjOrderBLL.GetbyCustomerID(SerchList.CustomerID);
                    if (ObjOrderModel != null)
                    {
                        if (ObjOrderModel.EmployeeID == User.Identity.Name.ToInt32())
                        {
                            Response.Redirect("FollowOrderDetails.aspx?CustomerID=" + ObjOrderModel.CustomerID + "&OrderID=" + ObjOrderModel.OrderID);
                        }
                        else
                        {
                            JavaScriptTools.AlertWindow("此客户资料已经存在！跟单责任人为" + ObjEmployeeBLL.GetByID(ObjOrderModel.EmployeeID).EmployeeName + ",请及时联系！", Page);
                        }
                    }
                    else
                    {
                        FL_Order ObjOrders = new FL_Order();
                        ObjOrders.CustomerID = SerchList.CustomerID;
                        ObjOrders.OrderCoder = new Order().ComputeOrderCoder(SerchList.CustomerID);
                        ObjOrders.FollowSum = 0;
                        ObjOrders.CreateDate = DateTime.Now;
                        ObjOrders.FlowCount = 0;
                        ObjOrders.EmployeeID = User.Identity.Name.ToInt32();
                        ObjOrders.EarnestFinish = false;
                        ObjOrders.EarnestMoney = 0;
                        ObjOrderBLL.Insert(ObjOrders);

                        //创建统计目录
                        Report ObjReportBLL = new Report();
                        SS_Report ObjReportModel = new SS_Report();

                        ObjReportModel.CreateEmployee = User.Identity.Name.ToInt32();
                        ObjReportModel.CustomerID = ObjOrders.CustomerID.Value;
                        ObjReportModel.CreateDate = DateTime.Now;
                        ObjReportModel.OrderCreateDate = DateTime.Now;
                        ObjReportModel.State=(int)CustomerStates.BeginFollowOrder;
                        ObjReportModel.OrderEmployee = User.Identity.Name.ToInt32();
                        if (txtPartyDay.Text != string.Empty)
                        {
                            ObjReportModel.Partydate = txtPartyDay.Text.ToDateTime();
                        }
                        ObjReportBLL.Insert(ObjReportModel);
                        Response.Redirect("FollowOrderDetails.aspx?FlowOrder=1&CustomerID=" + ObjOrders.CustomerID + "&OrderID=" + ObjOrders.OrderID);
                    }
                    //?CustomerID=0&OrderID=
                }
                else
                {

                    FL_Customers fl_Coustomers = new FL_Customers();
                    fl_Coustomers.Groom = txtGroom.Text;
             
                    fl_Coustomers.BrideCellPhone = txtBrideCellPhone.Text;
                    fl_Coustomers.GroomCellPhone = txtGroomCellPhone.Text;
                    fl_Coustomers.Bride = txtBride.Text;
   
                    fl_Coustomers.Referee = "直接到店";
                    fl_Coustomers.GroomCellPhone = txtGroomCellPhone.Text;
                    fl_Coustomers.BrideWeiXin = txtBrideWeiXin.Text;
                    fl_Coustomers.BrideEmail = txtBrideEmail.Text;
                    fl_Coustomers.BrideQQ = txtBrideQQ.Text;
                    fl_Coustomers.BrideWeiBo = txtBrideWeibo.Text;

                    fl_Coustomers.GroomQQ = txtGroomQQ.Text;
                    fl_Coustomers.GroomEmail = txtGroomEmail.Text;

                    fl_Coustomers.GroomteWeixin = txtGroomteWeixin.Text;
                    fl_Coustomers.GroomWeiBo = txtGroomWeibo.Text;

                    fl_Coustomers.BrideBirthday = MinDateTime;
                    fl_Coustomers.GroomBirthday = MinDateTime;

                    fl_Coustomers.Operator = txtOperator.Text;
                    fl_Coustomers.OperatorRelationship = "";
                    fl_Coustomers.OperatorPhone = txtOperatorPhone.Text;
                    fl_Coustomers.OperatorEmail = txtOperatorEmail.Text;

                    fl_Coustomers.OperatorQQ = txtOperatorQQ.Text;
                    fl_Coustomers.OperatorWeiBo = txtOperatorWeibo.Text;
                    fl_Coustomers.OperatorWeiXin = txtOperatorWeiXin.Text;

                    fl_Coustomers.PartyBudget = 0;//婚礼预算
                    fl_Coustomers.IsLose = false;//是否流失
                    fl_Coustomers.PartyDate = txtPartyDay.Text.ToDateTime(DateTime.Parse("1949-10-1"));
                    fl_Coustomers.ChannelType = 0;
                    fl_Coustomers.Channel = "自己到店";
                    fl_Coustomers.LikeColor = "";
                    fl_Coustomers.ExpectedAtmosphere = "";
                    fl_Coustomers.Hobbies = "";
                    fl_Coustomers.NoTaboos = "";
                    fl_Coustomers.WeddingServices = "";
                    fl_Coustomers.ImportantProcess = "";
                    fl_Coustomers.Experience = "";
                    fl_Coustomers.DesiredAppearance = "";
                    fl_Coustomers.Wineshop = ddlHotel.SelectedItem.Text;
                    fl_Coustomers.State = (int)CustomerStates.DoInvite;
                    fl_Coustomers.CustomerStatus = CustomerState.GetEnumDescription(CustomerStates.New);
                    fl_Coustomers.FormMarriage = "";
                    fl_Coustomers.IsDelete = false;
                    fl_Coustomers.TimeSpans = ddlTimerSpan.Text;
                    fl_Coustomers.Other = txtOther.Text;
                    fl_Coustomers.State = (int)CustomerStates.BeginFollowOrder;
                    //记录人
                    fl_Coustomers.Recorder = User.Identity.Name.ToInt32();
                    //记录时间
                    fl_Coustomers.RecorderDate = DateTime.Now;


                    var CustomerID = ObjCustomersBLL.Insert(fl_Coustomers);
                    //此时为直接到店

                    int employeeid = User.Identity.Name.ToInt32();
                    FL_Invite ObjInvte = new FL_Invite();
                    ObjInvte.CustomerID = fl_Coustomers.CustomerID;
                    ObjInvte.AgainDate = DateTime.Now;
                    ObjInvte.ComeDate = DateTime.Now;
                    ObjInvte.EmpLoyeeID = employeeid;
                    ObjInvte.CreateEmployee = employeeid;
                    ObjInvte.CreateEmployeeName = GetEmployeeName(employeeid);
                    ObjInvte.CreateDate = DateTime.Now;
                    ObjInvte.LastFollowDate = DateTime.Now;
                    ObjInvte.ToEmpLoyee = User.Identity.Name.ToInt32();
                    ObjInvte.ToDepartMent = new Employee().GetDepartmentID(employeeid);
                    ObjInvte.FllowCount = 0;
                    ObjInvte.ContentID = 0;
                    ObjInvte.OrderEmpLoyeeID = employeeid;
                    ObjInvte.UpdateDate = DateTime.Now;
                    new BLLAssmblly.Flow.Invite().Insert(ObjInvte);

                    FL_Order ObjOrders = new FL_Order();
                    ObjOrders.CustomerID = fl_Coustomers.CustomerID;
                    ObjOrders.OrderCoder = ObjOrderBLL.ComputeOrderCoder(fl_Coustomers.CustomerID);
                    ObjOrders.FollowSum = 0;
                    ObjOrders.FlowCount = 0;
                    ObjOrders.EmployeeID = User.Identity.Name.ToInt32();
                    ObjOrders.CreateDate = DateTime.Now;
                    ObjOrders.EarnestFinish = false;
                    ObjOrders.EarnestMoney = 0;
                    ObjOrderBLL.Insert(ObjOrders);
                    MissionManager ObjMissManagerBLL = new MissionManager();
                    ObjMissManagerBLL.WeddingMissionCreate(ObjOrders.CustomerID.Value, 1, (int)MissionTypes.Order, DateTime.Now, ObjOrders.EmployeeID.Value, "?CustomerID=" + ObjOrders.CustomerID.ToString() + "&OrderID=" + ObjOrders.OrderID + "&FlowOrder=1", MissionChannel.FL_TelemarketingManager, User.Identity.Name.ToInt32(), ObjOrders.OrderID);

            

                    Report ObjReportBLL = new Report();
                    SS_Report ObjReportModel = new SS_Report();

                    ObjReportModel.CreateEmployee = User.Identity.Name.ToInt32();
                    ObjReportModel.CustomerID = ObjOrders.CustomerID.Value;
                    ObjReportModel.CreateDate = DateTime.Now;
                    ObjReportModel.OrderCreateDate = DateTime.Now;
                    ObjReportModel.State = (int)CustomerStates.BeginFollowOrder;
                    ObjReportModel.OrderEmployee = User.Identity.Name.ToInt32();
                    if (txtPartyDay.Text != string.Empty)
                    {
                        ObjReportModel.Partydate = txtPartyDay.Text.ToDateTime();
                    }
                    ObjReportBLL.Insert(ObjReportModel);
                    Response.Redirect("FollowOrderDetails.aspx?FlowOrder=1&CustomerID=" + ObjOrders.CustomerID + "&OrderID=" + ObjOrders.OrderID);
                }
            }
            else
            {
                JavaScriptTools.AlertWindow("新娘姓名，电话不能为空！",Page);
            }
           
        }

        protected void txtPartyDay_TextChanged(object sender, EventArgs e)
        {

        }
    }
}