using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member
{
    public partial class CustomerCreate : SystemPage
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
        protected void btnCreate_Click(object sender, EventArgs e)
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
                        ObjCustomersModel = ObjCustomersBLL.GetByID(SerchList.CustomerID);
                        FL_Order ObjOrders = new FL_Order();
                        ObjOrders.CustomerID = SerchList.CustomerID;
                        ObjOrders.OrderCoder = DateTime.Now.ToString("yyyyMMdd") + ObjOrderBLL.GetOrderCoder(ObjCustomersModel.PartyDate.Value);
                        ObjOrders.FollowSum = 0;
                        ObjOrders.CreateDate = DateTime.Now;
                        ObjOrders.FlowCount = 0;
                        ObjOrders.EmployeeID = User.Identity.Name.ToInt32();
                        ObjOrders.EarnestFinish = false;
                        ObjOrders.EarnestMoney = 0;
                        ObjOrderBLL.Insert(ObjOrders);
                       
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
                    fl_Coustomers.BrideWeiBo = txtBrideweibo.Text;

                    fl_Coustomers.GroomQQ = txtGroomQQ.Text;
                    fl_Coustomers.GroomEmail = txtGroomEmail.Text;

                    fl_Coustomers.GroomteWeixin = txtGroomteWeixin.Text;
                    fl_Coustomers.GroomWeiBo = txtFroomWeibo.Text;

                    fl_Coustomers.BrideBirthday = "1949-10-1".ToDateTime();
                    fl_Coustomers.GroomBirthday = "1949-10-1".ToDateTime();

                    fl_Coustomers.Operator = txtOperator.Text;
                    fl_Coustomers.OperatorRelationship = "";
                    fl_Coustomers.OperatorPhone = txtOperatorPhone.Text;
                    fl_Coustomers.OperatorEmail = txtOperatorEmail.Text;

                    fl_Coustomers.OperatorQQ = txtOperatorqq.Text;
                    fl_Coustomers.OperatorWeiBo = txtOperatorweibo.Text;
                    fl_Coustomers.OperatorWeiXin = txtOperatorWeiXin.Text;

                    fl_Coustomers.PartyBudget = 0;//婚礼预算
                    fl_Coustomers.IsLose = false;//是否流失
                    fl_Coustomers.PartyDate = txtPartyDay.Text.ToDateTime();
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
                    fl_Coustomers.State = 23;
                    //记录人
                    fl_Coustomers.Recorder = User.Identity.Name.ToInt32();
                    //记录时间
                    fl_Coustomers.RecorderDate = DateTime.Now;

                    fl_Coustomers.FinishOver = true;
                    var CustomerID = ObjCustomersBLL.Insert(fl_Coustomers);
                    //此时为直接到店


                    FL_Order ObjOrders = new FL_Order();
                    ObjOrders.CustomerID = fl_Coustomers.CustomerID;
                    ObjOrders.OrderCoder = DateTime.Now.ToString("yyyyMMdd") + ObjOrderBLL.GetOrderCoder(fl_Coustomers.PartyDate.Value);
                    ObjOrders.FollowSum = 0;
                    ObjOrders.FlowCount = 0;
                    ObjOrders.EarnestFinish = true;
                    ObjOrders.EarnestMoney = txtEMoney.Text.ToDecimal();
                    ObjOrders.LastFollowDate = DateTime.Now;
                    ObjOrders.EmployeeID = User.Identity.Name.ToInt32();
                    ObjOrders.CreateDate = DateTime.Now;
                    ObjOrders.EarnestFinish = false;
                    ObjOrders.EarnestMoney = 0;
                    ObjOrderBLL.Insert(ObjOrders);
                   
                    //进入新人回访
                 

                    //进入新人报价单

                    HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();
                    ///录入报价单基础
                    FL_QuotedPrice ObjQuotedPriceModel = new FL_QuotedPrice();
                    if (ObjQuotedPriceBLL.GetByCustomerID(Request["CustomerID"].ToInt32()) == null)
                    {
                        ObjQuotedPriceModel.CustomerID = Request["CustomerID"].ToInt32();
                        ObjQuotedPriceModel.IsDelete = false;
                        ObjQuotedPriceModel.OrderID = ObjOrders.OrderID;// Request["OrderID"].ToInt32();
                        ObjQuotedPriceModel.CategoryName = "开始制作报价单";
                        ObjQuotedPriceModel.EmpLoyeeID = User.Identity.Name.ToInt32();//ObjEmpLoyeeHide.Value.ToInt32();
                        ObjQuotedPriceModel.IsChecks = false;
                        ObjQuotedPriceModel.CreateDate = DateTime.Now;
                        ObjQuotedPriceModel.IsDispatching = 0;
                        ObjQuotedPriceModel.EarnestMoney = 0;
                        ObjQuotedPriceModel.FinishAmount = 0;
                        ObjQuotedPriceModel.IsFirstCreate = false;
                        ObjQuotedPriceModel.OrderCoder = ObjOrders.OrderCoder;
                        ObjQuotedPriceModel.ParentQuotedID = 0;
                        ObjQuotedPriceModel.NextFlowDate = ObjOrders.LastFollowDate;
                        ObjQuotedPriceModel.StarDispatching = true;
                        ObjQuotedPriceModel.EarnestMoney = txtdingjin.Text.ToDecimal();
                        ObjQuotedPriceModel.FinishAmount = txtFinishAmount.Text.ToDecimal();
                        ObjQuotedPriceBLL.Insert(ObjQuotedPriceModel);


                    }
                    JavaScriptTools.AlertWindowAndLocation("保存成功！", Request.Url.ToString(), Page);
                    //Response.Redirect("FollowOrderDetails.aspx?FlowOrder=1&CustomerID=" + ObjOrders.CustomerID + "&OrderID=" + ObjOrders.OrderID);
                }
            }
            else
            {
                JavaScriptTools.AlertWindow("新娘姓名，电话不能为空！", Page);
            }

        }

        protected void txtPartyDay_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
