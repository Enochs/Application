
/**
 Version :HaoAi 1.0
 File Name :好爱1.0
 Author:杨洋
 Date:2013.3.21
 Description:知识库页面
 History:修改日志

 Author:杨洋
 date:2013.3.21
 version:好爱1.0
 description:修改描述
 
 
 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HA.PMS.BLLAssmblly;
using HA.PMS.BLLAssmblly.Flow;
using HA.PMS.ToolsLibrary;
using HA.PMS.Pages;
using HA.PMS.DataAssmblly;
using HA.PMS.BLLAssmblly.FD;
using HA.PMS.BLLAssmblly.Sys;
using HA.PMS.BLLAssmblly.Emnus;

namespace HA.PMS.WeddingManagerWeb.AdminPanlWorkArea.Foundation.FD_CelebrationKnowledge
{
    public partial class FD_CelebrationKnowledgeCreate :SystemPage
    {
        CelebrationKnowledge objCelebrationKnowledgeBLL = new CelebrationKnowledge();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBinder();
            }
        }

        /// <summary>
        /// 绑定下拉框数据源
        /// </summary>
        protected void DataBinder() 
        {
            ddlParent.DataSource = objCelebrationKnowledgeBLL.GetByAll();
            ddlParent.DataTextField = "KnowledgeTitle";
            ddlParent.DataValueField = "KnowledgeID";
            ddlParent.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            HA.PMS.DataAssmblly.FD_CelebrationKnowledge fdKnow = new DataAssmblly.FD_CelebrationKnowledge();
            fdKnow.KnowledgeContent = txtKnowContent.Text;
            if (chkTop.Checked)
            {
                fdKnow.ParentID = 0;//默认为父级
            }
            else
            {
                fdKnow.ParentID = ddlParent.SelectedValue.ToInt32();

            }
            fdKnow.IsDelete = false;
            fdKnow.KnowledgeTitle = txtKnowledgeTitle.Text;
            int result = objCelebrationKnowledgeBLL.Insert(fdKnow);
            if (result > 0)
            {
                JavaScriptTools.AlertAndClosefancybox("添加成功", this.Page);
            }
            else
            {
                JavaScriptTools.AlertAndClosefancybox("添加失败,请重新尝试", this.Page);

            }

        }
    }
}