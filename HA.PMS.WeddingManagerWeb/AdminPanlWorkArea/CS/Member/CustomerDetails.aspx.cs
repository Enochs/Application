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
using HA.PMS.BLLAssmblly.Report;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.CS.Member
{
    public partial class CustomerDetails : SystemPage
    {
        /// <summary>
        /// 订单
        /// </summary>
        Order objOrderBLL = new Order();

        /// <summary>
        /// 渠道
        /// </summary>
        SaleSources objSaleSourcesBLL = new SaleSources();

        /// <summary>
        /// 客户
        /// </summary>
        Customers objCustomersBLL = new Customers();

        /// <summary>
        /// 回访
        /// </summary>
        CustomerReturnVisit objCustomerReturnVisitBLL = new CustomerReturnVisit();

        /// <summary>
        /// 取件
        /// </summary>
        TakeDisk objTakeDiskBLL = new TakeDisk();

        /// <summary>
        /// 满意度
        /// </summary>
        DegreeOfSatisfaction objDegreeOfSatisfactionBLL = new DegreeOfSatisfaction();

        /// <summary>
        /// 投诉
        /// </summary>
        Complain objComplainBLL = new Complain();


        BLLAssmblly.CS.Member ObjMemberBLL = new BLLAssmblly.CS.Member();

        /// <summary>
        /// 统计
        /// </summary>
        Report ObjReportBLL = new Report();

        /// <summary>
        /// 报价单
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        /// <summary>
        /// 员工
        /// </summary>
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
                SS_Report Report = ObjReportBLL.GetByCustomerID(CustomerID);

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
                        ltlPlanner.Text = GetQuotedEmployee(CustomerID);
                        ltlOrderEmployee.Text = GetEmployeeName(singerOrder.EmployeeID);
                        ltlOrderCount.Text = singerOrder.FlowCount + string.Empty;
                    }

                    if (Report != null)
                    {
                        ltlFirstCome.Text = ShowShortDate(Report.OrderCreateDate);              //到店时间
                        ltlOrderDate.Text = ShowShortDate(Report.OrderSucessDate);              //订单时间(成功预定)
                        ltlQuotedSuccessDate.Text = ShowShortDate(Report.WorkCreateDate);       //签约时间
                    }


                    //新人回访（没有总的一个回访状态）


                    //int isHaveComplain = objComplainBLL.Getbyc().Where(C => C.CustomerID == CustomerID).Count();
                    //ltkComplain.Text = isHaveComplain == 0 ? "无" : "有";
                    ltlEmployee.Text = GetInviteName(CustomerID);           //邀约人
                    ltlMoney.Text = GetAggregateAmount(CustomerID);
                    ltllv.Text = GetProfitMarginByCustomerID(CustomerID);
                    ltlDegree.Text = GetSacNameByCustomernId(CustomerID);
                    ltlChannel.Text = member.Channel;
                    ltlChannelType.Text = GetChannelTypeName(member.ChannelType);
                    ltlReferee.Text = member.Referee;
                    ltlPartDate.Text = ShowShortDate(member.PartyDate);
                    ltlTimeSpan.Text = member.TimeSpans;
                    ltlWineShop.Text = member.Wineshop;
                    ltlTableNumber.Text = member.TableNumber.ToString();
                    ltlPartyBudget.Text = member.PartyBudget.ToString();
                    ltlIsVip.Text = member.IsVip == true ? "是" : "否";
                    ltlRecoder.Text = GetEmployeeName(member.Recorder);
                    ltlRecoderDate.Text = ShowShortDate(member.RecorderDate);
                    lblOther.Text = member.Other;
                    lblRebates.Text = member.Rebates;


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