using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.Pages;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissionSumupManagerCreateEdit : SystemPage
    {
        /// <summary>
        /// 任务详情操作
        /// </summary>
        MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();
        /// <summary>
        /// 任务
        /// </summary>
        MissionManager ObjMissionManagerBLL = new MissionManager();

        MissionSumup ObjMissionSumupBLL = new MissionSumup();

        Employee ObjEmployeeBLL = new Employee();

        MissionFile ObjMissionFileBLL = new MissionFile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int MissionID = Request["MissionID"].ToInt32();
                this.rptMission.DataSource = ObjMissionDetailedBLL.GetbyMissionID(MissionID);
                this.rptMission.DataBind();
                if (MissionID > 0)
                {
                    var ObjMissionModel = ObjMissionManagerBLL.GetByID(MissionID);
                    this.txtTitle.Text = ObjMissionModel.MissionTitle + "的总结";

                    lblAllMissionSum.Text = ObjMissionDetailedBLL.GetCountByEmployeeID(User.Identity.Name.ToInt32()).ToString();
                    lblFinishSum.Text = ObjMissionManagerBLL.GetMissionSum(User.Identity.Name.ToInt32(), 1, "",1).MissiionCount.ToString();
                    lblOverFinishSum.Text = ObjMissionManagerBLL.GetMissionSum(User.Identity.Name.ToInt32(), 3, "",1).MissiionCount.ToString();
                    decimal AllMisionSum = lblAllMissionSum.Text.ToDecimal();
                    decimal FinishSum = lblFinishSum.Text.ToDecimal() + lblOverFinishSum.Text.ToDecimal();
                    if (AllMisionSum > 0)
                    {
                        lblFinishRate.Text = (FinishSum / AllMisionSum).ToString("0.00%");
                    }
                    else
                    {
                        lblFinishRate.Text = "0.00%";
                    }
                }

            }
        }

        #region 点击保存  保存功能


        protected void btnSaveChange_Click(object sender, EventArgs e)
        {
            int MissionID = 0;
            if (Request["MissionID"] == null)
            {
                int EmpLoyeeID = User.Identity.Name.ToInt32();

                FL_MissionManager ObjMissionManagerModel = new FL_MissionManager();
                ObjMissionManagerModel.CreateDate = System.DateTime.Now;
                ObjMissionManagerModel.DepartmentID = ObjEmployeeBLL.GetByID(EmpLoyeeID).DepartmentID;
                ObjMissionManagerModel.EmployeeID = EmpLoyeeID;
                ObjMissionManagerModel.CheckEmpLoyeeID = ObjEmployeeBLL.GetMineCheckEmployeeID(User.Identity.Name.ToInt32());
                ObjMissionManagerModel.IsDelete = false;
                ObjMissionManagerModel.IsOver = true;
                ObjMissionManagerModel.Type = 80;
                if (ObjMissionManagerModel.CheckEmpLoyeeID == User.Identity.Name.ToInt32())
                {
                    ObjMissionManagerModel.MissionType = (int)MissionTypes.Mine;
                    ObjMissionManagerModel.IsAppraise = true;
                    ObjMissionManagerModel.IsCheck = true;
                    ObjMissionManagerModel.CheckState = 3;
                }
                else
                {
                    ObjMissionManagerModel.MissionType = (int)MissionTypes.Mine;
                    ObjMissionManagerModel.IsAppraise = false;
                    ObjMissionManagerModel.IsCheck = true;
                }
                //if (Title == string.Empty)
                //{
                //    ObjMissionManagerModel.MissionTitle = DateTime.Now.ToShortDateString() + "-" + OutTyper + "任务";
                //}
                //else
                //{
                ObjMissionManagerModel.MissionTitle = txtTitle.Text;
                //}

                MissionID = ObjMissionManagerBLL.Insert(ObjMissionManagerModel);


                FL_MissionSumup ObjMissionSumupModel = new FL_MissionSumup();


                ObjMissionSumupModel.MissionID = MissionID;
                ObjMissionSumupModel.IsFinish = true;
                ObjMissionSumupModel.SumUp = txtSumUp.Text;
                ObjMissionSumupModel.IsDelete = false;
                ObjMissionSumupModel.DetailedID = 0;
                ObjMissionSumupModel.Title = txtTitle.Text;
                ObjMissionSumupModel.Counselingadvice = string.Empty;

                ObjMissionSumupBLL.Insert(ObjMissionSumupModel);
            }
            else
            {
                MissionID = Request["MissionID"].ToInt32();

                var ObjMissionManagerModel = ObjMissionManagerBLL.GetByID(MissionID);
                //ObjMissionManagerModel.IsAppraise = true;
                ObjMissionManagerModel.IsOver = false;
                ObjMissionManagerBLL.Update(ObjMissionManagerModel);

                //更新
                var ObjUpdateModel = ObjMissionSumupBLL.GetByMissionID(MissionID);


                if (ObjUpdateModel == null)
                {
                    FL_MissionSumup ObjMissionSumupModel = new FL_MissionSumup();
                    ObjMissionSumupModel.MissionID = MissionID;
                    ObjMissionSumupModel.IsFinish = false;
                    ObjMissionSumupModel.SumUp = txtSumUp.Text;
                    ObjMissionSumupModel.IsDelete = false;
                    ObjMissionSumupModel.DetailedID = 0;
                    ObjMissionSumupModel.Title = txtTitle.Text;

                    ObjMissionSumupBLL.Insert(ObjMissionSumupModel);
                }
                else
                {

                    ObjUpdateModel.SumUp = txtSumUp.Text;
                    ObjMissionSumupBLL.Update(ObjUpdateModel);
                }
            }

            var DataList = ObjMissionDetailedBLL.GetbyMissionID(Request["MissionID"].ToInt32());
            foreach (var item in DataList)
            {
                //超时完成
                if (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime() > item.PlanDate)       //当前时间大于计划完成时间
                {
                    item.MissionState = 3;
                }
                //按时完成
                else if (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToDateTime() <= item.PlanDate)       //当前时间小于计划完成时间  
                {
                    item.MissionState = 1;
                }
                item.FinishDate = DateTime.Now;
                item.FinishNode = txtSumUp.Text.Trim().ToString();
                item.IsOver = true;
                ObjMissionDetailedBLL.Update(item);
            }

            JavaScriptTools.AlertWindowAndLocation("保存成功！", "FL_MissionSumupManager.aspx", Page);
        }
        #endregion


        #region 提交到部门主管
        /// <summary>
        /// 提交到部门主管
        /// </summary>
        protected void btnFinish_Click(object sender, EventArgs e)
        {

            //int MissionID = Request["MissionID"].ToInt32();
            //var ObjUpdateModel = ObjMissionSumupBLL.GetByMissionID(MissionID);
            //ObjUpdateModel.IsFinish = true;
            //if (txtCounselingadvice.Text != string.Empty && ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
            //{
            //    var ObjMissionModel= ObjMissionManagerBLL.GetByID(MissionID);
            //    ObjMissionModel.IsOver = true;
            //    ObjMissionModel.Counselingadvice = txtCounselingadvice.Text;
            //    ObjMissionManagerBLL.Update(ObjMissionModel);
            //}
            //ObjMissionSumupBLL.Update(ObjUpdateModel);
            var ObjMissionModel = ObjMissionManagerBLL.GetByID(Request["MissionID"].ToInt32());
            ObjMissionModel.IsOver = false;
            ObjMissionModel.IsAppraise = true;

            JavaScriptTools.AlertWindowAndLocation("保存成功！", "FL_MissionSumupManager.aspx", Page);
        }
        #endregion
    }
}