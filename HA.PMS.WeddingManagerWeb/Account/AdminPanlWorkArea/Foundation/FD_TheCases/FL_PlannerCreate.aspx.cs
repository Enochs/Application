using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using HA.PMS.ToolsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases
{
    public partial class FL_PlannerCreate : SystemPage
    {
        Planner ObjPlannerManager = new Planner();

        PlannerType ObjPlannerTypeBLL = new PlannerType();

        Evaulation ObjEvaulationBLL = new Evaulation();

        #region 页面加载


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DDLDataBind();
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
            ddlPlannerJob.Items.FindByValue("0").Selected = true;
        }

        #region 点击确定

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (ddlPlannerJob.SelectedItem.Text == "请选择")
            {
                JavaScriptTools.AlertWindow("请选择职位", Page);
                return;
            }
            FL_Planner PlannerModel = new FL_Planner();
            #region 上传个人头像  身份证复印件
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
            else
            {
                JavaScriptTools.AlertWindow("不能上传这种类型的文件", Page);
                return;
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

            int result = ObjPlannerManager.Insert(PlannerModel);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox1("添加成功", Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox1("添加失败,请稍候再试...", Page);
            }

            #endregion

        }
        #endregion

    }
}