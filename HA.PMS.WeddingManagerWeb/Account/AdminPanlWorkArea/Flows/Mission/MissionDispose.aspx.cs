using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;
using HA.PMS.DataAssmblly;
using System.IO;


namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class MissionDispose : SystemPage
    {
        /// <summary>
        /// 操作
        /// </summary>
        MissionDetailed ObjMissionManagerBLL = new MissionDetailed();

        MissionFile ObjMissionFileBLL = new MissionFile();

        /// <summary>
        /// 频道管理
        /// </summary>
        Channel ObjChannelBLL = new Channel();

        MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var BinderModel = ObjMissionDetailedBLL.GetByID(Request["DetailedID"].ToInt32());
                if (BinderModel != null)
                {
                    //BinderModel.FinishNode= txtFinishNode.Text;
                    ObjMissionDetailedBLL.Update(BinderModel);
                    if (!string.IsNullOrEmpty(BinderModel.KeyWords))
                    {
                        string[] keyvalue = BinderModel.KeyWords.Split(new char[] { '?', '&' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < keyvalue.Length; i++)
                        {
                            if (keyvalue[i].StartsWith("CustomerID=", StringComparison.OrdinalIgnoreCase))
                            {
                                CarrytaskCustomerTitle1.CustomerID = keyvalue[i].Replace("CustomerID=", null).ToInt32();
                                CarrytaskCustomerTitle1.Visible = true;
                                break;
                            }
                        }
                    }
                }
                DataBinder();
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void DataBinder()
        {

            var BinderModel = ObjMissionDetailedBLL.GetByID(Request["DetailedID"].ToInt32());

            if (BinderModel.AppraiseLevel > 0)
            {
                Response.Redirect("FL_MissionAppraise.aspx?DetailedID=" + Request["DetailedID"]);
                return;
            }
            var MissionType = BinderModel.MissionType;
            string OutTyper = string.Empty;
            switch (MissionType)
            {
                case 1:
                    OutTyper = "电话营销";
                    break;
                case 2:
                    OutTyper = "邀约";
                    break;
                case 3:
                    OutTyper = "跟单";
                    break;
                case 4:
                    OutTyper = "报价";
                    break;
                case 5:
                    OutTyper = "制作执行明细";
                    break;
                case 6:
                    OutTyper = "总派工";
                    break;
                case 7:
                    OutTyper = "分工执行";
                    break;
                case 8:
                    OutTyper = "婚礼统筹";
                    break;
            }
            BinderModel.IsLook = true;
            ObjMissionDetailedBLL.Update(BinderModel);

            lblMissionType.Text = BinderModel.MissionName;
            lblEmpLoyeeName.Text = GetEmployeeName(BinderModel.CreateEmployeeID);
            lblMissionName.Text = BinderModel.MissionName;
            // lblMissionType.Text = BinderModel.MissionType.ToString();
            txtBeginDate.Text = BinderModel.StarDate.ToString();
            //  txtCountdown.Text = BinderModel.Countdown.ToString();
            txtFinishNode.Text = BinderModel.FinishNode;
            //  txtPlanDate.Text = BinderModel.PlanDate.ToString();
            lblWorkNode.Text = BinderModel.WorkNode;
            if (BinderModel.PlanDate != null)
            {
                lblPlanDate.Text = BinderModel.PlanDate.ToString();
            }
            lblFinishStandard.Text = BinderModel.FinishStandard;
            txtFinishDate.Text = GetShortDateString(BinderModel.FinishDate.ToString());
            //if (BinderModel.MissionType <= 7 || BinderModel.MissionType == 11)
            //{
            //    var ObjChannelModel = ObjChannelBLL.GetByID(BinderModel.ChannelID);
            //    if (MissionType < 4)
            //    {
            //        hideOpen.Value = GetMissionAddress(BinderModel.MissionType) + BinderModel.KeyWords + "&MissionID=" + BinderModel.MissionID + "&NeedPopu=1&OnlyView=1";
            //    }
            //    else
            //    {
            //        hideOpen.Value = GetMissionAddress(BinderModel.MissionType) + BinderModel.KeyWords + "&MissionID=" + BinderModel.MissionID + "&NeedPopu=1";
            //    }
            //    hideLocation.Value = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "FL_NewMission.aspx?NeedPopu=1&singer=1";
            //}





            if (BinderModel.IsOver == true)
            {
                txtFinishDate.Enabled = false;
                btnFinish.Visible = false;
                txtBeginDate.Enabled = false;
                hideNeedOver.Value = "1";
                //txtCountdown.Enabled = false;
                //txtPlanDate.Enabled = false;
                txtFinishNode.Enabled = false;
            }
            this.repfileList.DataSource = ObjMissionFileBLL.GetByMission(Request["DetailedID"].ToInt32());
            this.repfileList.DataBind();
        }

        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        protected string GetMissionAddress(object source)
        {
            if (source != null)
            {
                var ValueList = Enum.GetValues(typeof(MissionTypes));
                foreach (var ObjItem in ValueList)
                {
                    if ((int)ObjItem == (int)source)
                    {
                        return MissionType.GetEnumDescription(ObjItem);
                    }
                }

            }
            return "";
        }
        /// <summary>
        /// 完结处理项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFinish_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            var OjbMissionModel = ObjMissionManagerBLL.GetByID(Request["DetailedID"].ToInt32());
            // OjbMissionModel.PlanDate = txtPlanDate.Text.ToDateTime();
            OjbMissionModel.StarDate = txtBeginDate.Text.ToDateTime();
            OjbMissionModel.FinishNode = txtFinishNode.Text;
            OjbMissionModel.IsLook = true;
            // OjbMissionModel.Countdown = txtCountdown.Text.ToInt32();

            ///婚礼统筹任务状态
            if (OjbMissionModel.MissionType == 8)
            {
                HA.PMS.BLLAssmblly.Flow.WeddingPlanning ObjWeddingPlanningBLL = new HA.PMS.BLLAssmblly.Flow.WeddingPlanning();
                var ObjUpdateModel = ObjWeddingPlanningBLL.GetByID(OjbMissionModel.FinishKey);
                ObjUpdateModel.State = "处理完毕";
                ObjWeddingPlanningBLL.Update(ObjUpdateModel);

            }

            if (OjbMissionModel.MissionType <= 8 || OjbMissionModel.MissionType == 11)
            {
                ObjMissionManagerBLL.Update(OjbMissionModel);
                var ObjChannelModel = ObjChannelBLL.GetByID(OjbMissionModel.ChannelID);

                if (OjbMissionModel.MissionType < 8 || OjbMissionModel.MissionType == 11)
                {
                    hideOpen.Value = GetMissionAddress(OjbMissionModel.MissionType) + OjbMissionModel.KeyWords + "&MissionID=" + OjbMissionModel.MissionID + "&NeedPopu=1&OnlyView=1";
                    hideLocation.Value = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "FL_NewMission.aspx?NeedPopu=1&singer=1";
                }
                //JavaScriptTools.do(GetMissionAddress(OjbMissionModel.MissionType) + OjbMissionModel.KeyWords + "&MissionID=" + OjbMissionModel.MissionID + "&NeedPopu=1", Page);

                // Response.Redirect(GetMissionAddress(OjbMissionModel.MissionType) + OjbMissionModel.KeyWords + "&MissionID=" + OjbMissionModel.MissionID + "&NeedPopu=1");
            }
            else
            {
                if (txtFinishDate.Text != string.Empty)
                {
                    OjbMissionModel.FinishDate = DateTime.Now;
                    OjbMissionModel.IsLook = true;
                    if (btn.ID.Equals("btnFinish"))
                    {
                        OjbMissionModel.IsOver = true;
                    }
                    ObjMissionManagerBLL.Update(OjbMissionModel);
                    var ObjChannelModel = ObjChannelBLL.GetByID(OjbMissionModel.ChannelID);
                    FL_Message ObjMessageModel = new FL_Message();
                    Message objMessageBLL = new Message();
                    Employee ObjEmployeeBLL = new Employee();
                    ObjMessageModel.EmployeeID = OjbMissionModel.CreateEmployeeID;

                    ObjMessageModel.MissionID = OjbMissionModel.MissionID;
                    ObjMessageModel.IsDelete = false;
                    ObjMessageModel.IsLook = false;
                    ObjMessageModel.Message = ObjEmployeeBLL.GetByID(OjbMissionModel.EmpLoyeeID).EmployeeName + "的" + OjbMissionModel.MissionName + " 任务已经完成。 录入时间为：" + DateTime.Now;
                    ObjMessageModel.MessAgeTitle = "任务完成";
                    ObjMessageModel.KeyWords = "FL_MissionAppraise.aspx?DetailedID=" + OjbMissionModel.DetailedID;
                    ObjMessageModel.CreateEmployeename = ObjEmployeeBLL.GetByID(User.Identity.Name.ToInt32()).EmployeeName;
                    objMessageBLL.Insert(ObjMessageModel);
                    if (btn.ID.Equals("btnFinish"))
                    {
                        JavaScriptTools.AlertWindowAndLocation("处理完毕", Request.UrlReferrer.ToString(), Page);
                    }
                    else
                    {
                        JavaScriptTools.AlertWindowAndLocation("保存完毕", Request.UrlReferrer.ToString(), Page);
                    }


                }
            }
            if (txtFinishDate.Text != string.Empty)
            {
                txtFinishDate.Enabled = false;
                btnFinish.Enabled = false;
                txtBeginDate.Enabled = false;
                //txtCountdown.Enabled = false;
                //txtPlanDate.Enabled = false;
                txtFinishNode.Enabled = false;
                btnFinish.Visible = false;
            }
        }

        protected void repfileList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var ObjDeleteModel = ObjMissionFileBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                System.IO.File.Delete(Server.MapPath(ObjDeleteModel.FileAddress));
                ObjMissionFileBLL.Delete(ObjDeleteModel);
            }
            DataBinder();
        }
    }
}