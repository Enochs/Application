using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;

//生成任务列表
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissionDetailedCreate : SystemPage
    {
        /// <summary>
        /// 责任人
        /// </summary>
        Employee objEmployeeBLL = new Employee();

        /// <summary>
        /// 部门
        /// </summary>
        Department objDepartmenttBLL = new Department();

        /// <summary>
        /// 任务管理
        /// </summary>
        MissionManager objMissionManagerBLL = new MissionManager();
        /// <summary>
        /// 任务详情操作
        /// </summary>
        MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();

        protected bool IsMissionPackDispatchOpening = false;   //指示任务同时可以派给多个人功能是否处于关闭状态

        int MissionID = 0;

        #region 临时类   序列化
        //序列化临时类
        [Serializable]
        public partial class MissionDetaileds
        {
            public string Attachment { get; set; }
            public int? ChecksEmployee { get; set; }
            public int? Countdown { get; set; }
            public int DetailedID { get; set; }
            public string Emergency { get; set; }
            public int? EmpLoyeeID { get; set; }
            public DateTime? FinishDate { get; set; }
            public string FinishStandard { get; set; }
            public bool? IsDelete { get; set; }
            public int? MissionID { get; set; }
            public string MissionName { get; set; }
            public string WorkNode { get; set; }
            public DateTime StarDate { get; set; }
        }
        #endregion



        #region 页面初始化
        /// <summary>
        /// 页面加载
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            //任务同时可以派给多个人功能
            IsMissionPackDispatchOpening = new SysConfig().IsMissionPackDispatchOpening(User.Identity.Name.ToInt32(), false);


            if (Request["MissionID"] == null)
            {
                MissionID = hideMisioniID.Value.ToInt32();
            }
            else
            {
                MissionID = Request["MissionID"].ToInt32();
            }
            if (Request["singer"] != null)
            {
                ViewState["style"] = "";

            }
            else
            {
                ViewState["style"] = "width:230px;";
            }
            Employee ObjEmployeeBLL = new Employee();
            if (Request["EmployeeID"].ToInt32() > 0)
            {
                rdoMissionList.Items.Clear();
                rdoMissionList.Items.Add(new ListItem("临时任务", "60"));
                rdoMissionList.Items[0].Selected = true;
                txtEmpLoyee.Value = GetEmployeeName(Request["EmployeeID"].ToInt32());
                btnSavetoChecks.Visible = false;
                hideEmpLoyeeID.Value = Request["EmployeeID"];
                hideEmployeeName.Value = txtEmpLoyee.Value;
            }
            else
            {
                txtEmpLoyee.Value = GetEmployeeName(User.Identity.Name.ToInt32());
            }
            if (ObjEmployeeBLL.IsManager(User.Identity.Name.ToInt32()))
            {
                hideIsmanager.Value = "1";

            }
            if (!IsPostBack)
            {
                //绑定父级任务 如果有
                if (Request["Parent"].ToInt32() > 0)
                {
                    var ObjMissionModel = ObjMissionDetailedBLL.GetByID(Request["Parent"].ToInt32());
                    if (ObjMissionModel.MissionType == 8)
                    {
                        txtMissionName.Text = ObjMissionModel.MissionName;
                        txtWorkNode.Text = ObjMissionModel.WorkNode;
                        txtFinishStandard.Text = ObjMissionModel.FinishStandard;
                        txtStarDate.Text = ObjMissionModel.StarDate.ToString();
                        txtFinishDate.Text = ObjMissionModel.FinishDate.ToString();
                    }
                }

                //判断审核人是否为自己
                if (objEmployeeBLL.GetMineCheckEmployeeID(User.Identity.Name.ToInt32()) == User.Identity.Name.ToInt32())
                {
                    btnSavetoChecks.Text = "下达任务";
                }

                hideEmployeeName.Value = GetEmployeeName(User.Identity.Name) + "的";
                hideEmpLoyeeID.Value = User.Identity.Name;
                BinderCreate();
            }
        }
        #endregion

        #region 创建之后  在下面的列表中 绑定数据 (方便修改)
        /// <summary>
        /// 绑定创建数据
        /// </summary>
        private void BinderCreate()
        {
            if (Request["MissionID"] != null)
            {
                var ObjModel = objMissionManagerBLL.GetByID(Request["MissionID"].ToInt32());
                if (ObjModel != null)
                {
                    txtMissiontitle.Text = ObjModel.MissionTitle;
                    txttimerEnd.Text = ObjModel.TimerEnd.ToString();
                    txttimerStar.Text = ObjModel.TimerStar.ToString();
                    ListItem listChoose = rdoMissionList.Items.FindByValue(ObjModel.Type.ToString());
                    if (listChoose != null)
                    {
                        listChoose.Selected = true;
                    }
                }
            }
            if (rdoMissionList.SelectedValue.Equals("60"))
            {
                this.rptMission.DataSource = ObjMissionDetailedBLL.GetbyMissionIDDistinct(MissionID);
                this.rptMission.DataBind();
            }
            else
            {
                this.rptMission.DataSource = ObjMissionDetailedBLL.GetbyMissionID(MissionID);
                this.rptMission.DataBind();
            }
        }
        #endregion

        #region 列表绑定事件
        /// <summary>
        /// 删除
        /// </summary>
        protected void rptMission_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //如果为临时任务,并且该功能已开启
            if (rdoMissionList.SelectedValue.Equals("60") && IsMissionPackDispatchOpening)      //临时任务
            {
                //所有责任人的 DetailedID
                List<int> DetailedIDs = new List<int>();
                var MissionDetailList = ObjMissionDetailedBLL.GetbyMissionID(e.GetTextValue("hideMissionID").ToString().ToInt32());
                foreach (FL_MissionDetailed Item in MissionDetailList)
                {
                    DetailedIDs.Add(Item.DetailedID);
                }

                string[] EmpLoyeeIDs = e.GetTextValue("hideEmpLoyeeID").ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                int MissionID = e.GetTextValue("hideMissionID").ToString().ToInt32();

                if (e.CommandName == "Checks")      //保存变更  (看不见这个按钮  感觉没什么用   所以没有深究)
                {
                    foreach (string EmpLoyeeID in EmpLoyeeIDs)
                    {
                        FL_MissionDetailed ObjMissionDetaileMdeol = new FL_MissionDetailed();
                        ObjMissionDetaileMdeol.MissionID = MissionID;
                        ObjMissionDetaileMdeol.MissionName = ((TextBox)e.Item.FindControl("txtMissionName")).Text;
                        ObjMissionDetaileMdeol.WorkNode = ((TextBox)e.Item.FindControl("txtWorkNode")).Text;
                        ObjMissionDetaileMdeol.FinishStandard = ((TextBox)e.Item.FindControl("txtFinishStandard")).Text;
                        ObjMissionDetaileMdeol.PlanDate = ((TextBox)e.Item.FindControl("txtFinishDate")).Text.ToDateTime();
                        ObjMissionDetaileMdeol.Countdown = ((TextBox)e.Item.FindControl("txtCountdown")).Text.ToInt32();
                        ObjMissionDetaileMdeol.Emergency = ((TextBox)e.Item.FindControl("txtEmergency")).Text;
                        ObjMissionDetaileMdeol.StarDate = ((TextBox)e.Item.FindControl("txtStarDate")).Text.ToDateTime();
                        ObjMissionDetaileMdeol.EmpLoyeeID = ((HiddenField)e.Item.FindControl("hideEmpLoyeeID")).Value.ToInt32();
                        ObjMissionDetaileMdeol.AppraiseLevel = -1;
                        ObjMissionDetaileMdeol.ChecksState = 3;

                        ObjMissionDetaileMdeol.ChecksNode = "本人任务";
                        ObjMissionDetaileMdeol.CreateEmployeeID = User.Identity.Name.ToInt32();
                        ObjMissionDetaileMdeol.CreateEmployeeName = GetEmployeeName(User.Identity.Name.ToInt32());
                        ObjMissionDetaileMdeol.Type = (int)MissionTypes.Mine;
                        ObjMissionDetaileMdeol.IsDelete = false;
                        ObjMissionDetaileMdeol.IsOver = false;
                        ObjMissionDetaileMdeol.IsLook = true;
                        ObjMissionDetaileMdeol.FinishKey = 0;
                        ObjMissionDetaileMdeol.ChannelID = 0;
                        ObjMissionDetaileMdeol.MissionType = rdoMissionList.SelectedValue.ToInt32();
                        ObjMissionDetaileMdeol.ChecksEmployee = User.Identity.Name.ToInt32();
                        ObjMissionDetaileMdeol.CreateDate = DateTime.Now;

                        ObjMissionDetailedBLL.Insert(ObjMissionDetaileMdeol);
                    }
                    foreach (int DetailedID in DetailedIDs)
                    {
                        ObjMissionDetailedBLL.Delete(new FL_MissionDetailed { DetailedID = DetailedID });
                    }
                }

                if (e.CommandName == "Change")      //保存编辑  (正常使用)
                {
                    foreach (string EmpLoyeeID in EmpLoyeeIDs)
                    {
                        FL_MissionDetailed ObjMissionDetaileMdeol = ObjMissionDetailedBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                        ObjMissionDetaileMdeol.MissionName = ((TextBox)e.Item.FindControl("txtMissionName")).Text;
                        ObjMissionDetaileMdeol.WorkNode = ((TextBox)e.Item.FindControl("txtWorkNode")).Text;
                        ObjMissionDetaileMdeol.FinishStandard = ((TextBox)e.Item.FindControl("txtFinishStandard")).Text;
                        ObjMissionDetaileMdeol.PlanDate = ((TextBox)e.Item.FindControl("txtFinishDate")).Text.ToDateTime();
                        ObjMissionDetaileMdeol.Countdown = ((TextBox)e.Item.FindControl("txtCountdown")).Text.ToInt32();
                        ObjMissionDetaileMdeol.Emergency = ((TextBox)e.Item.FindControl("txtEmergency")).Text;
                        ObjMissionDetaileMdeol.StarDate = ((TextBox)e.Item.FindControl("txtStarDate")).Text.ToDateTime();

                        ObjMissionDetailedBLL.Update(ObjMissionDetaileMdeol);
                    }
                }
                else if (e.CommandName == "Delete")     //删除  (同时删除任务  及任务管理)
                {
                    foreach (int DetailID in DetailedIDs)
                    {
                        ObjMissionDetailedBLL.Delete(ObjMissionDetailedBLL.GetByID(DetailID));
                        objMissionManagerBLL.Delete(objMissionManagerBLL.GetByID(MissionID));
                    }
                }
            }
            else            //除临时任务外的所有任务
            {
                if (e.CommandName == "Checks")
                {
                    FL_MissionDetailed ObjMissionDetaileMdeol = ObjMissionDetailedBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                    ObjMissionDetaileMdeol.MissionName = ((TextBox)e.Item.FindControl("txtMissionName")).Text;
                    ObjMissionDetaileMdeol.WorkNode = ((TextBox)e.Item.FindControl("txtWorkNode")).Text;
                    ObjMissionDetaileMdeol.FinishStandard = ((TextBox)e.Item.FindControl("txtFinishStandard")).Text;
                    ObjMissionDetaileMdeol.PlanDate = ((TextBox)e.Item.FindControl("txtFinishDate")).Text.ToDateTime();
                    ObjMissionDetaileMdeol.Countdown = ((TextBox)e.Item.FindControl("txtCountdown")).Text.ToInt32();
                    ObjMissionDetaileMdeol.Emergency = ((TextBox)e.Item.FindControl("txtEmergency")).Text;
                    ObjMissionDetaileMdeol.StarDate = ((TextBox)e.Item.FindControl("txtStarDate")).Text.ToDateTime();
                    ObjMissionDetaileMdeol.EmpLoyeeID = ((HiddenField)e.Item.FindControl("hideEmpLoyeeID")).Value.ToInt32();
                    ObjMissionDetaileMdeol.AppraiseLevel = -1;
                    ObjMissionDetaileMdeol.ChecksState = 1;

                    ObjMissionDetailedBLL.Update(ObjMissionDetaileMdeol);
                }

                else if (e.CommandName == "Change")
                {
                    FL_MissionDetailed ObjMissionDetaileMdeol = ObjMissionDetailedBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                    ObjMissionDetaileMdeol.MissionName = ((TextBox)e.Item.FindControl("txtMissionName")).Text;
                    ObjMissionDetaileMdeol.WorkNode = ((TextBox)e.Item.FindControl("txtWorkNode")).Text;
                    ObjMissionDetaileMdeol.FinishStandard = ((TextBox)e.Item.FindControl("txtFinishStandard")).Text;
                    ObjMissionDetaileMdeol.PlanDate = ((TextBox)e.Item.FindControl("txtFinishDate")).Text.ToDateTime();
                    ObjMissionDetaileMdeol.Countdown = ((TextBox)e.Item.FindControl("txtCountdown")).Text.ToInt32();
                    ObjMissionDetaileMdeol.Emergency = ((TextBox)e.Item.FindControl("txtEmergency")).Text;
                    ObjMissionDetaileMdeol.StarDate = ((TextBox)e.Item.FindControl("txtStarDate")).Text.ToDateTime();
                    ObjMissionDetaileMdeol.AppraiseLevel = -1;
                    ObjMissionDetaileMdeol.EmpLoyeeID = ((HiddenField)e.Item.FindControl("hideEmpLoyeeID")).Value.ToInt32();
                    ObjMissionDetaileMdeol.ChecksState = 1;
                    ObjMissionDetaileMdeol.MissionState = 0;
                    ObjMissionDetailedBLL.Update(ObjMissionDetaileMdeol);
                }
                else if (e.CommandName == "Delete")
                {
                    ObjMissionDetailedBLL.Delete(ObjMissionDetailedBLL.GetByID(e.CommandArgument.ToString().ToInt32()));
                }

            }
            BinderCreate();
        }
        #endregion


        #region 设置 除临时任务外的起止时间
        /// <summary>
        /// 设置模型时间
        /// </summary>
        public FL_MissionManager SetDateforModel(FL_MissionManager ObjModel)
        {
            int Satate = rdoMissionList.SelectedValue.ToInt32();
            switch (Satate)
            {
                case 61:
                    ObjModel.TimerEnd = txttimerEnd.Text.ToDateTime();
                    ObjModel.TimerStar = txttimerStar.Text.ToDateTime();
                    break;

                case 62:
                    if ((12 - ddlMonth.SelectedValue.ToInt32()) > 0)
                    {
                        ObjModel.TimerStar = (DateTime.Now.Year + "-" + ddlMonth.SelectedValue + "-1").ToDateTime();
                        ObjModel.TimerEnd = (DateTime.Now.Year + "-" + ddlMonth.SelectedValue + "-1").ToDateTime().AddDays(DateTime.DaysInMonth(DateTime.Now.Year, ddlMonth.SelectedValue.ToInt32()));
                    }
                    break;

                case 63:
                    if ((12 - ddlMonth.SelectedValue.ToInt32() * 3) > 0)
                    {
                        ObjModel.TimerStar = (DateTime.Now.Year + "-" + (ddljidu.SelectedValue.ToInt32() - 1) * 3 + "-1").ToDateTime();
                        ObjModel.TimerEnd = (DateTime.Now.Year + "-" + (ddljidu.SelectedValue.ToInt32() - 1) * 3 + "-1").ToDateTime().AddMonths(ddljidu.SelectedValue.ToInt32() * 3);
                    }
                    break;

                case 64:
                    ObjModel.TimerStar = DateTime.Parse(DateTime.Now.Year + "-1-1");
                    ObjModel.TimerEnd = DateTime.Parse(DateTime.Now.Year + "-12-30");
                    break;

                case 65:
                    if (ddlniandu.SelectedItem.Text == "上半年")
                    {

                        ObjModel.TimerStar = (DateTime.Now.Year + "-1-1").ToDateTime();
                        ObjModel.TimerEnd = (DateTime.Now.Year + "-1-1").ToDateTime().AddMonths(6);
                    }
                    else
                    {
                        ObjModel.TimerStar = (DateTime.Now.Year + "-6-1").ToDateTime();
                        ObjModel.TimerEnd = (DateTime.Now.Year + "-6-1").ToDateTime().AddMonths(6);
                    }

                    break;

            }
            return ObjModel;
        }
        #endregion

        #region 添加任务
        /// <summary>
        /// 添加/新建任务
        /// </summary>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string OutName = string.Empty;
            if (Request["MissionID"] == null)
            {
                if (hideMisioniID.Value == string.Empty)
                {
                    MissionID = objMissionManagerBLL.CheckInsert(txtMissiontitle.Text, rdoMissionList.SelectedValue.ToInt32(), (int)MissionTypes.Mine, DateTime.Now, User.Identity.Name.ToInt32(), out OutName);
                    hideMisioniID.Value = MissionID.ToString();
                }
            }
            else
            {
                MissionID = Request["MissionID"].ToInt32();
            }
            if (txtFinishDate.Text.ToDateTime() < txtStarDate.Text.ToDateTime())
            {
                JavaScriptTools.AlertWindow("计划完成时间必须大于计划开始时间！", Page);
                return;
            }


            FL_MissionDetailed ObjModel = new FL_MissionDetailed();
            Employee ObjEmployeeBLL = new Employee();
            FL_MissionDetailed ObjMissionDetaileMdeol = new FL_MissionDetailed();
            ObjMissionDetaileMdeol.MissionName = txtMissionName.Text;
            ObjMissionDetaileMdeol.WorkNode = txtWorkNode.Text;
            ObjMissionDetaileMdeol.FinishStandard = txtFinishStandard.Text;
            ObjMissionDetaileMdeol.PlanDate = txtFinishDate.Text.ToDateTime();
            ObjMissionDetaileMdeol.Countdown = ddlCotown.SelectedItem.Text.ToInt32();
            ObjMissionDetaileMdeol.Emergency = ddlEmergency.SelectedItem.Text;
            ObjMissionDetaileMdeol.ChecksNode = string.Empty;
            ObjMissionDetaileMdeol.CreateEmployeeID = User.Identity.Name.ToInt32();
            ObjMissionDetaileMdeol.CreateEmployeeName = GetEmployeeName(User.Identity.Name.ToInt32());
            ObjMissionDetaileMdeol.MissionID = MissionID;

            ObjMissionDetaileMdeol.Type = (int)MissionTypes.Mine;
            ObjMissionDetaileMdeol.IsDelete = false;
            ObjMissionDetaileMdeol.IsOver = false;
            ObjMissionDetaileMdeol.IsLook = true;
            ObjMissionDetaileMdeol.FinishKey = 0;
            ObjMissionDetaileMdeol.ChannelID = 0;
            ObjMissionDetaileMdeol.MissionType = rdoMissionList.SelectedValue.ToInt32();
            ObjMissionDetaileMdeol.ChecksEmployee = User.Identity.Name.ToInt32();
            ObjMissionDetaileMdeol.ChecksState = 1;
            ObjMissionDetaileMdeol.MissionState = 0;
            ObjMissionDetaileMdeol.AppraiseLevel = -1;
            ObjMissionDetaileMdeol.Emergency = ddlEmergency.SelectedItem.Text;

            //计算此处的开始结束时间
            ObjMissionDetaileMdeol.StarDate = txtStarDate.Text.ToDateTime();
            ObjMissionDetaileMdeol.PlanDate = txtFinishDate.Text.ToDateTime();
            if (rdoMissionList.SelectedValue == "60")
            {
                ObjMissionDetaileMdeol.ChecksState = 3;
                btnSavetoChecks.Visible = false;
                rdoMissionList.Enabled = false;
            }
            ObjMissionDetaileMdeol.CreateDate = DateTime.Now;
            ObjMissionDetaileMdeol.ChecksNode = string.Empty;

            string[] EmployeeIDs = hideEmpLoyeeID.Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //批量插入
            foreach (string Item in EmployeeIDs)
            {
                ObjMissionDetaileMdeol.EmpLoyeeID = Item.ToInt32();
            }
            ObjMissionDetailedBLL.Insert(ObjMissionDetaileMdeol);

            //开始植入时间段
            var ObjManagerModel = objMissionManagerBLL.GetByID(MissionID);
            ObjManagerModel = SetDateforModel(ObjManagerModel);

            objMissionManagerBLL.Update(ObjManagerModel);

            if (rdoMissionList.SelectedValue == "60")
            {

                var UpdateModel = objMissionManagerBLL.GetByID(MissionID);
                ObjModel.ChecksDate = DateTime.Now;
                ObjModel.ChecksChangeNode = "本人任务";
                ObjModel.ChecksState = 3;
                UpdateModel.IsCheck = true;
                UpdateModel.IsDelete = false;
                UpdateModel.CheckState = 3;
                UpdateModel.CheckEmpLoyeeID = 0;
                objMissionManagerBLL.Update(UpdateModel);
            }
            else
            {
                var UpdateModel = objMissionManagerBLL.GetByID(MissionID);
                ObjModel.ChecksDate = DateTime.Now;
                ObjModel.ChecksChangeNode = txtMissiontitle.Text;
                ObjModel.ChecksState = 1;
                UpdateModel.IsCheck = true;
                UpdateModel.IsDelete = false;
                UpdateModel.CheckEmpLoyeeID = 0;
                objMissionManagerBLL.Update(UpdateModel);

            }

            JavaScriptTools.AlertWindow("保存完毕！", Page);
            BinderCreate();
            txtFinishDate.Text = string.Empty;
            txtFinishStandard.Text = string.Empty;
            txtMissionName.Text = string.Empty;
            txtWorkNode.Text = string.Empty;
            txtStarDate.Text = string.Empty;
        }
        #endregion

        #region 提交审核功能  外部最底部
        /// <summary>
        /// 提交到审核人处
        /// </summary>
        protected void btnSavetoChecks_Click(object sender, EventArgs e)
        {
            string OutName = string.Empty;
            if (rdoMissionList.SelectedValue == "60")
            {
                JavaScriptTools.AlertWindow("临时任务不需要审核", Page);
                return;
            }
            if (rptMission.Items.Count > 0)
            {
                //btnSaveChange_Click(sender, e);
                //  var MissionID = hideMisioniID.Value.ToInt32(); //objMissionManagerBLL.CheckInsert(txtMissiontitle.Text, rdoMissionList.SelectedValue.ToInt32(), (int)MissionTypes.Mine, DateTime.Now, User.Identity.Name.ToInt32(), out OutName);
                var UpdateModel = objMissionManagerBLL.GetByID(MissionID);

                UpdateModel.IsCheck = true;
                UpdateModel.IsDelete = false;
                UpdateModel.CheckState = 2;

                if (objEmployeeBLL.GetMineCheckEmployeeID(User.Identity.Name.ToInt32()) == User.Identity.Name.ToInt32())
                {

                    UpdateModel.IsCheck = true;
                    UpdateModel.IsDelete = false;
                    UpdateModel.CheckState = 3;
                }
                UpdateModel.CheckEmpLoyeeID = objEmployeeBLL.GetMineCheckEmployeeID(User.Identity.Name.ToInt32());
                objMissionManagerBLL.Update(UpdateModel);

                ///进入变更
                HA.PMS.DataAssmblly.FL_MissionChange ObjMissionChangeModel = new HA.PMS.DataAssmblly.FL_MissionChange();
                if (rdoMissionList.SelectedValue != "60")
                {
                    MissionChange ObjMissionChangeBLL = new MissionChange();

                    ObjMissionChangeModel.IsChangeMission = false;
                    ObjMissionChangeModel.CreateDate = DateTime.Now;
                    ObjMissionChangeModel.DetailedID = 0;
                    ObjMissionChangeModel.MissionID = UpdateModel.MissionID;


                    ObjMissionChangeModel.KeyWords = "FL_MissionGroupCheck.aspx?MissionID=" + UpdateModel.MissionID;
                    ObjMissionChangeModel.CreateEmpLoyee = User.Identity.Name.ToInt32();
                    ObjMissionChangeModel.ChecksEmployee = User.Identity.Name.ToInt32();
                    ObjMissionChangeModel.MissionName = UpdateModel.MissionTitle;
                    ObjMissionChangeModel.MissionType = 2;      //dangrenwu单个任务

                    ObjMissionChangeBLL.Insert(ObjMissionChangeModel);
                    //}
                }
                if (UpdateModel.CheckEmpLoyeeID != User.Identity.Name.ToInt32())
                {

                    JavaScriptTools.AlertWindowAndLocation("成功提交到部门审核人‘" + objEmployeeBLL.GetByID(ObjMissionChangeModel.ChecksEmployee).EmployeeName + "’处!", "FL_MissionMananger.aspx?NeedPopu=1", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindowAndLocation("任务已经下达!", "FL_MissionMananger.aspx?NeedPopu=1", Page);
                }
            }
            else
            {
                JavaScriptTools.AlertWindow("请先添加任务!", Page);
            }
        }
        #endregion

        #region 点击保存(底部)
        /// <summary>
        /// 保存功能
        /// </summary>
        protected void btnSavetable_Click(object sender, EventArgs e)
        {
            if (rptMission.Items.Count > 0)
            {
                if (Request["EmployeeID"].ToInt32() > 0)
                {
                    JavaScriptTools.AlertWindowAndLocation("任务已下达!", "FL_MissionMananger.aspx?NeedPopu=1", Page);
                }
                else
                {
                    JavaScriptTools.AlertWindowAndLocation("未提交审核，已经保存到编辑中的任务，你可以'编辑中的任务/计划中'功能继续编辑!", "FL_MissionMananger.aspx?NeedPopu=1", Page);
                }
            }
            else
            {
                JavaScriptTools.AlertWindow("请在右边新增任务按钮处添加任务！", Page);
            }
        }
        #endregion

        #region 获取任务 责任人的姓名以及编号
        /// <summary>
        /// 获取姓名
        /// </summary>
        protected string GetMissionEmployeeNames(object DetailedID)
        {
            string result = string.Empty;
            var MissionDetailModel = ObjMissionDetailedBLL.GetByID((DetailedID + string.Empty).ToInt32());
            if (!object.ReferenceEquals(MissionDetailModel, null))
            {
                var ObjMissionDetailList = ObjMissionDetailedBLL.GetbyMissionID(MissionDetailModel.MissionID);
                foreach (FL_MissionDetailed Item in ObjMissionDetailList)
                {
                    result += GetEmployeeName(Item.EmpLoyeeID) + ",";
                }
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }

        protected string GetMissionEmployeeIDs(object DetailedID)
        {
            string result = string.Empty;
            var MissionDetailModel = ObjMissionDetailedBLL.GetByID((DetailedID + string.Empty).ToInt32());
            if (!object.ReferenceEquals(MissionDetailModel, null))
            {
                var ObjMissionDetailList = ObjMissionDetailedBLL.GetbyMissionID(MissionDetailModel.MissionID);
                foreach (FL_MissionDetailed Item in ObjMissionDetailList)
                {
                    result += Item.EmpLoyeeID.Value + ",";
                }
                result = result.Substring(0, result.Length - 1);
            }
            return result;
        }
        #endregion
    }
}