using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.SysTarget;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.CS;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Sys;

using System.Data.Objects;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch
{
    public partial class CanpanyReport : SystemPage
    {
        Target ObjTargetBLL = new Target();

        /// <summary>
        /// 订单
        /// </summary>
        Order ObjOrderBLL = new Order();

        /// <summary>
        /// 渠道返利
        /// </summary>
        PayNeedRabate ObjPayNeedRabateBLL = new PayNeedRabate();

        /// <summary>
        /// 报价单
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotedPriceBLL = new BLLAssmblly.Flow.QuotedPrice();


        /// <summary>
        /// 入客
        /// </summary>
        Telemarketing ObjTelemarketingBLL = new Telemarketing();
        /// <summary>
        /// 收款计划
        /// </summary>
        QuotedCollectionsPlan ObjQuotedCollectionsPlanBLL = new QuotedCollectionsPlan();

        /// <summary>
        /// 投诉
        /// </summary>
        Complain ObjComplainBLL = new Complain();


        /// <summary>
        /// 客户
        /// </summary>
        Customers ObjCustomersBLL = new Customers();



        int DepartmentKey = 0;
        int EmployeeKey = 0;
        Department ObjDepartmentBLL = new Department();
        /// <summary>
        /// 输出成本明细
        /// </summary>
        Cost ObjCostBLL = new Cost();
        int year = DateTime.Now.Year;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DdlRangeYear1.SelectedItem != null)
            {
                year = DdlRangeYear1.SelectedItem.Text.ToInt32();
                if (year == 0)
                {
                    year = DateTime.Now.Year;
                }
            }
            else
            {
                year = DateTime.Now.Year;
            }
            if (!IsPostBack)
            {

                ///绑定部门
                var ObjDepartMentList = ObjDepartmentBLL.GetMyManagerDepartment(User.Identity.Name.ToInt32());
                this.ddlDepartment.Items.Clear();
                this.ddlDepartment.DataSource = ObjDepartMentList;
                this.ddlDepartment.DataTextField = "DepartmentName";
                this.ddlDepartment.DataValueField = "DepartmentID";
                this.ddlDepartment.DataBind();
                BinderPage(0);
            }

        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderPage(int DepartmentKey)
        {

            var ObjDepartMentList = ObjDepartmentBLL.GetMyManagerDepartment(User.Identity.Name.ToInt32());
            List<FL_Target> ObjList = new List<FL_Target>();
            ObjList.Add(ObjTargetBLL.GetByID(7));
            FinishTargetSum ObjFinishTargetSumBLL = new FinishTargetSum();
            List<int> EmployeeKey = new List<int>();

            string KeyList = string.Empty;

            foreach (var Objdepartment in ObjDepartMentList)
            {
                KeyList += Objdepartment.DepartmentID + ",";

            }
            KeyList = KeyList.Trim(',');

            EmployeeKey.Add(User.Identity.Name.ToInt32());

            EmployeeKey.Add(User.Identity.Name.ToInt32());
            var ObjNeed = ObjFinishTargetSumBLL.GetinEmployeeKeyListNeedCreate(EmployeeKey, 1);


            if (DepartmentKey > 0)
            {
                KeyList = this.ddlDepartment.SelectedValue;
            }


            //查询人员目标
            if (ddlEmployee.SelectedValue.ToInt32() > 0)
            {
                if (ObjNeed.Count > 0)
                {
                    this.repReport.DataSource = ObjFinishTargetSumBLL.GetDepartmentTarget(KeyList, " and Year=" + year + " and IsActive=1 and TargetTitle='" + ObjNeed[0].TargetTitle + "'");
                    this.repReport.DataBind();
                }
            }
            else
            {
                if (ObjNeed.Count > 0)
                {
                    this.repReport.DataSource = ObjFinishTargetSumBLL.GetDepartmentTarget(KeyList, " and Year=" + year + " and IsActive=1 and TargetTitle='" + ObjNeed[0].TargetTitle + "'");
                    this.repReport.DataBind();
                }
            }


        }


        /// <summary>
        /// 获取管理ID
        /// </summary>
        /// <param name="ObjParList"></param>
        /// <returns></returns>
        public List<ObjectParameter> GetManagerEmployeeID(List<ObjectParameter> ObjParList)
        {

            if (ddlEmployee.SelectedValue.ToInt32() > 0)
            {
                ObjParList.Add(new ObjectParameter("EmpLoyeeID", ddlEmployee.SelectedValue.ToInt32()));
                return ObjParList;
            }

            if (ddlDepartment.SelectedValue.ToInt32() > 0)
            {
                ObjParList.Add(new ObjectParameter("EmpLoyeeID", ObjDepartmentBLL.GetDepartmentManager(ddlDepartment.SelectedValue.ToInt32())));
                return ObjParList;
            }
            return ObjParList;
        }

        /// <summary>
        /// 获取订单定金详情
        /// </summary>
        /// <returns></returns>
        public string GetEarnestMoneyByOrderYearMonth(object Month)
        {


            List<ObjectParameter> ObjParList = new List<ObjectParameter>();



            switch (Month.ToString().ToInt32())
            {
                case 13:
                    ObjParList.Add(new ObjectParameter("LastFollowDate_between", year + "-1-01," + year + "-12-" + DateTime.DaysInMonth(year, 12)));
                    break;

                case 14:
                    ObjParList.Add(new ObjectParameter("LastFollowDate_between", (year - 1) + "-1-01," + (year - 1) + "-12-" + DateTime.DaysInMonth((year - 1), 12)));
                    break;

                case 15:
                    ObjParList.Add(new ObjectParameter("LastFollowDate_between", (year - 50) + "-1-01," + year + "-12-" + DateTime.DaysInMonth((year + 50), 12)));
                    break;
                default:
                    ObjParList.Add(new ObjectParameter("LastFollowDate_between", year + "-" + Month.ToString() + "-01," + year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())));
                    break;
            }
            ObjParList.Add(new ObjectParameter("EarnestFinish", true));

            ObjParList = GetManagerEmployeeID(ObjParList);


            return ObjOrderBLL.GetOrderSumByWhere(ObjParList, 4);
        }


        /// <summary>
        /// 获取订单服务对数
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetOrderCountByYearmonth(object Month)
        {

            List<ObjectParameter> ObjParList = new List<ObjectParameter>();



            switch (Month.ToString().ToInt32())
            {
                case 13:
                    ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-1-01," + year + "-12-" + DateTime.DaysInMonth(year, 12)));
                    break;

                case 14:
                    ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 1) + "-1-01," + (year - 1) + "-12-" + DateTime.DaysInMonth((year - 1), 12)));
                    break;

                case 15:
                    ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 50) + "-1-01," + year + "-12-" + DateTime.DaysInMonth((year + 50), 12)));
                    break;
                default:
                    ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-" + Month.ToString() + "-01," + year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())));
                    break;

            }
 
            ObjParList = GetManagerEmployeeID(ObjParList);

            return ObjOrderBLL.GetOrderSumByWhere(ObjParList, 7);
        }

        /// <summary>
        /// 获取订单服务对数
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetQuotedCountByYearmonth(object Month)
        {

            List<ObjectParameter> ObjParList = new List<ObjectParameter>();



            switch (Month.ToString().ToInt32())
            {
                case 13:
                    ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-1-01," + year + "-12-" + DateTime.DaysInMonth(year, 12)));
                    break;

                case 14:
                    ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 1) + "-1-01," + (year - 1) + "-12-" + DateTime.DaysInMonth((year - 1), 12)));
                    break;

                case 15:
                    ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 50) + "-1-01," + year + "-12-" + DateTime.DaysInMonth((year + 50), 12)));
                    break;
                default:
                    ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-" + Month.ToString() + "-01," + year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())));
                    break;

            }

            ObjParList = GetManagerEmployeeID(ObjParList);
            return ObjOrderBLL.GetOrderSumByWhere(ObjParList, 8);
        }


        /// <summary>
        /// 获取完工额
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetOrderSumByDateTime(object Month)
        {

            List<ObjectParameter> ObjParList = new List<ObjectParameter>();
            switch (Month.ToString().ToInt32())
            {
                case 13:
                    ObjParList.Add(new ObjectParameter("PartyDate_between", year + "-1-01," + year + "-12-" + DateTime.DaysInMonth(year, 12)));
                    break;

                case 14:
                    ObjParList.Add(new ObjectParameter("PartyDate_between", (year - 1) + "-1-01," + (year - 1) + "-12-" + DateTime.DaysInMonth((year - 1), 12)));
                    break;

                case 15:
                    ObjParList.Add(new ObjectParameter("PartyDate_between", (year - 50) + "-1-01," + year + "-12-" + DateTime.DaysInMonth((year + 50), 12)));
                    break;
                default:
                    ObjParList.Add(new ObjectParameter("PartyDate_between", year + "-" + Month.ToString() + "-01," + year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())));
                    break;

            }

            ObjParList = GetManagerEmployeeID(ObjParList);
            return ObjOrderBLL.GetOrderSumByWhere(ObjParList, 9);
        }



        /// <summary>
        /// 获取完工服务对数
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetOrderFinishCountByDateTime(object Month)
        {

            List<ObjectParameter> ObjParList = new List<ObjectParameter>();
            switch (Month.ToString().ToInt32())
            {
                case 13:
                    ObjParList.Add(new ObjectParameter("PartyDate_between", year + "-1-01," + year + "-12-" + DateTime.DaysInMonth(year, 12)));
                    break;

                case 14:
                    ObjParList.Add(new ObjectParameter("PartyDate_between", (year - 1) + "-1-01," + (year - 1) + "-12-" + DateTime.DaysInMonth((year - 1), 12)));
                    break;

                case 15:
                    ObjParList.Add(new ObjectParameter("PartyDate_between", (year - 50) + "-1-01," + year + "-12-" + DateTime.DaysInMonth((year + 50), 12)));
                    break;
                default:
                    ObjParList.Add(new ObjectParameter("PartyDate_between", year + "-" + Month.ToString() + "-01," + year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())));
                    break;

            }

            ObjParList = GetManagerEmployeeID(ObjParList);
            return ObjOrderBLL.GetOrderSumByWhere(ObjParList, 8);
        }


        //获取完工平均消费金额
        public string GetOrderFinishAvgMoneyByDateTime(object Month)
        {

            List<ObjectParameter> ObjParList = new List<ObjectParameter>();
 
            decimal FiniMoney = ObjOrderBLL.GetOrderSumByDateTime(ObjParList, year, Month.ToString().ToInt32(), 9).ToDecimal();
            decimal FinishCount = ObjOrderBLL.GetOrderSumByDateTime(ObjParList, year, Month.ToString().ToInt32(), 8).ToDecimal();
            if (FinishCount > 0)
            {
                return (FiniMoney / FinishCount).ToString("0.00");
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取订单数量
        /// </summary>
        /// <param name="Star"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        public string GetFloat(object Plan, object End)
        {

            Decimal ReturnValue = 0.00M;
            if (Plan != null && End != null)
            {
                if (Plan.ToString().ToDecimal() > 0)
                {
                    ReturnValue = End.ToString().ToDecimal() / Plan.ToString().ToDecimal();
                    return ReturnValue.ToString("0.00%");
                }
                else
                {
                    return "0%";
                }
            }
            return "0%";

        }


        /// <summary>
        /// 根据年份和类型获取合计数据
        /// </summary>
        /// <param name="Year"></param>
        /// <returns></returns>
        public string GetSumByYear(object Year, int Type)
        {
            Department ObjDepartmentBLL = new Department();
            List<FL_Target> ObjList = new List<FL_Target>();
            ObjList.Add(ObjTargetBLL.GetByID(7));
            FinishTargetSum ObjFinishTargetSumBLL = new FinishTargetSum();
            List<int> EmployeeKey = new List<int>();
            var ObjDepartMentList = ObjDepartmentBLL.GetMyManagerDepartment(User.Identity.Name.ToInt32());
            string KeyList = string.Empty;

            foreach (var Objdepartment in ObjDepartMentList)
            {
                KeyList += Objdepartment.DepartmentID + ",";

            }
            KeyList = KeyList.Trim(',');

            EmployeeKey.Add(User.Identity.Name.ToInt32());

            EmployeeKey.Add(User.Identity.Name.ToInt32());
            var ObjNeed = ObjFinishTargetSumBLL.GetinEmployeeKeyListNeedCreate(EmployeeKey, 1);
            if (ObjNeed.Count > 0)
            {


                switch (Type)
                {
                    //计划完成 上年合计！
                    case 1:

                        var SumList = ObjFinishTargetSumBLL.GetDepartmentTarget(KeyList, " and Year=" + Year.ToString() + " and IsActive=1 and TargetTitle='" + ObjNeed[0].TargetTitle + "'");
                        if (SumList.Count > 0)
                        {
                            return (SumList[0].MonthPlan1 + SumList[0].MonthPlan2 + SumList[0].MonthPlan3 + SumList[0].MonthPlan4 + SumList[0].MonthPlan5 + SumList[0].MonthPlan6 + SumList[0].MonthPlan7 + SumList[0].MonthPlan8 + SumList[0].MonthPlan9 + SumList[0].MonthPlan10 + SumList[0].MonthPlan11 + SumList[0].MonthPlan12).ToString();
                        }
                        return "0";
                        break;
                    //计划完成 历史累计！
                    case 2:
                        var SumList1 = ObjFinishTargetSumBLL.GetDepartmentTarget(KeyList, " and Year>" + 1900 + " and Year<2100 and IsActive=1 and TargetTitle='" + ObjNeed[0].TargetTitle + "'");
                        if (SumList1.Count > 0)
                        {
                            decimal? ReturnValue = 0M;
                            foreach (var Objitem in SumList1)
                            {
                                ReturnValue += (Objitem.MonthPlan1 + Objitem.MonthPlan2 + Objitem.MonthPlan3 + Objitem.MonthPlan4 + Objitem.MonthPlan5 + Objitem.MonthPlan6 + Objitem.MonthPlan7 + Objitem.MonthPlan8 + Objitem.MonthPlan9 + Objitem.MonthPlan10 + Objitem.MonthPlan11 + Objitem.MonthPlan12);
                            }
                            return ReturnValue.ToString();
                        }
                        return "0";
                        break;
                    //上年合计实际完成
                    case 3:
                        var SumList2 = ObjFinishTargetSumBLL.GetDepartmentTarget(KeyList, " and Year=" + Year.ToString() + " and IsActive=1 and TargetTitle='" + ObjNeed[0].TargetTitle + "'");
                        if (SumList2.Count > 0)
                        {
                            return (SumList2[0].MonthFinsh1 + SumList2[0].MonthFinish2 + SumList2[0].MonthFinish3 + SumList2[0].MonthFinish4 + SumList2[0].MonthFinish5 + SumList2[0].MonthFinish6 + SumList2[0].MonthFinish7 + SumList2[0].MonthFinish8 + SumList2[0].MonthFinish9 + SumList2[0].MonthFinish10 + SumList2[0].MonthFinish11 + SumList2[0].MonthFinish12).ToString();
                        }
                        return "0";
                        break;
                    case 4:
                        var SumList3 = ObjFinishTargetSumBLL.GetDepartmentTarget(KeyList, " and Year>" + 1900 + " and Year<2100 and IsActive=1 and TargetTitle='" + ObjNeed[0].TargetTitle + "'");
                        if (SumList3.Count > 0)
                        {
                            decimal? ReturnValue1 = 0M;
                            foreach (var Objitem in SumList3)
                            {
                                ReturnValue1 += (Objitem.MonthFinsh1 + Objitem.MonthFinish2 + Objitem.MonthFinish3 + Objitem.MonthFinish4 + Objitem.MonthFinish5 + Objitem.MonthFinish6 + Objitem.MonthFinish7 + Objitem.MonthFinish8 + Objitem.MonthFinish9 + Objitem.MonthFinish10 + Objitem.MonthFinish11 + Objitem.MonthFinish12);
                            }
                            return ReturnValue1.ToString();
                        }
                        return "0";
                        break;
                }
            }

            return "0";
        }

        /// <summary>
        /// 新订单
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetNewOrder(int Month)
        {

            DateTime Star = DateTime.Now;
            DateTime End = DateTime.Now;
            if (ddlEmployee.SelectedValue.ToInt32() > 0)
            {
                EmployeeKey = ddlEmployee.SelectedValue.ToInt32();
            }
            else
            {
                EmployeeKey = User.Identity.Name.ToInt32();
            }
            switch (Month)
            {
                case 13:
                    Star = (year + "-1-01").ToDateTime();
                    End = (year + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjQuotedPriceBLL.GetQuotedSumMOney(Star, End, 3, EmployeeKey, 1).ToString();
                    // ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-1-01," + year + "-12-" + DateTime.DaysInMonth(year, 12)));
                    break;

                case 14:
                    Star = ((year - 1) + "-1-01").ToDateTime();
                    End = ((year - 1) + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjQuotedPriceBLL.GetQuotedSumMOney(Star, End, 3, EmployeeKey, 1).ToString();
                    //  ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 1) + "-1-01," + (year - 1) + "-12-" + DateTime.DaysInMonth((year - 1), 12)));
                    break;

                case 15:
                    Star = ((year - 50) + "-1-01").ToDateTime();
                    End = ((year + 50) + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjQuotedPriceBLL.GetQuotedSumMOney(Star, End, 3, EmployeeKey, 1).ToString();
                    //ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 50) + "-1-01," + year + "-12-" + DateTime.DaysInMonth((year + 50), 12)));
                    break;
                default:
                    Star = (year + "-" + Month.ToString() + "-1").ToDateTime();
                    End = (year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())).ToDateTime();
                    return ObjQuotedPriceBLL.GetQuotedSumMOney(Star, End, 3, EmployeeKey, 1).ToString();
                    //ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-" + Month.ToString() + "-01," + year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())));
                    break;
            }
            //return string.Empty;
            //return ObjQuotedPriceBLL.GetQuotedSumMOney((year + "-" + Month + "-01").ToDateTime(), (year + "-" + Month + "-" + DateTime.DaysInMonth(year, Month)).ToDateTime(), 3, User.Identity.Name.ToInt32(), 1).ToString();
        }

        /// <summary>
        /// 流水
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetLiuShui(int Month)
        {

            DateTime Star = DateTime.Now;
            DateTime End = DateTime.Now;
            switch (Month)
            {
                case 13:
                    Star = (year + "-1-01").ToDateTime();
                    End = (year + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjQuotedCollectionsPlanBLL.GetMoneySumBYYearMOnth(ddlDepartment.SelectedValue.ToInt32(), ddlEmployee.SelectedValue.ToInt32(), Star, End).ToString();
                    // ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-1-01," + year + "-12-" + DateTime.DaysInMonth(year, 12)));
                    break;

                case 14:
                    Star = ((year - 1) + "-1-01").ToDateTime();
                    End = ((year - 1) + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjQuotedCollectionsPlanBLL.GetMoneySumBYYearMOnth(ddlDepartment.SelectedValue.ToInt32(), ddlEmployee.SelectedValue.ToInt32(), Star, End).ToString();
                    //  ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 1) + "-1-01," + (year - 1) + "-12-" + DateTime.DaysInMonth((year - 1), 12)));
                    break;

                case 15:
                    Star = ((year - 50) + "-1-01").ToDateTime();
                    End = ((year + 50) + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjQuotedCollectionsPlanBLL.GetMoneySumBYYearMOnth(ddlDepartment.SelectedValue.ToInt32(), ddlEmployee.SelectedValue.ToInt32(), Star, End).ToString();
                    //ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 50) + "-1-01," + year + "-12-" + DateTime.DaysInMonth((year + 50), 12)));
                    break;
                default:
                    Star = (year + "-" + Month.ToString() + "-1").ToDateTime();
                    End = (year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())).ToDateTime();
                    return ObjQuotedCollectionsPlanBLL.GetMoneySumBYYearMOnth(ddlDepartment.SelectedValue.ToInt32(), ddlEmployee.SelectedValue.ToInt32(), Star, End).ToString();
                    //ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-" + Month.ToString() + "-01," + year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())));
                    break;
            }
            return string.Empty;
            // return ObjQuotedCollectionsPlanBLL.GetMoneySumBYYearMOnth(year, Month).ToString();
        }


        /// <summary>
        /// 当期应收
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetYinShou(int Month)
        {

            DateTime Star = DateTime.Now;
            DateTime End = DateTime.Now;
            switch (Month)
            {
                case 13:
                    Star = (year + "-1-01").ToDateTime();
                    End = (year + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjQuotedCollectionsPlanBLL.GetYinShouMoneySumBYYearMOnth(ddlDepartment.SelectedValue.ToInt32(), ddlEmployee.SelectedValue.ToInt32(), Star, End).ToString();
                    // ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-1-01," + year + "-12-" + DateTime.DaysInMonth(year, 12)));
                    break;

                case 14:
                    Star = ((year - 1) + "-1-01").ToDateTime();
                    End = ((year - 1) + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjQuotedCollectionsPlanBLL.GetYinShouMoneySumBYYearMOnth(ddlDepartment.SelectedValue.ToInt32(), ddlEmployee.SelectedValue.ToInt32(), Star, End).ToString();
                    //  ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 1) + "-1-01," + (year - 1) + "-12-" + DateTime.DaysInMonth((year - 1), 12)));
                    break;

                case 15:
                    Star = ((year - 50) + "-1-01").ToDateTime();
                    End = ((year + 50) + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjQuotedCollectionsPlanBLL.GetYinShouMoneySumBYYearMOnth(ddlDepartment.SelectedValue.ToInt32(), ddlEmployee.SelectedValue.ToInt32(), Star, End).ToString();
                    //ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 50) + "-1-01," + year + "-12-" + DateTime.DaysInMonth((year + 50), 12)));
                    break;
                default:
                    Star = (year + "-" + Month.ToString() + "-1").ToDateTime();
                    End = (year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())).ToDateTime();
                    return ObjQuotedCollectionsPlanBLL.GetYinShouMoneySumBYYearMOnth(ddlDepartment.SelectedValue.ToInt32(), ddlEmployee.SelectedValue.ToInt32(), Star, End).ToString();
                    //ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-" + Month.ToString() + "-01," + year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())));
                    break;
            }
            return string.Empty;

        }


        /// <summary>
        /// 获取当期渠道费用
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetPayNeedByMonth(int Month)
        {
            return ObjPayNeedRabateBLL.GetQudaoMoneySumBYYearMOnth(year, Month).ToString();

        }


        /// <summary>
        /// 获取平均订单量
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetAvgMoneyForQuotedPrice(int Month)
        {
            return ObjQuotedPriceBLL.GetAvgMoneyByMonth(year, Month).ToString("0.00");
        }


        /// <summary>
        /// 获取入客量
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetRuke(int Month)
        {
            return ObjTelemarketingBLL.GetCustomerSum(year, Month).ToString();
        }


        /// <summary>
        /// 总预订率
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetSucessOrderCount(object Month)
        {

            List<ObjectParameter> ObjParList = new List<ObjectParameter>();
            switch (Month.ToString().ToInt32())
            {
                case 13:
                    ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-1-01," + year + "-12-" + DateTime.DaysInMonth(year, 12)));
                    break;

                case 14:
                    ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 1) + "-1-01," + (year - 1) + "-12-" + DateTime.DaysInMonth((year - 1), 12)));
                    break;

                case 15:
                    ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 50) + "-1-01," + year + "-12-" + DateTime.DaysInMonth((year + 50), 12)));
                    break;
                default:
                    ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-" + Month.ToString() + "-01," + year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())));
                    break;

            }


            return ObjOrderBLL.GetOrderSumByWhere(ObjParList, 2);
        }

        /// <summary>
        /// 获取成交率
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetSucessCustomer(int Month)
        {
            return ObjCustomersBLL.GetAvgSucess(DateTime.Now.Year, Month);
        }

        /// 获取入客量
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetKeYuanliang(int Month)
        {
            DateTime Star = DateTime.Now;
            DateTime End = DateTime.Now;
            DepartmentKey = ddlDepartment.SelectedValue.ToInt32();
            EmployeeKey = ddlEmployee.SelectedValue.ToInt32();
            switch (Month)
            {
                case 13:
                    Star = (year + "-1-01").ToDateTime();
                    End = (year + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjTelemarketingBLL.GetAllCustomerSum(DepartmentKey, EmployeeKey, Star, End).ToString();
                    // ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-1-01," + year + "-12-" + DateTime.DaysInMonth(year, 12)));
                    break;

                case 14:
                    Star = ((year - 1) + "-1-01").ToDateTime();
                    End = ((year - 1) + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjTelemarketingBLL.GetAllCustomerSum(DepartmentKey, EmployeeKey, Star, End).ToString();
                    //  ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 1) + "-1-01," + (year - 1) + "-12-" + DateTime.DaysInMonth((year - 1), 12)));
                    break;

                case 15:
                    Star = ((year - 50) + "-1-01").ToDateTime();
                    End = ((year + 50) + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjTelemarketingBLL.GetAllCustomerSum(DepartmentKey, EmployeeKey, Star, End).ToString();
                    //ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 50) + "-1-01," + year + "-12-" + DateTime.DaysInMonth((year + 50), 12)));
                    break;
                default:
                    Star = (year + "-" + Month.ToString() + "-1").ToDateTime();
                    End = (year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())).ToDateTime();
                    return ObjTelemarketingBLL.GetAllCustomerSum(DepartmentKey, EmployeeKey, Star, End).ToString();
                    //ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-" + Month.ToString() + "-01," + year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())));
                    break;
            }


        }


        /// <summary>
        /// 有效率
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetSucessTel(int Month)
        {
            DateTime Star = DateTime.Now;
            DateTime End = DateTime.Now;
            switch (Month)
            {
                case 13:
                    Star = (year + "-1-01").ToDateTime();
                    End = (year + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjTelemarketingBLL.GetAllSucess(ddlDepartment.SelectedValue.ToInt32(), ddlEmployee.SelectedValue.ToInt32(), Star, End).ToString();
                    // ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-1-01," + year + "-12-" + DateTime.DaysInMonth(year, 12)));
                    break;

                case 14:
                    Star = ((year - 1) + "-1-01").ToDateTime();
                    End = ((year - 1) + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjTelemarketingBLL.GetAllSucess(ddlDepartment.SelectedValue.ToInt32(), ddlEmployee.SelectedValue.ToInt32(), Star, End).ToString();
                    //  ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 1) + "-1-01," + (year - 1) + "-12-" + DateTime.DaysInMonth((year - 1), 12)));
                    break;

                case 15:
                    Star = ((year - 50) + "-1-01").ToDateTime();
                    End = ((year + 50) + "-12-" + DateTime.DaysInMonth(year, 12)).ToDateTime();
                    return ObjTelemarketingBLL.GetAllSucess(ddlDepartment.SelectedValue.ToInt32(), ddlEmployee.SelectedValue.ToInt32(), Star, End).ToString();
                    //ObjParList.Add(new ObjectParameter("CreateDate_between", (year - 50) + "-1-01," + year + "-12-" + DateTime.DaysInMonth((year + 50), 12)));
                    break;
                default:
                    Star = (year + "-" + Month.ToString() + "-1").ToDateTime();
                    End = (year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())).ToDateTime();
                    return ObjTelemarketingBLL.GetAllSucess(ddlDepartment.SelectedValue.ToInt32(), ddlEmployee.SelectedValue.ToInt32(), Star, End).ToString();
                    //ObjParList.Add(new ObjectParameter("CreateDate_between", year + "-" + Month.ToString() + "-01," + year + "-" + Month.ToString() + "-" + DateTime.DaysInMonth(year, Month.ToString().ToInt32())));
                    break;
            }
            return string.Empty;
        }


        public string Getmanyidu(int Month)
        {
            DegreeOfSatisfaction ObjBLL = new DegreeOfSatisfaction();
            DateTime TimerStae = (year + "-" + Month + "-1").ToDateTime();
            DateTime TimerEnd = (year + "-" + Month + "-" + DateTime.DaysInMonth(year, Month)).ToDateTime();
            return ObjBLL.GetManyidu(TimerStae, TimerEnd);
        }

        /// <summary>
        /// 获取当期投诉量
        /// </summary>
        /// <param name="Month"></param>
        /// <returns></returns>
        public string GetComPlainSum(int Month)
        {

            return ObjComplainBLL.GetComplainSumByDate(year, Month).ToString();
        }

        public string GetCostByMOnth(int Month)
        {
            return ObjCostBLL.GetAvgProfitMargin(DateTime.Now.Year, Month).ToString("0.00");
        }

        protected void DdlRangeYear1_SelectedIndexChanged(object sender, EventArgs e)
        {
            hideKey.Value = DdlRangeYear1.SelectedItem.Text;
        }


        /// <summary>
        /// 绑定员工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlEmployee.BinderByDepartment(ddlDepartment.SelectedValue.ToInt32());

        }

        protected void btnSerch_Click(object sender, EventArgs e)
        {
            BinderPage(ddlDepartment.SelectedValue.ToInt32());
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + Guid.NewGuid().ToString() + ".xls");
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/vnd.ms-excel";
            this.EnableViewState = false;
        }
    }

}