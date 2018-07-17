using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.CA;
using HA.PMS.DataAssmblly;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using System.Text;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Company
{
    public partial class SaleTargetCreate : SystemPage
    {
        TargetType objTargetTypeLL = new TargetType();
        MyGoalTarget objMyGoalTargetBLL = new MyGoalTarget();
        Department objDepartmentBLL = new Department();
        Employee objEmployeeBLL = new Employee();
        TargetTypeRateValue objRateValueBLL = new TargetTypeRateValue();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                List<CA_TargetType> targe = GetTargetByUserId();

                List<CA_MyGoalTarget> MyGoalTarget = new List<CA_MyGoalTarget>();
                targe.ForEach(C => MyGoalTarget.Add(new CA_MyGoalTarget() { TargetTypeId = C.TargetTypeId }));
                rptTarget.DataSource = MyGoalTarget;
                rptTarget.DataBind();

                //部门数量平均率　
                ViewState["managerMent"] = 0;
                //部门质量平均率
                ViewState["managerMentRate"] = 0;
                //个人目标平均率
                ViewState["myGoal"] = 0;
                if (rptManagerMent.Visible)
                {
                    DataDepartment();
                    rptDepartTarget.DataSource = MyGoalTarget;
                    rptDepartTarget.DataBind();


                    //下拉框
                    DataDropDownList();

                    DataDepartmentRate();
                }
            }
        }


        /// <summary>
        /// 加载部门质量指标率
        /// </summary>
        protected void DataDepartmentRate()
        {
            for (int i = 0; i < rptDepartTarget.Items.Count; i++)
            {
                HiddenField hfValueRate = rptDepartTarget.Items[i].FindControl("hfValueRate") as HiddenField;
                PlaceHolder phTargetRate = rptDepartTarget.Items[i].FindControl("phTargetRate") as PlaceHolder;
                if (hfValueRate != null)
                {
                    int TargetTypeId = hfValueRate.Value.ToInt32();
                    //这里是关于 当前这个指标率的所有质量指标值
                    var query = objRateValueBLL.GetByAll().Where(C => C.TargetTypeId == TargetTypeId &&
                          C.CreateTime.Value.Year == DateTime.Now.Year).OrderBy(C => C.CreateTime).ToList();
               
                        for (int j = 0; j < query.Count(); j++)
                        {
                            int star = 1, end = 0;
                            if (j != 0)
                            {
                                star = query[j - 1].CreateTime.Value.Month;
                            }
                            if (query[query.Count - 1].CreateTime.Value.Month < 12)
                            {
                                end = 12;
                            }
                            else
                            {
                                end = query[j].CreateTime.Value.Month;
                            }
                            for (; star <= end; star++)
                            {
                              
                                TextBox txt = rptDepartTarget.Items[i]
                                    .FindControl("txtMonth" + star) as TextBox;
                                txt.Text = query[j].RateValue.Value+string.Empty;
                            }
                       


                        
                    }
                }
            }

        }
        private List<CA_TargetType> GetTargetByUserId()
        {
            int employeeId = User.Identity.Name.ToInt32();

            Sys_Employee loginEmployee = objEmployeeBLL.GetByID(employeeId);
            Sys_Department depart = objDepartmentBLL.GetByAll()
                .Where(C => C.DepartmentManager == employeeId).FirstOrDefault();
            List<CA_TargetType> allTargetList = objTargetTypeLL.GetByAll();
            List<CA_TargetType> targe = new List<CA_TargetType>();
            //如果不为空，就代表当前员工是该部门的主管
            if (depart != null)
            {
                targe = allTargetList.Where(C => C.DepartmentId
                == loginEmployee.DepartmentID).ToList();
                phManager.Visible = true;
            }
            else
            {
                targe = allTargetList.Where(C => C.DepartmentId
                == loginEmployee.DepartmentID && C.TargetType == 0).ToList();
            }

            //当前登陆人的目标类型

            return targe;

        }
        /// <summary>
        /// 工作 名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetJobNameByJobID(object source)
        {
            int jobId = (source + string.Empty).ToInt32();
            EmployeeJobI objJobBLL = new EmployeeJobI();
            Sys_EmployeeJob job = objJobBLL.GetByID(jobId);
            if (job != null)
            {
                return job.Jobname;
            }
            return "";
        }
        /// <summary>
        /// 返回指标名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetGoalByDepartId(object source)
        {
            int departId = (source + string.Empty).ToInt32();
            CA_TargetType target = objTargetTypeLL.GetByAll().Where(C => C.DepartmentId == departId).FirstOrDefault();
            if (target != null)
            {
                return target.Goal;
            }
            return "";
        }
        /// <summary>
        /// 返回部门名称
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetDepartmentNameByID(object source)
        {
            int departId = (source + string.Empty).ToInt32();
            Sys_Department depart = objDepartmentBLL.GetByID(departId);
            if (depart != null)
            {
                return depart.DepartmentName;
            }
            return "";

        }

        /// <summary>
        /// 绑定员工目标管理数据
        /// </summary>
        protected void DataDepartment()
        {

            int userID = User.Identity.Name.ToInt32();
            Sys_Employee emp = objEmployeeBLL.GetByID(userID);
            rptManagerMent.DataSource = objEmployeeBLL.GetByALLDepartmetnID(emp.DepartmentID);
            rptManagerMent.DataBind();

        }

        /// <summary>
        /// 添加我的目标管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isOk = false;
            int employeeId = User.Identity.Name.ToInt32();
            int TargetTypeId = 0;
            for (int i = 0; i < rptTarget.Items.Count; i++)
            {
                PlaceHolder phTarget = rptTarget.Items[i].FindControl("phTarget") as PlaceHolder;

                HiddenField hfValue = phTarget.FindControl("hfValue") as HiddenField;

                TargetTypeId = hfValue.Value.ToInt32();

                foreach (var item in phTarget.Controls)
                {
                    if (item is TextBox)
                    {
                        TextBox txtMonth = item as TextBox;

                        if (!string.IsNullOrEmpty(txtMonth.Text))
                        {
                            DateTime newTime = string.Format("{0}-{1}-01", DateTime.Now.Year, txtMonth.ToolTip).ToDateTime();
                            CA_MyGoalTarget myTarget = objMyGoalTargetBLL.GetByAll().Where(C => C.CreateTime == newTime
                                && C.CreateEmployeeId == employeeId && C.TargetTypeId == TargetTypeId).ToList().FirstOrDefault();
                            //不等于空为修改功能
                            if (myTarget != null)
                            {
                                myTarget.TargetValue = txtMonth.Text.ToDecimal();
                                objMyGoalTargetBLL.Update(myTarget);
                            }
                            else
                            {
                                //添加功能
                                myTarget = new CA_MyGoalTarget();
                                //2013-05-05
                                myTarget.CreateTime = newTime;
                                myTarget.CreateEmployeeId = employeeId;
                                myTarget.TargetTypeId = TargetTypeId;
                                myTarget.TargetValue = txtMonth.Text.ToDecimal();
                                objMyGoalTargetBLL.Insert(myTarget);
                            }

                            isOk = true;
                        }
                    }
                }
            }
            if (isOk)
            {
                JavaScriptTools.RegisterJsCodeSource("alert('操作成功');window.location.href=window.location.href;", this.Page);

            }


        }


        /// <summary>
        /// 我的目标管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptTarget_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CA_MyGoalTarget MyGoalTarget = e.Item.DataItem as CA_MyGoalTarget;
            PlaceHolder phTarget = e.Item.FindControl("phTarget") as PlaceHolder;
            HiddenField hfValue = phTarget.FindControl("hfValue") as HiddenField;
            int userID = User.Identity.Name.ToInt32();
            Label lblMonth = phTarget.FindControl("lblMonth") as Label;
            CA_TargetType targ = objTargetTypeLL.GetByID(MyGoalTarget.TargetTypeId);
            lblMonth.Text = targ.Goal;
            hfValue.Value = MyGoalTarget.TargetTypeId + string.Empty;

            Literal ltlYearCount = phTarget.FindControl("ltlYearCount") as Literal;
            var currentTarget = objMyGoalTargetBLL.GetByAll().Where(C => C.TargetTypeId.Value == MyGoalTarget.TargetTypeId);
            for (int i = 1; i <= 12; i++)
            {
                CA_MyGoalTarget currentMonth = currentTarget.Where(C => C.CreateTime.Value.Month == i
                      && C.CreateTime.Value.Year == DateTime.Now.Year && C.CreateEmployeeId == userID).FirstOrDefault();

                TextBox item = phTarget.FindControl("txtMonth" + i) as TextBox;

                if (currentMonth != null)
                {

                    item.Text = currentMonth.TargetValue + string.Empty;


                    int targetType = objTargetTypeLL.GetByID(MyGoalTarget.TargetTypeId).TargetType.Value;
                    if (targetType == 0)
                    {
                        ltlYearCount.Text = (ltlYearCount.Text.ToDecimal() + item.Text.ToDecimal()) + string.Empty;
                    }
                    else
                    {

                        //个人平均率
                        ltlYearCount.Text = (Math.Round(Convert.ToDecimal(ViewState["myGoal"]), 2)
                            + Math.Round(item.Text.ToDecimal() / 12, 2)) + string.Empty;
                        ViewState["myGoal"] = ltlYearCount.Text;
                    }


                    item.Enabled = false;
                    item.CssClass = currentMonth.GoalId + string.Empty;
                }
                else
                {

                    item.Text = "";
                    item.Enabled = true;

                }
                //不管是当前操作人是主管还是员工也好，质量指标是不能进行编辑的
                if (targ.TargetType.Value == 1)
                {
                    item.Enabled = false;
                }

            }


        }
        /// <summary>
        /// 员工目标管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptManagerMent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Sys_Employee sys = e.Item.DataItem as Sys_Employee;
            int employeeId = User.Identity.Name.ToInt32();
            var currentTarget = objMyGoalTargetBLL.GetByAll().Where(C => C.CreateEmployeeId == sys.EmployeeID);
            PlaceHolder phTarget = e.Item.FindControl("phTarget") as PlaceHolder;
            HiddenField hfValue = phTarget.FindControl("hfValue") as HiddenField;
            Literal ltlYearCount = phTarget.FindControl("ltlYearCount") as Literal;
            //存入TargetTypeId
            int TargetTypeId = 0;
             var query= objTargetTypeLL.GetByAll().Where(C => C.CreateEmployeeId == employeeId)
                .FirstOrDefault();
             if (query!=null)
             {
                 TargetTypeId = query.TargetTypeId;
             }
            hfValue.Value = TargetTypeId + string.Empty;
            for (int i = 1; i <= 12; i++)
            {

                CA_MyGoalTarget currentMonth = currentTarget.Where(C => C.CreateTime.Value.Month == i
                      && C.CreateTime.Value.Year == DateTime.Now.Year && C.TargetTypeId == TargetTypeId).FirstOrDefault();

                TextBox item = phTarget.FindControl("txtMonth" + i) as TextBox;

                if (currentMonth != null)
                {

                    item.Text = currentMonth.TargetValue + string.Empty;
                    ltlYearCount.Text = (ltlYearCount.Text.ToDecimal() + currentMonth.TargetValue) + string.Empty;
                    item.CssClass = currentMonth.GoalId + string.Empty;
                }
                else
                {
                    item.Text = "";

                }
            }



        }
        /// <summary>
        /// 员工目标管理
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptManagerMent_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int employeeId = (e.CommandArgument + string.Empty).ToInt32();
            //是否是修改标志
            bool isUpdate = true;
            //在目标表中找出所有关于employeeId的数据
            var currentTarget = objMyGoalTargetBLL.GetByAll().Where(C => C.CreateEmployeeId == employeeId);
            PlaceHolder phTarget = e.Item.FindControl("phTarget") as PlaceHolder;
            StringBuilder sbErrorPrompt = new StringBuilder();
            for (int i = 1; i <= 12; i++)
            {
                CA_MyGoalTarget currentMonth = currentTarget.Where(C => C.CreateTime.Value.Month == i
                     && C.CreateTime.Value.Year == DateTime.Now.Year).FirstOrDefault();

                TextBox item = phTarget.FindControl("txtMonth" + i) as TextBox;
                //不等于空的话，就代表该if是修改操作
                if (currentMonth != null)
                {
                    if (!string.IsNullOrEmpty(item.Text))
                    {
                        currentMonth.TargetValue = item.Text.ToDecimal();
                        objMyGoalTargetBLL.Update(currentMonth);
                    }
                    else
                    {
                        item.Text = currentMonth.TargetValue + string.Empty;
                        //如果已经在数据库中存在的值是不能进行为空的操作。
                        sbErrorPrompt.AppendFormat("{0}月份  ", i);
                    }

                }
                else
                {
                    HiddenField hfValue = phTarget.FindControl("hfValue") as HiddenField;

                    //添加操作
                    if (!string.IsNullOrEmpty(item.Text))
                    {
                        CA_MyGoalTarget myTarget = new CA_MyGoalTarget();
                        //2013-05-05
                        myTarget.CreateTime = string.Format("{0}-{1}-01", DateTime.Now.Year, item.ToolTip).ToDateTime();
                        myTarget.CreateEmployeeId = employeeId;
                        myTarget.TargetTypeId = hfValue.Value.ToInt32();
                        myTarget.TargetValue = item.Text.ToDecimal();
                        objMyGoalTargetBLL.Insert(myTarget);
                        isUpdate = false;
                    }

                }
            }
            if (isUpdate)
            {
                if (sbErrorPrompt.ToString().Length > 0)
                {
                    JavaScriptTools.AlertWindow(sbErrorPrompt.ToString() + "的目标计划已经存在，因此不能进行为空的修改", this.Page);
                }
                else
                {
                    JavaScriptTools.RegisterJsCodeSource("alert('操作成功');window.location.href=window.location.href;", this.Page);
                }
            }
            else
            {
                JavaScriptTools.RegisterJsCodeSource("alert('操作成功');window.location.href=window.location.href;", this.Page);
            }
        }
        /// <summary>
        /// 部门目标管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptDepartTarget_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            CA_MyGoalTarget MyGoalTarget = e.Item.DataItem as CA_MyGoalTarget;
            PlaceHolder phTarget = e.Item.FindControl("phTarget") as PlaceHolder;
            HiddenField hfValue = phTarget.FindControl("hfValue") as HiddenField;

            Label lblMonth = phTarget.FindControl("lblMonth") as Label;
            PlaceHolder phTargetRate = e.Item.FindControl("phTargetRate") as PlaceHolder;

            HiddenField hfValueRate = phTargetRate.FindControl("hfValueRate") as HiddenField;

            Label lblMonthRate = phTargetRate.FindControl("lblMonthRate") as Label;


            CA_TargetType targ = objTargetTypeLL.GetByID(MyGoalTarget.TargetTypeId);
            lblMonth.Text = targ.Goal;
            lblMonthRate.Text = targ.Goal;
            hfValue.Value = MyGoalTarget.TargetTypeId + string.Empty;
            hfValueRate.Value = hfValue.Value;
            Literal ltlYearCount = e.Item.FindControl("ltlYearCount") as Literal;
            Literal ltlYearCountRate = e.Item.FindControl("ltlYearCountRate") as Literal;

            List<CA_MyGoalTarget> currentTarget = objMyGoalTargetBLL.GetByAll().Where(C => C.TargetTypeId.Value == MyGoalTarget.TargetTypeId).ToList();
            for (int i = 1; i <= 12; i++)
            {
                List<CA_MyGoalTarget> currentMonth = currentTarget.Where(C => C.CreateTime.Value.Month == i
                       && C.CreateTime.Value.Year == DateTime.Now.Year && C.TargetTypeId == MyGoalTarget.TargetTypeId).ToList();
                Label item = phTarget.FindControl("lblMonth" + i) as Label;
                TextBox itemText = phTarget.FindControl("txtMonth" + i) as TextBox;




                if (currentMonth != null)
                {



                    //数量
                    if (targ.TargetType == 0)
                    {
                        item.Text = currentMonth.Sum(C => C.TargetValue.Value) + string.Empty;
                    }
                    else
                    {

                        //
                        //质量
                        //itemText.Text = currentMonth.Sum(C => C.TargetValue.Value) == 0 ? "" 
                        //    : currentMonth.Sum(C => C.TargetValue.Value)+string.Empty;
                        itemText.Enabled = false;


                        //itemText.Text
                    }



                    int targetType = objTargetTypeLL.GetByID(MyGoalTarget.TargetTypeId).TargetType.Value;
                    if (targetType == 0)
                    {
                        ltlYearCount.Text = (ltlYearCount.Text.ToDecimal() + item.Text.ToDecimal()) + string.Empty;
                    }
                    else
                    {     //部门平均率
                        //数量
                        if (targ.TargetType == 0)
                        {
                            ltlYearCount.Text = Math.Round((Convert.ToDecimal(ViewState["managerMent"])
                            + item.Text.ToDecimal() / 12), 2) + string.Empty;
                            ViewState["managerMent"] = ltlYearCount.Text;
                        }
                        else
                        {
                            ltlYearCountRate.Text = Math.Round((Convert.ToDecimal(ViewState["managerMentRate"])
                            + itemText.Text.ToDecimal() / 12), 2) + string.Empty;
                            ViewState["managerMentRate"] = ltlYearCountRate.Text;
                        }

                    }

                }


            }


            //如果是等于 0话，就代表此时的为数量指标不能进行显示的
            if (targ.TargetType == 0)
            {
                phTargetRate.Visible = false;
                phTarget.Visible = true;
            }
            else
            {
                phTargetRate.Visible = true;
                phTarget.Visible = false;
            }

        }
        /// <summary>
        /// 保存部门目标质量管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveRate_Click(object sender, EventArgs e)
        {
            bool isOk = false;
            int employeeId = User.Identity.Name.ToInt32();
            int TargetTypeId = 0;
            for (int i = 0; i < rptDepartTarget.Items.Count; i++)
            {
                PlaceHolder phTargetRate = rptDepartTarget.Items[i].FindControl("phTargetRate") as PlaceHolder;

                HiddenField hfValueRate = phTargetRate.FindControl("hfValueRate") as HiddenField;
                TargetTypeId = hfValueRate.Value.ToInt32();
                foreach (var item in phTargetRate.Controls)
                {
                    if (item is TextBox)
                    {
                        TextBox txtMonth = item as TextBox;

                        if (!string.IsNullOrEmpty(txtMonth.Text))
                        {
                            DateTime newTime = string.Format("{0}-{1}-01", DateTime.Now.Year, txtMonth.ToolTip).ToDateTime();
                            CA_MyGoalTarget myTarget = objMyGoalTargetBLL.GetByAll().Where(C => C.CreateTime == newTime
                                && C.CreateEmployeeId == employeeId && C.TargetTypeId == TargetTypeId).ToList().FirstOrDefault();
                            //不等于空为修改功能
                            if (myTarget != null)
                            {
                                myTarget.TargetValue = txtMonth.Text.ToDecimal();
                                objMyGoalTargetBLL.Update(myTarget);
                            }
                            else
                            {
                                //添加功能
                                myTarget = new CA_MyGoalTarget();
                                //2013-05-05
                                myTarget.CreateTime = newTime;
                                myTarget.CreateEmployeeId = employeeId;
                                myTarget.TargetTypeId = TargetTypeId;
                                myTarget.TargetValue = txtMonth.Text.ToDecimal();
                                objMyGoalTargetBLL.Insert(myTarget);
                            }

                            isOk = true;
                        }
                    }
                }


            }

            if (isOk)
            {
                JavaScriptTools.RegisterJsCodeSource("alert('操作成功');window.location.href=window.location.href;", this.Page);

            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            //if (ddlDepartment.SelectedItem.Text!="请选择")
            //{
            //    rptManagerMent.DataSource = objEmployeeBLL.GetByALLDepartmetnID(ddlDepartment.SelectedValue.ToInt32());
            //    rptManagerMent.DataBind();
            //}

        }

        //下拉框数据绑定
        protected void DataDropDownList()
        {
            int userID = User.Identity.Name.ToInt32();
            Sys_Employee emp = objEmployeeBLL.GetByID(userID);
            var query = objDepartmentBLL.GetbySublevelByDepartmetnID(emp.DepartmentID);
            ddlDepartment.DataSource = query;
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentID";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Add(new ListItem("请选择", "0"));
            ddlDepartment.SelectedIndex = ddlDepartment.Items.Count - 1;
        }

        protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

            var query = objEmployeeBLL.GetByALLDepartmetnID(ddlDepartment.SelectedValue.ToInt32());
            ddlEmployee.DataSource = query;
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeID";
            ddlEmployee.DataBind();
            ddlEmployee.Items.Add(new ListItem("请选择", "0"));
            ddlEmployee.SelectedIndex = ddlEmployee.Items.Count - 1;
        }

    }
}