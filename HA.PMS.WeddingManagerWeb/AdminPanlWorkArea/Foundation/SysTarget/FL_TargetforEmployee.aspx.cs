using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.SysTarget;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.SysTarget
{
    public partial class FL_TargetforEmployee : SystemPage
    {
        Employee ObjEmployeeBLL = new Employee();
        Target ObjTargetBLL = new Target();
        FinishTargetSum ObjFinishTargetSumBLL = new FinishTargetSum();
        Department ObjDepartmentBLL = new Department();
        //EmployeeTarget ObjEmployeeTargetBLL = new EmployeeTarget();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                this.hideIsManager.Value = ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()).ToString();
                hideCreateEmployee.Value = User.Identity.Name;
                BinderData();

                if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
                {
                    BinderDepartmentSum();
                }
                else
                {
                    repList.Visible = false;
                    repMine.Visible = false;
                }


            }
        }


        private void BinderDepartmentSum()
        {
            var Model = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32());
            //var ObjDepartMentList = ObjDepartmentBLL.GetMyManagerDepartment(User.Identity.Name.ToInt32());
            var ObjDepartMentList = ObjDepartmentBLL.GetbyChildenByDepartmetnID(Model.DepartmentID);

            string KeyList = string.Empty;

            foreach (var Objdepartment in ObjDepartMentList)
            {
                KeyList += Objdepartment.DepartmentID + ",";

            }
            KeyList = KeyList.Trim(',');



            List<int> EmployeeKey = new List<int>();


            EmployeeKey.Add(User.Identity.Name.ToInt32());

            //EmployeeKey.Add(User.Identity.Name.ToInt32());
            var ObjNeed = ObjFinishTargetSumBLL.GetinEmployeeKeyListNeedCreate(EmployeeKey, 1);
            //if (ObjNeed.Count > 0)
            //{
            //    this.repList.DataSource = ObjFinishTargetSumBLL.GetDepartmentTarget(KeyList, " and IsActive=1 and TargetTitle='" + ObjNeed[0].TargetTitle + "'");
            //    this.repList.DataBind();
            //}

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public string GetEmployeeNameByID(object EmployeeID)
        {
            string EmployeeName = GetEmployeeName(EmployeeID);
            if (EmployeeName != null)
            {
                return EmployeeName;
            }
            else
            {
                return GetEmployeeName(User.Identity.Name.ToInt32());
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {

            List<int> EmployeeKey = new List<int>();


            //var ObjEmployeeList = ObjEmployeeBLL.GetMyManagerEmpLoyee(User.Identity.Name.ToInt32(), true);
            var ObjEmployeeList = ObjEmployeeBLL.GetMyManagerEmpLoyee(User.Identity.Name.ToInt32());

            foreach (var ObjItem in ObjEmployeeList)
            {
                EmployeeKey.Add(ObjItem.EmployeeID);
            }
            EmployeeKey.Add(User.Identity.Name.ToInt32());
            var BinderSource = ObjFinishTargetSumBLL.GetinEmployeeKeyListNeedCreate(EmployeeKey, 1);
            //if (ddlMyManagerEmployee1.SelectedItem != null)
            //{
            //    this.repEmployeeTargetList.DataSource = BinderSource.Where(C => C.EmployeeID == ddlMyManagerEmployee1.SelectedValue.ToInt32());
            //    this.repEmployeeTargetList.DataBind();
            //    return;
            //}
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
            {
                this.repEmployeeTargetList.DataSource = BinderSource.Where(C => C.EmployeeID != User.Identity.Name.ToInt32());
                this.repEmployeeTargetList.DataBind();
            }
            else
            {
                this.repEmployeeTargetList.DataSource = BinderSource;
                this.repEmployeeTargetList.DataBind();
            }



            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
            {
                EmployeeKey = new List<int>();
                EmployeeKey.Add(User.Identity.Name.ToInt32());
                BinderSource = ObjFinishTargetSumBLL.GetinEmployeeKeyListNeedCreate(EmployeeKey, 1);
                this.repMine.DataSource = BinderSource;
                this.repMine.DataBind();

            }
            else
            {
                repMine.Visible = false;
            }
            //if (EmployeeKey.Count > 0)
            //{

            //    this.repEmployeeTargetList.DataSource = ObjEmployeeTargetBLL.GetEmployeeTarget(EmployeeKey);
            //    this.repEmployeeTargetList.DataBind();
            //}

        }



        /// <summary>
        /// 保存所有数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

            if (!ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
            {
                for (int i = 0; i < repEmployeeTargetList.Items.Count; i++)
                {

                    HiddenField ObjhideKey = (HiddenField)repEmployeeTargetList.Items[i].FindControl("hideKey");
                    HiddenField ObjTargetKey = (HiddenField)repEmployeeTargetList.Items[i].FindControl("TargetKey");
                    if (ObjhideKey.Value.ToInt32() > 0)
                    {
                        var ObjUpdateModel = ObjFinishTargetSumBLL.GetByID(ObjhideKey.Value.ToInt32());
                        ObjUpdateModel.LastYearFinishSum = 0;
                        ObjUpdateModel.LastYearCompletionrate = 0;
                        ObjUpdateModel.FinishSum = 0;
                        ObjUpdateModel.Completionrate = 0;
                        ObjUpdateModel.CreateEmployeeID = User.Identity.Name.ToInt32();
                        //ObjUpdateModel.EmployeeID = User.Identity.Name.ToInt32();
                        ObjUpdateModel.DepartmentID = ObjEmployeeBLL.GetByID(ObjUpdateModel.EmployeeID).DepartmentID;
                        ObjUpdateModel.OverYearFinishSum = 0;
                        ObjUpdateModel.OveryearRate = 0;
                        ObjUpdateModel.PlanSum = 0;
                        ObjUpdateModel.Year = DateTime.Now.Year;
                        ObjUpdateModel.UpdateTime = DateTime.Now;
                        ObjUpdateModel.Unite = "个";
                        ObjUpdateModel.TargetID = ObjTargetKey.Value.ToInt32();
                        ObjUpdateModel.TargetTitle = ObjTargetBLL.GetByID(ObjUpdateModel.TargetID).TargetTitle;
                        ObjUpdateModel.MonthPlan1 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth1")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan2 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth2")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan3 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth3")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan4 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth4")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan5 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth5")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan6 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth6")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan7 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth7")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan8 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth8")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan9 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth9")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan10 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth10")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan11 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth11")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan12 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth12")).Text.ToDecimal();


                        ObjFinishTargetSumBLL.Update(ObjUpdateModel);

                    }
                    else
                    {

                        var ObjUpdateModel = new FL_FinishTargetSum();
                        ObjUpdateModel.LastYearFinishSum = 0;
                        ObjUpdateModel.LastYearCompletionrate = 0;
                        ObjUpdateModel.FinishSum = 0;
                        ObjUpdateModel.Completionrate = 0;
                        ObjUpdateModel.CreateEmployeeID = User.Identity.Name.ToInt32();
                        ObjUpdateModel.EmployeeID = User.Identity.Name.ToInt32();
                        ObjUpdateModel.DepartmentID = ObjEmployeeBLL.GetByID(ObjUpdateModel.EmployeeID).DepartmentID;
                        ObjUpdateModel.OverYearFinishSum = 0;
                        ObjUpdateModel.OveryearRate = 0;
                        ObjUpdateModel.PlanSum = 0;
                        ObjUpdateModel.Year = DateTime.Now.Year;
                        ObjUpdateModel.UpdateTime = DateTime.Now;
                        ObjUpdateModel.Unite = "个";
                        ObjUpdateModel.TargetID = ObjTargetKey.Value.ToInt32();
                        ObjUpdateModel.TargetTitle = ObjTargetBLL.GetByID(ObjUpdateModel.TargetID).TargetTitle;
                        ObjUpdateModel.MonthPlan1 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth1")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan2 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth2")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan3 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth3")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan4 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth4")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan5 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth5")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan6 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth6")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan7 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth7")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan8 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth8")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan9 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth9")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan10 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth10")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan11 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth11")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan12 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth12")).Text.ToDecimal();

                        ObjUpdateModel.MonthFinsh1 = 0;
                        ObjUpdateModel.MonthFinish2 = 0;
                        ObjUpdateModel.MonthFinish3 = 0;
                        ObjUpdateModel.MonthFinish4 = 0;
                        ObjUpdateModel.MonthFinish5 = 0;
                        ObjUpdateModel.MonthFinish6 = 0;
                        ObjUpdateModel.MonthFinish7 = 0;
                        ObjUpdateModel.MonthFinish8 = 0;
                        ObjUpdateModel.MonthFinish9 = 0;
                        ObjUpdateModel.MonthFinish10 = 0;
                        ObjUpdateModel.MonthFinish11 = 0;
                        ObjUpdateModel.MonthFinish12 = 0;
                        ObjUpdateModel.IsActive = false;

                        ObjFinishTargetSumBLL.Insert(ObjUpdateModel);
                    }
                }
            }
            else
            {
                for (int i = 0; i < repEmployeeTargetList.Items.Count; i++)
                {
                    HiddenField ObjhideKey = (HiddenField)repEmployeeTargetList.Items[i].FindControl("hideKey");
                    HiddenField ObjTargetKey = (HiddenField)repEmployeeTargetList.Items[i].FindControl("TargetKey");
                    HiddenField ObjEmployeeKey = (HiddenField)repEmployeeTargetList.Items[i].FindControl("EmployeeKey");
                    if (ObjhideKey.Value.ToInt32() > 0)
                    {
                        var ObjUpdateModel = ObjFinishTargetSumBLL.GetByID(ObjhideKey.Value.ToInt32());
                        ObjUpdateModel.LastYearFinishSum = 0;
                        ObjUpdateModel.LastYearCompletionrate = 0;
                        ObjUpdateModel.FinishSum = 0;
                        ObjUpdateModel.Completionrate = 0;
                        ObjUpdateModel.CreateEmployeeID = User.Identity.Name.ToInt32();
                        ObjUpdateModel.EmployeeID = ObjEmployeeKey.Value.ToInt32();
                        ObjUpdateModel.DepartmentID = ObjEmployeeBLL.GetByID(ObjUpdateModel.EmployeeID).DepartmentID;
                        ObjUpdateModel.OverYearFinishSum = 0;
                        ObjUpdateModel.OveryearRate = 0;
                        ObjUpdateModel.PlanSum = 0;
                        ObjUpdateModel.Year = DateTime.Now.Year;
                        ObjUpdateModel.UpdateTime = DateTime.Now;
                        ObjUpdateModel.Unite = "个";
                        ObjUpdateModel.TargetID = ObjTargetKey.Value.ToInt32();
                        ObjUpdateModel.TargetTitle = ObjTargetBLL.GetByID(ObjUpdateModel.TargetID).TargetTitle;
                        ObjUpdateModel.MonthPlan1 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth1")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan2 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth2")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan3 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth3")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan4 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth4")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan5 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth5")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan6 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth6")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan7 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth7")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan8 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth8")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan9 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth9")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan10 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth10")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan11 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth11")).Text.ToDecimal();
                        ObjUpdateModel.MonthPlan12 = ((TextBox)repEmployeeTargetList.Items[i].FindControl("txtMonth12")).Text.ToDecimal();


                        ObjFinishTargetSumBLL.Update(ObjUpdateModel);

                    }
                }
            }
            Response.Redirect(Request.Url.ToString());
        }


        /// <summary>
        /// 初始化创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFiestCreate_Click(object sender, EventArgs e)
        {
            //var ObjEmployeeList = ObjEmployeeBLL.GetMyManagerEmpLoyee(User.Identity.Name.ToInt32());
            //var ObjTargetList= ObjTargetBLL.GetByType(2);
            //foreach (var Objitem in ObjEmployeeList)
            //{
            //    foreach (var Objtarget in ObjTargetList)
            //    {
            //        DataAssmblly.FL_EmployeeTarget ObjEmployeeTargetModel = new DataAssmblly.FL_EmployeeTarget();
            //        ObjEmployeeTargetModel.EmployeeID = Objitem.EmployeeID;
            //        ObjEmployeeTargetModel.CreateEmployeeID = User.Identity.Name.ToInt32();
            //        ObjEmployeeTargetModel.Year = DateTime.Now.Year;
            //        ObjEmployeeTargetModel.UpdateTime = DateTime.Now;
            //        ObjEmployeeTargetModel.Month1 = 0;
            //        ObjEmployeeTargetModel.Month2 = 0;
            //        ObjEmployeeTargetModel.Month3 = 0;
            //        ObjEmployeeTargetModel.Month4 = 0;
            //        ObjEmployeeTargetModel.Month5 = 0;
            //        ObjEmployeeTargetModel.Month6 = 0;
            //        ObjEmployeeTargetModel.Month7 = 0;
            //        ObjEmployeeTargetModel.Month8 = 0;
            //        ObjEmployeeTargetModel.Month9 = 0;
            //        ObjEmployeeTargetModel.Month10 = 0;
            //        ObjEmployeeTargetModel.Month11 = 0;
            //        ObjEmployeeTargetModel.Month12 = 0;
            //        ObjEmployeeTargetModel.Unite = "个";


            //        ObjEmployeeTargetBLL.Insert(ObjEmployeeTargetModel);
            //    }
            //}
        }

        protected void ddlTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            BinderData();
        }

        protected void btnSaveMine_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < repMine.Items.Count; i++)
            {

                HiddenField ObjhideKey = (HiddenField)repMine.Items[i].FindControl("hideKey");
                HiddenField ObjTargetKey = (HiddenField)repMine.Items[i].FindControl("TargetKey");
                if (ObjhideKey.Value.ToInt32() > 0)
                {
                    var ObjUpdateModel = ObjFinishTargetSumBLL.GetByID(ObjhideKey.Value.ToInt32());
                    ObjUpdateModel.LastYearFinishSum = 0;
                    ObjUpdateModel.LastYearCompletionrate = 0;
                    ObjUpdateModel.FinishSum = 0;
                    ObjUpdateModel.Completionrate = 0;
                    //ObjUpdateModel.CreateEmployeeID = User.Identity.Name.ToInt32();
                    //ObjUpdateModel.EmployeeID = User.Identity.Name.ToInt32();
                    ObjUpdateModel.DepartmentID = ObjEmployeeBLL.GetByID(ObjUpdateModel.EmployeeID).DepartmentID;
                    ObjUpdateModel.OverYearFinishSum = 0;
                    ObjUpdateModel.OveryearRate = 0;
                    ObjUpdateModel.PlanSum = 0;
                    ObjUpdateModel.Year = DateTime.Now.Year;
                    ObjUpdateModel.UpdateTime = DateTime.Now;
                    ObjUpdateModel.Unite = "个";
                    ObjUpdateModel.TargetID = ObjTargetKey.Value.ToInt32();
                    ObjUpdateModel.TargetTitle = ObjTargetBLL.GetByID(ObjUpdateModel.TargetID).TargetTitle;
                    ObjUpdateModel.MonthPlan1 = ((TextBox)repMine.Items[i].FindControl("txtMonth1")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan2 = ((TextBox)repMine.Items[i].FindControl("txtMonth2")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan3 = ((TextBox)repMine.Items[i].FindControl("txtMonth3")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan4 = ((TextBox)repMine.Items[i].FindControl("txtMonth4")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan5 = ((TextBox)repMine.Items[i].FindControl("txtMonth5")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan6 = ((TextBox)repMine.Items[i].FindControl("txtMonth6")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan7 = ((TextBox)repMine.Items[i].FindControl("txtMonth7")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan8 = ((TextBox)repMine.Items[i].FindControl("txtMonth8")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan9 = ((TextBox)repMine.Items[i].FindControl("txtMonth9")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan10 = ((TextBox)repMine.Items[i].FindControl("txtMonth10")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan11 = ((TextBox)repMine.Items[i].FindControl("txtMonth11")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan12 = ((TextBox)repMine.Items[i].FindControl("txtMonth12")).Text.ToDecimal();


                    ObjFinishTargetSumBLL.Update(ObjUpdateModel);

                }
                else
                {

                    var ObjUpdateModel = new FL_FinishTargetSum();
                    ObjUpdateModel.LastYearFinishSum = 0;
                    ObjUpdateModel.LastYearCompletionrate = 0;
                    ObjUpdateModel.FinishSum = 0;
                    ObjUpdateModel.Completionrate = 0;
                    ObjUpdateModel.CreateEmployeeID = User.Identity.Name.ToInt32();
                    ObjUpdateModel.EmployeeID = User.Identity.Name.ToInt32();
                    ObjUpdateModel.DepartmentID = ObjEmployeeBLL.GetByID(ObjUpdateModel.EmployeeID).DepartmentID;
                    ObjUpdateModel.OverYearFinishSum = 0;
                    ObjUpdateModel.OveryearRate = 0;
                    ObjUpdateModel.PlanSum = 0;
                    ObjUpdateModel.Year = DateTime.Now.Year;
                    ObjUpdateModel.UpdateTime = DateTime.Now;
                    ObjUpdateModel.Unite = "个";
                    ObjUpdateModel.TargetID = ObjTargetKey.Value.ToInt32();
                    ObjUpdateModel.TargetTitle = ObjTargetBLL.GetByID(ObjUpdateModel.TargetID).TargetTitle;
                    ObjUpdateModel.MonthPlan1 = ((TextBox)repMine.Items[i].FindControl("txtMonth1")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan2 = ((TextBox)repMine.Items[i].FindControl("txtMonth2")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan3 = ((TextBox)repMine.Items[i].FindControl("txtMonth3")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan4 = ((TextBox)repMine.Items[i].FindControl("txtMonth4")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan5 = ((TextBox)repMine.Items[i].FindControl("txtMonth5")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan6 = ((TextBox)repMine.Items[i].FindControl("txtMonth6")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan7 = ((TextBox)repMine.Items[i].FindControl("txtMonth7")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan8 = ((TextBox)repMine.Items[i].FindControl("txtMonth8")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan9 = ((TextBox)repMine.Items[i].FindControl("txtMonth9")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan10 = ((TextBox)repMine.Items[i].FindControl("txtMonth10")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan11 = ((TextBox)repMine.Items[i].FindControl("txtMonth11")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan12 = ((TextBox)repMine.Items[i].FindControl("txtMonth12")).Text.ToDecimal();

                    ObjUpdateModel.MonthFinsh1 = 0;
                    ObjUpdateModel.MonthFinish2 = 0;
                    ObjUpdateModel.MonthFinish3 = 0;
                    ObjUpdateModel.MonthFinish4 = 0;
                    ObjUpdateModel.MonthFinish5 = 0;
                    ObjUpdateModel.MonthFinish6 = 0;
                    ObjUpdateModel.MonthFinish7 = 0;
                    ObjUpdateModel.MonthFinish8 = 0;
                    ObjUpdateModel.MonthFinish9 = 0;
                    ObjUpdateModel.MonthFinish10 = 0;
                    ObjUpdateModel.MonthFinish11 = 0;
                    ObjUpdateModel.MonthFinish12 = 0;
                    ObjUpdateModel.IsActive = false;

                    ObjFinishTargetSumBLL.Insert(ObjUpdateModel);
                }
            }

            JavaScriptTools.AlertWindowAndLocation("保存完毕！", Request.Url.ToString(), Page);
        }



        /// <summary>
        /// 将所有目标锁定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLock_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < repMine.Items.Count; i++)
            {

                HiddenField ObjhideKey = (HiddenField)repMine.Items[i].FindControl("hideKey");
                HiddenField ObjTargetKey = (HiddenField)repMine.Items[i].FindControl("TargetKey");
                if (ObjhideKey.Value.ToInt32() > 0)
                {
                    var ObjUpdateModel = ObjFinishTargetSumBLL.GetByID(ObjhideKey.Value.ToInt32());
                    ObjUpdateModel.LastYearFinishSum = 0;
                    ObjUpdateModel.LastYearCompletionrate = 0;
                    ObjUpdateModel.FinishSum = 0;
                    ObjUpdateModel.Completionrate = 0;
                    ObjUpdateModel.CreateEmployeeID = User.Identity.Name.ToInt32();
                    //ObjUpdateModel.EmployeeID = User.Identity.Name.ToInt32();
                    ObjUpdateModel.DepartmentID = ObjEmployeeBLL.GetByID(ObjUpdateModel.EmployeeID).DepartmentID;
                    ObjUpdateModel.OverYearFinishSum = 0;
                    ObjUpdateModel.OveryearRate = 0;
                    ObjUpdateModel.PlanSum = 0;
                    ObjUpdateModel.Year = DateTime.Now.Year;
                    ObjUpdateModel.UpdateTime = DateTime.Now;
                    ObjUpdateModel.Unite = "个";
                    ObjUpdateModel.TargetID = ObjTargetKey.Value.ToInt32();
                    ObjUpdateModel.TargetTitle = ObjTargetBLL.GetByID(ObjUpdateModel.TargetID).TargetTitle;
                    ObjUpdateModel.MonthPlan1 = ((TextBox)repMine.Items[i].FindControl("txtMonth1")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan2 = ((TextBox)repMine.Items[i].FindControl("txtMonth2")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan3 = ((TextBox)repMine.Items[i].FindControl("txtMonth3")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan4 = ((TextBox)repMine.Items[i].FindControl("txtMonth4")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan5 = ((TextBox)repMine.Items[i].FindControl("txtMonth5")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan6 = ((TextBox)repMine.Items[i].FindControl("txtMonth6")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan7 = ((TextBox)repMine.Items[i].FindControl("txtMonth7")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan8 = ((TextBox)repMine.Items[i].FindControl("txtMonth8")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan9 = ((TextBox)repMine.Items[i].FindControl("txtMonth9")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan10 = ((TextBox)repMine.Items[i].FindControl("txtMonth10")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan11 = ((TextBox)repMine.Items[i].FindControl("txtMonth11")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan12 = ((TextBox)repMine.Items[i].FindControl("txtMonth12")).Text.ToDecimal();


                    ObjFinishTargetSumBLL.Update(ObjUpdateModel);

                }
                else
                {

                    var ObjUpdateModel = new FL_FinishTargetSum();
                    ObjUpdateModel.LastYearFinishSum = 0;
                    ObjUpdateModel.LastYearCompletionrate = 0;
                    ObjUpdateModel.FinishSum = 0;
                    ObjUpdateModel.Completionrate = 0;
                    ObjUpdateModel.CreateEmployeeID = User.Identity.Name.ToInt32();
                    ObjUpdateModel.EmployeeID = User.Identity.Name.ToInt32();
                    ObjUpdateModel.DepartmentID = ObjEmployeeBLL.GetByID(ObjUpdateModel.EmployeeID).DepartmentID;
                    ObjUpdateModel.OverYearFinishSum = 0;
                    ObjUpdateModel.OveryearRate = 0;
                    ObjUpdateModel.PlanSum = 0;
                    ObjUpdateModel.Year = DateTime.Now.Year;
                    ObjUpdateModel.UpdateTime = DateTime.Now;
                    ObjUpdateModel.Unite = "个";
                    ObjUpdateModel.TargetID = ObjTargetKey.Value.ToInt32();
                    ObjUpdateModel.TargetTitle = ObjTargetBLL.GetByID(ObjUpdateModel.TargetID).TargetTitle;
                    ObjUpdateModel.MonthPlan1 = ((TextBox)repMine.Items[i].FindControl("txtMonth1")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan2 = ((TextBox)repMine.Items[i].FindControl("txtMonth2")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan3 = ((TextBox)repMine.Items[i].FindControl("txtMonth3")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan4 = ((TextBox)repMine.Items[i].FindControl("txtMonth4")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan5 = ((TextBox)repMine.Items[i].FindControl("txtMonth5")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan6 = ((TextBox)repMine.Items[i].FindControl("txtMonth6")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan7 = ((TextBox)repMine.Items[i].FindControl("txtMonth7")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan8 = ((TextBox)repMine.Items[i].FindControl("txtMonth8")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan9 = ((TextBox)repMine.Items[i].FindControl("txtMonth9")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan10 = ((TextBox)repMine.Items[i].FindControl("txtMonth10")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan11 = ((TextBox)repMine.Items[i].FindControl("txtMonth11")).Text.ToDecimal();
                    ObjUpdateModel.MonthPlan12 = ((TextBox)repMine.Items[i].FindControl("txtMonth12")).Text.ToDecimal();

                    ObjUpdateModel.MonthFinsh1 = 0;
                    ObjUpdateModel.MonthFinish2 = 0;
                    ObjUpdateModel.MonthFinish3 = 0;
                    ObjUpdateModel.MonthFinish4 = 0;
                    ObjUpdateModel.MonthFinish5 = 0;
                    ObjUpdateModel.MonthFinish6 = 0;
                    ObjUpdateModel.MonthFinish7 = 0;
                    ObjUpdateModel.MonthFinish8 = 0;
                    ObjUpdateModel.MonthFinish9 = 0;
                    ObjUpdateModel.MonthFinish10 = 0;
                    ObjUpdateModel.MonthFinish11 = 0;
                    ObjUpdateModel.MonthFinish12 = 0;
                    ObjUpdateModel.IsActive = false;

                    ObjFinishTargetSumBLL.Insert(ObjUpdateModel);
                }
            }
        }

        protected void btnUnLock_Click(object sender, EventArgs e)
        {

        }


    }
}