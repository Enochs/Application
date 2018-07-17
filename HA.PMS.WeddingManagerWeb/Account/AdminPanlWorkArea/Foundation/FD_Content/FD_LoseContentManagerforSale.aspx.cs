using HA.PMS.BLLAssmblly.FD;
using HA.PMS.DataAssmblly;
using HA.PMS.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.ToolsLibrary;
namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_Content
{
    public partial class FD_LoseContentManagerforSale : SystemPage
    {
        /// <summary>
        /// 流失原因
        /// </summary>
        LoseContent ObjLoseContentBLL = new LoseContent();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BinderData();
            }
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BinderData()
        {

            repLostContentList.DataSource = ObjLoseContentBLL.GetByType(2);
            repLostContentList.DataBind();

        }



        /// <summary>
        /// 新加流失原因
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            FD_LoseContent ObjLoseContentModel = new FD_LoseContent();
            ObjLoseContentModel.Title = txtTitle.Text;
            ObjLoseContentModel.LoseContent = txtContent.Text;

            ObjLoseContentModel.IsDelete = false;
            ObjLoseContentModel.Type = 2;
            ObjLoseContentBLL.Insert(ObjLoseContentModel);
            txtTitle.Text = string.Empty;
            BinderData();
            JavaScriptTools.AlertWindowAndReload("保存成功!",Page);
        }


        /// <summary>
        /// 修改流失原因
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void repLostContentList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                var UpdateModel = ObjLoseContentBLL.GetByID(e.CommandArgument.ToString().ToInt32());
                UpdateModel.LoseContent = ((TextBox)e.Item.FindControl("txtTitle")).Text;
                UpdateModel.Title = ((TextBox)e.Item.FindControl("txtTitle")).Text;
                ObjLoseContentBLL.Update(UpdateModel);
                BinderData();
                JavaScriptTools.AlertWindowAndReload("修改成功!", Page);

            }
        }
    }
}