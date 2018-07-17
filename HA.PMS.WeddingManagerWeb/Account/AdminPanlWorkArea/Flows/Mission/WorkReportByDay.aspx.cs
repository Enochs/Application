using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class WorkReportByDay : SystemPage
    {
        /// <summary>
        /// 员工
        /// </summary>
        Employee ObjEmployeeBLL = new Employee();

        /// <summary>
        /// 顾客
        /// </summary>
        Customers ObjCustomerBLL = new Customers();

        /// <summary>
        /// 日报表(每日)
        /// </summary>
        WorkReport ObjWorkReportBLL = new WorkReport();

        /// <summary>
        /// 当天的起始时间
        /// </summary>
        DateTime Start = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
        DateTime End = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
        string OrderByName = "EmployeeID";
        int SourceCount = 0;

        List<View_WorkReport> DataList = null;


        #region 页面加载
        /// <summary>
        /// 页面初始化
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreateWorkReport();
                BinderData();
            }
        }
        #endregion

        #region 数据绑定
        /// <summary>
        /// 绑定数据
        /// </summary>

        public void BinderData()
        {
            List<PMSParameters> pars = new List<PMSParameters>();

            if (ddlDepartment.SelectedValue.ToInt32() > 0)
            {
                pars.Add("DepartmentID", ddlDepartment.SelectedValue.ToInt32(), NSqlTypes.Equal);
            }

            //责任人
            if (ddlEmployee.SelectedValue.ToInt32() > 0)
            {
                pars.Add("EmployeeID", ddlEmployee.SelectedValue.ToInt32(), NSqlTypes.Equal);
                OrderByName = "CreateDate";
            }
            else
            {
                pars.Add("EmployeeID", User.Identity.Name.ToInt32(), NSqlTypes.Equal, true);
            }

            //生成时间
            if (DateRanger.IsNotBothEmpty)
            {
                pars.Add("CreateDate", DateRanger.StartoEnd, NSqlTypes.DateBetween);
                string[] time = DateRanger.StartoEnd.Split(',');
                if (time[0] != "" && time[1] != "")
                {
                    Start = time[0].ToDateTime();
                    End = time[1].ToDateTime();
                }
                else if (time[0] == "" && time[1] != "")
                {
                    End = time[1].ToDateTime();
                }
                else if (time[0] != "" && time[1] == "")
                {
                    Start = time[0].ToDateTime();
                }
            }
            else        //没有选择时间
            {
                if (ddlEmployee.SelectedValue.ToInt32() > 0)      //选择了人  默认查询这个人所有的记录 (本来默认是显示当天的  既然选择了人  就查看这个人本月所有的记录)
                {
                    DateTime now = DateTime.Now;
                    Start = new DateTime(now.Year, now.Month, 1);
                    pars.Add("CreateDate", Start + "," + End, NSqlTypes.DateBetween);

                }
                else
                {
                    pars.Add("CreateDate", Start + "," + End, NSqlTypes.DateBetween);
                }
            }

            DataList = ObjWorkReportBLL.GetDataByParameters(pars, OrderByName, 200, 1, out SourceCount);
            GetTotal(DataList);
            repWorkReport.DataBind(DataList);


        }
        #endregion

        #region 查询事件
        /// <summary>
        /// 查询
        /// </summary>
        protected void btnLookFor_Click(object sender, EventArgs e)
        {
            BinderData();
        }
        #endregion

        #region 新增日报表
        /// <summary>
        /// 新增/修改
        /// </summary>
        public void CreateWorkReport()
        {
            Start = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00"));
            End = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59"));
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
                        workReport.CreateDate = DateTime.Now.ToString("yyyy-MM-dd").ToDateTime();               //生成时间 显示 这样方便查询
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
                        workReport.CreateDate = DateTime.Now.ToString("yyyy-MM-dd").ToDateTime();               //生成时间 显示 这样方便查询
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

        #region 点击标头进行排序
        /// <summary>
        /// 排序
        /// </summary>
        protected void lbtnNewCreate_Click(object sender, EventArgs e)
        {
            LinkButton lbtn = (sender as LinkButton);
            if (lbtn.ID == "lbtnNewCreate")
            {
                OrderByName = "CreateNum";
            }
            else if (lbtn.ID == "lbtnInviteNum")
            {
                OrderByName = "InviteNum";
            }
            else if (lbtn.ID == "lbtnLoseInviteNum")
            {
                OrderByName = "LoseInviteNum";
            }
            else if (lbtn.ID == "lbtnInviteSuccess")
            {
                OrderByName = "InviteSuccessNum";
            }
            else if (lbtn.ID == "lbtnOrderNum")
            {
                OrderByName = "OrderNum";
            }
            else if (lbtn.ID == "lbtnLoseOrderNum")
            {
                OrderByName = "LoseOrderNum";
            }
            else if (lbtn.ID == "lbtnOrderSuccessNum")
            {
                OrderByName = "OrderSuccessNum";
            }
            else if (lbtn.ID == "lbtnQuotedCheckNum")
            {
                OrderByName = "QuotedCheckNum";
            }
            else if (lbtn.ID == "lbtnFinishAmount")
            {
                OrderByName = "FinishAmount";
            }
            else if (lbtn.ID == "lbtnOrderamount")
            {
                OrderByName = "OrderAmount";
            }
            BinderData();
        }
        #endregion

        public void GetTotal(List<View_WorkReport> DataList)
        {
            //lblRecordSum.Text = DataList.Sum(C => C.CreateNum).ToString();                  //新录入
            lblInviteSum.Text = DataList.Sum(C => C.InviteNum).ToString();                  //电销量
            //lblInviteLose.Text = DataList.Sum(C => C.LoseInviteNum).ToString();             //流失  (邀约中)
            //lblInviteSumccess.Text = DataList.Sum(C => C.InviteSuccessNum).ToString();      //邀约成功量 
            lblOrderSum.Text = DataList.Sum(C => C.OrderNum).ToString();                    //跟单量
            //lblOrderLose.Text = DataList.Sum(C => C.LoseOrderNum).ToString();               //流失量(跟单)
            //lblOrderSumccess.Text = DataList.Sum(C => C.OrderSuccessNum).ToString();        //成功预定
            //lblQuotedSuccess.Text = DataList.Sum(C => C.QuotedCheckNum).ToString();         //获取已签约量
            //lblFinishAmountSum.Text = DataList.Sum(C => C.FinishAmount).ToString().ToDecimal().ToString("f2");         //获取现金流
            //lblOrderMoneySum.Text = DataList.Sum(C => C.OrderAmount).ToString().ToDecimal().ToString("f2");        //获取订单金额

        }

        #region 各种合计

        //获取新录入
        public int GetSumByToday()
        {
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()) == true)
            {
                if (ddlEmployee.SelectedValue.ToInt32() > 0)
                {
                    return ObjCustomerBLL.GetSumByToday(Start, End, ddlEmployee.SelectedValue.ToInt32());
                }
                else
                {
                    return ObjCustomerBLL.GetSumByToday(Start, End);
                }
            }
            else
            {
                return ObjCustomerBLL.GetSumByToday(Start, End, User.Identity.Name.ToInt32());
            }
        }

        //获取电销量
        public int GetInviteSums()
        {
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()) == true)
            {
                if (ddlEmployee.SelectedValue.ToInt32() > 0)
                {
                    return ObjCustomerBLL.GetInviteSumByToday(Start, End, ddlEmployee.SelectedValue.ToInt32());
                }
                else
                {

                    return ObjCustomerBLL.GetInviteSumByToday(Start, End);
                }
            }
            else
            {
                return ObjCustomerBLL.GetInviteSumByToday(Start, End, User.Identity.Name.ToInt32());
            }
        }

        //获取 7.流失(邀约中) 6.邀约成功
        public int GetInviteSum(object Source)
        {
            int State = Source.ToString().ToInt32();
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()) == true)
            {
                if (ddlEmployee.SelectedValue.ToInt32() > 0)
                {
                    return ObjCustomerBLL.GetInviteSumByTodays(Start, End, State, ddlEmployee.SelectedValue.ToInt32());
                }
                else
                {
                    return ObjCustomerBLL.GetInviteSumByTodays(Start, End, State);
                }
            }
            else
            {
                return ObjCustomerBLL.GetInviteSumByTodays(Start, End, State, User.Identity.Name.ToInt32());
            }
        }

        //获取跟单量
        public int GetOrderSums()
        {
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()) == true)
            {
                if (ddlEmployee.SelectedValue.ToInt32() > 0)
                {
                    return ObjCustomerBLL.GetOrderSumByToday(Start, End, ddlEmployee.SelectedValue.ToInt32());
                }
                else
                {
                    return ObjCustomerBLL.GetOrderSumByToday(Start, End);
                }
            }
            else
            {
                return ObjCustomerBLL.GetOrderSumByToday(Start, End, User.Identity.Name.ToInt32());
            }
        }

        //获取 10.流失(跟单中) 13.成功预定
        public int GetOrderSum(object Source)
        {
            int State = Source.ToString().ToInt32();
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()) == true)
            {
                if (ddlEmployee.SelectedValue.ToInt32() > 0)
                {
                    return ObjCustomerBLL.GetOrderSumByToday(Start, End, State, ddlEmployee.SelectedValue.ToInt32());
                }
                else
                {
                    return ObjCustomerBLL.GetOrderSumByToday(Start, End, State);
                }
            }
            else
            {
                return ObjCustomerBLL.GetOrderSumByToday(Start, End, State, User.Identity.Name.ToInt32());
            }
        }

        //获取 15.已签约
        public int GetQuotedSum(object Source)
        {
            int State = Source.ToString().ToInt32();
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()) == true)
            {
                if (ddlEmployee.SelectedValue.ToInt32() > 0)
                {
                    return ObjCustomerBLL.GetQuotedSumByToday(Start, End, ddlEmployee.SelectedValue.ToInt32());
                }
                else
                {
                    return ObjCustomerBLL.GetQuotedSumByToday(Start, End, State);
                }
            }
            else
            {
                return ObjCustomerBLL.GetQuotedSumByToday(Start, End, User.Identity.Name.ToInt32());
            }
        }

        //获取现金流
        public decimal GetFinishAmountSum()
        {
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()) == true)
            {
                if (ddlEmployee.SelectedValue.ToInt32() > 0)
                {
                    return ObjCustomerBLL.GetFinishAmountSumByToday(Start, End, ddlEmployee.SelectedValue.ToInt32());
                }
                else
                {
                    return ObjCustomerBLL.GetFinishAmountSumByToday(Start, End);
                }
            }
            else
            {
                return ObjCustomerBLL.GetFinishAmountSumByToday(Start, End, User.Identity.Name.ToInt32());
            }
        }

        //获取订单金额
        public decimal GetOrderAmountSum()
        {
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()) == true)
            {
                if (ddlEmployee.SelectedValue.ToInt32() > 0)
                {
                    return ObjCustomerBLL.GetOrderAmountSumByToday(Start, End, ddlEmployee.SelectedValue.ToInt32());
                }
                else
                {
                    return ObjCustomerBLL.GetOrderAmountSumByToday(Start, End);
                }
            }
            else
            {
                return ObjCustomerBLL.GetOrderAmountSumByToday(Start, End, User.Identity.Name.ToInt32());
            }
        }

        #endregion

        #region 选择部门变化事件
        /// <summary>
        /// 变化事件
        /// </summary> 
        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            int DepartmentID = ddlDepartment.SelectedValue.ToInt32();
            var ObjEmployeeList = ObjEmployeeBLL.GetByDepartmetnID(DepartmentID);
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataBind(ObjEmployeeList);
            ddlEmployee.Items.Insert(0, new ListItem { Text = "请选择", Value = "0" });
        }
        #endregion

    }

}