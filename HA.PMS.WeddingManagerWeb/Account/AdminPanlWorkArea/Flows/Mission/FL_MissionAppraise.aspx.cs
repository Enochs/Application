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

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Flows.Mission
{
    public partial class FL_MissionAppraise : SystemPage
    {
        /// <summary>
        /// 操作
        /// </summary>
        MissionDetailed ObjMissionManagerBLL = new MissionDetailed();


        /// <summary>
        /// 频道管理
        /// </summary>
        Channel ObjChannelBLL = new Channel();

        MissionFile ObjMissionFileBLL = new MissionFile();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
                if (txtFinishDate.Text != string.Empty)
                {
                    txtFinishDate.Enabled = false;

                    txtBeginDate.Enabled = false;
                    txtCountdown.Enabled = false;
                    txtPlanDate.Enabled = false;
                    txtFinishNode.Enabled = false;
                }


            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void DataBinder()
        {
            MissionDetailed ObjMissionDetailedBLL = new MissionDetailed();
            var BinderModel = ObjMissionDetailedBLL.GetByID(Request["DetailedID"].ToInt32());
            txtAppraiseContent.Text = BinderModel.AppraiseContent;
            var SelectItem=ddlAppraise.Items.FindByValue(BinderModel.AppraiseLevel.ToString());
            hideImage.Value = BinderModel.AppraiseUrl;
            MineImage.ImageUrl = BinderModel.AppraiseUrl;
            if (SelectItem != null)
            {
                SelectItem.Selected = true;
            }
            if (BinderModel.AppraiseLevel > 0)
            {
                btnFinish.Visible = false;
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
            lblEmpLoyeeName.Text = GetEmployeeName(BinderModel.EmpLoyeeID);
            lblMissionName.Text = BinderModel.MissionName;
            // lblMissionType.Text = BinderModel.MissionType.ToString();
            txtBeginDate.Text = BinderModel.StarDate.ToString();
            txtCountdown.Text = BinderModel.Countdown.ToString();
            txtFinishNode.Text = BinderModel.FinishNode;
            txtPlanDate.Text = BinderModel.PlanDate.ToString();
            txtFinishDate.Text = BinderModel.FinishDate.ToString();
            if (BinderModel.MissionType <= 8)
            {
                var ObjChannelModel = ObjChannelBLL.GetByID(BinderModel.ChannelID);
                hideOpen.Value = GetMissionAddress(BinderModel.MissionType) + BinderModel.KeyWords + "&MissionID=" + BinderModel.MissionID + "&NeedPopu=1";
                hideLocation.Value = Request.UrlReferrer.ToString();
            }

            this.repfileList.DataSource = ObjMissionFileBLL.GetByMission(Request["DetailedID"].ToInt32()).Where(C => C.IsFinish == true);
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


            var OjbMissionModel = ObjMissionManagerBLL.GetByID(Request["DetailedID"].ToInt32());
            OjbMissionModel.AppraiseContent = txtAppraiseContent.Text;
            OjbMissionModel.AppraiseLevel = ddlAppraise.SelectedValue.ToInt32();
            OjbMissionModel.AppraiseUrl = hideImage.Value;
            
            ObjMissionManagerBLL.Update(OjbMissionModel);
            JavaScriptTools.AlertWindowAndLocation("评价完毕", "/AdminPanlWorkArea/Flows/Mission/FL_MyCreateMission.aspx", Page);

            //OjbMissionModel.PlanDate = txtPlanDate.Text.ToDateTime();
            //OjbMissionModel.StarDate = txtBeginDate.Text.ToDateTime();
            //OjbMissionModel.FinishNode = txtFinishNode.Text;
            //OjbMissionModel.IsLook = true;
            //OjbMissionModel.Countdown = txtCountdown.Text.ToInt32();

            //if (OjbMissionModel.MissionType <= 8)
            //{
            //    ObjMissionManagerBLL.Update(OjbMissionModel);
            //    var ObjChannelModel = ObjChannelBLL.GetByID(OjbMissionModel.ChannelID);
            //    hideOpen.Value = GetMissionAddress(OjbMissionModel.MissionType) + OjbMissionModel.KeyWords + "&MissionID=" + OjbMissionModel.MissionID + "&NeedPopu=1";
            //    hideLocation.Value = Request.UrlReferrer.ToString();
            //    //JavaScriptTools.do(GetMissionAddress(OjbMissionModel.MissionType) + OjbMissionModel.KeyWords + "&MissionID=" + OjbMissionModel.MissionID + "&NeedPopu=1", Page);

            //    // Response.Redirect(GetMissionAddress(OjbMissionModel.MissionType) + OjbMissionModel.KeyWords + "&MissionID=" + OjbMissionModel.MissionID + "&NeedPopu=1");
            //}
            //else
            //{
            //    if (txtFinishDate.Text != string.Empty)
            //    {
            //        OjbMissionModel.FinishDate = DateTime.Now;
            //        OjbMissionModel.IsLook = true;
            //        OjbMissionModel.IsOver = true;
            //        ObjMissionManagerBLL.Update(OjbMissionModel);
            //        var ObjChannelModel = ObjChannelBLL.GetByID(OjbMissionModel.ChannelID);
            //        JavaScriptTools.AlertWindow("处理完毕", Page);
            //    }
            //}
        }
    }
}