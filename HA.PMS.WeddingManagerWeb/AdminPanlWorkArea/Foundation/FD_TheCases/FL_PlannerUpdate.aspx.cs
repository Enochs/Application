using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases
{
    public partial class FL_PlannerUpdate : SystemPage
    {
        int PlannerID = 0;
        Planner ObjPlannerBLL = new Planner();
        PlannerType ObjPlannerTypeBLL = new PlannerType();

        Evaulation ObjEvaulationBLL = new Evaulation();

        #region 页面初始化

        protected void Page_Load(object sender, EventArgs e)
        {
            PlannerID = Request["PlannerID"].ToString().ToInt32();
            if (!IsPostBack)
            {
                DDLDataBind();
                DataBinder();
            }
        }
        #endregion

        public void DDLDataBind()
        {
            var DataList = ObjPlannerTypeBLL.GetByAll();
            ddlPlannerJob.DataSource = DataList;
            ddlPlannerJob.DataTextField = "TypeName";
            ddlPlannerJob.DataValueField = "TypeID";
            ddlPlannerJob.DataBind();
            ddlPlannerJob.Items.Add(new ListItem { Text = "请选择", Value = "0" });
        }

        #region 数据绑定


        public void DataBinder()
        {
            var Model = ObjPlannerBLL.GetByID(PlannerID);
            txtPlannerName.Text = Model.PlannerName;
            rdoSex.Items.FindByValue(Model.PlannerSex.ToString()).Selected = true;
            ddlPlannerJob.Items.FindByValue(Model.PlannerJob.ToString()).Selected = true;
            txtImage.Text = Model.PlannerImageName;
            txtPlannerSpecial.Text = Model.PlannerSpecial;
            txtPlannerJobDescription.Text = Model.PlannerJobDescription;
            txtPlannerIntrodution.Text = Model.PlannerIntrodution;
            txtRemark.Text = Model.Remark;
            int Delete = Model.IsDelete == false ? 0 : 1;
            rdoIsDelete.Items.FindByValue(Delete.ToString()).Selected = true;
            txtSort.Text = Model.Sort.ToString();

            var DataList = ObjEvaulationBLL.GetByPlannerID(Request["PlannerID"].ToInt32());     //代表作品
            if (DataList.Count > 0)
            {
                rptEvaulation.DataSource = DataList;
                rptEvaulation.DataBind();
            }
        }
        #endregion

        #region 点击确定事件


        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (ddlPlannerJob.SelectedItem.Text == "请选择")
            {
                JavaScriptTools.AlertWindow("请选择职位", Page);
                return;
            }
            var PlannerModel = ObjPlannerBLL.GetByID(PlannerID);
            string FileAddress = string.Empty;

            if (ImageUpload.HasFile)
            {
                FileAddress = "/AdminPanlWorkArea/Sys/Personnel/PersonnelImage/" + Guid.NewGuid().ToString() + ".jpg";

                if (System.IO.Directory.Exists(Server.MapPath("/AdminPanlWorkArea/Sys/Personnel/PersonnelImage/")))
                {
                    ImageUpload.SaveAs(Server.MapPath(FileAddress));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("/AdminPanlWorkArea/Sys/Personnel/PersonnelImage/"));
                    ImageUpload.SaveAs(Server.MapPath(FileAddress));
                }
                PlannerModel.PlannerImagePath = FileAddress;
                PlannerModel.PlannerImageName = ImageUpload.FileName.ToString();
            }

            PlannerModel.PlannerName = txtPlannerName.Text.ToString();
            PlannerModel.PlannerSex = rdoSex.SelectedValue.ToInt32();
            PlannerModel.PlannerJob = ddlPlannerJob.SelectedValue.ToInt32();
            PlannerModel.PlannerSpecial = txtPlannerSpecial.Text.ToString();
            PlannerModel.PlannerJobDescription = txtPlannerJobDescription.Text.ToString();
            PlannerModel.PlannerIntrodution = txtPlannerIntrodution.Text.ToString();
            PlannerModel.CreateDate = DateTime.Now.ToString().ToDateTime();
            PlannerModel.CreateEmployee = User.Identity.Name.ToInt32();
            PlannerModel.Remark = txtRemark.Text.ToString();
            PlannerModel.Sort = txtSort.Text.ToInt32();
            int value = rdoIsDelete.SelectedValue.ToInt32();
            if (value == 0)
            {
                PlannerModel.IsDelete = false;
            }
            else
            {
                PlannerModel.IsDelete = true;
            }

            int result = ObjPlannerBLL.Update(PlannerModel);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("修改成功", Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox1("修改失败,请稍候再试...", Page);
            }
        }
        #endregion

        #region 作品绑定事件  删除功能


        protected void rptEvaulation_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int EvalID = e.CommandArgument.ToString().ToInt32();
            var Model = ObjEvaulationBLL.GetByID(EvalID);
            if (e.CommandName == "Delete")
            {

                int result = ObjEvaulationBLL.Delete(Model);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("删除成功", Page);
                }

            }
            else if (e.CommandName == "Disable")
            {
                Model.IsShow = 0;
                int result = ObjEvaulationBLL.Update(Model);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("修改成功", Page);
                }
            }
            else if (e.CommandName == "Enable")
            {
                Model.IsShow = 1;
                int result = ObjEvaulationBLL.Update(Model);
                if (result > 0)
                {
                    JavaScriptTools.AlertWindow("修改成功", Page);
                }
            }
            DataBinder();

        }
        #endregion


    }
}