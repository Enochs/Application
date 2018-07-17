using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
using HA.PMS.DataAssmblly;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_TheCases
{
    public partial class PlannerTypeManager : SystemPage
    {
        PlannerType ObjTypeBLL = new PlannerType();

        #region 页面加载

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }
        #endregion

        #region 数据绑定
        public void BinderData()
        {
            var DataList = ObjTypeBLL.GetAll();
            rptPlannerType.DataSource = DataList;
            rptPlannerType.DataBind();
        }
        #endregion

        #region 绑定事件 (修改 禁用 启用)


        protected void rptPlannerType_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int TypeID = e.CommandArgument.ToString().ToInt32();
            var Model = ObjTypeBLL.GetByID(TypeID);
            int result = 0;
            if (e.CommandName == "lbtnUpdate")
            {
                string TypeName = (e.Item.FindControl("txtTypeNames") as TextBox).Text;
                Model.TypeName = TypeName;
                result = ObjTypeBLL.Update(Model);

            }
            else if (e.CommandName == "lbtnDisble")
            {
                Model.IsDelete = true;
                result = ObjTypeBLL.Update(Model);
            }
            else if (e.CommandName == "lbtnEnable")
            {
                Model.IsDelete = false;
                result = ObjTypeBLL.Update(Model);
            }
            if (result > 0)
            {
                JavaScriptTools.AlertWindow("修改成功", Page);
            }
            BinderData();
        }
        #endregion

        #region 点击保存事件

        protected void btnConfirmSave_Click(object sender, EventArgs e)
        {
            FL_PlannerType TypeModel = new FL_PlannerType();
            TypeModel.TypeName = txtTypeName.Text;
            TypeModel.IsDelete = false;
            int result = ObjTypeBLL.Insert(TypeModel);
            if (result > 0)
            {
                JavaScriptTools.AlertWindow("新增成功", Page);
            }
            else
            {
                JavaScriptTools.AlertWindow("新增失败,请稍候再试...", Page);
            }
            BinderData();
        }
        #endregion

        #region 获取状态 是否删除

        public string GetState(object Source)
        {
            var IsDelete = Source.ToString().ToBool();
            if (IsDelete == false)
            {
                return "启用";
            }
            else
            {
                return "禁用";
            }
        }
        #endregion
    }
}