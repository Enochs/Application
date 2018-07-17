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
    public partial class FL_SetEmployeeTarget : SystemPage
    {
        Employee ObjEmployeeBLL = new Employee();
 
        FinishTargetSum ObjFinishTargetSumBLL = new FinishTargetSum();
        Channel ObjChannelBLL = new Channel();
        HA.PMS.BLLAssmblly.SysTarget.Target ObjTargetBLL = new HA.PMS.BLLAssmblly.SysTarget.Target();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }

        public string GetChannelNameByID(object ID)
        {
            return ObjChannelBLL.GetByID(ObjTargetBLL.GetByID(ID.ToString().ToInt32()).ChannelID).ChannelName;
        }

        #region 初始化 绑定数据

        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {
            var ObjEmployeeList = ObjEmployeeBLL.GetByID(Request["EmployeeID"].ToInt32());
            List<int> EmployeeKey = new List<int>();
            EmployeeKey.Add(ObjEmployeeList.EmployeeID);
            //foreach (var ObjItem in ObjEmployeeList)
            //{
            //    EmployeeKey.Add(ObjItem.EmployeeID);
            //}
            
            var BinderSource = ObjFinishTargetSumBLL.GetinEmployeeKeyList(EmployeeKey, 1);
            var TargetSource = ObjTargetBLL.GetByType(1);
            if (BinderSource.Count >= 0 && TargetSource.Count > 0)
            {
                if (BinderSource.Count != TargetSource.Count)       //判断是否相等  不想等 就说明有新增的
                {
                    List<PMSParameters> parms = new List<PMSParameters>();
                    int SourceCount=0;
                    foreach (var item in BinderSource)
                    {
                        parms.Add("TargetID", item.TargetID, NSqlTypes.NotIN);      //取出已存在的 查询NotIn
                    }
                    parms.Add("TargetType", 1, NSqlTypes.Equal);      //取出类型为1的
                    var source = ObjTargetBLL.GetDataByParameter(parms, "TargetID", 200, 1, ref SourceCount);
                    foreach (var item in source)
                    {
                        AddFinishTarget(item.TargetID);
                    }
                }
            }

            foreach (var ObjSource in BinderSource)
            {
                if (ObjSource.FinishKey > 0)
                {
                    if (ObjSource.IsActive != null)
                    {
                        if (ObjSource.IsActive.Value)
                        {
                            hideOldKey.Value = ObjSource.FinishKey + string.Empty;
                            hideFinishKey.Value = ObjSource.FinishKey + string.Empty;
                            continue;
                        }
                    }
                }
                else
                {
                    AddFinishTarget(ObjSource.TargetID);
                }
            }


            this.repEmployeeTargetList.DataSource = ObjFinishTargetSumBLL.GetinEmployeeKeyList(EmployeeKey, 1); 
            this.repEmployeeTargetList.DataBind();
            //if (EmployeeKey.Count > 0)
            //{

            //    this.repEmployeeTargetList.DataSource = ObjEmployeeTargetBLL.GetEmployeeTarget(EmployeeKey);
            //    this.repEmployeeTargetList.DataBind();
            //}

        }
        #endregion

        #region 点击保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        protected void btnSaveChange_Click(object sender, EventArgs e)
        {

            ObjFinishTargetSumBLL.SetNoneByEmployeeID(Request["EmployeeID"].ToInt32());
            var ObjUpdateModel = ObjFinishTargetSumBLL.GetByID(hideFinishKey.Value.ToInt32());
            ObjUpdateModel.IsActive = true;
            ObjFinishTargetSumBLL.Update(ObjUpdateModel);

            //if (hideOldKey.Value.ToInt32() > 0)
            //{
            //    var ObjOldUpdateModel = ObjFinishTargetSumBLL.GetByID(hideOldKey.Value.ToInt32());
            //    ObjOldUpdateModel.IsActive = false;
            //    ObjFinishTargetSumBLL.Update(ObjUpdateModel);
            //}

            JavaScriptTools.AlertWindow("已经指定考核指标为" + ObjUpdateModel.TargetTitle,Page);
        }
        #endregion


        #region 添加新的考核标准
        /// <summary>
        /// 传入TargetId为其赋值
        /// </summary>
        /// <param name="TargetID"></param>
        
        public void AddFinishTarget(int TargetID)           //该Id 是赋值
        {
            var ObjUpdateModel = new FL_FinishTargetSum();
            ObjUpdateModel.LastYearFinishSum = 0;
            ObjUpdateModel.LastYearCompletionrate = 0;
            ObjUpdateModel.FinishSum = 0;
            ObjUpdateModel.Completionrate = 0;
            ObjUpdateModel.CreateEmployeeID = User.Identity.Name.ToInt32() == 0 ? Request["EmployeeID"].ToInt32() : User.Identity.Name.ToInt32();
            ObjUpdateModel.EmployeeID = Request["EmployeeID"].ToInt32();
            ObjUpdateModel.DepartmentID = ObjEmployeeBLL.GetByID(ObjUpdateModel.EmployeeID).DepartmentID;
            ObjUpdateModel.OverYearFinishSum = 0;
            ObjUpdateModel.OveryearRate = 0;
            ObjUpdateModel.PlanSum = 0;
            ObjUpdateModel.Year = DateTime.Now.Year;
            ObjUpdateModel.UpdateTime = DateTime.Now;
            ObjUpdateModel.Unite = "个";
            ObjUpdateModel.TargetID = TargetID;
            ObjUpdateModel.TargetTitle = ObjTargetBLL.GetByID(ObjUpdateModel.TargetID).TargetTitle;
            ObjUpdateModel.MonthPlan1 = 0;
            ObjUpdateModel.MonthPlan2 = 0;
            ObjUpdateModel.MonthPlan3 = 0; ;
            ObjUpdateModel.MonthPlan5 = 0;
            ObjUpdateModel.MonthPlan6 = 0;
            ObjUpdateModel.MonthPlan7 = 0;
            ObjUpdateModel.MonthPlan8 = 0;
            ObjUpdateModel.MonthPlan9 = 0;
            ObjUpdateModel.MonthPlan10 = 0;
            ObjUpdateModel.MonthPlan11 = 0;
            ObjUpdateModel.MonthPlan12 = 0;

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
        #endregion
    }
}