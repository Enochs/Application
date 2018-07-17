using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Linq;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.CS;
using System.Collections.Generic;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Report;
using HA.PMS.BLLAssmblly.FD;
using System.Net;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class AdminMain : SystemPage
    {
        /// <summary>
        /// 客户
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 报价单
        /// </summary>
        HA.PMS.BLLAssmblly.Flow.QuotedPrice ObjQuotePriceBLL = new BLLAssmblly.Flow.QuotedPrice();

        /// <summary>
        /// 内部员工
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();


        /// <summary>
        /// 满意度
        /// </summary>
        DegreeOfSatisfaction ObjDegreeBLL = new DegreeOfSatisfaction();

        /// <summary>
        /// 收款
        /// </summary>
        QuotedCollectionsPlan ObjPlanBLL = new QuotedCollectionsPlan();

        /// <summary>
        /// 通知
        /// </summary>
        Report ObjReportBLL = new Report();

        WorkReport ObjWorkReportBLL = new WorkReport();

        #region 页面加载

        protected void Page_Load(object sender, EventArgs e)
        {
            string ABCD = Request.Cookies["HAEmployeeID"].Value;

            #region 登录日志文件

            LoginLog ObjLoginLogBLL = new LoginLog();
            sys_LoginLog LoginModel = new sys_LoginLog();
            LoginModel.EmployeeId = User.Identity.Name.ToInt32();
            LoginModel.IpAddress = Page.Request.UserHostAddress;
            LoginModel.CreateDate = DateTime.Now;
            ObjLoginLogBLL.Insert(LoginModel);

            #endregion



            //自动完成
            IsFinish();
            //满意度调查
            BindSatisfaction();
            //新增日报表
            CreateWorkReport();
        }
        #endregion


        #region 满意度调查
        public void BindSatisfaction()
        {
            //删除所有重复的满意度调查  只要有评价过 删除为评价的 否则删除所有
            var query = ObjDegreeBLL.GetByAll().GroupBy(C => C.CustomerID).ToList();
            for (int i = 0; i < query.Count; i++)
            {
                if (query[i].ToList().Count() >= 2)
                {
                    foreach (var item in query[i])
                    {
                        int CustomerID = item.CustomerID.ToString().ToInt32();
                        if (CustomerID == 4012)
                        {
                            int index = 1;
                            if (item.State != 2)        // 2.已评价过的 0.是未评价的  删除为评价的
                            {
                                ObjDegreeBLL.Deletes(item);
                                index++;
                            }
                        }
                    }
                }
            }

            //添加满意度调查
            var DataList = ObjCustomerBLL.GetByAll().Where(C => C.State == 206 || C.State == 208);
            foreach (var item in DataList)
            {
                var ObjDegreeModel = ObjDegreeBLL.GetByCustomersID(item.CustomerID);
                if (ObjDegreeModel == null)             //未创建满意度调查
                {
                    ObjDegreeBLL.Insert(new CS_DegreeOfSatisfaction()
                    {
                        CustomerID = item.CustomerID,
                        SumDof = "",
                        DofContent = "",
                        DofDate = null,
                        IsDelete = false,
                        DegreeResult = null,
                        State = 0,
                        UpdateTime = DateTime.Now.ToString().ToDateTime(),
                        UpdateEmployeeID = User.Identity.Name.ToInt32(),
                        PlanDate = null,
                    });
                }
            }

        }
        #endregion

        #region 自动完成

        public void IsFinish()
        {
            //婚期已过的婚礼
            List<FL_Customers> DataList = ObjCustomerBLL.GetByAll().Where(C => C.State != 29 && C.State == 19 && C.PartyDate <= DateTime.Now).ToList();

            foreach (var item in DataList)
            {
                item.State = 208;
                item.FinishOver = true;
                ObjCustomerBLL.UpdateCustomer(item);
            }
        }

        #endregion

        #region 新增或者修改日报表
        /// <summary>
        /// 新增/修改
        /// </summary>
        public void CreateWorkReport()
        {
            DateTime Start = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00"));      //默认生成昨天的
            DateTime End = Convert.ToDateTime(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
            int Years = Start.Year;
            int Month = Start.Month;
            int Day = Start.Day;

            sys_WorkReport workReport = new sys_WorkReport();
            var EmployeeList = ObjEmployeeBLL.GetByAll().ToList();

            foreach (var item in EmployeeList)
            {
                int EmployeeID = item.EmployeeID;
                if (EmployeeID != 1)
                {
                    if (ObjWorkReportBLL.GetEntityByTimes(EmployeeID, Years, Month, Day) == null)     //该员工的当天汇总不存在 就新增
                    {
                        workReport = new sys_WorkReport();
                        workReport.EmployeeID = item.EmployeeID;                                                            //公司内部员工
                        workReport.CreateNum = ObjCustomerBLL.GetNumByToday(EmployeeID, Start, End);                        //新录入
                        workReport.InviteNum = ObjCustomerBLL.GetInviteNumByToday(EmployeeID, Start, End);                  //电销量
                        workReport.LoseInviteNum = ObjCustomerBLL.GetInviteNumByToday(EmployeeID, Start, End, 7);           //流失  (邀约中)
                        workReport.InviteSuccessNum = ObjCustomerBLL.GetInviteNumByToday(EmployeeID, Start, End, 6);        //邀约成功量 
                        workReport.OrderNum = ObjCustomerBLL.GetOrderNumByToday(EmployeeID, Start, End);                    //跟单量
                        workReport.LoseOrderNum = ObjCustomerBLL.GetOrderNumByToday(EmployeeID, Start, End, 10);            //流失量(跟单)
                        workReport.OrderSuccessNum = ObjCustomerBLL.GetOrderNumByToday(EmployeeID, Start, End, 13);         //成功预定
                        workReport.QuotedCheckNum = ObjCustomerBLL.GetQuotedNumByToday(EmployeeID, Start, End);         //获取已签约量
                        workReport.FinishAmount = ObjCustomerBLL.GetFinishAmountByToday(EmployeeID, Start, End);            //获取现金流
                        workReport.OrderAmount = ObjCustomerBLL.GetOrderAmountByToday(EmployeeID, Start, End);              //获取订单金额
                        workReport.CreateDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd").ToDateTime();               //生成时间 显示昨天 这样方便查询
                        workReport.Year = Years;
                        workReport.Month = Month;
                        workReport.Day = Day;
                        workReport.Remark = string.Empty;

                        ObjWorkReportBLL.Insert(workReport);
                    }
                    else
                    {
                        workReport = ObjWorkReportBLL.GetEntityByTimes(EmployeeID, Years, Month, Day);
                        workReport.EmployeeID = item.EmployeeID;                                                            //公司内部员工
                        workReport.CreateNum = ObjCustomerBLL.GetNumByToday(EmployeeID, Start, End);                        //新录入
                        workReport.InviteNum = ObjCustomerBLL.GetInviteNumByToday(EmployeeID, Start, End);                  //电销量
                        workReport.LoseInviteNum = ObjCustomerBLL.GetInviteNumByToday(EmployeeID, Start, End, 7);           //流失  (邀约中)
                        workReport.InviteSuccessNum = ObjCustomerBLL.GetInviteNumByToday(EmployeeID, Start, End, 6);        //邀约成功量 
                        workReport.OrderNum = ObjCustomerBLL.GetOrderNumByToday(EmployeeID, Start, End);                    //跟单量
                        workReport.LoseOrderNum = ObjCustomerBLL.GetOrderNumByToday(EmployeeID, Start, End, 10);            //流失量(跟单)
                        workReport.OrderSuccessNum = ObjCustomerBLL.GetOrderNumByToday(EmployeeID, Start, End, 13);         //成功预定
                        workReport.QuotedCheckNum = ObjCustomerBLL.GetQuotedNumByToday(EmployeeID, Start, End);         //获取已签约量
                        workReport.FinishAmount = ObjCustomerBLL.GetFinishAmountByToday(EmployeeID, Start, End);            //获取现金流
                        workReport.OrderAmount = ObjCustomerBLL.GetOrderAmountByToday(EmployeeID, Start, End);              //获取订单金额
                        workReport.CreateDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd").ToDateTime();               //生成时间 显示昨天 这样方便查询
                        workReport.Year = Years;
                        workReport.Month = Month;
                        workReport.Day = Day;
                        workReport.Remark = string.Empty;

                        ObjWorkReportBLL.Update(workReport);
                    }
                }

            }
        }
        #endregion

    }
}
