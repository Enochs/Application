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
using System.Data.Objects;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Commandanddispatch.GeneralManagerPodium
{
    public partial class UnderTheWorkingState : SystemPage
    {
        MissionDetailed objMissionDetailedBLL = new MissionDetailed();
        MissionManager ObjMissionBLL = new MissionManager();
        Employee objEmployeeBLL = new Employee();

        Department ObjDeparmentBLL = new Department();

        /// <summary>
        /// 任务zhuti 
        /// </summary>
        MissionManager ObjMissionManagerBLL = new MissionManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                DataBinder();
            }
        }

        protected void DataBinder()
        {
            #region 查询参数
            string GetWhere = string.Empty;
            DateTime StartDate = txtStar.Text.Trim().ToString().ToDateTime();
            DateTime EndsDate = txtEnd.Text.Trim().ToString().ToDateTime();
            if (txtEnd.Text != string.Empty && txtStar.Text != string.Empty)
            {
                GetWhere = "and CreateDate>='" + txtStar.Text.ToDateTime().ToShortDateString() + "' and CreateDate<='" + txtEnd.Text.ToDateTime().ToShortDateString() + "'";
            }
            else if (txtStar.Text != string.Empty && txtStar.Text == string.Empty)
            {
                GetWhere = "and CreateDate>='" + txtStar.Text.ToDateTime().ToShortDateString() + "'";
            }
            else if (txtStar.Text == string.Empty && txtEnd.Text != string.Empty)
            {
                GetWhere = "and CreateDate<='" + txtEnd.Text.ToDateTime().ToShortDateString() + "'";
            }

            var ObjEmpLoyeeList = objEmployeeBLL.GetMyManagerEmpLoyee(User.Identity.Name.ToInt32());
            if (DepartmentDropdownList1.SelectedValue.ToInt32() > 0)
            {
                if (DdlEmployee1.SelectedValue.ToInt32() > 0)
                {
                    int EmployeeID = DdlEmployee1.SelectedValue.ToInt32();
                    if (objEmployeeBLL.IsManager(EmployeeID))
                    {
                        ObjEmpLoyeeList = objEmployeeBLL.GetMyManagerEmpLoyee(DdlEmployee1.SelectedValue.ToInt32());
                    }
                    else
                    {
                        List<Sys_Employee> list = new List<Sys_Employee>();
                        list.Add(new Sys_Employee { EmployeeID = EmployeeID });
                        ObjEmpLoyeeList = list;
                    }
                }
                else
                {
                    ObjEmpLoyeeList = objEmployeeBLL.GetMyManagerEmpLoyee(ObjDeparmentBLL.GetByID(DepartmentDropdownList1.SelectedValue.ToInt32()).DepartmentManager);

                }
            }
            else
            {
                List<Sys_Employee> list = new List<Sys_Employee>();
                var DataList = objEmployeeBLL.GetByAll();
                foreach (var item in DataList)
                {
                    list.Add(new Sys_Employee { EmployeeID = item.EmployeeID });
                }
                ObjEmpLoyeeList = list;
            }

            #endregion

            string EmpLoyeeIDList = string.Empty;
            if (ObjEmpLoyeeList.Count > 0)
            {
                foreach (var ObjEmployeeItem in ObjEmpLoyeeList)
                {
                    EmpLoyeeIDList += ObjEmployeeItem.EmployeeID + ",";
                }

                EmpLoyeeIDList = EmpLoyeeIDList.Trim(',');

                GetWhere += " and EmployeeID in (" + EmpLoyeeIDList + ")";
            }


            Missions Models = new Missions();
            List<Missions> MissionList = new List<Missions>();
            string[] str = EmpLoyeeIDList.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                Models = new Missions();
                var MissionModel = ObjMissionBLL.GetMissionSum(str[i].ToInt32(), 0, GetWhere, 1);        //未完成/进行中

                Models.EmployeeID = str[i].ToInt32();                                                                   //员工ID
                Models.EmployeeName = GetEmployeeName(str[i]).ToString();                                               //员工姓名
                //Models.AllMissionSum = objMissionDetailedBLL.GetCountByEmployeeID(str[i].ToInt32());                  //所有任务
                Models.AllMissionSum = ObjMissionBLL.GetMissionSum(str[i].ToInt32(), 1, GetWhere, 0).MissiionCount;    //所有任务
                Models.UnFinishSum = ObjMissionBLL.GetMissionSum(str[i].ToInt32(), 0, GetWhere, 1).MissiionCount;       //按时进行中
                Models.FinishSum = ObjMissionBLL.GetMissionSum(str[i].ToInt32(), 1, GetWhere, 1).MissiionCount;         //已完成
                Models.DelaySum = ObjMissionBLL.GetMissionSum(str[i].ToInt32(), 2, GetWhere, 1).MissiionCount;          //超时进行中
                Models.OverFinishSum = ObjMissionBLL.GetMissionSum(str[i].ToInt32(), 3, GetWhere, 1).MissiionCount;     //超时已完成

                MissionList.Add(Models);
            }

            rptMission.DataSource = MissionList;
            rptMission.DataBind();


            //rptMission.DataSource = ObjMissionManagerBLL.GetMissionByWhere(GetWhere, ObjEmpLoyeeList);
            //rptMission.DataBind();
        }

        public string GetFinishRate(object source1, object source2)
        {
            decimal AllMissionSum = source1.ToString().ToInt32();
            decimal FinishMissionSum = source2.ToString().ToInt32();
            if (AllMissionSum != 0)
            {
                return (FinishMissionSum / AllMissionSum).ToString("0.00%").ToString();
            }
            else
            {
                return "0.00%";
            }
        }



        /// <summary>
        /// 返回起始时间
        /// </summary>
        /// <returns></returns>
        private DateTime GetDateStar()
        {
            //开始时间
            DateTime startTime = new DateTime();
            //如果没有选择结束时间就默认是当前时间

            //如果时间查询文本框不为空的话，就代表查询这个时间段的，该员工所有的工作任务量
            if (!string.IsNullOrEmpty(txtStar.Text))
            {
                startTime = txtStar.Text.ToDateTime();

            }
            else
            {
                //相反就是今天的任务量
                startTime = DateTime.Now;
            }
            return startTime;
        }
        /// <summary>
        /// 返回截止时间
        /// </summary>
        /// <returns></returns>
        private DateTime GetDateEnd()
        {
            string dateStr = "2100-1-1";

            DateTime endTime = dateStr.ToDateTime();
            if (!string.IsNullOrEmpty(txtEnd.Text))
            {
                endTime = txtEnd.Text.ToDateTime();
            }
            return endTime;

        }
        /// <summary>
        /// 返回时间段参数对象
        /// </summary>
        /// <returns></returns>
        private ObjectParameter GetDateSpan()
        {

            return new ObjectParameter("CreateDate_between", GetDateStar() + "," + GetDateEnd());
        }
        /// <summary>
        ///根据时间获取任务数量
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetSingerEmployeeMissionCountByEmployeeId(object source)
        {
            /// <summary>
            /// 明细操作
            /// </summary>
            MissionDetailed objMissionDetailsedBLL = new MissionDetailed();
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            ObjParameterList.Add(new ObjectParameter("ChecksState", 3));
            ObjParameterList.Add(new ObjectParameter("IsOver", false));
            ObjParameterList.Add(new ObjectParameter("EmployeeID", source.ToString().ToInt32()));

            if (txtStar.Text != string.Empty && txtEnd.Text != string.Empty)
            {
                ObjParameterList.Add(new ObjectParameter("PlanDate_between", txtStar.Text.ToDateTime() + "," + txtEnd.Text.ToInt32()));
            }
            else
            {
                ObjParameterList.Add(new ObjectParameter("PlanDate", DateTime.Now));
            }
            int sourceCount = 0;
            objMissionDetailsedBLL.GetMissionDetailedByWhere(0, 0, out sourceCount, ObjParameterList);
            return sourceCount.ToString();
        }



        /// <summary>
        /// 时间段内完成的任务数量
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetActualMissionCount(object source)
        {

            /// <summary>
            /// 明细操作
            /// </summary>
            MissionDetailed objMissionDetailsedBLL = new MissionDetailed();
            List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
            ObjParameterList.Add(new ObjectParameter("ChecksState", 3));
            ObjParameterList.Add(new ObjectParameter("IsOver", true));
            ObjParameterList.Add(new ObjectParameter("EmployeeID", source.ToString().ToInt32()));

            if (txtStar.Text != string.Empty && txtEnd.Text != string.Empty)
            {
                ObjParameterList.Add(new ObjectParameter("PlanDate_between", txtStar.Text.ToDateTime() + "," + txtEnd.Text.ToInt32()));
            }
            else
            {
                ObjParameterList.Add(new ObjectParameter("PlanDate", DateTime.Now));
            }
            int sourceCount = 0;
            objMissionDetailsedBLL.GetMissionDetailedByWhere(0, 0, out sourceCount, ObjParameterList);
            return sourceCount.ToString();
            // int employeeId = (source + string.Empty).ToInt32();
            // var empList = GetMissionDetailedEmployee(employeeId, GetDateSpan());
            // //实际完成时间小于计划完成时间，就实际完成任务数量
            //int count= empList.Where(C => C.FinishDate <= C.PlanDate).Count();
            //return count + string.Empty;
        }


        /// <summary>
        /// 实际完成任务率
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetFinishMissionRate(object source)
        {
            return "0%";
            //int employeeId = (source + string.Empty).ToInt32();
            ////时间段内任务数量
            //int allMissionCount = GetSingerEmployeeMissionCountByEmployeeId(employeeId).Count();
            ////实际完成任务数量
            //int finishMissionCount = GetMissionDetailedEmployee(employeeId, GetDateSpan())
            //    .Where(C => C.FinishDate <= C.PlanDate).Count();
            //if (allMissionCount == 0 || finishMissionCount == 0)
            //{
            //    return "0 %";
            //}
            //else
            //{
            //    return GetDoubleFormat((double)finishMissionCount / allMissionCount);
            //}

        }

        ///// <summary>
        ///// 根据员工ID，以及时间段返回的数据集合
        ///// </summary>
        ///// <param name="employeeId"></param>
        ///// <returns></returns>
        //private List<FL_MissionDetailedEmployee> GetMissionDetailedEmployee(int employeeId,ObjectParameter parDateTime) 
        //{
        //    List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();
        //    ObjParameterList.Add(parDateTime);
        //    ObjParameterList.Add(new ObjectParameter("EmployeeID", employeeId));
        //    var empList = objMissionDetailedBLL.GetMissionDetailedEmployeeParameter(ObjParameterList.ToArray());
        //    return empList;
        //}


        protected string GetInProgressMissionCount(object source)
        {
            //int employeeId = (source + string.Empty).ToInt32();
            //List<ObjectParameter> ObjParameterList = new List<ObjectParameter>();

            //ObjParameterList.Add(new ObjectParameter("EmployeeID", employeeId));
            //var empList = objMissionDetailedBLL.GetMissionDetailedEmployeeParameter(ObjParameterList.ToArray());
            ////根据任务开始时间大于等于 查询起始时间，同时 计划完成时间小于截止时间
            //return empList.Where(
            //    C => C.StarDate >= GetDateStar() && C.PlanDate <= GetDateEnd()
            //    ).Count()+string.Empty;

            return "0";
        }
        /// <summary>
        /// 待办任务数量
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetToBeDoneMissionCount(object source)
        {
            //int employeeId = (source + string.Empty).ToInt32();
            //int allMissionCount = GetSingerEmployeeMissionCountByEmployeeId(employeeId).Count();
            ////实际完成任务数量
            //int finishMissionCount = GetMissionDetailedEmployee(employeeId, GetDateSpan())
            //    .Where(C => C.FinishDate <= C.PlanDate).Count();
            //return (allMissionCount - finishMissionCount) + stdeping.Empty;
            return "0";
        }


        protected void MissionPager_PageChanged(object sender, EventArgs e)
        {
            DataBinder();
        }


        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_Click(object sender, EventArgs e)
        {
            DataBinder();
        }

        protected void DepartmentDropdownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DdlEmployee1.BinderByDepartment(DepartmentDropdownList1.SelectedValue.ToInt32());
        }



    }

    public class Missions
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int AllMissionSum { get; set; }
        public int FinishSum { get; set; }
        public int OverFinishSum { get; set; }
        public int UnFinishSum { get; set; }
        public int DelaySum { get; set; }
    }
}