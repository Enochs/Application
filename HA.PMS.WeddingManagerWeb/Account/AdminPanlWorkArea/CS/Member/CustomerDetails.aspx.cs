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
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Control;
using System.Data.Objects;
using HA.PMS.BLLAssmblly.FD;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member
{
    public partial class CustomerDetails : SystemPage
    {
        Order objOrderBLL = new Order();
        SaleSources objSaleSourcesBLL = new SaleSources();
        Customers objCustomersBLL = new Customers();
        CustomerReturnVisit objCustomerReturnVisitBLL = new CustomerReturnVisit();
        TakeDisk objTakeDiskBLL = new TakeDisk();
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();
        Complain objComplainBLL = new Complain();
        BLLAssmblly.CS.Member ObjMemberBLL = new BLLAssmblly.CS.Member();

        Employee ObjEmployeeBLL = new Employee();

        // MemberService objMemberServiceBLL = new MemberService();
        protected void DataBinder(int CustomerID)
        {

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int CustomerID = Request.QueryString["CustomerID"].ToInt32();
                ViewState["CustomerID"] = CustomerID;
                ObjectParameter pars = new ObjectParameter("CustomerID", CustomerID);
                FL_Customers member = objCustomersBLL.GetByID(CustomerID);

                if (User.Identity.Name.ToInt32() == member.Recorder || ObjEmployeeBLL.IsManagers(User.Identity.Name.ToInt32()))
                {
                    td_source.Visible = true;
                    td_employee.Visible = true;
                    td_channel.Visible = true;
                    td_Rebates.Visible = true;
                }
                else
                {
                    td_source.Visible = false;
                    td_employee.Visible = false;
                    td_channel.Visible = false;
                    td_Rebates.Visible = false;
                }

                if (member != null)
                {

                    DataBinder(CustomerID);
                    ltlGroom.Text = member.Groom;
                    ltlBride.Text = member.Bride;
                    ltlOperator.Text = member.Operator;
                    ltlGroomCellPhone.Text = member.GroomCellPhone;
                    ltlBrideCellPhone.Text = member.BrideCellPhone;
                    ltlOperatorPhone.Text = member.OperatorPhone;
                    ltlGroomEmail.Text = member.GroomEmail;
                    ltlBrideEmail.Text = member.BrideEmail;
                    ltlGroomIDCard.Text = member.GroomIdCard;
                    ltlBrideIDCard.Text = member.BrideIdCard;

                    ltlGroomteWeixin.Text = member.GroomteWeixin;
                    ltlBrideWeiXin.Text = member.BrideWeiXin;
                    ltlGroomWeiBo.Text = member.GroomWeiBo;
                    ltlBrideWeiBo.Text = member.BrideWeiBo;

                    ltlGroomQQ.Text = (member.GroomQQ + string.Empty).TrimStart('0');
                    ltlBrideQQ.Text = (member.BrideQQ + string.Empty).TrimStart('0');
                    ltlGroomBirthday.Text = ShowShortDate(member.GroomBirthday);
                    ltlBrideBirthday.Text = ShowShortDate(member.BrideBirthday);
                    ltlGroomJob.Text = member.GroomJob;
                    ltlBrideJob.Text = member.BrideJob;
                    ltlBrideJobCompany.Text = member.BrideJobCompany;
                    ltlGroomJobCompany.Text = member.GroomJob;
                    ltlCustomersType.Text = member.CustomersType;
                    try
                    {
                        ltlLoseMessage.Text = GetLoseContentDetails(objOrderBLL.GetbyCustomerID(CustomerID).OrderID);
                    }
                    catch
                    {

                    }

                    var ValueList = Enum.GetValues(typeof(CustomerStates));
                    string state = Enum.Parse(typeof(CustomerStates), member.State.ToString()).ToString();

                    foreach (var ObjItem in ValueList)
                    {

                        if (ObjItem.ToString() == state)
                        {
                            ltlState.Text = CustomerState.GetEnumDescription(ObjItem);
                        }
                    }

                    FL_Order singerOrder = objOrderBLL.GetbyCustomerID(CustomerID);
                    if (singerOrder != null)
                    {
                        ltlPlanner.Text = GetPlannerName(singerOrder.Planner);
                        ltlEmployee.Text = GetInviteName(singerOrder.EmployeeID);
                        ltlOrderEmployee.Text = GetEmployeeName(singerOrder.EmployeeID);
                        ltlOrderCount.Text = singerOrder.FlowCount + string.Empty;
                        ltlOrderDate.Text = ShowShortDate(singerOrder.PlanComeDate);
                        ltlSureDate.Text = GetDateStr(singerOrder.ComeDate);
                    }

                    //int isHaveComplain = objComplainBLL.Getbyc().Where(C => C.CustomerID == CustomerID).Count();
                    //ltkComplain.Text = isHaveComplain == 0 ? "无" : "有";
                    ltlMoney.Text = GetAggregateAmount(CustomerID);
                    ltllv.Text = GetProfitMarginByCustomerID(CustomerID);
                    ltlDegree.Text = GetReturnResultByCustomerId(CustomerID);
                    ltlChannel.Text = member.Channel;
                    ltlChannelType.Text = GetChannelTypeName(member.ChannelType);
                    ltlReferee.Text = member.Referee;
                    ltlPartDate.Text = ShowShortDate(member.PartyDate);
                    ltlTimeSpan.Text = member.TimeSpans;
                    ltlWineShop.Text = member.Wineshop;
                    ltlTableNumber.Text = member.TableNumber.ToString();
                    ltlPartyBudget.Text = member.PartyBudget.ToString();
                    ltlRecoder.Text = GetEmployeeName(member.Recorder);
                    ltlRecoderDate.Text = ShowShortDate(member.RecorderDate);
                    lblOther.Text = member.Other;
                    lblRebates.Text = member.Rebates;
                    ltlFirstCome.Text = ShowShortDate(member.Todate);


                    CS_DegreeOfSatisfaction faction = objDegreeOfSatisfactionBLL.GetByCustomerID(CustomerID);
                    if (faction != null)
                    {
                        ltlDofDate.Text = ShowShortDate(faction.DofDate);
                        ltlSumDof.Text = faction.SumDof + string.Empty;

                    }

                    CS_TakeDisk take = objTakeDiskBLL.GetByCustomerID(CustomerID);

                    if (take != null)
                    {
                        ltlTakeDate.Text = ShowShortDate(take.realityTime);
                    }

                }
            }
        }

        /// <summary>
        /// 获取服务方式
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetMenber(object CustomerID)
        {
            return ObjMemberBLL.GetMemberTypeByCustomerID(CustomerID.ToString().ToInt32());

        }

        public string GetMemberServiceType(object CustomerID)
        {
            var ObjMemberModel = ObjMemberBLL.GetByCustomerID((CustomerID + string.Empty).ToInt32());
            if (ObjMemberModel != null)
            {
                if (ObjMemberModel.Type == 1)
                {
                    return "生日";
                }
                else if (ObjMemberModel.Type == 2)
                {
                    return "周年庆";
                }
                else if (ObjMemberModel.Type == 3)
                {
                    return "其他";
                }
            }
            return string.Empty;
        }


        /// <summary>
        /// 获取服务完成时间
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetFiniShDate(object CustomerID)
        {
            return ObjMemberBLL.GetByCustomerID(CustomerID.ToString().ToInt32()).CreateDate.ToShortDateString();

        }

        /// <summary>
        /// 记录创建人
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetCreateEmployee(object CustomerID)
        {
            Employee ObjEmployeeBLL = new Employee();
            return ObjEmployeeBLL.GetByID(ObjMemberBLL.GetByCustomerID(CustomerID.ToString().ToInt32()).CreateEmployee).EmployeeName;
        }

        /// <summary>
        /// 补充记录
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public string GetContent(object CustomerID)
        {
            return ObjMemberBLL.GetByCustomerID(CustomerID.ToString().ToInt32()).ServiceContent;

        }
    }
}