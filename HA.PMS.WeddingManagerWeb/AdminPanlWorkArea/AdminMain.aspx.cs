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
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Web;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea
{
    public partial class AdminMain : SystemPage
    {
        string ConnString = ConfigurationManager.AppSettings["PMS_WeddingEntities"];
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

        Celebration ObjCelebrationBll = new Celebration();

        DispatchingState ObjStateBLL = new DispatchingState();

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

            //自动完成
            IsFinish();

            #region 登录日志文件

            LoginLog ObjLoginLogBLL = new LoginLog();
            sys_LoginLog LoginModel = new sys_LoginLog();
            LoginModel.EmployeeId = User.Identity.Name.ToInt32();
            LoginModel.IpAddress = Page.Request.UserHostAddress;
            LoginModel.CreateDate = DateTime.Now;
            ObjLoginLogBLL.Insert(LoginModel);

            #endregion

            //新增日报表
            CreateWorkReport();     //昨天
            //数据备份
            BackUp();
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

            //ObjStateBLL.Handle();
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
                        workReport.InviteSumTotal = ObjCustomerBLL.GetInviteSumTotal(EmployeeID, Start, End);               //潜在客户(邀约)
                        workReport.CreateNum = ObjCustomerBLL.GetNumByToday(EmployeeID, Start, End);                        //新录入
                        workReport.InviteNum = ObjCustomerBLL.GetInviteNumByToday(EmployeeID, Start, End);                  //电销量
                        workReport.LoseInviteNum = ObjCustomerBLL.GetInviteNumByToday(EmployeeID, Start, End, 7);           //流失  (邀约中)
                        workReport.InviteSuccessNum = ObjCustomerBLL.GetInviteNumByToday(EmployeeID, Start, End, 6);        //邀约成功量 
                        workReport.OrderSumTotal = ObjCustomerBLL.GetOrderSumTotal(EmployeeID, Start, End);                 //潜在客户(跟单)
                        workReport.OrderNum = ObjCustomerBLL.GetOrderNumByToday(EmployeeID, Start, End);                    //跟单量
                        workReport.LoseOrderNum = ObjCustomerBLL.GetOrderNumByToday(EmployeeID, Start, End, 10);            //流失量(跟单)
                        workReport.OrderSuccessNum = ObjCustomerBLL.GetOrderNumByToday(EmployeeID, Start, End, 13);         //成功预定
                        workReport.QuotedCheckNum = ObjCustomerBLL.GetQuotedNumByToday(EmployeeID, Start, End);             //获取已签约量
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
                        workReport.InviteSumTotal = ObjCustomerBLL.GetInviteSumTotal(EmployeeID, Start, End);               //潜在客户(邀约)
                        workReport.CreateNum = ObjCustomerBLL.GetNumByToday(EmployeeID, Start, End);                        //新录入
                        workReport.InviteNum = ObjCustomerBLL.GetInviteNumByToday(EmployeeID, Start, End);                  //电销量
                        workReport.LoseInviteNum = ObjCustomerBLL.GetInviteNumByToday(EmployeeID, Start, End, 7);           //流失  (邀约中)
                        workReport.InviteSuccessNum = ObjCustomerBLL.GetInviteNumByToday(EmployeeID, Start, End, 6);        //邀约成功量 
                        workReport.OrderSumTotal = ObjCustomerBLL.GetOrderSumTotal(EmployeeID, Start, End);                 //潜在客户(跟单)
                        workReport.OrderNum = ObjCustomerBLL.GetOrderNumByToday(EmployeeID, Start, End);                    //跟单量
                        workReport.LoseOrderNum = ObjCustomerBLL.GetOrderNumByToday(EmployeeID, Start, End, 10);            //流失量(跟单)
                        workReport.OrderSuccessNum = ObjCustomerBLL.GetOrderNumByToday(EmployeeID, Start, End, 13);         //成功预定
                        workReport.QuotedCheckNum = ObjCustomerBLL.GetQuotedNumByToday(EmployeeID, Start, End);             //获取已签约量
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

        public void BackUp()
        {
            SqlConnection conn = new SqlConnection();

            try
            {
                string ip = HttpContext.Current.Request.UserHostAddress;
                conn = new SqlConnection("server=" + ip + ";uid=sa;pwd=sasa;database=PMS_Wedding");
                string sql = "backup database PMS_Wedding to disk='D:/BackUp/PMS_Wedding.bak'";
                conn.Open();

                //首先判断文件夹是否存在
                if (!Directory.Exists("D:/BackUp"))       //如果文件夹不存在  就新建一个文件夹
                {
                    Directory.CreateDirectory("D:/BackUp");
                }

                if (File.Exists("D:/BackUp/PMS_Wedding.bak"))   //文件名称已存在
                {
                    File.Delete("D:/BackUp/PMS_Wedding.bak");
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();      //执行sql语句
                    JavaScriptTools.AlertWindow("备份数据成功", Page);
                }
                else        //文件不存在  第一次生成备份数据
                {

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();      //执行sql语句
                    JavaScriptTools.AlertWindow("备份数据成功", Page);

                }
            }
            catch
            {
                JavaScriptTools.AlertWindow("备份数据失败,请稍后再试...", Page);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
